using aman.Models;
using System.Text;
using Weighted_Randomizer;

namespace aman._03_tic_tac_toe;

public class Model : ModelBase
{
    public Model(Parameter parameter)
    {
        this.parameter = parameter;
    }

    public int Run(int[] playfield, bool reversed)
    {
        List<int> validFields = new();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 9; i++)
        {
            int field = playfield[i];
            if (field == 0) validFields.Add(i);
            else if (reversed && field == 1) field = 2;
            else if (reversed && field == 2) field = 1;
            sb.Append(field);
        }

        int[] weights = ((Parameter)parameter).parameters[sb.ToString()];
        return Shuffle(weights, validFields);
    }

    private static int Shuffle(int[] weights, List<int> validFields)
    {
        IWeightedRandomizer<int> randomizer = new DynamicWeightedRandomizer<int>();
        for (int field = 0; field < weights.Length; field++)
        {
            randomizer.Add(field, weights[field]);
        }

        for (; ; )
        {
            int field = randomizer.NextWithRemoval();
            if (validFields.Contains(field)) return field;
        }
    }
}
