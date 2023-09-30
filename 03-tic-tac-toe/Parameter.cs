using aman.Models;
using System.Text;

namespace aman._03_tic_tac_toe;

public class Parameter : ParameterBase
{
    public Dictionary<string, int[]> parameters;

    public Parameter(Dictionary<string, int[]> parameters)
    {
        this.parameters = parameters;
    }

    public override string ToString()
    {
        int[] weights = parameters["000000000"];

        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < 9; i++)
        {
            stringBuilder.Append($" {weights[i]}");
        }
        return stringBuilder.ToString();
    }
}
