using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.ItemFolder;

namespace TextRPG
{
    //public enum JobType // 직업 Enum
    //{
    //    Warrior,
    //    Wizard,
    //    Archer,
    //    Thief,
    //    Pirate
    //}
    public class Player
    {
        //player 기본상태
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; } // 최대체력: 오버힐 방지에
        public int MaxMp { get; set; } // 최대마력: 오버힐 방지
        public int Gold { get; set; }

        public int Exp { get; private set; } // 경험치

        public int ExtraAtk { get;  set; } // 추가공격력
        public int ExtraDef { get;  set; } // 추가방어력
        public int CritChance { get; private set; } = 15; // 치명타 확률
        public float CritMultiplier { get; private set; } = 1.6f; //치명타 피해
        public int DodgeChance { get; private set; } = 10; // 회피 확률



        public int FinalAtk
        {
            get
            {
                return Atk + ExtraAtk;
            }
        }
        public int FinalDef
        {
            get
            {
                return Def + ExtraDef;
            }
        }

        Random rand = new Random(); // 난수 생성(공격력 및 여러 난수) 

        //인벤토리 공간
        //List<Item>Inventory = new List<Item>(); 
        //List<Item>EquipList = new List<Item>(); 
        public List<Item> Inventory { get; private set; } = new List<Item>();
        public Item EquippedWeapon { get; set; }
        public Item EquippedArmor { get; set; }

        /*
        public int InventoryCount // 인벤토리 아이템 갯수
            {
            get
                {
                    return Inventory.Count;
                }
            }

        */
        // 
        public Player(int level, string name, string job, int atk, int def, int maxHp, int gold) // 플레이어 초기값
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = maxHp; // 체력 초기값은 최대체력
            MaxHp = maxHp; 
            Gold = gold;
        }

        public void Attack(Monster target) // 플레이어의 공격 행동
        {
            Console.WriteLine($"{Name} 의 공격!");
            double errorRate = rand.NextDouble() * 0.2 + 0.9; // 공격력 오차 0.9~1.1
            int finalAtk = (int)Math.Ceiling(Atk * errorRate);
            bool isCritical = rand.Next(0, 100) < CritChance; // 15% 치명타 확률
            bool isDodge = rand.Next(0, 100) < DodgeChance; // 10% 회피율

            if (isDodge)
            {
                Console.WriteLine($"Lv.{target.Level:D2} {target.Name} 을(를) 공격했지만 아무일도 일어나지 않았다.");
                return;
            }
           
            if (isCritical)
            { 
                finalAtk = (int)Math.Ceiling(finalAtk * CritMultiplier);
                Console.WriteLine($"Lv.{target.Level:D2} {target.Name} 을(를) 맞췄습니다. [데미지 : {finalAtk}] - 치명타 공격!!");
            }
            else
            {
                Console.WriteLine($"Lv.{target.Level:D2} {target.Name} 을(를) 맞췄습니다. [데미지 : {finalAtk}]");
            }
            target.TakeDamage(finalAtk);
        }

        public void Heal(int hpAmount = 0, int mpAmount = 0) // hp/mp 회복
        {
            if (hpAmount > 0)
            {
                Hp += hpAmount;
                if (Hp > MaxHp) Hp = MaxHp;
            }

            if (mpAmount > 0)
            {
                Mp += mpAmount;
                if (Mp > MaxMp) Mp = MaxMp;
            }
        }


        public void TakeDamage(int amount) //데미지를 받으면 hp 감소
        {
            int finalDamage = amount; //플레이어의 방어력 만큼 데미지 감소
            if (finalDamage  <= 0)
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

        /*
        public void DisplayInventory(bool showIdx)
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("🎒 소지한 아이템이 없습니다.");
                return;
            }

            Console.WriteLine("📦 인벤토리 목록");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━");
            for (int i = 0; i < Inventory.Count; i++)
            {
                Item targetItem = Inventory[i];
                string displayIdx = showIdx ? $"{i + 1}. " : "";
                string displayEquipped = IsEquipped(targetItem) ? "[E] " : "";
                Console.WriteLine($"- {displayIdx}{displayEquipped}{targetItem.ItemInfoText()}");
            }
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━");
        }

        //아이템 장착
        public void EquipItem(Item item) // 아이템 타입을 숫자로 받아오는걸 상정했음. 아이템에 붙은 추가 스텟만큼 추가 공격력 방어력이 증가하는 형태
        {
            if (IsEquipped(item))
            {
                EquipList.Remove(item);
                //if (item.Type == 0)
                //    ExtraAtk -= item.Value;
                //else
                //    ExtraDef -= item.Value;
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

        public bool IsEquipped(Item item) // 아이템 장착 여부 판단
        {
            return EquipList.Contains(item);
        }

        public bool HasItem(Item item) // 아이템 소지 여부 판단
        {
            return Inventory.Contains(item);
        }
        public void AddItem(Item item) // 인벤토리에 아이템 추가
        {
            Inventory.Add(item);
        }
        */

        public bool UseGold(int amount) // 골드 사용
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                return true;
            }
            return false;
        }
        public void AddItem(Item item)
        {
            if (!Inventory.Contains(item))
            {
                Inventory.Add(item);
                item.Price = 0; // 아이템을 "보유 상태"로 변경
            }
        }

        public void AddGold(int amount) // 골드 획득
        {
            Gold += amount;
        }

        public void GetExp(int amount) // 경험치 획득
        {
            Exp += amount;
            while (Exp >= GetRequiredExp(Level))
            {
                Exp -= GetRequiredExp(Level);
                LevelUp();
            }
        }

        public void LevelUp() // 레벨 업
        {
            Level++;
            MaxHp += 10;
            //MaxMp += 5;
            Hp = MaxHp;
            //Mp = MaxMp;
        }

        private int GetRequiredExp(int level) // 레벨업 필요 경험치량
        {
            switch (level)
            {
                case 1: return 10;
                case 2: return 35;
                case 3: return 65;
                case 4: return 100;
                default: return (int)(100 + Math.Pow(level - 4, 2) * 20); // 등차수열 적용
            }
        }
    }
}
