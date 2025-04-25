using SubstringSearchLibrary.Implementations;

namespace SubstringSearchLibrary.Algorithms
{
    public class BoyerMooreAlgorithm : ISubstringSearch
    {
        private int[] badCharTable = new int[0];

        private void BuildBadCharTable(string pattern)
        {
            int m = pattern.Length;
            badCharTable = new int[256];

            for (int i = 0; i < 256; i++)
                badCharTable[i] = -1;

            for (int i = 0; i < m; i++)
                badCharTable[(int)pattern[i]] = i;
        }

        public IEnumerable<int> IndexesOf(string text, string pattern)
        {
            if(pattern.Length == 0 || text.Length == 0) return Enumerable.Empty<int>();

            int n = text.Length;
            int m = pattern.Length;

            BuildBadCharTable(pattern);

            List<int> result = new List<int>();
            int shift = 0;

            while (shift <= n - m)
            {
                int j = m - 1;

                while (j >= 0 && pattern[j] == text[shift + j])
                    j--;

                if (j < 0)
                {
                    result.Add(shift);
                    // Сдвигаем на 1, чтобы найти перекрывающиеся вхождения
                    shift += 1;
                }
                else
                {
                    int badCharIndex = (int)text[shift + j];
                    shift += Math.Max(1, j - badCharTable[badCharIndex]);
                }
            }

            return result;
        }
    }
}