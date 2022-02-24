using System;
using System.Collections.Generic;
using System.Linq;
using Match.App.Models;

namespace Match.App.Services
{
    public class CardService : ICardService
    {
        private readonly IRandomService _randomService;

        public CardService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public List<Card> DrawCards(int numberOfPacks)
        {
            var cards = new List<Card>();

            for (var pack = 0; pack < numberOfPacks; pack++) 
            {
                foreach (var suit in (Suit[])Enum.GetValues(typeof(Suit)))
                {
                    cards.AddRange(from rank in (Rank[])Enum.GetValues(typeof(Rank)) select new Card(suit, rank));
                }
            }

            return Shuffle(cards);
        }

        private List<Card> Shuffle(List<Card> cards)
        {
            var cardsInPack = cards.Count;

            while (cardsInPack > 1)
            {
                cardsInPack--;
                var nextRandomPosition = _randomService.Next(cardsInPack + 1);
                (cards[nextRandomPosition], cards[cardsInPack]) = (cards[cardsInPack], cards[nextRandomPosition]);
            }

            return cards;
        }
    }
}
