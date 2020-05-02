using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unclassified.Net;

namespace CoinMachine.Library
{
    internal class TCPObserver
    {
        public Action TcpOverride;
        public Action TcpRunning;

        public AsyncTcpListener server;

        public TCPObserver()
        {
            _ = RunAsync2();
        }

        public async Task RunAsync2()
        {
            server = new AsyncTcpListener
            {
                IPAddress = IPAddress.IPv6Any,
                Port = 12345,
                ClientConnectedCallback = tcpClient =>
                    new AsyncTcpClient
                    {
                        ServerTcpClient = tcpClient,
                        ConnectedCallback = async (serverClient, isReconnected) =>
                        {
                            await Task.Delay(500);
                            // byte[] bytes = Encoding.UTF8.GetBytes($"Hello, {tcpClient.Client.RemoteEndPoint}, my name is Server. Talk to me.");
                            byte[] bytes = Encoding.UTF8.GetBytes($"connected");
                            await serverClient.Send(new ArraySegment<byte>(bytes, 0, bytes.Length));
                            TcpRunning?.Invoke();
                        },
                        ReceivedCallback = async (serverClient, count) =>
                        {
                            //serverClient.AutoReconnect = true;
                            //byte[] bytes = serverClient.ByteBuffer.Buffer;

                            await Task.Delay(500);

                            byte[] bytes = serverClient.ByteBuffer.Dequeue(count);

                            string message = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                            Console.WriteLine(message);
                            Console.WriteLine("paso");
                            if (message.Trim().Equals("override"))
                            {
                                TcpOverride?.Invoke();
                            }

                            //  bytes = Encoding.UTF8.GetBytes("You said: " + message);
                            //await serverClient.Send(new ArraySegment<byte>(bytes, 0, bytes.Length));

                            //  if (message == "bye")
                            // {
                            // Let the server close the connection
                            //serverClient.Disconnect();
                            // }
                        }
                    }.RunAsync()
            };
            await server.RunAsync();
        }

        public void StopServer()
        {
            server.Stop(false);
        }
    }
}