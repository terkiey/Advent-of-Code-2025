namespace AoC.Days;

internal class Robot
{
    private List<int> numbers = new(2);
    public int? High 
    {
        get => numbers.Count > 0 ? numbers.Max() : null; 
    }
    public int? Low
    {
        get => numbers.Count > 0 ? numbers.Min() : null;
    }

    public Robot()
    {

    }

    public Robot(int numOne)
    {
        numbers.Add(numOne);
    }

    public Robot(int numOne, int numTwo)
    {
        numbers.AddRange([numOne, numTwo]);
    }

    public void Give(int? number)
    {
        if (number == null)
        {
            throw new Exception("Only should be giving non-null values!");
        }
        if (numbers.Count == 2)
        {
            throw new Exception("robot should only have two numbers");
        }

        numbers.Add(number.Value);
    }

    public bool IsReadyForComparison()
    {
        return numbers.Count == 2;
    }
}
