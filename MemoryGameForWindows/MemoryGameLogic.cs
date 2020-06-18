using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameForWindows
{
    internal class MemoryGameLogic
    {
        private const int k_QuantityFromEachObj = 2;
        private const int k_MinNumberRowInput = 4;
        private const int k_MinNumberColInput = 4;
        private const int k_MaxNumberRowInput = 6;
        private const int k_MaxNumberColInput = 6;
        private int m_LeftUndiscoveredObjs;
        private Dictionary<int, char> m_LettersDictionary;
        private Board m_BoardGame;
        private Random m_Rand = new Random();


        public MemoryGameLogic(int i_RowSize, int i_ColSize)
        {
            m_BoardGame = new Board(i_RowSize, i_ColSize);
        }
        private void initLettersDictionary()
        {
            m_LettersDictionary = new Dictionary<int, char>();
            for (int i = 0; i < m_BoardGame.NumOfCols * m_BoardGame.NumOfRows / 2; i++)
            {
                m_LettersDictionary.Add(i, (char)('A' + i));
            }
        }

        public static List<Point> GetPossibleBoardSize()
        {
            List<Point> possibleBoardSize = new List<Point>();
            int currCol, currRow = k_MinNumberRowInput;

            for (int i = k_MinNumberRowInput; i <= k_MaxNumberRowInput; i++)
            {
                currCol = k_MinNumberColInput;
                for (int j = k_MinNumberColInput; j <= k_MaxNumberColInput; j++)
                {
                    if (currRow * currCol % 2 == 0)
                    {
                        possibleBoardSize.Add(new Point(currRow, currCol));
                    }

                    currCol++;
                }

                currRow++;
            }

            return possibleBoardSize;
        }

        private void createMemoryBoardGame()
        {
            int rowSize, colSize, indexOfAvaliableSpotsInBoard = 0, indexForCurrRepNum = 0;
            int convertToIndexRowInBoard, convertToIndexColInBoard;
            List<int> indexesForCells, avaliableSpotsInBoard;

            rowSize = m_BoardGame.NumOfRows;
            colSize = m_BoardGame.NumOfCols;
            /// In a new board, every cell is covered.
            m_LeftUndiscoveredObjs = rowSize * colSize;
            indexesForCells = new List<int>(Enumerable.Range(0, rowSize * colSize / 2));
            avaliableSpotsInBoard = new List<int>(Enumerable.Range(0, rowSize * colSize));
            for (int i = 0; i < avaliableSpotsInBoard.Count; i++)
            {
                indexOfAvaliableSpotsInBoard = m_Rand.Next(i, avaliableSpotsInBoard.Count);
                convertToIndexRowInBoard = avaliableSpotsInBoard[indexOfAvaliableSpotsInBoard] / colSize;
                convertToIndexColInBoard = avaliableSpotsInBoard[indexOfAvaliableSpotsInBoard] % colSize;
                m_BoardGame.BoardGame[convertToIndexRowInBoard, convertToIndexColInBoard] = new Board.Cell(indexesForCells[indexForCurrRepNum]);
                swap(avaliableSpotsInBoard, i, indexOfAvaliableSpotsInBoard);
                /// for each object there are two cards. 
                if (i % 2 != 0)
                {
                    indexForCurrRepNum++;
                }
            }

            initLettersDictionary();
        }

        private void swap<T>(List<T> io_List, int i_Index1, int i_Index2)
        {
            T temp = io_List[i_Index1];
            io_List[i_Index1] = io_List[i_Index2];
            io_List[i_Index2] = temp;
        }

        private void decrementUndiscoveredCells()
        {
            m_LeftUndiscoveredObjs -= k_QuantityFromEachObj;
        }

        public void PlayerChooseCell(int i_ChoosenRow, int i_ChooseCol)
        {
            if(!m_BoardGame.BoardGame[i_ChoosenRow, i_ChooseCol].IsAvailable)
            {
                throw new Exception("You choosed a cell that was already uncovered! Please choose another cell.");
            }

            m_BoardGame.MakeCellUnAvailable(i_ChoosenRow, i_ChooseCol);
        }

        public void ComputerChooseCell(List<int> i_UnDiscoveredCells, out int o_ChoosenRow, out int o_ChooseCol)
        {
            int randCell = m_Rand.Next(0, i_UnDiscoveredCells.Count);

            o_ChoosenRow = i_UnDiscoveredCells[randCell] / m_BoardGame.NumOfCols;
            o_ChooseCol = i_UnDiscoveredCells[randCell] % m_BoardGame.NumOfCols;
            m_BoardGame.MakeCellUnAvailable(o_ChoosenRow, o_ChooseCol);
        }
    }
}
