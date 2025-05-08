using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal abstract class Piece
    {
        public int row;
        public int col;
        public int Pointvalue { get; protected set; }
        public bool hasBeenTaken = false;
        public bool team { get; protected set; }               //True for Player 1/White. False for Player 2/Black
        public bool hasMoved = false;
        public string name;


        public abstract List<Tuple<int, int>> GetPossibleMoves(Board Chessboard);

        public abstract Piece Copy();

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
            }
            return Pieces;
        }

        public bool IsValidPostion( Tuple<int,int> Selected, List<Tuple<int, int>> possibleMoves)
        {
            return possibleMoves.Contains(Selected);
        }


    }
}
