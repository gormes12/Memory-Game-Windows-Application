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
        Label m_BoarSize = new Label();
        TextBox m_TextBoxFirstPlayer = new TextBox();
        TextBox m_TextBoxFriend = new TextBox();
        Button m_ButtonChooseOppenent = new Button();
        Button m_ButtonBoardSize = new Button();
        Button m_ButtonStart = new Button();

        public SettingForm()
        {
            this.Text = "Memory Game - Setting";
            this.Size = new Size(390, 210);

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

            m_LabelNameFriend.Text = "Second Player Name:";
            m_LabelNameFriend.Location = new Point(10, 50);

            int textBoxFirstPlayerTop = m_LabelNameFirstPlayer.Top + m_LabelNameFirstPlayer.Height / 2;
            textBoxFirstPlayerTop -= m_TextBoxFirstPlayer.Height / 2;
            m_TextBoxFirstPlayer.Location = new Point(m_LabelNameFriend.Right + 4, textBoxFirstPlayerTop);

            int textBoxFriendTop = m_LabelNameFriend.Top + m_LabelNameFriend.Height / 2;
            textBoxFriendTop -= m_LabelNameFriend.Height / 2;
            m_TextBoxFriend.Location = new Point(m_LabelNameFriend.Right + 4, textBoxFriendTop);

            this.Controls.AddRange(new Control[] { m_LabelNameFirstPlayer, m_LabelNameFriend, m_TextBoxFirstPlayer, m_TextBoxFriend });
        }
    }
}
