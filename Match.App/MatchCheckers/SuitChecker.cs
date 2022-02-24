using Match.App.Models;

namespace Match.App.MatchCheckers
{
    public class SuitChecker : IChecker
    {
        public bool CanCheck(MatchCondition matchCondition)
        {
            return matchCondition == MatchCondition.Suits;
        }

        public bool IsMatch(Card currentCard, Card previousCard)
        {
            return previousCard != null && currentCard.Suit == previousCard.Suit;
        }
    }
}
