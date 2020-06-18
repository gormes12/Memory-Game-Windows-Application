using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameForWindows
{
    class User
    {
        private readonly string r_Name;
        private uint m_Points;

        public User(string i_Name) // c'tor
        {
            r_Name = i_Name;
            m_Points = 0;
        }

        /// public properties
        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public uint Points
        {
            get
            {
                return m_Points;
            }
        }

        public void IncrementPoints()
        {
            m_Points++;
        }

        public void RestartPoints()
        {
            m_Points = 0;
        }
    }
}
