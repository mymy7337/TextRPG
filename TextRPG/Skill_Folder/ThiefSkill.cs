using System;
using System.Collections.Generic;

namespace TextRPG.Skill_Folder
{
    internal class ThiefSkill : SkillSet
    {
        public List<string> SkillNames => new()
        {
            "기습",       // MP 10 - 확정 치명타
            "이중 베기",   // MP 15 - 0.6배 피해 × 2회
            "속공 연타"    // MP 20 - 빠른 공격, × 1.8 피해
        };

        public void UseSkill(int index, Player player, Monster monster)
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
                        Console.WriteLine($"[기본 공격 - 치명타!] {monster.Name}에게 {basedamage}의 피해!");
                    }
                    else
                    {
                        Console.WriteLine($"[기본 공격] {monster.Name}에게 {basedamage}의 피해!");
                    }
                    monster.TakeDamage(basedamage);
                    break;
                case 1: // 기습
                    {
                        int mpCost = 10;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[도적] MP가 부족하여 '기습'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * player.CritMultiplier);
                        Console.WriteLine($"[도적] '기습' 사용! {monster.Name}에게 치명타 {damage} 피해! (MP -{mpCost})");
                        monster.TakeDamage(damage);
                        break;
                    }

                case 2: // 이중 베기
                    {
                        int mpCost = 15;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[도적] MP가 부족하여 '이중 베기'를 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int d1 = (int)(player.FinalAtk * 0.6f);
                        int d2 = (int)(player.FinalAtk * 0.6f);
                        Console.WriteLine($"[도적] '이중 베기' 사용! {monster.Name}에게 {d1} + {d2} 피해! (MP -{mpCost})");
                        monster.TakeDamage(d1 + d2);
                        break;
                    }

                case 3: // 속공 연타
                    {
                        int mpCost = 20;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[도적] MP가 부족하여 '속공 연타'를 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 1.8f);
                        Console.WriteLine($"[도적] '속공 연타' 사용! {monster.Name}에게 {damage}의 빠른 연속 피해! (MP -{mpCost})");
                        monster.TakeDamage(damage);
                        break;
                    }

                default:
                    Console.WriteLine("알 수 없는 스킬입니다.");
                    break;
            }
        }
    }
}
