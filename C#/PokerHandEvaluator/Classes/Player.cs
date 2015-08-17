using System;

namespace PokerHands.Classes {

    public class Player {
        public readonly string Name;
        private Hand _hand;
        public HandEvaluator.HandTypes HandType;

        public Card HighCard {
            get { return _hand.Cards[0]; }
        }

        public string HandName {
            get { return HandEvaluator.GetHandName(HandType); }
        }

        public Player(string name) {
            if (name == null)
                throw new ArgumentNullException("Player Instantiation Error", "Player name was null.");
            if (name == string.Empty)
                throw new ArgumentException("Player Instantiation Error", "Player name was an empty string.");
            Name = name;
        }

        public Player(Hand hand) {
            if (hand == null)
                throw new ArgumentNullException("Player Instantiation Error", "Player hand was null");
            _hand = hand;
            Name = "test";
            HandType = HandEvaluator.GetHandType(_hand.Cards);
        }

        public string PrintHand() {
            return _hand.ToString();
        }

        public void DrawNewHand(Deck deck) {
            _hand = new Hand(deck.DrawCards(5));
            HandType = HandEvaluator.GetHandType(_hand.Cards);
        }
    }
}