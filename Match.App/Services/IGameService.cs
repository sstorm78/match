using System.Collections.Generic;
using Match.App.Models;

namespace Match.App.Services
{
    public interface IGameService
    {
        void SetCondition(MatchCondition matchCondition);
        string Play(List<Card> cards);

        void SetNumberOfPlayers(int numberOfPlayers);
    }
}