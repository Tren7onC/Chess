using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Game
    {
        public bool whoTurn = true; //Player 1 = true. Player 2 = false
        //Stuff for current selected Piece
        public Tuple<int, int> SelectedPiece;
        public bool PieceSelected;

    }
}
