using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JobProject
    {
    public class Client
        {
        private int id;
        private string ip = "88.212.241.115";
        private int port = 2012;
        private TcpClient client;
        private List<int> threadResult;
        private string dataReadOnServer = "";
        private string localResultNumber = "";
        private NetworkStream stream;
        private int lastWriteNumber;
        private int startNumber;
        private int finalNumber;
        private Log log;

        public Client(int startNumber, int finalNumber, int id)
            {
            this.id = id;
            log = new Log(GetLogPath());
            GetRangeNumber(startNumber, finalNumber);
            lastWriteNumber = startNumber;
            threadResult = new List<int>();
            }

        public void Start()
            {
            Connect();
            StartListining();
            }

        private void Connect()
            {
            try
                {
                client = new TcpClient();
                var result = client.BeginConnect(ip, port, null, null);
                result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                stream = client.GetStream();
                log.Logging("Connected");
                Thread.Sleep(3000);
                }
            catch (Exception e)
                {
                log.Logging($"Exeption {e.Message}");
                Thread.Sleep(3000);
                Connect();
                }
            }

        private void StartListining()
            {
            for (int i = lastWriteNumber; i <= finalNumber; i++)
                {
                SentMessageOnServer(i.ToString());
                Thread.Sleep(1000);
                ReadMessageOnServer();
                }
            }

        private void SentMessageOnServer(string message)
            {
            var dataWriteOnServer = Encoding.ASCII.GetBytes($"{message}\n");
            stream.Write(dataWriteOnServer, 0, dataWriteOnServer.Length);
            log.Logging($"Sent: {message}\n");
            }

        private void ReadMessageOnServer()
            {
            while (true)
                {
                try
                    {
                    byte[] bytes = new byte[1];

                    stream.Read(bytes, 0, 1);

                    dataReadOnServer = Encoding.UTF8.GetString(bytes);

                    ;
                    if (int.TryParse(dataReadOnServer, out int onlyInt))
                        {
                        localResultNumber += onlyInt.ToString();
                        continue;
                        }
                    if (dataReadOnServer == "\n")
                        {
                        threadResult.Add(Convert.ToInt32(localResultNumber));
                        log.Logging($"Final result: {localResultNumber}");
                        log.Logging($"Count final result: {threadResult.Count}");
                        localResultNumber = "";
                        lastWriteNumber++;
                        return;
                        }
                    }
                catch (Exception e)
                    {
                    log.Logging($"Exeption {e.Message}");
                    Connect();
                    StartListining();
                    }
                }
            }

        private void GetRangeNumber(int startNumber, int finalNumber)
            {
            this.startNumber = startNumber;
            this.finalNumber = finalNumber;
            }

        private string GetLogPath()
            {
            return $"logClient{id}.txt";
            }
        }
    }