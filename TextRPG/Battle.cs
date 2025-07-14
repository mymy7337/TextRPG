using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Battle
    {
        bool isWrong;
        int choice;
        string message;

        public Battle()
        {
            StartUI();
        }

        void StartUI()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine("");
                //몬스터 정보
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[내정보]");
                //플레이어 정보
                Console.WriteLine("");
                Console.WriteLine("1. 공격");
                Console.WriteLine("");
                message = (isWrong == true) ? "잘못된 입력입니다." : "원하시는 행동을 입력해주세요.";
                Console.WriteLine(message);
                Console.Write(">>");
                isWrong = ChoiceCheck(1, 1);

                switch(choice)
                {
                    case 1:
                        AttackUI();
                        break;
                }
            }
            
        }

        void AttackUI()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine("");
            //몬스터 정보
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            //플레이어 정보
            Console.WriteLine("");
            Console.WriteLine("0. 취소");
            Console.WriteLine("");
            message = (isWrong == true) ? "잘못된 입력입니다." : "대상을 선택해주세요.";
            Console.WriteLine(message);
            Console.Write(">>");
            isWrong = ChoiceCheck(0, 4);
            switch(choice)
            {
                
            }

        }

        void PlayerPhase()
        {

        }

        void EnemyPhase()
        {

        }

        void result()
        {

        }


        bool ChoiceCheck(int min, int max)
        {
            string input = Console.ReadLine();
            if(int.TryParse(input, out choice))
            {
                if(choice >= min && choice <= max)
                return false;
            }
            return true;
        }

        void SpawnMonster()
        {

        }
    }
}
