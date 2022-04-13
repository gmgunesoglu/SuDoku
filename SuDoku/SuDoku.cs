using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuDoku
{
    class Cell
    {
        public bool[] odds = new bool[10];
        public int value = 0;
    }
    class Block
    {
        public Cell[,] cell = new Cell[3, 3];
        public bool[] hasDigit = new bool[10];
    }
    class SuDoku
    {
        public Block[,] block = new Block[3, 3];
        public bool[,] X = new bool[9,10];
        public bool[,] Y = new bool[9,10]; //[8,6]=ture;
    }
}
