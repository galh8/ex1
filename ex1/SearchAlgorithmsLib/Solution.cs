using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class Solution<T>
    {
        private List<T> pathToGoal;
        
        public List<T> PathToGoal
        {
            get; set; 
        }

        public override string ToString()
        {
            StringBuilder pathToReturn = new StringBuilder();
            foreach(T node in pathToGoal)
            {
                pathToReturn.Append(node.ToString());
            }
            return pathToReturn.ToString();
        }

        public void insertNode(T node)
        {
            pathToGoal.Add(node);
        }

        public int solutionLength()
        {
            return pathToGoal.Count();
        }
    }
}
