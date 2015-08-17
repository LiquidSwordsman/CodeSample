using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHands.Classes {

    public static class HandEvaluator {

        public enum HandTypes {
            HighCard = 1,
            Pair = 2,
            TwoPair = 3,
            ThreeOfAKind = 4,
            Straight = 5,
            Flush = 6,
            FullHouse = 7,
            FourOfAKind = 8,
            StraightFlush = 9
        }

        public static Player DetermineWinner(Player player1, Player player2) {
            var player1Points = (int)player1.HandType;
            var player2Points = (int)player2.HandType;
            Player winner;

            // Player 1 wins by points
            if (player1Points > player2Points)
                winner = player1;

            //Points are tied
            else if (player1Points == player2Points)
                // P1 high card
                if (player1.HighCard.Value > player2.HighCard.Value)
                    winner = player1;
                // Point and high card tie
                else if (player1.HighCard.Value == player2.HighCard.Value)
                    winner = null;
                //P2 high card
                else
                    winner = player2;
            // P2 point win.
            else
                winner = player2;
            return winner;
        }

        public static string GetHandName(HandTypes handTypes) {
            string text = handTypes.ToString();
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++) {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString().ToLower();
        }

        public static HandTypes GetHandType(List<Card> hand) {
            bool isFlush = IsFlush(hand);
            bool isStraight = IsStraight(hand);

            if (isStraight && isFlush)
                return HandTypes.StraightFlush;
            if (ContainsSetOfSizeX(4, hand))
                return HandTypes.FourOfAKind;
            if (ContainsSetOfSizeX(3, hand) && ContainsSetOfSizeX(2, hand))
                return HandTypes.FullHouse;
            if (isFlush)
                return HandTypes.Flush;
            if (isStraight)
                return HandTypes.Straight;
            if (ContainsSetOfSizeX(3, hand))
                return HandTypes.ThreeOfAKind;
            if (ContainsTwoPairs(hand))
                return HandTypes.TwoPair;
            if (ContainsSetOfSizeX(2, hand))
                return HandTypes.Pair;
            return HandTypes.HighCard;
        }

        private static bool IsFlush(List<Card> hand) {
            return (hand.GroupBy(card => card.Suit).Count() == 1);
        }

        private static bool IsStraight(List<Card> hand) {
            // If there are less than five groups of cards by value (we have two of the same card number)
            if (hand.GroupBy(card => card.Value).Count() != hand.Count())
                return false;

            // Otherwise we can use this lazy check since we know the list is sorted, and contains no duplicates.
            bool isSequential = (hand[hand.Count - 1].Value + hand.Count - 1 == hand[0].Value);
            return isSequential;
        }

        //Check for sets of values of a given quantity.
        private static bool ContainsSetOfSizeX(int sizeBeingLookedFor, List<Card> hand) {
            var groups = hand.GroupBy(card => card.Value);
            return groups.Any(group => group.ToList().Count() == sizeBeingLookedFor);
        }

        private static bool ContainsTwoPairs(List<Card> hand) {
            var orderedGroups = hand.GroupBy(card => card.Value).OrderByDescending(grouping => grouping.Count());
            return (orderedGroups.ElementAt(0).Count() == 2 && orderedGroups.ElementAt(1).Count() == 2);
        }
    }
}