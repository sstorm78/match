using Match.App.Models;

namespace Match.App.MatchCheckers
{
    public interface IChecker
    {
        bool CanCheck(MatchCondition matchCondition);
        bool IsMatch(Card currentCard, Card previousCard);
    }
}