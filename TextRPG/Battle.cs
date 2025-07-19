using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Quest_Folder;
using TextRPG.Skill_Folder;
using TextRPG.ItemFolder;

namespace TextRPG
{
    internal class Battle
    {
        MonsterUI monUI = new MonsterUI();
        Random random = new Random();
        public enum BattleState
        {
            Main,
            Encounter,
            Result,
            Exit
        }

        bool isWrong;
        int choice;
        string message = "";
        int deadCount;
        int nowHp;

        int dungeonLevel = 1; // 1: 쉬움, 2: 보통, 3: 어려움


        public BattleState MainUI(Player player, BattleState state)
        {
            // 🧭 던전 진입 화면 처리
            state = ShowDungeonMenu(player, state);
            if (state != BattleState.Main)
                return state; // 0번 누르면 Exit로 빠짐

            // ✅ 몬스터 없으면 새로 소환
            if (monsterSpanwed.Count == 0)
            {
                SpawnMonster();
                deadCount = 0;
            }

            nowHp = player.Hp;

            // ✅ 선택지 보여주기
            List<string> option = new() { "1. 공격", "0. 돌아가기" };
            DisplayUI(player, state, option);

            isWrong = ChoiceCheck(0, option.Count - 1);
            if (isWrong)
                return BattleState.Main;

            switch (choice)
            {
                case 0:
                    if (random.Next(0, 100) < (100 - (monsterSpanwed.Count - deadCount) * 10))
                    {
                        monsterSpanwed.Clear();
                        return BattleState.Exit;
                    }
                    player.Hp -= (monsterSpanwed.Count - deadCount) * 2;
                    if (player.Hp <= 0)
                        return BattleState.Result;
                    return BattleState.Main;

                case 1:
                    return BattleState.Encounter;
                default:
                    return BattleState.Main;
            }
        }


        public void BattleStart(Player player, SkillSet skillSet)
        {
            // 던전 아트 출력 (선택한 난이도에 따라)
            switch (dungeonLevel)
            {
                case 1:
                    ShowDungeon1UI();
                    break;
                case 2:
                    ShowDungeon2UI();
                    break;
                case 3:
                    ShowDungeon3UI();
                    break;
            }

            BattleState state = BattleState.Main;

            while (state != BattleState.Exit)
            {
                switch (state)
                {
                    case BattleState.Main:
                        state = MainUI(player, state);
                        break;
                    case BattleState.Encounter:
                        state = EncounterUI(player, state, skillSet);
                        break;
                    case BattleState.Result:
                        state = result(player);
                        break;
                }
            }
        }


        BattleState EncounterUI(Player player, BattleState state, SkillSet skillSet)
        {
            List<string> option = new List<string>() { "0. 돌아가기" };
            DisplayUI(player, state, option);
            isWrong = ChoiceCheck(0, monsterSpanwed.Count);

            if (isWrong)
                return BattleState.Encounter;

            if (choice == 0)
                return BattleState.Main;

            int monsterIdx = choice - 1;
            if (monsterIdx >= 0 && monsterIdx < monsterSpanwed.Count)
            {
                Monster monster = monsterSpanwed[monsterIdx];
                if (monster.Hp <= 0)
                {
                    isWrong = true;
                    return BattleState.Encounter;
                }
                return PlayerPhase(player, monster, skillSet); // skillSet 전달
            }

            return BattleState.Encounter;
        }


