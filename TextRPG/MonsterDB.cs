using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.ItemFolder;

namespace TextRPG
{
    internal class MonsterDB
    {
        public static List<Monster> monsterData = new();
        public static void InitMonsters()
        {
            monsterData = new List<Monster>()
    {
        new() {Level = 2, Hp = 15, MaxHp = 15, Atk = 5, Name = "미니언", Item = Item.Items[0]},
        new() {Level = 3, Hp = 10, MaxHp = 10, Atk = 9, Name = "공허충", Item = Item.Items[1]},
        new() {Level = 5, Hp = 25, MaxHp = 25, Atk = 8, Name = "대포미니언", Item = Item.Items[2]},
    };
        }

    }
}
