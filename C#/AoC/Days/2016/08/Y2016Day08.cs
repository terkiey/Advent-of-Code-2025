namespace AoC.Days;

internal class Y2016Day08 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        int screenWidth = -1;
        int screenHeight = -1;
        if (RunParameter == 1)
        {
            screenWidth = 50;
            screenHeight = 6;
        }
        else if (RunParameter == 2)
        {
            screenWidth = 8;
            screenHeight = 3;
        }

        TwoFaDoorScreen screen = new(screenWidth, screenHeight);
        screen.RunCommands(inputLines);
        AnswerOne = screen.CountOnPixels().ToString();
        AnswerTwo = "Read this from the bottom to the top (tilt your head left) \n" + screen.ScreenAsString();
    }
}
