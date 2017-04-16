using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServerProject
{
    class PositionSolution<Position> : Solution<Position>
    {
        public override string ToString()
        {
            StringBuilder pathToReturn = new StringBuilder();
            foreach (State<Position> position in pathToGoal)
            {
                //if we came from the same row
                if (position.stateInstance == position.CameFrom.stateInstance.Row)
                {
                    if (position.stateInstance.Col > position.CameFrom.stateInstance.Col)
                    {
                        pathToReturn.Append("1");
                    }
                    else
                    {
                        pathToReturn.Append("0");
                    }
                }
                //if we came from the same col
                else
                {
                    if (position.stateInstance.Row > position.CameFrom.stateInstance.Row)
                    {
                        pathToReturn.Append("3");
                    }
                    else
                    {
                        pathToReturn.Append("2");
                    }
                }
                
            }
            return pathToReturn.ToString();
        }

    }
}
