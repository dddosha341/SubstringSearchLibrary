using SubstringSearchLibrary.Implementations;

namespace SubstringSearchLibrary.Algorithms
{
    public class BoyerMooreAlgorithm : ISubstringSearch
    {
        public IEnumerable<int> IndexesOf(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern) || pattern.Length > text.Length)
                return Enumerable.Empty<int>();

            var badCharTable = BuildBadCharTable(pattern);
            var goodSuffixTable = BuildGoodSuffixTable(pattern);

            var result = new List<int>();
            int textLength = text.Length;
            int patternLength = pattern.Length;
            int shift = 0;

            while (shift <= textLength - patternLength)
            {
                int j = patternLength - 1;

                // Сравнение символов с конца шаблона
                while (j >= 0 && pattern[j] == text[shift + j])
                    j--;

                if (j < 0)
                {
                    result.Add(shift);

                    // Используем таблицу хороших суффиксов для сдвига
                    shift += (shift + patternLength < textLength) ? patternLength - goodSuffixTable[0] : 1;
                }
                else
                {
                    // Сдвиг на основе максимального значения из таблиц
                    shift += Math.Max(goodSuffixTable[j], j - (badCharTable.ContainsKey(text[shift + j]) ? badCharTable[text[shift + j]] : -1));
                }
            }

            return result;
        }

        private Dictionary<char, int> BuildBadCharTable(string pattern)
        {
            var table = new Dictionary<char, int>();
            for (int i = 0; i < pattern.Length; i++)
                table[pattern[i]] = i;

            return table;
        }

        private int[] BuildGoodSuffixTable(string pattern)
        {
            int m = pattern.Length;
            var table = new int[m];
            var suffixes = new int[m];

            suffixes[m - 1] = m;
            int g = m - 1;
            int f = 0;

            for (int i = m - 2; i >= 0; --i)
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
                        --g;
                    suffixes[i] = f - g;
                }
            }

            for (int i = 0; i < m; ++i)
                table[i] = m;

            int j = 0;
            for (int i = m - 1; i >= 0; --i)
            {
                if (suffixes[i] == i + 1)
                {
                    for (; j < m - 1 - i; ++j)
                    {
                        if (table[j] == m)
                            table[j] = m - 1 - i;
                    }
                }
            }

            for (int i = 0; i <= m - 2; ++i)
                table[m - 1 - suffixes[i]] = m - 1 - i;

            return table;
        }
    }
}
