namespace StringTrie
{
    public class TrieNode
    {
        public char letter { get; private set; }
        public Dictionary<char, TrieNode> Children { get; private set; }
        public bool IsWord { get; set; }

        public TrieNode(char letter)
        {
            this.letter = letter;
            Children = new Dictionary<char, TrieNode>();
            IsWord = false;
        }
    }
}
