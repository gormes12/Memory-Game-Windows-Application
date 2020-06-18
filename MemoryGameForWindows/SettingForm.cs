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
    public enum eOpponent
    {
        OtherPlayer,
        Computer
    }

    public partial class SettingForm : Form
    {
        private List<Point> m_PossibleBoardSize;
        private int m_IndexForCurrBoardSize;

        public SettingForm()
        {
            InitializeComponent();
            m_PossibleBoardSize = MemoryGameLogic.GetPossibleBoardSize();
            m_IndexForCurrBoardSize = 0;
            buttonBoardSize.Text = string.Format("{0}X{1}", m_PossibleBoardSize[m_IndexForCurrBoardSize].X, m_PossibleBoardSize[m_IndexForCurrBoardSize].Y);
        }

        public eOpponent Opponent
        {
            get
            {
                if(textBoxFriend.Text == "- computer -")
                {
                    return eOpponent.Computer;
                }
                else
                {
                    return eOpponent.OtherPlayer;
                }
            }
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            m_IndexForCurrBoardSize++;
            if(m_IndexForCurrBoardSize >= m_PossibleBoardSize.Count)
            {
                m_IndexForCurrBoardSize = 0;
            }
            buttonBoardSize.Text = string.Format("{0}X{1}", m_PossibleBoardSize[m_IndexForCurrBoardSize].X, m_PossibleBoardSize[m_IndexForCurrBoardSize].Y);
        }

        private void buttonOpponent_Click(object sender, EventArgs e)
        {
            textBoxFriend.Enabled = !textBoxFriend.Enabled;

            if (textBoxFriend.Enabled)
            {
                buttonOpponent.Text = "Against Computer";
                textBoxFriend.Text = "";
            }
            else
            {
                buttonOpponent.Text = "Against a Friend";
                textBoxFriend.Text = "- computer -";
            }
        }

        public string FirstPlayerName
        {
            get
            {
                return textBoxFirstPlayerName.Text;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return textBoxFriend.Text;
            }
        }

        public Point BoardSize
        {
            get
            {
                return m_PossibleBoardSize[m_IndexForCurrBoardSize];
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
