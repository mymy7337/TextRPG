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
        new() {Level = 4, Hp = 20, MaxHp = 20, Atk = 6, Name = "고블린 정찰병", Item = Item.Items[1]},
        new() {Level = 6, Hp = 22, MaxHp = 22, Atk = 7, Name = "작은 리자드맨", Item = Item.Items[2]},
        new() {Level = 9, Hp = 25, MaxHp = 25, Atk = 8, Name = "야생 늑대", Item = Item.Items[3]},

        // 🟡 보통 (20~30)
        new() {Level = 20, Hp = 45, MaxHp = 45, Atk = 15, Name = "리자드맨", Item = Item.Items[4]},
        new() {Level = 24, Hp = 55, MaxHp = 55, Atk = 18, Name = "리자드맨 방패병", Item = Item.Items[5]},
        new() {Level = 28, Hp = 60, MaxHp = 60, Atk = 20, Name = "성난 켄타우로스", Item = Item.Items[6]},
        new() {Level = 30, Hp = 65, MaxHp = 65, Atk = 22, Name = "와이번 해츨링", Item = Item.Items[7]},

        // 🔴 어려움 (50~100)
        new() {Level = 50, Hp = 120, MaxHp = 120, Atk = 35, Name = "불의 골렘", Item = Item.Items[8]},
        new() {Level = 65, Hp = 200, MaxHp = 200, Atk = 45, Name = "암흑 드레이크", Item = Item.Items[9]},
        new() {Level = 80, Hp = 350, MaxHp = 350, Atk = 70, Name = "와이번 군주", Item = Item.Items[9]},
        new() {Level = 100, Hp = 500, MaxHp = 500, Atk = 100, Name = "광포한 레드 드래곤", Item = Item.Items[10]}
    };
        }

    }
}
