using System;
using System.Collections.Generic;
using System.Linq;
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

        bool isWrong;
        int choice;
        string message;
        int deadCount;
        int currentHp;

        public void StartUI(Player player)
        {
            currentHp = player.Hp;
            if(monsterSpanwed.Count == 0)
            {
                SpawnMonster();
            }
            deadCount = 0;
            for (int i = 0; i < monsterSpanwed.Count; i++)
            {
                if (monsterSpanwed[i].Hp <= 0)
                    deadCount++;
            }
            if (deadCount == monsterSpanwed.Count)
                result(player);
                
            while (true)
            {
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

                switch (choice)
                {
                    case 0:
                        GameManager gm = new GameManager();
                        gm.LoadMainScene(player);
                        break;
                    case 1:
                        AttackUI(player);
                        break;
                }
            }

        }

        void AttackUI(Player player)
        {
            deadCount = 0;
            for(int i = 0; i < monsterSpanwed.Count; i++)
            {
                if (monsterSpanwed[i].Hp <= 0)
                    deadCount++;
            }
            if (deadCount == monsterSpanwed.Count)
                result(player);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                for (int i = 0; i < monsterSpanwed.Count; i++)
                {
                    Monster monster = monsterSpanwed[i];
                    Console.Write($"{i+1} ");
                    monster.DisplayMonsterInfo();
                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level:D2} {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Hp}/100");
                //플레이어 정보
                Console.WriteLine();
                Console.WriteLine("0. 취소");
                Console.WriteLine();
                message = (isWrong == true) ? "잘못된 입력입니다." : "대상을 선택해주세요.";
                Console.WriteLine(message);
                Console.Write(">>");
                isWrong = ChoiceCheck(0, monsterSpanwed.Count);

                if (isWrong == false)
                    break;
            }
            switch(choice)
            {
                case 0:
                    StartUI(player);
                    break;
                case 1:
                    if (monsterSpanwed[0].Hp <= 0)
                    {
                        isWrong = true;
                        AttackUI(player);
                    }
                    else
                        PlayerPhase(player, monsterSpanwed[0]);
                    break;
                case 2:
                    if (monsterSpanwed[1].Hp <= 0)
                    {
                        isWrong = true;
                        AttackUI(player);
                    }
                    else
                        PlayerPhase(player, monsterSpanwed[1]);
                    break;
                case 3:
                    if (monsterSpanwed[2].Hp <= 0)
                    {
                        isWrong = true;
                        AttackUI(player);
                    }
                    else
                        PlayerPhase(player, monsterSpanwed[2]);
                    break;
                case 4:
                    if (monsterSpanwed[3].Hp <= 0)
                    {
                        isWrong = true;
                        AttackUI(player);
                    }
                    else
                        PlayerPhase(player, monsterSpanwed[3]);
                    break;
            }

        }

        void PlayerPhase(Player player, Monster monster)
        {
            Console.Clear();
            int prevHp = monster.Hp; //  추가
            monster.DisplayHpInfo(prevHp); // 매개변수 추가
            monster.DisplayHpInfo();
            Console.WriteLine();
            Console.WriteLine("다음");
            Console.Write(">>");
            Console.ReadLine();
            EnemyPhase(player, monster);
        }

        void EnemyPhase(Player player, Monster monster)
        {
            Console.Clear();
            monster.Attack(player);
            int prevHp = player.Hp; //  추가
            player.DisplayHpInfo(prevHp); // 매개변수 추가
            Console.WriteLine();
            Console.WriteLine("다음");
            Console.Write(">>");
            Console.ReadLine();
            if(player.Hp <= 0)
                result(player);
            else
                AttackUI(player);
        }

        void result(Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            if(player.Hp > 0)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Victory");
                    Console.WriteLine();
                    Console.WriteLine($"던전에서 몬스터를 {monsterSpanwed.Count}마리를 잡았습니다.");
                    Console.WriteLine();
                    Console.WriteLine($"Lv{player.Level:D2} {player.Name}");
                    Console.WriteLine($"HP {currentHp}-> {player.Hp}");
                    Console.WriteLine();
                    Console.WriteLine("1. 던전 탐사\n0. 마을");
                    Console.WriteLine();
                    message = (isWrong == true) ? "잘못된 입력입니다." : "원하시는 행동을 입력해주세요.";
                    Console.WriteLine(message);
                    Console.Write(">>");
                    monsterSpanwed.Clear();
                    isWrong = ChoiceCheck(0, 1);

                    switch (choice)
                    {
                        case 0:
                            GameManager gm = new GameManager();
                            gm.LoadMainScene(player);
                            break;
                        case 1:
                            StartUI(player);
                            break;
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
                Console.WriteLine("다음");
                Console.WriteLine(">>");
                monsterSpanwed.Clear();
                Console.ReadLine();
            }
        }
        List<Monster> monsterSpanwed = new List<Monster>();

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
