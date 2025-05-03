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
      

        public bool Move()
        {
            return true;
        }

        public bool IsValidPostion(bool team, Tuple<int,int> Selected, List<Tuple<int, int>> possibleMoves, Board board)
        {
            if (team)       //Team 1
            {
                if(possibleMoves.Contains(Selected))
                {

                }
            }
            else if (!team) //Team 2
            {

            }
                return false;
        }
    }
}
