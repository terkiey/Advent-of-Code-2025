namespace AoC.Days;

internal class Y2016Day07 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        IpAddressAnalyser analyser = new();
        AnswerOne = analyser.CountTlsSupportingIps(inputLines).ToString();
        AnswerTwo = analyser.CountSslSupportingIps(inputLines).ToString();
    }
}
