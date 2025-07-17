using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
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
        public int Gold { get; private set; }

        public int ExtraAtk { get; private set; } // 추가공격력
        public int ExtraDef { get; private set; } // 추가방어력
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
        List<Item>Inventory = new List<Item>(); 
        List<Item>EquipList = new List<Item>(); 

        public int InventoryCount // 인벤토리 아이템 갯수
            {
            get
                {
                    return Inventory.Count;
                }
            }

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

        public void DisplayPlayerInfo() // 플레이어 상태 정보 표시
        {
            Console.WriteLine($"Lv.{Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {FinalAtk}" + (ExtraAtk == 0 ? "" : $" (+{ExtraAtk})"));
            Console.WriteLine($"방어력 : {FinalDef}" + (ExtraDef == 0 ? "" : $" (+{ExtraDef})"));
            Console.WriteLine($"체 력 : {Hp}");
            Console.WriteLine($"Gold : {Gold}");
        }

        public void DisplayBattleInfo() // 전투 시작 전 플레이어 정보
        {
            Console.WriteLine($"Lv. {Level:D2} {Name} ({Job})");
            Console.WriteLine($"Hp {Hp}/{MaxHp}");
        }

        public void DisplayHpInfo(int previousHp) // 전투 시 Hp 변화 정보 표시
        {
            Console.WriteLine($"Lv. {Level:D2} {Name} ({Job})" );
            string nowHp = Hp <= 0 ? "Dead" : Hp.ToString(); // hp가 0 이하면 Dead 표시
            Console.WriteLine($"Hp {previousHp} -> {nowHp}");
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

        public void Heal(int amount) // 매개변수의 수치 만큼 회복 // mp추가시 매개 변수 하나더 추가
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

        public void DisplayInventory(bool showIdx) // 인벤토리 리스트(장착여부 및 번호표시 유무 설정)
        {
            if(Inventory.Count == 0)
            {
                //Console.WriteLine("소지한 아이템이 없습니다.");
                return;
            }
            for (int i = 0; i < Inventory.Count; i++)
            {
                Item targetItem = Inventory[i]; 
                string displayIdx = showIdx ? $"{i + 1} " : "";
                string displayEquipped = IsEquipped(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}"); // - 아이템 번호 [E] 아이템 정보
            }
        }
        //아이템 장착
        public void EquipItem(Item item) // 아이템 타입을 숫자로 받아오는걸 상정했음. 아이템에 붙은 추가 스텟만큼 추가 공격력 방어력이 증가하는 형태
        {
            if (IsEquipped(item))
            {
                EquipList.Remove(item);
                //if (item.Type == 0)
                //    ExtraAtk += item.Value;
                //else
                //    ExtraDef += item.Value;
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

        public bool UseGold(int amount) // 골드 사용
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                return true;
            }
            return false;
        }

        public void AddGold(int amount) // 골드 획득
        {
            Gold += amount;
        }
    }
}
