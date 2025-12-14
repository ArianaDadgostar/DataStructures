using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedConcepts
{
    public class Node
    {
        public char Letter { get; set; }
        public int priority { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node(char letter, int priority)
        {
            Letter = letter;
            this.priority = priority;
            Left = null;
            Right = null;
        }
    }

    public class Tree
    {         
        public Node Root { get; set; }
        public Tree(Node root)
        {
            Root = root;
        }

        public Tree() { }
    }

    public class Huffman
    {
        string file;
        List<char> Enqueued;
        PriorityQueue<Node, int> PriorityQueue;

        public Huffman(string file)
        {
            this.file = file;
            Enqueued = new List<char>();
            PriorityQueue = new PriorityQueue<Node, int>();
        }

        private Tree CreateTree()
        {
            SortByPriority();
            Tree huffmanTree = new Tree(null);

            while (PriorityQueue.Count > 1)
            {
                Node first = PriorityQueue.Dequeue();
                Node second = PriorityQueue.Dequeue();

                Node parent = new Node(default, first.priority + second.priority);

                huffmanTree.Root = parent;

                parent.Left = first;
                parent.Right = second;
                PriorityQueue.Enqueue(parent, parent.priority);
            }

            return huffmanTree;
        }

        private void GenerateCodes(ref Dictionary<char, List<bool>> codes, Node node, List<bool> code)
        {
            if(node.Letter != default)
            {
                codes[node.Letter] = code;
            }

            if(node.Left != null)
            {
                List<bool> lCopy = code.Select((bool b) => b).ToList();
                lCopy.Add(false);
                GenerateCodes(ref codes, node.Left, lCopy);
            }

            if (node.Right == null) return;
            
            List<bool> rCopy = code.Select((bool b) => b).ToList();
            rCopy.Add(true);
            GenerateCodes(ref codes, node.Right, rCopy);
        }

        public BitArray ReadValues(ref Tree huffmanTree)
        {
            huffmanTree = CreateTree();
            Dictionary<char, List<bool>> codes = new Dictionary<char, List<bool>>();
            List<bool> code = new List<bool>();

            GenerateCodes(ref codes, huffmanTree.Root, code);

            List<bool> encodedFile = new List<bool>();

            foreach(char letter in file)
            {
                foreach(bool bit in codes[letter])
                {
                    encodedFile.Add(bit);
                }
            }

            BitArray compressed = new BitArray(encodedFile.ToArray());
            return compressed;
        }

        private void SortByPriority()
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();
            foreach (char letter in file)
            {
                if(frequency.ContainsKey(letter))
                {
                    frequency[letter]++;
                }
                else
                {
                    frequency[letter] = 1;
                    Enqueued.Add(letter);
                }
            }

            foreach (var pair in frequency)
            {
                PriorityQueue.Enqueue(new Node(pair.Key, pair.Value), pair.Value);
            }
        }

        public string Decompress(BitArray compressed, Tree huffmanTree)
        {
            int currentBit = 0;
            Node currentNode = huffmanTree.Root;
            List<char> decompressed = new List<char>();
            
            while(decompressed.Count < file.Length)
            {
                if(currentNode.Letter != default)
                {
                    decompressed.Add(currentNode.Letter);
                    currentNode = huffmanTree.Root;
                    continue;
                }

                if (!compressed[currentBit])
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
                currentBit++;
            }

            return new string(decompressed.ToArray());
        }
    }
}
