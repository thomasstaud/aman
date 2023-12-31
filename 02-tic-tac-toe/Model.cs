﻿using aman.Models;
using Weighted_Randomizer;

namespace aman._02_tic_tac_toe;

public class Model : ModelBase
{
    public Model(Parameter parameter)
    {
        this.parameter = parameter;
    }

    public int Run(int turn, List<int> validFields)
    {
        int[] weights = ((Parameter)parameter).parameters[turn];
        return Shuffle(weights, validFields);
    }

    private static int Shuffle(int[] weights, List<int> validFields)
    {
        IWeightedRandomizer<int> randomizer = new DynamicWeightedRandomizer<int>();
        for (int field = 0; field < weights.Length; field++)
        {
            randomizer.Add(field, weights[field]);
        }

        for(; ; )
        {
            int field = randomizer.NextWithRemoval();
            if (validFields.Contains(field)) return field;
        }
    }
}
