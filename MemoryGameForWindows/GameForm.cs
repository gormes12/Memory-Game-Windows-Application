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
        private MemoryGameLogic m_GameLogic;
        private SettingForm m_SettingForm;
        private MemoryBoardButton[,] m_BoardButtons;
        private bool m_IsFirstClick;
        private int currRowClicked;
        private int currColClicked;
        private Point m_FirstBoardClick;
        private User m_CurrUser;


        public GameForm()
        {
            InitializeComponent();
            m_SettingForm = new SettingForm();
            if(m_SettingForm.ShowDialog() == DialogResult.OK)
            {
                Point boardSize = m_SettingForm.BoardSize;
                m_GameLogic = new MemoryGameLogic(
                                                 boardSize.X, boardSize.Y,
                                                 m_SettingForm.FirstPlayerName,
                                                 m_SettingForm.SecondPlayerName);
                m_BoardButtons = new MemoryBoardButton[boardSize.X, boardSize.Y];
                initializeBoard();
                initializeLebels();
                m_GameLogic.FoundPair += M_GameLogic_FoundPair;
                startGame();
            }
            else
            {
                this.Close();
            }
        }

        private void M_GameLogic_FoundPair(Point i_Cell1, Point i_Cell2)
        {
            makeVisible(i_Cell1,i_Cell2);
        }

        private void startGame()
        {
           if(m_SettingForm.Opponent == eOpponent.Computer)
            {
                startGameAgainsComputer();
            }
           else
            {
                startGameAgainstOtherPlayer();
            }
        }

        private void startGameAgainstOtherPlayer()
        {
            m_CurrUser = m_GameLogic.CurrPlayer;
            labelCurrentPlayer.Text = string.Format("Current Player: {0}", m_CurrUser.Name);
            labelCurrentPlayer.BackColor = m_SettingForm.FirstPlayerName == m_CurrUser.Name ?
                labelFirstPlayerRes.BackColor : labelSecondPlayerRes.BackColor;

        }

        private void startGameAgainsComputer()
        {
            
        }

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
            m_BoardButtons[0, 0] = new MemoryBoardButton();
            m_BoardButtons[0, 0].Location = buttonFirstOnBoard.Location;
            m_BoardButtons[0, 0].Anchor = buttonFirstOnBoard.Anchor;
            m_BoardButtons[0, 0].Size = buttonFirstOnBoard.Size;
            this.Controls.Remove(buttonFirstOnBoard);
            for (int i = 0; i < boardRow; i++)
            {
                for (int j = 0; j < boardCol; j++)
                {
                    if(letterDictionary.TryGetValue(boardGame.BoardGame[i, j].RepresentIndexForObj, out currChar))
                    {
                        if (!(i == 0 && j == 0))
                        {
                            m_BoardButtons[i, j] = new MemoryBoardButton();
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

                            m_BoardButtons[i, j].Size = (m_BoardButtons[0, 0].Size);
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
           // currRowClicked = i_currRow;
            //currColClicked = i_currCol;
            if(m_IsFirstClick)
            {
                m_IsFirstClick = false;
                m_FirstBoardClick = new Point(i_CurrRow, i_CurrCol);
                m_BoardButtons[i_CurrRow, i_CurrCol].Text = m_BoardButtons[i_CurrRow, i_CurrCol].CellText;
                m_BoardButtons[i_CurrRow, i_CurrCol].BackColor = labelCurrentPlayer.BackColor;
            }
            else
            {
                m_IsFirstClick = true;
                m_BoardButtons[i_CurrRow, i_CurrCol].Text = m_BoardButtons[i_CurrRow, i_CurrCol].CellText;
                m_BoardButtons[i_CurrRow, i_CurrCol].BackColor = labelCurrentPlayer.BackColor;

                if(!m_GameLogic.IsSameObject(m_CurrUser, m_FirstBoardClick, new Point(i_CurrRow, i_CurrCol)))
                {
                    m_BoardButtons[m_FirstBoardClick.X, m_FirstBoardClick.Y].Text = "";
                    m_BoardButtons[i_CurrRow, i_CurrCol].Text = "";
                    m_BoardButtons[m_FirstBoardClick.X, m_FirstBoardClick.Y].BackColor = DefaultBackColor;
                    m_BoardButtons[i_CurrRow, i_CurrCol].BackColor = DefaultBackColor;
                }
               
            }

        }


        private void makeVisible(Point i_FirstCell, Point i_SecondCell)
        {
            //m_BoardButtons[i_FirstCell.X, i_FirstCell.Y].Text = m_BoardButtons[i_FirstCell.X, i_FirstCell.Y].CellText;
            //m_BoardButtons[i_SecondCell.X, i_SecondCell.Y].Text = m_BoardButtons[i_SecondCell.X, i_SecondCell.Y].CellText;
            m_BoardButtons[i_FirstCell.X, i_FirstCell.Y].Enabled = false;
            m_BoardButtons[i_SecondCell.X, i_SecondCell.Y].Enabled = false;

        }


    }
}
