using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordCount
{
  public class WordCountProcessor
  {
    public Dictionary<string, int> ProcessFile(string fileName)
    {
      // Read in from the file
      var text = File.ReadAllText(fileName);
      var occurences = new Dictionary<string, int>();
      var words = SplitWords(text);

      while(words.Count > 0)
      {
        var word = words[0].ToLower();

        if (!string.IsNullOrWhiteSpace(word))
        {
          if (!occurences.ContainsKey(word))
          {
            occurences.Add(word, 1);
          }
          else
          {
            occurences[word]++;
          }
        }
        words.RemoveAt(0);
      }
      return occurences;
    }

    public List<string> SplitWords(string text)
    {
      var str = new StringBuilder();
      char[] charsToRemove = {',', '.', '!', '\"', '?'};

      // Removes special characters
      foreach (char character in text)
      {
        if (!charsToRemove.Contains(character))
        {
          str.Append(character);
        }
      }
      text = str.ToString();

      // Split by whitespace
      return Regex.Split(text, "\\s").ToList();
    }

    public void PrintCounts(Dictionary<string, int> wordCount)
    {
      var file = File.OpenWrite("output.txt");
      var str = new StringBuilder();

      // Sorts the list and builds the output
      var sortedList = wordCount.OrderByDescending(x => x.Value).ToList();
      sortedList.ForEach(x => str.Append($"{x.Key}: {x.Value}"+Environment.NewLine));

      var bytes = new UTF8Encoding(true).GetBytes(str.ToString());
      file.Write(bytes, 0, bytes.Length);
    }
  }
}