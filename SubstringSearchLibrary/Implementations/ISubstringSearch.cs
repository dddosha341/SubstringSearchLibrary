namespace SubstringSearchLibrary.Implementations
{
    public interface ISubstringSearch
    {
        public IEnumerable<int> IndexesOf(string pattern, string text);
    }
}
