using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MemoryGameForWindows
{
    class SettingForm : Form
    {
        Label m_LabelNameFirstPlayer = new Label();
        Label m_LabelNameFriend = new Label();
        Label m_LabelBoarSize = new Label();
        TextBox m_TextBoxFirstPlayer = new TextBox();
        TextBox m_TextBoxFriend = new TextBox();
        Button m_ButtonChooseOppenent = new Button();
        Button m_ButtonBoardSize = new Button();
        Button m_ButtonStart = new Button();
        private List<Point> m_PossibleBoardSize;
        private int m_IndexForCurrBoardSize;

        public SettingForm()
        {
            this.Text = "Memory Game - Setting";
            this.Size = new Size(390, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
           m_PossibleBoardSize =  MemoryGameLogic.GetPossibleBoardSize();
            m_IndexForCurrBoardSize = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            initControl();
        }

        private void initControl()
        {
            m_LabelNameFirstPlayer.Text = "First Player Name:";
            m_LabelNameFirstPlayer.Location = new Point(10, 20);
            this.Controls.Add(m_LabelNameFirstPlayer);

            m_LabelNameFriend.Text = "Second Player Name:";
            m_LabelNameFriend.AutoSize = true;
            m_LabelNameFriend.Location = new Point(10, 50);
            this.Controls.Add(m_LabelNameFriend);

            m_LabelBoarSize.Text = "Board Size:";
            m_LabelBoarSize.Location = new Point(10, 100);
            this.Controls.Add(m_LabelBoarSize);

            int textBoxFirstPlayerTop = m_LabelNameFirstPlayer.Top + m_LabelNameFirstPlayer.Height / 2;
            textBoxFirstPlayerTop -= m_TextBoxFirstPlayer.Height / 2;
            m_TextBoxFirstPlayer.Location = new Point(m_LabelNameFriend.Right + 8, textBoxFirstPlayerTop);
            this.Controls.Add(m_TextBoxFirstPlayer);

            int textBoxFriendTop = m_LabelNameFriend.Top + m_LabelNameFriend.Height / 2;
            textBoxFriendTop -= m_LabelNameFriend.Height / 2;
            m_TextBoxFriend.Location = new Point(m_LabelNameFriend.Right + 8, textBoxFriendTop);
            m_TextBoxFriend.Enabled = false;
            this.Controls.Add(m_TextBoxFriend);

            m_ButtonChooseOppenent.Text = "Against a Friend";
            m_ButtonChooseOppenent.AutoSize = true;
            m_ButtonChooseOppenent.Location = new Point(m_TextBoxFriend.Right + 10, textBoxFriendTop - 4);
            this.Controls.Add(m_ButtonChooseOppenent);

            m_ButtonBoardSize.Text = string.Format("{0}X{1}", m_PossibleBoardSize[m_IndexForCurrBoardSize].X, m_PossibleBoardSize[m_IndexForCurrBoardSize].Y);
            this.Controls.Add(m_ButtonBoardSize);


        }
    }
}