        BattleState PlayerPhase(Player player, Monster monster, SkillSet skillset)
        {
            Console.Clear();
            Console.WriteLine("━━━━━━━━━━━━━━ PLAYER PHASE ━━━━━━━━━━━━━━");
            Console.WriteLine($"🎯 {monster.Name} 을(를) 상대로 어떤 행동을 할까요?");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            int selected = SkillUI.SelectSkill(skillset);
            Console.WriteLine();

            if (selected == -1)
            {
                Console.WriteLine("\n🚫 행동을 취소했습니다.");
                Console.WriteLine("🔙 아무 키나 누르면 돌아갑니다...");
                Console.ReadKey();
                return BattleState.Encounter;
            }

            Console.WriteLine("━━━━━━━━━━━━━━ ACTION RESULT ━━━━━━━━━━━━━━");

            // ✅ 전체 몬스터의 이전 HP 저장
            var prevHpDict = monsterSpanwed.ToDictionary(m => m, m => m.Hp);

            // ✅ 스킬 실행 (여러 마리를 공격할 수도 있음)
            skillset.UseSkill(selected - 1, player, monsterSpanwed, monster);

            Console.WriteLine("━━━━━━━━━━━━━━ ACTION RESULT ━━━━━━━━━━━━━━\n\n");

            // ✅ HP 변화 출력 (피해를 입은 몬스터만)
            var damagedMonsters = monsterSpanwed.Where(m => m.Hp < prevHpDict[m]).ToList();
            if (damagedMonsters.Count > 0)
            {
                foreach (var m in damagedMonsters)
                {
                    monUI.DisplayHpInfo(m, prevHpDict[m]); // ✅ 하나씩 변화 출력
                }
            }

            Console.WriteLine(":다음으로 진행하려면 아무 키나 누르세요...");
            Console.ReadKey();

            return EnemyPhase(player, monster);
        }





        BattleState EnemyPhase(Player player, Monster monster)
        {
            if (monster.Hp <= 0)
            {
                // 🎯 퀘스트 처리
                QuestManager.CheckKill(monster.Name, player);

                // 🎯 경험치 획득
                int earnedExp = monster.Level * 5;
                player.Exp += earnedExp;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n📈 {monster.Name} 처치! 경험치 +{earnedExp} 획득!");
                Console.ResetColor();


                int oldHp = player.MaxHp;
                int oldAtk = player.Atk;
                int oldDef = player.Def;

                // 🎯 레벨업 처리
                if (player.Exp >= player.ExpToNextLevel)
                {
                    // 레벨업 전 능력치 저장 후 레벨업
                    player.Exp -= player.ExpToNextLevel;
                    player.Level++;
                    player.MaxHp += 10;
                    player.Hp = player.MaxHp;
                    player.Atk += 2;
                    player.Def += 2;

                    ShowLevelUpAnimation(player, oldHp, oldAtk, oldDef);
                }

                // 🎯 아이템 드랍 (50%)
                if (random.Next(0, 100) < 50 && Item.Items.Count > 0)
                {
                    Item dropItem = Item.Items[random.Next(Item.Items.Count)];
                    getItem.Add(dropItem);
                }

                // 🎯 죽은 몬스터 수 갱신
                deadCount = monsterSpanwed.Count(mon => mon.Hp <= 0);

                // 🎯 전멸 시 전투 종료
                if (deadCount == monsterSpanwed.Count)
                    return result(player);

                return BattleState.Encounter;
            }

            // 몬스터가 생존했을 경우 → 공격
            int prevHp = player.Hp;
            Console.Clear();
            monster.Attack(player);
            PlayerUI.DisplayHpInfo(player, prevHp);
            Console.WriteLine();
            Console.WriteLine("다음");
            Console.Write(">>");
            Console.ReadKey();

            // 플레이어 사망 시
            if (player.Hp <= 0)
                return result(player);

            // 몬스터가 전부 죽었는지 재확인
            if (deadCount == monsterSpanwed.Count)
                return result(player);

            return BattleState.Encounter;
        }


