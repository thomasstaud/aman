using aman.Models;
using aman.Utils;
using System.Text;

namespace aman._03_tic_tac_toe;

public class Training : TrainingBase
{
    private int[] playfield;
    private List<string> keys;

    public Training()
    {
        modelCount = 100;
        iterations = 1000;

        keys = TicTacToeStates.GetActiveLegalStates();
    }

    protected override void CreateModels()
    {
        models = new();

        int[] initialWeights = new int[9];
        for (int j = 0; j < 9; j++)
        {
            initialWeights[j] = initialValue;
        }

        Parameter initialParameter = new(new());
        foreach (string key in keys)
        {
            initialParameter.parameters[key] = initialWeights;
        }

        for (int i = 0; i < modelCount; i++)
        {
            models.Add(CreateModel(initialParameter));
        }
    }

    protected override ModelBase CreateModel(ParameterBase pb)
    {
        Parameter parameter = (Parameter)pb;

        foreach (string key in keys)
        {
            for (int j = 0; j < 9; j++)
            {
                int variance = r.Next(-maxVariance, maxVariance + 1);
                parameter.parameters[key][j] = Math.Clamp(
                    parameter.parameters[key][j] + variance, minValue, maxValue
                );
            }
        }

        return new Model(parameter);
    }



    protected override int Match(ModelBase mb0, ModelBase mb1)
    {
        Model model0 = (Model)mb0;
        Model model1 = (Model)mb1;

        int startingPlayer = r.Next(2);

        playfield = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < 9; i++)
        {
            if (i % 2 == startingPlayer)
            {
                int field = model0.Run(playfield, startingPlayer == 1);
                playfield[field] = 1;
            }
            else
            {
                int field = model1.Run(playfield, startingPlayer == 1);
                playfield[field] = 2;
            }

            int victory = CheckVictory();
            if (victory != 0) return victory - 1;
        }

        // tie, 50% chance for both models to win
        if (r.Next(2) == 0) return 0;
        return 1;
    }




    private int CheckVictory()
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
