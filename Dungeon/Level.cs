
using Utils;

namespace Dungeon
{

    public class Level
    {
        public char[][] leveldata;

        public Level(string lvlFile)
        {
            string content = Utils.HelperFunctions.ReadFile(lvlFile);
            leveldata = Utils.HelperFunctions.Create2DArrayFromMultiLineString(content);

        }
    }
}