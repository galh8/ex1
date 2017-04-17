using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
using MazeLib;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : PrioritySearcher<T>
    {

        public override Solution<T> search(ISearchable<T> searchable)
        {
            //Priority_Queue.SimplePriorityQueue<State<T>> openQueue = new Priority_Queue.SimplePriorityQueue<State<T>>();
            HashSet<State<T>> closedSet = new HashSet<State<T>>();
            State<T> initialState = searchable.getInitialState();
            addToOpenList(initialState);
            while (openList.Count > 0)
            {
                State<T> currentState = popOpenList();
                closedSet.Add(currentState);
                if (currentState.Equals(searchable.getGoalState()))
                    return backTrace(initialState,currentState);
                List<State<T>> succerssors = searchable.getAllPossibleStates(currentState);
                foreach (State<T> currentSuccessor in succerssors)
                {
                    if (!closedSet.Contains(currentSuccessor) && !openList.Contains(currentSuccessor))
                    {

                        openList.Enqueue(currentSuccessor,(float)(currentSuccessor.Cost + currentState.Cost));
                        currentSuccessor.CameFrom = currentState;
                    }
                    else if (openList.Contains(currentSuccessor))
                    {
                        if (currentSuccessor.Cost + currentState.Cost < getSpecificElementCost(openList, currentSuccessor))
                        {
                            openList.UpdatePriority(currentSuccessor, (float)(currentSuccessor.Cost + currentState.Cost));
                        }
                    }
                }

            }
            return null;
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
        private float getSpecificElementCost(Priority_Queue.SimplePriorityQueue<State<T>> queue, State<T> element)
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
