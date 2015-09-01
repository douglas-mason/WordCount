namespace WordCount
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // sets default filename if none is provided
      var fileName = args.Length < 1 ? "data.txt" : args[0];
      var processor = new WordCountProcessor();
      var wordCount = processor.ProcessFile(fileName);
      processor.PrintCounts(wordCount);
    }
  }
}