using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands.Classes {

    public class Deck {
        private Stack<Card> _cards;

        public Deck() {
            List<Card> cards = CreateCardsForDeck();
            _cards = new Stack<Card>(cards);
            Shuffle();
        }

        public List<Card> DrawCards(int numberToBeDrawn) {
            var drawnCards = new List<Card>();

            for (int i = 0; i < numberToBeDrawn; i++)
                drawnCards.Add(_cards.Pop());
            return drawnCards;
        }

        private void Shuffle() {
            var rng = new Random();
            var cards = new List<Card>(_cards);
            var shuffledCards = cards.OrderBy(a => rng.Next()).ToList();
            _cards = new Stack<Card>(shuffledCards);
        }

        public override string ToString() {
            string output = "[";
            foreach (Card card in new List<Card>(_cards))
                output += (card.ToString() + ", ");
            return output += "]";
        }

        private List<Card> CreateCardsForDeck() {
            var cards = new List<Card>();
            var suites = new List<Card.Suits>() {
                Card.Suits.Hearts,
                Card.Suits.Clubs,
                Card.Suits.Spades,
                Card.Suits.Diamonds
            };
            for (int value = 2; value <= 14; value++) {
                for (int i = 0; i < suites.Count; i++) {
                    cards.Add(new Card(value, suites[i]));
                }
            }
            return cards;
        }
    }
}