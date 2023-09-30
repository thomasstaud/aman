using _02 = aman._02_tic_tac_toe;
using _03 = aman._03_tic_tac_toe;
using aman.Models;

namespace aman
{
    public class TicTacToe
    {
        private static ModelBase model;

        private static readonly Random r = new();

        public static void Run(ModelBase model)
        {
            TicTacToe.model = model;

            for (; ; )
            {
                Play();
                Console.WriteLine("Nochmal versuchen? (J/N)");
                string s;
                for (; ; )
                {
                    s = Console.ReadLine();
                    if (s.ToLower() is "j" or "n") break;
                }
                if (s.ToLower() == "n") break;
            }
        }

        private static void Play()
        {
            int winner = 0;
            int[] playfield = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Console.WriteLine("\nSpielfeld:");
            PrintPlayfield(playfield, true);
            Console.WriteLine("\nViel Glück!!!");

            // 50% chance for player to start
            bool playerTurn = false;
            if (r.Next(2) == 1)
                playerTurn = true;

            for (int i = 0; i < 9; i++)
            {
                if (playerTurn)
                {
                    PrintPlayfield(playfield, false);
                    Console.WriteLine("\nEnter a valid field (0-8):\n");

                    for (; ; )
                    {
                        try
                        {
                            int field = Convert.ToInt32(Console.ReadLine());
                            if (ValidFields(playfield).Contains(field))
                            {
                                playfield[field] = 1;
                                break;
                            }
                        }
                        catch { }
                        Console.WriteLine("\nThat's not valid!!! Try again!");
                    }
                }
                else
                {
                    Console.WriteLine("\nAman is making his move...");

                    int field;
                    if (model is _02.Model m2)
                        field = m2.Run(i, ValidFields(playfield));
                    else if (model is _03.Model m3)
                        field = m3.Run(playfield, !playerTurn);
                    else throw new Exception("invalid model");

                    playfield[field] = 2;
                }

                playerTurn = !playerTurn;
                winner = CheckVictory(playfield);
                if (winner != 0) break;
            }

            switch (winner)
            {
                case 0:
                    Console.WriteLine("Unentschieden. Starkes Match!!");
                    break;
                case 1:
                    Console.WriteLine("Du hast gewonnen. Glück gehabt!!");
                    break;
                case 2:
                    Console.WriteLine("Hah, verloren! Du Loser!!");
                    break;
            }
        }

        private static void PrintPlayfield(int[] playfield, bool numbers)
        {
            Console.Write("┌─┬─┬─┐");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("\n│");
                for (int j = 0; j < 3; j++)
                {
                    int field = 3 * i + j;
                    int sign = playfield[field];
                    switch (sign)
                    {
                        case 0:
                            if (numbers)
                                Console.Write($"{field}│");
                            else
                                Console.Write(" │");
                            break;
                        case 1:
                            Console.Write("X│");
                            break;
                        case 2:
                            Console.Write("O│");
                            break;
                    }
                }

                if (i != 2)
                {
                    Console.Write("\n├─┼─┼─┤");
                }
            }
            Console.Write("\n└─┴─┴─┘");
        }

        private static List<int> ValidFields(int[] playfield)
        {
            List<int> validFields = new();
            for (int i = 0; i < playfield.Length; i++)
            {
                if (playfield[i] == 0) validFields.Add(i);
            }
            return validFields;
        }

        private static int CheckVictory(int[] playfield)
        {
            for (int player = 1; player <= 2; player++)
            {
                // check rows
                if (playfield[0] == player && playfield[1] == player && playfield[2] == player) return player;
                if (playfield[3] == player && playfield[4] == player && playfield[5] == player) return player;
                if (playfield[6] == player && playfield[7] == player && playfield[8] == player) return player;

                // check columns
                if (playfield[0] == player && playfield[3] == player && playfield[6] == player) return player;
                if (playfield[1] == player && playfield[4] == player && playfield[7] == player) return player;
                if (playfield[2] == player && playfield[5] == player && playfield[8] == player) return player;

                // check diagonals
                if (playfield[0] == player && playfield[4] == player && playfield[8] == player) return player;
                if (playfield[6] == player && playfield[4] == player && playfield[2] == player) return player;
            }

            return 0;
        }
    }
}
