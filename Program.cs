using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp104
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessBord game = new ChessBord();
            Console.Title = "CHESS GAME";
            do
            {
                game.show();
                Console.WriteLine("Your Move");
                string move = Console.ReadLine();
                if (game.Joue_Permis(move))
                {
                    game.Player_turn_check(game.Conversio(move));
                    Console.Clear();
                }
            } while (ChessBord.endgame == 0);
            Console.WriteLine("Game Ended ");
            Console.ReadKey();
        }
    }
}
