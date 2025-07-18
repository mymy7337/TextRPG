using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    internal class MonsterUI
    {
        public void DisplayMonsterInfo(Monster monster) //몬스터 상태 정보 표시
        {
            string nowHp = monster.Hp <= 0 ? "Dead" : monster.Hp.ToString();
            Console.WriteLine($"Lv.{monster.Level:D2} {monster.Name} HP {nowHp}");
        }

        public void DisplayBattleInfo(Monster monster) // 전투 시작 전 몬스터 정보
        {
            Console.WriteLine($"Lv. {monster.Level:D2} {monster.Name}");
            Console.WriteLine($"Hp {monster.Hp}/{monster.MaxHp}");
        }

        public void DisplayHpInfo(Monster monster, int previousHp) // 전투 시 몬스터 Hp 변화 정보 표시
        {
            Console.WriteLine($"Lv. {monster.Level:D2} {monster.Name}");
            string nowHp = monster.Hp <= 0 ? "Dead" : monster.Hp.ToString(); // hp가 0 이하면 Dead 표시
            Console.WriteLine($"Hp {previousHp} -> {nowHp}");
        }
    }
}
