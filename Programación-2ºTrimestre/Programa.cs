using System;
using System.IO;

namespace Programación_2ºTrimestre
{
    class Programa
    {


        static void Main(string[] args)
        {
            var estado = Estado.Portada;
            Tablero m1, m2, m3;
            Jugador player;
            String portadaFile = "data/portada.txt";
            String menuFile = "data/menu.txt";
            String controlFile = "data/controls.txt";
            String overFile = "data/over.txt";
            String contenidoOver;
            String contenidoPortada;
            String contenidoMenu;
            String contenidoControls;

            StreamReader archivoOver;
            StreamReader archivoPortada;
            StreamReader archivoMenu;
            StreamReader archivoControls;

            archivoOver = new StreamReader(overFile);
            contenidoOver = archivoOver.ReadToEnd();

            archivoPortada = new StreamReader(portadaFile);
            contenidoPortada = archivoPortada.ReadToEnd();

            archivoControls = new StreamReader(controlFile);
            contenidoControls = archivoControls.ReadToEnd();

            archivoMenu = new StreamReader(menuFile);
            contenidoMenu = archivoMenu.ReadToEnd();

            ConsoleKeyInfo seleccion;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(contenidoPortada);

            Console.CursorVisible = false;

            do
            {
                seleccion = Console.ReadKey(true);

                switch (estado)
                {
                    case Estado.Over:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(contenidoOver);
                        switch (seleccion.Key)
                        {
                            case ConsoleKey.R:
                                estado = Estado.InGame;
                                break;

                            case ConsoleKey.M:
                                estado = Estado.Menu;
                                break;
                        }
                        break;

                    case Estado.Portada:
                        if (seleccion.Key == ConsoleKey.Spacebar)
                        {
                            estado = Estado.Menu;
                        }
                        break;

                    case Estado.Controls:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(contenidoControls);

                        if (seleccion.Key == ConsoleKey.M)
                        {
                            estado = Estado.Menu;
                        }
                        break;

                    case Estado.Menu:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(contenidoMenu);
                        Console.SetCursorPosition(24, 13);
                        Console.WriteLine("J - Jugar");
                        Console.SetCursorPosition(24, 15);
                        Console.WriteLine("C - Controles");
                        switch (seleccion.Key)
                        {
                            case ConsoleKey.J:
                                Console.Clear();
                                estado = Estado.InGame;
                                break;

                            case ConsoleKey.C:
                                Console.Clear();
                                estado = Estado.Controls;
                                break;
                        }
                        break;

                    case Estado.InGame:
                        Console.Clear();
                        do
                        {

                            Console.CursorVisible = false;
                            m1 = new Tablero(300, 300);
                            player = new Jugador(m1, 150, 150);


                            IniciarMapa(m1);


                            ConsoleKeyInfo option;
                            do
                            {
                                while (Console.KeyAvailable == true)
                                {
                                    Console.ReadKey(true);
                                }


                                m1.Display(player.x, player.y);
                                player.Display(player.GetCoins());
                                option = Console.ReadKey(true);
                                player.Controls(option);

                                if (player.HasWon() && player.GetCoins() > 20)
                                {
                                    break;
                                }

                            } while (player.HasntLost());
                            if (player.HasLost())
                            {
                                estado = Estado.Over;
                                break;
                            }

                            m2 = new Tablero(300, 300);
                            player = new Jugador(m2, 150, 150);

                            IniciarMapa(m2);

                            do
                            {
                                while (Console.KeyAvailable == true)
                                {
                                    Console.ReadKey(true);
                                }

                                m2.Display(player.x, player.y);
                                player.Display(player.GetCoins());
                                option = Console.ReadKey(true);
                                player.Controls(option);

                                if (player.HasWon() && player.GetCoins() > 20)
                                {
                                    break;
                                }

                            } while (player.HasntLost());
                            if (player.HasLost())
                            {
                                estado = Estado.Over;
                                break;
                            }

                            m3 = new Tablero(300, 300);
                            player = new Jugador(m3, 150, 150);

                            IniciarMapa(m3);


                            do
                            {
                                while (Console.KeyAvailable == true)
                                {
                                    Console.ReadKey(true);
                                }

                                m3.Display(player.x, player.y);
                                player.Display(player.GetCoins());
                                option = Console.ReadKey(true);
                                player.Controls(option);



                                if (player.HasWon() && player.GetCoins() > 20)
                                {
                                    break;
                                }

                            } while (player.HasntLost());
                            if (player.HasLost())
                            {
                                estado = Estado.Over;
                                break;
                            }
                        } while (player.HasntLost());
                        break;

                }
            } while (seleccion.Key != ConsoleKey.Escape);


            void IniciarMapa(Tablero nivel)
            {
                nivel.RandomWalk(1500);
                nivel.ConvierteMonedas();
                nivel.PutItems(250);
                nivel.PutGear();
                nivel.PutMonsters(100);
            }

        }
    }
}
