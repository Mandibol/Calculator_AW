using System;
using System.Collections.Generic;

namespace gametest
{   
    class Position
    {
        public int x = 0;
        public int y = 0;
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Skapar en String för att hantera historiken. String fungerar bra då antalet poster varierar och växer.
            List<string> historik = new List<string>();

            //Loopa programmet
            bool calc = false;
            while (!calc)
            {
                string inputOne = "";
                string inputTwo = "";
                string op = "";
                string output = "";
                string result = "";
                double numOne = 0;
                double numTwo = 0;

                //Input för första numret och kontrollera att det är ett nummer
                while (!double.TryParse(inputOne, out numOne))
                {
                    Console.Clear();
                    Console.WriteLine("> Minräknare,Skriv ett nummer:");
                    Console.Write("> ");
                    inputOne = Console.ReadLine();
                }

                //Input för operatorn och kontrollera att det är en giltig operator

                while (!(op == "+" || op == "-" || op == "/" || op == "*" || op == "Marcus"))
                {
                    Console.Clear();
                    Console.WriteLine("> Minräknare, välj operator +,-,/,*:");
                    Console.Write("> " + inputOne + " ");
                    op = Console.ReadLine();
                }
                if (op == "Marcus")
                {
                    Console.Clear();
                    break;
                }

                //Input för andra numret och kontrollera att det är ett nummer
                while (!double.TryParse(inputTwo, out numTwo))
                {
                    Console.Clear();
                    Console.WriteLine("> Minräknare, Skriv ett nummer:");
                    Console.Write("> " + inputOne + " " + op + " ");
                    inputTwo = Console.ReadLine();
                }

                // Räkna ut Resultatet
                switch (op)
                {
                    case "+":
                        result = Convert.ToString(numOne + numTwo);
                        break;
                    case "-":
                        result = Convert.ToString(numOne - numTwo);
                        break;
                    case "/":
                        if (numTwo == 0)
                        {
                            result = "CANT DIVIDE BY ZERO!";
                        }
                        else
                        {
                            result = Convert.ToString(numOne / numTwo);
                        }
                        break;
                    case "*":
                        result = Convert.ToString(numOne * numTwo);
                        break;
                }

                // Skriv ut Resultat
                output = inputOne + " " + op + " " + inputTwo + " = " + result;
                Console.Clear();
                Console.WriteLine("> Minräknare: Resultat");
                Console.WriteLine(output);

                // Historia, Skriv ut listan med historik i omvänd ordning.
                historik.Add(output);
                Console.WriteLine("\nHistorik:");
                for (int i = historik.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(historik[i]);
                }

                Console.WriteLine("Tryck <Escape> För att avsluta. Vilken knapp som helst för att fortsätta.");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape) { Environment.Exit(0); }
            }


            //Spelet Börjar här
            //
            //
            Position player = new Position();
            player.x = 2;
            player.y = 2;

            Position camera = new Position();
            Position offset = new Position();
            offset.x = 1;
            offset.y = 3;

            bool haveKey = false;
            bool exit = false;

            //En 2dimensionell array som används som ritning för labyrinten. Array över lista då den har en fast storlek.
            int[,] gameGrid = new int[16, 16] {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,1,0,0,0,1,0,0,0,0,0,5,1},
                {1,0,0,0,0,0,1,0,1,0,1,1,1,1,1,1},
                {1,0,0,0,1,1,1,0,1,0,0,0,0,1,0,1},
                {1,1,4,1,1,1,1,0,1,1,1,1,0,1,0,1},
                {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,1},
                {1,1,0,1,0,1,0,3,0,1,0,1,1,1,0,1},
                {1,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1},
                {1,0,1,1,0,1,1,1,1,1,1,1,1,1,4,1},
                {1,0,1,1,0,1,0,0,0,1,0,0,0,1,0,1},
                {1,0,1,0,0,1,0,1,3,1,0,1,3,1,0,1},
                {1,0,0,0,1,1,0,1,1,1,0,1,1,1,0,1},
                {1,0,1,0,0,0,0,1,0,0,0,1,0,0,0,1},
                {1,0,1,1,1,1,1,1,0,1,1,1,0,1,1,1},
                {1,0,0,0,0,4,0,0,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            };

            //En 2dimensionell array som används för att spara grafiken. Array över lista då den har en fast storlek och inte behöver moddifieras.
            // Grafik och Färg
            string[,] gridArt = new string[6,4];

