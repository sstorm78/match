using Match.App.Models;

namespace Match.App.MatchCheckers
{
    public class RankChecker : IChecker
    {
        public bool CanCheck(MatchCondition matchCondition)
        {
            return matchCondition == MatchCondition.Ranks;
        }

        public bool IsMatch(Card currentCard, Card previousCard)
        {
            return previousCard != null && currentCard.Rank == previousCard.Rank;
        }
    }
}
