using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Battle
    {
        Random random = new Random();
        enum BattleState
        {
            Main,
            Encounter,
            Exit
        }

        bool isWrong;
        int choice;
        string message = "";
        int deadCount;
        int nowHp;

        public void BattleStart(Player player)
        {
            BattleState state = BattleState.Main;

            while (state != BattleState.Exit)
            {
                switch (state)
                {
                    case BattleState.Main:
                        state = MainUI(player);
                        break;
                    case BattleState.Encounter:
                        state = EncounterUI(player);
                        break;
                }
            }
        }

        BattleState MainUI(Player player)
        {
            if (monsterSpanwed.Count == 0)
            {
                SpawnMonster();
            }
            nowHp = player.Hp;
            deadCount = 0;
            Monster monster;
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            for (int i = 0; i < monsterSpanwed.Count; i++)
            {
                monster = monsterSpanwed[i];
                monster.DisplayMonsterInfo();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            player.DisplayBattleInfo();
            Console.WriteLine();
            Console.WriteLine("1. 공격\n0. 돌아가기");
            Console.WriteLine();
            message = (isWrong == true) ? "잘못된 입력입니다." : "원하시는 행동을 입력해주세요.";
            Console.WriteLine(message);
            Console.Write(">>");
            isWrong = ChoiceCheck(0, 1);
            if (isWrong)
                return BattleState.Main;

            switch (choice)
            {
                case 0:
                    if(random.Next(0, 100) < (100 - monsterSpanwed.Count * 10))
                    {
                        monsterSpanwed.Clear();
                        return BattleState.Exit;
                    }
                    player.Hp -= monsterSpanwed.Count;
                    return BattleState.Main;
                    
                case 1:
                    return BattleState.Encounter;
                default:
                    return BattleState.Main;
            }

        }

        BattleState EncounterUI(Player player)
        {

            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsterSpanwed.Count; i++)
            {
                Monster monster = monsterSpanwed[i];
                Console.Write($"{i + 1} ");
                monster.DisplayMonsterInfo();
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            player.DisplayBattleInfo();
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            message = (isWrong == true) ? "잘못된 입력입니다." : "대상을 선택해주세요.";
            Console.WriteLine(message);
            Console.Write(">>");
            isWrong = ChoiceCheck(0, monsterSpanwed.Count);

            if (isWrong)
                return BattleState.Encounter;

            switch (choice)
            {
                case 0:
                    return BattleState.Main;
                case 1:
                    if (monsterSpanwed[0].Hp <= 0)
                    {
                        isWrong = true;
                        return BattleState.Encounter;
                    }
                    else
                        return PlayerPhase(player, monsterSpanwed[0]);
                case 2:
                    if (monsterSpanwed[1].Hp <= 0)
                    {
                        isWrong = true;
                        return BattleState.Encounter;
                    }
                    else
                        return PlayerPhase(player, monsterSpanwed[1]);
                case 3:
                    if (monsterSpanwed[2].Hp <= 0)
                    {
                        isWrong = true;
                        return BattleState.Encounter;
                    }
                    else
                        return PlayerPhase(player, monsterSpanwed[2]);
                case 4:
                    if (monsterSpanwed[3].Hp <= 0)
                    {
                        isWrong = true;
                        return BattleState.Encounter;
                    }
                    else
                        return PlayerPhase(player, monsterSpanwed[3]);
                default:
                    return BattleState.Encounter;
            }

        }

        BattleState PlayerPhase(Player player, Monster monster)
        {
            int prevHp = monster.Hp;
            Console.Clear();
            player.Attack(monster);
            monster.DisplayHpInfo(prevHp);
            Console.WriteLine();
            Console.WriteLine("다음");
            Console.Write(">>");
            Console.ReadKey();

            return EnemyPhase(player, monster);
        }

        BattleState EnemyPhase(Player player, Monster monster)
        {
            if (monster.Hp <= 0)
            {
                if (random.Next(1, 101) < 100)
                    getItem.Add(monster.Item);
                deadCount++;
                if(deadCount == monsterSpanwed.Count)
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
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Victory");
                    Console.WriteLine();
                    Console.WriteLine($"던전에서 몬스터를 {monsterSpanwed.Count}마리를 잡았습니다.");
                    Console.WriteLine();
                    Console.WriteLine($"HP {nowHp} -> {player.Hp}");
                    Console.WriteLine();
                    if(getItem.Count > 0)
                    {
                        foreach (var item in getItem)
                        {
                            Console.WriteLine($"{item.ItemName} 획득");
                            player.AddItem(item);
                        }
                    }
                    getItem.Clear();
                    Console.WriteLine();
                    Console.WriteLine("1. 던전 탐사\n0. 마을");
                    Console.WriteLine();
                    message = (isWrong == true) ? "잘못된 입력입니다." : "원하시는 행동을 입력해주세요.";
                    Console.WriteLine(message);
                    Console.Write(">>");
                    
                    isWrong = ChoiceCheck(0, 1);
                    if (isWrong)
                        continue;
                    monsterSpanwed.Clear();
                    switch (choice)
                    {
                        case 0:
                            return BattleState.Exit;
                        case 1:
                            return BattleState.Main;
                    }
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


    }
}
