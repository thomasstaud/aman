using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Weighted_Randomizer;

namespace aman._02_tic_tac_toe
{
    public class Model
    {
        public int[][] parameters = new int[9][];

        public Model(int[][] parameters)
        {
            this.parameters = parameters;
        }

        public int Run(int turn, List<int> validFields)
        {
            int[] weights = parameters[turn];
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
}
