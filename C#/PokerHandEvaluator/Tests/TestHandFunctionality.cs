using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerHands.Classes;

namespace PokerHandsTest {

    [TestClass]
    public class TestHandFunctionality {

        public List<Card> SampleHand = new List<Card>(){
                new Card(2, Card.Suits.Spades),
                new Card(6, Card.Suits.Hearts),
                new Card(14, Card.Suits.Diamonds),
                new Card(13, Card.Suits.Clubs),
                new Card(4, Card.Suits.Hearts)
            };

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHandInstantiation() {
            var hand = new Hand(null);
            Assert.IsInstanceOfType(new Hand(SampleHand), typeof(Hand), "Instantiation Error", "Hand failed to properly instantiate.");
        }

        [TestMethod]
        public void TestHandOrdering() {
            var hand = new Hand(SampleHand);
            Assert.IsFalse(SampleHand.SequenceEqual(hand.Cards), "Hand Order Failure", "The hand was not ordered properly");
        }
    }
}