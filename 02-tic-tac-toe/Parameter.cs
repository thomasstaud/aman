using aman.Models;
using System.Text;

namespace aman._02_tic_tac_toe;

public class Parameter : ParameterBase
{
    public int[][] parameters;

    public Parameter(int[][] parameters)
    {
        this.parameters = parameters;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < 9; i++)
        {
            stringBuilder.Append("\n{");
            for (int j = 0; j < 9; j++)
            {

                stringBuilder.Append($" {parameters[i][j]}");
            }
            stringBuilder.Append(" }");
        }

        return stringBuilder.ToString();
    }
}
