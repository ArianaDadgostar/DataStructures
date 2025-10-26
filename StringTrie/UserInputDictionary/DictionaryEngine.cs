using StringTrie;
using System.Text.Json;

namespace UserInputDictionary
{
    public class DictionaryEngine
    {
        static void Main(string[] args)
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                string fileData = File.ReadAllText(@"../../../../fulldictionary.json");
                Dictionary<string, string> words = JsonSerializer.Deserialize<Dictionary<string, string>>(fileData);

                Trie trie = new Trie();

                foreach (string key in words.Keys)
                {
                    trie.Insert(key);
                }

                Console.WriteLine("PLEASE BEGIN TYPING...");

                GuessingWords(trie, words);

                Console.WriteLine("KEEP RUNNING? (y if yes and n if no)");

                if (Console.ReadLine() == "n") keepRunning = false;
            }
        }

        static void GuessingWords(Trie trie, Dictionary<string, string> words)
        {
            string prefix = Console.ReadLine();
            if(trie.SearchNode(prefix) != null && trie.SearchNode(prefix).IsWord)
            {
                words.TryGetValue(prefix, out string definition);
                Console.WriteLine(definition);
                return;
            }

            List<string> possibles = trie.GetAllMatchingPrefix(prefix);

            if(possibles.Count == 0)
            {
                Console.WriteLine("NONE FOUND");
                return;
            }

            foreach(string possible in possibles)
            {
                Console.WriteLine(possible);
            }
        }
    }
}
