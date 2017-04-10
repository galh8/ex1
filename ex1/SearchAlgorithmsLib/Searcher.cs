using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        //private FastPriorityQueue<State> openList;
        private int evaluatedNodes;
        public Searcher()
        {
            // openList = new FastPriorityQueue<State>();
            evaluatedNodes = 0;
        }

        public int getNumberOfNodesEvaluated()
        {
            //throw new NotImplementedException();
            return 0;
        }

        //protected State popOpenList()
        //{
        //evaluatedNodes++;
        //  return openList.poll();
        //}
        //public int OpenListSize
        //{
        //get { return openList.Count; }
        //}
        //public int getNumberOfNodeEvaluated()
        //{
        //   return evaluatedNodes;
        //}
        public abstract Solution<T> search(ISearchable<T> searchable);
        

    }
}

