﻿

using Lab.Class.Bank;
using Labs.Bank.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Bank.Shared
{
    public class SharedBusiness
    {
        protected static int clientsTotalBalances()
        {
            int totalBalances = 0;

            List<object> clientsList = FileDbContext.convertFileDataToList(FileDbContext.ClientsDbConnectionString, FileDbContext.FileRowSeparator);

            foreach (BankClient client in clientsList)
                totalBalances += int.Parse(client.AccountBalance.ToString());

            return totalBalances;
        }
        protected static void printMenueOptions(string[] menuOptions)
        {
            for (int i = 0; i < menuOptions.Length; i++)
                Console.WriteLine("[{0}] " + menuOptions[i], i + 1);
        }
        protected static int readUserMenuChoose(int to)
        {
            Console.Write("Choose What do you want to Do? [1 to {0}] :", to);
            return int.Parse(Console.ReadLine());
        }
        public static BankClient getEmptyClientObject() => new BankClient("", "", "", "", "", "", 0, BankClient.enMode.EmptyMode);
        public static object readClientOneInfo(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine();
        }
        public static BankClient readClientInfo(string accountNumber = "") =>
           new BankClient(accountNumber == null ? readClientOneInfo("Enter Account Number: ").ToString() : accountNumber,
               readClientOneInfo("Enter Pin Code: ").ToString(),
               readClientOneInfo("Enter First Name: ").ToString(),
               readClientOneInfo("Enter Last Name: ").ToString(),
               readClientOneInfo("Enter Email: ").ToString(),
               readClientOneInfo("Enter Phone: ").ToString(),
               double.Parse(readClientOneInfo("Enter Account Balance: ").ToString()),
               BankClient.enMode.UpdateMode);
        public static BankClient findClient(string accountNumber, string pinCode = "")
        {
            BankClient client = getEmptyClientObject();

            List<object> clientsList = FileDbContext.convertFileDataToList(FileDbContext.ClientsDbConnectionString, FileDbContext.FileRowSeparator);

            foreach (BankClient clientsLine in clientsList)
            {
                if (pinCode == "")
                {
                    if (clientsLine.AccountNumber == accountNumber)
                    {
                        client = clientsLine;
                        break;
                    }
                }
                else
                {
                    if (clientsLine.AccountNumber == accountNumber && clientsLine.PinCode == pinCode)
                    {
                        client = clientsLine;
                        break;
                    }
                }
            }

            return client;
        }
        public static bool IsClientExist(string accountNumber) => findClient(accountNumber).Mode == BankClient.enMode.EmptyMode ? false : true;
        public static void goBack()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to go back Menue...");
            Console.ReadKey();
        }
        public static void printBreakLine(string lineType, int rowLength=30) => Console.WriteLine(padRight(lineType, rowLength, char.Parse(lineType)));
        public static string padRight(string word, int numberOfFiilingCells, char fillChar)
        {
            if (word.Length >= numberOfFiilingCells)
                return word;

            for (int i = word.Length; i < numberOfFiilingCells; i++)
                word += fillChar;

            return word;
        }
        public static char confirmationMessage(string msg, string accountNumber)
        {
            Console.Write("Are you sure to " + msg + " Acc. ({0}) Y/N ?", accountNumber);
            return char.Parse(Console.ReadLine().ToLower());
        }
        public static void PrintClient(BankClient client)
        {
            Console.WriteLine();
            Console.WriteLine("Client Card: ");
            Console.WriteLine("____________________________________");
            Console.WriteLine("Acc. Number : {0}", client.AccountNumber);
            Console.WriteLine("Pin Code : {0}", client.PinCode);
            Console.WriteLine("First Name : {0}", client.FirstName);
            Console.WriteLine("Last Name : {0}", client.LastName);
            Console.WriteLine("Email : {0}", client.Email);
            Console.WriteLine("Phone : {0}", client.Phone);
            Console.WriteLine("Balance : {0}", client.AccountBalance);
            Console.WriteLine("____________________________________");
        }
        public static int userMenuChoose(int to)
        {
            Console.Write("Choose What do you want to do [1 - {0}]: ", to);
            int userChoose = int.Parse(Console.ReadLine());

            while (userChoose < 1 || userChoose > to)
            {
                Console.Write("Invalid Menu Number, Please choose between 1 and {0}: ", to);
                userChoose = int.Parse(Console.ReadLine());
            }

            return userChoose;
        }
    }
}