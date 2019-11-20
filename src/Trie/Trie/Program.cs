using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class Trie
    {

        private class TrieNode
        {
            private TrieNode[] _nodes = new TrieNode[26];

            public bool Finish { get; set; }

            public TrieNode AddCharacter(char c)
            {
                var index = CharToIndex(c);
                if (_nodes[index] == null)
                {
                    _nodes[index] = new TrieNode();
                }

                return _nodes[index];
            }

            public TrieNode GetCharacter(char c)
            {
                var index = CharToIndex(c);
                return _nodes[index];
            }

            private int CharToIndex(char c)
            {
                return (int)(c - 'a');
            }
        }

        private TrieNode _root;

        /** Initialize your data structure here. */
        public Trie()
        {
            _root = new TrieNode();
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            TrieNode lastTrie = _root;
            for (int i = 0; i < word.Length; i++)
            {
                lastTrie = _root.AddCharacter(word[i]);
            }

            lastTrie.Finish = true;
        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            TrieNode lastTrie = _root;
            for (int i = 0; i < word.Length && lastTrie != null; i++)
            {
                lastTrie = lastTrie.GetCharacter(word[i]);
            }

            return (lastTrie != null && lastTrie.Finish);
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            var lastTrie = _root;
            for (int i = 0; i < prefix.Length && lastTrie != null; i++)
            {
                lastTrie = lastTrie.GetCharacter(prefix[i]);
            }

            return lastTrie != null;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie();

            trie.Insert("apple");
            Console.WriteLine(trie.Search("apple"));
            Console.WriteLine(trie.Search("app"));
            Console.WriteLine(trie.StartsWith("app"));
            
            trie.Insert("app");
            Console.WriteLine(trie.Search("app"));
        }
    }
}
