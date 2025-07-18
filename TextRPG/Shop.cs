using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.ItemFolder;
using static System.Formats.Asn1.AsnWriter;


namespace TextRPG
{
    public class Shop
    {
        //public List<Item> Items { get; private set; } = new List<Item>();

        public enum ShopMenu
        {
            ShopMenu = 1,
            BuyItem = 2,
            SellItem = 3,
            Exit = 4,
        }

        public void ShowShopMenu(Player player)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║       🏪 상점 메뉴         ║");
            Console.WriteLine("╚════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("1. 📦 아이템 보기");
            Console.WriteLine("2. 💰 아이템 구매");
            Console.WriteLine("3. 🪙 아이템 판매");
            Console.WriteLine("4. 🔙 시작 화면으로");
            Console.Write("\n메뉴 선택: ");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int Sel))
            {
                ShopMenu menu = (ShopMenu)Sel;

                switch (menu)
                {
                    case ShopMenu.ShopMenu:
                        Console.Clear();
                        ShowShopItems(player);
                        //ShowShopMenu();
                        break;
                    case ShopMenu.BuyItem:
                        //Console.Clear();
                        BuyItem(player);
                        //ShowShopMenu();
                        break;
                    case ShopMenu.SellItem:
                        Console.Clear();
                        SellItem(player);
                        //ShowShopMenu();
                        break;
                    case ShopMenu.Exit:
                        //Console.Clear();
                        GameManager.instance.LoadMainScene(); // 시작 화면으로 돌아가기
                        //Program.StartGame();
                        break;
                    default:
                        //GameSystem.FaileInput();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n❌ 잘못된 입력입니다.");
                        Console.ResetColor();
                        ShowShopMenu(player);
                        //ShowShopMenu();
                        break;
                }
                //ShowShopMenu();
            }
            Console.WriteLine("\n아무 키나 누르면 상점 메뉴로 돌아갑니다...");
            Console.ReadKey();
            ShowShopMenu(player);
        }

        public void ShowShopItems(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                        🛍️ 상점 아이템 목록                                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine($"{AdjustWidth("이름", 18)} {AdjustWidth("종류", 8)} {AdjustWidth("스탯", 8)} {AdjustWidth("설명", 70)} {AdjustWidth("가격", 10)}");
            Console.WriteLine(new string('-', 120));

            foreach (var item in Item.Items)
            {
                string type = item.Type == ItemType.Weapon ? "공격력" : "방어력";
                string stat = $"+{item.StatValue}";
                string priceStr = item.Price == 0 ? "구매완료" : $"{item.Price} G";

                Console.ForegroundColor = item.Price != 0 ? ConsoleColor.Green : ConsoleColor.DarkGray; // 가격이 0이 아니면 초록색, 0이면 회색 삼항 연산자

                if (item.Price != 0) Console.ForegroundColor = ConsoleColor.Green; else Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.WriteLine($"{AdjustWidth(item.Name, 18)} {AdjustWidth(type, 8)} {AdjustWidth(stat, 8)} {AdjustWidth(item.Info, 70)} {AdjustWidth(priceStr, 10)}");
            }
            Console.ResetColor();

            Console.WriteLine(new string('-', 120));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"💰 소지 Gold  : {player.Gold} G");
            Console.ResetColor();
            Console.WriteLine("\n아무 키나 누르면 상점 메뉴로 돌아갑니다...");
            Console.ReadKey();
            ShowShopMenu(player);
        }

        void BuyItem(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                        🛍️ 상점 아이템 구매                                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine($"{AdjustWidth("번호", 4)} {AdjustWidth("이름", 18)} {AdjustWidth("종류", 8)} {AdjustWidth("스탯", 8)} {AdjustWidth("설명", 70)} {AdjustWidth("가격", 10)}");
            Console.WriteLine(new string('-', 120));

            for (int i = 0; i < Item.Items.Count; i++)
            {
                var item = Item.Items[i];
                string indexStr = $"{i + 1}.";
                string type = item.Type == ItemType.Weapon ? "공격력" : "방어력";
                string stat = $"+{item.StatValue}";
                string priceStr = item.Price == 0 ? "구매완료" : $"{item.Price} G";

                Console.ForegroundColor = item.Price != 0 ? ConsoleColor.Green : ConsoleColor.DarkGray; // 가격이 0이 아니면 초록색, 0이면 회색 삼항 연산자

                Console.WriteLine($"{AdjustWidth(indexStr, 4)} {AdjustWidth(item.Name, 18)} {AdjustWidth(type, 8)} {AdjustWidth(stat, 8)} {AdjustWidth(item.Info, 70)} {AdjustWidth(priceStr, 10)}");
            }
            Console.ResetColor(); // 원래 색상으로 되돌림
            Console.WriteLine(new string('-', 120));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"💰 소지 Gold  : {player.Gold} G");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("0. 뒤로가기");
            Console.Write("구매할 아이템 번호를 입력하세요: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= Item.Items.Count)
            {
                var selectedItem = Item.Items[choice - 1];
                if (selectedItem.Price == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.ResetColor();
                    Console.WriteLine("\n아무 키나 누르면 계속합니다...");
                    Console.ReadKey();    
                    Console.Clear();
                    BuyItem(player);
                    return;
                }
                else if (player.Gold >= selectedItem.Price)
                {
                    player.Gold -= selectedItem.Price;
                    selectedItem.Price = 0; // 구매 완료 처리

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n✅ {selectedItem.Name}을(를) 구매했습니다!");
                    Console.WriteLine($"💰 남은 Gold : {player.Gold} G");
                    Console.ResetColor();

                    Console.WriteLine("\n아무 키나 누르면 계속합니다...");
                    Console.ReadKey();

                    Console.Clear();
                    BuyItem(player);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Gold가 부족합니다.");
                    Console.WriteLine("\n아무 키나 누르면 계속합니다...");
                    Console.ReadKey();

                    Console.Clear();
                    BuyItem(player);
                }
                Console.ResetColor();
            }
            else if (choice != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ 잘못된 입력입니다. 다시 시도해주세요.\n");
                Console.ResetColor();
            }
            else if (choice == 0)
            {
                Console.Clear();
                ShowShopMenu(player);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        public void SellItem(Player player)
        {
            var inventoryItems = Item.Items.Where(i => i.Price == 0).ToList(); // ✅ 수정된 부분

            if (inventoryItems.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("인벤토리에 판매할 아이템이 없습니다.");
                Console.ResetColor();

                Console.WriteLine("\n아무 키나 누르면 상점 메뉴로 돌아갑니다...");
                Console.ReadKey();
                Console.Clear();
                ShowShopMenu(player);
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                           🛍️ 아이템 판매                                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine($"{AdjustWidth("번호", 4)} {AdjustWidth("이름", 18)} {AdjustWidth("종류", 8)} {AdjustWidth("스탯", 8)} {AdjustWidth("설명", 70)} {AdjustWidth("가격", 12)}");
            Console.WriteLine(new string('-', 120));

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                var item = inventoryItems[i];
                string indexStr = $"{i + 1}.";
                string type = item.Type == ItemType.Weapon ? "공격력" : "방어력";
                string stat = $"+{item.StatValue}";
                string priceStr = $"{(int)(item.OriginalPrice * 0.85)} G (판매가)";

                Console.WriteLine($"{AdjustWidth(indexStr, 4)} {AdjustWidth(item.Name, 18)} {AdjustWidth(type, 8)} {AdjustWidth(stat, 8)} {AdjustWidth(item.Info, 70)} {AdjustWidth(priceStr, 12)}");
            }

            Console.WriteLine(new string('-', 120));
            Console.WriteLine("0. 뒤로가기");
            Console.Write("판매할 아이템 번호를 입력하세요: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= inventoryItems.Count)
            {
                var selectedItem = inventoryItems[choice - 1];

                // 장착 중이면 해제 후 능력치 감소
                if (selectedItem.IsEquipped)
                {
                    selectedItem.IsEquipped = false;

                    if (selectedItem.Type == ItemType.Weapon)
                        player.ExtraAtk -= selectedItem.StatValue;
                    else if (selectedItem.Type == ItemType.Armor)
                        player.ExtraDef -= selectedItem.StatValue;
                }

                // 판매가 계산 (85%로)
                int sellPrice = (int)(selectedItem.OriginalPrice * 0.85);
                player.Gold += sellPrice;

                // 상태 초기화
                selectedItem.Price = selectedItem.OriginalPrice;

                // 인벤토리에서 제거
                inventoryItems.Remove(selectedItem);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n🧷 {selectedItem.Name}을(를) 판매했습니다!");
                Console.WriteLine($"💰 현재 Gold : {player.Gold} G");
                Console.ResetColor();

                Console.WriteLine("\n아무 키나 누르면 판매로 돌아갑니다...");
                Console.ReadKey();
                Console.Clear();
                SellItem(player);
            }
            else if (choice == 0)
            {
                Console.Clear();
                ShowShopMenu(player);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ 잘못된 입력입니다. 다시 시도해주세요.\n");
                Console.ResetColor();

                Console.WriteLine("\n아무 키나 누르면 다시 판매 화면으로 돌아갑니다...");
                Console.ReadKey();
                Console.Clear();
                SellItem(player); // 재진입
            }

            Console.WriteLine();
        }


        public static string AdjustWidth(string input, int totalWidth)
        {
            int width = input.Sum(c => c > 127 ? 2 : 1); // 한글은 2칸, 영문은 1칸
            int padding = totalWidth - width;
            return input + new string(' ', Math.Max(padding, 0));
        }

        public void InitializeItems()
        {
            Item.Items.Add(new Item("견습용 단검", "초보 모험가도 들기 쉬운 단검이다.", 200, ItemType.Weapon, 3));
            Item.Items.Add(new Item("고블린의 가죽갑옷", "적당히 몸에 맞게 수선해 입을 수 있을 것 같다. 몸통을 보호한다.", 500, ItemType.Armor, 6));
            Item.Items.Add(new Item("린넨 천갑옷", "천을 겹치면 날카로운 일격도 막을 수 있다. 활동성이 좋다.", 500, ItemType.Armor, 5));
            Item.Items.Add(new Item("강철 단검", "병정들이여, 일어서라. 견습용보다 튼튼하고 날카로운 단검이다.", 500, ItemType.Weapon, 6));
            Item.Items.Add(new Item("가죽 투구", "머리를 약간 보호해주는 가죽 재질의 투구.", 400, ItemType.Armor, 8));
            Item.Items.Add(new Item("가죽 신발", "다리를 약간 보호해주는 가죽 재질의 신발.", 400, ItemType.Armor, 8));
            Item.Items.Add(new Item("브루탈 메이스", "거칠고 야만적인 메이스. 두개골을 부술 수 있을 것 같다.", 700, ItemType.Weapon, 10));
            Item.Items.Add(new Item("투 핸디드 소드", "무게중심이 잘 잡힌 장인의 검이다. 공격력이 높다.", 1300, ItemType.Weapon, 15));
            Item.Items.Add(new Item("체인 메일", "고급 가죽을 덧댄 갑옷으로, 가볍고 튼튼하다.", 800, ItemType.Armor, 15));
            Item.Items.Add(new Item("용골 할버드", "용살자를 위하여 제작된 할버드. 실제 용골이 들어간 것은 아니다.", 3200, ItemType.Weapon, 25)); // 공격+방어 구분 없을 경우 Mixed 처리
            Item.Items.Add(new Item("무거운 판금갑옷", "높은 방어력을 제공하지만, 무겁다... 민첩하게 움직이긴 어려워진다.", 4000, ItemType.Armor, 40));

        }


    }
}