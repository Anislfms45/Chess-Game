using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp104
{
    class ChessBord : Conversion
    {
        /// fulfil_once va remplire la matrice une fois seulement 
        private static int fulfil_once = 0;
        public static Pieces[,] chess;
        // Quand Rotate_player prend un valeur 1 cela veut dire que c'est le toure de Player2 et quand elle prends 0 cela veut dire tour de Player1
        private static int Rotat_Player = 0;
        /// Si un Rois est en echec et mate endgame prend la valeur 1 cela va permettre de sortire de l boucle do While dans la main et finir le jeux
        public static int endgame = 0;
        // c'est deux liste vont contenir les movement des deux joueurs
        private List<string> player1_Moves = new List<string>();
        private List<string> player2_Moves = new List<string>();
        public ChessBord()
        {
            chess = new Pieces[8, 8];
        }
        // funtcion Fulfil Ramplis La matrice Pieces avec les piece
        private void Remplire_la_Matrice()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    chess[i, j] = new vide("uno");
                }
            }
            for (int j = 0; j < 8; j++)
            {
                chess[6, j] = new Pion("White");
                chess[1, j] = new Pion("black");
            }
            chess[7, 4] = new King("White");
            chess[0, 4] = new King("black");

            chess[0, 3] = new Queen("black");

            chess[7, 3] = new Queen("White");

            chess[0, 7] = new tour("black");
            chess[7, 0] = new tour("White");

            chess[7, 7] = new tour("White");
            chess[0, 0] = new tour("black");
            chess[0, 1] = new chvalier("black");
            chess[0, 6] = new chvalier("black");
            chess[7, 1] = new chvalier("White");
            chess[7, 6] = new chvalier("White");
            chess[7, 2] = new fou("White");
            chess[7, 5] = new fou("White");
            chess[0, 2] = new fou("black");
            chess[0, 5] = new fou("black");
        }
        // FUNCTION SHOW AFFICHE LA MATRICE DANS LE COMMANDE "please dont resize the commande size becous of the color"
        public void show()
        {
            if (fulfil_once == 0)
            {
                Remplire_la_Matrice(); fulfil_once++;
            }
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    if (j % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        chess[i, j].Affichage();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        chess[i, j].Affichage();
                    }
                    if (j == 7)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine("");
                        i++;
                        for (j = 0; j < 8; j++)
                        {
                            if (j % 2 == 0)
                            {
                                chess[i, j].Affichage();
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                            }
                            else
                            {
                                chess[i, j].Affichage();
                                Console.BackgroundColor = ConsoleColor.Blue;
                            }
                        }
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("");
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            /// cette partie affiche les movements des players
            Console.Write("Player 1 Moves : ");
            foreach (string a in player1_Moves)
            {
                Console.Write(a + " ");
            }
            Console.WriteLine(" ");
            Console.Write("Player 2 Moves : ");
            foreach (string b in player2_Moves)
            {
                Console.Write(b + " ");
            }
            Console.WriteLine(" ");
        }
        /// Function Player_turn_check elle va chequer c'est le tour de qui Et c'est le movement et legal ou non et si la partie et terminer
        /// 
        private void Player_turn_Validator(int[] conversion)
        {
            string color = Rotat_Player == 0 ? "White" : "black";
            int[] teh = new int[4];
            string chk;
            while (!(chess[conversion[0], conversion[1]].color.Equals(color)) || (chess[conversion[0], conversion[1]].Move_Permited_check(conversion) == false))
            {
                if (chess[conversion[0], conversion[1]].color.Equals(color))
                {
                    Console.WriteLine("Move not Allowed ");
                }
                else
                {
                    Console.WriteLine("Its {0} player Turn", color);
                }
                chk = Console.ReadLine();
                while (!Joue_Permis(chk))
                {
                    chk = Console.ReadLine();
                }
                conversion = Conversio(chk);
                teh = conversion;
                while (!(chess[conversion[0], conversion[1]].color.Equals(color)) || chess[teh[0], teh[1]].Move_Permited_check(conversion) == false)
                {
                    Console.WriteLine("Non Valide Move");
                    chk = Console.ReadLine();
                    while (!Joue_Permis(chk))
                    {
                        chk = Console.ReadLine();
                    }
                    teh = Conversio(chk);
                }
                conversion = teh;
                break;
            }
            if (color == "White")
            {
                player1_Moves.Add(PlayerMoveAdd);
            }
            else
            {
                player2_Moves.Add(PlayerMoveAdd);
            }
            chess[conversion[0], conversion[1]].movve(conversion);
            /// si check mate alors partie terminer 
            /// 
            string Reverse_color = color == "White" ? "black" : "White";

            if (echec_check(Reverse_color, conversion[2], conversion[3]))
            {
                Console.WriteLine("{0} Player Won" , color);
                Console.ReadKey();
                endgame++;
            }
        }
        public void Player_turn_check(int[] conversion)
        {
            if (Rotat_Player == 0)
            {
                Player_turn_Validator(conversion);
                Rotat_Player--;
            }
            else
            {
                Player_turn_Validator(conversion);
                Rotat_Player++;
            }
        }
        /// Function eche_check Elle va verifier c'est un roi et en echec et si il est en echec elle vas verifier si il peut se deplaces si il peut pas elle va verifier si il est possible de manger la piece qui aggrese le Roi sinon elle verifier si ell peut se mettre dans le chemin de la piece
        private bool echec_check(string color, int x, int y)
        {
            Pieces[,] clop = (Pieces[,])chess.Clone();
            var result = color.Equals("White") ? "black" : "White";
            /// check va contenir location du roi et de l'aggreseur
            int[] check = new int[4];
            // CHECK3 elle va contenir la location de l'agresseur et la location de tous les piece de meme color que le roi
            int z = 0;
            int x_King = 0, y_King = 0;
            // LA PARTIE SUIVANT VA VERIFIER SI LE ROI EST EN ECHEQUE 
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chess[i, j] is King & chess[i, j].color.Equals(color))
                    {
                        x_King = i;
                        y_King = j;
                    }
                }
            }
            if (chess[x, y].Move_Permited_check(new int[] { x, y, x_King, y_King }))
            {
                Console.WriteLine("**************** {0} King is In check *************", color);
                Console.ReadKey();
                z++;
            }
            // SI LE ROIS EST EN ECHEQUE VERIFIER SI IL PEUT BOUGER OU SECRIFIER UNE PIECES DE MEME COLORS
            if (z > 0)
            {
                //king and it cannot move to any other squares
                int[] King_move = new int[] { -1, 1, 0 };
                int[] Next_Clone_King_Move;
                foreach (int move in King_move)
                {
                    foreach (int Sec_move in King_move)
                    {
                        Next_Clone_King_Move = new int[] { x_King, y_King, x_King + move, y_King + Sec_move };
                        //if king can move and offender can't eat it then we don't have a check mate
                        if (x_King + move >= 0 && y_King + Sec_move <= 7 && x_King + move <= 7 && y_King + Sec_move >= 0)
                        {
                            if (chess[x_King, y_King].Move_Permited_check(Next_Clone_King_Move) && !chess[x, y].Move_Permited_check(new int[] {x, y, x_King + move, y_King + Sec_move }))
                            {
                                return false;
                            }
                        }
                    }
                }
                //checking piece cannot be captured.
                if (check_if_move_allowed_to_stop_check(color, x, y) == false)
                {
                    return false;
                } 
                //cannot be protected by another piece
                if (!(chess[x,y] is chvalier))
                {
                    int proces_x =x_King - x;
                    int proces_y =y_King - y;
                    int Pos_x = proces_x < 0 ? -1 : (proces_x == 0 ? 0 : 1) ;
                    int Pos_y = proces_y < 0 ? -1 : (proces_y == 0 ? 0 : 1);
                    proces_x = Math.Abs(proces_x);
                    proces_y = Math.Abs(proces_y);
                    int i = 1;
                   //cords of offnder positions to get to king 
                        while (i < (proces_x > proces_y ? proces_x : proces_y))
                        {
                            if (check_if_move_allowed_to_stop_check(color, ((Pos_x * i) + x), ((Pos_y * i) + y)) == false)
                            {
                            return false;
                            }
                            i++;
                        }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool check_if_move_allowed_to_stop_check(string color , int x , int y)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chess[i, j].color.Equals(color))
                    {
                        if (chess[i, j].Move_Permited_check(new int[] { i, j, x, y }))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
    
}
