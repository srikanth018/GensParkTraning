using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    public class ValidateAllRows
    {
        private int[,] board = new int[9, 9]
        {
            {5,3,4,6,7,8,9,1,2},
            {6,7,2,1,9,5,3,4,8}, 
            {1,9,8,3,4,2,5,6,7},
            {8,5,9,7,6,1,4,2,3},
            {4,2,6,8,5,3,7,9,1},
            {7,1,3,9,2,4,8,5,6},
            {9,6,1,5,3,7,2,8,4},
            {2,8,7,4,1,9,6,3,5},
            {3,4,5,2,8,6,1,7,9}
        };


        private int[] getData(object row, object col, object gridStart = null)
        {
            if (row != null)
            {
                int[] rowArr = new int[9];
                for (int i = 0; i < 9; i++)
                {
                    rowArr[i] = board[(int)row, i];
                }
                return rowArr;
            }
            else if (col != null)
            {
                int[] colArr = new int[9];
                for (int i = 0; i < 9; i++)
                {
                    colArr[i] = board[i, (int)col];
                }
                return colArr;
            }
            else if (gridStart != null)
            {
                int[] gridArr = new int[9];
                (int startRow, int startCol) = ((int, int))gridStart;
                int index = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        gridArr[index++] = board[startRow + i, startCol + j];
                    }
                }
                return gridArr;
            }

            return null;
        }

        private bool IsValidRow(int[] arr)
        {
            ValidRow validRow = new ValidRow();
            return validRow.isValid(arr);
        }

        public void ValidateRows()
        {
            for (int i = 0; i < 9; i++)
            {
                int[] row = getData(i, null);
                if (!IsValidRow(row))
                {
                    Console.WriteLine($"Row {i + 1} is invalid");
                    return;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                int[] column = getData(null, i);
                if (!IsValidRow(column))
                {
                    Console.WriteLine($"Column {i + 1} is invalid");
                    return;
                }
            }

            for (int row = 0; row < 9; row += 3)
            {
                for (int col = 0; col < 9; col += 3)
                {
                    int[] grid = getData(null, null, (row, col));
                    if (!IsValidRow(grid))
                    {
                        Console.WriteLine($"3x3 Grid starting at ({row + 1},{col + 1}) is invalid");
                        return;
                    }
                }
            }

            Console.WriteLine("The Sudoku board is valid!");
        }
    }
}