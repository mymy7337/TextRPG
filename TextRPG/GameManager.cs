using System;
using System.Numerics;
    
namespace TextRPG;

public class GameManager
    {
    private Player player;
    private Shop shop = new Shop();
    private Battle battle = new Battle();

    private string[] jobNames = { "검사", "마법사", "궁수", "도적", "해적" };
    private string[] statNames = { "공격력", "방어력", "민첩", "체력" };

    public void Start()
    {
        Console.Title = "TextRPG";
        CreatePlayer();
        ShowFinalInfo(); // 스탯 확인 후 메인 씬으로 이동
    }

    private void CreatePlayer()
    {
        Console.Clear();
        Console.WriteLine("플레이어를 생성합니다.\n");
        Console.Write("이름을 입력하세요: ");
        string name = Console.ReadLine();

        int selectedJobIndex = -1;
        while (selectedJobIndex < 0 || selectedJobIndex >= jobNames.Length)
        {
            Console.Clear();
            Console.WriteLine("직업을 선택하세요:");
            for (int i = 0; i < jobNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {jobNames[i]}");
            }

            Console.Write("\n번호 입력 (1~5): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int parsedInput) && parsedInput >= 1 && parsedInput <= jobNames.Length)
            {
                selectedJobIndex = parsedInput - 1;
            }
            else
            {
                Console.WriteLine("유효한 번호를 입력해주세요.");
                Console.ReadKey();
            }
        }

        string chosenJob = jobNames[selectedJobIndex];
        int[] stats = GetJobStats(chosenJob);
        player = new Player(01, name, chosenJob, 10, 5, 100, 1500);
    }

    private int[] GetJobStats(string job) // 주석처리 가능성 있음
    {
        return job switch
        {
            "검사" => new[] { 7, 6, 2, 7 },
            "마법사" => new[] { 9, 2, 3, 5 },
            "궁수" => new[] { 8, 3, 6, 4 },
            "도적" => new[] { 10, 2, 8, 4 },
            "해적" => new[] { 6, 5, 4, 5 },
            _ => new[] { 5, 5, 5, 5 },
        };
    }

    private void ShowFinalInfo()
    {
        bool stay = true;

        while (stay)
        {
            Console.Clear();
            Console.WriteLine("====== 최종 정보 ======\n");
            Console.WriteLine($"이름: {player.Name}");
            Console.WriteLine($"직업: {player.Job}");

            Console.WriteLine("\n[스탯 정보]");
            player.DisplayPlayerInfo();
            
            Console.WriteLine("\n[B] 직업 변경  |  [Enter] 게임 시작");
            Console.Write("선택: ");
            string input = Console.ReadLine().ToUpper();

            if (input == "B")
            {
                CreatePlayer();
                continue;
            }
            else if (input == "")
            {
                LoadMainScene();
                stay = false;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
            }
        }
    }

    private void LoadMainScene()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            player.DisplayPlayerInfo();

            Console.WriteLine("\n1. 마을 방문");
            Console.WriteLine("2. 던전 탐험");
            Console.WriteLine("3. 인벤토리 확인");
            Console.WriteLine("4. 게임 종료");
            Console.Write("\n선택: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    VisitTown();
                    break;
                case "2":
                    battle.StartUI(player);
                    break;
                case "3":
                    player.DisplayInventory(true); // 임시
                    break;
                case "4":
                    running = false;
                    Console.WriteLine("게임을 종료합니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void VisitTown()
    {
        bool inTown = true;

        while (inTown)
        {
            Console.Clear();
            Console.WriteLine("마을에 오신 것을 환영합니다");
            Console.WriteLine("1. 상점");
            Console.WriteLine("2. 여관에서 휴식 (20골드)");
            Console.WriteLine("3. 돌아가기");
            Console.Write("선택: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Shop.OpenShop();
                    break;
                case "2":
                    if (player.Gold >= 20)
                    {
                        player.Gold -= 20;
                        player.Hp = player.MaxHp;
                        Console.WriteLine("체력이 회복되었습니다!");
                    }
                    else
                    {
                        Console.WriteLine(" 골드가 부족합니다!");
                    }
                    Console.ReadKey();
                    break;
                case "3":
                    inTown = false;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }