using System.Collections.Generic;

namespace TextRPG;

public class Potion : Item
{
    public int ItemHP { get; set; }
    public int ItemMP { get; set; }

    public Potion(string itemName, string itemScript, int itemHP, int itemMP, int gold, ItemType iType)
        : base(itemName, itemScript, gold, iType)
    {
        ItemHP = itemHP;
        ItemMP = itemMP;
    }

    public static List<Potion> Items = new List<Potion>
    {
        new Potion("이름", "어떤 아이템이다", 2, 3, 10, ItemType.Shop),
    };
}
