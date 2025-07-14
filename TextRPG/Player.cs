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
        public int Gold { get; private set; }
        public int ExtraAtk { get; private set; } // 추가공격력
        public int ExtraDef { get; private set; } // 추가방어력

        // 
        public Player(int level, string name, string job, int atk, int def, int hp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }

        public void DisplayPlayerInfo()
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

        // 매개변수의 수치 만큼 회복
        public void Heal(int amount)
        {
            if (amount <= 0)
            {
                return;
            }
            else
            {
                Hp += amount;
            }
        }
        
        //데미지를 받으면 hp 감소
        public void TakeDamage(int amount)
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
        }
    }
}
