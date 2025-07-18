using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class PlayerManager
    {
        public static PlayerManager instance;

        public PlayerManager() 
        { 
            if (instance == null)
                instance = this;
        }
    }
}
 