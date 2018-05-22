using System;
using System.Collections.Generic;
using System.Linq;

namespace Structures
{
	internal class PrefixNode
	{
		private readonly System.Collections.Generic.Dictionary<char, PrefixNode> _nodes;
		public string Word { get; }
		public bool IsEndOfWord { get; set; }

		public PrefixNode(string word)
		{
			_nodes = new System.Collections.Generic.Dictionary<char, PrefixNode>();
			Word = word;
		}

		public PrefixNode() : this("")
		{
		}

		public void AddLetter(char letter)
		{
			if (_nodes.ContainsKey(letter)) throw new ArgumentException("Key already exist!");
			_nodes[letter] = new PrefixNode(Word + letter);
		}

		public bool ContainsLetter(char letter) => _nodes.ContainsKey(letter);
		public PrefixNode GetNextNodeOrThrow(char letter) => _nodes[letter];
		public IEnumerable<PrefixNode> GetNextNodes() => _nodes.Values;
	}

	public class PrefixTree
	{
		private readonly PrefixNode _root = new PrefixNode();

		public void Add(string key)
		{
			var currentNode = _root;
			foreach (var letter in key)
			{
				if (!currentNode.ContainsLetter(letter))
					currentNode.AddLetter(letter);
				currentNode = currentNode.GetNextNodeOrThrow(letter);
			}
			currentNode.IsEndOfWord = true;
		}

		public bool Contains(string key)
		{
			return GetLastNodeOrNull(key)?.IsEndOfWord ?? false;
		}

		public void Remove(string key)
		{
			var result = GetLastNodeOrNull(key);
			if (result == null) return;
			result.IsEndOfWord = false;
		}

		public IEnumerable<string> GetWordsByPrefix(string prefix)
		{
			var node = GetLastNodeOrNull(prefix);
			return node == null ? Enumerable.Empty<string>() : EnumerateWords(node);
		}

		private PrefixNode GetLastNodeOrNull(string key)
		{
			var currentNode = _root;
			foreach (var letter in key)
			{
				if (!currentNode.ContainsLetter(letter)) return null;
				currentNode = currentNode.GetNextNodeOrThrow(letter);
			}
			return currentNode;
		}

		private static IEnumerable<string> EnumerateWords(PrefixNode node)
		{
			if (node.IsEndOfWord) yield return node.Word;
			foreach (var nextNode in node.GetNextNodes())
			{
				foreach (var result in EnumerateWords(nextNode))
				{
					yield return result;
				}
			}
		}
	}
}