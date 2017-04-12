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

        public override string ToString()
        {
            return state.ToString();
        }

        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }

        public override int GetHashCode()
        {
            return state.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj!=null) 
             return state.Equals((obj as State<T>).state);
            return false;
        }

        public double Cost
        {
            get; set;

        }

        public State<T> CameFrom
        {
            get; set;

        }

    }
}
