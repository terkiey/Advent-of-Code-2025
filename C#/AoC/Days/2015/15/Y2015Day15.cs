namespace AoC.Days;

internal class Y2015Day15 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        const int teaSpoonsRemaining = 100;
        const int propertyTypes = 4;
        int[][] ingredients = new int[inputLines.Length][];
        for (int ingredientIndex = 0; ingredientIndex < ingredients.Length; ingredientIndex++)
        {
            string line = inputLines[ingredientIndex];
            string[] ingredientProperties = line.Split(':')[1]
                                                .Split(',');

            int[] ingredient = new int[ingredientProperties.Length];
            for (int propertyIndex = 0; propertyIndex < ingredientProperties.Length; propertyIndex++)
            {
                string[] splitProperty = ingredientProperties[propertyIndex].Split(' ');
                ingredient[propertyIndex] = int.Parse(splitProperty[2]);
            }
            ingredients[ingredientIndex] = ingredient;
        }

        int maxCookieScore = 0;
        int[] propertyScores = new int[propertyTypes];
        for (int firstIngredientSpoons = 0; firstIngredientSpoons <= teaSpoonsRemaining;  firstIngredientSpoons++)
        {
            int ingredientCount = firstIngredientSpoons;
            for (int propertyIndex = 0; propertyIndex < propertyTypes;  propertyIndex++)
            {
                propertyScores[propertyIndex] += firstIngredientSpoons * ingredients[0][propertyIndex];
            }
            for (int secondIngredientSpoons = 0; secondIngredientSpoons <= teaSpoonsRemaining - ingredientCount; secondIngredientSpoons++)
            {
                ingredientCount += secondIngredientSpoons;
                for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                {
                    propertyScores[propertyIndex] += secondIngredientSpoons * ingredients[1][propertyIndex];
                }
                for (int thirdIngredientSpoons = 0; thirdIngredientSpoons <= teaSpoonsRemaining - ingredientCount; thirdIngredientSpoons++)
                {
                    ingredientCount += thirdIngredientSpoons;
                    for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                    {
                        propertyScores[propertyIndex] += thirdIngredientSpoons * ingredients[2][propertyIndex];
                    }
                    int fourthIngredientSpoons = teaSpoonsRemaining - ingredientCount;
                    for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                    {
                        propertyScores[propertyIndex] += fourthIngredientSpoons * ingredients[3][propertyIndex];
                    }
                    int cookieScore = propertyScores.Aggregate(1, (acc, score) =>
                    {
                        return acc *= score > 0 ? score : 0;
                    });
                    maxCookieScore = cookieScore > maxCookieScore ? cookieScore : maxCookieScore;
                    for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                    {
                        propertyScores[propertyIndex] -= fourthIngredientSpoons * ingredients[3][propertyIndex];
                    }
                    for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                    {
                        propertyScores[propertyIndex] -= thirdIngredientSpoons * ingredients[2][propertyIndex];
                    }
                    ingredientCount -= thirdIngredientSpoons;
                }
                for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                {
                    propertyScores[propertyIndex] -= secondIngredientSpoons * ingredients[1][propertyIndex];
                }
                ingredientCount -= secondIngredientSpoons;
            }
            for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
            {
                propertyScores[propertyIndex] -= firstIngredientSpoons * ingredients[0][propertyIndex];
            }
            ingredientCount -= firstIngredientSpoons;
        }
        AnswerOne = maxCookieScore.ToString();

        maxCookieScore = 0;
        propertyScores = new int[propertyTypes];
        int calories = 0;
        for (int firstIngredientSpoons = 0; firstIngredientSpoons <= teaSpoonsRemaining; firstIngredientSpoons++)
        {
            int ingredientCount = firstIngredientSpoons;
            calories += ingredients[0][4] * firstIngredientSpoons;
            for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
            {
                propertyScores[propertyIndex] += firstIngredientSpoons * ingredients[0][propertyIndex];
            }
            for (int secondIngredientSpoons = 0; secondIngredientSpoons <= teaSpoonsRemaining - ingredientCount; secondIngredientSpoons++)
            {
                ingredientCount += secondIngredientSpoons;
                calories += ingredients[1][4] * secondIngredientSpoons;
                for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                {
                    propertyScores[propertyIndex] += secondIngredientSpoons * ingredients[1][propertyIndex];
                }
                for (int thirdIngredientSpoons = 0; thirdIngredientSpoons <= teaSpoonsRemaining - ingredientCount; thirdIngredientSpoons++)
                {
                    ingredientCount += thirdIngredientSpoons;
                    calories += ingredients[2][4] * thirdIngredientSpoons;
                    for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                    {
                        propertyScores[propertyIndex] += thirdIngredientSpoons * ingredients[2][propertyIndex];
                    }
                    int fourthIngredientSpoons = teaSpoonsRemaining - ingredientCount;
                    calories += ingredients[3][4] * fourthIngredientSpoons;
                    for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                    {
                        propertyScores[propertyIndex] += fourthIngredientSpoons * ingredients[3][propertyIndex];
                    }
                    int cookieScore = propertyScores.Aggregate(1, (acc, score) =>
                    {
                        return acc *= score > 0 ? score : 0;
                    });
                    if (calories == 500)
                    {
                        maxCookieScore = cookieScore > maxCookieScore ? cookieScore : maxCookieScore;
                    }
                    for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                    {
                        propertyScores[propertyIndex] -= fourthIngredientSpoons * ingredients[3][propertyIndex];
                    }
                    for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                    {
                        propertyScores[propertyIndex] -= thirdIngredientSpoons * ingredients[2][propertyIndex];
                    }
                    calories -= ingredients[3][4] * fourthIngredientSpoons;
                    calories -= ingredients[2][4] * thirdIngredientSpoons;
                    ingredientCount -= thirdIngredientSpoons;
                }
                for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
                {
                    propertyScores[propertyIndex] -= secondIngredientSpoons * ingredients[1][propertyIndex];
                }
                calories -= ingredients[1][4] * secondIngredientSpoons;
                ingredientCount -= secondIngredientSpoons;
            }
            for (int propertyIndex = 0; propertyIndex < propertyTypes; propertyIndex++)
            {
                propertyScores[propertyIndex] -= firstIngredientSpoons * ingredients[0][propertyIndex];
            }
            calories -= ingredients[0][4] * firstIngredientSpoons;
            ingredientCount -= firstIngredientSpoons;
        }
        AnswerTwo = maxCookieScore.ToString();
    }
}
