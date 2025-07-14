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
        Player player = new Player();

        bool isWrong;
        int choice;
        string message;
        int deadCount;
        int currentHp;

        public Battle()
        {
            StartUI(player);
        }

        void StartUI(Player player)
        {
            currentHp = player.hp;
            if(monsterSpanwed.Count == 0)
            {
                SpawnMonster();
            }
            deadCount = 0;
            for (int i = 0; i < monsterSpanwed.Count; i++)
            {
                if (monsterSpanwed[i].hp <= 0)
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
                    string hpMessage = (monster.hp <= 0) ? "Dead" : $"HP {monster.hp}";
                    Console.WriteLine($"Lv.{monster.level:D2} {monster.name} {hpMessage}");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.level:D2} {player.name} ({player.job})");
                Console.WriteLine($"HP {player.hp}/100");
                //플레이어 정보
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine();
                message = (isWrong == true) ? "잘못된 입력입니다." : "원하시는 행동을 입력해주세요.";
                Console.WriteLine(message);
                Console.Write(">>");
                isWrong = ChoiceCheck(1, 1);

                switch (choice)
                {
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
                if (monsterSpanwed[i].hp <= 0)
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
                    string hpMessage = (monster.hp <=0) ? "Dead" : $"HP {monster.hp}";
                    Console.WriteLine($"{i + 1} Lv.{monster.level:D2} {monster.name} {hpMessage}");
                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.level:D2} {player.name} ({player.job})");
                Console.WriteLine($"HP {player.hp}/100");
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
                    if (monsterSpanwed[0].hp <= 0)
                    {
                        isWrong = true;
                        AttackUI(player);
                    }
                    else
                        PlayerPhase(player, monsterSpanwed[0]);
                    break;
                case 2:
                    if (monsterSpanwed[1].hp <= 0)
                    {
                        isWrong = true;
                        AttackUI(player);
                    }
                    else
                        PlayerPhase(player, monsterSpanwed[1]);
                    break;
                case 3:
                    if (monsterSpanwed[2].hp <= 0)
                    {
                        isWrong = true;
                        AttackUI(player);
                    }
                    else
                        PlayerPhase(player, monsterSpanwed[2]);
                    break;
                case 4:
                    if (monsterSpanwed[3].hp <= 0)
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
            int atkDamage = random.Next((int)Math.Ceiling((float)player.atk * 0.9f), (int)Math.Ceiling((float)player.atk * 1.1f));

            Console.WriteLine($"{player.name} 의 공격!");
            Console.WriteLine($"Lv.{monster.level:D2}{monster.name} 을(를) 맞췄습니다. [데미지 : {atkDamage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.level:D2}{monster.name}");
            Console.Write($"HP {monster.hp} -> ");
            monster.hp -= atkDamage;
            string resultMessage = (monster.hp <= 0) ? "Dead" : $"{monster.hp}";
            Console.WriteLine(resultMessage);
            Console.WriteLine();
            Console.WriteLine("다음");
            Console.Write(">>");
            Console.ReadLine();
            EnemyPhase(player, monster);
        }

        void EnemyPhase(Player player, Monster monster)
        {
            Console.Clear();
            int atkDamage = random.Next((int)Math.Ceiling((float)monster.atk * 0.9f), (int)Math.Ceiling((float)monster.atk * 1.1f));

            Console.WriteLine($"{monster.name} 의 공격!");
            Console.WriteLine($"{player.name} 을(를) 맞췄습니다. [데미지 : {atkDamage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.level:D2}{player.name}");
            Console.Write($"HP {player.hp} -> ");
            player.hp -= atkDamage;
            if(player.hp <=0)
                player.hp = 0;
            Console.WriteLine(player.hp);
            Console.WriteLine();
            Console.WriteLine("다음");
            Console.Write(">>");
            Console.ReadLine();
            if(player.hp <= 0)
                result(player);
            else
                AttackUI(player);
        }

        void result(Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            if(player.hp > 0)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Victory");
                    Console.WriteLine();
                    Console.WriteLine($"던전에서 몬스터를 {monsterSpanwed.Count}마리를 잡았습니다.");
                    Console.WriteLine();
                    Console.WriteLine($"Lv{player.level:D2} {player.name}");
                    Console.WriteLine($"HP {currentHp}-> {player.hp}");
                    Console.WriteLine();
                    Console.WriteLine("1. 던전 탐사\n0. 마을");
                    Console.WriteLine();
                    message = (isWrong == true) ? "잘못된 입력입니다." : "원하시는 행동을 입력해주세요.";
                    Console.WriteLine(message);
                    Console.Write(">>");
                    for(int i = monsterSpanwed.Count-1; i >= 0;i--)
                    {
                        monsterSpanwed.RemoveAt(i);
                    }
                    isWrong = ChoiceCheck(0, 1);

                    switch (choice)
                    {
                        case 0:

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
                Console.WriteLine($"Lv{player.level:D2} {player.name}");
                Console.WriteLine($"HP  -> 0");
                Console.WriteLine();
                Console.WriteLine("다음");
                Console.WriteLine(">>");
                for (int i = monsterSpanwed.Count - 1; i >= 0; i--)
                {
                    monsterSpanwed.RemoveAt(i);
                }
                Console.ReadLine();
            }
        }

        class Player()
        {
            public int level = 1;
            public string name = "Chad";
            public string job = "전사";
            public int hp = 100;
            public int atk = 10;
        }

        public class Monster()
        {
            public int level;
            public int hp;
            public int atk;
            public string name;

        }
        List<Monster> monsterData = new List<Monster>()
        {
            new() {level = 2, hp = 15, atk = 5, name = "미니언"},
            new() {level = 3, hp = 10, atk = 9, name = "공허충"},
            new() {level = 5, hp = 25, atk = 8, name = "대포미니언"},
        };

        List<Monster> monsterSpanwed = new List<Monster>();

        

        void SpawnMonster()
        {
            int monNum = random.Next(1, 5);
            for (int i = 0; i < monNum; i++)
            {
                int monId = random.Next(0, 3);
                Monster baseMon = monsterData[monId];
                Monster newMon = new Monster()
                {
                    name = baseMon.name,
                    level = baseMon.level,
                    hp = baseMon.hp,
                    atk = baseMon.atk,
                };

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
