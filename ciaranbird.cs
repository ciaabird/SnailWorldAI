using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GridWorld;

namespace ciaranbird
{
    public class ciaranbird : GridWorld.BasePlayer
    {
        private PlayerWorldState currentstate;
        int[] myPos;
        int[,] grid;

        public ciaranbird() : base()
        {
            this.Name = "(ಥ﹏ಥ)";
        }

        private List<Command.Direction> GetSquaresAdjacentToMySnail()
        {
            List<Command.Direction> retVal = new List<Command.Direction>();

            int x = myPos[0];
            int y = myPos[1];

            if (onBoard(x + 1, y) && isSquareEmpty(x + 1, y)) //Right
            {
                retVal.Add(Command.Direction.Right);
            }
            if (onBoard(x, y - 1) && isSquareEmpty(x, y - 1)) //Up
            {
                retVal.Add(Command.Direction.Down); 
            }
            if (onBoard(x - 1, y) && isSquareEmpty(x - 1, y)) //Left
            {
                retVal.Add(Command.Direction.Left);
            }
            if (onBoard(x, y + 1) && isSquareEmpty(x, y + 1)) //Down
            {
                retVal.Add(Command.Direction.Up);
            }
            
            return retVal;
        }

        private List<Command.Direction> StuckGetSquares() //Stuck - Check to look for my slime
        {
            List<Command.Direction> noMovesDir = new List<Command.Direction>();
            int x = myPos[0];
            int y = myPos[1];

            if (onBoard(x + 1, y)
                     && (currentstate[x + 1, y].Player == currentstate.ID)
                     && (currentstate[x + 1, y].Contents == GridSquare.ContentType.Trail))
            {
                noMovesDir.Add(Command.Direction.Right);
            }
            if (onBoard(x - 1, y)
                    && (currentstate[x - 1, y].Player == currentstate.ID)
                    && (currentstate[x - 1, y].Contents == GridSquare.ContentType.Trail))
            {
                noMovesDir.Add(Command.Direction.Left);
            }
            if (onBoard(x, y - 1)
                    && (currentstate[x, y - 1].Player == currentstate.ID)
                    && (currentstate[x, y - 1].Contents == GridSquare.ContentType.Trail))
            {
                noMovesDir.Add(Command.Direction.Down);
            }
            if (onBoard(x, y + 1)
                    && (currentstate[x, y + 1].Player == currentstate.ID)
                    && (currentstate[x, y + 1].Contents == GridSquare.ContentType.Trail))
            {
                noMovesDir.Add(Command.Direction.Up);
            }

            return noMovesDir;
        }

        private static Random random = new Random();

        public override ICommand GetTurnCommands(IPlayerWorldState igrid)
        {
            //Save our current state
            currentstate = (PlayerWorldState)igrid;

            //Save my current position
            myPos = GetMySnailPosition();

            //Build our grid
            //BuildGrid();
            Board board = new Board(currentstate);
            board.grid[0, 0];

            //Get a list of references to every square next to our snail
            List<Command.Direction> potentialMoves = GetSquaresAdjacentToMySnail();

            if (potentialMoves.Count > 0)
            {
                WriteTrace("Potential Moves: " + potentialMoves.Count);
                //Get a random square from the list
                int randomIndex = random.Next(0, potentialMoves.Count);
               
                return new Command(myPos[0],myPos[1],potentialMoves[randomIndex]);
            }
            else
            {
                List<Command.Direction> stuckMoves = StuckGetSquares(); //List of directions in which the snail can get back out through it's own slime
                int randomStuck = random.Next(0, stuckMoves.Count); //Picks a random number out of the options available.
           
                //There are no valid moves - return any old move
                return new Command(myPos[0], myPos[1], stuckMoves[randomStuck]);
            }
        }

        //public void initialiseNodes()
        //{
        //    TreeNode node1 = new TreeNode();
        //}

        /// <summary>
        /// Fill our local grid with zeroes and a one for our position
        /// </summary>
        //private void BuildGrid()
        //{
        //    grid = new int[currentstate.GridWidthInSquares, currentstate.GridHeightInSquares];
        //    String concatedInfo = "";
        //    for (int y = 0; y < currentstate.GridHeightInSquares; y++)
        //    {
        //        for (int x = 0; x < currentstate.GridWidthInSquares; x++)
        //        {
        //            if(currentstate[x, y].Contents == GridSquare.ContentType.Empty)
        //            {
        //                grid[x, y] = 0;
        //                concatedInfo += " 0,";
        //            }
        //            else if(currentstate[x,y].Contents == GridSquare.ContentType.Snail)
        //            {
        //                grid[x, y] = 2;
        //                concatedInfo += " 2,";
        //            }
        //            else if((currentstate[x,y].Contents == GridSquare.ContentType.Trail) && (currentstate[x, y].Player == currentstate.ID))
        //            {
        //                grid[x, y] = 1;
        //                concatedInfo += " 1,";
        //            }
        //            else if((currentstate[x, y].Contents == GridSquare.ContentType.Trail) && (currentstate[x, y].Player != currentstate.ID))
        //            {
        //                grid[x, y] = 3;
        //                concatedInfo += " 3,";
        //            }
        //        }
        //        WriteTrace(concatedInfo);
        //        concatedInfo = "";
        //    }
        //}

        public int getSurroundingSlimeScore(int x, int y)
        {
            return 0;
        }

        public bool isSquareEmpty(int x, int y)
        {
            return currentstate[x, y].Contents == GridSquare.ContentType.Empty;
        }

        public bool onBoard(int x, int y)
        {
            if (x >= currentstate.GridWidthInSquares || x < 0 || y >= currentstate.GridHeightInSquares || y < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int[] GetMySnailPosition()
        {
            for (int x = 0; x < currentstate.GridWidthInSquares; x++)
            {
                for (int y = 0; y < currentstate.GridHeightInSquares; y++)
                {
                    if ((currentstate[x, y].Contents == GridSquare.ContentType.Snail) && (currentstate[x, y].Player == currentstate.ID))
                    {
                        return new int[2] { x, y };
                    }
                }
            }

            return new int[2] { -1, -1 };
        }
    }
}