        BattleState result(Player player)
        {
            Console.Clear();
            int prevHp = player.Hp;
            int oldMp = player.Mp;
            Console.WriteLine("━━━━━━━━━━━━━━ 🏆 전투 결과 🏆 ━━━━━━━━━━━━━━");
            Console.WriteLine();

            if (player.Hp > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🎉 Victory! 전투에서 승리했습니다!");
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine($"🧟‍♂️ 잡은 몬스터 수: {monsterSpanwed.Count} 마리");
                Console.WriteLine($"❤️ HP: {nowHp} → {player.Hp}");

                // 🎯 현재 경험치 출력만
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"📈 현재 경험치: {player.Exp} / 다음 레벨까지 {player.ExpToNextLevel- player.Exp}");
                Console.ResetColor();

                // ✅ MP 회복
                if (!isWrong)
                {
                    player.Mp += 10;
                    if (player.Mp > player.MaxMp)
                        player.Mp = player.MaxMp;
                }
                else
                    oldMp = player.Mp - 10;

                Console.WriteLine($"💧 MP: {oldMp} → {player.Mp}");

                // 아이템 획득 출력
                if (getItem.Count > 0)
                {
                    Console.WriteLine("\n📦 획득한 아이템:");
                    foreach (var item in getItem)
                    {
                        Console.WriteLine($"- {item.Name}");
                        player.AddItem(item);  // 인벤토리에 추가 + 구매 상태 처리
                    }
                    getItem.Clear(); // 다음 전투에 영향 없도록 초기화
                }


                Console.WriteLine("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                Console.WriteLine("어디로 이동하시겠습니까?");
                Console.WriteLine("[1] 🔁 던전 탐사 계속하기");
                Console.WriteLine("[0] 🏠 마을로 돌아가기");
                Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                message = isWrong ? "❌ 잘못된 입력입니다." : "👉 번호를 입력해주세요:";
                Console.WriteLine(message);
                Console.Write(">> ");

                isWrong = ChoiceCheck(0, 1);
                if (isWrong)
                    return BattleState.Result;

                //getItem.Clear();
                monsterSpanwed.Clear();

                return choice switch
                {
                    0 => BattleState.Exit,
                    1 => BattleState.Main,
                    _ => BattleState.Result
                };
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("💀 You Lose... 전투에 패배했습니다.");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"Lv{player.Level:D2} {player.Name}");
                Console.WriteLine($"HP: 0");

                // 🎯 레드 드래곤 업적 + 부활
                if (monsterSpanwed.Any(m => m.Name == "광포한 레드 드래곤"))
                {
                    TriggerDragonResurrection(player); // ✅ 부활
                    //monsterSpanwed.Clear();            // 전투 리셋
                    return BattleState.Main;           // 다시 전투로
                }

                Console.WriteLine("\n☠️ 게임 오버. 마을로 돌아갑니다...");
                Console.Write(">> ");
                Console.ReadKey();
                //Environment.Exit(0); // 여기서만 진짜 종료
                return BattleState.Exit; // 이건 실행되지 않지만, 문법상 남김
            }


        }

        List<Monster> monsterSpanwed = new List<Monster>();
        List<Item> getItem = new List<Item>();

        void SpawnMonster()
        {
            int monNum = random.Next(1, 4); // 생성할 몬스터 수 (1~3마리)

            int minLevel = 1;
            int maxLevel = 100;

            // 🎯 dungeonLevel은 ShowDungeonMenu에서 세팅됨 (1: 쉬움, 2: 보통, 3: 어려움)
            switch (dungeonLevel)
            {
                case 1: // 쉬움
                    minLevel = 1;
                    maxLevel = 10;
                    break;
                case 2: // 보통
                    minLevel = 20;
                    maxLevel = 30;
                    break;
                case 3: // 어려움
                    minLevel = 50;
                    maxLevel = 100;
                    break;
            }

            // 🎯 레벨 조건에 맞는 몬스터만 필터링
            var filteredMonsters = MonsterDB.monsterData
                .Where(mon => mon.Level >= minLevel && mon.Level <= maxLevel)
                .ToList();

            // 예외 처리: 해당 난이도 몬스터가 없는 경우
            if (filteredMonsters.Count == 0)
            {
                Console.WriteLine("⚠️ 해당 난이도에 맞는 몬스터가 없습니다.");
                return;
            }

            // 무작위로 몬스터 소환
            for (int i = 0; i < monNum; i++)
            {
                Monster baseMon = filteredMonsters[random.Next(filteredMonsters.Count)];
                monsterSpanwed.Add(baseMon.Clone());
            }
        }


