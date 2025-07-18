using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    public static class PlayerUI
    {
        public static void DisplayPlayerInfo(Player player) // 플레이어 상태 정보 표시
        {
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine($"🧙‍♂️ {player.Name} - {player.Job} | Lv.{player.Level:D2}");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine($"🗡️ 공격력 : {player.FinalAtk}" + (player.ExtraAtk == 0 ? "" : $" (+{player.ExtraAtk})"));
            Console.WriteLine($"🛡️ 방어력 : {player.FinalDef}" + (player.ExtraDef == 0 ? "" : $" (+{player.ExtraDef})"));
            Console.WriteLine($"❤️ 체  력 : {player.Hp} / {player.MaxHp}");
            Console.WriteLine($"🔮 마  력 : {player.Mp} / {player.MaxMp}");
            Console.WriteLine($"💰 골  드 : {player.Gold}");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }

        public static void DisplayBattleInfo(Player player) // 전투 시 플레이어 정보
        {
            Console.WriteLine($"🎖️ Lv. {player.Level:D2} {player.Name} ({player.Job})");
            Console.WriteLine($"❤️ HP : {player.Hp} / {player.MaxHp}");
            Console.WriteLine($"🔮 MP : {player.Mp} / {player.MaxMp}");
        }

        public static void DisplayHpInfo(Player player, int previousHp) // 전투 시 Hp
        {
            Console.WriteLine($"🎯 대상: Lv.{player.Level:D2} {player.Name} ({player.Job})");

            string nowHp = player.Hp <= 0 ? "💀 Dead" : player.Hp.ToString();
            Console.WriteLine($"❤️ HP : {previousHp} → {nowHp}");
        }
    }
}
