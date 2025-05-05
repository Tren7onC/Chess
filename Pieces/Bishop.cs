using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Bishop : Piece
    {

        public Bishop(bool team, int row, int col)
        {
            this.team = team;
            this.row = row;
            this.col = col;
            this.Pointvalue = 5;
            this.name = "Bishop";
        }
        //tmp.Add(new Tuple<int, int>(fRow, fCol));
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

            //Right Up
            for (int k = col + 1, i = row - 1; k <= 7 && i >= 0; k++,i--)
            {
                if (Chessboard.board[i, k] == 0)
                    tmp.Add(new Tuple<int, int>(i, k));
                else if (IsOppenent(Chessboard.board[i, k])) //17 > is player 2 piece 
                {
                    tmp.Add(new Tuple<int, int>(i, k));
                    break;
                }
                else if (IsFriend(Chessboard.board[i, k])) // 1 > is player 1
                    break;
            }

            //Left Up
            for (int k = col - 1, i = row - 1; k >= 0 && i >= 0; k--, i--)
            {
                if (Chessboard.board[i, k] == 0)
                    tmp.Add(new Tuple<int, int>(i, k));
                else if (IsOppenent(Chessboard.board[i, k])) //17 > is player 2 piece
                {
                    tmp.Add(new Tuple<int, int>(i, k));
                    break;
                }
                else if (IsFriend(Chessboard.board[i, k])) // 1 > is player 1
                    break;
            }

            //Left Down
            for (int k = col - 1, i = row + 1; k >= 0 && i <= 7; k--, i++)
            {
                if (Chessboard.board[i, k] == 0)
                    tmp.Add(new Tuple<int, int>(i, k));
                else if (IsOppenent(Chessboard.board[i, k])) //17 > is player 2 piece
                {
                    tmp.Add(new Tuple<int, int>(i, k));
                    break;
                }
                else if (IsFriend(Chessboard.board[i, k])) // 1 > is player 1
                    break;
            }
            //Right Down
            for (int k = col + 1, i = row + 1; k <= 7 && i <= 7; k++, i++)
            {
                if (Chessboard.board[i, k] == 0)
                    tmp.Add(new Tuple<int, int>(i, k));
                else if (IsOppenent(Chessboard.board[i, k])) //17 > is player 2 piece
                {
                    tmp.Add(new Tuple<int, int>(i, k));
                    break;
                }
                else if (IsFriend(Chessboard.board[i, k])) // 1 > is player 1
                    break;
            }
            return tmp;
        }
    }
}
