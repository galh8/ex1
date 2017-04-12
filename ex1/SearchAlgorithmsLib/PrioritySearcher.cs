using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        protected SimplePriorityQueue<State<T>> openList;
        private int evaluatedNodes;
        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }



        public override abstract Solution<T> search(ISearchable<T> searchable);

        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        
        }

        protected void addToOpenList(State<T> stateToAdd)
        {
            openList.Enqueue(stateToAdd,(float)stateToAdd.Cost);
        }

        public int OpenListSize
          {
             get { return openList.Count; }
          }





    }
}

