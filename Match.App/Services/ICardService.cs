using System.Collections.Generic;
using Match.App.Models;

namespace Match.App.Services
{
    public interface ICardService
    {
        List<Card> DrawCards(int numberOfPacks);
    }
}