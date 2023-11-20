

using Dungeon;
using System.Diagnostics;
using System.Runtime.InteropServices;


PrepeareTerminal(100, 50, "Dungon");

GameEngine engine = new GameEngine(typeof(SplashScreen));
engine.Run();



static void PrepeareTerminal(int columns, int rows, string title)
{
    Console.InputEncoding = System.Text.Encoding.UTF8;
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.Title = title;

    /*
        var psi = new ProcessStartInfo
        {
            FileName = "/bin/zsh",
            Arguments = $"-c \"resize -s {rows} {columns}\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };
        Process.Start(psi);
        */

}