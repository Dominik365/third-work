using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q8_2048
{
    class LogicGrid
    {
        private List<List<int>> logicGrid;
        private int size;
        public LogicGrid(int size)
        {
            this.size = size;
            logicGrid = new List<List<int>>();
            
            for(int i = 0; i < size; i++)
            {
                logicGrid.Add(new List<int>());
                for(int j = 0; j < size; j++)
                {
                    logicGrid[i].Add(0);

                }
            }
        }
        public void SwipeLeft()
        {

            for (int row = 0; row < size; row++)
            {
                logicGrid[row].RemoveAll(x => x == 0);
                for(int col = 0; col < logicGrid[row].Count - 1; col++)
                {
                    if(logicGrid[row][col] == logicGrid[row][col + 1])
                    {
                        logicGrid[row][col] *= 2;
                        logicGrid[row].RemoveAt(col + 1);
                    }

                }
                while(logicGrid[row].Count < size)
                {
                    logicGrid[row].Add(0);
                }
            }

            }
        
        public void SwipeRight()
        {
            for (int row = 0; row < size; row++)
            {
                logicGrid[row].RemoveAll(x => x == 0);
                logicGrid[row].Reverse();
                for (int col = 0; col < logicGrid[row].Count - 1; col++)
                {
                    if (logicGrid[row][col] == logicGrid[row][col + 1])
                    {
                        logicGrid[row][col] *= 2;
                        logicGrid[row].RemoveAt(col + 1);
                    }

                }
                while (logicGrid[row].Count < size)
                {
                    logicGrid[row].Add(0);
                }
                logicGrid[row].Reverse();
            }
        }
        public void SwipeUp()
        {
            for(int col = 0; col < size; col++)
            {
                List<int> temp = new List<int>();
                for(int row = 0; row < size; row++)
                {
                    if(logicGrid[row][col] != 0)
                    {
                        temp.Add(logicGrid[row][col]);
                    }
                }
                for(int row = 0; row < temp.Count - 1; row++)
                {
                    if(temp[row] == temp[row + 1])
                    {
                        temp[row] *= 2;
                        temp.RemoveAt(row + 1);
                    }
                }
                while(temp.Count < size)
                {
                    temp.Add(0);
                }
                for(int row = 0; row < size; row++)
                {
                    logicGrid[row][col] = temp[row];
                }
            }    
        }
        public void SwipeDown()
        {
            for(int col = 0; col < size; col++)
            {
                List<int> temp = new List<int>();
                for(int row = size - 1; row >= 0; row--)
                {
                    if(logicGrid[row][col] != 0)
                    {
                        temp.Add(logicGrid[row][col]);
                    }
                }
                for(int row = 0; row < temp.Count - 1; row++)
                {
                    if(temp[row] == temp[row + 1])
                    {
                        temp[row] *= 2;
                        temp.RemoveAt(row + 1);
                    }
                }
                while(temp.Count < size)
                {
                    temp.Add(0);
                }
                temp.Reverse();
                for(int row = 0; row < size; row++)
                {
                    logicGrid[row][col] = temp[row];
                }
            }
        }
      
       
        public List<List<int>> GetGrid()
        {
            return logicGrid;
        }
        public void AddBlockToRandom()
        {
            List<int> freeIndexes = GetFreeIndexes();
            Random rnd = new Random();
            int rIndex = freeIndexes[rnd.Next(0, freeIndexes.Count)];
            logicGrid[rIndex / size][rIndex % size] = 2;
        }
        public List<int> GetFreeIndexes()
        {
            List<int> freeIndexes = new List<int>();
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (logicGrid[row][col] == 0)
                    {
                        freeIndexes.Add(row * size + col);
                    }
                }
            }
            return freeIndexes;
        }
        public bool HasAdjacentValues()
        {
            int rows = logicGrid.Count;
            int columns = logicGrid[0].Count;

            // Check for adjacent equal integers in rows
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns - 1; col++)
                {
                    if (logicGrid[row][col] == logicGrid[row][col + 1])
                    {
                        return true; // Found adjacent equal integers in a row
                    }
                }
            }

            // Check for adjacent equal integers in columns
            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows - 1; row++)
                {
                    if (logicGrid[row][col] == logicGrid[row + 1][col])
                    {
                        return true; // Found adjacent equal integers in a column
                    }
                }
            }

            return false; // No adjacent equal integers found
        }
        public bool Contains2048()
        {
            foreach (List<int> row in logicGrid)
            {
                foreach (int element in row)
                {
                    if (element == 2048)
                    {
                        return true; // Found 2048 in the matrix
                    }
                }
            }

            return false; // 2048 not found in the matrix
        }

    }
}
