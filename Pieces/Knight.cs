using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Knight : Piece
    {

        public Knight(bool team, int row, int col) 
        {
            this.team = team;
            this.row = row;
            this.col = col;
            this.Pointvalue = 3;
        }
        //possibleMoves.Add(new Tuple<int, int>(Row - i, Col);
        public override List<Tuple<int, int>> GetPossibleMoves()
        {
            List<Tuple<int, int>> tmp = new List<Tuple<int, int>>();

            int[] rowMoves = {2,2,-2,-2,1,1,-1,-1};
            int[] colMoves = {1,-1,1,-1,2,-2,2,-2};

            for(int k = 0; k < 8; k++)
            {
                int fRow = row + rowMoves[k];
                int fCol = col + colMoves[k];

                if(fRow >= 0 && fRow <= 7 && fCol >= 0 && fCol<= 7)
                    tmp.Add(new Tuple<int, int>(fRow,fCol));
            }

            return tmp;
        }
    }
}
