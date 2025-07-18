using System;
using System.Collections.Generic;

namespace TextRPG.Quest_Folder
{
    public static class QuestManager
    {
        public static List<Quest> AllQuests = new();

        // 몬스터 DB 기반 퀘스트 자동 생성
        public static void InitializeQuestsFromMonsterDB()
        {
            AllQuests.Clear();

            foreach (var monster in MonsterDB.monsterData)
            {
                AllQuests.Add(new Quest
                {
                    Title = $"{monster.Name} 3마리 처치",
                    Info = $"{monster.Name}을(를) 3마리 처치하세요.",
                    TargetCount = 3,
                    CurrentCount = 0,
                    IsAccepted = false,
                    IsCompleted = false,
                    RewardGold = monster.Level * 100,
                    RewardItems = new List<string>() // 아이템 보상은 나중에
                });
            }
        }

        // 진행 중인 퀘스트 리스트 반환
        public static List<Quest> GetAcceptedQuests()
        {
            return AllQuests.FindAll(q => q.IsAccepted && !q.IsCompleted);
        }

        // 몬스터 처치 시 호출 → 퀘스트 진행도 반영
        public static void CheckKill(string monsterName, Player player)
        {
            foreach (var quest in GetAcceptedQuests())
            {
                if (quest.Title.Contains(monsterName) && !quest.IsCompleted)
                {
                    quest.CurrentCount++;

                    if (quest.CurrentCount >= quest.TargetCount)
                    {
                        quest.IsCompleted = true;
                        Console.WriteLine($"\n🎉 퀘스트 완료: {quest.Title}");

                        // 💰 보상 지급
                        player.AddGold(quest.RewardGold);
                        Console.WriteLine($"💰 골드 +{quest.RewardGold}");

                        foreach (var itemName in quest.RewardItems)
                        {
                            // Equipment.Items에서 이름으로 찾기
                            var item = Equipment.Items.Find(e => e.ItemName == itemName);

                            if (item != null)
                            {
                                player.AddItem(item);
                                Console.WriteLine($"🎁 아이템 획득: {item.ItemName}");
                            }
                            else
                            {
                                Console.WriteLine($"⚠️ '{itemName}' 아이템을 찾을 수 없습니다.");
                            }
                        }

                        Console.WriteLine();
                    }
                }
            }
        }

    }
}
