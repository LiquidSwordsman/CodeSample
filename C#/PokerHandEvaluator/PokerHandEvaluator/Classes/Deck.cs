using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Classes {
    public class Deck {
        private Stack<Card> Cards;

        public Deck() {
            List<Card> cards = CreateCardsForDeck();
            Cards = new Stack<Card>(cards);
            Shuffle();
        }

        public List<Card> DrawCards(int numberToBeDrawn){
            var drawnCards = new List<Card>();

            for (int i = 0; i < numberToBeDrawn; i++)
                drawnCards.Add(Cards.Pop());
            return drawnCards;
        }

        public static List<Card> CreateCardsForDeck() {
            var cards = new List<Card>();
            var suites = new List<string>() { "Hearts", "Clubs", "Spades", "Diamonds" };

            for (int value = 2; value <= 14; value++) {
                for (int i = 0; i < suites.Count; i++) {
                    cards.Add(new Card(value, suites[i]));
                }
            }
            return cards;
        }

        // I thought that a Fisher-Yates shuffle or System.Cryptography was a bit excessive for 
        // the needs of this application sample as it doesn't really  need TRUE randomness
        public void Shuffle() {
            var rng = new Random();
            var cards = new List<Card>(Cards);
            var shuffledCards = cards.OrderBy(a => rng.Next()).ToList();
            Cards = new Stack<Card>(shuffledCards);
        }

        public override string ToString() {
            string output = "[";
            foreach(Card card in new List<Card>(Cards))
                output += (card.ToString() + ", ");
            return output += "]";
        }
    }
}
