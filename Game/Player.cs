using System;
namespace Game
{
    public abstract class Player 
    {
        public string humanName;
        public bool isHuman;

        public string HName 
        {
            get 
            {
                return humanName;
            }
        }
        public bool IsHuman
        {
            get 
            {
                return isHuman;
            }
        }
    }
}