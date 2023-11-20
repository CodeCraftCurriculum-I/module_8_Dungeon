namespace Dungeon
{
    public class Hero
    {
        public string name { get; set; }
        public string choice { get; set; }

        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }


        public int XP { get; }
        public int HP { get; }

        public int AvailablePoints { get; set; }


        public Hero()
        {
            Level = 1;
            AvailablePoints = 20;
        }

        public override string ToString()
        {
            return $"XP ({XP})\nHP ({HP})";
        }
    }
}