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

        public static void PlayerInfo_Color(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║             📋 캐릭터 상태 보기            ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"👤 이름       : {player.Name}");
            Console.WriteLine($"💼 직업       : {player.Job}");
            Console.WriteLine($"📈 레벨       : Lv. {player.Level}"); //(Exp: {player.Exp}/{player.ExpToNextLevel})
            Console.WriteLine($"❤️ 체력       : {player.Hp}");
            Console.WriteLine($"❤️ 마력       : {player.Mp}");

            int bonusAtk = player.ExtraAtk - player.Atk;
            int bonusDef = player.ExtraDef - player.Def;

            Console.Write("⚔️ 공격력     : ");
            Console.Write($"{player.Atk + player.ExtraAtk}");
            if (player.ExtraAtk > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($" (+{player.ExtraAtk})");
                Console.ResetColor();
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("🛡️ 방어력     : ");
            Console.Write($"{player.Def + player.ExtraDef}");
            if (player.ExtraDef > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($" (+{player.ExtraDef})");
                Console.ResetColor();
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("⚡ 민첩력     : ");
            Console.Write($"{player.Dex + player.ExtraDex}");
            if (player.ExtraDex > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($" (+{player.ExtraDex})");
                Console.ResetColor();
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"💰 소지 Gold  : {player.Gold} G");
            Console.ResetColor();
        }
    }
}
