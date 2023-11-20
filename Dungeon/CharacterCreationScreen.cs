using System.ComponentModel;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using Utils;
using static Utils.Output;

namespace Dungeon
{
    class QuestionAndAnswer
    {
        public string question { get; set; }
        public string answer { get; set; }
        public Action<object> action { get; set; }
    }

    public class CharacterCreationScreen : GameEngine.IScene
    {
        #region Constants And Variables 

        Queue<QuestionAndAnswer> questions = new();
        QuestionAndAnswer qa;
        Hero hero;

        #endregion


        #region GameEngine.IScene ------------------------------------------------------------------------------------------

        public Action<Type, Object[]> OnExitScreen { get; set; }

        public void init()
        {
            hero = new Hero();
            questions.Enqueue(new() { question = "Character name", answer = "", action = (name) => { hero.name = (string)name; } });
            questions.Enqueue(new() { question = "Character class", answer = "", action = (choice) => { hero.choice = (string)choice; } });
        }

        public void input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key != ConsoleKey.Enter && qa != null)
                {
                    if (keyInfo.Key == ConsoleKey.Backspace && qa.answer.Length > 0)
                    {
                        qa.answer = qa.answer.Substring(0, qa.answer.Length - 1);
                    }
                    else
                    {
                        qa.answer += keyInfo.KeyChar;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter && qa != null)
                {
                    qa.action(qa.answer);
                    qa = null;
                }
            }
        }

        public void update()
        {
            if (qa == null && questions.Count > 0)
            {
                qa = questions.Dequeue();
            }
            else if (qa == null)
            {
                OnExitScreen(typeof(DungeonGame), new object[] { hero });
            }

        }

        public void draw()
        {
            if (qa != null)
            {
                Console.Clear();
                Console.WriteLine($"{ANSICodes.Colors.Yellow}{qa.question} :{ANSICodes.Reset} {qa.answer}");
            }

        }

        #endregion



    }

}