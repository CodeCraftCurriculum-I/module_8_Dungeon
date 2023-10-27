


using Utils;

namespace Dungeon
{


    public class GAME_SIZE
    {
        const int MIN_ROWS = 10;
        const int MIN_COLUMNS = 10;
        public int rows = 0;
        public int columns = 0;

        public static GAME_SIZE SMALL { get { return new GAME_SIZE() { rows = MIN_ROWS, columns = MIN_COLUMNS }; } }
        public static GAME_SIZE LARGE { get { throw new NotImplementedException("You must implement Large, it should use as close to  3/4 of the screen"); } }
        public static GAME_SIZE XTRA_LARGE { get { throw new NotImplementedException("You must implement Large,it should use as close to 100% of the screen as we can get"); } }
    }


    public class GreedyGame : GameEngine.IScene
    {
        const int EMPTY = 0;
        const int MIN_NUMBER = 1;
        const int MAX_NUMBER = 9;
        const int PLAYER_ID = -1;
        const char PLAYER_TOKEN = '\u263A';

        const int HUD_HEIGHT = 3;

        GAME_SIZE gameSize;
        int[,] gameBoard;
        (int row, int column) player;
        bool playerMoved = false;
        bool dirty = true;
        int delta_y;
        int moveCount = 0;

        int score = 0;

        public GreedyGame(GAME_SIZE size)
        {
            gameSize = size;
        }

        #region Greedy Game Functions ----------------------------------------------------------------------------------

        int[,] CreateGameBoard(GAME_SIZE size)
        {
            int[,] gameBoard = new int[size.rows, size.columns];
            return gameBoard;
        }

        int[,] FillGameBoard(int[,] gameBoard, int minValue, int maxValue)
        {

            int rowCount = gameBoard.GetLength(0);
            int colCount = gameBoard.GetLength(1);
            Random rnd = new Random((int)DateTime.Now.Ticks);

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    gameBoard[row, col] = rnd.Next(minValue, maxValue);
                }
            }

            return gameBoard;

        }

        (int row, int column) PickRandomStartPosition(int[,] gameBoard)
        {
            int rows = gameBoard.GetLength(0);
            int col = gameBoard.GetLength(1);
            Random rnd = new Random((int)DateTime.Now.Ticks);
            return (row: rnd.Next(0, rows + 1), column: rnd.Next(0, col + 1));
        }

        #endregion -----------------------------------------------------------------------------------------------------

        #region GameEngine.IScene --------------------------------------------------------------------------------------

        public Action<Type, object[]> OnExitScreen { get; set; }

        public void init()
        {
            gameBoard = CreateGameBoard(gameSize);
            gameBoard = FillGameBoard(gameBoard, MIN_NUMBER, MAX_NUMBER);
            player = PickRandomStartPosition(gameBoard);
            gameBoard[player.Item1, player.Item2] = PLAYER_ID;

            dirty = true;
        }

        public void input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyCode = Console.ReadKey(true).Key;
                if (keyCode == ConsoleKey.DownArrow || keyCode == ConsoleKey.W)
                {
                    delta_y = 1;

                }
                else if (keyCode == ConsoleKey.UpArrow || keyCode == ConsoleKey.S)
                {
                    delta_y = -1;
                }

                playerMoved = delta_y != 0;
            }
        }

        public void update()
        {
            if (playerMoved)
            {
                playerMoved = false;
                gameBoard[player.row, player.column] = EMPTY;
                player.row += delta_y;
                moveCount = gameBoard[player.row, player.column];
                gameBoard[player.row, player.column] = PLAYER_ID;
                dirty = true;
            }
            else if (moveCount > 0)
            {
                gameBoard[player.row, player.column] = EMPTY;
                player.row += delta_y;
                gameBoard[player.row, player.column] = PLAYER_ID;
                score++;
                moveCount--;
                dirty = true;
            }
        }

        public void draw()
        {
            ///TODO: Refactor this function
            if (dirty)
            {
                Console.Clear();
                dirty = false;

                int rowCount = gameBoard.GetLength(0);
                int colCount = gameBoard.GetLength(1);

                string output = "";

                int x = (int)((Console.WindowWidth - gameSize.columns) * 0.5);
                int y = (int)((Console.WindowHeight - gameSize.rows) * 0.5) + HUD_HEIGHT;

                Console.Write($"{ANSICodes.Positioning.SetCursorPos((y - HUD_HEIGHT), x)}Score : {score}");

                for (int row = 0; row < rowCount; row++)
                {
                    output = $"{output}{ANSICodes.Positioning.SetCursorPos(y + row, x)}";
                    for (int col = 0; col < colCount; col++)
                    {
                        if (gameBoard[row, col] != PLAYER_ID && gameBoard[row, col] != EMPTY)
                        {
                            output = $"{output} \u001b[38;5;{gameBoard[row, col]}m{gameBoard[row, col]}{ANSICodes.Reset}";
                        }
                        else if (gameBoard[row, col] == PLAYER_ID)
                        {
                            output = $"{output} {ANSICodes.BgColors.Blue}{PLAYER_TOKEN}{ANSICodes.Reset}";
                        }
                        else
                        {
                            output = $"{output}  ";
                        }
                    }

                }

                Console.WriteLine(output);

            }

        }

        #endregion ---------------------------------------------------------------------------------------------------------



    }
}