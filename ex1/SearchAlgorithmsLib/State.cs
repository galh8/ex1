using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private T state;
        private double cost;
        private State<T> cameFrom;

        public State(T state)
        {
            this.state = state;
        }

        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
