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
            Console.WriteLine("📦 인벤토리 목록");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━");
            inventory.HasItemEquipped(showIdx);
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }

        public static void DisplayEquip(Inventory inventory, bool showIdx) // 장착관리 목록
        {
            Console.WriteLine("📦 인벤토리 목록 - 장착 관리");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━");
            inventory.HasItemEquipped(showIdx);
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }
    }
}
