using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameForWindows
{
    public class Board
    {
        private readonly int r_Rows;
        private readonly int r_Cols;
        private readonly Cell[,] r_BoardGame;
        

        public Board(int i_NumOfRows, int i_NumOfCols) // c'tor
        {
            r_Rows = i_NumOfRows;
            r_Cols = i_NumOfCols;
            r_BoardGame = new Cell[i_NumOfRows, i_NumOfCols];
        }

        /// public properties
        public int NumOfRows
        {
            get
            {
                return r_Rows;
            }
        }

        public int NumOfCols
        {
            get
            {
                return r_Cols;
            }
        }

        public Cell[,] BoardGame
        {
            get
            {
                return r_BoardGame;
            }
        }

        public void MakeCellUnAvailable(int i_ChosenRow, int i_ChosenCol)
        {
            r_BoardGame[i_ChosenRow, i_ChosenCol].IsAvailable = false;
        }

        public void MakeCellAvailable(int i_ChosenRow, int i_ChosenCol)
        {
            r_BoardGame[i_ChosenRow, i_ChosenCol].IsAvailable = true;
        }

        public bool IsSameObject(int i_FirstChosenRow, int i_FirstChosenCol, int i_SecondChosenRow, int i_SecondChosenCol)
        {
            return r_BoardGame[i_FirstChosenRow, i_FirstChosenCol].RepresentIndexForObj == r_BoardGame[i_SecondChosenRow, i_SecondChosenCol].RepresentIndexForObj;
        }

        public class Cell
        {
            private bool m_IsAvailable;
            private int m_RepresentIndexForObj;

            public Cell(int i_RepresentIndexForObj) // c'tor
            {
                m_IsAvailable = true;
                m_RepresentIndexForObj = i_RepresentIndexForObj;
            }

            /// public properties
            public bool IsAvailable
            {
                get
                {
                    return m_IsAvailable;
                }

                set
                {
                    m_IsAvailable = value;
                }
            }

            public int RepresentIndexForObj
            {
                get
                {
                    return m_RepresentIndexForObj;
                }

                set
                {
                    m_RepresentIndexForObj = value;
                }
            }
        }
    }
}
