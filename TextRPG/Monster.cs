using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Monster
    {
        //몬스터 필드 설정
        public int Level { get; set; }
        public string Name { get; set; }
        public int Atk { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }

        public Monster(int level, string name, int atk, int maxHp) 
        { 
            Level = level;
            Name = name;
            Atk = atk;
            Hp = maxHp;
            MaxHp = maxHp;
        }

        
        public void DisplayMonsterInfo() //몬스터 상태 정보 표시
        {
            Console.WriteLine($"Lv.{Level} {Name} HP {Hp}");
        }

        public void TakeDamage(int amount) //데미지를 받으면 hp 감소
        {
            if (amount <= 0)
            {
                return;
            }
            else
            {
                Hp -= amount;
            }
            if (Hp < 0) // hp가 0 이하가 되지 않게 설정
            {
                Hp = 0;
            }
        }

        public void DisplayBattleInfo() // 전투 시작 전 몬스터 정보
        {
            Console.WriteLine($"Lv. {Level} {Name}");
            Console.WriteLine($"Hp {Hp}/{MaxHp}");
        }

        public void DisplayHpInfo() // 전투 시 몬스터 Hp 변화 정보 표시
        {
            Console.WriteLine($"Lv. {Level} {Name}");
            string nowHp = Hp <= 0 ? "Dead" : Hp.ToString(); // hp가 0 이하면 Dead 표시
            Console.WriteLine($"Hp {Hp} -> {nowHp}");
        }
    }
}
