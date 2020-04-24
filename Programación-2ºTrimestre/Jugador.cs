using System;
using System.Collections.Generic;
using System.Text;

namespace Programación_2ºTrimestre
{
    public class Jugador
    {
        public int x;
        public int y;
        Tablero mapa;
        Cosa espada;
        Cosa armadura;
        Cosa anillo;
        Inventario mochila;
        public int turns;

        public Jugador(int x, int y, Tablero t)
        {
            this.mapa = t;
            this.x = x;
            this.y = y;
            this.mochila = new Inventario();
            this.turns = 350;
        }

        private void EndTurn()
        {
            this.turns -= 1;
        }

        public bool HasntLost()
        {
            if (this.turns > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasLost()
        {
            if (this.turns <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Controls(ConsoleKeyInfo option)
        {
            switch (option.Key)
            {
                case ConsoleKey.NumPad8:
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    this.MoveUp();
                    break;

                case ConsoleKey.NumPad5:
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    this.MoveDown();
                    break;

                case ConsoleKey.NumPad4:
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    this.MoveLeft();
                    break;

                case ConsoleKey.NumPad6:
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    this.MoveRight();
                    break;

                case ConsoleKey.NumPad9:
                    this.UpRight();
                    break;

                case ConsoleKey.NumPad7:
                    this.UpLeft();
                    break;

                case ConsoleKey.NumPad1:
                    this.DownLeft();
                    break;

                case ConsoleKey.NumPad3:
                    this.DownRight();
                    break;

                case ConsoleKey.Spacebar:
                    this.UseItem();
                    break;

                case ConsoleKey.Q:
                    this.selectPreviousItem();
                    break;

                case ConsoleKey.E:
                    this.selectNextItem();
                    break;
            }
        }

        public void UseItem()
        {
            if (mochila.GetIndex() != -1)
            {
                if (this.mochila.items[mochila.GetIndex()] is Potion && mochila.GetIndex() <= mochila.items.Count - 1)
                {
                    AddTurns();
                }
                else if (this.mochila.items[mochila.GetIndex()] is Armadura && mochila.GetIndex() <= mochila.items.Count - 1)
                {
                    armadura = new Armadura();
                }
                else if (this.mochila.items[mochila.GetIndex()] is Espada && mochila.GetIndex() <= mochila.items.Count - 1)
                {
                    espada = new Espada();
                }
                this.mochila.UseItem();
            }
        }

        public void AddTurns()
        {
            this.turns += 60;
        }

        internal void selectPreviousItem()
        {
            mochila.SelectPreviousItem();
        }

        internal void selectNextItem()
        {
            mochila.SelectNextItem();
        }

        private void Move(int IncX, int IncY)
        {
            int newX, newY;

            newX = this.x + IncX;
            newY = this.y + IncY;

            if (mapa.celdas[newX, newY].IsWalkable())
            {
                this.x = newX;
                this.y = newY;
                GetItem();
                if (mapa.celdas[x, y] is Monster)
                {

                    if (armadura != null)
                    {
                        this.turns -= 20;
                    }
                    else
                    {
                        this.turns -= (this.mapa.celdas[x, y] as Monster).getDmg() ;
                    }

                    if (this.espada != null)
                    {
                        this.mapa.celdas[x, y] = new Celda();
                    }

                }
                EndTurn();
            }
        }

        private void MoveUp()
        {

            this.Move(0, -1);
        }

        private void MoveDown()
        {
            this.Move(0, 1);
        }

        private void MoveRight()
        {
            this.Move(1, 0);
        }

        private void MoveLeft()
        {
            this.Move(-1, 0);
        }



        public void Display(int coins)
        {



            Console.SetCursorPosition(25, 10);

            if (this.armadura != null)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("I");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("¡");
            }

            Console.SetCursorPosition(50, 3);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" Turnos restantes: " + turns + " ");

            if (this.espada != null)
            {
                Console.SetCursorPosition(50, 6);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" ESPADA");
            }

            if (this.armadura != null)
            {
                Console.SetCursorPosition(50, 7);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" ARMADURA");
            }


            Console.SetCursorPosition(50, 4);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" MONEDAS: " + mochila.GetCoins() + " ");

            mochila.Display();



            Console.SetCursorPosition(50, 1);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(" X: " + this.x + " Y: " + this.y + " ");
        }

        public void GetItem()
        {
            if (mapa.celdas[x, y].objeto != null)
            {
                bool puedecoger;

                puedecoger = mochila.TryAdd(mapa.celdas[x, y].objeto);
                if (puedecoger == true)
                {
                    mapa.celdas[x, y].objeto = null;
                }
                else
                {
                    if (mapa.celdas[x, y].objeto is Moneda)
                    {
                        this.mochila.CM++;
                        mapa.celdas[x, y].objeto = null;
                    }

                }

            }
        }


        public bool HasWon()
        {
            if (mapa.celdas[x, y].IsEnd())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetCoins()
        {
            return this.mochila.GetCoins();
        }


        public void UpLeft()
        {
            this.Move(-1, -1);
        }

        public void UpRight()
        {
            this.Move(1, -1);
        }

        public void DownRight()
        {
            this.Move(1, 1);
        }

        public void DownLeft()
        {
            this.Move(-1, 1);
        }
    }
}