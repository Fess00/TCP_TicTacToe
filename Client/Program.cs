// See https://aka.ms/new-console-template for more information

using System.Net.Sockets;
using System.Text;

var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    socket.Connect("127.0.0.1", 8080);
    NetworkStream stream = new NetworkStream(socket);
    byte[] bytes = new byte[256];
    stream.Read(bytes);
    Console.WriteLine(Encoding.UTF8.GetString(bytes));
    while (true)
    {
        stream.Flush();
        string s = Console.ReadLine();
        byte[] message = Encoding.UTF8.GetBytes(s);
        stream.Write(message);
    }
}
catch (SocketException)
{
    Console.WriteLine($"Не удалось установить подключение с {socket.RemoteEndPoint}");
}
