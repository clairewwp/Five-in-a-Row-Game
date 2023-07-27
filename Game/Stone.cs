using System;

namespace Game
{
    public abstract class StonePosition
    {
        public char symbol;
        public StoneColor color;
    }
    public enum StoneColor 
    {
        black=0,
        white=1
    }
}
