namespace aman.Models;

public abstract class ModelBase
{
    public ParameterBase parameter;

    public override string ToString()
    {
        return parameter.ToString();
    }
}
