using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGameForWindows
{
    public partial class GameForm : Form
    {
        private bool m_IsFirstClick;
        private MemoryGameLogic m_GameLogic;
        private SettingForm m_SettingForm;
        private MemoryBoardButton[,] m_BoardButtons;
        private Point m_FirstBoardClick;
        private User m_CurrUser;

        public GameForm()
        {
            InitializeComponent();
            m_SettingForm = new SettingForm();
            if(m_SettingForm.ShowDialog() == DialogResult.OK)
            {
                m_IsFirstClick = true;
                Point boardSize = m_SettingForm.BoardSize;
                m_GameLogic = new MemoryGameLogic(
                                                 boardSize.X,
                                                 boardSize.Y,
                                                 m_SettingForm.FirstPlayerName,
                                                 m_SettingForm.SecondPlayerName);
                m_BoardButtons = new MemoryBoardButton[boardSize.X, boardSize.Y];
                m_GameLogic.SwitchTurn += M_GameLogic_SwitchTurn;
                m_GameLogic.FoundPair += M_GameLogic_FoundPair;
                m_GameLogic.EndGame += M_GameLogic_EndGame;
                initializeBoard();
                initializeLebels();
                startGame();
            }
            else
            {
                this.Close();
            }
        }

        private void restartGame()
        {
            Point boardSize = m_SettingForm.BoardSize;
            m_GameLogic = new MemoryGameLogic(
                                             boardSize.X,
                                             boardSize.Y,
                                             m_SettingForm.FirstPlayerName,
                                             m_SettingForm.SecondPlayerName);                                    
            m_GameLogic.SwitchTurn += M_GameLogic_SwitchTurn;
            m_GameLogic.FoundPair += M_GameLogic_FoundPair;
            m_GameLogic.EndGame += M_GameLogic_EndGame;
            for (int i = 0; i < m_GameLogic.BoardRows; i++)
            {
                for (int j = 0; j < m_GameLogic.BoardCols; j++)
                {
                    m_BoardButtons[i, j].BoardButtonClicked -= buttonBoard_Clicked;
                }
            }
            initializeBoard();
            initializeLebels();
            startGame();
        }

        private void M_GameLogic_EndGame(string i_EndGameMessage)
        {
            if(MessageBox.Show(i_EndGameMessage, "Game Over",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                restartGame();
            }
            else
            {
                this.Close();
            }
        }

        private void M_GameLogic_FoundPair()
        {
            if(m_CurrUser.Name == m_SettingForm.FirstPlayerName)
            {
                labelFirstPlayerRes.Text = string.Format("{0}: {1} pairs", m_GameLogic.FirstPlayerName, m_CurrUser.Points);
            }
            else
            {
                labelSecondPlayerRes.Text = string.Format("{0}: {1} pairs", m_GameLogic.SecondPlayerName, m_CurrUser.Points);
            }
        }

        private void M_GameLogic_SwitchTurn(User i_CurrUser)
        {
            switchTurns(i_CurrUser);
        }

        private void startGame()
        {
            m_CurrUser = m_GameLogic.CurrPlayer;
            labelCurrentPlayer.Text = string.Format("Current Player: {0}", m_CurrUser.Name);
            labelCurrentPlayer.BackColor = m_SettingForm.FirstPlayerName == m_CurrUser.Name ?
                labelFirstPlayerRes.BackColor : labelSecondPlayerRes.BackColor;

            if (m_SettingForm.Opponent == eOpponent.Computer)
            {
                m_GameLogic.StartGameAgainsComputer();
                m_GameLogic.ComputerMove += M_GameLogic_ComputerMove;
                m_GameLogic.ComputerNotFoundPair += M_GameLogic_ComputerNotFoundPair;
            }
            //else
            // {
            //     startGameAgainstOtherPlayer();
            // }
        }

        private void M_GameLogic_ComputerNotFoundPair(Point i_ComputerMove)
        {
            m_BoardButtons[i_ComputerMove.X, i_ComputerMove.Y].Text = "";
            m_BoardButtons[i_ComputerMove.X, i_ComputerMove.Y].BackColor = DefaultBackColor;
            m_BoardButtons[i_ComputerMove.X, i_ComputerMove.Y].Enabled = true;
        }

        private void M_GameLogic_ComputerMove(Point i_ComputerMove)
        {
            makeVisibleAndUnClickable(i_ComputerMove.X, i_ComputerMove.Y);
        }

        //private void startGameAgainstOtherPlayer()
        //{
        //    m_CurrUser = m_GameLogic.CurrPlayer;
        //    labelCurrentPlayer.Text = string.Format("Current Player: {0}", m_CurrUser.Name);
        //    labelCurrentPlayer.BackColor = m_SettingForm.FirstPlayerName == m_CurrUser.Name ?
        //        labelFirstPlayerRes.BackColor : labelSecondPlayerRes.BackColor;
        //}

        private void switchTurns(User i_CurrUser)
        {
            m_CurrUser = i_CurrUser;
            labelCurrentPlayer.Text = string.Format("Current Player: {0}", m_CurrUser.Name);
            labelCurrentPlayer.BackColor = m_SettingForm.FirstPlayerName == m_CurrUser.Name ?
                labelFirstPlayerRes.BackColor : labelSecondPlayerRes.BackColor;
        }

        //private void startGameAgainsComputer()
        //{
            
        //}

        private void initializeLebels()
        {
            labelCurrentPlayer.Top = m_BoardButtons[m_GameLogic.BoardRows - 1, 0].Bottom + 10;
            labelFirstPlayerRes.Top = labelCurrentPlayer.Bottom + 10;
            labelFirstPlayerRes.Text =string.Format("{0}: {1} pairs", m_GameLogic.FirstPlayerName,m_GameLogic.Player1Score);
            labelSecondPlayerRes.Top = labelFirstPlayerRes.Bottom + 10;
            labelSecondPlayerRes.Text = string.Format("{0}: {1} pairs", m_GameLogic.SecondPlayerName, m_GameLogic.Player2Score);
            this.ClientSize = new Size(m_BoardButtons[m_GameLogic.BoardRows - 1, m_GameLogic.BoardCols - 1].Right + 10, labelSecondPlayerRes.Top + labelSecondPlayerRes.Height + 3);
        }


        private void initializeBoard()
        {
            char currChar;
            int boardRow = m_GameLogic.BoardRows;
            int boardCol = m_GameLogic.BoardCols;
            Board boardGame = m_GameLogic.BoardGame;
            Dictionary<int, char> letterDictionary = m_GameLogic.LetterDictionary;
            if (m_BoardButtons[0, 0] == null)
            {
                m_BoardButtons[0, 0] = new MemoryBoardButton(0, 0/*, currChar.ToString()*/);
            }

            m_BoardButtons[0, 0].Location = buttonFirstOnBoard.Location;
            m_BoardButtons[0, 0].Anchor = buttonFirstOnBoard.Anchor;
            m_BoardButtons[0, 0].Size = buttonFirstOnBoard.Size;
            m_BoardButtons[0, 0].BackColor = buttonFirstOnBoard.BackColor;
            m_BoardButtons[0, 0].Enabled = true;
            this.Controls.Remove(buttonFirstOnBoard);
            for (int i = 0; i < boardRow; i++)
            {
                for (int j = 0; j < boardCol; j++)
                {
                    if(letterDictionary.TryGetValue(boardGame.BoardGame[i, j].RepresentIndexForObj, out currChar))
                    {
                        if (!(i == 0 && j == 0))
                        {
                            if (m_BoardButtons[i, j] == null)
                            {
                                m_BoardButtons[i, j] = new MemoryBoardButton(i, j/*, currChar.ToString()*/);
                                
                            }

                            m_BoardButtons[i, j].ColIndex = j;
                            m_BoardButtons[i, j].RowIndex = i;
                            if (j != 0)
                            {
                                m_BoardButtons[i, j].Location = new Point(m_BoardButtons[i, j - 1].Right + 10, m_BoardButtons[i, j - 1].Top);
                            }
                            else
                            {
                                m_BoardButtons[i, j].Location = new Point(m_BoardButtons[i - 1, j].Left, m_BoardButtons[i - 1, j].Top + m_BoardButtons[i - 1, j].Height + 10);
                            }

                            m_BoardButtons[i, j].Size = m_BoardButtons[0, 0].Size;
                            m_BoardButtons[i, j].BackColor = m_BoardButtons[0, 0].BackColor;
                            m_BoardButtons[i, j].Enabled = true;
                        }

                        this.Controls.Add(m_BoardButtons[i, j]);
                        m_BoardButtons[i, j].BoardButtonClicked += buttonBoard_Clicked;
                        m_BoardButtons[i,j].CellText = currChar.ToString();
                        m_BoardButtons[i, j].Text = "";
                    }
                }
            }
        }

        private void buttonBoard_Clicked(int i_CurrRow, int i_CurrCol)
        {
            makeVisibleAndUnClickable(i_CurrRow, i_CurrCol);

            if (m_IsFirstClick)
            {
                m_FirstBoardClick = new Point(i_CurrRow, i_CurrCol);
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                if (!m_GameLogic.IsSameObject(m_CurrUser, m_FirstBoardClick, new Point(i_CurrRow, i_CurrCol)))
                {
                    m_BoardButtons[i_CurrRow, i_CurrCol].Text = "";
                    m_BoardButtons[m_FirstBoardClick.X, m_FirstBoardClick.Y].Text = "";
                    m_BoardButtons[i_CurrRow, i_CurrCol].BackColor = DefaultBackColor;
                    m_BoardButtons[m_FirstBoardClick.X, m_FirstBoardClick.Y].BackColor = DefaultBackColor;
                    m_BoardButtons[i_CurrRow, i_CurrCol].Enabled = true;
                    m_BoardButtons[m_FirstBoardClick.X, m_FirstBoardClick.Y].Enabled = true;
                    System.Threading.Thread.Sleep(1000);
                    if (m_SettingForm.Opponent == eOpponent.Computer)
                    {
                        m_GameLogic.MakeComputerMove();
                    }
                }
                else
                {
                    m_BoardButtons[i_CurrRow, i_CurrCol].BoardButtonClicked -= buttonBoard_Clicked;
                    m_BoardButtons[m_FirstBoardClick.X, m_FirstBoardClick.Y].BoardButtonClicked -= buttonBoard_Clicked;
                    //System.Threading.Thread.Sleep(1000);

                }
            }

            m_IsFirstClick = !m_IsFirstClick;
        }


        private void makeVisibleAndUnClickable(int i_CurrRow, int i_CurrCol)
        {
            m_BoardButtons[i_CurrRow, i_CurrCol].Text = m_BoardButtons[i_CurrRow, i_CurrCol].CellText;
            m_BoardButtons[i_CurrRow, i_CurrCol].BackColor = labelCurrentPlayer.BackColor;
            m_BoardButtons[i_CurrRow, i_CurrCol].Enabled = false;
        }
        //private void makeUnclickable(Point i_FirstCell, Point i_SecondCell)
        //{
            
        //    m_BoardButtons[i_FirstCell.X, i_FirstCell.Y].Enabled = false;
        //    m_BoardButtons[i_SecondCell.X, i_SecondCell.Y].Enabled = false;

        //}
    }
}
