using System;
using System.Collections.Generic;

namespace TextRPG.Skill_Folder
{
    internal class ArcherSkill : SkillSet
    {
        public List<string> SkillNames => new()
        {
            "급소 사격",     // MP 10 - 치명타 피해
            "연속 사격",     // MP 15 - 0.7배 2회
            "집중 사격",      // MP 20 - 강한 2.0배 피해
            "연발 사격"    // MP 18 - 0.85배 피해, 2명에게
        };

        public void UseSkill(int index, Player player, List<Monster> monsters, Monster mainTarget)
        {
            Random random = new Random();
            switch (index)
            {
                case 0: // 기본 공격
                    int basedamage = player.FinalAtk;
                    bool isCrit = random.Next(0, 100) < player.CritChance;
                    if (isCrit)
                    {
                        basedamage = (int)(basedamage * player.CritMultiplier);
                        Console.WriteLine($"[기본 공격 - 치명타!] {mainTarget.Name}에게 {basedamage}의 피해!");
                    }
                    else
                    {
                        Console.WriteLine($"[기본 공격] {mainTarget.Name}에게 {basedamage}의 피해!");
                    }
                    mainTarget.TakeDamage(basedamage);
                    break;
                case 1:
                    {
                        int mpCost = 10;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[궁수] MP가 부족하여 '급소 사격'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * player.CritMultiplier);
                        Console.WriteLine($"[궁수] '급소 사격'! {mainTarget.Name}에게 치명타 {damage} 피해! (MP -{mpCost})");
                        mainTarget.TakeDamage(damage);
                        break;
                    }

                case 2:
                    {
                        int mpCost = 15;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[궁수] MP가 부족하여 '연속 사격'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int d1 = (int)(player.FinalAtk * 0.7f);
                        int d2 = (int)(player.FinalAtk * 0.7f);
                        Console.WriteLine($"[궁수] '연속 사격'! {mainTarget.Name}에게 {d1} + {d2} 피해! (MP -{mpCost})");
                        mainTarget.TakeDamage(d1 + d2);
                        break;
                    }

                case 3:
                    {
                        int mpCost = 20;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[궁수] MP가 부족하여 '집중 사격'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 2.0f);
                        Console.WriteLine($"[궁수] '집중 사격'! {mainTarget.Name}에게 {damage}의 강한 피해! (MP -{mpCost})");
                        mainTarget.TakeDamage(damage);
                        break;
                    }
                case 4: // 연발 사격
                    {
                        int mpCost = 18;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[궁수] MP가 부족하여 '연발 사격'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 0.85f);

                        Console.WriteLine($"[궁수] '연발 사격' 사용! 2명의 적을 빠르게 조준합니다! (MP -{mpCost})");

                        var targets = monsters.Where(m => m.Hp > 0).Take(2).ToList();
                        foreach (var m in targets)
                        {
                            Console.WriteLine($"- 🏹 {m.Name}에게 {damage}의 피해!");
                            m.TakeDamage(damage);
                        }
                        break;
                    }

                default:
                    Console.WriteLine("알 수 없는 스킬입니다.");
                    break;
            }
        }
    }
}
