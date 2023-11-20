


using System.Dynamic;
using Microsoft.VisualBasic;
using Utils;

namespace Dungeon
{
    public class Position
    {
        public int row;
        public int column;
    }

    class Attack
    {
        public String Description { get; set; }
        public int Damage { get; set; }
    }

    class Enemy
    {
        public Position Position { get; set; }
        public char Symbole { get; set; }
        public string Name { get; set; }
        public string Weapon { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }

        private string[] adventureGameCharacterNames = new string[] { "Aria", "Thorin", "Zara", "Jasper", "Elena", "Cedric", "Lyra", "Orion", "Seraphina", "Gareth" };
        private string[] adventureGameSurnames = new string[] { "the Brave", "Ironheart", "Shadowwalker", "the Wise", "Stormchaser", "Dragonbane", "Nightwhisper", "the Fearless", "Oakenshield", "Lightbringer" };
        private string[] attackMethods = { "slashes with", "throws the", "stabs using the" };

        public Enemy MakeCopy()
        {
            return (Enemy)this.MemberwiseClone();
        }

        public void Rename()
        {
            Name = $"{adventureGameCharacterNames.RandomElement()} {adventureGameSurnames.RandomElement()}";
        }


        public Attack Attack()
        {
            string method = attackMethods.RandomElement(); // Note: I am using an extension I added to the array type in utils/array.cs to do this.
            Random rnd = new Random();
            int damage = rnd.Next(0, HitPoints + 1);
            string description = $"{Name} {method} {Weapon}, causing {damage} of damage to you";
            if (damage == 0) // Epic failure 
            {
                description = $"{Name} trys to {method} {Weapon}, but misses fantasticly";
            }
            else if (damage == HitPoints) // Critical hit
            {
                description = $"{Name} lunges and {method} {Weapon}, causing masive damage to you";
            }

            return new Dungeon.Attack() { Description = description, Damage = damage };

        }
    }

    class Item
    {


        public string symbole { get; }
        public string description { get; }
        public int value { get; }

        public Item(string itemType)
        {

            if (itemType == "$")
            {
                symbole = itemType;
                value = new Random().Next(5, 21);
                description = $"A small bag of loot with the value {value}";
            }
            else if (itemType == "*")
            {
                symbole = itemType;
                value = new Random().Next(3, 8);
                description = "A smal viale of poison, doing {value} points of damage";
            }

        }

    }

    public class DungeonGame : GameEngine.IScene
    {

        const char HERO_CHAR = 'H';

        static Dictionary<char, Object> GAME_ITEMS = new(){
            {' ', " "},
            {'█', "█"},
            {'X', new Enemy(){Symbole = 'X', Name = "Axe Wielder", Weapon = "Axe", HitPoints = 5, Strength= 8} },
            {'D', new Enemy(){Symbole = 'D', Name = "Ranged attacker", Weapon = "Bow", HitPoints = 5, Strength= 8} },
            {'$', new Item("$")},
            {HERO_CHAR, null}
        };


        Hero hero;
        Position heroPos;
        bool playerMoved = false;
        bool dirty = true;
        int delta_y;
        int delta_x;


        object[][] levelMap;

        string eventMessage;

        public DungeonGame(Hero hero)
        {
            this.hero = hero;
            GAME_ITEMS[HERO_CHAR] = hero;
            levelMap = LoadLevel("levels/level1.txt");
        }

        #region Dungeon Game Functions ----------------------------------------------------------------------------------

        object[][] LoadLevel(string levelFile)
        {
            string rawFileData = HelperFunctions.ReadFile(levelFile);
            char[][] tempMap = HelperFunctions.Create2DArrayFromMultiLineString(rawFileData);
            object[][] outputMap = new object[tempMap.Length][];

