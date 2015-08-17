using PokerHands.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.UnitTests {
    public static class TestHandEvaluator {

        private static Hand straightFlush = new Hand(new List<Card>(){
            new Card(3, "Heart"), 
            new Card(4, "Heart"), 
            new Card(5, "Heart"), 
            new Card(6, "Heart"), 
            new Card(7, "Heart")
        });

        // Flush but not straight
        private static Hand flushHand = new Hand(new List<Card>(){
            new Card(2, "Heart"), 
            new Card(4, "Heart"), 
            new Card(5, "Heart"), 
            new Card(6, "Heart"), 
            new Card(7, "Heart")
        });

        // Straight but not flush
        private static Hand straightHandNoAce = new Hand(new List<Card>(){
            new Card(3, "Clubs"), 
            new Card(4, "Heart"), 
            new Card(5, "Heart"), 
            new Card(6, "Heart"), 
            new Card(7, "Heart")
        });

        // Straight (but not flush) with low ace (special case).
        private static Hand wheelStraight = new Hand(new List<Card>(){
            new Card(14, "Clubs"), 
            new Card(2, "Heart"), 
            new Card(3, "Heart"), 
            new Card(4, "Heart"), 
            new Card(5, "Heart")
        });

        private static Hand fourOfAKind = new Hand(new List<Card>(){
            new Card(14, "Clubs"), 
            new Card(2, "Heart"), 
            new Card(2, "Clubs"), 
            new Card(2, "Spades"), 
            new Card(2, "Diamonds")
        });

        private static Hand fullHouse = new Hand(new List<Card>(){
            new Card(3, "Clubs"), 
            new Card(3, "Heart"), 
            new Card(2, "Clubs"), 
            new Card(2, "Spades"), 
            new Card(2, "Diamonds")
        });

        private static Hand threeOfAKind = new Hand(new List<Card>(){
            new Card(14, "Clubs"), 
            new Card(10, "Heart"), 
            new Card(2, "Clubs"), 
            new Card(2, "Spades"), 
            new Card(2, "Diamonds")
        });

        private static Hand twoPair= new Hand(new List<Card>(){
            new Card(14, "Clubs"), 
            new Card(2, "Heart"), 
            new Card(2, "Clubs"), 
            new Card(3, "Spades"), 
            new Card(3, "Diamonds")
        });

        private static Hand onePair = new Hand(new List<Card>(){
            new Card(14, "Clubs"), 
            new Card(2, "Heart"), 
            new Card(2, "Clubs"), 
            new Card(4, "Spades"), 
            new Card(8, "Diamonds")
        });

        private static Hand highCard = new Hand(new List<Card>(){
            new Card(14, "Clubs"), 
            new Card(1, "Heart"), 
            new Card(2, "Clubs"), 
            new Card(5, "Spades"), 
            new Card(8, "Diamonds")
        });

        /* Toggle IsFlush to Public to test.
         * 1. Ensure that it returns true with a flush.
         * 2. Make sure it returns false when provided otherwise.
         */
        public static void TestFlushDetection() {
            Debug.Assert(HandEvaluator.IsFlush(flushHand.Cards), "Flush Error", "Failed to detect that a flush hand is a flush.");
            Debug.Assert(!HandEvaluator.IsFlush(straightHandNoAce.Cards), "Flush Error", "Falsely identified a non flush hand as flush.");
            Console.WriteLine("Can properly check if a hand is a flush.");
            Console.ReadKey();
        }

        /* 
         * 1. Ensure that it outright rejects hands with more than one card of the same value (like two of a kind).
         * 2. DISABLED - Ensure that it properly detects wheel straights.
         * 3. Ensure it can properly detect a standard straight
         */
        public static void TestStraightDetection() {
            Debug.Assert(!HandEvaluator.IsStraight(fourOfAKind.Cards), "Straight Error", "Evaluated a hand with duplicate values as straight.");
            // Debug.Assert(HandEvaluator.IsStraight(wheelStraight.Cards), "Straight Error", "Failed to detect a wheel straight");
            Debug.Assert(HandEvaluator.IsStraight(straightHandNoAce.Cards), "Straight Error", "Failed to detect a standard straight");
            Console.WriteLine("Can properly check if a hand is or is not a straight!");
            Console.ReadKey();
        }

        /* 
         * 1. Ensure that it can properly detect a four of a kind.
         * 2. Ensure that it can identify when it fails (test using a straight looking for a two of a kind).
         */
        public static void TestSetDetection() {
            Debug.Assert(HandEvaluator.ContainsSetOfX(4, fourOfAKind.Cards), "Set Identification error.", "Failed to properly identify a four of a kind.");
            Debug.Assert(!HandEvaluator.ContainsSetOfX(2, straightHandNoAce.Cards), "Set Identification error.", "Detected a two of a kind when given a straight.");
            Console.WriteLine("Can properly identify sets of cards of a given number.");
            Console.ReadKey();
        }

        /*
         * Ensure it can properly detect two pair.
         * Ensure it returns false on one pair.
         * Ensure it returns false on everything else.
         */
        public static void TestTwoPairDetection() {
            Debug.Assert(HandEvaluator.ContainsTwoPairs(twoPair.Cards), "Two pair error", "Failed to detect two pairs in a hand.");
            Debug.Assert(!HandEvaluator.ContainsTwoPairs(onePair.Cards), "Two pair error", "Detected two pair when given one pair");
            Debug.Assert(!HandEvaluator.ContainsTwoPairs(highCard.Cards), "Two pair error", "Detected a high card hand as two pairs.");
            Console.WriteLine("Can properly identify hands that do or do not contain two pairs!");
            Console.ReadKey();
        }
        /*
         * Ensure that each hand gets the expected point value back. This also funtions as a full test of all hand evaluator functions.
         */
        public static void TestHandRankings() {
            Debug.Assert(HandEvaluator.GetHandType(straightFlush) == 9,"Point Error","Straight flush did not get the appropriate number of points");
            Debug.Assert(HandEvaluator.GetHandType(fourOfAKind) == 8, "Point Error","Four of a kind did not get the appropriate point value.");
            Debug.Assert(HandEvaluator.GetHandType(fullHouse) == 7,"Point Error","Full house did not get the appropriate point value.");
            Debug.Assert(HandEvaluator.GetHandType(flushHand) == 6, "Point Error","Flush did not get the appropriate point value.");
            Debug.Assert(HandEvaluator.GetHandType(straightHandNoAce) == 5,"Point Error","Straight did not get the appropriate point value.");
            Debug.Assert(HandEvaluator.GetHandType(threeOfAKind) == 4, "Point Error", "Three of a kind did not get the appropriate point value.");
            Debug.Assert(HandEvaluator.GetHandType(twoPair) == 3,"Point Error", "Two pair did not get the appropriate point value.");
            Debug.Assert(HandEvaluator.GetHandType(onePair) == 2,"Point Error","Pair did not get the appropriate point value.");
            Debug.Assert(HandEvaluator.GetHandType(highCard) == 1,"Point Error","High card did not get the appropriate point value.");
            Console.WriteLine("Points are being allocated to the appropriate hand correctly.");
            Console.ReadLine();
        }

        /* 
         * 1. Check three different combinations of hands to ensure that the winner and loser are being properly 
              identified. 
         * 2. Check to ensure that ties can be properly detected.
         */
        public static void TestDetermineWinner() {
            var flush = new Player("Winner", flushHand);
            var highCardPlayer = new Player("Loser", highCard);
            Debug.Assert((flush.Equals(HandEvaluator.DetermineWinner(flush, highCardPlayer))), "Victory Error", "Determine winner failed to properly identify a winner");
            Debug.Assert((null == HandEvaluator.DetermineWinner(highCardPlayer, highCardPlayer)));
        }
    }
}
