using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace NexGenTagParser
{
    internal class UnparsedTag
    {
        public string filePath { get; set; }
        public string fileName { get; set; }
        public List<string> fileLines { get; set; }
        public UnparsedTag(string filePath)
        {
            this.filePath = filePath;
            fileName = Path.GetFileName(filePath);
            fileLines = File.ReadAllLines(filePath).ToList();
        }
    }
    internal class ParsedTag
    {
        public string fileName { get; set; }
        public string artist { get; set; }
        public string songTitle { get; set; }
        public ParsedTag(string fileName, string artist = "", string songTitle = "")
        {
            this.fileName = fileName;
            this.artist = artist;
            this.songTitle = songTitle;
        }
    }
    internal class Program
    {
        static readonly string outputFolder = "G:/tags/";
        static readonly string inputFolder = "G:/prodrive/NexGen Song Backup/DataFile/";
        static readonly string tagExtension = "dat";

        public static string BetweenStrings(string text, string start, string end) // https://stackoverflow.com/a/46940181
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);

            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }

        static List<UnparsedTag> CollectUnparsedTags()
        {
            List<UnparsedTag> unparsedTags = new List<UnparsedTag>();

            foreach (string filePath in Directory.GetFiles(inputFolder, $"*.{tagExtension}"))
            {
                UnparsedTag unparsedTag = new UnparsedTag(filePath);
                unparsedTags.Add(unparsedTag);
            }

            return unparsedTags;
        }

        static string GetTagByName(UnparsedTag unparsedTag, string tagName)
        {
            // THIS FUCKING SUCKS
            for (int i = 0; i < unparsedTag.fileLines.Count - 1; i++)
            {
                string currentLine = unparsedTag.fileLines[i];
                string nextLine = unparsedTag.fileLines[i + 1];

                if (string.Equals(currentLine, $"\"{tagName}\""))
                {
                    return BetweenStrings(nextLine, "\"", "\"");
                }
            }

            return string.Empty;
        }

        static List<ParsedTag> ParseUnparsedTags(List<UnparsedTag> unparsedTags)
        {
            List<ParsedTag> parsedTags = new List<ParsedTag>();

            foreach (UnparsedTag unparsedTag in unparsedTags)
            {
                ParsedTag parsedTag = new ParsedTag(unparsedTag.fileName)
                {
                    songTitle = GetTagByName(unparsedTag, "SONG"),
                    artist = GetTagByName(unparsedTag, "ARTIST")
                };

                if (parsedTag.songTitle.Contains("|") || parsedTag.artist.Contains("|"))
                {
                    throw new Exception("SONG TITLE OR ARTIST NAME CONTAINS BAD CHARACTER!");
                }

                parsedTags.Add(parsedTag);
            }

            return parsedTags;
        }

        static void WriteParsedTags(List<ParsedTag> parsedTags)
        {
            foreach (ParsedTag parsedTag in parsedTags)
            {
                string outputPath = Path.Combine(outputFolder, parsedTag.fileName);
                string outputString = $"{parsedTag.artist} | {parsedTag.songTitle}";

                Console.WriteLine($"{outputString} -> {outputPath}");
                File.WriteAllText(outputPath, outputString);
            }
        }

        static void Main(string[] args)
        {
            List<UnparsedTag> unparsedTags = CollectUnparsedTags();
            List<ParsedTag> parsedTags = ParseUnparsedTags(unparsedTags);
            WriteParsedTags(parsedTags);
        }
    }
}
