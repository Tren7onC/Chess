using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class King : Piece
    {
        public King(bool team, int row, int col)
        {
            this.team = team;
            this.row = row;
            this.col = col;
            this.Pointvalue = 20;
            this.name = "King";
        }
        private bool IsOppenent(int val)
        {
            return team ? val >= 17 : val >= 1 && val <= 16; //So this is like super condenced if statements
            //Takes in team and compares val based on team. So if true //Player 1 // checks to see if val >=17
            //This means based on the team it sees if the value is on the oppenents team
        }
        private bool IsFriend(int val)
        {
            return team ? val >= 1 && val <= 16 : val >= 17; //This checks if, based on team, if friend or not 

        }
        public override List<Tuple<int, int>> GetPossibleMoves(Board Chessboard)
        {
            List<Tuple<int, int>> tmp = new List<Tuple<int, int>>();

            int[] rowMoves = { -1, -1, -1, 1, 1, 1, 0, 0 };
            int[] colMoves = { -1,  0,  1,-1, 0, 1, 1, -1 };

            for (int k = 0; k < 8; k++)
            {
                int fRow = row + rowMoves[k];
                int fCol = col + colMoves[k];

                if (fRow >= 0 && fRow <= 7 && fCol >= 0 && fCol <= 7 && !IsFriend(Chessboard.board[fRow, fCol]))
                    tmp.Add(new Tuple<int, int>(fRow, fCol));
            }

            return tmp;
        }
        public override Piece Copy()
        {
            King copy = new King(this.team, this.row, this.col);
            copy.Pointvalue = this.Pointvalue;
            copy.hasBeenTaken = this.hasBeenTaken;
            copy.hasMoved = this.hasMoved;
            copy.name = this.name;
            return copy;
        }
    }
}
