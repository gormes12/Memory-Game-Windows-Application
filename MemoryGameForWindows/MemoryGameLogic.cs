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
        public event Action<Point, Point> FoundPair;
        private const int k_QuantityFromEachObj = 2;
        private const int k_MinNumberRowInput = 4;
        private const int k_MinNumberColInput = 4;
        private const int k_MaxNumberRowInput = 6;
        private const int k_MaxNumberColInput = 6;
        private int m_LeftUndiscoveredObjs;
        private Dictionary<int, char> m_LettersDictionary;
        private Board m_BoardGame;
        private Random m_Rand = new Random();
        private User m_FirstPlayer;
        private User m_SecondPlayer;
        private User m_CurrPlayer;

        public MemoryGameLogic(int i_RowSize, int i_ColSize, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_BoardGame = new Board(i_RowSize, i_ColSize);
            m_FirstPlayer = new User(i_FirstPlayerName);
            m_SecondPlayer = new User(i_SecondPlayerName);
            m_CurrPlayer = m_FirstPlayer;
            createMemoryBoardGame();
        }

        public uint Player1Score
        {
            get
            {
                return m_FirstPlayer.Points;
            }
        }

        public uint Player2Score
        {
            get
            {
                return m_SecondPlayer.Points;
            }
        }

        public User CurrPlayer
        {
            get
            {
                return m_CurrPlayer;
            }
        }

        public void startGameAgainsOtherPlayer()
        {
            User currPlayer = m_FirstPlayer;
            while(m_LeftUndiscoveredObjs > 0)
            {

            }
        }

        public bool IsSameObject(User i_CurrPlayer, Point i_FirstChoose, Point i_SecondChoose )
        {
            bool isSameObject = false;
            if(m_BoardGame.IsSameObject(i_FirstChoose.X,i_FirstChoose.Y,i_SecondChoose.X,i_SecondChoose.Y))
            {
                i_CurrPlayer.IncrementPoints();
                m_BoardGame.MakeCellUnAvailable(i_FirstChoose.X, i_FirstChoose.Y);
                m_BoardGame.MakeCellUnAvailable(i_SecondChoose.X, i_SecondChoose.Y);
                FoundPair.Invoke(i_FirstChoose,i_SecondChoose);
                isSameObject = true;
            }
            return isSameObject;
        }

        public string FirstPlayerName
        {
            get
            {
                return m_FirstPlayer.Name;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return m_SecondPlayer.Name;
            }
        }

        public Dictionary<int, char> LetterDictionary
        {
            get
            {
                return m_LettersDictionary;
            }
        }

        public Board BoardGame
        {
            get
            {
                return m_BoardGame;
            }
        }

        public int BoardRows
        {
            get
            {
                return m_BoardGame.NumOfRows;
            }
        }

        public int BoardCols
        {
            get
            {
                return m_BoardGame.NumOfCols;
            }
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
