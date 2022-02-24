using System;
using System.Collections.Generic;
using Match.App.MatchCheckers;
using Match.App.Models;
using Match.App.Services;
using Match.App.UIPresenters;
using Microsoft.Extensions.DependencyInjection;

namespace Match.App
{
    class Program
    {
        public static IServiceProvider ServiceProvider;
        public static IUIPresenter ConsoleWriter;

        static void Main(string[] args)
        {
            ServiceProvider = RegisterServices();
            ConsoleWriter = ServiceProvider.GetService<IUIPresenter>();
            var cardService = ServiceProvider.GetService<ICardService>();
            var gameService = ServiceProvider.GetService<IGameService>();

            DisplayIntro();

            var numberOfPacks = ReadNumberOfPacks();
            var cards = cardService.DrawCards(numberOfPacks);

            gameService.SetCondition(ReadMatchCondition());
            gameService.SetNumberOfPlayers(2);

            ConsoleWriter.WriteLine("The Game is On!");

            var result = gameService.Play(cards);
            DisplayResults(result);

            Console.ReadLine();
        }

        private static void DisplayIntro()
        {
            ConsoleWriter.WriteLine("Welcome to Sergey Storm's Match game");
            ConsoleWriter.WriteLine("");
        }

        private static void DisplayResults(string result)
        {
            foreach (var line in result.Split(Environment.NewLine))
            {
                ConsoleWriter.WriteLine(line);
            }
        }

        private static int ReadNumberOfPacks()
        {
            ConsoleWriter.WriteLine("How many packs of cards you want to play with? (enter a number between 1 and 9");

            while (true)
            {
                var input = Console.ReadLine();

                if (int.TryParse(input, out var numberOfPacks)) return numberOfPacks;

                ConsoleWriter.WriteLine("enter a number between 1 and 9");
            }
        }

        private static MatchCondition ReadMatchCondition()
        {
            ConsoleWriter.WriteLine("What is the winning match condition?");
            ConsoleWriter.WriteLine("Press S for suits");
            ConsoleWriter.WriteLine("Press R for ranks");
            ConsoleWriter.WriteLine("Press B for suits and ranks");

            var validInputs = new List<string>{"s", "r", "b"};

            while (true)
            {
                var input = Console.ReadLine();

                if (validInputs.Contains(input.ToLowerInvariant()))
                {
                    switch (input.ToLowerInvariant())
                    {
                        case "s":
                            return MatchCondition.Suits;
                        case "r":
                            return MatchCondition.Ranks;
                        case "b":
                            return MatchCondition.SuitRanks;
                    }
                }

                ConsoleWriter.WriteLine("Press S, R or B");
            }
        }

        private static ServiceProvider RegisterServices()
        {
            return new ServiceCollection()
                .AddSingleton<IUIPresenter, ConsolePresenter>()
                .AddSingleton<IRandomService, RandomService>()
                .AddSingleton<ICardService,CardService>()
                .AddSingleton<IGameService, MatchService>()
                .AddSingleton<IChecker, SuitChecker>()
                .AddSingleton<IChecker, RankChecker>()
                .AddSingleton<IChecker, SuitAndRankChecker>()
                .BuildServiceProvider();
        }
    }
}
