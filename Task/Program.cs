using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var win1251 = Encoding.GetEncoding("windows-1251");
using var sr = new StreamReader("../../../война и мир.txt", win1251);

var line = sr.ReadLine();
var wordCount = new Dictionary<string, int>();
var punctuationMarks = new[] { "?", ".", ",", "!", ":", ":", "–", "—", "-", };
while (line != null)
{
    line = punctuationMarks.Aggregate(line, (current, mark) => current.Replace(mark, string.Empty));
    var words = line.Split(' ');
    foreach (var word in words)
    {
        if (wordCount.TryGetValue(word, out var count))
        {
            wordCount[word] = count + 1;
        }
        else
        {
            wordCount.Add(word, 1);
        }
    }

    line = sr.ReadLine();
}

wordCount = wordCount
    .OrderByDescending(x => x.Value)
    .ToDictionary(x => x.Key, x => x.Value);

using var sw = new StreamWriter("../../../ans.txt", true, win1251);
foreach (var i in wordCount)
{
    sw.WriteLine($"{i.Key} {i.Value}");
}

sw.Close();
sr.Close();