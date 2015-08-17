using System;
using PokerHands.Classes;

namespace PokerHands {

    internal class Program {

        private static void Main(string[] args) {
            var player1 = new Player("Chris");
            var player2 = new Player("Corey");
            var deck = new Deck();
            player1.DrawNewHand(deck);
            player2.DrawNewHand(deck);
            Player winner = HandEvaluator.DetermineWinner(player1, player2);
            if (winner != null) {
                Player loser = (winner == player1) ? player2 : player1;
                OutputWinnerInfo(winner, loser);
            }
            else
                OutputTie(player1, player2);
        }

        private static void OutputWinnerInfo(Player winner, Player loser) {
            Console.WriteLine("{0} beat {1}.\nThe winning hand was a {2} that consisted of:\n{3}\n\nThe losing hand was a {4} consisting of:\n{5}",
                winner.Name, loser.Name, winner.HandName, winner.PrintHand(), loser.HandName, loser.PrintHand());
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }

        private static void OutputTie(Player player1, Player player2) {
            Console.WriteLine("{0} and {1} tied. They both drew a {2} with a high card of {3}.", player1.Name, player2.Name, player1.HandName, player1.HighCard);
            Console.WriteLine("{0}'s hand was:\n{1}\n\n{2}'s hand was:\n{4}", player1.Name, player1.PrintHand(), player2.Name, player2.PrintHand());
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }
    }
}