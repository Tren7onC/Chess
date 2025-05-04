using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Piece
    {
        public int row;
        public int col;
        public int Pointvalue;
        public bool hasBeenTaken = false;
        public bool team;               //True for Player 1/White. False for Player 2/Black
        public bool hasMoved = false;
        

        public virtual List<Tuple<int, int>> GetPossibleMoves()
        {
            List<Tuple<int, int>> tmp = new List<Tuple<int, int>>();
            return tmp;
        }
      

        public Piece[] Move(bool Valid,Board ChessBoard, Piece[] Pieces, Tuple<int, int> SelectedSpot, int PieceMoving)
        {
            if(Valid)
            {
                if (ChessBoard.board[SelectedSpot.Item1, SelectedSpot.Item2] != 0) //Sees if where moving is a piece
                {
                    Pieces[ChessBoard.board[SelectedSpot.Item1, SelectedSpot.Item2]].hasBeenTaken = true; //If it is changes that piece to taken
                }
                //Set the piece location to new location
                Pieces[PieceMoving].row = SelectedSpot.Item1;
                Pieces[PieceMoving].col = SelectedSpot.Item2;
                Pieces[PieceMoving].hasBeenTaken = true;
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
                    if (ChessBoard.board[Selected.Item1, Selected.Item2] >= 17 || ChessBoard.board[Selected.Item1, Selected.Item2] == 0) //To see if its on your team
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
