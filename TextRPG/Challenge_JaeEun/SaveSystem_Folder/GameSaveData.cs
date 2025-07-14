using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Challenge_JaeEun.SaveSystem_Folder
{
    public class GameSaveData
    {
        public string PlayerName = "";
        public string JobName = "";

        public int Level;
        public int Exp;
        public int Gold;

        public int HP;
        public int MP;

        public List<string> InventoryItems;
        public List<string> CompletedQuests;
    }
}
