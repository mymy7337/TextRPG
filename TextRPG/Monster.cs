using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Monster
    {
        //몬스터 필드 설정
        public int Level { get; set; }
        public string Name { get; set; }
        public int Atk { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int DodgeChance { get; private set; } = 10; // 회피 확률

        Random rand = new Random(); // 난수 생성(공격력 및 여러 난수)

        private int previousHp;

        //public Monster(int level, string name, int atk, int maxHp) 
        //{ 
        //    Level = level;
        //    Name = name;
        //    Atk = atk;
        //    Hp = maxHp;
        //    MaxHp = maxHp;
        //}

        public Monster Clone()
        {
            return new Monster()
            {
                Name = this.Name,
                Level = this.Level,
                Hp = this.MaxHp,
                Atk = this.Atk,
                MaxHp = this.MaxHp
            };
        }

        public void DisplayMonsterInfo() //몬스터 상태 정보 표시
        {
            Console.WriteLine($"Lv.{Level:D2} {Name} HP {Hp}");
        }

        public void DisplayBattleInfo() // 전투 시작 전 몬스터 정보
        {
            Console.WriteLine($"Lv. {Level:D2} {Name}");
            Console.WriteLine($"Hp {Hp}/{MaxHp}");
        }

        public void DisplayHpInfo() // 전투 시 몬스터 Hp 변화 정보 표시
        {
            Console.WriteLine($"Lv. {Level:D2} {Name}");
            string nowHp = Hp <= 0 ? "Dead" : Hp.ToString(); // hp가 0 이하면 Dead 표시
            Console.WriteLine($"Hp {previousHp} -> {nowHp}");
        }

        public void TakeDamage(int amount) //데미지를 받으면 hp 감소
        {
            previousHp = Hp;
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
        public void Attack(Player target) // 몬스터의 공격 행동
        {
            Console.WriteLine($"{Name} 의 공격!");
            double errorRate = rand.NextDouble() * 0.2 + 0.9; // 공격력 오차 0.9~1.1
            int finalAtk = (int)Math.Ceiling(Atk * errorRate);
            bool isDodge = rand.Next(0, 100) < DodgeChance; // 10% 회피율

            if (isDodge)
            {
                Console.WriteLine($"Lv.{target.Level:D2} {target.Name} 을(를) 공격했지만 아무일도 일어나지 않았다.");
                target.DisplayHpInfo();
                return;
            }
            Console.WriteLine($"{target.Name} 을(를) 맞췄습니다. [데미지 : {finalAtk}]");
            target.TakeDamage(finalAtk);
        }
    }
}
