using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Challenge_JaeEun.System_Folder
{
    public class GameSaveData
    {
        public string PlayerName { get; set; }
        public string JobName { get; set; }

        public int Level { get; set; }
        public int Exp { get; set; }
        public int Gold { get; set; }

        public int HP { get; set; }
        public int MP { get; set; }

        public List<string> InventoryItems { get; set; } = new();
        public List<string> CompletedQuests { get; set; } = new();

        public GameSaveData()
        {
            InventoryItems = new List<string>();
            CompletedQuests = new List<string>();
        }


        /*
        static void Main()
        {
            SaveManager saveManager = new SaveManager();

            // 저장용 데이터 입력
            saveManager.CurrentSaveData.PlayerName = "재은";
            saveManager.CurrentSaveData.JobName = "마법사";
            saveManager.SaveToJson();

            // 현재 데이터 초기화 후 불러오기
            saveManager.CurrentSaveData = new GameSaveData();
            saveManager.LoadFromJson();

            // 결과 출력
            Console.WriteLine($"[불러온 이름] {saveManager.CurrentSaveData.PlayerName}");
            Console.WriteLine($"[불러온 직업] {saveManager.CurrentSaveData.JobName}");
        }
        */
    }
}
