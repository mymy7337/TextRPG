using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Quest_Folder;
using TextRPG.Skill_Folder;

namespace TextRPG
{
    internal class Battle
    {
        Random random = new Random();
        enum BattleState
        {
            Main,
            Encounter,
            Result,
            Exit
        }

        bool isWrong;
        int choice;
        string message = "";
        int deadCount;
        int nowHp;

        public void BattleStart(Player player, SkillSet skillSet)
        {
            BattleState state = BattleState.Main;

            while (state != BattleState.Exit)
            {
                switch (state)
                {
                    case BattleState.Main:
                        state = MainUI(player, state);
                        break;
                    case BattleState.Encounter:
                        state = EncounterUI(player, state, skillSet); // skillSet 전달
                        break;
                    case BattleState.Result:
                        state = result(player);
                        break;
                }
            }
        }


        BattleState MainUI(Player player, BattleState state)
        {
            if (monsterSpanwed.Count == 0)
            {
                SpawnMonster();
                deadCount = 0;
            }
            nowHp = player.Hp;
            
            List<string> option = new List<string>() { "1. 공격", "0. 돌아가기" };
            DisplayUI(player, state, option);
            isWrong = ChoiceCheck(0, option.Count-1);
            if (isWrong)
                return BattleState.Main;

            switch (choice)
            {
                case 0:
                    if (random.Next(0, 100) < (100 - (monsterSpanwed.Count-deadCount) * 10))
                    {
                        monsterSpanwed.Clear();
                        return BattleState.Exit;
                    }
                    player.Hp -= (monsterSpanwed.Count - deadCount) * 2;
                    return BattleState.Main;

                case 1:
                    return BattleState.Encounter;
                default:
                    return BattleState.Main;
            }

        }

        BattleState EncounterUI(Player player, BattleState state, SkillSet skillSet)
        {
            List<string> option = new List<string>() { "0. 돌아가기" };
            DisplayUI(player, state, option);
            isWrong = ChoiceCheck(0, monsterSpanwed.Count);

            if (isWrong)
                return BattleState.Encounter;

            if (choice == 0)
                return BattleState.Main;

            int monsterIdx = choice - 1;
            if (monsterIdx >= 0 && monsterIdx < monsterSpanwed.Count)
            {
                Monster monster = monsterSpanwed[monsterIdx];
                if (monster.Hp <= 0)
                {
                    isWrong = true;
                    return BattleState.Encounter;
                }
                return PlayerPhase(player, monster, skillSet); // skillSet 전달
            }

            return BattleState.Encounter;
        }


        BattleState PlayerPhase(Player player, Monster monster, SkillSet skillset)
        {
            Console.Clear();
            Console.WriteLine($"[전투] {monster.Name}에게 행동을 선택하세요.");

            // 직업에 따라 스킬 세트 가져오기
            SkillSet skillSet = SkillFactory.GetSkillSet(player.Job);

            // 스킬 선택
            int selected = SkillUI.SelectSkill(skillSet);
            Console.WriteLine();

            if (selected == -1)
            {
                Console.WriteLine("행동을 취소했습니다.");
                Console.WriteLine("아무 키나 누르면 돌아갑니다.");
                Console.ReadKey();
                return BattleState.Encounter;
            }
            
            /*
            else if (selected == 0)
            {
                // ✅ 기본 공격 처리
                int damage = player.FinalAtk;
                bool isCrit = random.Next(0, 100) < player.CritChance;
            }
            */
            else
            {
                skillSet.UseSkill(selected - 1, player, monster);
            }
            

            Console.WriteLine();
            monster.DisplayHpInfo(monster.Hp);
            Console.WriteLine("\n다음으로 진행하려면 아무 키나 누르세요.");
            Console.ReadKey();

            return EnemyPhase(player, monster);
        }



