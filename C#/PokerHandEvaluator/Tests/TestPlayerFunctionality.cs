using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerHands.Classes;

namespace PokerHandsTest {

    [TestClass]
    public class TestPlayerFunctionality {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnsurePlayerNameCannotBeEmpty() {
            var player = new Player(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnsurePlayerNameCannotBeNull() {
            var player = new Player(name: null);
        }

        // Player draw hand is covered in the test of deck.draw hand as well as the tests of Hand Evaluator.
    }
}