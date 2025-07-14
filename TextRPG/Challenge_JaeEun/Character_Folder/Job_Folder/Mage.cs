using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Challenge_JaeEun.Character.Job
{
    public class Mage : CharacterJob
    {
        public Mage()
        {
            JobName = "마법사";
            BaseHP = 80;
            BaseMP = 70;
            BaseAttack = 10;
            BaseDefense = 3;
        }

        public override void UseSkill()
        {
            // 마법사 전용 스킬 사용 로직
            Console.WriteLine($"{JobName}의 강력한 마법을 사용합니다!");
        }
    }
}
