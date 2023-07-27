using System;
using static System.Console;
namespace Game
{
    public abstract class Move : BoardFactory
    {
        public Move()
        {

        }
        protected StonePosition sp;
        public virtual void DisplayGrid()
        {

        }
        public void MakeMove(int x, int y, StonePosition sp)
        {
            finalX = x;
            finalY = y;
            Position[x, y] = sp;
        }
        public virtual bool ValidInput(int x, int y)
        {
            if (x >= row || x < 0 || y >= column || y < 00)
            {
                Write("\n    * Please enter a valid move * ");
                return false;
            }
            StonePosition sp = Position[x, y];
            if (sp == null)//is not occupied, then true
                return true;
            else
            {
                Write("\n     * The location has been occupied * ");
                return false;
            }
        }
        public virtual void Undo()
        {
            if (finalX > row-1 || finalX < 0)
                return;
            if (finalY >column -1 || finalY < 0)
                return;
            Position[finalX, finalY] = null;
        }
        public virtual void Redo(int x, int y)
        {
            if (x >= row || x < 0)
                return;
            if (y >= column || y < 0)
                return;
        }
        
    }
}
