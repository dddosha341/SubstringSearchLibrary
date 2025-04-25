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
                    shift += Math.Max(goodSuffixTable[j], j - badCharTable[text[shift + j]]);
                }
            }

            return result;
        }

        private int[] BuildBadCharTable(string pattern)
        {
            const int alphabetSize = 256; // ASCII
            var table = new int[alphabetSize];
            for (int i = 0; i < alphabetSize; i++)
                table[i] = -1;

            for (int i = 0; i < pattern.Length; i++)
                table[pattern[i]] = i;

            return table;
        }

        private int[] BuildGoodSuffixTable(string pattern)
        {
            int length = pattern.Length;
            var table = new int[length];
            var borderPos = new int[length + 1];
            int i = length, j = length + 1;
            borderPos[i] = j;

            while (i > 0)
            {
                while (j <= length && (j == length || pattern[i - 1] != pattern[j - 1]))
                {
                    if (j < length && table[j] == 0)
                        table[j] = j - i;
                    j = borderPos[j];
                }
                i--;
                j--;
                borderPos[i] = j;
            }

            j = borderPos[0];
            for (i = 0; i < length; i++)
            {
                if (table[i] == 0)
                    table[i] = j;
                if (i == j)
                    j = borderPos[j];
            }

            return table;
        }
    }
}