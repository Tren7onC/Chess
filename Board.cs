using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{

    using Chess.Pieces;
    internal class Board
    {
        public int[,] board = new int[8, 8];
        
        /*
        Piece numbers for reference:
   
        1 -  8  = PLayer 1 pawns
        9 - 10  = Player 1 Rook
        11 - 12 = Player 1 Horses
        13 - 14 = Player 1 Bishops
        15      = Player 1 Queen
        16      = Player 1 King

        17 - 24 = Player 2 pawns
        25 - 26 = Player 2 Rook
        27 - 28 = Player 2 Horses
        29 - 30 = Player 2 Bishops
        31      = Player 2 Queen
        32      = Player 1 King
        */
        public Board()
        {
            //Set up for intial Board
                //Row 7 for Player 1/ White Team
                board[7, 0] = 9; board[7, 1] = 11; board[7, 2] = 13; board[7, 3] = 15; board[7, 4] = 16; board[7, 5] = 14; board[7, 6] = 12; board[7, 7] = 10;
                //Row 6 for Player 1/ White Team
                board[6, 0] = 1; board[6, 1] = 2; board[6, 2] = 3; board[6, 3] = 4; board[6, 4] = 5; board[6, 5] = 6; board[6, 6] = 7; board[6, 7] = 8;

                //Row 0 for Player 1/ Black Team
                board[0, 0] = 25; board[0, 1] = 27; board[0, 2] = 29; board[0, 3] = 31; board[0, 4] = 32; board[0, 5] = 30; board[0, 6] = 28; board[0, 7] = 26;
                //Row 1 for Player 1/ Black Team
                board[1, 0] = 17; board[1, 1] = 18; board[1, 2] = 19; board[1, 3] = 20; board[1, 4] = 21; board[1, 5] = 22; board[1, 6] = 23; board[1, 7] = 24;
        
        }

        public Board UpdateBoard(Piece[] Pieces )
        {
            Board tmp = new Board();
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    tmp.board[row, col] = 0;

            for(int k = 1; k < Pieces.Length; k++)
                if (!Pieces[k].hasBeenTaken)
                    tmp.board[Pieces[k].row, Pieces[k].col] = k;


            return tmp;

        }


    }

}
