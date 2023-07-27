using System;
using static System.Console;
using System.IO;
namespace Game
{
    class Game : GameController
    {
        public void StartMenu()
        {
            DisplayMenu();
        }
        public void DisplayMenu()
        {
            string g = @"
 __          __  _                          _ 
 \ \        / / | |                        | |
  \ \  /\  / /__| | ___ ___  _ __ ___   ___| |
   \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \ |
    \  /\  /  __/ | (_| (_) | | | | | |  __/_|
     \/  \/ \___|_|\___\___/|_| |_| |_|\___(_)
                                              
Please select which game you wish to play: " + "\n (↑ or ↓ to select and enter key to confirm)";
            string[] o = { "Othello", "Gomoku", "Exit" };
            MainMenu m = new MainMenu(g, o);
            int selectedItem = m.Run();
            switch (selectedItem)
            {
                case 0:
                    RunOthello();
                    break;
                case 1:
                    RunGomoku();
                    break;
                case 2:
                    Exit();
                    break;
                case 3:
                    break;
                case 4:
                    DisplayMenu();
                    break;
            }
        }
        private void RunOthello()
        {
            Clear();
            string name = $"{"Othello",15}";
            string[] m = { "Play PVP mode", "Play PVE mode", "Resume", "About Othello", "Main Menu" };
            OthelloMenu oMenu = new OthelloMenu(name, m);
            int s = oMenu.Run();
            oMenu.Modes(s);
        }
        private void RunGomoku()
        {
            Clear();
            string name = $"{"Gomoku",15}";
            string[] m = { "Play PVP mode", "Play PVE mode", "Resume", "About Gomoku", "Main Menu" };
            GomokuMenu gMenu = new GomokuMenu(name, m);
            int item = gMenu.Run();
            gMenu.Modes(item);
        }
        private void Exit()
        {
            WriteLine("     Are you sure you want to leave? Y/N ");
            char response = Convert.ToChar(ReadLine());
            if (response == 'Y')
                Environment.Exit(0);
            else if (response == 'N')
                DisplayMenu();
            else
            {
                WriteLine("     Please enter 'Y' to stay or 'N' to leave");
                Exit();
            }
        }
        protected override Move CreateBoard()
        {
            //board generator
            GomokuBoardGenerator g = new GomokuBoardGenerator();
            return g.CreateBoard(); ;
        }

        protected override void Initialize()
        {
            playerNo = 2;//always two for both gomoku and othello
            string Name;
            //generate black and white stones
            stoneArray = new StonePosition[playerNo];
            stoneArray[0] = new BlackStone();
            stoneArray[1] = new WhiteStone();

            //whether player2 is an AI or a human player
            playerArray = new Player[playerNo];
            WriteLine(" ");
            Write("     Please enter the name of player 1 : ");
            Name = ReadLine();
            playerArray[0] = new HumanPlayer(Name, true);
            if (isPVE == false)
            {
                Write("     Please enter the name of player 2 : ");
                Name = ReadLine();
                playerArray[1] = new HumanPlayer(Name, true);
            }
            else
            {
                Name = "AI";
                playerArray[1] = new AIPlayer(Name, false);
            }
            m.DisplayGrid();
        }
        protected override void StartPlay(int playerNo)
        {

            string undo, redo;
            while (true)
            {
                //where to place the stone?
                if (playerArray[playerNo].IsHuman)
                {
                    Write("\n     {0}, input value for X axis: ", playerArray[playerNo].HName, ForegroundColor = ConsoleColor.White);
                    string hexX = ReadLine();
                    if (hexX.ToUpper() == "S") { Save(); }
                    int integer, xValue, yValue;
                    if (int.TryParse(hexX, out integer))
                    {
                        xValue = integer;
                    }
                    else if (hexX.ToUpper() == "A" || hexX.ToUpper() == "B" || hexX.ToUpper() == "C" || hexX.ToUpper() == "D" || hexX.ToUpper() == "E")
                    {
                        xValue = Convert.ToInt32(hexX, 16);
                    }
                    else
                    {
                        Write("\n     Invalid, please re-enter", ForegroundColor = ConsoleColor.Red);
                        continue;
                    }
                    Write("     {0}, input value for Y axis: ", playerArray[playerNo].HName);
                    string hexY = ReadLine();
                    if (hexY.ToUpper() == "S") { Save(); }
                    if (int.TryParse(hexY, out integer))
                    {
                        yValue = integer;
                    }
                    else if (hexY.ToUpper() == "A" || hexY.ToUpper() == "B" || hexY.ToUpper() == "C" || hexY.ToUpper() == "D" || hexY.ToUpper() == "E")
                    {
                        yValue = Convert.ToInt32(hexY, 16);
                    }
                    else
                    {
                        Write("\n     Invalid, please re-enter", ForegroundColor = ConsoleColor.Red);
                        continue;
                    }
                    if (m.ValidInput(xValue, yValue))//if ture
                    {
                        m.MakeMove(xValue, yValue, stoneArray[playerNo]);
                        int x = xValue;
                        int y = yValue;
                        Clear();
                        m.DisplayGrid();
                        //simple Undo  
                        Write("\n   {0}, do you wish to 'Undo' ?  y: yes    Any key:stay    ", playerArray[playerNo].HName);
                        undo = ReadLine();
                        if (undo.ToUpper() == "Y")
                        {
                            m.Undo();
                            Clear();
                            m.DisplayGrid();

                            //Redo    Redo will only appear after undo is selected, and if player wish to redo, the next turn will still be AI or the other player, otherwise, the player re-enter x&y
                            Write("\n   {0}, do you wish to 'Redo' ?  y:yes    Any key:stay    ", playerArray[playerNo].HName);
                            redo = ReadLine();

                            if (redo.ToUpper() == "Y")
                            {
                                m.Redo(x, y);
                                m.MakeMove(x, y, stoneArray[playerNo]);

                                Clear();
                                m.DisplayGrid();
                                return;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
                //AI makes move
                else
                {
                    Random random = new Random();
                    for (int x = 0; x < m.Column; x++)
                    {
                        x = random.Next(0, m.Column);
                        for (int y = 0; y < m.Row; y++)
                        {
                            y = random.Next(0, m.Row);
                            if (m.ValidInput(x, y))
                            {
                                m.MakeMove(x, y, stoneArray[playerNo]);
                                Clear();
                                m.DisplayGrid();
                                WriteLine("\n     '{0}' places the stone at [{1} , {2}]", playerArray[playerNo].HName, x, y, ForegroundColor = ConsoleColor.Yellow);
                                return;
                            }
                        }
                    }
                }

            }
        }
        public override bool IsEnd()//As long as there is a five in a row appeared in one of the 8 directions, we got a winner and the game is over
        {
            int stoneCount = 0;
            for (int x = 0; x < m.Row; x++)
            {
                for (int y = 0; y < m.Column; y++)
                {
                    StonePosition stone = m.Position[x, y];
                    StoneColor TargetColor;
                    if (stone == null)
                        continue; //if there is no stone on the board, then continue
                    else
                        stoneCount++; //otherwise, stoneCount+1
                    //get the color of the stone
                    TargetColor = m.Position[x, y].color;
                    //check if there is a unbroken chain of five from each of the 8 direction 
                    for (int xDir = -1; xDir <= 1; xDir++)
                    {
                        for (int yDir = -1; yDir <= 1; yDir++)
                        {
                            if (xDir == 0 && yDir == 0)
                                continue;
                            int count = 0;
                            while (count < 5)
                            {
                                int targetX = x + count * xDir;
                                int targetY = y + count * yDir;
                                if (targetX < 0 || targetX >= m.Row || targetY < 0 || targetY >= m.Column || m.Position[targetX, targetY] == null || m.Position[targetX, targetY].color != TargetColor)
                                    break;
                                count++;
                            }
                            if (count == 5)
                            {
                                if (TargetColor == StoneColor.black)
                                    win = 0;
                                else
                                    win = 1;
                                return true;
                            }
                        }
                    }
                }
            }
            //if no space left for move
            if (stoneCount == m.Row * m.Column)
            {
                Write("       It's a tie");
                return true;
            }
            else//continue playing
                return false;
        }
        protected override void GameResult()
        {
            if (win == 0 || win == 1)
                WriteLine("\n     {0} Wins!!!!   ", playerArray[win].HName);

            Write("\n     Press 'Enter' to return to main menu or 'Esc' to quit ");
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
                if (keyPressed == ConsoleKey.Enter)
                {
                    DisplayMenu();
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
        public override void Save()
        {
            Write("\n    Enter 'y' to save or  any key to leave ", ForegroundColor=ConsoleColor.Red);
            string save = ReadLine();
            if (save.ToUpper() == "Y")
            {
                WriteLine("\n    Thank you for playing, you can start the game from current stage next time");
                fileName = "./History/" + DateTime.Now.ToString("yyyy-dd-MM--HH-mm-ss") + ".omk";
                FileStream outPut = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(outPut);
                writer.WriteLine();
                writer.Close();
                outPut.Close();
                Environment.Exit(0);
            }
            else
            {
                Write("See you next time. :D ");
                Environment.Exit(0);
            }

        }

    }
}
