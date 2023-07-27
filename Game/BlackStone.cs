

namespace Game
{
    class BlackStone : StonePosition
    {
        public StoneColor Color 
        {
            get 
            {
                return color;
            }
        }
        public BlackStone()
        {
            color = StoneColor.black;
            symbol = 'x';
        }
    }
}