        BattleState EnemyPhase(Player player, Monster monster)
        {
            if (monster.Hp <= 0)
            {
                // ✅ 퀘스트 진행도 반영 + 보상 지급
                QuestManager.CheckKill(monster.Name, player);

                // 기존 아이템 드랍은 일단 생략
                deadCount++;

                if (deadCount == monsterSpanwed.Count)
                    return result(player);

                return BattleState.Encounter;
            }




            int prevHp = player.Hp;
            Console.Clear();
            monster.Attack(player);
            player.DisplayHpInfo(prevHp);
            Console.WriteLine();
            Console.WriteLine("다음");
            Console.Write(">>");
            Console.ReadKey();
            if (player.Hp <= 0)
                return result(player);
            if (deadCount == monsterSpanwed.Count)
                return result(player);
            else
                return BattleState.Encounter;
        }

        BattleState result(Player player)
        {
            Console.Clear();
            int prevHp = player.Hp;
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            if (player.Hp > 0)
            {
                Console.Clear();
                Console.WriteLine("Victory");
                Console.WriteLine();
                Console.WriteLine($"던전에서 몬스터를 {monsterSpanwed.Count}마리를 잡았습니다.");
                Console.WriteLine();
                Console.WriteLine($"HP {nowHp} -> {player.Hp}");
                Console.WriteLine();

                // ✅ MP 회복 추가
                int oldMp = player.Mp;
                player.Mp += 10;
                if (player.Mp > player.MaxMp)
                    player.Mp = player.MaxMp;

                Console.WriteLine($"MP {oldMp} -> {player.Mp}");

                if (getItem.Count > 0)
                {
                    foreach (var item in getItem)
                    {
                        Console.WriteLine($"{item.ItemName} 획득");
                        player.AddItem(item);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("1. 던전 탐사\n0. 마을");
                Console.WriteLine();
                message = (isWrong == true) ? "잘못된 입력입니다." : "원하시는 행동을 입력해주세요.";
                Console.WriteLine(message);
                Console.Write(">>");

                isWrong = ChoiceCheck(0, 1);
                if (isWrong)
                    return BattleState.Result;
                switch (choice)
                {
                    case 0:
                        getItem.Clear();
                        monsterSpanwed.Clear();
                        return BattleState.Exit;
                    case 1:
                        getItem.Clear();
                        monsterSpanwed.Clear();
                        return BattleState.Main;
                    default:
                        return BattleState.Result;
                }
            }
            else
            {
                Console.WriteLine("You Lose");
                Console.WriteLine();
                Console.WriteLine($"Lv{player.Level:D2} {player.Name}");
                Console.WriteLine($"HP  -> 0");
                Console.WriteLine();
                Console.WriteLine("게임 오버");
                Console.WriteLine("게임을 종료합니다.");
                Console.WriteLine(">>");
                monsterSpanwed.Clear();
                Console.ReadKey();
                Environment.Exit(0);
                return BattleState.Exit;
            }
        }
        List<Monster> monsterSpanwed = new List<Monster>();
        List<Item> getItem = new List<Item>();

        void SpawnMonster()
        {
            int monNum = random.Next(1, 5);
            for (int i = 0; i < monNum; i++)
            {
                int monId = random.Next(0, 3);
                Monster baseMon = MonsterDB.monsterData[monId];
                Monster newMon = baseMon.Clone();
                monsterSpanwed.Add(newMon);
            }
        }
        bool ChoiceCheck(int min, int max)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out choice))
            {
                if (choice >= min && choice <= max)
                    return false;
            }
            return true;
        }

        void DisplayUI(Player player, BattleState state, List<string> option)
        {
            Monster monster;
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            for (int i = 0; i < monsterSpanwed.Count; i++)
            {
                monster = monsterSpanwed[i];
                Console.Write(state == BattleState.Encounter ? $"{i + 1} " : "");
                monster.DisplayMonsterInfo();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            player.DisplayBattleInfo();
            Console.WriteLine();
            foreach(string optionLine in option)
                Console.WriteLine(optionLine);
            Console.WriteLine();
            string defaultMessage = state switch
            {
                BattleState.Encounter => "대상을 선택해주세요.",
                _ => "원하시는 행동을 입력해주세요."
            };
            message = isWrong ? "잘못된 입력입니다." : defaultMessage;
            Console.WriteLine(message);
            Console.Write(">>");
        }
    }
}
