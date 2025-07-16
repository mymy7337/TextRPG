using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace = TextRPG;
public abstract class Item
{
    public string ItemName { get; set; }
    public string ItemScript { get; set; }
    public int Price { get; set; }

    protected Item(string itemName, string itemScript, int price)
    {
        ItemName = itemName;
        ItemScript = itemScript;
        Price = price;
    }

    internal object ItemInfoText() // 임시로 생성 나중에 삭제
    {
        throw new NotImplementedException();
    }
}
