using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aman._01_highest_number
{
    public class Model
    {
        public float parameter;

        public Model(float parameter)
        {
            this.parameter = parameter;
        }

        public float Run()
        {
            return parameter;
        }
    }
}
