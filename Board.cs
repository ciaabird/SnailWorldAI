using GridWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ciaranbird
{
    public class Board
    {
        public int[,] grid;
        public Board(PlayerWorldState state)
        {
            BuildGrid(state);
        }

        private void BuildGrid(PlayerWorldState currentstate)
        {
            grid = new int[currentstate.GridWidthInSquares, currentstate.GridHeightInSquares];
            for (int y = 0; y < currentstate.GridHeightInSquares; y++)
            {
                for (int x = 0; x < currentstate.GridWidthInSquares; x++)
                {
                    if (currentstate[x, y].Contents == GridSquare.ContentType.Empty)
                    {
                        grid[x, y] = 0;
                    }
                    else if (currentstate[x, y].Contents == GridSquare.ContentType.Snail)
                    {
                        grid[x, y] = 2;
                    }
                    else if ((currentstate[x, y].Contents == GridSquare.ContentType.Trail) && (currentstate[x, y].Player == currentstate.ID))
                    {
                        grid[x, y] = 1;
                    }
                    else if ((currentstate[x, y].Contents == GridSquare.ContentType.Trail) && (currentstate[x, y].Player != currentstate.ID))
                    {
                        grid[x, y] = 3;
                    }
                }
            }
        }
    }
}
