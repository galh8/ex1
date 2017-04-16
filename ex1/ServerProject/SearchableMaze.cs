using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;


namespace ServerProject
{
    class SearchableMaze : ISearchable<Position>
    {
        private Maze maze;
        private State<Position> initialState;
        private State<Position> goalState;
        private Dictionary<State<Position>,List<State<Position>>> adjacencyList; // Dictionary between state and all his successors.

        public SearchableMaze(Maze maze)
        {
            this.maze = maze;
            adjacencyList = new Dictionary<State<Position>, List<State<Position>>>();
            initializeSuccessors();
            this.initialState = new State<Position>(maze.InitialPos);
            this.goalState = new State<Position>(maze.GoalPos);
            
        }

        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            return this.adjacencyList[s];
        }

        public State<Position> getGoalState()
        {
            return this.goalState;
        }

        public State<Position> getInitialState()
        {
            return this.initialState;
        }

        //Method to initialize all the successors of each state.
        private void initializeSuccessors()
        {
            for(int row = 0;row < maze.Rows; ++row)
            {
                for(int col = 0;col < maze.Cols; ++col)
                {
                    List<State<Position>> successorsList = new List<State<Position>>();
                    State<Position> currentState = new State<Position>(new Position(row, col));
                    //Left succesor
                    if (isAccessable(row, col - 1)) {
                        State<Position> leftSuccessor = new State<Position>(new Position(row, col - 1));
                        leftSuccessor.Cost = 1;
                        successorsList.Add(leftSuccessor);
                    }
                    //Right succesor
                    if (isAccessable(row, col + 1))
                    {
                        State<Position> rightSuccessor = new State<Position>(new Position(row, col + 1));
                        rightSuccessor.Cost = 1;
                        successorsList.Add(rightSuccessor);
                    }
                    //Up succesor
                    if (isAccessable(row + 1, col))
                    {
                        State<Position> upSuccessor = new State<Position>(new Position(row + 1, col));
                        upSuccessor.Cost = 1;
                        successorsList.Add(upSuccessor);
                    }
                    //Down succesor
                    if (isAccessable(row - 1, col))
                    {
                        State<Position> downSuccessor = new State<Position>(new Position(row - 1, col));
                        downSuccessor.Cost = 1;
                        successorsList.Add(downSuccessor);
                    }
                    adjacencyList.Add(currentState, successorsList);
                }
            }
        }

        private bool isAccessable(int row,int col)
        {
            if(row >= maze.Rows || row < 0 )
                return false;
            if (col >= maze.Cols || col < 0)
                return false;
            if (maze[row, col] == CellType.Wall)
                return false;
            return true;
        }
    }
}
