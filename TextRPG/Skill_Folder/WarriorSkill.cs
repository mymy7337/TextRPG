using System;
using System.Collections.Generic;


namespace TextRPG.Skill_Folder
{
    internal class WarriorSkill : SkillSet
    {
        public List<string> SkillNames => new()
        {
            "강타",     // MP 10 - 공격력 × 1.5
            "회전 베기", // MP 15 - 전체 대상 공격용(지금은 단일 대상), ×1.2
            "분쇄"      // MP 20 - 높은 데미지, ×2.0
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
                case 1: // 강타
                    {
                        int mpCost = 10;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[전사] MP가 부족하여 '강타'를 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 1.5f);
                        Console.WriteLine($"[전사] '강타' 사용! {monster.Name}에게 {damage}의 피해! (MP -{mpCost})");
                        monster.TakeDamage(damage);
                        break;
                    }

                case 2: // 회전 베기
                    {
                        int mpCost = 15;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[전사] MP가 부족하여 '회전 베기'를 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 1.2f);
                        Console.WriteLine($"[전사] '회전 베기' 사용! {monster.Name}에게 {damage}의 회전 피해! (MP -{mpCost})");
                        monster.TakeDamage(damage);
                        break;
                    }

                case 3: // 분쇄
                    {
                        int mpCost = 20;
                        if (player.Mp < mpCost)
                        {
                            Console.WriteLine("[전사] MP가 부족하여 '분쇄'를 사용할 수 없습니다.");
                            return;
                        }

                        player.Mp -= mpCost;
                        int damage = (int)(player.FinalAtk * 2.0f);
                        Console.WriteLine($"[전사] '분쇄' 사용! {monster.Name}에게 {damage}의 강력한 피해! (MP -{mpCost})");
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
