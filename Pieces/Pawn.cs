using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Pawn : Piece
    {

        public Pawn(bool team, int row, int col)
        {
            this.team = team;
            this.row = row;
            this.col = col;
            this.Pointvalue = 1;
            this.name = "Pawn";

        }
        public override List<Tuple<int, int>> GetPossibleMoves(Board Chessboard)
        {
            List<Tuple<int, int>> tmp = new List<Tuple<int, int>>();

            if(team) // Player 1
            {
                if (row > 0 && Chessboard.board[row - 1, col] == 0)
                    tmp.Add(new Tuple<int, int>(row - 1, col));

                if (row == 6 && Chessboard.board[row - 1, col] == 0 && Chessboard.board[row - 2, col] == 0)
                    tmp.Add(new Tuple<int, int>(row - 2, col));
                if (row != 0)
                {
                    if (col > 0 && Chessboard.board[row - 1, col - 1] >= 17)
                        tmp.Add(new Tuple<int, int>(row - 1, col - 1));

                    if (col < 7 && Chessboard.board[row - 1, col + 1] >= 17)
                        tmp.Add(new Tuple<int, int>(row - 1, col + 1));
                }
            }
            else if(!team)
            {
                if (row > 0 && Chessboard.board[row + 1, col] == 0)
                    tmp.Add(new Tuple<int, int>(row + 1, col));

                if (row == 1 && Chessboard.board[row + 1, col] == 0 && Chessboard.board[row + 2, col] == 0)
                    tmp.Add(new Tuple<int, int>(row + 2, col));
                if (row != 7)
                {
                    if (col > 0 && Chessboard.board[row + 1, col - 1] <= 17 && Chessboard.board[row + 1, col - 1] >= 1)
                        tmp.Add(new Tuple<int, int>(row + 1, col - 1));

                    if (col < 7 && Chessboard.board[row + 1, col - 1] <= 17 && Chessboard.board[row + 1, col - 1] >= 1)
                        tmp.Add(new Tuple<int, int>(row + 1, col + 1));
                }
            }

            return tmp;
        }
        public override Piece Copy()
        {
            Pawn copy = new Pawn(this.team, this.row, this.col);
            copy.Pointvalue = this.Pointvalue;
            copy.hasBeenTaken = this.hasBeenTaken;
            copy.hasMoved = this.hasMoved;
            copy.name = this.name;
            return copy;
        }
    }
}
