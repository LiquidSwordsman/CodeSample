using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerHands.Classes;

namespace PokerHandsTest {

    [TestClass]
    public class TestHandEvaluatorFunctionality {

        public static Hand StraightFlush = new Hand(new List<Card>(){
                    new Card(3, Card.Suits.Hearts),
                    new Card(4, Card.Suits.Hearts),
                    new Card(5, Card.Suits.Hearts),
                    new Card(6, Card.Suits.Hearts),
                    new Card(7, Card.Suits.Hearts)
                });

        // Flush but not straight
        public static Hand Flush = new Hand(new List<Card>(){
                    new Card(2, Card.Suits.Hearts),
                    new Card(4, Card.Suits.Hearts),
                    new Card(5, Card.Suits.Hearts),
                    new Card(6, Card.Suits.Hearts),
                    new Card(7, Card.Suits.Hearts)
                });

        // Straight but not flush
        public static Hand StraightNoAce = new Hand(new List<Card>(){
                    new Card(3, Card.Suits.Clubs),
                    new Card(4, Card.Suits.Hearts),
                    new Card(5, Card.Suits.Hearts),
                    new Card(6, Card.Suits.Hearts),
                    new Card(7, Card.Suits.Hearts)
                });

        // Straight (but not flush) with low ace (special case).
        public static Hand WheelStraight = new Hand(new List<Card>(){
                    new Card(14, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Hearts),
                    new Card(3, Card.Suits.Hearts),
                    new Card(4, Card.Suits.Hearts),
                    new Card(5, Card.Suits.Hearts)
                });

        public static Hand FourOfAKindHighCard = new Hand(new List<Card>(){
                    new Card(14, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Hearts),
                    new Card(2, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Spades),
                    new Card(2, Card.Suits.Diamonds)
                });

        public static Hand FourOfAKindLowCard = new Hand(new List<Card>(){
                    new Card(13, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Hearts),
                    new Card(2, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Spades),
                    new Card(2, Card.Suits.Diamonds)
                });

        public static Hand FullHouse = new Hand(new List<Card>(){
                    new Card(3, Card.Suits.Clubs),
                    new Card(3, Card.Suits.Hearts),
                    new Card(2, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Spades),
                    new Card(2, Card.Suits.Diamonds)
                });

        public static Hand ThreeOfAKind = new Hand(new List<Card>(){
                    new Card(14, Card.Suits.Clubs),
                    new Card(10, Card.Suits.Hearts),
                    new Card(2, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Spades),
                    new Card(2, Card.Suits.Diamonds)
                });

        public static Hand TwoPair = new Hand(new List<Card>(){
                    new Card(14, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Hearts),
                    new Card(2, Card.Suits.Clubs),
                    new Card(3, Card.Suits.Spades),
                    new Card(3, Card.Suits.Diamonds)
                });

        public static Hand OnePair = new Hand(new List<Card>(){
                    new Card(14, Card.Suits.Clubs),
                    new Card(2, Card.Suits.Hearts),
                    new Card(2, Card.Suits.Clubs),
                    new Card(4, Card.Suits.Spades),
                    new Card(8, Card.Suits.Diamonds)
                });

        public static Hand HighCard = new Hand(new List<Card>(){
                    new Card(14, Card.Suits.Clubs),
                    new Card(3, Card.Suits.Hearts),
                    new Card(2, Card.Suits.Clubs),
                    new Card(5, Card.Suits.Spades),
                    new Card(8, Card.Suits.Diamonds)
                });

        [TestMethod]
        public void TestFlushDetection() {
            var handEvaluator = new PrivateType(typeof(HandEvaluator));
            Assert.IsTrue((bool)handEvaluator.InvokeStatic("IsFlush", Flush.Cards), "Flush Error", "Failed to identify a flush.");
            Assert.IsFalse((bool)handEvaluator.InvokeStatic("IsFlush", StraightNoAce.Cards), "Flush Error", "Falsely identified a non flush hand as flush.");
        }

        [TestMethod]
        public void TestStraightDetection() {
            var handEvaluator = new PrivateType(typeof(HandEvaluator));
            Assert.IsFalse((bool)handEvaluator.InvokeStatic("IsStraight", FourOfAKindHighCard.Cards), "Straight Error", "Evaluated a hand with duplicate values as straight.");
            Assert.IsTrue((bool)handEvaluator.InvokeStatic("IsStraight", StraightNoAce.Cards), "Straight Error", "Failed to detect a standard straight");
        }

