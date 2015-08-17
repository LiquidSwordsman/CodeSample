using PokerHands.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.UnitTests {
    public static class TestPlayer {
        /*
         * 1. Test creation of player.
         * 2. Test drawing of a hand.
         */
        public static Player testPlayer = new Player("SPOOKY_GHOST");

        public static void TestPlayerFunctionality() {
            Debug.Assert(testPlayer != null, "Player Error", "Player was not created properly and is null.");
            var deck = new Deck();
            testPlayer.DrawNewHand(deck);
            Debug.Assert(testPlayer.Hand.Cards.Count() == 5, "Player Error", "Player failed to draw exactly five cards.");
            Console.WriteLine("The player has been properly instantiated and has drawn 5 cards!");
            Console.ReadKey();
        }

    }
}
