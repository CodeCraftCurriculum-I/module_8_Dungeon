using Utils;
using static Utils.HelperFunctions;

namespace Dungeon
{

    public class SplashScreen : GameEngine.IScene
    {
        const int MAX_RUNTIME = 100;
        const int TICKS_PER_FRAME = 4;
        const string art = """
▓█████▄  █    ██  ███▄    █   ▄████ ▓█████  ▒█████   ███▄    █ 
▒██▀ ██▌ ██  ▓██▒ ██ ▀█   █  ██▒ ▀█▒▓█   ▀ ▒██▒  ██▒ ██ ▀█   █ 
░██   █▌▓██  ▒██░▓██  ▀█ ██▒▒██░▄▄▄░▒███   ▒██░  ██▒▓██  ▀█ ██▒
░▓█▄   ▌▓▓█  ░██░▓██▒  ▐▌██▒░▓█  ██▓▒▓█  ▄ ▒██   ██░▓██▒  ▐▌██▒
░▒████▓ ▒▒█████▓ ▒██░   ▓██░░▒▓███▀▒░▒████▒░ ████▓▒░▒██░   ▓██░
 ▒▒▓  ▒ ░▒▓▒ ▒ ▒ ░ ▒░   ▒ ▒  ░▒   ▒ ░░ ▒░ ░░ ▒░▒░▒░ ░ ▒░   ▒ ▒ 
 ░ ▒  ▒ ░░▒░ ░ ░ ░ ░░   ░ ▒░  ░   ░  ░ ░  ░  ░ ▒ ▒░ ░ ░░   ░ ▒░
 ░ ░  ░  ░░░ ░ ░    ░   ░ ░ ░ ░   ░    ░   ░ ░ ░ ▒     ░   ░ ░ 
   ░       ░              ░       ░    ░  ░    ░ ░           ░ 
 ░                                                             
""";
        System.Collections.Hashtable colorPalet = new() {
            {9608 , "\u001b[38;5;232m" },
            {9619,  "\u001b[38;5;88m" },
            {9618,  "\u001b[38;5;52m" },
            {9617,  "\u001b[38;5;124m" },
            {9616,  "\u001b[38;5;88m" },
            {9612, "\u001b[38;5;160m"},
            {9604, "\u001b[38;5;88m"},
            {9600,"\u001b[38;5;124m"},
            {32, ANSICodes.Colors.Black},

        };
        int tickCount = 0;
        int totalTuntime = 0;
        bool dirty = true;
        char[][] artArray;
        int startY = 0;
        int startX = 0;
        string outputGraphics = "";
        public Action<Type, object[]> OnExitScreen { get; set; }

        int mainColor = 232;


        int[][] values;
        public void init()
        {
            Console.Clear();
            artArray = Create2DArrayFromMultiLineString(art);
            values = new int[artArray.Length][];

            for (int row = 0; row < artArray.Length; row++)
            {
                values[row] = new int[artArray[row].Length];
                for (int col = 0; col < artArray[row].Length; col++)
                {
                    values[row][col] = (int)artArray[row][col];
                }
            }



            //colorMatrix = CreateColorMapFrom(artArray, COLOR_DELTA, MIN_COLOR, MAX_COLOR);

            /*startY = (int)((Console.WindowHeight - colorMatrix.Length) * 0.25);
            startX = (int)((Console.WindowWidth - colorMatrix[0].Length) * 0.5);
*/
            tickCount = TICKS_PER_FRAME;
        }
        public void input()
        {
        }

        int colorDelta = 1;

        public void update()
        {
            tickCount++;
            if (tickCount >= TICKS_PER_FRAME)
            {
                dirty = true;
                outputGraphics = "";
                colorPalet[9608] = $"\u001b[38;5;{mainColor}m";
                mainColor += colorDelta;
                if (mainColor > 255)
                {
                    mainColor = 255;
                    colorDelta = -1;
                }
                else if (mainColor < 232)
                {
                    mainColor = 232;
                    colorDelta = 1;
                }

                for (int row = 0; row < values.Length; row++)
                {
                    for (int col = 0; col < artArray[row].Length; col++)
                    {

                        if (colorPalet.ContainsKey((int)artArray[row][col]))
                        {
                            outputGraphics += $"{colorPalet[(int)artArray[row][col]]}{artArray[row][col]}{ANSICodes.Reset}";
                        }

                    }
                    outputGraphics += "\n";
                }
            }

        }



        public void draw()
        {
            if (dirty)
            {
                dirty = false;
                Console.Clear();
                Console.Write(outputGraphics);



            }
        }



    }

}