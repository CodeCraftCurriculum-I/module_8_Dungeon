using Utils;
using System.Collections;
using static Utils.HelperFunctions;

namespace Dungeon
{

    public class SplashScreen : GameEngine.IScene
    {
        const int MAX_RUNTIME = 100;
        const int TICKS_PER_FRAME = 3;
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
        Hashtable colorPalet = new() {
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
        int totalTickCount = 0;
        bool dirty = true;
        bool exit = false;
        char[][] artArray;
        int startY = 0;
        int startX = 0;
        int padding = 3;
        string outputGraphics = "";
        Func<int> mainColorOcilator = Oscillate(232, 255);

        public Action<Type, object[]> OnExitScreen { get; set; }

        public void init()
        {
            Console.Clear();
            artArray = Create2DArrayFromMultiLineString(art);
            startY = (int)((Console.WindowHeight - artArray.Length) * 0.25);
            startX = (int)((Console.WindowWidth - artArray[0].Length) * 0.5);
            tickCount = TICKS_PER_FRAME;
        }

        public void input()
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey();
                exit = true;
            }
        }

        public void update()
        {
            tickCount++;
            totalTickCount++;

            if (tickCount >= TICKS_PER_FRAME)
            {
                dirty = true;
                outputGraphics = "";
                colorPalet[9608] = $"\u001b[38;5;{mainColorOcilator()}m";

                for (int row = 0; row < artArray.Length; row++)
                {
                    outputGraphics += $"{ANSICodes.Positioning.SetCursorPos(startY + row, startX)}";
                    for (int col = 0; col < artArray[row].Length; col++)
                    {
                        if (colorPalet.ContainsKey((int)artArray[row][col]))
                        {
                            outputGraphics += $"{colorPalet[(int)artArray[row][col]]}{artArray[row][col]}{ANSICodes.Reset}";
                        }
                    }
                }

                if (totalTickCount > MAX_RUNTIME)
                {
                    outputGraphics += $"{ANSICodes.Positioning.SetCursorPos(startY + artArray.Length + padding, startX)}";
                }
            }

            if (exit)
            {
                OnExitScreen(typeof(CharacterCreationScreen), null);
            }
        }

        public void draw()
        {
            if (dirty)
            {
                dirty = false;
                Console.Clear();
                Console.WriteLine(outputGraphics);
                Console.WriteLine(Output.Align("Press Any Key To Start", Alignment.CENTER));

            }
        }

    }

}