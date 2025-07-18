using System;

namespace TextRPG
{
    internal class MonsterUI
    {
        public void DisplayMonsterInfo(Monster monster) // 몬스터 상태 정보 표시
        {
            string nowHp = monster.Hp <= 0 ? "💀 Dead" : $"❤️ {monster.Hp}";
            Console.WriteLine($"[👾 Lv.{monster.Level:D2}] {monster.Name} - {nowHp}");
        }

        public void DisplayBattleInfo(Monster monster) // 전투 시작 전 몬스터 정보
        {
            Console.WriteLine("━━━━━━━━━━━━━━ MONSTER ━━━━━━━━━━━━━━");
            Console.WriteLine($"👾 Lv.{monster.Level:D2} {monster.Name}");
            Console.WriteLine($"❤️ HP : {monster.Hp} / {monster.MaxHp}");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
        }

        public void DisplayHpInfo(Monster monster, int previousHp) // 전투 시 몬스터 Hp 변화 정보
        {
            string nowHp = monster.Hp <= 0 ? "💀 Dead" : $"❤️ {monster.Hp}";
            Console.WriteLine("━━━━━━━━━━ HP 변화 ━━━━━━━━━━");
            Console.WriteLine($"👾 Lv.{monster.Level:D2} {monster.Name}");
            Console.WriteLine($"❤️ HP : {previousHp} → {nowHp}");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
        }
    }
}
