using System.Collections.Generic;

namespace TextRPG;

public class Equipment : Item
{
    public int ItemAttack { get; set; }
    public int ItemDefense { get; set; }
    public ItemType IType { get; set; }

    public enum ItemType
    {
        Drop,
        Shop,
    }

    public Equipment(string itemName, string itemScript, int itemAttack, int itemDefense, int price, ItemType iType)
        : base(itemName, itemScript, price)
    {
        ItemAttack = itemAttack;
        ItemDefense = itemDefense;
        IType = iType;
    }

    public static List<Equipment> Items = new List<Equipment>
    {
        new Equipment("이름", "어떤 아이템이다", 2, 3, 10, ItemType.Shop),
        new Equipment("이름2", "어떤 아이템일까", 4, 5, 10, ItemType.Drop)
    };
}
