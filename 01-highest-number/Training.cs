using aman.Models;

namespace aman._01_highest_number;

public class Training : TrainingBase
{
    protected override void CreateModels()
    {
        models = new();

        Parameter initalParameter = new(initialValue);
        for (int i = 0; i < modelCount; i++)
        {
            models.Add(CreateModel(initalParameter));
        }
    }

    protected override ModelBase CreateModel(ParameterBase pb)
    {
        Parameter parameter = (Parameter)pb;

        float variance = ((float)r.NextDouble() - 0.5f) * maxVariance;
        float newValue = Math.Clamp(parameter.value + variance, minValue, maxValue);
        return new Model(new Parameter(newValue));
    }

    protected override int Match(ModelBase mb0, ModelBase mb1)
    {
        Model model0 = (Model)mb0;
        Model model1 = (Model)mb1;

        if (model0.Run() > model1.Run()) return 0;
        if (model0.Run() < model1.Run()) return 1;
        if (r.Next(2) == 0) return 0;
        else return 1;
    }
}
