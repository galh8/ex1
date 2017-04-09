using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State
    {
        private string state;
        private double cost;
        private State cameFrom;

        public State(string state)
        {
            this.state = state;
        }

        public override bool Equals(object obj)
        {
            return state.Equals((obj as State).state);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
