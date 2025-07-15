using System.Collections.Generic;

namespace TextRPG;

public class Potion : Item
{
    public int ItemHP { get; set; }
    public int ItemMP { get; set; }
    public ItemType IType { get; set; }

    public enum ItemType
    {
        Drop,
        Shop,
    }

    public Potion(string itemName, string itemScript, int itemHP, int itemMP, int price, ItemType iType)
        : base(itemName, itemScript, price)
    {
        ItemHP = itemHP;
        ItemMP = itemMP;
        IType = iType;
    }

    public static List<Potion> Items = new List<Potion>
    {
        new Potion("이름", "어떤 아이템이다", 2, 3, 10, ItemType.Shop),
    };
}
