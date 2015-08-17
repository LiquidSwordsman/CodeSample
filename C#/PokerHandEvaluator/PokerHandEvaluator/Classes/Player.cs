using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Classes {
    public class Player {
        public string Name;
        public Hand Hand;
        public int HandPointValue;
        public string HandName;

        public Player(string name) {
            Name = name;
        }

        public Player(string name, Hand hand)
        {
            Name = name;
            Hand = hand;
        }

        public void DrawNewHand(Deck deck){
            Hand = new Hand(deck.DrawCards(5));
        }
    }
}
