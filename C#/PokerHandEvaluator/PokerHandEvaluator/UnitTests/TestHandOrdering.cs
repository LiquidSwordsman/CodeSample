using PokerHands.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.UnitTests {
    /*
     * 1. Provide a hand of cards to test with.
     * 2. Make sure the class initializes fine.
     * 3. Ensure that the sort is returning a list of cards.
     * 4. Ensure sort order and test to string override by printing the list.
     */
    
    public static class TestHandOrdering {
        public static List<Card> hand1 = new List<Card>(){
            new Card(2, "Spades"),
            new Card(6, "Hearts"),
            new Card(14, "Diamonds"),
            new Card(13, "Clubs"),
            new Card(4, "Hearts")
        };

        public static void TestHandOrder(List<Card> cards){
            Hand orderedHand = new Hand(cards);
            Debug.Assert(orderedHand.Cards is List<Card>, "Type Error", "orderedHand.Cards is not actually a List<Card>!");
            Debug.Assert(orderedHand is Hand, "Type Error", "orderedHand is not actually a Hand!");
            Console.WriteLine(orderedHand);
            Console.ReadKey();
        }
    }
}