            for (int row = 0; row < tempMap.Length; row++)
            {
                outputMap[row] = new object[tempMap[row].Length];
                for (int column = 0; column < tempMap[row].Length; column++)
                {
                    char key = tempMap[row][column];
                    if (GAME_ITEMS.ContainsKey(key))
                    {
                        Object item = GAME_ITEMS[key];
                        if (item.GetType() == typeof(Enemy))
                        {
                            // Create a new enemy to put into the map.
                            Enemy e = ((Enemy)item).MakeCopy();
                            e.Rename(); // Give ghe enemy a cool name. 
                            e.Position = new Position() { row = row, column = column }; // Save the map position in the enemy
                            outputMap[row][column] = e; // Put the enemy into that map position. 
                        }
                        else if (item.GetType() == typeof(Hero))
                        {
                            heroPos = new() { row = row, column = column };
                            outputMap[row][column] = item;

                        }
                        else
                        {
                            // Item dos not requier or have anny special thing we need to do.
                            outputMap[row][column] = item;
                        }
                    }
                }
            }

            return outputMap;
        }

        string GetDisplaySymboleFor(object item)
        {

            if (item.GetType() == typeof(Enemy))
            {
                return $"{((Enemy)item).Symbole}";
            }
            else if (item.GetType() == typeof(Hero))
            {
                return $"{HERO_CHAR}";
            }
            else if (item.GetType() == typeof(Item))
            {
                return $"{((Item)item).symbole}";
            }

            return $"{item}";
        }

        bool isThisAEnemy(object mappLocationItem)
        {
            if (mappLocationItem != null)
            {
                return mappLocationItem.GetType() == typeof(Enemy);

            }
            return false;
        }


        #endregion -----------------------------------------------------------------------------------------------------

        #region GameEngine.IScene --------------------------------------------------------------------------------------

        public Action<Type, object[]> OnExitScreen { get; set; }

        public void init()
        {


        }

        public void input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyCode = Console.ReadKey(true).Key;
                if (keyCode == ConsoleKey.DownArrow || keyCode == ConsoleKey.W)
                {
                    delta_y = 1;

                }
                else if (keyCode == ConsoleKey.UpArrow || keyCode == ConsoleKey.S)
                {
                    delta_y = -1;
                }

                if (keyCode == ConsoleKey.RightArrow || keyCode == ConsoleKey.D)
                {
                    delta_x = 1;

                }
                else if (keyCode == ConsoleKey.LeftArrow || keyCode == ConsoleKey.A)
                {
                    delta_x = -1;
                }

                playerMoved = delta_y != 0 || delta_x != 0;
            }
        }

        public void update()
        {
            if (playerMoved)
            {
                playerMoved = false;

                int newRow = heroPos.row + delta_y;
                int newCol = heroPos.column + delta_x;

                char newLocationDisplayChar = GetDisplaySymboleFor(levelMap[newRow][newCol])[0];
                object locationItem = levelMap[newRow][newCol];

                // Now we ditermin what happens if we enter the new location.
                if (newLocationDisplayChar == ' ')
                {
                    levelMap[heroPos.row][heroPos.column] = ' ';
                    levelMap[newRow][newCol] = hero;
                    heroPos.row = newRow;
                    heroPos.column = newCol;
                }
                else if (isThisAEnemy(locationItem) == true)
                {
                    eventMessage = ((Enemy)locationItem).Attack().Description;

                }
                else if (newLocationDisplayChar == '$')
                {
                    levelMap[heroPos.row][heroPos.column] = ' ';
                    heroPos.row = newRow;
                    heroPos.column = newCol;
                    eventMessage = ((Item)levelMap[newRow][newCol]).description;
                    levelMap[newRow][newCol] = hero;
                }

                delta_y = 0;
                delta_x = 0;
                dirty = true;
            }


        }

        public void draw()
        {


            if (dirty)
            {
                dirty = false;
                string output = "";

                Console.Clear();
                int sRow = 1;
                int sCol = 0;

                for (int row = 0; row < levelMap.Length; row++)
                {
                    output += ANSICodes.Positioning.SetCursorPos(sRow + row, sCol);
                    for (int col = 0; col < levelMap[row].Length; col++)
                    {
                        output += GetDisplaySymboleFor(levelMap[row][col]);
                    }
                }


                Console.Write(output);

                if (eventMessage != "")
                {
                    Console.WriteLine($"\n{eventMessage}");
                }

                Console.Write(ANSICodes.Positioning.SetCursorPos(sRow, sCol));
                Console.Write(ANSICodes.Colors.Yellow);
                Console.Write(hero);
                Console.Write(ANSICodes.Reset);

            }


        }

        #endregion ---------------------------------------------------------------------------------------------------------


    }


}