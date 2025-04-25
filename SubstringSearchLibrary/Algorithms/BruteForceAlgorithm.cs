using SubstringSearchLibrary.Implementations;

namespace SubstringSearchLibrary.Algorithms
{
    public class BruteForceAlgorithm : ISubstringSearch
    {
        // Метод для поиска всех вхождений шаблона в тексте
        public IEnumerable<int> IndexesOf(string text, string pattern)
        {
            // Проверяем на пустые строки или если длина шаблона больше длины текста
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern) || pattern.Length > text.Length)
                return Enumerable.Empty<int>();

            // Список для хранения индексов найденных вхождений
            var result = new List<int>();
            int textLength = text.Length; // Длина текста
            int patternLength = pattern.Length; // Длина шаблона

            // Проходим по тексту, начиная с каждого символа
            for (int i = 0; i <= textLength - patternLength; i++)
            {
                int j;

                // Сравниваем символы текста и шаблона
                for (j = 0; j < patternLength; j++)
                {
                    if (text[i + j] != pattern[j]) // Если символы не совпадают, выходим из цикла
                        break;
                }

                // Если все символы шаблона совпали, добавляем индекс в результат
                if (j == patternLength)
                    result.Add(i);
            }

            // Возвращаем список индексов
            return result;
        }
    }
}