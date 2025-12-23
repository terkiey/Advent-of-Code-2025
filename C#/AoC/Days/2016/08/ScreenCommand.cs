namespace AoC.Days;

internal class ScreenCommand
{
    public string CommandType { get; set; } = String.Empty;
    public int[] Args { get; set; } = [];

    public ScreenCommand(string commandType, int[] args)
    {
        CommandType = commandType;
        Args = args;
    }
}
