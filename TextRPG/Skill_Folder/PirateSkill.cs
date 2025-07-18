using System;
using System.Collections.Generic;

namespace TextRPG.Skill_Folder
{
    internal class PirateSkill : SkillSet
    {
        public List<string> SkillNames => new()
        {
            "총격",       // MP 10 - 기본 사격 (1.3배)
            "폭탄 투척",   // MP 18 - 광역감 느낌 (1.5배)
            "칼날 돌진"    // MP 20 - 빠르고 강한 일격 (1.9배)
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
                case 1: // 총격
                    {
                        int mpCost = 10;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[해적] MP가 부족하여 '총격'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 1.3f);
                        Console.WriteLine($"[해적] '총격'! {monster.Name}에게 {damage}의 피해! (MP -{mpCost})");
                        monster.TakeDamage(damage);
                        break;
                    }

                case 2: // 폭탄 투척
                    {
                        int mpCost = 18;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[해적] MP가 부족하여 '폭탄 투척'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 1.5f);
                        Console.WriteLine($"[해적] '폭탄 투척'! {monster.Name}에게 {damage}의 폭발 피해! (MP -{mpCost})");
                        monster.TakeDamage(damage);
                        break;
                    }

                case 3: // 칼날 돌진
                    {
                        int mpCost = 20;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[해적] MP가 부족하여 '칼날 돌진'을 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 1.9f);
                        Console.WriteLine($"[해적] '칼날 돌진'! {monster.Name}에게 {damage}의 강력한 피해! (MP -{mpCost})");
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
