using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unclassified.Net;

namespace CoinMachine.Library
{
    internal class TCPObserver
    {
        private async Task RunAsync2() {
            var server = new AsyncTcpListener
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
                            byte[] bytes = Encoding.UTF8.GetBytes($"Hello, {tcpClient.Client.RemoteEndPoint}, my name is Server. Talk to me.");
                            await serverClient.Send(new ArraySegment<byte>(bytes, 0, bytes.Length));
                            // Custom server logic
                        },
                        ReceivedCallback = async (serverClient, count) =>
                        {
                            byte[] bytes = serverClient.ByteBuffer.Dequeue(count);
                            string message = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                            Console.WriteLine("Server client: received: " + message);

                            bytes = Encoding.UTF8.GetBytes("You said: " + message);
                            await serverClient.Send(new ArraySegment<byte>(bytes, 0, bytes.Length));

                            if (message == "bye")
                            {
                                // Let the server close the connection
                                serverClient.Disconnect();
                            }
                        }
                    }.RunAsync()
            };
            await server.RunAsync();
        }
    }
}