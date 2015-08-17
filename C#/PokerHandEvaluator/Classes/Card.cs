using System;

namespace PokerHands.Classes {

    public struct Card {

        public enum Suits {
            Clubs,
            Hearts,
            Spades,
            Diamonds
        };

        private Suits _suits;
        public readonly int Value;

        public string Suit {
            get {
                return _suits.ToString();
            }
        }

        public Card(int value, Suits givenSuits) {
            _suits = givenSuits;
            if (value >= 2 && value <= 14)
                Value = value;
            else
                throw new ArgumentOutOfRangeException("Card Constructor Error", "Card constructor recieved a value that was either < 2 or > 14.");
        }

        public override string ToString() {
            return this.Value.ToString() + " of " + Suit;
        }
    }
}