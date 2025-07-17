using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG;
public abstract class Item
{
    public string ItemName { get; set; }
    public string ItemScript { get; set; }
    public int Gold { get; set; }
    public GetType IType { get; set; }

    private int[] _itemEffect;
    public int[] ItemEffect => _itemEffect;

    private EffectType[] _effectTypes;
    public EffectType[] EffectTypes => _effectTypes;

    protected Item(string itemName, string itemScript, int gold, GetType iType, int[] itemEffect, EffectType[] effectTypes)
    {
        ItemName = itemName;
        ItemScript = itemScript;
        Gold = gold;
        IType = iType;
        _itemEffect = itemEffect;
        _effectTypes = effectTypes;
    }

    public enum GetType
    {
        Drop,
        Shop,
    }

    public enum EffectType
    {
        ItemAtk,
        ItemDef,
        ItemHp,
        ItemMp
    }

    public string ItemInfoText()
    {
        return $"{ItemName} | {ItemScript} | {Gold}G";
    }

    public virtual string ItemDetailsText()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine($"이름: {ItemName}");
        sb.AppendLine($"설명: {ItemScript}");
        sb.AppendLine($"가격: {Gold} Gold");
        sb.AppendLine($"획득방법: {IType}");

        if (ItemEffect != null && EffectTypes != null)
        {
            sb.AppendLine("효과:");
            for (int i = 0; i < Math.Min(ItemEffect.Length, EffectTypes.Length); i++)
            {
                sb.AppendLine($" - {EffectTypes[i]}: +{ItemEffect[i]}");
            }
        }
        return sb.ToString();
    }

}