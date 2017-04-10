using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher : ISearcher
    {
        //private FastPriorityQueue<State> openList;
        private int evaluatedNodes;
        public Searcher()
        {
            // openList = new FastPriorityQueue<State>();
            evaluatedNodes = 0;
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
        //public abstract Solution search(ISearchable searchable);
        //}

    }
}

