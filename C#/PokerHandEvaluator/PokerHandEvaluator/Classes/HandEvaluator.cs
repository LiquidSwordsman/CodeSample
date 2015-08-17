using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Classes {
    class HandEvaluator {
        enum HandType { 
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
            Player winner;
            player1.HandPointValue = GetHandType(player1.Hand);
            player2.HandPointValue = GetHandType(player2.Hand);
            player1.HandName = GetHandName(player1.HandPointValue);
            player2.HandName = GetHandName(player2.HandPointValue);

            // Player 1 wins by points
            if (player1.HandPointValue > player2.HandPointValue)
                winner = player1;
            
            //Points are tied
            else if (player1.HandPointValue == player2.HandPointValue)
                // P1 high card
                if (player1.Hand.Cards[0].Value > player2.Hand.Cards[0].Value)
                    winner = player1;
                // Point and high card tie
                else if (player1.Hand.Cards[0].Value > player2.Hand.Cards[0].Value)
                    winner = null;
                //P2 high card
                else
                    winner = player2;
            // P2 point win.
            else
                winner = player2;
            return winner;
        }

        public static string GetHandName(int pointValue) {
            string handName = "";
            if (pointValue == 1)
                handName = "high card";
            if (pointValue == 2)
                handName = "pair";
            if (pointValue == 3)
                handName = "two pairs";
            if (pointValue == 4)
                handName = "three of a kind";
            if(pointValue == 5)
                handName = "straight";
            if (pointValue == 6)
                handName = "flush";
            if (pointValue == 7)
                handName = "full house";
            if (pointValue == 8)
                handName = "four of a kind";
            if (pointValue == 9)
                handName = "straight flush";
            return handName;
        }

        public static int GetHandType(Hand Hand){
            List<Card> hand = Hand.Cards;
            bool isFlush = IsFlush(hand);
            bool isStraight = IsStraight(hand);

            if (isStraight && isFlush)
                return (int)HandType.StraightFlush;
            else if (ContainsSetOfX(4, hand))
                return (int)HandType.FourOfAKind;
            else if (ContainsSetOfX(3, hand) && ContainsSetOfX(2, hand))
                return (int)HandType.FullHouse;
            else if (isFlush)
                return (int)HandType.Flush;
            else if (isStraight)
                return (int)HandType.Straight;
            else if (ContainsSetOfX(3, hand))
                return (int)HandType.ThreeOfAKind;
            if (ContainsTwoPairs(hand))
                return (int)HandType.TwoPair;
            else if (ContainsSetOfX(2, hand))
                return (int)HandType.Pair;
            else
                return (int)HandType.HighCard;
        }

        public static bool IsFlush(List<Card> hand){
            var result = hand.GroupBy(card => card.Suit);
            if(result.Count() == 1)
                return true;
            else
                return false;
        }

        public static bool IsStraight(List<Card> hand, bool wheelEnabled=false) {
            // If there are less than five groups of cards by value (we have two of the same card number)
            if(hand.GroupBy(card => card.Value).Count() != hand.Count())
                return false;
            
            // If the hand contains an ace
            if (wheelEnabled && hand.Any(card => card.Value == 14))
                // And the ace is a low card in an A-5 straight (wheel).
                if(hand[hand.Count-1].Value == hand[1].Value + hand.Count - 2)
                    return true;

            // Otherwise we can use this lazy check since we know the list is sorted, and contains no duplicates.            
            bool isSequential = (hand[hand.Count - 1].Value + hand.Count - 1 == hand[0].Value);
            return isSequential;
        }

        //Check for sets of values of a given quantity.
        public static bool ContainsSetOfX(int setSizeBeingLookedFor, List<Card> hand) {
            var groups = hand.GroupBy(card => card.Value);
            foreach (var group in groups)
                if (group.ToList().Count() == setSizeBeingLookedFor)
                    return true;
            return false;
        }

        //Inelegant handling of checking for two pairs.
        public static bool ContainsTwoPairs(List<Card> hand) {
            var groups = hand.GroupBy(card => card.Value);
            IGrouping<int, Card> group1 = null;
            foreach (var group in groups)
                if (group.ToList().Count() == 2)
                    if (group1 == null)
                        group1 = group;
                    else if(group1 != group)
                        return true;
            return false;
        }
    }
}
