using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StringTrie
{
    public class Trie
    {
        public TrieNode Sentinal = new TrieNode('*');

        private void AddToTrie(int stringIndex, string word, TrieNode node)
        {
            if (stringIndex == word.Length)
            {
                node.IsWord = true;
                return;
            }

            bool result = node.Children.TryGetValue(word[stringIndex], out TrieNode child);
            if (!result)
            {
                TrieNode val = new TrieNode(word[stringIndex]);
                node.Children.Add(word[stringIndex], val);
                child = val;
            }
            AddToTrie(stringIndex + 1, word, child);
        }

        public void Insert(string word)
        {
            AddToTrie(0, word, Sentinal);
        }

        public TrieNode SearchNode(string prefix)
        {
            TrieNode test = Sentinal;
            foreach (char val in prefix)
            {
                if (test == null) return test;
                test = FindNode(val, test);
            }
            return test;
        }

        private TrieNode FindNode(char letter, TrieNode test)
        {
            foreach (TrieNode node in test.Children.Values)
            {
                if (node.letter == letter)
                {
                    return node;
                }
            }
            return null;
        }

        public List<string> GetAllMatchingPrefix(string prefix)
        {
            TrieNode first = SearchNode(prefix);
            List<string> result = new List<string>();

            if (first == null) return result;

            FindChildWords(ref result, first, prefix);

            return result;
        }

        private void FindChildWords(ref List<string> words, TrieNode first, string previous)
        {
            if (first.IsWord)
            {
                words.Add(previous);
            }

            foreach(TrieNode child in first.Children.Values)
            {
                FindChildWords(ref words, child, (previous + child.letter));
            }
        }

        public bool Remove(string word)
        {
            return RemovingTraversalRecursive(word, 0, Sentinal);
        }

        private bool RemovingTraversalRecursive(string word, int index, TrieNode parent)
        {
            bool result = parent.Children.TryGetValue(word[index], out TrieNode child);
            if (!result) return false;

            if (index + 1 >= word.Length)
            {
                bool returnVal = child.IsWord;
                child.IsWord = false;

                return returnVal;
            }

            bool wordExisting = RemovingTraversalRecursive(word, index + 1, child);

            if (wordExisting) return true;

            return false;
        }
    }
}
