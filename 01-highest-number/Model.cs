using aman.Models;
using System.Text;

namespace aman._01_highest_number;

public class Model : ModelBase
{
    public Model(Parameter parameter)
    {
        this.parameter = parameter;
    }

    public float Run()
    {
        return ((Parameter)parameter).value;
    }
}
