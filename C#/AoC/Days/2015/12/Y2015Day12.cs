using System.Text.Json;
using System.Text.RegularExpressions;

namespace AoC.Days;

internal partial class Y2015Day12 : Day
{
    protected override void RunLogic(string[] inputLines)
    {
        string json = inputLines[0];
        json = json.Replace(" ", "");
        int numberSum = MatchNumbers().Matches(json)
            .Aggregate(0, (acc, match) =>
            {
                return acc += int.Parse(match.Value);
            });
        AnswerOne = numberSum.ToString();
        var jsonParsed = JsonDocument.Parse(json);
        JsonElement root = jsonParsed.RootElement;
        int AnswerTwoNum = ProcessElement(root);
        AnswerTwo = AnswerTwoNum.ToString();
    }

    private int ProcessElement(JsonElement currentElement)
    {
        int elementValue = 0;
        switch (currentElement.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var property in currentElement.EnumerateObject())
                {
                    if (property.Value.ValueKind == JsonValueKind.String && property.Value.GetString() == "red")
                    {
                        return 0;
                    }
                    elementValue += ProcessElement(property.Value);
                }
                break;

            case JsonValueKind.Array:
                foreach (var arrayItem in currentElement.EnumerateArray())
                {
                    elementValue += ProcessElement(arrayItem);
                }
                break;

            case JsonValueKind.String:
                break;

            case JsonValueKind.Number:
                elementValue += currentElement.GetInt32();
                break;
        }
        return elementValue;
    }

    [GeneratedRegex(@"(-?\d+)")]
    private static partial Regex MatchNumbers();

    [GeneratedRegex(@"(""[a-zA-Z]"":""red"")")]
    private static partial Regex MatchRedProperties();

    private int RegexParserMethod(string json)
    {
        var RedMatches = MatchRedProperties().Matches(json);
        HashSet<StringIndexPair> redObjectIndices = [];
        foreach (Match redMatch in RedMatches)
        {
            StringIndexPair indices = ParseRedObject(json, redMatch);
            redObjectIndices.Add(indices);
        }

        bool ObjectPrunedInLoop = true;
        while (ObjectPrunedInLoop)
        {
            ObjectPrunedInLoop = false;
            var indicesCopy = redObjectIndices.ToHashSet();
            foreach (StringIndexPair RedObjectIndexPair in indicesCopy)
            {
                if (ObjectPrunedInLoop)
                { continue; }
                foreach (StringIndexPair otherObject in indicesCopy.Except([RedObjectIndexPair]))
                {
                    if (RedObjectIndexPair.start <= otherObject.start && RedObjectIndexPair.end >= otherObject.end)
                    {
                        redObjectIndices.Remove(otherObject);
                        ObjectPrunedInLoop = true;
                        break;
                    }
                    else if (RedObjectIndexPair.start > otherObject.start && RedObjectIndexPair.end < otherObject.end)
                    {
                        redObjectIndices.Remove(RedObjectIndexPair);
                        ObjectPrunedInLoop = true;
                        break;
                    }
                }
            }
        }
        List<string> redObjects = [];
        foreach (StringIndexPair indexPair in redObjectIndices)
        {
            string redObject = json.Substring(indexPair.start, indexPair.end - indexPair.start + 1);
            redObjects.Add(redObject);
        }
        int RedObjectSum = redObjects
                .Aggregate(0, (acc1, match1) =>
                {
                    return acc1 += MatchNumbers()
                        .Matches(match1)
                        .Aggregate(0, (acc2, match2) =>
                        {
                            return acc2 += int.Parse(match2.Value);
                        });
                });
        return RedObjectSum;
    }

    private StringIndexPair ParseRedObject(string json, Match redStringMatch)
    {
        int lastOpeningBrace = -1;
        int cursorIndex = redStringMatch.Index;
        while (lastOpeningBrace == -1)
        {
            cursorIndex--;
            if (json[cursorIndex] == '{')
            { lastOpeningBrace = cursorIndex; }
        }

        cursorIndex = redStringMatch.Index;
        int braceLevel = 1;
        while (braceLevel > 0)
        {
            cursorIndex++;
            if (json[cursorIndex] == '}')
            { braceLevel--; }
            else if (json[cursorIndex] == '{')
            { braceLevel++; }
        }

        return new(lastOpeningBrace, cursorIndex);
    }
}
