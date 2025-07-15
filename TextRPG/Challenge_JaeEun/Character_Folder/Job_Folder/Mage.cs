using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Challenge_JaeEun.Character.Job_Folder
{
    public class Mage : Job
    {
        public Mage()
        {
            JobName = "마법사";
            BaseHP = 80;
            BaseMP = 70;
            BaseAttack = 10;
            BaseDefense = 3;
        }


    }
}
