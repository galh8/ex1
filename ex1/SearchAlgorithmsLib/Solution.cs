using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class Solution<T>
    {
        private List<State<T>> pathToGoal;
        
        public List<State<T>> PathToGoal
        {
            get; set; 
        }

        public override string ToString()
        {
            StringBuilder pathToReturn = new StringBuilder();
            foreach(State<T> node in pathToGoal)
            {
                pathToReturn.Append(node.ToString());
            }
            return pathToReturn.ToString();
        }

        public void insertNode(State<T> node)
        {
            pathToGoal.Add(node);
        }

        public int solutionLength()
        {
            return pathToGoal.Count();
        }
    }
}
