using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Challenge_JaeEun.Character.Job_Folder;

namespace TextRPG
{
    public static class PlayerUI
    {
        public static void DisplayPlayerInfo(Player player) // 플레이어 상태 정보 표시
        {
            Console.WriteLine($"Lv.{player.Level:D2}");
            Console.WriteLine($"{player.Name} ({Job})");
            Console.WriteLine($"공격력 : {player.FinalAtk}" + (player.ExtraAtk == 0 ? "" : $" (+{player.ExtraAtk})"));
            Console.WriteLine($"방어력 : {player.FinalDef}" + (player.ExtraDef == 0 ? "" : $" (+{player.ExtraDef})"));
            Console.WriteLine($"체 력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold}");
        }

        public static void DisplayBattleInfo(Player player) // 전투 시작 전 플레이어 정보
        {
            Console.WriteLine($"Lv. {player.Level:D2} {player.Name} ({player.Job})");
            Console.WriteLine($"Hp {player.Hp}/{player.MaxHp}");
        }

        public static void DisplayHpInfo(Player player, int previousHp) // 전투 시 Hp 변화 정보 표시
        {
            Console.WriteLine($"Lv. {player.Level:D2} {player.Name} ({player.Job})");
            string nowHp = player.Hp <= 0 ? "Dead" : player.Hp.ToString(); // hp가 0 이하면 Dead 표시
            Console.WriteLine($"Hp {previousHp} -> {nowHp}");
        }
    }


}
