using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace AoC.Days;

internal class Y2015Day11 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        string currentPassword = inputLines[0];
        currentPassword = "hxbxwxba";
        while (!IsPasswordValid(currentPassword))
        {
            currentPassword = IncrementPassword(currentPassword);
        }
        AnswerOne = currentPassword;
        currentPassword = IncrementPassword(currentPassword);
        while (!IsPasswordValid(currentPassword))
        {
            currentPassword = IncrementPassword(currentPassword);
        }
        AnswerTwo = currentPassword;
    }

    private bool IsPasswordValid(string password)
    {
        int pairs = 0;
        bool increasingStraight = false;
        bool pairCooldown = false;
        for (int charIndex = 0; charIndex < password.Length; charIndex++)
        {
            char charOne = password[charIndex];
            if (charOne == 'i' || charOne == 'o' || charOne == 'l')
            {
                return false;
            }

            if (charIndex >= password.Length - 1) 
            { continue; }
            char charTwo = password[charIndex + 1];
            if (!pairCooldown && charOne == charTwo)
            {
                pairs++; 
                pairCooldown = true;
            }
            else { pairCooldown = false; }

            if (charIndex >= password.Length - 2) 
            { continue; }
            char charThree = password[charIndex + 2];
            if (increasingStraight == false && charOne != 'y' && charOne != 'z' && charTwo != 'z' && charThree == IncrementLetter(charTwo) && charTwo == IncrementLetter(charOne))
            {
                increasingStraight = true;
            }
        }
        return pairs > 1 && increasingStraight;
    }

    private char IncrementLetter(char character)
    {
        character++;
        if (character > 'z')
        {
            return 'a';
        }
        return character;
    }

    private string IncrementPassword(string password)
    {
        if(TryShortcutBadLettersIncrement(password, out string incrementedPassword))
        {
            return incrementedPassword;
        }

        if (password.Length == 1) { return IncrementLetter(password[0]).ToString(); }

        char[] chars = password.ToCharArray();
        if (chars[password.Length - 1] == 'z')
        {
            char[] iterChars = new char[password.Length - 1];
            for (int charIndex = 0; charIndex < chars.Length - 1;  charIndex++)
            {
                iterChars[charIndex] = chars[charIndex];
            }
            string iterString = new string(iterChars);
            iterString = IncrementPassword(iterString);

            return iterString + "a";
        }

        chars[password.Length - 1] = IncrementLetter(chars[password.Length - 1]);
        return new string(chars);
    }

    private bool TryShortcutBadLettersIncrement(string password, out string incrementedPassword)
    {
        int shortcutIndex = password.IndexOfAny(['i', 'o', 'l']);
        if (shortcutIndex == -1) 
        { incrementedPassword = string.Empty; return false; }

        string unchangedPart = password.Substring(0, shortcutIndex);
        char shortcutChar = IncrementLetter(password[shortcutIndex]);
        string remainderPart = new string(Enumerable.Repeat('a', password.Length - shortcutIndex - 1).ToArray());
        incrementedPassword = unchangedPart + shortcutChar.ToString() + remainderPart;
        return true;
    }
}
