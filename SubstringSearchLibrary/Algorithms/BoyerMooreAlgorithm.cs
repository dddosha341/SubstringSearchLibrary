using SubstringSearchLibrary.Implementations;

namespace SubstringSearchLibrary.Algorithms
{
    public class BoyerMooreAlgorithm : ISubstringSearch
    {
        private int[] badCharTable = new int[256];
        private int[] goodSuffixTable = new int[0];

        private void BuildBadCharTable(string pattern)
        {
            for (int i = 0; i < badCharTable.Length; i++)
                badCharTable[i] = -1;

            for (int i = 0; i < pattern.Length; i++)
                badCharTable[(int)pattern[i]] = i;
        }

        private void BuildGoodSuffixTable(string pattern)
        {
            int m = pattern.Length;
            goodSuffixTable = new int[m];
            int[] suffixes = new int[m];

            // Вычисляем суффиксы
            suffixes[m - 1] = m;
            int g = m - 1;
            int f = 0;

            for (int i = m - 2; i >= 0; i--)
            {
                if (i > g && suffixes[i + m - 1 - f] < i - g)
                {
                    suffixes[i] = suffixes[i + m - 1 - f];
                }
                else
                {
                    if (i < g)
                        g = i;
                    f = i;
                    while (g >= 0 && pattern[g] == pattern[g + m - 1 - f])
                        g--;
                    suffixes[i] = f - g;
                }
            }

            // Используем суффиксы для goodSuffixTable
            for (int i = 0; i < m; i++)
                goodSuffixTable[i] = m;

            int j = 0;
            for (int i = m - 1; i >= 0; i--)
            {
                if (suffixes[i] == i + 1)
                {
                    for (; j < m - 1 - i; j++)
                    {
                        if (goodSuffixTable[j] == m)
                            goodSuffixTable[j] = m - 1 - i;
                    }
                }
            }

            for (int i = 0; i <= m - 2; i++)
                goodSuffixTable[m - 1 - suffixes[i]] = m - 1 - i;
        }

        public IEnumerable<int> IndexesOf(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
                return Enumerable.Empty<int>();

            int n = text.Length;
            int m = pattern.Length;
            if (m > n) return Enumerable.Empty<int>();

            BuildBadCharTable(pattern);
            BuildGoodSuffixTable(pattern);

            List<int> result = new List<int>();
            int s = 0;

            while (s <= n - m)
            {
                int j = m - 1;
                while (j >= 0 && pattern[j] == text[s + j])
                    j--;

                if (j < 0)
                {
                    result.Add(s);
                    s += goodSuffixTable[0]; // Может быть 1, если перекрытие
                }
                else
                {
                    int bcShift = j - badCharTable[text[s + j]];
                    int gsShift = goodSuffixTable[j];
                    s += Math.Max(1, Math.Max(bcShift, gsShift));
                }
            }

            return result;
        }
    }
}
