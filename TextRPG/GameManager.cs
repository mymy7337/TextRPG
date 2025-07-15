using System;
using System.Collections.Generic;

class Player
{
    public string Name;
    public string Job;
    public int[] Stats; // [공격력, 방어력, 민첩, 체력]
    public int HP;
    public int MaxHP = 100;
    public int Gold = 100;
    public List<string> Inventory;

    public Player(string name, string job, int[] stats)
    {
        Name = name;
        Job = job;
        Stats = stats;
        HP = stats[3] * 20;      // 초기 체력 = 체력 스탯 영향받음
        HP = MaxHP;
        Inventory = new List<string>();
    }
}

class Program
{
    static void Main()
    {
        Player player = null;
        string[] jobNames = { "검사", "마법사", "궁수", "도적", "해적" };
        string[] statNames = { "공격력", "방어력", "민첩", "체력" };
        bool exitGame = false;

        while (!exitGame)
        {
            if (player == null)
            {
                Console.Write("플레이어 이름을 입력하세요: ");
                string playerName = Console.ReadLine();

                int selectedJobIndex = -1;
                while (selectedJobIndex < 0 || selectedJobIndex >= jobNames.Length)
                {
                    Console.Clear();
                    Console.WriteLine("직업을 선택하세요:");
                    for (int i = 0; i < jobNames.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {jobNames[i]}");
                    }

                    Console.Write("\n번호를 입력하세요 (1~5): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int parsedInput) &&
                        parsedInput >= 1 && parsedInput <= jobNames.Length)
                    {
                        selectedJobIndex = parsedInput - 1;
                    }
                    else
                    {
                        Console.WriteLine("1번부터 5번 중에 골라주세요.");
                        Console.ReadKey();
                    }
                }

                string chosenJob = jobNames[selectedJobIndex];
                int[] stats = GetJobStats(chosenJob);
                player = new Player(playerName, chosenJob, stats);
            }

            bool stayOnScreen = true;


            while (stayOnScreen)
            {
                Console.Clear();
                Console.WriteLine("======최종 정보======\n");
                Console.WriteLine($"이름: {player.Name}");
                Console.WriteLine($"직업: {player.Job}");
                Console.WriteLine("\n[스탯 정보]");

                for (int i = 0; i < statNames.Length; i++)
                {
                    Console.WriteLine($"{statNames[i]}: {player.Stats[i]}");
                }

                Console.WriteLine($"\n현재 체력: {player.HP} / {player.MaxHP}");
                Console.WriteLine($"보유 골드: {player.Gold}");

                Console.WriteLine("\n[B] 직업 변경 | [Enter] 게임 시작");
                Console.Write("선택: ");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "B")
                {
                    player = null;
                    stayOnScreen = false;
                }
                else if (choice == "")
                {
                    Console.Clear();
                    LoadMainScene(player);
                    Console.ReadLine();
                    exitGame = true;
                    stayOnScreen = false;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 'B' 또는 엔터 키를 입력하세요.");
                    Console.ReadKey();
                }
            }
        }
    }

    static int[] GetJobStats(string jobName)
    {
        switch (jobName)
        {
            case "검사": return new int[] { 7, 6, 2, 7 };
            case "마법사": return new int[] { 9, 2, 3, 5 };
            case "궁수": return new int[] { 8, 3, 6, 4 };
            case "도적": return new int[] { 10, 2, 8, 4 };
            case "해적": return new int[] { 6, 5, 4, 5 };
            default: return new int[] { 5, 5, 5, 5 };
        }
    }

    static void LoadMainScene(Player player)
    {
        Console.Clear();
        Console.WriteLine($"환영합니다, {player.Name} {player.Job}님!");
        Console.WriteLine("========================================");

        string[] statNames = { "공격력", "방어력", "민첩", "체력" };
        Console.WriteLine("\n[스탯 정보]");
        for (int i = 0; i < statNames.Length; i++)
        {
            Console.WriteLine($"{statNames[i]}: {player.Stats[i]}");
        }

        Console.WriteLine($"\n현재 체력: {player.HP} / {player.MaxHP}");
        Console.WriteLine($"보유 골드: {player.Gold}");

        Console.WriteLine("\n1. 마을 방문");
        Console.WriteLine("2. 던전 탐험");
        Console.WriteLine("3. 인벤토리 확인");
        Console.WriteLine("4. 게임 종료");
        Console.Write("\n선택: ");

        string choice = Console.ReadLine();
        if (choice == "1")
        {
            VisitTown(player);
        }
        else if (choice == "4")
        {
            Console.WriteLine("게임을 종료합니다.");
        }
        else
        {
            Console.WriteLine("수정 해야할 부분");
            Console.ReadKey();
        }
    }

    static void VisitTown(Player player)
    {
        bool stayInTown = true;

        while (stayInTown)
        {
            Console.Clear();
            Console.WriteLine("마을에 오신 것을 환영합니다.");
            Console.WriteLine("1. 상점");
            Console.WriteLine("2. 여관에서 휴식 (20골드)");
            Console.WriteLine("3. 돌아가기");

            Console.Write("선택: ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                OpenShop(player);
            }
            else if (input == "2")
            {
                if (player.Gold >= 20)
                {
                    player.Gold -= 20;
                    player.HP = player.MaxHP;
                    Console.WriteLine("체력이 모두 회복되었습니다!");
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다.");
                }
                Console.ReadKey();
            }
            else if (input == "3")
            {
                stayInTown = false;
                LoadMainScene(player);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
            }
        }
    }

    static void OpenShop(Player player)
    {
        Dictionary<string, string[]> shopItems = new Dictionary<string, string[]>
        {
            { "검사", new[] { "강철 검 (+공격력)", "철갑옷 (+방어력)" } },
            { "마법사", new[] { "마법봉 (+공격력)", "마법 로브 (+방어력)" } },
            { "궁수", new[] { "롱보우 (+공격력)", "가죽 갑옷 (+방어력)" } },
            { "도적", new[] { "단검 (+공격력)", "후드 조끼 (+방어력)" } },
            { "해적", new[] { "머스킷 (+공격력)", "해적 조끼 (+방어력)" } },
        };

        Console.Clear();
        Console.WriteLine("상점입니다.\n");
        string[] items = shopItems.ContainsKey(player.Job) ? shopItems[player.Job] : new string[] { "기본 무기", "기본 방어구" };
        int[] prices = { 50, 40 };

        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]} - {prices[i]} 골드");
        }

        Console.Write("구매할 아이템 번호를 입력하세요 (0: 취소): ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int choice) && choice >= 1 && choice <= items.Length)
        {
            if (player.Gold >= prices[choice - 1])
            {
                player.Gold -= prices[choice - 1];
                Console.WriteLine($"{items[choice - 1]}을 구매했습니다!");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다.");
            }
        }
        else
        {
            Console.WriteLine("구매를 취소합니다.");
            LoadMainScene(player);
        }

        Console.ReadKey();
    }
}
