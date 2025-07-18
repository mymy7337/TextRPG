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
        new() {Level = 2, Hp = 15, MaxHp = 15, Atk = 5, Name = "고블린", Item = Item.Items[0]},
        new() {Level = 3, Hp = 10, MaxHp = 10, Atk = 9, Name = "리자드맨", Item = Item.Items[1]},
        new() {Level = 5, Hp = 25, MaxHp = 25, Atk = 8, Name = "리자드맨 방패병", Item = Item.Items[2]},
        new() {Level = 7, Hp = 30, MaxHp = 30, Atk = 10, Name = "와이번 해츨링", Item = Item.Items[1]},
        new() {Level = 100, Hp = 1000, MaxHp = 1000, Atk = 100, Name = "광포한 레드 드래곤", Item = Item.Items[10]},
    };
        }

    }
}
