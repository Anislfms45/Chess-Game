using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp104
{
    abstract class Conversion
    {
        public int[] conversion = new int[4];
        protected string PlayerMoveAdd;
        // Function Joue_Permis va verifier si la chaine entrer est constitue de 2 letrre et de chiffre et de taile eqal a 4
        public bool Joue_Permis(string move)
        {
            Regex regex = new Regex("^[a-h]+[1-8]+[a-h]+[1-8]");
            if (regex.Match(move).Success)
            {
                return true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Check Inputs a-h 1-8");
                return false;
            }
        }
        // Conversion va convertire E2E4 TO 6444
        public int[] Conversio(string move)
        {
            PlayerMoveAdd = move;
            if (move[1].Equals('1')) conversion[0] = 7;
            if (move[1].Equals('2')) conversion[0] = 6;
            if (move[1].Equals('3')) conversion[0] = 5;
            if (move[1].Equals('4')) conversion[0] = 4;
            if (move[1].Equals('5')) conversion[0] = 3;
            if (move[1].Equals('6')) conversion[0] = 2;
            if (move[1].Equals('7')) conversion[0] = 1;
            if (move[1].Equals('8')) conversion[0] = 0;
            if (move[3].Equals('1')) conversion[2] = 7;
            if (move[3].Equals('2')) conversion[2] = 6;
            if (move[3].Equals('3')) conversion[2] = 5;
            if (move[3].Equals('4')) conversion[2] = 4;
            if (move[3].Equals('5')) conversion[2] = 3;
            if (move[3].Equals('6')) conversion[2] = 2;
            if (move[3].Equals('7')) conversion[2] = 1;
            if (move[3].Equals('8')) conversion[2] = 0;

            conversion[1] = move[0] - 97;
            conversion[3] = move[2] - 97;
            return conversion;
        }
    }
}
