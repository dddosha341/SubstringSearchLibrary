using SubstringSearchLibrary.Implementations;

namespace SubstringSearchLibrary.Algorithms
{
    public class KMPAlgorithm : ISubstringSearch
    {
        public IEnumerable<int> IndexesOf(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern) || pattern.Length > text.Length)
                yield break;

            int[] lps = ComputeLPS(pattern);
            int i = 0, j = 0;

            while (i < text.Length)
            {
                if (text[i] == pattern[j])
                {
                    i++;
                    j++;
                }

                if (j == pattern.Length) 
                {
                    yield return i - j;
                    j = lps[j - 1]; 
                }
                else if (i < text.Length && text[i] != pattern[j])
                {
                    if (j > 0)
                        j = lps[j - 1]; 
                    else
                        i++; 
                }
            }
        }

        private int[] ComputeLPS(string pattern)
        {
            int[] lps = new int[pattern.Length];
            int len = 0; 
            int i = 1;

            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len > 0)
                        len = lps[len - 1]; 
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
            return lps;
        }
    }
}
