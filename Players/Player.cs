using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    using Chess.Pieces;
    internal abstract class Player
    {
        public int points;
        //public List<string> PiecesTaken = new List<string>();


        public abstract void UpdatePiecesTaken(Piece[] Pieces);

        public abstract bool IsInCheck(Piece[] Pieces, Board board);

        public abstract Player Copy();

    }

}
