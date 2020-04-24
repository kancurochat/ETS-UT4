using System;


namespace Programación_2ºTrimestre
{
    public class Tablero
    {
        public Celda[,] celdas;
        public int anchura, altura;
        public Random r;


        public Tablero(int c0, int c1)
        {
            this.r = new Random();
            this.celdas = new Celda[c0, c1];
            this.anchura = this.celdas.GetLength(0);
            this.altura = this.celdas.GetLength(1);

            for (int i = 0; i < celdas.GetLength(0); i++)
            {
                for (int j = 0; j < celdas.GetLength(1); j++)
                {
                    this.celdas[i, j] = new Celda();
                }

            }
        }





        public void PutWalls(int quantity)
        {
            int x;
            int y;
            for (int i = 0; i < quantity; i++)
            {
                do
                {

                    x = r.Next(0, celdas.GetLength(0) - 1);
                    y = r.Next(0, celdas.GetLength(1) - 1);
                }
                while (celdas[x, y].NotEmpty());
                this.celdas[x, y].PutWall();
            }

        }

        public void PutMonsters(int quantity)
        {
            int x;
            int y;
            for (int i = 0; i < quantity; i++)
            {
                do
                {

                    x = r.Next(0, celdas.GetLength(0) - 1);
                    y = r.Next(0, celdas.GetLength(1) - 1);
                }
                while (celdas[x, y].NotEmpty() || celdas[x, y].IsObject());
                this.celdas[x, y] = new Monster();
            }

        }

        public void PutItems(int quantity)
        {
            int x;
            int y;
            for (int i = 0; i < quantity; i++)
            {
                do
                {

                    x = r.Next(0, celdas.GetLength(0) - 1);
                    y = r.Next(0, celdas.GetLength(1) - 1);
                }
                while (celdas[x, y].NotEmpty() || celdas[x, y].IsObject());


                if (r.Next(100) < 65)
                {
                    this.celdas[x, y].PutCoin();
                }
                else
                {
                    this.celdas[x, y].PutPotion();
                }

            }

        }

        public void PutGear(int quantity)
        {
            int x;
            int y;
            for (int i = 0; i < quantity; i++)
            {
                do
                {

                    x = r.Next(0, celdas.GetLength(0) - 1);
                    y = r.Next(0, celdas.GetLength(1) - 1);
                }
                while (celdas[x, y].NotEmpty() || celdas[x, y].IsObject());

                this.celdas[x, y].PutArmor();
            }

            for (int i = 0; i < quantity; i++)
            {
                do
                {

                    x = r.Next(0, celdas.GetLength(0) - 1);
                    y = r.Next(0, celdas.GetLength(1) - 1);
                }
                while (celdas[x, y].NotEmpty() || celdas[x, y].IsObject());

                this.celdas[x, y].PutSword();
            }

        }

        public void Display(int centroX, int centroY)
        {
            int ventanaX = 50;
            int ventanaY = 20;
            for (int i = 0; i < ventanaX; i++)
            {
                for (int j = 0; j < ventanaY; j++)
                {
                    Console.SetCursorPosition(i, j);

                    int celdaX, celdaY;
                    celdaX = centroX - ventanaX / 2 + i;
                    celdaY = centroY - ventanaY / 2 + j;

                    if (celdaX >= 0 && celdaX < anchura && celdaY >= 0 && celdaY < altura)
                    {
                        double distancia = Math.Sqrt(Math.Pow(celdaX - centroX, 2) + Math.Pow(celdaY - centroY, 2));
                        if (distancia < 5)
                        {
                            this.celdas[celdaX, celdaY].TurnLight();
                        }
                        this.celdas[celdaX, celdaY].Display();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("~");
                    }

                }
            }
        }


        public void ConvierteMonedas()
        {
            for (int i = 0; i < anchura; i++)
            {
                for (int j = 0; j < altura; j++)
                {
                    celdas[i, j].vecinos = 0;

                    if (IsSafe(i - 1, j - 1) && celdas[i - 1, j - 1].IsWall())
                    {
                        celdas[i, j].vecinos++;
                    }
                    if (IsSafe(i - 1, j + 1) && celdas[i - 1, j + 1].IsWall())
                    {
                        celdas[i, j].vecinos++;
                    }
                    if (IsSafe(i + 1, j - 1) && celdas[i + 1, j - 1].IsWall())
                    {
                        celdas[i, j].vecinos++;
                    }
                    if (IsSafe(i + 1, j + 1) && celdas[i + 1, j + 1].IsWall())
                    {
                        celdas[i, j].vecinos++;
                    }
                    if (IsSafe(i, j - 1) && celdas[i, j - 1].IsWall())
                    {
                        celdas[i, j].vecinos++;
                    }
                    if (IsSafe(i, j + 1) && celdas[i, j + 1].IsWall())
                    {
                        celdas[i, j].vecinos++;
                    }
                    if (IsSafe(i - 1, j) && celdas[i - 1, j].IsWall())
                    {
                        celdas[i, j].vecinos++;
                    }
                    if (IsSafe(i + 1, j) && celdas[i + 1, j].IsWall())
                    {
                        celdas[i, j].vecinos++;
                    }

                    if (celdas[i, j].vecinos == 1 || celdas[i, j].vecinos == 2 || celdas[i, j].vecinos == 0)
                    {
                        if (celdas[i, j].IsWall())
                        {
                            celdas[i, j].PutEmpty();
                        }
                    }
                }
            }
        }

        public bool IsSafe(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < anchura && y < altura)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void RandomWalk(int max)
        {
            int x, y;
            int floors;

            for (int i = 0; i < anchura; i++)
            {
                for (int j = 0; j < altura; j++)
                {
                    this.celdas[i, j].PutWall();
                }
            }

            x = anchura / 2;
            y = altura / 2;

            celdas[x, y].valor = TipoCelda.Empty;
            floors = 1;

            Random r = new Random();
            while (floors <= max)
            {
                int direction = r.Next(4);

                switch (direction)
                {
                    case 0:
                        x++;
                        break;

                    case 1:
                        x--;
                        break;

                    case 2:
                        y++;
                        break;

                    case 3:
                        y--;
                        break;
                }

                if (x < 0 || y < 0 || x >= anchura || y >= altura)
                {
                    x = 0;
                    y = 0;
                }

                if (celdas[x, y].IsWall() == true)
                {
                    celdas[x, y].PutEmpty();
                    floors++;

                    if (floors == max)
                    {
                        celdas[x, y].PutEnd();
                    }
                }



            }


        }

    }
}
