using System;

namespace Game
{
    class WhiteStone : StonePosition
    {
        public StoneColor Color
        {
            get
            {
                return color;
            }
        }
        public WhiteStone()
        {
            color = StoneColor.white;
            symbol = 'o';
        }
    }
}
