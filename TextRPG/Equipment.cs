using System.Collections.Generic;

namespace TextRPG
{
    public class Equipment : Item
    {
        public int ItemAttack { get; set; }
        public int ItemDefense { get; set; }

        public Equipment(string itemName, string itemScript, int gold, GetType iType, int[] itemEffect, EffectType[] eTypes)
            : base(itemName, itemScript, gold, iType, itemEffect, eTypes)
        {

        }

        public static List<Equipment> Items = new List<Equipment>
    {
        new Equipment("견습용 단검", "초보 모험가도 들기 쉬운 단검이다.", 2, GetType.Shop, new int[]{3}, new EffectType[] { EffectType.ItemAtk } ),
        new Equipment("고블린의 가죽갑옷", "적당히 몸에 맞게 수선해 입을 수 있을 것 같다. 몸통을 보호한다.", 5, GetType.Drop, new int[]{5}, new EffectType[] { EffectType.ItemDef}),
        new Equipment("무거운 판금갑옷", "높은 방어력을 제공하지만, 무겁다... 민첩하게 움직이긴 어려워진다.", 10, GetType.Shop, new int[]{-2, 20}, new EffectType[] {EffectType.ItemAtk, EffectType.ItemDef})
    };
    }
}

