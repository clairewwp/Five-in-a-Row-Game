
namespace Game
{
    public abstract class GameController
    {
        protected int playerNo, win;
        protected bool isPVE;
        protected string fileName;
        protected Move m;
       
        protected StonePosition[] stoneArray;
        protected Player[] playerArray;

        protected abstract Move CreateBoard();
        protected abstract void Initialize();
        protected abstract void StartPlay(int player);
        public abstract bool IsEnd();
        protected abstract void GameResult();
        public abstract void Save();
        //protected abstract void Load();
        public void Play(bool isPVE)
        {
            this.isPVE = isPVE;
            int i = 0;
            m = CreateBoard();
            Initialize();
            while (!IsEnd())
            {
                StartPlay(i);
                i = (i + 1) % playerNo;
            }
            GameResult();
        }
    }
}
