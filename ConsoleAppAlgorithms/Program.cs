using SubstringSearchLibrary.Implementations;
using SubstringSearchLibrary.Algorithms;
using System.Diagnostics;

namespace ConsoleAppAlgorithms
{
    internal class Program
    {
        public static string PATTERN = "";

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
            Console.WriteLine("Enter The Path to txt File: ");
            var path = Console.ReadLine()?.Replace("\"", "") ?? throw new ArgumentException();

            var text = File.ReadAllText(path, System.Text.Encoding.UTF8);

            List<ISubstringSearch> substringSearches = new List<ISubstringSearch>()
            {
                new BoyerMooreAlgorithm(),
                new BruteForceAlgorithm(),
                new RabinKarpAlgorithm(),
                new KMPAlgorithm()
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach(var searcher in substringSearches)
            {
                sw.Restart();
                var indexes = searcher.IndexesOf(PATTERN, text);
                sw.Stop();
                Console.WriteLine($"{TestedType(searcher)} has been done by {sw.ElapsedMilliseconds}");

                GC.Collect();
            }

            return;
        }
    }
}
