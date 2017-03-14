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
        int moves = 0;
        public static Board prevBoard;
        int startup;

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
                moves++;
                retVal.Add(Command.Direction.Right);
            }
            if (onBoard(x, y - 1) && isSquareEmpty(x, y - 1)) //Up
            {
                moves++;
                retVal.Add(Command.Direction.Down); 
            }
            if (onBoard(x - 1, y) && isSquareEmpty(x - 1, y)) //Left
            {
                moves++;
                retVal.Add(Command.Direction.Left);
            }
            if (onBoard(x, y + 1) && isSquareEmpty(x, y + 1)) //Down
            {
                moves++;
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
            if(startup != 0)
            {
                prevBoard = board;
                TreeNode node = new TreeNode(prevBoard);
                node.setParent(false);
                node.setChildren(true);

                startup++;
            }

            Board board = new Board(currentstate); //Creates a board you
            initialiseNodes(board, myPos[0], myPos[1]);

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
        /// <summary>
        /// Initialises a node in whilst and looks for the possible movements and edits a grid to all the movements and changes the grid responsibly.
        /// </summary>
        public void initialiseNodes(Board board, int x, int y)
        {
            //If the snail has moved then create a Parent Node
            if(board.grid[x + 1, y] == 0) //If the square is empty or has my trail, add it to children
            {
                TreeNode node = new TreeNode(board);
                node.setParent(true);
                board.grid[x, y] = 1; //Set the previous square to slime
                board.grid[x + 1, y] = 2; //Set the square moving to as a snail.
            }
            if (board.grid[x - 1, y] == 0) //If the square is empty or has my trail, add it to children
            {
                TreeNode node = new TreeNode(board);
                node.setParent(true);
                board.grid[x, y] = 1; //Set the previous square to slime
                board.grid[x - 1, y] = 2; //Set the square moving to as a snail.
            }
            if (board.grid[x, y + 1] == 0) //If the square is empty or has my trail, add it to children
            {
                TreeNode node = new TreeNode(board);
                node.setParent(true);
                board.grid[x, y] = 1; //Set the previous square to slime
                board.grid[x, y + 1] = 2; //Set the square moving to as a snail.
            }
            if (board.grid[x, y - 1] == 0) //If the square is empty or has my trail, add it to children
            {
                TreeNode node = new TreeNode(board);
                node.setParent(true);
                board.grid[x, y] = 1; //Set the previous square to slime
                board.grid[x, y - 1] = 2; //Set the square moving to as a snail.
            }
            //Alter the board grid for that node so that it's different

            //To put alter the new table depending on whether the square next to the snail
            //is a slime and replaces squares touched with 1's until it finds the last trail and puts a snail there
            int steps1 = currentstate.GridWidthInSquares - x; //To know how far away from the border you are.
            int steps2 = currentstate.GridHeighInSquares - y;
            if (board[x + 1, y] == 1) //if the square to the right is slime
            {
                for (int i = 1; i < steps1; i++)
                {
                    if (board[x + i, y] == 1) //if it's not equal to 1
                    {
                        board[x + i, y] = 1; //set that to my snail pos
                    }
                    if (board[x + i, y] == 2 || board[x + i, y] == 0)
                    {
                        board[x + i - 1, y] = 2;
                        break;
                    }
                }
            }
            if (board[x - 1, y] == 1) //if the square to the left is slime
            {
                for (int i = 1; i < x; i++)
                {
                    if (board[x - i, y] == 1) //if it's not equal to 1
                    {
                        board[x - i, y] = 1; //set that to my snail pos
                    }
                    if(board[x - i, y] == 2 || board[x - i, y] == 0)
                    {
                        board[x - i + 1, y] = 2;
                        break;
                    }
                }
            }
            if (board[x, y + 1] == 1) //if the square to the top is slime
            {
                for (int i = 1; i < steps2; i++)
                {
                    if (board[x, y + i] == 1) //if it's not equal to 1
                    {
                        board[x, y + i] = 1; //set that to my snail pos
                    }
                    if (board[x, y + i] == 2 || board[x, y + i] == 0)
                    {
                        board[x, y + i] = 1;
                    }
                }
            }
            if (board[x, y - 1] == 1) //if the square to the bottom is slime
            {
                for (int i = 1; i < y; i++)
                {
                    if (board[x , y - i] == 1) //if it's not equal to 1
                    {
                        board[x, y - i] = 2; //set that to my snail pos
                    }
                    if (board[x, y - i] == 2 || board[x, y - i] == 0)
                    {
                        board[x, y - i + 1] = 1;
                    }
                }
            }
            //To put alter the new table depending on whether the square next to the snail
            //is a slime and replaces squares touched with 1's until it finds the last trail and puts a snail there
        }

        public void setNode(TreeNode node, bool parent, bool child)
        {
            node.setParent(parent);
            node.setChildren(child);
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
