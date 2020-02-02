using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp104
{
    abstract class Pieces 
    {
        public string color;
        public void movve(int[] conversion)
        {
           ChessBord.chess[conversion[2], conversion[3]] = ChessBord.chess[conversion[0], conversion[1]];
           ChessBord.chess[conversion[0], conversion[1]] = new vide("uno");
        }
        public abstract void Affichage();
        public abstract bool Move_Permited_check(int[] conversion);
        public Pieces(string color)
        {
            this.color = color;
        }
    }
    class vide : Pieces
    {
        public vide(string color) : base(color) { }
        public override void Affichage()
        { 
            Console.Write("   ");
        }
        public override bool Move_Permited_check(int[] conversion)
        {
            return false;
        }
    }
    class chvalier : Pieces
    {
        public chvalier(string color) : base(color) { }
        public override void Affichage()
        {
            var result = this.color.Equals("White") ? "WC " : "BC ";
            Console.Write(result);
        }
        public override bool Move_Permited_check(int[] conversion)
        {
            if (!ChessBord.chess[conversion[2], conversion[3]].color.Equals(ChessBord.chess[conversion[0], conversion[1]].color))
            {
                if (Math.Abs(conversion[2] - conversion[0]) == 2 && Math.Abs(conversion[3] - conversion[1]) == 1 || Math.Abs(conversion[2] - conversion[0]) == 1 && Math.Abs(conversion[3] - conversion[1]) == 2)
                    return true;
            }
            return false;
        }
    }
    class fou : Pieces
    {
        public fou(string color) : base(color) { }
        public override void Affichage()
        {
            var result = this.color.Equals("White") ? "WF " : "BF ";
            Console.Write(result);
        }
        public override bool Move_Permited_check(int[] conversion)
        {
            string swtcolor = color.Equals("White") ? "black" : "White";
            int k = 0;
            if (!(ChessBord.chess[conversion[2], conversion[3]].color.Equals(this.color)))
            {
                ////////////////////////////////////////////// Diagonale Invers CHECKED PERFECT///////////////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i < 8; i++)
                {
                    if ((conversion[2] == conversion[0] - i && conversion[3] == conversion[1] + i) || (conversion[2] == conversion[0] + i && conversion[3] == conversion[1] - i))
                    {
                        k = 1;
                        if (conversion[0] > conversion[2])
                        {
                            for (int j = 1; j < i; j++)
                            {
                                if (ChessBord.chess[conversion[0] - j, conversion[1] + j].color.Equals(this.color) || ChessBord.chess[conversion[0] - j, conversion[1] + j].color.Equals(swtcolor))
                                {
                                    return false;
                                }
                            }
                        }
                        if (conversion[0] < conversion[2])
                        {
                            for (int j = 1; j < i; j++)
                            {
                                if (ChessBord.chess[conversion[0] + j, conversion[1] - j].color.Equals(this.color) || ChessBord.chess[conversion[0] + j, conversion[1] - j].color.Equals(swtcolor))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    ////////////////////////////////////////////// Diagonale CHECKED PERFECT///////////////////////////////////////////////////////////////////////////////////////
                    if ((conversion[2] == conversion[0] - i && conversion[3] == conversion[1] - i) || (conversion[2] == conversion[0] + i && conversion[3] == conversion[1] + i))
                    {
                        k = 1;
                        if (conversion[0] > conversion[2])
                        {
                            for (int j = 1; j < i; j++)
                            {
                                if (ChessBord.chess[conversion[0] - j, conversion[1] - j].color.Equals(this.color) ||ChessBord.chess[conversion[0] - j, conversion[1] - j].color.Equals(swtcolor))
                                {
                                    return false;
                                }
                            }
                        }
                        if (conversion[0] < conversion[2])
                        {
                            for (int j = 1; j < i; j++)
                            {
                                if (ChessBord.chess[conversion[0] + j, conversion[1] + j].color.Equals(this.color) || ChessBord.chess[conversion[0] + j, conversion[1] + j].color.Equals(swtcolor))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            if (k == 1)
                return true;
            return false;
        }
    }
    class tour : Pieces
    {
        public tour(string color) : base(color) { }
        public override void Affichage()
        {
            var result = this.color.Equals("White") ? "WT " : "BT ";
            Console.Write(result);
        }
        public override bool Move_Permited_check(int[] conversion)
        {
            string swtcolor = color.Equals("White") ? "black" : "White";
            if (!(ChessBord.chess[conversion[2], conversion[3]].color.Equals(this.color)))
            {
                /////////////////////////// HORIZENTAL MOVE PERFECT CHECKED//////////////////////////////////////////////////////////////
                if (conversion[0] == conversion[2] && Math.Abs(conversion[1] - conversion[3]) != 0 || conversion[3] == conversion[1] && Math.Abs(conversion[0] - conversion[2]) != 0)
                {
                    if (conversion[3] < conversion[1])
                    {
                        for (int j = conversion[3] + 1; j < conversion[1]; j++)
                        {
                            if ((ChessBord.chess[conversion[2], j].color.Equals(this.color)) || (ChessBord.chess[conversion[2], j].color.Equals(swtcolor)))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int j = conversion[1] + 1; j < conversion[3]; j++)
                        {
                            if (ChessBord.chess[conversion[2], j].color.Equals(this.color) || (ChessBord.chess[conversion[2], j].color.Equals(swtcolor)))
                            {
                                return false;
                            }
                        }
                    }
                    //////////// VERTICAL MOVE PERFECT CHECKED/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (conversion[2] < conversion[0])
                    {
                        for (int j = conversion[2] + 1; j < conversion[0]; j++)
                        {
                            if (ChessBord.chess[j, conversion[3]].color.Equals(this.color) || ChessBord.chess[j, conversion[3]].color.Equals(swtcolor))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        for (int j = conversion[0] + 1; j < conversion[2]; j++)
                        {
                            if (ChessBord.chess[j, conversion[3]].color.Equals(this.color) || ChessBord.chess[j, conversion[3]].color.Equals(swtcolor))
                            {
                                return false;
                            }
                        }
                    }
                }
                else { return false; }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
    class Queen : Pieces
    {
        // Fou And tour objects becous if we combine them they will have the movemonts that the queen can have 
       private fou chekfou;
        private tour checktour;
        public Queen(string color) : base(color) { chekfou = new fou(color); checktour = new tour(color); }
        public override void Affichage()
        {
            var result = this.color.Equals("White") ? "WQ " : "BQ ";
            Console.Write(result);
        }
        public override bool Move_Permited_check(int[] conversion)
        {
            return chekfou.Move_Permited_check(conversion) || checktour.Move_Permited_check(conversion)  ;
        }
    }
    class Pion : Pieces
    {
        private bool check2move = false;
        private int promotion_du_Pion;
        public Pion(string color) : base(color) { }
        public override void Affichage()
        {
            var result = this.color.Equals("White") ? "WP " : "BP ";
            Console.Write(result);
        }
        public override bool Move_Permited_check(int[] conversion)
        {
            string swtcolor = color.Equals("White") ? "black" : "White";
            int i = 0;
            int j = 0;
            if (ChessBord.chess[conversion[0], conversion[1]].color.Equals("White"))

            {
                i = -1;
                j = -1;
            }
            else
            {
                if (ChessBord.chess[conversion[0], conversion[1]].color.Equals("black"))
                {
                    i = 1;
                    j = 1;
                }
            }
            int k = 0;
            if (conversion[2] == conversion[0] + i && conversion[3] == conversion[1])
            {
                if (ChessBord.chess[conversion[2], conversion[3]] is vide)
                {
                    check2move = true;
                    k++;
                }
            }
            else
            {
                if (conversion[2] == conversion[0] + i + j && !check2move && conversion[3] == conversion[1])
                {

                    if (ChessBord.chess[conversion[0] + i, conversion[1]] is vide)
                    {
                        if (ChessBord.chess[conversion[0] + i + j, conversion[1]] is vide)
                        {
                            check2move = true;
                            k++;
                        }
                    }
                }
            }
            if (conversion[2] == conversion[0] + i && (conversion[3] == conversion[1] + i || conversion[3] == conversion[1] - i))
            {
                if (ChessBord.chess[conversion[2], conversion[3]].color.Equals(swtcolor))
                {
                    k++;
                }
            }
            //// Le code suivant va gérer la promotion du pion 
            if (k == 1 && (conversion[2] == 0 || conversion[2]==7) )
            {
                Console.WriteLine("Promotion Du pion choisi un numbre entre 1-4 \n 1- Pour remplacer le pion par dame \n 2- Pour remplaver le pion par chevalier \n 3 - Pour Remplacer le pion par Fou \n 4 - Pour Remplacer le Pion par Tour  ");
                promotion_du_Pion = Int32.Parse(Console.ReadLine());
                while(promotion_du_Pion>4 || promotion_du_Pion < 1)
                {
                    Console.WriteLine("Entre 1 et 4 Svp");
                    promotion_du_Pion = Int32.Parse(Console.ReadLine());
                }
                switch (promotion_du_Pion)
                {
                    case 1:
                        ChessBord.chess[conversion[0], conversion[1]] = new Queen(this.color);
                        break;
                    case 2:
                        ChessBord.chess[conversion[0], conversion[1]] = new chvalier(this.color);
                        break;
                    case 3:
                        ChessBord.chess[conversion[0], conversion[1]] = new fou(this.color);
                        break;
                    case 4:
                        ChessBord.chess[conversion[0], conversion[1]] = new tour(this.color);
                        break;
                }
                return true;
            }
            else
            {
                if (k == 1)    return true;   return false;
            }
        }
    }
    class King : Pieces
    {
        private bool King_Moved_for_Roque = false;
        public King(string color) : base(color) { }
        public override void Affichage()
        {
            var result = this.color.Equals("White") ? "WK " : "BK ";
            Console.Write(result);
        }
        public override bool Move_Permited_check(int[] conversion)
        {
           string swtcolor = color.Equals("White") ? "black" : "White";
            bool check = false;
            int ch = 0;
            Pieces[,] clop = (Pieces[,])ChessBord.chess.Clone();
            ///////////// THIS LIST IS CREATED TO MOVE PLAYER2 DEPART TO WHERE THE KING IS GOING SO WE CAN PREVENT UNE MISE EN ECHEC AND TO USE THIS CODE IN MISE EN ECHEC SO WE DONT HAVE TO WRITE IT AGAINE   /////////////////////////////////////
            int[] King = new int[4];
            if (!(ChessBord.chess[conversion[2], conversion[3]].color.Equals(this.color)))
            {
               
                if ((conversion[1] == conversion[3] && Math.Abs(conversion[0] - conversion[2])==1) || (conversion[0]==conversion[2] && Math.Abs(conversion[1]-conversion[3])==1) )
                {
                    ch++;
                }
                if (Math.Abs(conversion[3] - conversion[1]) == 1 && Math.Abs(conversion[0] - conversion[2]) == 1)
                {
                    ch++;
                }
                //////------------------- PETIT ROQUE----------------------
                if(!King_Moved_for_Roque && conversion[1] - conversion[3] == -2)
                {
                    if(ChessBord.chess[conversion[0],7] is tour && ChessBord.chess[conversion[0], 7].color.Equals(color))
                    {
                        ChessBord.chess[conversion[0], 5] = new tour(color);
                        ChessBord.chess[conversion[0], 7] = new vide("uno");
                        return true;
                    }
                }
                //// -------------------GRANDE ROQUE--------------------
                if(!King_Moved_for_Roque && conversion[1] - conversion[3] == 2)
                {
                    if(ChessBord.chess[conversion[0],0] is tour && ChessBord.chess[conversion[0], 0].color.Equals(color))
                    {
                        ChessBord.chess[conversion[0], 3] = new tour(color);
                        ChessBord.chess[conversion[0], 0] = new vide("uno");
                        return true;
                    }
                }
                if (ch > 0)
                {
                    // IF KING CAN MOVE WE WILL MOVE IT IN CLONE
                    ChessBord.chess[conversion[0], conversion[1]].movve(conversion);
                    for (int a = 0; a < 8; a++)
                    {
                        for (int z = 0; z < 8; z++)
                        {
                            if ((clop[a, z].color.Equals(swtcolor)))
                            {
                                King[0] = a;
                                King[1] = z;
                                King[2] = conversion[2];
                                King[3] = conversion[3];
                                
                                ///////////////////// IF KING IS MOVED AND AAN ENEMY CAN EAT THAN RETURN MOVE NOT ALLOWED AND CLONE BACK THE ORIGINAL CHESSBORD
                                if (ChessBord.chess[King[0], King[1]].Move_Permited_check(King))
                                {
                                    ChessBord.chess = (Pieces[,])clop.Clone();
                                    return false;
                                }
                            }
                        }
                    }
                    ChessBord.chess = (Pieces[,])clop.Clone();
                    King_Moved_for_Roque = true;
                    return true;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }
    }
}