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
        public event Action<User> SwitchTurn;
        public event Action FoundPair;
        public event Action<string> EndGame;
        public event Action<Point> ComputerMove;
        public event Action<Point> ComputerNotFoundPair;
        private const int k_QuantityFromEachObj = 2;
        private const int k_MinNumberRowInput = 2;
        private const int k_MinNumberColInput = 3;
        private const int k_MaxNumberRowInput = 6;
        private const int k_MaxNumberColInput = 6;
        private int m_LeftUndiscoveredObjs;
        List<int> m_UnDiscoveredCells;
        private Dictionary<int, string> m_ImagesDictionary;
        private Board m_BoardGame;
        private Random m_Rand;
        private User m_CurrPlayer;
        private User m_FirstPlayer;
        private User m_SecondPlayer;
        private Point m_FirstChoose;
        private Point m_SecondChoose;
        private bool m_IsGameEnded = false;

        public MemoryGameLogic(int i_RowSize, int i_ColSize, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_Rand = new Random();
            m_BoardGame = new Board(i_RowSize, i_ColSize);
            m_FirstPlayer = new User(i_FirstPlayerName);
            m_SecondPlayer = new User(i_SecondPlayerName);
            m_CurrPlayer = m_FirstPlayer;
            createMemoryBoardGame();
        }

        public int LeftUnDiscovered
        {
            get
            {
                return m_LeftUndiscoveredObjs;
            }
        }

        public bool IsGameEnded
        {
            get
            {
                return m_IsGameEnded;
            }
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

        public void StartGameAgainsComputer()
        {
            m_UnDiscoveredCells = new List<int>(Enumerable.Range(0, m_BoardGame.NumOfRows * m_BoardGame.NumOfCols));
        }

        public void MakeComputerMove()
        {
            bool isFirstChoose = true;
            int convertToUnDiscoveredCellIndex;
            //int convertToUnDiscoveredCellIndex;
            do
            {
                if(m_UnDiscoveredCells.Count ==0)
                {
                    break;
                }
                ComputerChooseCell(isFirstChoose);
               // System.Threading.Thread.Sleep(1000);
                ComputerChooseCell(!isFirstChoose);
                //System.Threading.Thread.Sleep(1000);

                //  if (IsSameObject(m_CurrPlayer, m_FirstChoose, m_SecondChoose))
                // {
                //if (m_BoardGame.IsSameObject(m_FirstChoose.X, m_FirstChoose.Y, m_SecondChoose.X, m_SecondChoose.Y))
                //{
                //    convertToUnDiscoveredCellIndex = (m_FirstChoose.X * m_BoardGame.NumOfCols) + m_FirstChoose.Y;
                //    m_UnDiscoveredCells.Remove(convertToUnDiscoveredCellIndex);
                //    convertToUnDiscoveredCellIndex = (m_SecondChoose.X * m_BoardGame.NumOfCols) + m_SecondChoose.Y;
                //    m_UnDiscoveredCells.Remove(convertToUnDiscoveredCellIndex);
                //    m_CurrPlayer.IncrementPoints();
                //    m_BoardGame.MakeCellUnAvailable(m_FirstChoose.X, m_FirstChoose.Y);
                //    m_BoardGame.MakeCellUnAvailable(m_SecondChoose.X,m_SecondChoose.Y);
                //    decrementUndiscoveredCells();

                //    if (m_LeftUndiscoveredObjs == 0)
                //    {
                //        OnEndGame();
                //    }

                //MakeComputerMove();
                //  }
            }
            while (IsSameObject(m_CurrPlayer, m_FirstChoose, m_SecondChoose));
            //else
            //{
            ComputerNotFoundPair.Invoke(m_FirstChoose);
            ComputerNotFoundPair.Invoke(m_SecondChoose);
            convertToUnDiscoveredCellIndex = (m_FirstChoose.X * m_BoardGame.NumOfCols) + m_FirstChoose.Y;
            m_UnDiscoveredCells.Add(convertToUnDiscoveredCellIndex);
            OnSwitchTurns();
            //}


        }

        public bool IsSameObject(User i_CurrPlayer, Point i_FirstChoose, Point i_SecondChoose)
        {
            int convertToUnDiscoveredCellIndex;
            bool isSameObject = false;
            m_FirstChoose = i_FirstChoose;
            m_SecondChoose = i_SecondChoose;

            if (m_BoardGame.IsSameObject(i_FirstChoose.X, i_FirstChoose.Y, i_SecondChoose.X, i_SecondChoose.Y))
            {
                i_CurrPlayer.IncrementPoints();
                m_BoardGame.MakeCellUnAvailable(i_FirstChoose.X, i_FirstChoose.Y);
                m_BoardGame.MakeCellUnAvailable(i_SecondChoose.X, i_SecondChoose.Y);
                FoundPair.Invoke();
                decrementUndiscoveredCells();
                if (m_UnDiscoveredCells != null)
                {
                    convertToUnDiscoveredCellIndex = (m_FirstChoose.X * m_BoardGame.NumOfCols) + m_FirstChoose.Y;
                    m_UnDiscoveredCells.Remove(convertToUnDiscoveredCellIndex);
                    convertToUnDiscoveredCellIndex = (m_SecondChoose.X * m_BoardGame.NumOfCols) + m_SecondChoose.Y;
                    m_UnDiscoveredCells.Remove(convertToUnDiscoveredCellIndex);
                }

                if (m_LeftUndiscoveredObjs == 0)
                {
                    m_IsGameEnded = true;
                    OnEndGame();
                }
                
                    isSameObject = true;
                

            }
            //else
            //{
            //    OnSwitchTurns();
            //}

            return isSameObject;
        }

        private void OnEndGame()
        {
            StringBuilder msg = new StringBuilder();
            User winner, loser;
            if (m_FirstPlayer.Points != m_SecondPlayer.Points)
            {
                winner = m_FirstPlayer.Points > m_SecondPlayer.Points ? m_FirstPlayer : m_SecondPlayer;
                loser = m_FirstPlayer == winner ? m_SecondPlayer : m_FirstPlayer;
                msg.Append(string.Format(
@"The winner is {0}!.
{0} has {1} points while {2} has {3} points{4}",
                                            winner.Name,
                                            winner.Points,
                                            loser.Name,
                                            loser.Points,
                                            Environment.NewLine));
            }
            else
            {
                msg.Append(string.Format("It's a tie! both players got {0} points!{1}", m_SecondPlayer.Points, Environment.NewLine));
            }
            msg.Append("Would you like to start a new game?");
            EndGame(msg.ToString());
        }

        public void OnSwitchTurns()
        {
            if (m_CurrPlayer == m_FirstPlayer)
            {
                m_CurrPlayer = m_SecondPlayer;
            }
            else
            {
                m_CurrPlayer = m_FirstPlayer;
            }
            SwitchTurn(m_CurrPlayer);
            //if (m_CurrPlayer.Name == "- computer -")
            //{
            //    MakeComputerMove();
            //}

        }

        private void swapValue<T>(ref T i_Index1, ref T i_Index2)
        {
            T temp = i_Index1;
            i_Index1 = i_Index2;
            i_Index2 = temp;
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

        public Dictionary<int, string> LetterDictionary
        {
            get
            {
                return m_ImagesDictionary;
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

        private void initImagesDictionary()
        {
            m_ImagesDictionary = new Dictionary<int, string>();
            int randomImageID;
            string beginURL = "https://picsum.photos/id/";
            string endURL = "/80/80";
            StringBuilder fullURL;
            for (int i = 0; i < m_BoardGame.NumOfCols * m_BoardGame.NumOfRows / 2; i++)
            {
                fullURL = new StringBuilder();
                fullURL.Append(beginURL);
                randomImageID = m_Rand.Next(1, 999);
                fullURL.Append(randomImageID.ToString());
                fullURL.Append(endURL);
                m_ImagesDictionary.Add(i, fullURL.ToString());
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

            initImagesDictionary();
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

        public void PlayerFirstChoose(int i_ChoosenRow, int i_ChooseCol)
        {
            if(!m_BoardGame.BoardGame[i_ChoosenRow, i_ChooseCol].IsAvailable)
            {
                throw new Exception("You choosed a cell that was already uncovered! Please choose another cell.");
            }

            m_BoardGame.MakeCellUnAvailable(i_ChoosenRow, i_ChooseCol);
        }

        public void ComputerChooseCell(bool i_IsFirstChoose)
        {
            int randCell = m_Rand.Next(0, m_UnDiscoveredCells.Count);
            int choosenRow = m_UnDiscoveredCells[randCell] / m_BoardGame.NumOfCols;
            int chooseCol = m_UnDiscoveredCells[randCell] % m_BoardGame.NumOfCols;
            int convertToUnDiscoveredCellIndex;
            Point choose = new Point(choosenRow, chooseCol);

            if (i_IsFirstChoose)
            {
                m_FirstChoose = choose;
                convertToUnDiscoveredCellIndex = (choosenRow * m_BoardGame.NumOfCols) + chooseCol;
                m_UnDiscoveredCells.Remove(convertToUnDiscoveredCellIndex);
            }
            else
            {
                m_SecondChoose = choose;
            }

            ComputerMove.Invoke(choose);

            //m_BoardGame.MakeCellUnAvailable(choosenRow, chooseCol);
        }
    }
}
