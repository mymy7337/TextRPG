using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public static class InventoryUI
    {
        public static void DisplayInventory(Inventory inventory, bool showIdx) // 인벤토리 목록
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            inventory.HasItemEquipped(showIdx);
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }

        public static void DisplayEquip(Inventory inventory, bool showIdx) // 장착관리 목록
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            inventory.HasItemEquipped(showIdx);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }
    }
}
