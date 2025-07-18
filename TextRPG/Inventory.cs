using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Inventory
    {
        List<Item> Items = new List<Item>();
        List<Item> EquipList = new List<Item>();

        public int InventoryCount => Items.Count; // 아이템 소지 갯수

        public void HasItemEquipped(bool showIdx)
        {
            if (Items.Count == 0)
            {
                //Console.WriteLine("소지한 아이템이 없습니다.");
                return;
            }
            for (int i = 0; i < Items.Count; i++)
            {
                Item targetItem = Items[i];
                string displayIdx = showIdx ? $"{i + 1} " : "";
                string displayEquipped = IsEquipped(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}"); // - 아이템 번호 [E] 아이템 정보
            }
        }

        public bool IsEquipped(Item item) // 아이템 장착 여부 판단
        {
            return EquipList.Contains(item);
        }

        public bool HasItem(Item item) // 아이템 소지 여부 판단
        {
            return Items.Contains(item);
        }

        public void EquipItem(Item item) // 아이템 타입을 숫자로 받아오는걸 상정했음. 아이템에 붙은 추가 스텟만큼 추가 공격력 방어력이 증가하는 형태
        {
            if (IsEquipped(item))
            {
                EquipList.Remove(item);
                //if (item.Type == 0)
                //    player.ExtraAtk -= item.Value;
                //else
                //    player.ExtraDef -= item.Value;
            }
            else
            {
                EquipList.Add(item);
                //if (item.Type == 0)
                //    ExtraAtk += item.Value;
                //else
                //    ExtraDef += item.Value;
            }
        }

        public void AddItem(Item item) // 인벤토리에 아이템 추가
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item) // 아이템 제거
        {
            Items.Remove(item);
        }
    }
}
