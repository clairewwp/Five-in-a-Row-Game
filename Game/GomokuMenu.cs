using System;
using static System.Console;
using System.IO;
namespace Game
{
    class GomokuMenu : MainMenu
    {
        public GomokuMenu(string name, string[] gMenu) : base(name, gMenu)
        {

        }
        public void Modes(int selected)
        {

            switch (selected)
            {
                case 0:
                    RunPVP();
                    break;
                case 1:
                    RunPVE();
                    break;
                case 2:
                    Resume();
                    break;
                case 3:
                    About();
                    break;
                case 4://for returning back to the main menu
                    Game g = new Game();
                    g.StartMenu();
                    break;
            }
        }
        public void RunPVP()
        {
            Clear();
            Game g = new Game();
            g.Play(false);
        }

        public void RunPVE() 
        {
            Clear();
            Game g = new Game();
            g.Play(true);
        }
        public void Resume() { }
        public void About()
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            const string FileName = "AboutGomoku.txt";
            string text = File.ReadAllText(FileName);
            WriteLine(text);
            bool valid = true;
            WriteLine("\nPlease press 'Enter' to return previous menu.", ForegroundColor = ConsoleColor.White);
            while (valid != false)
            {
                ConsoleKeyInfo k = ReadKey(true);
                ConsoleKey keyPressed = k.Key;
                if (keyPressed == ConsoleKey.Enter)
                {
                    string name = $"{"Gomoku",15}";
                    string[] m = { "Play PVP mode", "Play PVE mode", "Resume", "About Gomoku", "Main Menu" };
                    GomokuMenu gMenu = new GomokuMenu(name, m);
                    int item = gMenu.Run();
                    gMenu.Modes(item);
                }
                else
                {
                    WriteLine("\nPress 'Enter' to return ");
                    ReadKey();
                    valid = true;
                }
            }
            ReadKey();
        }
    }
}
