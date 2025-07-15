using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Challenge_JaeEun.Character.Job_Folder
{
    public class Archer : Job
    {
        public Archer()
        {
            JobName = "궁수";
            BaseHP = 90;
            BaseMP = 40;
            BaseAttack = 18;
            BaseDefense = 5;
        }
    }
}
