using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Classes {
    public struct Card {
        public string Suit;
        public int Value;
            
        public Card(int value, string suit) {
            Suit = suit;
            Value = value;
        }

        public override string ToString() {
            return this.Value.ToString() + " of " + Suit;
        }
    }
}

