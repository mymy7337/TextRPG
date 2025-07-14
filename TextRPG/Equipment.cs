using System.Collections.Generic;

namespace TextRPG;

public class Equipment
{
    public string ItemName { get; set; }
    public string ItemScript { get; set; }
    public int ItemAttack { get; set; }
    public int ItemDefense { get; set; }
    public int Price { get; set; } 
    public ItemType IType { get; set; }

    public enum ItemType
    {
        Drop,
        Shop,
    }

    public Equipment(string itemName, string itemScript, int itemAttack, int itemDefense, int price, ItemType iType)
    {
        ItemName = itemName;
        ItemScript = itemScript;
        ItemAttack = itemAttack;
        ItemDefense = itemDefense;
        Price = price;
        IType = iType;
    }
    public static List<Equipment> equipments = new List<Equipment>
    {
        new Equipment("이름","어떤 아이템이다", 2, 3, 10, ItemType.Shop),
        new Equipment("이름2", "어떤 아이템일까", 4, 5, 10, ItemType.Drop)
    };
}
