using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MemoryGameForWindows
{
    class MemoryBoardButton : Button
    {
        private int m_RowIndex;
        private int m_ColIndex;
        private string m_CellText;


        public MemoryBoardButton(int i_RowIndex, int i_ColIndex/*, string i_CellText*/)
        {
            m_RowIndex = i_RowIndex;
            m_ColIndex = i_ColIndex;
            //m_CellText = i_CellText;
        }
        public string CellText
        {
            get
            {
                return m_CellText;
            }
            set
            {
                m_CellText = value;
            }
        }

        public event Action<int,int> BoardButtonClicked;

        public int RowIndex
        {
            get
            {
                return m_RowIndex;
            }
            set
            {
                m_RowIndex = value;
            }
        }

        public int ColIndex
        {
            get
            {
                return m_ColIndex;
            }
            set
            {
                m_ColIndex = value;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (BoardButtonClicked != null)
            {
                BoardButtonClicked(m_RowIndex, m_ColIndex);
            }
        }
    }
}
