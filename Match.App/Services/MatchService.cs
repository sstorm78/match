using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Match.App.MatchCheckers;
using Match.App.Models;

namespace Match.App.Services
{
    public class MatchService : IGameService
    {
        private  MatchCondition _matchCondition;
        private readonly IEnumerable<IChecker> _checkers;
        private readonly IRandomService _randomService;
        private int _numberOfPlayers;

        public MatchService(
            IEnumerable<IChecker> checkers,
            IRandomService randomService)
        {
            _checkers = checkers;
            _randomService = randomService;
        }

        public void SetCondition(MatchCondition matchCondition)
        {
            _matchCondition = matchCondition;
        }

        public void SetNumberOfPlayers(int numberOfPlayers)
        {
            _numberOfPlayers = numberOfPlayers;
        }

        private bool IsMatch(Card currentCard, Card previousCard)
        {
            foreach (var checker in _checkers)
            {
                if (checker.CanCheck(_matchCondition))
                {
                    return checker.IsMatch(currentCard, previousCard);
                }
            }

            return false;
        }

        public string Play(List<Card> cards)
        {
            var stack = new Stack<Card>();
            cards.ForEach(stack.Push);

            var unclaimedCards = new List<Card>();

            var players = GenerateListOfPlayers();

            Card previousCard = null;

            while (stack.Count > 0)
            {
                var currentCard = stack.Pop();

                if (IsMatch(currentCard, previousCard))
                {
                    var luckyPlayerIndex = _randomService.Next(0,players.Count);
                    players[luckyPlayerIndex].AddRange(unclaimedCards);
                    unclaimedCards = new List<Card>();
                    continue;
                }

                unclaimedCards.Add(currentCard);
                previousCard = currentCard;
            }

            return GenerateResult(players);
        }

        private List<List<Card>> GenerateListOfPlayers()
        {
            var result = new List<List<Card>>();

            for (var playerIndex = 0; playerIndex < _numberOfPlayers; playerIndex++)
            {
                result.Add(new List<Card>());
            }

            return result;
        }

        private string GenerateResult(List<List<Card>> players)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("Game Results!");

            for (var playerIndex = 0; playerIndex < players.Count; playerIndex++)
            {
                sb.AppendLine($"Player {playerIndex + 1}: {players[playerIndex].Count} cards");
            }

            var isDraw = players.Select(i => i.Count).Distinct().Take(2).Count() == 1;

            if (isDraw)
            {
                sb.AppendLine("The Draw!");
            }
            else
            {
                var playerCardCount = players.Select(i => i.Count).ToList(); 
                sb.AppendLine($"The Winner is Player {playerCardCount.IndexOf(playerCardCount.Max()) + 1}");
            }

            return sb.ToString();
        }
    }
}
