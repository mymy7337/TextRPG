using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class Item
{
    public string ItemName { get; set; }
    public string ItemScript { get; set; }
    public int Gold { get; set; }
    public ItemType IType { get; set; }

    protected Item(string itemName, string itemScript, int gold, ItemType iType)
    {
        ItemName = itemName;
        ItemScript = itemScript;
        Gold = gold;
        IType = iType;
    }
    public enum ItemType
    {
        Drop,
        Shop,
    }
    public enum ItemClass
    {
        //무기종류
    }

    internal object ItemInfoText() // 임시로 생성 나중에 삭제
    {
        throw new NotImplementedException();
    }
}
