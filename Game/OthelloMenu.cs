using System;
namespace Game
{
    class OthelloMenu : MainMenu
    {
        string[] oMenu;
        string name;
        public OthelloMenu(string name, string[] oMenu) : base(name, oMenu)
        {
            this.name = name;
            this.oMenu = oMenu;
        }
        public void Modes(int selected)
        {

            switch (selected)
            {
                case 0:
                    RunPVP();
                    break;
                case 1:
                    RunPVE();
                    break;
                case 2:
                    Resume();
                    break;
                case 3:
                    About();
                    break;
                case 4:
                    Game g = new Game();
                    g.StartMenu();
                    break;
            }
        }
        public void RunPVP() { UnderConstruction(name, oMenu); }
        public void RunPVE() { UnderConstruction(name, oMenu); }
        public void Resume() { UnderConstruction(name, oMenu); }
        public void About() { UnderConstruction(name, oMenu); }
    }
}
