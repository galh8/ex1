using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DFS<T> : Searcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            
            Stack<State<T>> nodesStack = new Stack<State<T>>();
            HashSet<State<T>> closedNodesSet = new HashSet<State<T>>();
            //increasing the counter
            evaluatedNodes++;

            //adding the intial State to the stack and the hashset.
            nodesStack.Push(searchable.getInitialState());
            closedNodesSet.Add(searchable.getInitialState());

            while (nodesStack.Count() != 0)
            {
                State<T> currentNode = nodesStack.Peek();
                List<State<T>> succerssors = searchable.getAllPossibleStates(currentNode);
                
                for (int i = 0; i < succerssors.Count; ++i)
                {
                    //increasing the counter
                    evaluatedNodes++;
                    if (!closedNodesSet.Contains(succerssors[i]))
                    {
                        nodesStack.Push(succerssors[i]);
                        closedNodesSet.Add(succerssors[i]);
                        if (succerssors[i].Equals(searchable.getGoalState())) {
                            return backTrace(nodesStack);
                        }
                        break;
                    }

                    //checking if all the succerssors already visited
                    if (i == succerssors.Count - 1)
                        nodesStack.Pop();
           
                }
            }

            return null;
       }


        private Solution<T> backTrace(Stack<State<T>> path)
        {
            Solution<T> solution = new Solution<T>();
            State<T> currentState = path.Pop();
            while (path.Count != 0 )
            {
                //currentState.CameFrom = path.Peek();
                solution.insertNode(currentState);
                currentState = path.Pop();
            }

            solution.insertNode(currentState);
            currentState = path.Pop();
            
            return solution;
        }


    }
}
