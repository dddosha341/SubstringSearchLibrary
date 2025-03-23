using SubstringSearchLibrary.Implementations;

namespace SubstringSearchLibrary.Algorithms
{
    public class RabinKarpAlgorithm : ISubstringSearch
    {
        private const int AlphabetSize = 256;
        private const int Mod = 5381;

        public IEnumerable<int> IndexesOf(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern) || pattern.Length > text.Length)
                yield break;

            int m = pattern.Length;
            int n = text.Length;
            long patternHash = 0, textHash = 0, firstIndexHash = 1;

            for (int i = 0; i < m; i++)
            {
                patternHash = (patternHash * AlphabetSize + pattern[i]) % Mod;
                textHash = (textHash * AlphabetSize + text[i]) % Mod;
                if (i > 0) firstIndexHash = (firstIndexHash * AlphabetSize) % Mod;
            }

            for (int i = 0; i <= n - m; i++)
            {
                if (patternHash == textHash && text.Substring(i, m) == pattern)
                    yield return i;

                if (i < n - m)
                {
                    textHash = (textHash - (text[i] * firstIndexHash) % Mod + Mod) % Mod;
                    textHash = (Math.BigMul((int)textHash, AlphabetSize) + text[i + m]) % Mod;
                }
            }
        }
    }
}
