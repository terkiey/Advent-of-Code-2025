using System.Runtime.CompilerServices;

namespace AoC.Days;

internal class WeatherMachineCodeCalculator
{
    private const long divisor = 33554393;
    private const long multiplicand = 252533;
    private const long startFigure = 20151125;

    public long CalculateCodeAt(long x, long y)
    {
        // I just did the algebra (sum of first x values etc.)
        long index = ((x*x) + (2*x*y) - x + (y*y) - (3*y) + 2) / 2;

        long code = startFigure;
        for (int i = 2; i <= index; i++)
        {
            code = (code * multiplicand) % divisor;
        }
        return code;
    }
}
