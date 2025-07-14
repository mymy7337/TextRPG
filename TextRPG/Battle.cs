using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Battle
    {
        Random random = new Random();

        bool isWrong;
        bool isMade;
        int choice;
        string message;

        public Battle()
        {
            StartUI();
        }

        void StartUI()
        {
            SpawnMonster();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine("");

                for (int i = 0; i < monsterSpanwed.Count; i++)
                {
                    Monster monster = monsterSpanwed[i];
                    Console.WriteLine($"Lv.{monster.level:D2} {monster.name} HP {monster.hp}");
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[내정보]");
                //플레이어 정보
                Console.WriteLine("");
                Console.WriteLine("1. 공격");
                Console.WriteLine("");
                message = (isWrong == true) ? "잘못된 입력입니다." : "원하시는 행동을 입력해주세요.";
                Console.WriteLine(message);
                Console.Write(">>");
                isWrong = ChoiceCheck(1, 1);

                switch (choice)
                {
                    case 1:
                        AttackUI();
                        break;
                }
            }

        }

        void AttackUI()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine("");
            for (int i = 0; i < monsterSpanwed.Count; i++)
            {
                Monster monster = monsterSpanwed[i];
                Console.WriteLine($"{i + 1} Lv.{monster.level:D2} {monster.name} HP {monster.hp}");
            }
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            //플레이어 정보
            Console.WriteLine("");
            Console.WriteLine("0. 취소");
            Console.WriteLine("");
            message = (isWrong == true) ? "잘못된 입력입니다." : "대상을 선택해주세요.";
            Console.WriteLine(message);
            Console.Write(">>");
            isWrong = ChoiceCheck(0, 4);
            switch (choice)
            {

            }

        }

        void PlayerPhase()
        {

        }

        void EnemyPhase()
        {

        }

        void result()
        {

        }

        struct Monster()
        {
            public int level;
            public int hp;
            public int atk;
            public string name;
        }
        List<Monster> monsterData = new List<Monster>()
        {
            new Monster {level = 2, hp = 15, atk = 5, name = "미니언"},
            new Monster {level = 3, hp = 10, atk = 9, name = "공허충"},
            new Monster {level = 5, hp = 25, atk = 8, name = "대포미니언"},
        };

        List<Monster> monsterSpanwed = new List<Monster>();

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

        void SpawnMonster()
        {
            int monNum = random.Next(1, 5);
            for (int i = 0; i < monNum; i++)
            {
                int monId = random.Next(0, 3);
                monsterSpanwed.Add(monsterData[monId]);
            }
        }
    }
}
