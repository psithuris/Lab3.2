using System;
using System.IO;

namespace CaesarCipherLab
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 3)
                {
                    Console.WriteLine("Usage: CaesarCipherLab <trainingFile> <scrambledFile> <outputFile>");
                    Console.ReadLine();
                    return;
                }

                string trainingPath = args[0];
                string scrambledPath = args[1];
                string outputPath = args[2];

                var result = CaesarService.Process(trainingPath, scrambledPath, outputPath);

                Console.WriteLine("1. Reading input file \"" + Path.GetFileName(trainingPath) + "\".");
                Console.WriteLine("2. Length of input file is " + result.LineCount + " lines, and " + result.CharCount + " characters.");
                Console.WriteLine("3. The two most occurring characters are " + result.TrainingTop1.Key + " and " + result.TrainingTop2.Key +
                                  ", occurring " + result.TrainingTop1.Value + " times and " + result.TrainingTop2.Value + " times.");
                Console.WriteLine("4. Reading the encrypted file \"" + Path.GetFileName(scrambledPath) + "\".");
                Console.WriteLine("5. The most occurring character is " + result.ScrambledTop.Key +
                                  ", occurring " + result.ScrambledTop.Value + " times.");
                Console.WriteLine("6. A shift factor of " + result.Shift + " has been determined.");
                Console.WriteLine("7. Writing output file now to \"" + Path.GetFileName(outputPath) + "\".");

                // file read
                Console.Write("8. Display the file? (y/n): ");
                string choice = Console.ReadLine();

                if (choice != null && choice.ToLower() == "y")
                {
                    string[] lines = File.ReadAllLines(outputPath);
                    Console.WriteLine("\n--- Output File Contents ---");
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine("--- End of File ---\n");
                }

                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
