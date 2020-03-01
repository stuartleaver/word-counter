using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WordCounter.Core;
using WordCounter.Core.Interfaces;

namespace WordCounter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var servicesProvider = BuildDi();

            var fileRepository = servicesProvider.GetRequiredService<IFileRepository>();

            var wordCounter = servicesProvider.GetRequiredService<IWordCounter>();

            var stopwatch = new Stopwatch();

            try
            {
                if (args.Length == 0)
                    throw new ArgumentException("The file to count has not been provided.");

                stopwatch.Start();

                var text = fileRepository.LoadFile(args[0]);

                var wordCount = wordCounter.CountWords(text);

                stopwatch.Stop();

                foreach (var (key, value) in wordCount)
                {
                    Console.WriteLine($"{value}\t{key}");
                }

                Console.WriteLine($"{Environment.NewLine}Elapsed time - {stopwatch.ElapsedMilliseconds}ms");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured - {e.Message}");
            }

            Console.WriteLine("Press Enter or Return to exit.");

            Console.ReadLine();
        }

        private static IServiceProvider BuildDi()
        {
            return new ServiceCollection()
                .AddSingleton<IFileManager, FileManager>()
                .AddSingleton<IFileRepository, FileRepository>()
                .AddSingleton<IWordCounter, Core.WordCounter>()
                .AddLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .BuildServiceProvider();
        }
    }
}
