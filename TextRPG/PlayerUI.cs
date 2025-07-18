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
            Console.WriteLine($"{player.Name} ({player.Job})");
            Console.WriteLine($"공격력 : {player.FinalAtk}" + (player.ExtraAtk == 0 ? "" : $" (+{player.ExtraAtk})"));
            Console.WriteLine($"방어력 : {player.FinalDef}" + (player.ExtraDef == 0 ? "" : $" (+{player.ExtraDef})"));
            Console.WriteLine($"Hp : {player.Hp}");
            Console.WriteLine($"Mp : {player.Mp}");
            Console.WriteLine($"Gold : {player.Gold}");
        }

        public static void DisplayBattleInfo(Player player) // 전투 시 플레이어 정보
        {
            Console.WriteLine($"Lv. {player.Level:D2} {player.Name} ({player.Job})");
            Console.WriteLine($"Hp {player.Hp}/{player.MaxHp}");
            Console.WriteLine($"Mp {player.Mp}/{player.MaxMp}");
        }

        public static void DisplayHpMpInfo(Player player, int previousHp, int previousMp) // 전투 시 Hp/ 변화 정보 표시
        {
            Console.WriteLine($"Lv. {player.Level:D2} {player.Name} ({player.Job})");
            string nowHp = player.Hp <= 0 ? "Dead" : player.Hp.ToString(); // hp가 0 이하면 Dead 표시
            Console.WriteLine($"Hp {previousHp} -> {nowHp}");
            Console.WriteLine($"Hp {previousMp} -> {player.Mp}");
        }
    }
}
