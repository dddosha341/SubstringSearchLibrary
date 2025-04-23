using SubstringSearchLibrary.Implementations;

namespace SubstringSearchLibrary.Algorithms
{
    public class BruteForceAlgorithm : ISubstringSearch
    {
        public IEnumerable<int> IndexesOf(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern) || pattern.Length > text.Length)
                return Enumerable.Empty<int>();

            var result = new List<int>();
            int textLength = text.Length;
            int patternLength = pattern.Length;

            for (int i = 0; i <= textLength - patternLength; i++)
            {
                int j;
                for (j = 0; j < patternLength; j++)
                {
                    if (text[i + j] != pattern[j])
                        break;
                }

                if (j == patternLength)
                    result.Add(i);
            }

            return result;
        }
    }
}