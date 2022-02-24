using Match.App.Models;

namespace Match.App.MatchCheckers
{
    public class SuitAndRankChecker : IChecker
    {
        public bool CanCheck(MatchCondition matchCondition)
        {
            return matchCondition == MatchCondition.SuitRanks;
        }

        public bool IsMatch(Card currentCard, Card previousCard)
        {
            return previousCard != null && currentCard.Rank == previousCard.Rank && currentCard.Suit == previousCard.Suit;
        }
    }
}
