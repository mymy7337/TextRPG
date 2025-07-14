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

        public Monster(int level, string name, int atk, int hp) 
        { 
            Level = level;
            Name = name;
            Atk = atk;
            Hp = hp;
        }

        //몬스터 상태 정보 표시
        public void DisplayMonsterInfo()
        {
            Console.WriteLine($"Lv.{Level} {Name} HP {Hp}");
        }

        public void TakeDamage(int amount) //데미지를 받으면 hp 감소
        {
            ; //플레이어의 방어력 만큼 데미지 감소
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
    }
}
