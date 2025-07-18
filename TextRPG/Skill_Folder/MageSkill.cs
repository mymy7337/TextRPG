using System;
using System.Collections.Generic;

namespace TextRPG.Skill_Folder
{
    internal class MageSkill : SkillSet
    {
        public List<string> SkillNames => new()
        {
            "파이어볼",   // MP 12 - 1.6배 화염 피해
            "얼음 화살",  // MP 15 - 1.2배 냉기 피해
            "마나 폭발",   // MP 25 - 2.2배 강력 피해
            "연쇄 번개"    // MP 20 - 0.8배 전격 피해, 3명에게
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
                case 1: // 파이어볼
                    {
                        int mpCost = 12;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[마법사] MP가 부족하여 '파이어볼'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 1.6f);
                        Console.WriteLine($"[마법사] '파이어볼' 발사! {mainTarget.Name}에게 {damage}의 화염 피해! (MP -{mpCost})");
                        mainTarget.TakeDamage(damage);
                        break;
                    }

                case 2: // 얼음 화살
                    {
                        int mpCost = 15;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[마법사] MP가 부족하여 '얼음 화살'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 1.2f);
                        Console.WriteLine($"[마법사] '얼음 화살' 발사! {mainTarget.Name}에게 {damage}의 냉기 피해! (MP -{mpCost})");
                        mainTarget.TakeDamage(damage);
                        break;
                    }

                case 3: // 마나 폭발
                    {
                        int mpCost = 25;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[마법사] MP가 부족하여 '마나 폭발'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 2.2f);
                        Console.WriteLine($"[마법사] '마나 폭발'! {mainTarget.Name}에게 {damage}의 강력한 마법 피해! (MP -{mpCost})");
                        mainTarget.TakeDamage(damage);
                        break;
                    }
                case 4: // 연쇄 번개
                    {
                        int mpCost = 20;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[마법사] MP가 부족하여 '연쇄 번개'를 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 0.8f);

                        Console.WriteLine($"[마법사] '연쇄 번개' 사용! 3명의 적에게 피해를 줍니다! (MP -{mpCost})");

                        var targets = monsters.Where(m => m.Hp > 0).Take(3).ToList();
                        foreach (var m in targets)
                        {
                            Console.WriteLine($"- ⚡ {m.Name}에게 {damage}의 전격 피해!");
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
