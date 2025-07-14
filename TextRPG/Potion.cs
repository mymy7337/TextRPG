using System.Collections.Generic;

namespace TextRPG;

public class Potion
{
    public string ItemName { get; set; }
    public string ItemScript { get; set; }
    public int ItemHP { get; set; }
    public int ItemMP { get; set; }
    public int Price { get; set; } 
    public ItemType IType { get; set; }

    public enum ItemType
    {
        Drop,
        Shop,
    }

    public Potion(string itemName, string itemScript, int itemHP, int itemMP, int price, ItemType iType)
    {
        ItemName = itemName;
        ItemScript = itemScript;
        ItemHP = itemHP;
        ItemMP = itemMP;
        Price = price;
        IType = iType;
    }
    public static List<Potion> potions = new List<Potion>
    {
        new Potion("이름","어떤 아이템이다", 2, 3, 10, ItemType.Shop),
    };
}