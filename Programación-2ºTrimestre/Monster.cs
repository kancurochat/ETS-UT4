using System;
using System.Collections.Generic;
using System.Text;

namespace Programación_2ºTrimestre
{
    public class Monster : Celda, IEnemy
    {
        private int dmg;

        public Monster()
        {
            this.dmg = 40;
        }

        public int getDmg()
        {
            return this.dmg;
        }

        public override void Display()
        {
            if (this.shadow == true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            else
            {

            
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("M");
            }
        }
    }
}
