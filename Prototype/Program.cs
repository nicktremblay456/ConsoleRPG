using System.Runtime.InteropServices;
using Prototype;

public class Program
{
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();
    private static IntPtr ThisConsole = GetConsoleWindow();
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    //private const int HIDE = 0;
    private const int MAXIMIZE = 3;
    //private const int MINIMIZE = 6;
    //private const int RESTORE = 9;

    private static void Main(string[] args)
    {
        Console.Title = "World of Console";

        // Set Console Window
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Only work for windows platform.
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
        }
        // Start Game
        Game game = new Game();
        game.ShowMainMenu();
    }
}