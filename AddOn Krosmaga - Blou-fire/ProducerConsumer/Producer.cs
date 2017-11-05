using SniffingLibs;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace AddOn_Krosmaga___Blou_fire.ProducerConsumer
{
    public class Producer
    {
        private Socket mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] byteData = new byte[2048];

        private Queue<byte[]> _queue;
        private SyncEvents _syncEvents;

        private BlockingCollection<byte[]> bQueue;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Producer(Queue<byte[]> q, SyncEvents e)
        {
            _queue = q;
            _syncEvents = e;
        }

        public Producer(BlockingCollection<byte[]> value)
        {
            bQueue = value;
            ThreadRun();
        }

        // Producer.ThreadRun
        public void ThreadRun()
        {
            string ipadd = "";
            IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
            if (HosyEntry.AddressList.Length > 0)
            {
                foreach (IPAddress ip in HosyEntry.AddressList)
                {
                    logger.Info("ip.AddressFamily = " + ip.AddressFamily + " / AddressFamily.InterNetwork = " + AddressFamily.InterNetwork + " / ipadd = " + ip.ToString());
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipadd = ip.ToString();
                        break;
                    }
                }
            }
            if (ipadd == "")
            {
                logger.Error("Error : ipadd is empty (ip.AddressFamily never equal to AddressFamily.InterNetwork)");
                logger.Error("Error : HosyEntry.AddressList.Length = " + HosyEntry.AddressList.Length);
            }
            try
            {
                mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            }
            catch (Exception e)
            {
                logger.Error("Error : new Socket() " + e);
            }
            try
            {
                mainSocket.Bind(new IPEndPoint(IPAddress.Parse(ipadd), 4988));
            }
            catch (Exception e)
            {
                logger.Error("Error : mainSocket.Bind() " + e);
            }
            try
            {
                mainSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
            }
            catch (Exception e)
            {
                logger.Error("Error : mainSocket.SetSocketOption() " + e);
            }
            byte[] byTrue = new byte[4] {1, 0, 0, 0};
            byte[] byOut = new byte[4] {0, 0, 0, 0};
            try
            {
                mainSocket.IOControl(IOControlCode.ReceiveAll, BitConverter.GetBytes(1), null);
            }
            catch (Exception e)
            {
                logger.Error("Error : mainSocket.IOControl() " + e);
            }
            try
            {
                mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
            catch (Exception e)
            {
                logger.Error("Error : mainSocket.BeginReceive() " + e);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                int nReceived = mainSocket.EndReceive(ar);

                //Analyze the bytes received...

                ParseData(byteData, nReceived);

                byteData = new byte[4096];
                mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
            catch (Exception e)
            {
                logger.Error("Error : private void OnReceive " + e);
                byteData = new byte[4096];
                ThreadRun();
            }
        }

        private void ParseData(byte[] byteData, int nReceived)
        {
            try
            {
                IPHeader ipHeader = new IPHeader(byteData, nReceived);

                switch (ipHeader.ProtocolType)
                {
                    case Protocol.TCP:
                        TCPHeader tcpHeader = new TCPHeader(ipHeader.Data, ipHeader.MessageLength); //Length of the data field
                        if (tcpHeader.SourcePort == "4988")
                        {
                            /*lock (((ICollection)_queue).SyncRoot)
							{
							    _queue.Enqueue(byteData);
							    _syncEvents.NewItemEvent.Set();
							}*/
                            bQueue.Add(byteData);
                        }
                        break;

                    case Protocol.UDP:
                        break;

                    case Protocol.Unknown:
                        break;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error : private void ParseData " + e);
                byteData = new byte[4096];
                ThreadRun();
            }
        }
    }
}