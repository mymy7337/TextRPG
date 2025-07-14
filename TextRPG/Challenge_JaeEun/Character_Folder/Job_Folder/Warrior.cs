using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Challenge_JaeEun.Character.Job_Folder
{
    public class Warrior : Job
    {
        public Warrior()
        {
            JobName = "전사";
            BaseHP = 120;
            BaseMP = 30;
            BaseAttack = 15;
            BaseDefense = 10;
        }
    }
}