        bool ChoiceCheck(int min, int max)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out choice))
            {
                if (choice >= min && choice <= max)
                    return false;
            }
            return true;
        }

        void DisplayUI(Player player, BattleState state, List<string> option)
        {
            Console.Clear(); // ✅ 다시 추가: 항상 같은 위치에서 새로 그림

            ShowDungeonUIByLevel(); // ✅ 현재 던전에 해당하는 UI 다시 출력

            Console.WriteLine("═══════════════════════ ⚔️ Battle ⚔️ ═══════════════════════");
            Console.WriteLine();

            // 📌 몬스터 목록
            Console.WriteLine("🧟‍♂️ 몬스터 목록");
            for (int i = 0; i < monsterSpanwed.Count; i++)
            {
                Monster monster = monsterSpanwed[i];
                string prefix = state == BattleState.Encounter ? $"[{i + 1}] " : "   ";

                if (monster.Hp <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(prefix + "[Dead] ");
                }
                else
                {
                    Console.Write(prefix);
                }

                monUI.DisplayMonsterInfo(monster);
                Console.ResetColor();
            }

            Console.WriteLine("\n════════════════════════════════════════════════════════════");

            // 📌 플레이어 정보
            Console.WriteLine("🧙‍♂️ 내 정보");
            PlayerUI.DisplayBattleInfo(player);

            Console.WriteLine("════════════════════════════════════════════════════════════\n");

            // 📌 선택지
            Console.WriteLine("🛡️ 선택지");
            foreach (string optionLine in option)
            {
                Console.WriteLine(optionLine);
            }

            Console.WriteLine();

            // 📌 안내 메시지
            string defaultMessage = state switch
            {
                BattleState.Encounter => "대상을 선택해주세요.",
                _ => "원하시는 행동을 입력해주세요."
            };
            message = isWrong ? "❌ 잘못된 입력입니다." : $"👉 {defaultMessage}";

            Console.WriteLine(message);
            Console.Write(">> ");
        }





        /////////////////////////////////////////////////////////////////////////////////////////////






        public BattleState ShowDungeonMenu(Player player, BattleState state)
        {
            Console.Clear();
            DungeonScreen();

            Console.WriteLine("0. 🔙 돌아가기");
            Console.Write("\n>> ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    dungeonLevel = 1;
                    return BattleState.Main;
                case "2":
                    dungeonLevel = 2;
                    return BattleState.Main;
                case "3":
                    dungeonLevel = 3;
                    return BattleState.Main;
                case "0":
                    return BattleState.Exit;
                default:
                    Console.WriteLine("⚠️ 잘못된 입력입니다.");
                    Console.ReadKey();
                    return ShowDungeonMenu(player, state);
            }
        }



        void ShowDungeonUIByLevel()
        {
            switch (dungeonLevel)
            {
                case 1: ShowDungeon1UI(); break;
                case 2: ShowDungeon2UI(); break;
                case 3: ShowDungeon3UI(); break;
            }
        }
        public static void DungeonScreenUI()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                            ║");
            Console.WriteLine("║     ███████╗ ██╗   ██╗███╗   ██╗ ██████╗  ███████╗ ██████╗ ███╗   ██╗      ║");
            Console.WriteLine("║     ██╔═══██╗██║   ██║████╗  ██║██╔════╝  ██╔════╝██╔═══██╗████╗  ██║      ║");
            Console.WriteLine("║     ██║   ██║██║   ██║██╔██╗ ██║██║  ███╗ █████╗  ██║   ██║██╔██╗ ██║      ║");
            Console.WriteLine("║     ██║   ██║██║   ██║██║╚██╗██║██║   ██║ ██╔══╝  ██║   ██║██║╚██╗██║      ║");
            Console.WriteLine("║     ███████╔╝╚██████╔╝██║ ╚████║╚██████╔╝ ███████╗╚██████╔╝██║ ╚████║      ║");
            Console.WriteLine("║     ╚══════╝  ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝  ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝      ║");
            Console.WriteLine("║                                                                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        // 던전 선택 화면 UI 출력 함수
        public static void DungeonScreen()
        {
            DungeonScreenUI();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n⚔️ 입장할 던전을 선택하세요\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. 🟢 쉬운 던전    (권장 공격력: 10 / 방어력: 5)");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2. 🟡 일반 던전    (권장 공격력: 45 / 방어력: 40)");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("3. 🔴 어려운 던전  (권장 공격력: 75 / 방어력: 60)");
            Console.ResetColor();
        }

        public static void ShowDungeon3UI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            string[] artLines = {
        @"                ,'\   |\",
        @"               / /.:  ;;",
        @"              / :'|| //",
        @"             (| | ||;'",
        @"             / ||,;'-.._",
        @"            : ,;,`';:.--`",
        @"            |:|'`-(\\",
        @"            ::: \-'\`'",
        @"             \\\ \,-`.",
        @"              `'\ `.,-`-._      ,-._",
        @"       ,-.       \  `.,-' `-.  / ,..`.",
        @"      / ,.`.      `.  \ _.-' \',: ``\ \",
        @"     / / :..`-'''``-)  `.   _.:''  ''\ \",
        @"    : :  '' `-..''`/    |-''  |''  '' \ \",
        @"    | |  ''   ''  :     |__..-;''  ''  : :",
        @"    | |  ''   ''  |     ;    / ''  ''  | |",
        @"    | |  ''   ''  ;    /--../_ ''_ '' _| |",
        @"    : :  ''  _;:_/    :._  /-.'',-.'',-. |",
        @"    \ \  '',;'`;/     |_ ,(   `'   `'   \|",
        @"     \ \  \(   /\     :,'  \\",
        @"      \ \.'/  : /    ,)    /",
        @"       \ ':   ':    / \   :",
        @"        `.\    :   :\  \  |",
        @"                \  | `. \ |..-_",
        @"             SSt ) |.  `/___-.-`",
        @"               ,'  -.'.  `. `'        _,)",
        @"               \'\(`.\ `._ `-..___..-','",
        @"                  `'      ``-..___..-'"
    };

            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            foreach (string line in artLines)
            {
                Console.WriteLine($"║ {line.PadRight(50)} ║");
            }
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🧟 [던전 3: 죽음의 계곡]");
            //int winRate = GameSystem.CalculateWinRate(Program.player, 75, 60);
            //Console.WriteLine($"📊 권장 공격력: 75 / 방어력: 60");
            //Console.WriteLine($"🎯 예상 승리 확률: {winRate}%");
            //Console.WriteLine("⚔️ 전투를 시작하시겠습니까?");
            //Console.WriteLine("1. ✅ 예   2. ❌ 아니오");
            Console.ResetColor();
        }


        // 던전 2 UI 출력 함수
        public static void ShowDungeon2UI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            string[] artLines = {
        @"           """"---==____   ____==---""""",
        @"        """"""---==='__  """"  __`===---""""""",
        @"         """"""--===(___=-_-=___)===--""""""",
        @"         """"""--=== ) _=====_ ( ===--""""""",
        @"         """"--===//\""""\""/\\===--""""""",
        @"   ___----______---|___-----___|---______-----___",
        @" ,'        """"--==`\`       '/'==--\""""       __`----__",
        @" \          """"---==| \   / |==---\""""  __--""  """"-_",
        @"  \                  `:-| |-:'      \ /'              `\",
        @"   )                 | `/ \' |      /'     ,------_      `\",
        @"  '                  | `-^-' |    /'     /'        `\      \",
        @"                    |       |   |     /\\           \      \",
        @"                    |       |  |     |  \ \          \      \",
        @"                    \       \  |     |___) )          |      |",
        @"                    \       \-""|     |_---'          |      |",
        @"                    _\       \-\     \              /       |",
        @"                  /' \       \  \     \         _,-""       /",
        @"                /   _-\       \__\_____\____--""         /",
        @"               (   ""--\                               /'",
        @"                `-__    \_                         _,-'",
        @"                    `--_  ""-___________________--""",
        @"                        `\   \__    )    )",
        @"                          \     ""--""    /",
        @"                           \__        /'",
        @"                              ""---"""
    };

            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════╗");
            foreach (string line in artLines)
            {
                Console.WriteLine($"║ {line.PadRight(67)} ║");
            }
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════╝");

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🌋 [던전 2: 용암 골짜기]");
            //int winRate = GameSystem.CalculateWinRate(Program.player, 45, 40);
            //Console.WriteLine($"📊 권장 공격력: 45 / 방어력: 40");
            //Console.WriteLine($"🎯 예상 승리 확률: {winRate}%");
            //Console.WriteLine("⚔️ 전투를 시작하시겠습니까?");
            //Console.WriteLine("1. ✅ 예   2. ❌ 아니오");
            Console.ResetColor();
        }

        // 던전 1 UI 출력 함수
        public static void ShowDungeon1UI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            string[] artLines = {
@" _________________________________________________________",
@"/|     -_-                                             _-  |\",
@"/ |_-_- _                                         -_- _-   -| \   ",
@"  |                            _-  _--                      | ",
@"  |                            ,                            |",
@"  |      .-'````````'.        '(`        .-'```````'-.      |",
@"  |    .` |           `.      `)'      .` |           `.    |",
@"  |   /   |   ()        \      U      /   |    ()       \   |",
@"  |  |    |    ;         | o   T   o |    |    ;         |  |",
@"  |  |    |     ;        |  .  |  .  |    |    ;         |  |",
@"  |  |    |     ;        |   . | .   |    |    ;         |  |",
@"  |  |    |     ;        |    .|.    |    |    ;         |  |",
@"  |  |    |____;_________|     |     |    |____;_________|  |",
@"  |  |   /  __ ;   -     |     !     |   /     `'() _ -  |  |",
@"  |  |  / __  ()        -|        -  |  /  __--      -   |  |",
@"  |  | /        __-- _   |   _- _ -  | /        __--_    |  |",
@"  |__|/__________________|___________|/__________________|__|",
@"/                                             _ -        lc \",
@"/   -_- _ -             _- _---                       -_-  -_ \"
    };

            // 박스 상단
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════╗");

            // 아트 삽입 (좌우 여백 3칸 삽입해서 박스에 정렬)
            foreach (string line in artLines)
            {
                Console.WriteLine($"║ {line.PadRight(67)} ║");
            }

            // 박스 하단
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════╝");

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🦖 [던전 1: 해골의 던전]");
            //int winRate = GameSystem.CalculateWinRate(Program.player, 10, 5);
            //Console.WriteLine($"📊 예상 승리 확률: {winRate}%");
            //Console.WriteLine("⚔️ 전투를 시작하시겠습니까?");
            //Console.WriteLine("1. ✅ 예   2. ❌ 아니오");
            Console.ResetColor();
        }

        // 레벨업 애니메이션 함수
        public static void ShowLevelUpAnimation(Player player, int oldHp, int oldAtk, int oldDef)
        {
            Console.Clear();
            string[] frames =
            {
        "\n\n\n\n            🎉",
        "\n\n\n      🎉     🎉",
        "\n\n   🎉   🎉   🎉",
        "\n🎉 🎉 LEVEL UP! 🎉 🎉",
        $"         Lv. {player.Level} 도달!",
    };

            foreach (string frame in frames)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(frame);
                Console.ResetColor();
                Thread.Sleep(300);
            }

            // 🎯 능력치 상승 정보 출력
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🎉 Lv. {player.Level} 달성!\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"❤️ 최대 HP ({oldHp} → {player.MaxHp}) +{player.MaxHp - oldHp}");
            Console.WriteLine($"🗡️ 공격력 ({oldAtk} → {player.Atk}) +{player.Atk - oldAtk}");
            Console.WriteLine($"🛡️ 방어력 ({oldDef} → {player.Def}) +{player.Def - oldDef}");
            Console.ResetColor();

            Thread.Sleep(3000); // 3초 동안 보여주기
            Console.Clear();
        }

        //이스터에그 드래곤 에게 죽었을때
        public static void TriggerDragonResurrection(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n🏆 업적 달성: [용은 나의 숙적]");
            Console.WriteLine("당신은 용의 피를 뒤집어쓰고 부활했다…");
            Console.ResetColor();

            Thread.Sleep(3000);

            // 🎯 스토리 출력
            Console.Clear();
            Console.WriteLine("🔥 붉은 용의 불길 속에서 당신은 죽었습니다...");
            Thread.Sleep(2000);
            Console.WriteLine("💀 그러나 그 순간, 미지의 힘이 당신을 감쌉니다...");
            Thread.Sleep(2000);
            Console.WriteLine("🌟 당신은 새로운 모습으로 부활했습니다!");
            Thread.Sleep(2000);

            // 🎯 능력치 변화
            int prevLevel = player.Level;
            int prevHp = player.MaxHp;
            int prevAtk = player.Atk;
            int prevDef = player.Def;

            player.Level = 100;
            player.Exp = 0;
            player.Hp = 999;
            player.MaxHp = 999;
            player.Mp = 999;
            player.MaxMp = 999;
            player.Atk = 300;
            player.Def = 300;

            // 🎯 변화된 능력치 출력
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"🔥 당신은 전설의 용사로 부활했습니다!");
            Console.WriteLine($"Lv. {prevLevel} → Lv. {player.Level}");
            Console.WriteLine($"❤️ MaxHP : {prevHp} → {player.MaxHp}");
            Console.WriteLine($"⚔️  공격력 : {prevAtk} → {player.Atk}");
            Console.WriteLine($"🛡️  방어력 : {prevDef} → {player.Def}");
            Console.ResetColor();

            Thread.Sleep(5000);
        }


    }
}
