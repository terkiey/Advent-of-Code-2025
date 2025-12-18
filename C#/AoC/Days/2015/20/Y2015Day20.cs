namespace AoC.Days;

internal class Y2015Day20 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        var houseCalculator = new HousePresentCalculator();
        int presentsNeeded = int.Parse(inputLines[0]);
        bool notEnoughPresents = true;
        int houseNumber = 0;
        while (notEnoughPresents == true)
        {
            houseNumber++;
            if(houseCalculator.PresentCount(houseNumber) >= presentsNeeded)
            {
                notEnoughPresents = false;
            }
        }
        AnswerOne = houseNumber.ToString();

        houseCalculator = new HousePresentCalculator();
        notEnoughPresents = true;
        houseNumber = 0;
        while (notEnoughPresents == true)
        {
            houseNumber++;
            if (houseCalculator.LazyElfPresentCount(houseNumber) >= presentsNeeded)
            {
                notEnoughPresents = false;
            }
        }
        AnswerTwo = houseNumber.ToString();
    }
}
