using PokerHands.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.UnitTests {
    /*
     * 1. Test the creation of the 52 cards (toggle CreateCardsForDeck to public).
     *  1.1. Check the length of the deck to ensure it is 52 cards.
     *  1.2. Check to ensure there are 13 different card values each holding 4 cards.
     *  1.3. Check to ensure there are 4 suites each with 13 cards.
     * 2. Test shuffling of the deck (for the purpose of this test I made shuffle return the 
     *    shuffled list instead of turning it back into a stack and assigning it to Deck.Cards)
     *    by printing the created deck, then the shuffled deck.
     * 3. Test the drawing of five cards.
     */
    public static class TestDeckFunctionality {
        private static List<Card> Cards = Deck.CreateCardsForDeck();

        // Convenient all test in case of changes (multi-deck poker foor example)
        public static void TestAllDeckFunctionality() {
            Console.WriteLine("Press a key to start deck functionality tests.");
            Console.ReadKey();
            Console.WriteLine("Test Deck Count");
            TestDeckCount();
            Console.WriteLine("\nMaking sure there are 13 different values.");
            EnsureDeckHasRightNumberOfValues();
            Console.WriteLine("\nMaking sure the deck contains four suites.");
            EnsureDeckHasRightNumberOfCardsPerSuite();
            Console.WriteLine("\nMaking sure shuffling the deck works");
            TestDeckShuffling();
            Console.WriteLine("\nTesting drawing a five-card hand.");
            TestDrawingAHand();
            Console.WriteLine("\nEnd of Deck Testing (press any key).");
            Console.ReadKey();
        }

        public static void TestDeckCount(){
            // Testing < and > instead of == for more robust feedback in case of failure.
            Debug.Assert(!(Cards.Count < 52), "Deck Length Error", "There are less than 52 cards in the deck!");
            Debug.Assert(!(Cards.Count > 52), "Deck Length Error", "There are more than 52 cards in the deck!");
            Console.WriteLine("The deck contains EXACTLY 52 cards at time of creation.");
            Console.ReadKey();
        }

        public static void EnsureDeckHasRightNumberOfValues() {
            var result = Cards.GroupBy(card => card.Value);
            Console.WriteLine("The results of grouping the deck by value are:");
            foreach (IGrouping<int, Card> valueGrouping in result) {
                Console.WriteLine("    There are {0} cards with a value of {1}.", valueGrouping.Count().ToString(), valueGrouping.Key.ToString());
            }
            Console.ReadKey();
        }

        public static void EnsureDeckHasRightNumberOfCardsPerSuite() {
            var result = Cards.GroupBy(card => card.Suit);
            Console.WriteLine("The results of grouping the deck by suit are:");
            foreach(IGrouping<string, Card> suitGroup in result){
                Console.WriteLine("    There are {0} cards with a suit of {1}.", suitGroup.Count().ToString(), suitGroup.Key);
            }
            Console.ReadKey();
        }

        //From here on its safe just to initialize a new deck since its been proven working.
        //Toggle deck.Shuffle() to public and remove Shuffle from decks constructor to test this.
        public static void TestDeckShuffling() {
            var deck = new Deck();
            Console.WriteLine("The unshuffled deck order is:\n" + deck.ToString());
            deck.Shuffle();
            Console.WriteLine("\nThe shuffled deck order is:\n" + deck.ToString());
            Console.ReadKey();
        }

        public static void TestDrawingAHand() {
            var deck = new Deck();
            deck.Shuffle();
            //Creating and printing a hand was known to be working at this time.
            var hand = new Hand(deck.DrawCards(5));
            Console.WriteLine("The drawn cards were: " + hand.ToString());
            Console.ReadKey();
        }
    }
}
