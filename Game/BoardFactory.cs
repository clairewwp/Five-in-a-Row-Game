
namespace Game
{
    public abstract class BoardFactory
    {
        protected StonePosition[,] position;//雖然這是抽象但仍可以呼叫另一個class的obj
        protected int row, column, finalX, finalY;
        protected int origin = 0;
        public BoardFactory()
        {
            row = origin;
            column = origin;

        }
        public int Row
        {
            get
            {
                return row;
            }
        }
        public int Column
        {
            get
            {
                return row;
            }
        }
        public int FinalX
        {
            get
            {
                return finalX;
            }
        }
        public int FinalY
        {
            get
            {
                return finalX;
            }
        }
        public StonePosition[,] Position
        {
            get
            {
                return position;
            }
        }
        public virtual void CreateRow(int r)
        {
            this.row = r;
        }
        public virtual void CreateColumn(int c)
        {
            this.column = c;
        }
        protected void InitialGrid()
        {
            this.position = new StonePosition[row, column];
        }
    }
}
