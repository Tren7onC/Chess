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
        public int points = 0;
        public int Time = 0;
        public List<string> PiecesTaken;

        public abstract void UpdatePiecesTaken(Piece[] Pieces);

        public abstract Player Copy();

        public int GetPoints()
        {
            return points;
        }

        public int GetTime()
        {
            return Time;
        }

    }

}
