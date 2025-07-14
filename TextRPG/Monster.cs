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
    }
}
