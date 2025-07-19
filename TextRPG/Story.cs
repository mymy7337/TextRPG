using System;
using System.Collections.Generic;
using System.Threading;

namespace TextRPG
{
    public static class Story
    {
        public static void ShowIntroStory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string[] lines =
            {
        "🌫️  이곳은 용족과 인간이 대립하는 국경의 최전선...",
        "",
        "⚔️  먼 옛날, 용살자 '지크프리트'가 드래곤 로드를 죽이고 공멸한 이후",
        "    인간은 용들의 노예 생활에서 해방되었고",
        "    🐉 용들은 그 분노를 안고 깊은 레어로 숨어들었다.",
        "",
        "🌫️  안개가 자욱하고, 분위기가 심상치 않은 이 마을 밖으로 나서면……",
        "    머지않아 드래곤의 레어가 모습을 드러낼 것이다.",
        "",
        "💀 그곳, '던전'이라 불리는 악의 소굴에는",
        "    수백만 마리의 흉폭한 악룡들이 도사리고 있다!",
        "    아이들을 잡아먹고, 보물을 갈취하고, 마을을 불태우는 괴물들!",
        "",
        "🗣️  사람들은 노래한다. 용감한 모험가라면 명예롭게 죽어야 한다고…",
        "",
        "🔥 인류의 터전을 되찾기 위해, 모험가들은 오늘도",
        "    부나방처럼 어둠의 던전에 몸을 던진다.",
        "",
        "🏘️  마을은 당신을 환영한다.",
        "✨ 새로운 모험가여…… 이제 너의 전설을 써라.",
    };

            Random rand = new Random();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    Thread.Sleep(400);
                    continue;
                }

                Console.WriteLine(line);
                Thread.Sleep(1000 + rand.Next(200, 400));
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("▶▶ 아무 키나 눌러서 모험을 시작하세요...");
            Console.ResetColor();
            Console.ReadKey();
        }


        public static void ShowEnding(Player player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━ 📜 엔딩 스토리 📜 ━━━━━━━━━━━━━━━━━━━━\n");
            Console.ResetColor();
            string[] ending;

            if (player.Level >= 10 && !Battle.isEnd)
            {
                // 인간의 선봉장 엔딩
                ending = new string[]
                {
            "🏆 엔딩: 인간의 선봉장",
            "",
            "⚔️  단신으로 드래곤 레어에 쳐들어가,",
            "    수많은 몬스터를 도륙하며 위명을 떨친 당신.",
            "    인간 왕국의 최전방을 수호한 전설로 남았습니다.",
            "",
            "💀 그런 당신에게도 결국 끝은 찾아옵니다.",
            "    흐려지는 의식 속에서,",
            "    동족의 복수를 부르짖는 리자드맨들의 전투함성이 울려퍼집니다.",
            "",
            "🔥 괴물들아, 즐겨두어라.",
            "👊 인간은 포기하지 않는다.",
            "    다음, 또 다음의 모험가가 언젠가 너희를 섬멸할 테니…",
                };
                Battle.isEnd = true;
            }
            else
            {
                // 자원의 순환 엔딩
                ending = new string[]
                {
            "🌱 엔딩: 자원의 순환",
            "",
            "😢 초보 모험가의 용기가 지나쳤던 걸까요?",
            "    당신의 몸은 스러져, 드래곤 레어의 거름이 되었습니다.",
            "",
            "⚖️  억울할 것 없어요.",
            "    세상은 그렇게 돌아가는 법이니까요.",
            "",
            "🕊️ 부디 다음 모험가는...",
            "    무사히 집으로 돌아갈 수 있기를 바랍니다.",
                };
            }

            // 줄 단위 출력
            foreach (string line in ending)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    Thread.Sleep(500);
                    continue;
                }

                Console.WriteLine(line);
                Thread.Sleep(1000);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n▶▶ 아무 키나 눌러 마을로 돌아가기...");
            Console.ResetColor();
            Console.ReadKey();
        }


    }
}
