using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    class BFS<T> : ISearcher<T>
    {
        public override search(ISearchable<T> searchable)
        {
            Priority_Queue.SimplePriorityQueue<State<T>> openQueue = new Priority_Queue.SimplePriorityQueue<State<T>>();
            HashSet<State<T>> closedSet = new HashSet<State<T>>();
            State<T> initialState = searchable.getInitialState();
            while (openQueue.Count > 0)
            {
                State<T> currentState = openQueue.Dequeue();
                closedSet.Add(currentState);
                if (currentState.Equals(searchable.getGoalState()))
                    return backTrace(initialState,currentState);
                List<State<T>> succerssors = searchable.getAllPossibleStates(currentState);
                foreach (State<T> currentSuccessor in succerssors)
                {
                    if (!closedSet.Contains(currentSuccessor) && !openQueue.Contains(currentSuccessor))
                    {

                        openQueue.Enqueue(currentSuccessor,(float)currentSuccessor.Cost);
                        currentSuccessor.CameFrom = currentState;
                    }
                    else if (openQueue.Contains(currentSuccessor))
                    {
                        if (currentSuccessor.Cost + currentState.Cost < getSpecificElement(openQueue, currentSuccessor))
                        {
                            openQueue.UpdatePriority(currentSuccessor, (float)(currentSuccessor.Cost + currentState.Cost));
                        }
                    }
                }

            }
        }
        private Solution<T> backTrace(State<T>initialState,State<T> goalState)
        {
            Solution<T> solution = new Solution<T>();
            State<T> currentState = goalState;
            while (!currentState.Equals(initialState))
            {
                solution.insertNode(currentState);
                currentState = currentState.CameFrom;
            }
            return solution;
        }
        private float getSpecificElement(Priority_Queue.SimplePriorityQueue<State<T>> queue, State<T> element)
        {
            foreach(State<T> elm in queue)
            {
                if(elm.Equals(element))
                {
                    return (float)elm.Cost;
                }
            }
            return 0;
        }

    }


}
