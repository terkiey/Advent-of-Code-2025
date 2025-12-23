namespace AoC.Days;

internal class MicrochipComparison
{
    public int RobotIndex { get; set; }
    public string LowTo { get; set; } = string.Empty;
    public string HighTo { get; set; } = string.Empty;

    public MicrochipComparison(int robotIndex, string lowTo, string highTo)
    {
        RobotIndex = robotIndex;
        LowTo = lowTo;
        HighTo = highTo;
    }
}
