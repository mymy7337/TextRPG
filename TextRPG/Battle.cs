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
using TextRPG.ItemFolder;

namespace TextRPG
{
    internal class Battle
    {
        MonsterUI monUI = new MonsterUI();
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
            Console.WriteLine("━━━━━━━━━━━━━━ PLAYER PHASE ━━━━━━━━━━━━━━");
            Console.WriteLine($"🎯 {monster.Name} 을(를) 상대로 어떤 행동을 할까요?");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            int selected = SkillUI.SelectSkill(skillset);
            Console.WriteLine();

            if (selected == -1)
            {
                Console.WriteLine("\n🚫 행동을 취소했습니다.");
                Console.WriteLine("🔙 아무 키나 누르면 돌아갑니다...");
                Console.ReadKey();
                return BattleState.Encounter;
            }

            Console.WriteLine("━━━━━━━━━━━━━━ ACTION RESULT ━━━━━━━━━━━━━━");

            // ✅ 전체 몬스터의 이전 HP 저장
            var prevHpDict = monsterSpanwed.ToDictionary(m => m, m => m.Hp);

            // ✅ 스킬 실행 (여러 마리를 공격할 수도 있음)
            skillset.UseSkill(selected - 1, player, monsterSpanwed, monster);

            Console.WriteLine("━━━━━━━━━━━━━━ ACTION RESULT ━━━━━━━━━━━━━━\n\n");

            // ✅ HP 변화 출력 (피해를 입은 몬스터만)
            var damagedMonsters = monsterSpanwed.Where(m => m.Hp < prevHpDict[m]).ToList();
            if (damagedMonsters.Count > 0)
            {
                foreach (var m in damagedMonsters)
                {
                    monUI.DisplayHpInfo(m, prevHpDict[m]); // ✅ 하나씩 변화 출력
                }
            }

            Console.WriteLine(":다음으로 진행하려면 아무 키나 누르세요...");
            Console.ReadKey();

            return EnemyPhase(player, monster);
        }





        BattleState EnemyPhase(Player player, Monster monster)
        {
            if (monster.Hp <= 0)
            {
                QuestManager.CheckKill(monster.Name, player);

                // 🎁 아이템 드랍 (50% 확률)
                if (random.Next(0, 100) < 50)
                {
                    if (Item.Items.Count > 0)
                    {
                        Item dropItem = Item.Items[random.Next(Item.Items.Count)];
                        getItem.Add(dropItem);
                    }
                }
                deadCount = 0;
                foreach(Monster mon in monsterSpanwed)
                {
                    if (mon.Hp <= 0)
                        deadCount++;
                }
                if (deadCount == monsterSpanwed.Count)
                    return result(player);

                return BattleState.Encounter;
            }
            
            int prevHp = player.Hp;
            Console.Clear();
            monster.Attack(player);
            PlayerUI.DisplayHpInfo(player,prevHp);
            Console.WriteLine();
            Console.WriteLine("다음");
            Console.Write(">>");
            Console.ReadKey();
            if (player.Hp <= 0)
                return result(player);
            if (deadCount == monsterSpanwed.Count)
            {
                return result(player);
            }
                
            else
                return BattleState.Encounter;
        }

        BattleState result(Player player)
        {
            Console.Clear();
            int prevHp = player.Hp;
            int oldMp = player.Mp;
            Console.WriteLine("━━━━━━━━━━━━━━ 🏆 전투 결과 🏆 ━━━━━━━━━━━━━━");
            Console.WriteLine();

            if (player.Hp > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🎉 Victory! 전투에서 승리했습니다!");
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine($"🧟‍♂️ 잡은 몬스터 수: {monsterSpanwed.Count} 마리");
                Console.WriteLine($"❤️ HP: {nowHp} → {player.Hp}");
                
                // ✅ MP 회복
                if (!isWrong)
                {
                    player.Mp += 10;
                    if (player.Mp > player.MaxMp)
                        player.Mp = player.MaxMp;
                }
                else
                    oldMp = player.Mp - 10;

                Console.WriteLine($"💧 MP: {oldMp} → {player.Mp}");

                // 아이템 획득 출력
                if (getItem.Count > 0)
                {
                    Console.WriteLine("\n📦 획득한 아이템:");
                    foreach (var item in getItem)
                    {
                        Console.WriteLine($"- {item.Name}");
                        player.AddItem(item);  // 인벤토리에 추가 + 구매 상태 처리
                    }
                    getItem.Clear(); // 다음 전투에 영향 없도록 초기화
                }


                Console.WriteLine("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                Console.WriteLine("어디로 이동하시겠습니까?");
                Console.WriteLine("[1] 🔁 던전 탐사 계속하기");
                Console.WriteLine("[0] 🏠 마을로 돌아가기");
                Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                message = isWrong ? "❌ 잘못된 입력입니다." : "👉 번호를 입력해주세요:";
                Console.WriteLine(message);
                Console.Write(">> ");

                isWrong = ChoiceCheck(0, 1);
                if (isWrong)
                    return BattleState.Result;

                //getItem.Clear();
                monsterSpanwed.Clear();

                return choice switch
                {
                    0 => BattleState.Exit,
                    1 => BattleState.Main,
                    _ => BattleState.Result
                };
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("💀 You Lose... 전투에 패배했습니다.");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"Lv{player.Level:D2} {player.Name}");
                Console.WriteLine($"HP: 0");
                Console.WriteLine("\n☠️ 게임 오버. 게임을 종료합니다...");
                Console.Write(">> ");
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
                int monId = random.Next(0, MonsterDB.monsterData.Count);
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
            Console.Clear();
            Console.WriteLine("═══════════════════════ ⚔️ Battle ⚔️ ═══════════════════════");
            Console.WriteLine();

            // 📌 몬스터 목록 표시
            Console.WriteLine("🧟‍♂️ 몬스터 목록");
            for (int i = 0; i < monsterSpanwed.Count; i++)
            {
                Monster monster = monsterSpanwed[i];
                string prefix = state == BattleState.Encounter ? $"[{i + 1}] " : "   ";

                if (monster.Hp <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(prefix + "[Dead] ");
                    //Console.ResetColor();
                }
                else
                {
                    Console.Write(prefix);
                }

                monUI.DisplayMonsterInfo(monster);
                Console.ResetColor();
            }


            Console.WriteLine("\n════════════════════════════════════════════════════════════");

            // 📌 플레이어 정보
            Console.WriteLine("🧙‍♂️ 내 정보");
            PlayerUI.DisplayBattleInfo(player);

            Console.WriteLine("════════════════════════════════════════════════════════════\n");

            // 📌 행동 선택 옵션
            Console.WriteLine("🛡️ 선택지");
            foreach (string optionLine in option)
            {
                Console.WriteLine(optionLine);
            }

            Console.WriteLine();

            // 📌 안내 메시지
            string defaultMessage = state switch
            {
                BattleState.Encounter => "대상을 선택해주세요.",
                _ => "원하시는 행동을 입력해주세요."
            };
            message = isWrong ? "❌ 잘못된 입력입니다." : $"👉 {defaultMessage}";

            Console.WriteLine(message);
            Console.Write(">> ");
        }

    }
}
