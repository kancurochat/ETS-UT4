using System;

namespace Programación_2ºTrimestre
{
    public class Celda
    {
        public int valor;
        public int vecinos;
        public bool shadow;
        public Cosa objeto;


        public Celda()
        {
            this.valor = TipoCelda.Empty;
            this.shadow = true;
            this.objeto = null;

        }


        public virtual void Display()
        {
            if (this.shadow == true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            else
            {


                if (this.objeto != null)
                {
                    if (this.objeto is Moneda)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("$");
                    }
                    if (this.objeto is Potion)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("º");
                    }
                    if (this.objeto is Espada)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("t");
                    }
                    if (this.objeto is Armadura)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("#");
                    }
                    
                }
                else
                {

                    switch (valor)
                    {
                        case TipoCelda.Empty:
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.Write(" ");
                            break;

                        case TipoCelda.Wall:
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("▒");
                            break;

                        case TipoCelda.End:
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.Write(" ");
                            break;
                    }
                }
            }
        }


        public void TurnLight()
        {
            this.shadow = false;
        }

        public void PutCoin()
        {
            this.objeto = new Moneda();
        }

        public void PutPotion()
        {
            this.objeto = new Potion();
        }

        public void PutSword()
        {
            this.objeto = new Espada();
        }

        public void PutArmor()
        {
            this.objeto = new Armadura();
        }

        public bool IsObject()
        {
            if (this.objeto != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveCoin()
        {
            this.objeto = null;
        }

        public void PutWall()
        {
            this.valor = TipoCelda.Wall;
        }

        public void PutEnd()
        {
            this.valor = TipoCelda.End;
        }

        public void PutEmpty()
        {
            this.valor = TipoCelda.Empty;
        }



        public bool NotEmpty()
        {
            if (this.valor != TipoCelda.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsEmpty()
        {
            if (this.valor == TipoCelda.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsWalkable()
        {
            if (this.valor == TipoCelda.Empty || this.valor == TipoCelda.End)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCoin()
        {
            if (this.objeto is Moneda)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsEnd()
        {
            if (this.valor == TipoCelda.End)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsWall()
        {
            if (this.valor == TipoCelda.Wall)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
