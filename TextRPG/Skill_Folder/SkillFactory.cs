using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Skill_Folder
{
    public static class SkillFactory
    {
        public static SkillSet GetSkillSet(string jobName)
        {
            Random random = new Random();

            return jobName switch
            {
                "전사" => new WarriorSkill(),
                "마법사" => new MageSkill(),
                "도적" => new ThiefSkill(),
                "궁수" => new ArcherSkill(),
                "해적" => new PirateSkill(),
                _ => throw new Exception($"알 수 없는 직업입니다: {jobName}")
            };
        }
    }
}
