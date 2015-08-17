using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerHands.Classes;

namespace PokerHandsTest {

    [TestClass]
    public class TestCardFunctionality {

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCardValueLowerBound() {
            var testCard = new Card(1, Card.Suits.Clubs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCardValueUpperBound() {
            var testCard = new Card(15, Card.Suits.Clubs);
        }

        [TestMethod]
        public void TestCardInstantiation() {
            var testCard = new Card(2, Card.Suits.Clubs);
            Assert.IsNotNull(testCard, "Card Instantiation Error", "testCard is null.");
            Assert.IsInstanceOfType(testCard, typeof(Card), "Card Instantiation Error", "Card is not really a card.");
            Console.WriteLine(testCard);
        }
    }
}