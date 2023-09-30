using System.Diagnostics;

namespace aman.Models;

public abstract class TrainingBase
{
    // parameters with default values, can be changed if neccessary
    protected int iterations = 1000;
    protected int modelCount = 1000;
    protected int initialValue = 50;
    protected int minValue = 1;
    protected int maxValue = 100;
    protected int maxVariance = 5;

    protected readonly Random r = new();

    protected List<ModelBase> models = new();

    public List<ModelBase> Run(bool debug = false)
    {
        if (debug) return DebugRun();



        CreateModels();

        for (int i = 0; i < iterations; i++)
        {
            Battle();
            Replicate();
            models = models.OrderBy(model => r.Next()).ToList();
        }

        return models;
    }

    protected abstract void CreateModels();
    protected abstract ModelBase CreateModel(ParameterBase pb);
    private void Battle()
    {
        List<ModelBase> survivors = new();

        for (int i = 0; i < models.Count - 1; i += 2)
        {
            int winner = Match(models[i], models[i + 1]);
            survivors.Add(models[i + winner]);
        }

        models = survivors;
    }
    private void Replicate()
    {
        List<ModelBase> newGeneration = new();

        for (int i = 0; i < models.Count; i++)
        {
            ModelBase model = models[i];
            ParameterBase parameter = model.parameter;
            newGeneration.Add(model);
            newGeneration.Add(CreateModel(parameter));
        }

        models = newGeneration;
    }

    protected abstract int Match(ModelBase mb0, ModelBase mb1);






    private List<ModelBase> DebugRun()
    {
        Console.WriteLine($"Initializing Models...");

        CreateModels();

        Console.WriteLine($"Models initialized.\n\nStarting Training...\n");

        for (int i = 0; i < iterations; i++)
        {
            Battle();
            Replicate();
            models = models.OrderBy(model => r.Next()).ToList();

            Console.WriteLine($"Iteration {i} completed.");
        }

        Console.WriteLine($"Training completed.\n");
        Console.WriteLine(models[0]);

        return models;
    }
}
