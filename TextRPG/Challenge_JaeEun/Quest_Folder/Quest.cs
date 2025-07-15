namespace TextRPG.Challenge_JaeEun.Quest_Folder
{
    public class Quest
    {
        public string Title = "";
        public string Info = "";

        public int CurrentCount;
        public int TargetCount;

        public bool IsAccepted;
        public bool IsCompleted;

        public int RewardGold;
        public List<string> RewardItems;

    }
}
