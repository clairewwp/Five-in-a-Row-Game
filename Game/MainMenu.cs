using System;
using static System.Console;
namespace Game
{
    public class MainMenu
    {
        private int SelectedItem;
        private string[] Options;
        private string Greets;
        //contructor starts here
        public MainMenu(string g, string[] o)
        {
            Greets = g;
            Options = o;
            SelectedItem = 0;
        }
        private void DisplayOptions()
        {
            WriteLine("\n" + Greets, ForegroundColor = ConsoleColor.Yellow);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                if (i == SelectedItem)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"\n     <{currentOption}>", ForegroundColor = ConsoleColor.Blue);
            }
            ResetColor();
        }
        public virtual int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayOptions();
                ConsoleKeyInfo k = ReadKey(true);
                keyPressed = k.Key;
                //use arrow keys to select options
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedItem -= 1;
                    if (SelectedItem == -1)
                        SelectedItem = Options.Length - 1;

                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedItem += 1;
                    if (SelectedItem == Options.Length)
                        SelectedItem = 0;
                }
            } while (keyPressed != ConsoleKey.Enter);
            return SelectedItem;
        }
        public virtual void UnderConstruction(string name, string[] o)
        {
            WriteLine("\n   !Under Construction!\n", ForegroundColor = ConsoleColor.DarkRed);
            WriteLine("   Press 'Enter' to return or 'ESC'to quit ", ForegroundColor = ConsoleColor.White);
            ConsoleKeyInfo k = ReadKey(true);
            ConsoleKey keyPressed = k.Key;
            bool validation = false;
            while (!validation)
            {
                if (keyPressed != ConsoleKey.Enter && keyPressed != ConsoleKey.Escape)
                {
                    validation = false;
                    WriteLine("\n   please press 'Enter' to return or 'ESC' to quit");
                    k = ReadKey(true);
                    keyPressed = k.Key;
                }
                else
                    validation = true;
            }
            if (keyPressed == ConsoleKey.Enter)
            {
                OthelloMenu oM = new OthelloMenu(name, o);
                int oSeleted = oM.Run();
                oM.Modes(oSeleted);
            }
            else if (keyPressed == ConsoleKey.Escape)
            {
                Clear();
                WriteLine("\n  See you next time ~ Bye ", ForegroundColor = ConsoleColor.White);
                ReadKey();
                Environment.Exit(0);

            }
        }
    }

}
