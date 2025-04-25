using SubstringSearchLibrary.Implementations;
using SubstringSearchLibrary.Algorithms;
using System.Diagnostics;
using System.Linq;

namespace ConsoleAppAlgorithms
{
    internal class Program
    {
        public static string PATTERN = "cdeab";

        internal static string TestedType(ISubstringSearch searcher)
        {
            if(searcher.GetType() == typeof(BoyerMooreAlgorithm))
            {
                return typeof(BoyerMooreAlgorithm).ToString();
            }
            else if(searcher.GetType() == typeof(BruteForceAlgorithm))
            {
                return typeof(BruteForceAlgorithm).ToString();
            }
            else if(searcher.GetType() == typeof(KMPAlgorithm))
            {
                return typeof(KMPAlgorithm).ToString();
            }
            else if(searcher.GetType() == typeof(RabinKarpAlgorithm))
            {
                return typeof(RabinKarpAlgorithm).ToString();
            }

            return "null";
        }

        static void Main(string[] args)
        {
            string text = string.Concat(Enumerable.Repeat("abcde", 2_000_000)); // 10 млн символов

            List<ISubstringSearch> substringSearches = new List<ISubstringSearch>()
            {
                new BoyerMooreAlgorithm(),
                new BruteForceAlgorithm(),
                new RabinKarpAlgorithm(),
                new KMPAlgorithm()
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<List<int>> indexesLists = new List<List<int>>();

            foreach(var searcher in substringSearches)
            {
                sw.Restart();
                var indexes = searcher.IndexesOf(text, PATTERN);
                sw.Stop();
                indexesLists.Add(indexes.ToList());
                Console.WriteLine($"{TestedType(searcher)} has been done by {sw.ElapsedMilliseconds}");

                GC.Collect();
            }

            Console.WriteLine("They`ve got the similar result: ");
            for(int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    Console.WriteLine(indexesLists[i].SequenceEqual(indexesLists[j]));
                }
            }

            return;
        }
    }
}
