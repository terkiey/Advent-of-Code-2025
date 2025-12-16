namespace AoC.Days;

internal class Y2015Day13 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        List<DinnerGuest> guests = [];
        foreach(string line in inputLines)
        {
            string[] splitLine = line.Split(' ');
            string dinerName = splitLine[0];
            int happinessValue;
            switch (splitLine[2])
            {
                case "gain":
                    happinessValue = int.Parse(splitLine[3]);
                    break;

                case "lose":
                    happinessValue = int.Parse(splitLine[3]) * -1;
                    break;

                default:
                    throw new Exception("expecting gain and lose only");
            }
            string adjacentDinerName = splitLine[10].Replace(".","");
            bool guestProcessed = false;
            foreach (DinnerGuest guest in guests)
            {
                if (guest.Name == dinerName)
                {
                    guest.Preferences.Add(adjacentDinerName, happinessValue);
                    guestProcessed = true;
                    break;
                }
            }

            if (!guestProcessed)
            {
                DinnerGuest newGuest = new(dinerName, []);
                newGuest.Preferences.Add(adjacentDinerName, happinessValue);
                guests.Add(newGuest);
            }
        }

        int happinessTotal = int.MinValue;
        foreach (IEnumerable<DinnerGuest> permutation in Permutations(guests, guests.Count()))
        {
            happinessTotal = GetHappiness(permutation) > happinessTotal ? GetHappiness(permutation) : happinessTotal;
        }
        AnswerOne = happinessTotal.ToString();

        DinnerGuest me = new("Jack", []);
        foreach (DinnerGuest otherGuest in guests)
        {
            me.Preferences.Add(otherGuest.Name, 0);
            otherGuest.Preferences.Add("Jack", 0);
        }
        guests.Add(me);

        happinessTotal = int.MinValue;
        foreach (IEnumerable<DinnerGuest> permutation in Permutations(guests, guests.Count()))
        {
            happinessTotal = GetHappiness(permutation) > happinessTotal ? GetHappiness(permutation) : happinessTotal;
        }
        AnswerTwo = happinessTotal.ToString();
    }

    private IEnumerable<IEnumerable<DinnerGuest>> Permutations(IEnumerable<DinnerGuest> list, int length)
    {
        return length == 1
            ? list.Select(t => new[] { t })
            : Permutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t, o) => t.Append(o));
    }

    private int GetHappiness(IEnumerable<DinnerGuest> permutation)
    {
        List<DinnerGuest> guestsInOrder = permutation.ToList();
        int happinessTotal = 0;
        for(int guestIndex = 0; guestIndex < guestsInOrder.Count; guestIndex++)
        {
            DinnerGuest currentGuest = guestsInOrder[guestIndex];

            int firstAdjacentIndex = guestIndex == 0 ? guestsInOrder.Count - 1 : guestIndex - 1;
            int secondAdjacentIndex = guestIndex == guestsInOrder.Count - 1 ? 0 : guestIndex + 1;

            string firstAdjGuestName = guestsInOrder[firstAdjacentIndex].Name;
            string secondAdjGuestName = guestsInOrder[secondAdjacentIndex].Name;

            happinessTotal += currentGuest.Preferences[firstAdjGuestName];
            happinessTotal += currentGuest.Preferences[secondAdjGuestName];
        }
        return happinessTotal;
    }

}
