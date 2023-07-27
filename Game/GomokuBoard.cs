
using static System.Console;

namespace Game
{
    class GomokuBoard : Move
    {
        
        public GomokuBoard()
        {
            CreateRow(15);
            CreateColumn(15);
            InitialGrid();
            
        }
        public override void DisplayGrid()
        {
            Write("       ");
            for (int r = 0; r < row; r++)
            {
                Write(" _");
            }
            WriteLine("   ");
            for (int c = 0; c < column; c++)
            {
                if (c < 9)
                    Write("    " + c + "  |");
                else
                    Write("    " + c.ToString("X") + "  |");
                for (int r = 0; r < column; r++)
                {
                    char symbol;
                    StonePosition sp = Position[r, c];
                    if (sp != null)
                    {
                        symbol = Position[r,c].symbol;
                        Write("{0}|", symbol);
                    }
                    else
                        Write("_|");
                }
                WriteLine();
            }
            Write("       ");
            for (int c = 0; c < column; c++)
            {
                if (c < 9)
                    Write(" " + c);
                else
                    Write(" " + c.ToString("X"));
            }
            Write("   -------> X axis         enter 's' to save, while input for X or Y");

            WriteLine("  ");

        }
    }
}
