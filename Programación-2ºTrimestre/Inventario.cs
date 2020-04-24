using System;
using System.Collections.Generic;
using System.Text;

namespace Programación_2ºTrimestre
{
    public class Inventario
    {
        public List<Cosa> items;
        public int ItemIndex;
        public int CM;

        public Inventario()
        {
            this.items = new List<Cosa>();
            this.ItemIndex = -1;
            this.CM = 0;
        }

        public int GetIndex()
        {
            return ItemIndex;
        }

        public void SelectPreviousItem()
        {

            this.ItemIndex = Math.Max(this.ItemIndex - 1, -1);
        }

        public void SelectNextItem()
        {
            this.ItemIndex = Math.Min(this.items.Count - 1, this.ItemIndex + 1);
        }

        public void SelectItem(int numeroItem)
        {
            this.ItemIndex = numeroItem;
        }

        public virtual void Display()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i != this.ItemIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(50, 9 + i);

                items[i].Display();


            }
            Console.SetCursorPosition(50, 9 + items.Count);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("                  ");
            
            
        }

        public bool Borra()
        {

            items.RemoveAt(this.ItemIndex);
            this.ItemIndex = -1;
            return true;
        }

        public void AddCoins()
        {
            this.CM++;
        }

        public int GetCoins()
        {
            return this.CM;
        }

        public bool UseItem()
        {
            if (this.ItemIndex != -1)
            {


                Cosa objeto = items[this.ItemIndex];

                if (objeto is Moneda)
                {
                    AddCoins();
                    Borra();
                    return true;
                }
                else if (objeto is Potion)
                {
                    Borra();
                    return true;
                }
                else if (objeto is Espada)
                {
                    Borra();
                    return true;
                }
                else if (objeto is Armadura)
                {
                    Borra();
                    return true;
                }
                else
                {
                    this.ItemIndex = -1;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool TryAdd(Cosa item)
        {
            if (items.Count < 10 || item is Moneda)
            {
                if (item is Moneda)
                {
                    this.AddCoins();
                    return true;
                }
                else
                {
                    items.Add(item);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
    }
}