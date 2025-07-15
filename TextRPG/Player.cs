using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        //player 기본상태
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; } // 최대체력: 오버힐 방지에 필요
        public int Gold { get; private set; }

        public int ExtraAtk { get; private set; } // 추가공격력
        public int ExtraDef { get; private set; } // 추가방어력

        //인벤토리 공간
        List<int>Inventory = new List<int>(); // develop 브랜치에 머지 후 int -> item으로 변경
        List<int>EquipList = new List<int>(); // int -> item

        public int InventoryCount // 인벤토리 아이템 갯수
            {
            get
                {
                    return Inventory.Count;
                }
            }

        // 
        public Player(int level, string name, string job, int atk, int def, int maxHp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = maxHp; // 체력 초기값은 최대체력
            MaxHp = maxHp; 
            Gold = gold;
        } // 플레이어 초기값

        public void DisplayPlayerInfo() // 플레이어 상태 정보 표시
        {
            Console.WriteLine("상태보기");
            Console.WriteLine("플레이어의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Atk}");
            Console.WriteLine($"방어력 : {Def}");
            Console.WriteLine($"체 력 : {Hp}");
            Console.WriteLine($"Gold : {Gold}");
        }

        
        public void Heal(int amount) // 매개변수의 수치 만큼 회복
        {
            if (amount <= 0)
            {
                return;
            }
            else
            {
                Hp += amount;
            }
            if (Hp > MaxHp) // hp가 최대hp을 넘지 않는다.
            {
                Hp = MaxHp;
            }
        }
        
        
        public void TakeDamage(int amount) //데미지를 받으면 hp 감소
        {
            int finalDamage = amount - Def; //플레이어의 방어력 만큼 데미지 감소

            if(finalDamage  <= 0)
            {
                return;
            }
            else
            {
                Hp -= finalDamage;
            }
            if (Hp < 0) // hp가 0 이하가 되지 않게 설정
            {
                Hp = 0;
            }
        }

        public void DisplayInventory(bool showIdx) // 인벤토리 리스트(장착여부 및 번호표시 유무 설정)
        {
            if(Inventory.Count == 0)
            {
                //Console.WriteLine("소지한 아이템이 없습니다.");
                return;
            }
            for (int i = 0; i < Inventory.Count; i++)
            {
                int targetItem = Inventory[i]; //int -> item 으로 변경 필요 
                string displayIdx = showIdx ? $"{i + 1} " : "";
                string displayEquipped = IsEquipped(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}");
            }
        }
    }
}
