namespace TextRPG;

public class Equipment
{
    public string EquipName { get; set; }
    public string EquipScript { get; set; }
    public int EquipAttack { get; set; }
    public int EquipDefense { get; set; }
    public ItemType EquipType { get; set; }

    public enum ItemType
    {
        Drop,
        Shop,
    }

    public Equipment(string equipName, string equipScript, int equipAttack, int equipDefense, ItemType equiptype)
    {
        EquipName = equipName;
        EquipScript = equipScript;
        EquipAttack = equipAttack;
        EquipDefense = equipDefense;
        EquipType = equiptype;
    }
    private List<Equipment> equipments = new List<Equipment>
    {
        new Equipment("이름","어떤 아이템이다", 2, 3, Equipment.ItemType.Shop),
        new Equipment("이름2", "어떤 아이템일까", 4, 5, ItemType.Drop)
    };
}
