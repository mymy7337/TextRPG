using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace TextRPG.Skill_Folder
{
    internal static class SkillUI
    {
        public static void ShowSkillList(SkillSet skillSet)
        {
            //Console.Clear();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("⚔️  [전투 행동 선택]");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine(" [1] 🗡️ 기본 공격");

            for (int i = 0; i < skillSet.SkillNames.Count; i++)
            {
                Console.WriteLine($" [{i + 2}] ✨ {skillSet.SkillNames[i]}");
            }

            Console.WriteLine();
            Console.WriteLine(" [X] ❌ 취소하기");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }

        public static int SelectSkill(SkillSet skillSet)
        {
            ShowSkillList(skillSet);

            while (true)
            {
                Console.Write("사용할 번호를 선택하세요: ");
                string input = Console.ReadLine()?.Trim();

                if (input?.ToLower() == "x")
                {
                    Console.WriteLine("행동을 취소합니다.");
                    return -1;
                }

                if (int.TryParse(input, out int choice))
                {
                    return choice;
                }

                Console.WriteLine("올바른 번호를 입력해주세요.");
            }
        }


    }
}
