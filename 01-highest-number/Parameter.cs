using aman.Models;

namespace aman._01_highest_number;

public class Parameter : ParameterBase
{
    public float value;

    public Parameter(float value)
    {
        this.value = value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
