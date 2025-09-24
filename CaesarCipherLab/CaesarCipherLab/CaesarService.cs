using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaesarCipherLab
{
    public static class CaesarService
    {
        public static ProcessResult Process(string trainingPath, string scrambledPath, string outputPath)
        {
            string trainingText = File.ReadAllText(trainingPath);
            int lineCount = File.ReadLines(trainingPath).Count();
            int charCount = trainingText.Length;

            var trainingFreq = CountLetters(trainingText);
            var trainingTop = trainingFreq.OrderByDescending(x => x.Value).Take(2).ToList();

            string scrambledText = File.ReadAllText(scrambledPath);
            var scrambledFreq = CountLetters(scrambledText);
            var scrambledTop = scrambledFreq.OrderByDescending(x => x.Value).First();

            int shift = GetShift(trainingTop[0].Key, scrambledTop.Key);
            string decrypted = ShiftText(scrambledText, -shift);
            File.WriteAllText(outputPath, decrypted);

            return new ProcessResult
            {
                LineCount = lineCount,
                CharCount = charCount,
                TrainingTop1 = trainingTop[0],
                TrainingTop2 = trainingTop[1],
                ScrambledTop = scrambledTop,
                Shift = shift
            };
        }

        private static Dictionary<char, int> CountLetters(string text)
        {
            var counts = new Dictionary<char, int>();
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char upper = char.ToUpper(c);
                    if (!counts.ContainsKey(upper)) counts[upper] = 0;
                    counts[upper]++;
                }
            }
            return counts;
        }

        private static int GetShift(char trainingLetter, char scrambledLetter)
        {
            int t = trainingLetter - 'A';
            int s = scrambledLetter - 'A';
            return (s - t + 26) % 26;
        }

        private static string ShiftText(string input, int shift)
        {
            string result = "";
            foreach (char c in input)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    result += (char)('A' + (c - 'A' + shift + 26) % 26);
                }
                else if (c >= 'a' && c <= 'z')
                {
                    result += (char)('a' + (c - 'a' + shift + 26) % 26);
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }
    }

    public class ProcessResult
    {
        public int LineCount { get; set; }
        public int CharCount { get; set; }
        public KeyValuePair<char, int> TrainingTop1 { get; set; }
        public KeyValuePair<char, int> TrainingTop2 { get; set; }
        public KeyValuePair<char, int> ScrambledTop { get; set; }
        public int Shift { get; set; }
    }
}
