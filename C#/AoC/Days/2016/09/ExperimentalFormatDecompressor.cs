using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Days;

internal partial class ExperimentalFormatDecompressor
{
    public int DecompressAndCountLength(string[] compressedLines)
    {
        int count = 0;
        foreach (string line in compressedLines)
        {
            string decompressedLine = Decompress(line.Trim());
            count += decompressedLine.Length;
        }
        return count;
    }

    public long CalculateLengthFormatVersionTwo(string[] compressedLines)
    {
        long count = 0;
        foreach (string line in compressedLines)
        {
            long decompressedLineLength = CalculateLengthWithVersionTwo(line.Trim());
            count += decompressedLineLength;
        }
        return count;
    }

    private long CalculateLengthWithVersionTwo(string compressedLine)
    {
        MatchCollection markers = MarkerPattern().Matches(compressedLine);
        long length = 0;

        if (markers.Count == 0)
        {
            return compressedLine.Length;
        }

        for (int pointer = 0; pointer < compressedLine.Length; pointer++)
        {
            if (pointer >= compressedLine.Length - 4)
            {
                length++;
                pointer++;
                continue;
            }

            bool markerFound = false;
            foreach (Match marker in markers)
            {
                if (marker.Index == pointer)
                {
                    markerFound = true;
                    pointer += marker.Length;
                    int readLength = int.Parse(marker.Groups[1].Value);
                    int repetitions = int.Parse(marker.Groups[2].Value);

                    StringBuilder sb = new();
                    string section;
                    if (readLength > compressedLine.Length - pointer)
                    {
                        section = compressedLine.Substring(pointer);
                    }
                    else
                    {
                        section = compressedLine.Substring(pointer, readLength);
                    }

                    for (int repetition = 0; repetition < repetitions; repetition++)
                    {
                        sb.Append(section);
                    }

                    length += CalculateLengthWithVersionTwo(sb.ToString());

                    pointer += readLength - 1;
                    break;
                }
            }

            if (pointer >= compressedLine.Length)
            {
                return length;
            }

            if (!markerFound)
            {
                length++;
            }
        }
        return length;
    }

    private string Decompress(string compressedLine)
    {
        MatchCollection markers = MarkerPattern().Matches(compressedLine);
        StringBuilder sb = new();

        if (markers.Count == 0)
        {
            return compressedLine;
        }

        for (int pointer = 0; pointer < compressedLine.Length; pointer++)
        {
            if (pointer >= compressedLine.Length - 4)
            {
                sb.Append(compressedLine[pointer]);
                pointer++;
                continue;
            }

            bool markerFound = false;
            foreach (Match marker in markers)
            {
                if (marker.Index == pointer)
                {
                    markerFound = true;
                    pointer += marker.Length;
                    int readLength = int.Parse(marker.Groups[1].Value);
                    int repetitions = int.Parse(marker.Groups[2].Value);

                    string section;
                    if (readLength > compressedLine.Length - pointer)
                    {
                        section = compressedLine.Substring(pointer);
                    }
                    else
                    {
                        section = compressedLine.Substring(pointer, readLength);
                    }

                    for (int repetition = 0; repetition < repetitions; repetition++)
                    {
                        sb.Append(section);
                    }

                    pointer += readLength - 1;
                    break;
                }
            }

            if (pointer >= compressedLine.Length)
            {
                return sb.ToString();
            }

            if (!markerFound)
            {
                sb.Append(compressedLine[pointer]);
            }
        }
        return sb.ToString();
    }

    [GeneratedRegex(@"\((\d+)x(\d+)\)")]
    private static partial Regex MarkerPattern();
}
