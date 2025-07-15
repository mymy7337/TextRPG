using System;
using System.Linq;

namespace TextRPG;

public class Shop
{
    public static void OpenShop()
    {
        Console.Clear();
        Console.WriteLine("상점에 입장하였습니다.");
        Console.WriteLine("(E) 장비 / (P) 포션");
        string input = Console.ReadLine().ToLower();

        if (input == "p" || input == "buypotion")
        {
            ShowRandomPotions(3);
        }
        else if (input == "e" || input == "buyequipment")
        {
            ShowRandomEquipments(3);
        }
        else
        {
            Console.WriteLine("상점 주인: 뭘 찾는다고? 여기선 장비랑 포션밖에 안 팔아.");
        }
    }

    private static void ShowRandomPotions(int count)
    {
        var shopPotions = Potion.Items.Where(p => p.IType == Potion.ItemType.Shop).ToList();
    }

    private static void ShowRandomEquipments(int count)
    {
        var shopEquipments = Equipment.Items.Where(p => p.IType == Equipment.ItemType.Shop).ToList();
    }
}