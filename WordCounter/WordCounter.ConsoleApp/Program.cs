using System;
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

            Console.WriteLine("Welcome to Word Counter.");

            Console.WriteLine(fileRepository.LoadFile("test.txt"));
        }

        private static IServiceProvider BuildDi()
        {
            return new ServiceCollection()
                .AddSingleton<IFileManager, FileManager>()
                .AddSingleton<IFileRepository, FileRepository>()
                .AddLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .BuildServiceProvider();
        }
    }
}
