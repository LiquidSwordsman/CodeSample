using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Classes {
    public class Hand {
        public List<Card> Cards { get; private set; }

        public Hand(List<Card> cards) {
            this.Cards = cards.OrderByDescending(card => card.Value).ToList();
        }

        public override string ToString() {
            string output = "[";
            for (int i = 0; i < Cards.Count-1; i++)
                output += (Cards[i].ToString() + ", ");
            return output + Cards[Cards.Count-1].ToString() + "]";
        }
    }
}
