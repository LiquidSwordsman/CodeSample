using PokerHands.Classes;
using PokerHands.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands {
    class Program {
        static void Main(string[] args) {
            //    var player1 = new Player("Chris");
            //    var player2 = new Player("Corey");
            //    var deck = new Deck();
            //    player1.DrawNewHand(deck);
            //    player2.DrawNewHand(deck);
            //    Player winner = HandEvaluator.DetermineWinner(player1, player2);
            //    Player loser = (winner == player1) ? player2 : player1;
            //    OutputWinnerInfo(winner, loser);
            TestHandEvaluator.TestDetermineWinner();
        }

        static void OutputWinnerInfo(Player winner, Player loser) {
            Console.WriteLine("{0} beat {1}.\nThe winning hand was a {2} that consisted of:\n{3}\n\nThe losing hand was a {4} consisting of:\n{5}",
                winner.Name, loser.Name, winner.HandName, winner.Hand.ToString(), loser.HandName, loser.Hand.ToString());
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }

        static void RunFullTestSuite() {
            Console.WriteLine("Testing all program functionality, press any key after each output.");
            TestDeckFunctionality.TestAllDeckFunctionality();
            TestHandEvaluator.TestHandRankings();
            TestPlayer.TestPlayerFunctionality();
        }
    }
}
