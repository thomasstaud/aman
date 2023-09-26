using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aman._02_tic_tac_toe
{
    public class Main
    {
        private const int modelCount = 250;
        private const int initialValue = 50;
        private const int minValue = 1;
        private const int maxValue = 100;
        private const int maxVariance = 5;

        private const int iterations = 100000;

        private static readonly Random r = new();

        public static Model Run()
        {
            Console.WriteLine($"Initializing Models...");

            List<Model> models = CreateModels();

            Console.WriteLine($"Models initialized.\n\nStarting Training...\n");

            for (int i = 0; i < iterations; i++)
            {
                models = Battle(models);
                models = Replicate(models);
                models = models.OrderBy(model => r.Next()).ToList();

                Console.WriteLine($"Iteration {i} completed.");
            }

            //Console.WriteLine($"Training completed.\n");
            //Console.WriteLine($"Surviving Model #1:");
            //Console.WriteLine($"parameters:");

            //int[][] parameters = models[0].parameters;
            //for (int i = 0; i < 9; i++)
            //{
            //    Console.Write("\n{");
            //    for (int j = 0; j < 9; j++)
            //    {

            //        Console.Write($" {parameters[i][j]}");
            //    }
            //    Console.Write(" }");
            //}

            return models[0];
        }



        private static List<Model> Replicate(List<Model> models)
        {
            List<Model> newGeneration = new();

            for (int j = 0; j < models.Count; j++)
            {
                int[][] parameters = models[j].parameters;
                newGeneration.Add(models[j]);
                newGeneration.Add(CreateModel(parameters));
            }

            return newGeneration;
        }

        private static List<Model> Battle(List<Model> models)
        {
            List<Model> newGeneration = new();

            for (int j = 0; j < models.Count - 1; j += 2)
            {
                int winner = Match(models[j], models[j + 1]);
                newGeneration.Add(models[j + winner]);
            }

            return newGeneration;
        }

        private static int Match(Model model0, Model model1)
        {
            // 50% chance to swap models
            if (r.Next(2) == 1)
                (model0, model1) = (model1, model0);

            int[] playfield = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 9; i++)
            {
                if (i % 2 == 1)
                {
                    int field = model0.Run(i, ValidFields(playfield));
                    playfield[field] = 1;
                }
                else
                {
                    int field = model1.Run(i, ValidFields(playfield));
                    playfield[field] = 2;
                }

                int victory = CheckVictory(playfield);
                if (victory != 0) return victory-1;
            }

            // tie, 50% chance for both models to win
            if (r.Next(2) == 0) return 0;
            return 1;
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

        private static List<Model> CreateModels()
        {
            List<Model> models = new();

            int[][] initialParameters = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                initialParameters[i] = new int[9];

                for (int j = 0; j < 9; j++)
                {
                    initialParameters[i][j] = initialValue;
                }
            }

            for (int i = 0; i < modelCount; i++)
            {
                models.Add(CreateModel(initialParameters));
            }

            return models;
        }

        private static Model CreateModel(int[][] parameters)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int variance = r.Next(-maxVariance, maxVariance+1);
                    parameters[i][j] = Math.Clamp(parameters[i][j] + variance, minValue, maxValue);
                }
            }

            return new Model(parameters);
        }
    }
}
