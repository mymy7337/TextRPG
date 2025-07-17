using System.Collections.Generic;

namespace TextRPG;

public class Potion : Item
{
    public int ItemHP { get; set; }
    public int ItemMP { get; set; }

    public Potion(string itemName, string itemScript, int gold, ItemType iType, int[] itemEffect, EffectType[] eTypes)
        : base(itemName, itemScript, gold, iType, itemEffect, eTypes)
    {
    
    }

    public static List<Potion> Items = new List<Potion>
    {
        new Potion("체력포션(소)", "체력을 아주 조금 회복해준다.", 2, ItemType.Shop, new int[]{5}, new EffectType[] { EffectType.ItemHp }),
        new Potion("마력포션(소)", "마력을 아주 조금 회복해준다.", 2, ItemType.Shop, new int[]{5}, new EffectType[] { EffectType.ItemMp }),
        new Potion("요정의 비약", "체력과 마력을 모두 크게 회복해준다. 이런 귀한 걸 얻다니 운이 좋은걸?", 20, ItemType.Drop, new int[]{30,30}, new EffectType[] { EffectType.ItemHp, EffectType.ItemMp})
    };
}
