using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppInterface
{
    public interface ITransaction
    {
        void Start();
        void PinCheck(Account Ac);
        void Display(Account Ac);
        void BalanceCheck(Account Ac);
        void Deposit(Account Ac);
    }

    class Transaction : ITransaction
    {
        List<Account> accountList = new List<Account> { };
        Dictionary<int, int> TnxLimit = new Dictionary<int, int>();

        public Transaction()
        {
            for(int i = 1; i <= 10; i++)
            {
                Account Ac = new Account();

                Ac.CardNo = 200 + i;
                Ac.PinNo = 100 + i ;
                Ac.Balance = 50000;
                accountList.Add(Ac);
                TnxLimit.Add(Ac.CardNo, 0);
            }
        }

        public void Start()
        {
            Console.WriteLine("\nEnter UR Card ");
            var inputCard = Int32.Parse(Console.ReadLine());

            var Items = accountList.FindIndex(item => item.CardNo == inputCard);
            if(Items < 0 )
            {
                Start();
            }

            PinCheck(accountList[Items]);
            Display(accountList[Items]);
        }

        public void PinCheck( Account Ac)
        {
            Console.WriteLine("\nEnter UR PIN ");
            var inputPin = Int32.Parse(Console.ReadLine());

            if (Ac.PinNo == inputPin)
            {
                return;
            }

            Console.WriteLine("\nWrong PIN, Please try again\n");
            PinCheck(Ac);
        }

        public void Display( Account Ac)
        {
            Console.WriteLine("\nEnter your choise : ");
            Console.WriteLine("\n1 Balance Check \n2 Deposit \n3 Withdrawal \n0 Exit");
            var choise = Int32.Parse(Console.ReadLine());

            if (choise == 1) { BalanceCheck(Ac); }
            else if (choise == 2) { Deposit(Ac); }
            else if (choise == 3) { Withdrawal(Ac); }
            else if (choise == 0) { Start(); }

            Display(Ac);
        }

        public void BalanceCheck(Account Ac)
        {
            Console.WriteLine("\nYour Current Balance is " + Ac.Balance);
        }

        public void Deposit(Account Ac)
        {
            Console.WriteLine("\nEnter your amount : ");
            double amount = Int32.Parse(Console.ReadLine());

            Ac.Balance += amount;

            Console.WriteLine("\nYour balance has been updated");
            BalanceCheck(Ac);
        }

        public void Withdrawal(Account Ac)
        {
            Console.WriteLine("\nEnter your amount : ");
            double amount = Int32.Parse(Console.ReadLine());

            if (Ac.Balance < amount)
            {
                Console.WriteLine("\nDont have sufficient balance, try again");
                Withdrawal(Ac);
            }

            if (TnxLimit[Ac.CardNo] > 2)
            {
                Console.WriteLine("\nYour daily withdrawal limit reached");
                Start();
            }

            Ac.Balance -= amount;
            TnxLimit[Ac.CardNo]++;

            Console.WriteLine("\nYour balance has been updated");
            BalanceCheck(Ac);
        }
    }
}
