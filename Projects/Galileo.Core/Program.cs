// See https://aka.ms/new-console-template for more information

namespace Galileo // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static readonly List<string> Documents = new List<string>()
        {
            "Epic Gaming Pc - intel i7 2.4ghz processor, NVIDIA RTX 3070",
            "Cool Gaming Pc - amd 3.2ghz ryzen",
            "Chromebook laptop - 2.4ghz snapdragon 3, 1080p"
        };

        static void Main(string[] args)
        {
            var searchQuery = "chromebook";

            var tokenizedDocuments = TokenParser.Parse(Documents);

            var index = new SearchIndex(tokenizedDocuments);
            index.Build();

            var seacher = new Searcher(index, Documents);

            var documents = seacher.Search(searchQuery);

            Console.WriteLine($"Found {documents.Count} results:");
            foreach (var document in documents)
            {
                Console.WriteLine(document);
            }
        }
    }
}