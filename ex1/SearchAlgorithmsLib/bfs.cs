using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class Bfs : ISearchable
    {

    }

    public override Bfs search(ISearchable searchable)
    {
        addToOpenList(searchable.getIntialState());
        HashSet<State> closed = new HashSet<State>();
        while (OpenListsSize > 0) {
            State n = popOpenList();
            closed.Add(n);
            if (n.Equals(searchable.getIGoalLState()))
                return backTrace();
            List<State> succerssors = searchable.getAllPossibleStates(n);
            foreach (State s in succerssors)
            {
                if (!closed.Contains(s) && !openContainers(s))
                {
                    addToOpenList(s);
                }
                else
                {

                }
            }

        }
    }
}
