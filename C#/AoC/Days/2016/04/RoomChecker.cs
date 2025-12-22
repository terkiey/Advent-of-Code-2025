using System.Text;

namespace AoC.Days;

internal class RoomChecker
{
    private readonly Dictionary<char, int> charCount = [];

    public int SumValidSectorIds(string[] roomList)
    {
        int sectorSum = 0;
        foreach(string room in roomList)
        {
            sectorSum += RoomIsValid(room) ? GetSectorId(room) : 0;
        }
        return sectorSum;
    }

    public string NorthPoleRoomCandidates(string[] roomList)
    {
        StringBuilder sb = new();
        sb.AppendLine("A human must read through these to find the right sector!");
        foreach(string room in roomList)
        {
            if (!RoomIsValid(room))
            {
                continue;
            }

            int sector = GetSectorId(room);
            string roomName = Decrypt(room, sector);
            if (roomName.Contains("north"))
            {
                sb.AppendLine($"Sector {sector} = {roomName}");
            }
        }
        return sb.ToString();
    }

    private string Decrypt(string room, int sector)
    {
        int letterRange = 'z' - 'a';
        int shiftAmount = sector % (letterRange + 1);
        string[] words = room.Split('-');

        StringBuilder sb = new();
        for (int wordIndex = 0; wordIndex < words.Length -1; wordIndex++)
        {
            char[] letters = words[wordIndex].ToCharArray();
            for (int letterIndex = 0; letterIndex < letters.Length; letterIndex++)
            {
                char letter = letters[letterIndex];
                letters[letterIndex] = (char) (((letter - 'a' + shiftAmount) % (letterRange + 1)) + 'a');
            }
            string newWord = new string(letters);
            sb.Append(newWord + " ");
        }
        return sb.ToString();
    }

    private bool RoomIsValid(string room)
    {
        charCount.Clear();
        string[] roomSegments = room.Split('-');
        for (int segmentIndex = 0; segmentIndex < roomSegments.Length - 1; segmentIndex++)
        {
            foreach(char letter in roomSegments[segmentIndex])
            {
                charCount[letter] = charCount.GetValueOrDefault(letter) + 1;
            }
        }

        string roomSum = GetCurrentCheckSum();
        string checkSum = roomSegments[^1].Split('[')[1][..5];
        return roomSum == checkSum;
    }

    private string GetCurrentCheckSum()
    {
        PriorityQueue<char, (int count, int characterPrio)> charHeap = new();
        int minimalCount = int.MaxValue;
        foreach (char letter in charCount.Keys)
        {
            int letterCount = charCount[letter];
            if(charHeap.Count < 5)
            {
                charHeap.Enqueue(letter, (letterCount, letter * -1));
                minimalCount = Math.Min(letterCount, minimalCount);
                continue;
            }

            if(letterCount > minimalCount)
            {
                _ = charHeap.Dequeue();
                charHeap.Enqueue(letter, (letterCount, letter * -1));
                _ = charHeap.TryPeek(out char _, out (int minCount, int _) minPrio);
                minimalCount = minPrio.minCount;
                continue;
            }

            if (letterCount == minimalCount && charHeap.Peek() > letter )
            {
                _ = charHeap.Dequeue();
                charHeap.Enqueue(letter, (letterCount, letter * -1));
                continue;
            }
        }

        char[] letters = new char[5];
        for (int letterIndex = 4; letterIndex >= 0; letterIndex--)
        {
            letters[letterIndex] = charHeap.Dequeue();
        }
        return new string(letters);
    }

    private int GetSectorId(string room)
    {
        string[] roomSegments = room.Split('-');
        return int.Parse(roomSegments[^1].Split('[')[0]);
    }
}
