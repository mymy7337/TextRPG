using System;
using System.Linq;

namespace TextRPG;

public class Shop
{
    Player player;

    public Shop(Player player)
    {
        this.player = player;
    }


    public void OpenShop()
    {
        Console.Clear();
        Console.WriteLine("상점에 입장하였습니다.");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
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

    private void ShowRandomPotions(int count)
    {
        var shopPotions = Potion.Items.Where(p => p.IType == Potion.ItemType.Shop).ToList();

        if (shopPotions.Count == 0) //리스트에 0개가 될 수 없으니 더미인데 일단 냅두기
        {
            Console.WriteLine("상점 주인: 포션 다 팔렸어.");
            return;
        }

        Random rand = new Random();
        var randomPotions = shopPotions.OrderBy(x => rand.Next()).Take(count).ToList();

        Console.Clear();
        Console.WriteLine("상점 주인: 자, 자. 포션들 골라 보시라고.");

        for (int i = 0; i < randomPotions.Count; i++)
        {
            var potion = randomPotions[i]; 
            Console.WriteLine($"[{i + 1}]");
            Console.WriteLine(potion.ItemDetailsText());
        }

        Console.Write("구매할 아이템 번호: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= randomPotions.Count)
        {
            var selectedPotion = randomPotions[choice - 1];
            BuyPotion(selectedPotion);
        }
        else
        {
            Console.WriteLine("상점 주인: 그런 물건은 안 파는데.");
        }
    }

    void BuyPotion(Potion potion)
    {
        Console.WriteLine($"{potion.ItemName}을(를) {potion.Gold}골드에 구매하였다.");
        player.UseGold(potion.Gold);
        player.AddItem(potion);
    }
    
    private void ShowRandomEquipments(int count)
    {
        var shopEquipments = Equipment.Items.Where(e => e.IType == Equipment.ItemType.Shop).ToList();

        if (shopEquipments.Count == 0) //더미
        {
            Console.WriteLine("상점 주인: 장비 다 팔렸어.");
            return;
        }

        Random rand = new Random();
        var randomEquipments = shopEquipments.OrderBy(x => rand.Next()).Take(count).ToList();

        Console.Clear();
        Console.WriteLine("상점 주인: 좋은 무기랑 방어구야. 이 근방에선 제일 좋을걸.");
        Console.WriteLine("뭐, 애당초 가게가 여기 하나뿐이지만! 하하.");

        for (int i = 0; i < randomEquipments.Count; i++)
        {
            var equip = randomEquipments[i];
            Console.WriteLine($"[{i + 1}]");
            Console.WriteLine(equip.ItemDetailsText());
        }

        Console.Write("구매할 아이템 번호: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= randomEquipments.Count)
        {
            var selectedEquipment = randomEquipments[choice - 1];
            BuyEquipment(selectedEquipment);
        }
        else
        {
            Console.WriteLine("상점 주인: 아, 안 돼. 미안하지만 그건 예약된 물품이라고. 매대에 있는 걸 골라.");
        }
    }

    void BuyEquipment(Equipment equipment)
    {
        Console.WriteLine($"{equipment.ItemName}을(를) {equipment.Gold}골드에 구매하였다.");
        player.UseGold(equipment.Gold);
        player.AddItem(equipment);
    }
}

