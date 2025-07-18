using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Skill_Folder
{
    public interface SkillSet
    {
        List<string> SkillNames { get; }
        public void UseSkill(int index, Player player, List<Monster> monsters, Monster mainTarget);

    }
}