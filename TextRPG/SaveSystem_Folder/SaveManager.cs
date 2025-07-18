using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.SaveSystem_Folder
{
    public class SaveManager
    {
        public GameSaveData CurrentSaveData;

        private static readonly string SaveDirectory = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Challenge_JaeEun\JSON_Data");
        private static readonly string SavePath = Path.Combine(SaveDirectory, "save.json");

        public SaveManager()
        {
            CurrentSaveData = new GameSaveData();
        }

        public void SaveToJson()
        {
            Console.WriteLine("[디버그] SaveToJson 호출됨");

            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
                Console.WriteLine($"[디렉토리 생성] {SaveDirectory}");
            }

            CurrentSaveData.PlayerName ??= "NULL!";
            CurrentSaveData.JobName ??= "NULL!";

            var json = JsonSerializer.Serialize(CurrentSaveData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SavePath, json);

            Console.WriteLine($"[저장 완료] {SavePath} 에 저장되었습니다.");
        }


        public void LoadFromJson()
        {
            if (File.Exists(SavePath))
            {
                var json = File.ReadAllText(SavePath);
                CurrentSaveData = JsonSerializer.Deserialize<GameSaveData>(json);
                Console.WriteLine("[불러오기 완료] 저장된 데이터를 불러왔습니다.");
            }
            else
            {
                Console.WriteLine($"[불러오기 실패] 저장 파일이 없습니다: {SavePath}");
            }
        }
    }
}