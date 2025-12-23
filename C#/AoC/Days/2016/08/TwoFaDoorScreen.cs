using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Days;

internal class TwoFaDoorScreen
{
    public bool[][] screen { get; }

    public TwoFaDoorScreen(int screenWidth, int screenHeight)
    {
        screen = new bool[screenWidth][];
        for (int x = 0; x < screenWidth; x++)
        {
            screen[x] = new bool[screenHeight];
        }
    }

    public void RunCommands(string[] commands)
    {
        List<ScreenCommand> commandList = [];
        foreach (string command in commands)
        {
            string commandType = "bruh";
            int[] args = [-1];
            string[] splitCommand = command.Split(' ');
            if (splitCommand[0] == "rect")
            {
                string[] dims = splitCommand[1].Split('x');
                int width = int.Parse(dims[0]);
                int height = int.Parse(dims[1]);
                commandType = "rect";
                args = [width, height];
            }
            else if (splitCommand[0] == "rotate")
            {
                string rotateType = splitCommand[1];
                int rotateIndex = int.Parse(splitCommand[2].Split('=')[1]);
                int rotatePower = int.Parse(splitCommand[4]);
                
                commandType = rotateType;
                args = [rotateIndex, rotatePower];
            }
            commandList.Add(new(commandType, args));
        }

        foreach (ScreenCommand command in commandList)
        {
            RunScreenCommand(command);
        }
    }

    public string ScreenAsString()
    {
        StringBuilder sb = new();
        for (int x = screen.Length - 1; x >= 0; x--)
        {
            bool[] screenCol = screen[x];
            for (int y = 0; y < screenCol.Length; y++)
            {
                bool pixel = screenCol[y];
                if (pixel)
                {
                    sb.Append('#');
                }
                else
                {
                    sb.Append('.');
                }
                
            }
            sb.Append('\n');
        }
        return sb.ToString();
    }

    public int CountOnPixels()
    {
        int count = 0;
        foreach (bool[] pixels in screen)
        {
            foreach (bool pixel in pixels)
            {
                count += pixel ? 1 : 0;
            }
        }
        return count;
    }

    private void RunScreenCommand(ScreenCommand command)
    {
        switch (command.CommandType)
        {
            case "rect":
                DrawRectangle(command.Args[0], command.Args[1]);
                break;

            case "row":
                RotateRow(command.Args[0], command.Args[1]);
                break;

            case "column":
                RotateColumn(command.Args[0], command.Args[1]);
                break;
        }
    }

    private void DrawRectangle(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                screen[x][y] = true;
            }
        }
    }

    private void RotateRow(int y, int shiftPower)
    {
        bool[] newRow = new bool[screen.Length];
        for (int x = 0; x < newRow.Length; x++)
        {
            int destinationIndex = (x + shiftPower) % newRow.Length;
            newRow[destinationIndex] = screen[x][y];
        }

        for (int x = 0; x < newRow.Length; x++)
        {
            screen[x][y] = newRow[x];
        }
    }

    private void RotateColumn(int x, int shiftPower)
    {
        bool[] newCol = new bool[screen[0].Length];
        for (int y = 0; y < newCol.Length; y++)
        {
            int destinationIndex = (y + shiftPower) % newCol.Length;
            newCol[destinationIndex] = screen[x][y];
        }

        for (int y = 0; y < newCol.Length; y++)
        {
            screen[x][y] = newCol[y];
        }
    }
}
