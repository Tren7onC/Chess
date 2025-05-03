using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal abstract class Piece
    {
        public int row;
        public int col;
        public int Pointvalue;
        public bool hasBeenTaken = false;
        public bool team;               //True for Player 1/White. False for Player 2/Black
        public bool hasMoved = false;
        

        public abstract List<Tuple<int, int>> GetPossibleMoves();
      

        public Piece[] Move(bool Valid,Board ChessBoard, Piece[] Pieces, Tuple<int, int> Selected)
        {
            if(Valid)
            {
                if (ChessBoard.board[Selected.Item1, Selected.Item2] != 0)
                {
                    Pieces[ChessBoard.board[Selected.Item1, Selected.Item2]].hasBeenTaken = true;
                }
            }
            return Pieces;
        }

        public bool IsValidPostion(bool team, Tuple<int,int> Selected, List<Tuple<int, int>> possibleMoves, Board ChessBoard)
        {
            if (team)       //Team 1
            {
                if(possibleMoves.Contains(Selected)) //Places in the bounds of board
                {
                    if (ChessBoard.board[Selected.Item1, Selected.Item2] <= 16) //To see if its on your team
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
            else if (!team) //Team 2
            {
                if (possibleMoves.Contains(Selected)) //Places in the bounds of board
                {
                    if (ChessBoard.board[Selected.Item1, Selected.Item2] >= 17) //To see if its on your team
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
                return false;
        }
    }
}
