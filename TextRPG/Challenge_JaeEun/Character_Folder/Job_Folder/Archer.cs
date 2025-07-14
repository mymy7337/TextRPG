using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Challenge_JaeEun.Character.Job
{
    public class Archer : CharacterJob
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
