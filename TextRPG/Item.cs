using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
