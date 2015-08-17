using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands.Classes {

    public class Hand {
        public readonly List<Card> Cards;

        public Hand(List<Card> cards) {
            if (cards != null)
                Cards = cards.OrderByDescending(card => card.Value).ToList();
            else
                throw new ArgumentNullException("Hand Error", "Hand constructor recieved null instead of cards.");
        }

        public override string ToString() {
            string output = "[";
            for (int i = 0; i < Cards.Count - 1; i++)
                output += (Cards[i].ToString() + ", ");
            return output + Cards[Cards.Count - 1].ToString() + "]";
        }
    }
}