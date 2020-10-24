using System;
using System.Collections.Generic;
using System.Threading;

namespace JobProject
    {
    class Program
        {
        private static List<Client> listClients;
        private static List<ulong> finalResult;
        private static int[,] array;
        private static int countNumber = 2018;
        private static List<Thread> threads;

        private static void Main(string[] args)
            {
            finalResult = new List<ulong>();
            threads = new List<Thread>();
            listClients = new List<Client>();
            var countThread = 100;

            array = Calculate.TaskCalculate(countNumber, countThread);
            var arrayLength = array.GetLength(0);
            for (int i = 0; i < arrayLength; i++)
                {
                var thread = new Thread(new ParameterizedThreadStart(CreateThread));
                threads.Add(thread);
                thread.Start(i);
                }
            Thread.Sleep(5000);
            while (true)
                {
                if (listClients.Count != arrayLength)
                    {
                    continue;
                    }
                var completeNumberCount = 0;

                foreach (var client in listClients)
                    {
                    completeNumberCount += client.GetCompleteNumbersCount();
                    }

                Console.WriteLine($"Count complete number {completeNumberCount}/{countNumber}");
                if (IsEnd())
                    {
                    var medianNumbers = Calculate.GetMedianNumber(finalResult);
                    Console.WriteLine($"Task comlete. Median all numbers = {medianNumbers}");
                    Console.ReadKey();
                    return;
                    }
                Thread.Sleep(5000);
                Console.Clear();
                }
            }

        private static void CreateThread(object i)
            {
            var client = new Client(array[(int)i, 0], array[(int)i, 1], (int)i);
            listClients.Add(client);
            client.EndClientWork += Client_EndClientWork;
            client.Start();
            }

        private static void Client_EndClientWork(object sender, Client.EndClientTaskEventArgs e)
            {
            var clientResult = e.Client.GetClientResult();
            foreach (var number in clientResult)
                {
                finalResult.Add(number);
                }
            e.Client.Stop();
            var id = e.Client.id;
            threads[id].Abort();
            }
        private static bool IsEnd()
            {
            return finalResult.Count == countNumber;
            }
        }
    }
