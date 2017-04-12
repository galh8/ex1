using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace ex1
{
    interface IController
    {
        void setModel(IModel m);
        void setView(IClientHandler v);
        void setSolution(Solution<T> s);
        void calculate(Problem p);
    }
}
