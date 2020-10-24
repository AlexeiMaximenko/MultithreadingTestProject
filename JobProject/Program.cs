using System;
using System.Collections.Generic;
using System.Threading;

namespace JobProject
    {
    internal class Program
        {
        private static List<Client> listClients;
        private static int[,] array;
        private static List<Thread> threads;

        private static void Main(string[] args)
            {
            threads = new List<Thread>();
            listClients = new List<Client>();
            var countThread = 50;
            array = Calculate.TaskCalculate(2018, countThread);

            for (int i = 0; i < countThread; i++)
                {
                var thread = new Thread(new ParameterizedThreadStart(CreateThread));
                threads.Add(thread);
                thread.Start(i);
                }
            Console.ReadKey();
            }

        private static void CreateThread(object i)
            {
            var client = new Client(array[(int)i, 0], array[(int)i, 1], (int)i + 1);
            listClients.Add(client);
            client.Start();
            }
        }
    }