        [TestMethod]
        public void TestSetDetection() {
            var handEvaluator = new PrivateType(typeof(HandEvaluator));
            Assert.IsTrue((bool)handEvaluator.InvokeStatic("ContainsSetOfSizeX", 4, FourOfAKindHighCard.Cards), "Set Identification error.", "Failed to properly identify a four of a kind.");
            Assert.IsFalse((bool)handEvaluator.InvokeStatic("ContainsSetOfSizeX", 2, StraightNoAce.Cards), "Set Identification error.", "Detected a two of a kind when given a straight.");
        }

        [TestMethod]
        public void TestTwoPairDetection() {
            var handEvaluator = new PrivateType(typeof(HandEvaluator));
            Assert.IsTrue((bool)handEvaluator.InvokeStatic("ContainsTwoPairs", TwoPair.Cards), "Two pair error", "Failed to detect two pairs in a hand.");
            Assert.IsFalse((bool)handEvaluator.InvokeStatic("ContainsTwoPairs", OnePair.Cards), "Two pair error", "Detected two pair when given one pair");
            Assert.IsFalse((bool)handEvaluator.InvokeStatic("ContainsTwoPairs", HighCard.Cards), "Two pair error", "Detected a high card hand as two pairs.");
        }

        [TestMethod]
        public void TestHandRankings() {
            var handEvaluator = new PrivateType(typeof(HandEvaluator));
            Assert.AreEqual(9, (int)handEvaluator.InvokeStatic("GetHandType", StraightFlush.Cards), "Point Error", "Straight flush did not get the appropriate number of points");
            Assert.AreEqual(8, (int)handEvaluator.InvokeStatic("GetHandType", FourOfAKindHighCard.Cards), "Point Error", "Four of a kind did not get the appropriate point value.");
            Assert.AreEqual(7, (int)handEvaluator.InvokeStatic("GetHandType", FullHouse.Cards), "Point Error", "Full house did not get the appropriate point value.");
            Assert.AreEqual(6, (int)handEvaluator.InvokeStatic("GetHandType", Flush.Cards), "Point Error", "Flush did not get the appropriate point value.");
            Assert.AreEqual(5, (int)handEvaluator.InvokeStatic("GetHandType", StraightNoAce.Cards), "Point Error", "Straight did not get the appropriate point value.");
            Assert.AreEqual(4, (int)handEvaluator.InvokeStatic("GetHandType", ThreeOfAKind.Cards), "Point Error", "Three of a kind did not get the appropriate point value.");
            Assert.AreEqual(3, (int)handEvaluator.InvokeStatic("GetHandType", TwoPair.Cards), "Point Error", "Two pair did not get the appropriate point value.");
            Assert.AreEqual(2, (int)handEvaluator.InvokeStatic("GetHandType", OnePair.Cards), "Point Error", "Pair did not get the appropriate point value.");
            Assert.AreEqual(1, (int)handEvaluator.InvokeStatic("GetHandType", HighCard.Cards), "Point Error", "High card did not get the appropriate point value.");
        }

        /*
         * 1. A player wins based on point value.
         * 2. Point values are tied and a player wins based on a high card.
         * 3. Both points and high card are tied.
         */

        [TestMethod]
        public void TestDeterminingWinners() {
            var fourOfAKindHighCard = new Player(FourOfAKindHighCard);
            var fourOfAKindLowCard = new Player(FourOfAKindLowCard);
            var twoPair = new Player(TwoPair);
            var lowCardTie = new Player(FourOfAKindLowCard);

            // Check for standard win.
            Assert.AreSame(fourOfAKindHighCard, HandEvaluator.DetermineWinner(fourOfAKindHighCard, twoPair), "Winner Error", "Improperly evaluated a winner based on points.");
            Assert.AreSame(fourOfAKindHighCard, HandEvaluator.DetermineWinner(fourOfAKindHighCard, fourOfAKindLowCard), "Winner Error", "Improperly evaluated the winner of a point tie using the high card");
            Assert.IsNull(HandEvaluator.DetermineWinner(fourOfAKindLowCard, lowCardTie), "Winner Error", "Failed to detect a tie in both points and high card");
        }
    }
}