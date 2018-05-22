using FluentAssertions;
using NUnit.Framework;

namespace Structures.Tests
{
	[TestFixture]
	public class PrefixTreeShould
	{
		private PrefixTree _tree;

		[SetUp]
		public void SetUp()
		{
			_tree = new PrefixTree();
		}

		[Test]
		public void AddOneWord()
		{
			_tree.Add("word");
			_tree.Contains("word").Should().BeTrue();
		}

		[Test]
		public void NotHaveWord_WhenEmptyTree()
		{
			_tree.Contains("word").Should().BeFalse();
		}

		[Test]
		public void NotHaveWordsInPrefix()
		{
			const string word = "word";
			var letters = word.ToCharArray();
			_tree.Add(word);
			var currentWord = "";
			foreach (var letter in letters)
			{
				_tree.Contains(currentWord).Should().Be(false);
				currentWord += letter;
			}
			_tree.Contains(currentWord).Should().Be(true);
		}

		[Test]
		public void AddSeveralWords()
		{
			_tree.Add("word");
			_tree.Add("wiki");
			_tree.GetWordsByPrefix("").Should().BeEquivalentTo("word", "wiki");
		}

		[Test]
		public void ShouldRemoveWord_WhenOneWord()
		{
			_tree.Add("word");
			_tree.Remove("word");
			_tree.GetWordsByPrefix("").Should().BeEquivalentTo();
		}

		[Test]
		public void ShouldRemoveWord_WhenSeveralWords()
		{
			_tree.Add("word");
			_tree.Add("wiki");
			_tree.Remove("word");
			_tree.GetWordsByPrefix("").Should().BeEquivalentTo("wiki");
		}
	}
}
