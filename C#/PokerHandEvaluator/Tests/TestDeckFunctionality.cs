using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerHands.Classes;

namespace PokerHandsTest {

    [TestClass]
    public class TestDeckFunctionality {

        [TestMethod]
        public void TestDeckCount() {
            var testDeck = new PrivateObject(typeof(Deck));
            // Testing < and > instead of == for more robust feedback in case of failure.
            var cardsInTestDeck = new List<Card>((Stack<Card>)testDeck.GetFieldOrProperty("_cards"));
            Assert.IsFalse(cardsInTestDeck.Count < 52, "Deck Length Error", "There are less than 52 cards in the deck!");
            Assert.IsFalse(cardsInTestDeck.Count > 52, "Deck Length Error", "There are more than 52 cards in the deck!");
        }

        [TestMethod]
        public void EnsureDeckHasRightNumberOfValues() {
            var testDeck = new PrivateObject(typeof(Deck));
            var cardsInTestDeck = new List<Card>((Stack<Card>)testDeck.GetFieldOrProperty("_cards"));
            var result = cardsInTestDeck.GroupBy(card => card.Value);
            Assert.IsFalse(result.Count() < 13, "Value Error", "The deck contains less than thirteen values.");
            Assert.IsFalse(result.Count() > 13, "Value Error", "The deck contains more than thirteen values.");
        }

        [TestMethod]
        public void EnsureDeckHasRightNumberOfSuites() {
            var testDeck = new PrivateObject(typeof(Deck));
            var cardsInTestDeck = new List<Card>((Stack<Card>)testDeck.GetFieldOrProperty("_cards"));
            var result = cardsInTestDeck.GroupBy(card => card.Suit);
            Assert.IsFalse(result.Count() < 4, "Suits Error", "The deck contains less than four suits.");
            Assert.IsFalse(result.Count() > 4, "Suits Error", "The deck contains more than four suits.");
        }

        [TestMethod]
        public void TestDrawingAHand() {
            var deck = new Deck();
            var cardsToDraw = 5;
            List<Card> drawnCards = deck.DrawCards(cardsToDraw);
            Assert.AreEqual(cardsToDraw, drawnCards.Count, "Hand Error",
                "The deck did not draw the expected number of cards.");
            foreach (var card in drawnCards)
                Assert.IsInstanceOfType(card, typeof(Card), "Card Error",
                    "The list of drawn cards contained something that wasn't a card.");
        }
    }
}