            gridArt[0, 0] = "      ";
            gridArt[0, 1] = "      ";
            gridArt[0, 2] = "      ";
            gridArt[0, 3] = "15";

            gridArt[1, 0] = "██████";
            gridArt[1, 1] = "██████";
            gridArt[1, 2] = "██████";
            gridArt[1, 3] = "15";

            gridArt[2, 0] = " (o)  ";
            gridArt[2, 1] = "/(≡)\\ ";
            gridArt[2, 2] = " / \\  ";
            gridArt[2, 3] = "11";

            gridArt[3, 0] = "      ";
            gridArt[3, 1] = " []╦╗ ";
            gridArt[3, 2] = "      ";
            gridArt[3, 3] = "15";

            gridArt[4, 0] = "║    ║";
            gridArt[4, 1] = "║ () ║";
            gridArt[4, 2] = "║ /\\ ║";
            gridArt[4, 3] = "15";

            gridArt[5, 0] = "╔════╗";
            gridArt[5, 1] = "║SLUT║";
            gridArt[5, 2] = "╚════╝";
            gridArt[5, 3] = "10";

            //Backgrunden och HUD ritas ut.
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║                 LABYRINT                 ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║              Press Any Key               ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("║                                          ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ Arrows : Move                 Esc : Exit ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            // En while Loop Inom vilken spelet körs
            while (!exit)
            {   
                // Target behövs inte utanför loopen så definieras här.
                // Target är den position spelaren försöker ta sig till.
                Position target = new Position();
                target.x = player.x;
                target.y = player.y;

                // Programmet stannar och väntar på en Key input. 
                // Om det är en ArrowKey så uppdateras Target
                // Escape Avslutar Spelet
                ConsoleKey inputKey = Console.ReadKey(true).Key;
                switch (inputKey)
                {
                    case ConsoleKey.LeftArrow:
                        target.x = Math.Max(player.x - 1,0);
                        break;
                    case ConsoleKey.RightArrow:
                        target.x = Math.Min(player.x + 1, 15);
                        break;
                    case ConsoleKey.UpArrow:
                        target.y = Math.Max(player.y - 1, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        target.y = Math.Min(player.y + 1, 15);
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        Console.SetCursorPosition(0, 22);
                        Console.WriteLine("haha, gav du upp?");
                        break;
                }

                // Kontrollerar värdet i Target gameGrid
                switch (gameGrid[target.y, target.x])
                {
                    case 0: //Ingen kollision, flytta Spelaren och kameran
                        gameGrid[player.y, player.x] = 0; //rensa spelaren från gridden
                        gameGrid[target.y, target.x] = 2; //skriv in spelarens nya position
                        // uppdatera spelarens position
                        player.x = target.x; 
                        player.y = target.y;
                        // Sätter Kamerans postion till spelarens men clampad för att vara inom gridden.
                        camera.x = Math.Clamp(player.x - 3, 0, 9);
                        camera.y = Math.Clamp(player.y - 2, 0, 11);
                        break;
                    case 3: //Nyckel, plocka upp nyckeln
                        gameGrid[target.y, target.x] = 0;
                        haveKey = true;
                        Console.SetCursorPosition(offset.x, offset.y - 2);
                        Console.Write(" []╦╗ ");
                        break;
                    case 4: //Lås, plocka upp nyckeln
                        if (haveKey == true) 
                        {
                            gameGrid[target.y, target.x] = 0;
                            haveKey = false;
                            Console.SetCursorPosition(offset.x, offset.y - 2);
                            Console.Write("      ");
                        }
                        break;
                    case 5: //Mål, avsluta spelet och grattulera
                        exit = true;
                        Console.SetCursorPosition(0, 22);
                        Console.WriteLine("Grattis Du Vann!");
                        break;

                }
                //Ritar ut Grafiken med en offset för att ta hänsyn till HUD. Ritar ut en 5*7 del av kartan baserat på kamerans position.
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        Console.ForegroundColor = (ConsoleColor)int.Parse(gridArt[gameGrid[camera.y + y, camera.x + x], 3]);
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition((x * 6) + offset.x, (y * 3) + offset.y + i);
                            Console.WriteLine(gridArt[gameGrid[camera.y + y, camera.x + x],i]);
                        }
                        Console.ResetColor();
                        Console.SetCursorPosition(0, 24);
                    }
                }
            }
            // Tryck Enter för att avsluta
            Console.WriteLine("Tryck <Escape> För att avsluta.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) {}
        }
    }
}
