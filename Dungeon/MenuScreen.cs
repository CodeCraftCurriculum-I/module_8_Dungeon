using System.ComponentModel;
using System.Runtime.CompilerServices;
using Utils;
using static Utils.Output;

namespace Dungeon
{
    using MenuItem = System.ValueTuple<string, Action>; // This is creating a better name for our 
    enum TOC_INDEXES : int // The available menus 
    {
        RootMenu,
        StartGameMenu,
        SettingsMenu,
    }

    public class MenuScreen : GameEngine.IScene
    {
        #region Constants And Variables 
        Menu currentMenu;
        TOC_INDEXES currentTOC = TOC_INDEXES.RootMenu;
        int selectedItemInMenu = 0;
        int menuChange = 0;
        string art;
        #endregion


        public MenuScreen(string graphics)
        {
            art = graphics;
            currentMenu = GetMenuForMenuIndex(currentTOC);
        }

        Menu GetMenuForMenuIndex(TOC_INDEXES menuIndex)
        {
            Menu output;
            if (TOC_INDEXES.StartGameMenu == menuIndex)
            {
                output = new Menu()
                {
                    itemsDescription = new string[] { "Small Game", "Large Game", "XL Game", "Back" },
                    itemsAction = new Action[]{
                        ()=>{ OnExitScreen(typeof(GreedyGame),new object[]{GAME_SIZE.SMALL}); },
                        ()=>{ },
                        ()=>{ },
                        ()=>{SwapMenu(TOC_INDEXES.RootMenu); }
                     }
                };

            }
            else if (TOC_INDEXES.SettingsMenu == menuIndex)
            {
                output = new Menu()
                {
                    itemsDescription = new string[] { "Language", "Dificulty", "Credits", "Back" },
                    itemsAction = new Action[]{
                        ()=>{ },
                        ()=>{ },
                        ()=>{ },
                        ()=>{SwapMenu(TOC_INDEXES.RootMenu); }
                     }
                };
            }
            else
            {
                output = new Menu()
                {
                    itemsDescription = new string[] { "Start New Game", "Settings", "Quit" },
                    itemsAction = new Action[]{
                        ()=>{ SwapMenu(TOC_INDEXES.StartGameMenu); },
                        ()=>{ SwapMenu(TOC_INDEXES.SettingsMenu); },
                        ()=>{ OnExitScreen(null,null); },
                     }
                };
            }
            return output;
        }

        void SwapMenu(TOC_INDEXES newMenuIndex)
        {
            currentMenu = GetMenuForMenuIndex(newMenuIndex);
            selectedItemInMenu = 0;
        }

        void OntMenuAction(int item)
        {
            if (item < currentMenu.Length)
            {
                currentMenu[item].action();
            }
        }

        #region GameEngine.IScene ------------------------------------------------------------------------------------------
        public Action<Type, Object[]> OnExitScreen { get; set; }
        public void init()
        {

        }
        public void input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyCode = Console.ReadKey(true).Key;
                if (keyCode == ConsoleKey.DownArrow)
                {
                    menuChange = 1;
                }
                else if (keyCode == ConsoleKey.UpArrow)
                {
                    menuChange = -1;
                }
                else if (keyCode == ConsoleKey.Enter)
                {
                    OntMenuAction(selectedItemInMenu);
                }
            }

        }
        public void update()
        {
            selectedItemInMenu += menuChange;
            selectedItemInMenu = Math.Clamp(selectedItemInMenu, 0, currentMenu.Length - 1);
            menuChange = 0;
        }
        public void draw()
        {
            Console.Clear();
            Console.WriteLine(art);
            Console.WriteLine("\n\n");
            for (int index = 0; index < currentMenu.Length; index++)
            {
                if (index == selectedItemInMenu)
                {
                    printActiveMenuItem($"* {currentMenu[index].description} *");
                }
                else
                {
                    printMenuItem($"  {currentMenu[index].description}  ");
                }
            }
        }
        #endregion

        void printActiveMenuItem(string item)
        {
            Output.Write(Reset(Bold(Align(item, Alignment.CENTER))), newLine: true);
        }
        void printMenuItem(string item)
        {
            Output.Write(Reset(Align(item, Alignment.CENTER)), newLine: true);
        }

    }

    class Menu
    {

        public string[] itemsDescription { get; set; }
        public Action[] itemsAction { get; set; }

        public int Length { get { return this.itemsDescription.Length; } }

        public (string description, Action action) this[int index]
        {
            get
            {
                return (description: itemsDescription[index], action: itemsAction[index]);
            }
        }

    }
}