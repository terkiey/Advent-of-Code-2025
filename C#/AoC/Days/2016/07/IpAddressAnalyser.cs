using System.Text;
using System.Text.RegularExpressions;

namespace AoC.Days;

internal partial class IpAddressAnalyser
{
    public int CountTlsSupportingIps(string[] ipAddresses)
    {
        int count = 0;
        foreach (string ipAddress in ipAddresses)
        {
            if (IsTlsSupporting(ipAddress))
            {
                count++;
            }
        }
        return count;
    }

    public int CountSslSupportingIps(string[] ipAddresses)
    {
        int count = 0;
        foreach (string ipAddress in ipAddresses)
        {
            if (IsSslSupporting(ipAddress))
            {
                count++;
            }
        }
        return count;
    }

    private bool IsTlsSupporting(string ipAddress)
    {
        MatchCollection segments = ipSegmentPattern().Matches(ipAddress);
        int index = 0;
        List<string> ipSegments = [];
        List<string> hypernetSeqs = [];
        foreach (Match segment in segments)
        {
            if(index % 2 == 0)
            {
                ipSegments.Add(segment.Value);
            }
            else
            {
                hypernetSeqs.Add(segment.Value);
            }
            index++;
        }

        foreach (string hypernetSeq in hypernetSeqs)
        {
            if (ContainsAbbaSequence(hypernetSeq))
            {
                return false;
            }
        }

        foreach (string ipSegment in ipSegments)
        {
            if (ContainsAbbaSequence(ipSegment))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsSslSupporting(string ipAddress)
    {
        MatchCollection segments = ipSegmentPattern().Matches(ipAddress);
        int index = 0;
        List<string> ipSegments = [];
        List<string> hypernetSeqs = [];
        foreach (Match segment in segments)
        {
            if (index % 2 == 0)
            {
                ipSegments.Add(segment.Value);
            }
            else
            {
                hypernetSeqs.Add(segment.Value);
            }
            index++;
        }

        HashSet<string> abaSequences = [];
        foreach (string ipSegment in ipSegments)
        {
            HashSet<string> abaSeqs = GetAbaSequences(ipSegment);
            abaSequences.UnionWith(abaSeqs);
        }

        foreach (string hypernetSeq in hypernetSeqs)
        {
            foreach(string abaSeq in abaSequences)
            {
                if (ContainsBabSequence(hypernetSeq, abaSeq))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool ContainsBabSequence(string ipSegment, string abaSeq)
    {
        if (ipSegment.Length < 3)
        {
            return false;
        }
        string babSeq = new([abaSeq[1], abaSeq[0], abaSeq[1]]);
        if (ipSegment.Contains(babSeq))
        {
            return true;
        }
        return false;
    }

    private HashSet<string> GetAbaSequences(string ipSegment)
    {
        HashSet<string> seqs = [];
        if (ipSegment.Length < 3)
        {
            return seqs;
        }
        for (int letterIndex = 0; letterIndex < ipSegment.Length - 2; letterIndex++)
        {
            if (ipSegment[letterIndex] != ipSegment[letterIndex + 1] &&
            ipSegment[letterIndex] == ipSegment[letterIndex + 2])
            {
                string seq = new ([ipSegment[letterIndex], ipSegment[letterIndex + 1], ipSegment[letterIndex + 2]]);
                seqs.Add(seq);
            }
        }
        return seqs;
    }

    private bool ContainsAbbaSequence(string ipSegment)
    {
        if (ipSegment.Length < 4)
        {
            return false;
        }

        for (int letterIndex = 0; letterIndex < ipSegment.Length - 3; letterIndex++)
        {
            if (ipSegment[letterIndex] != ipSegment[letterIndex + 1] && 
            ipSegment[letterIndex] == ipSegment [letterIndex + 3] && 
            ipSegment[letterIndex + 1] == ipSegment[letterIndex + 2])
            {
                return true;
            }
        }

        return false;
    }

    [GeneratedRegex(@"([a-z]+)")]
    private static partial Regex ipSegmentPattern();
}
