using TextRPG.Quest_Folder;

public static class QuestUI
{
    public static void ShowQuestList()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("📜 [퀘스트 목록]\n");

            for (int i = 0; i < QuestManager.AllQuests.Count; i++)
            {
                Quest quest = QuestManager.AllQuests[i];
                string status = quest.IsCompleted ? "✅ 완료됨"
                                 : quest.IsAccepted ? "🟡 진행 중"
                                 : "⚪ 미수락";

                Console.WriteLine($"[{i + 1}] {quest.Title} ({status})");
            }

            Console.WriteLine("\n[0] 나가기");
            Console.Write("퀘스트 번호를 선택하세요: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
            {
                if (choice == 0)
                    break;

                if (choice >= 1 && choice <= QuestManager.AllQuests.Count)
                {
                    Quest selected = QuestManager.AllQuests[choice - 1];
                    ShowQuestDetail(selected);
                }
                else
                {
                    Console.WriteLine("유효한 번호를 입력해주세요.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                Console.ReadKey();
            }
        }
    }

    private static void ShowQuestDetail(Quest quest)
    {
        Console.Clear();
        Console.WriteLine($"📌 {quest.Title}");
        Console.WriteLine($"📝 설명: {quest.Info}");
        Console.WriteLine($"🎯 목표: {quest.CurrentCount} / {quest.TargetCount}");
        Console.WriteLine($"💰 보상: 골드 {quest.RewardGold}");

        if (!quest.IsAccepted)
        {
            Console.Write("\n이 퀘스트를 수락하시겠습니까? (Y/N): ");
            string accept = Console.ReadLine()?.ToUpper();
            if (accept == "Y")
            {
                quest.IsAccepted = true;
                Console.WriteLine("\n✅ 퀘스트를 수락했습니다.");
            }
        }
        else if (quest.IsCompleted)
        {
            Console.WriteLine("\n🎉 이 퀘스트는 이미 완료되었습니다.");
        }
        else
        {
            Console.WriteLine("\n🟡 이 퀘스트는 현재 진행 중입니다.");
        }

        Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
        Console.ReadKey();
    }
}
