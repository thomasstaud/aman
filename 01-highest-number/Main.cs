using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aman._01_highest_number
{
    public class Main
    {
        private const int modelCount = 1000;
        private const float minValue = 0.0f;
        private const float maxValue = 1.0f;
        private const float initialValue = 0.5f;
        private const float maxVariance = 0.1f;

        private const int iterations = 1000;

        private static readonly Random r = new();

        public static void Run()
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

            Console.WriteLine($"Training completed.\nSurviving Model #1:\n");
            Console.WriteLine($"parameter: {models[0].parameter}");
        }

        private static List<Model> Replicate(List<Model> models)
        {
            List<Model> newGeneration = new();

            for (int j = 0; j < models.Count; j++)
            {
                float parameter = models[j].parameter;
                newGeneration.Add(models[j]);
                newGeneration.Add(CreateModel(parameter));
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
            if (model0.Run() > model1.Run()) return 0;
            if (model0.Run() < model1.Run()) return 1;
            if (r.Next(2) == 0) return 0;
            else return 1;
        }

        private static List<Model> CreateModels()
        {
            List<Model> models = new();

            for (int i = 0; i < modelCount; i++)
            {
                models.Add(CreateModel(initialValue));
            }

            return models;
        }

        private static Model CreateModel(float parameter)
        {
            float variance = ((float)r.NextDouble() - 0.5f) * maxVariance;
            float newParameter = Math.Clamp(parameter + variance, minValue, maxValue);
            return new Model(newParameter);
        }
    }
}
