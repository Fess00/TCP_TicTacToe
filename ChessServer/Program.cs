using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

Server server = new Server();



//string author = "Katy McClachlen";
// converts a C# string to a byte array
//byte[] bytes = Encoding.UTF8.GetBytes(author);
//string s = Encoding.UTF8.GetString(bytes);
//foreach (byte b in bytes)
//{
//    Console.WriteLine(b);
//}
//Console.WriteLine(s);



public class Server
{
    IPEndPoint ipPoint;
    Socket socket;
    List<Socket> clients;
    int turn;
    NetworkStream stream;

    public Server()
    {
        clients = new List<Socket>();
        turn = 0;
        ipPoint = new IPEndPoint(IPAddress.Any, 8080);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(ipPoint);
        Start();
    }

    private void Start()
    {
        socket.Listen(1000);
        Console.WriteLine("Started and waiting");
        int count = 0;
        byte[] bytes = new byte[256];
        while (true)
        {
            if (count < 2)
            {
                clients.Add(socket.Accept());
                stream = new NetworkStream(clients[count]);
                bytes = Encoding.UTF8.GetBytes($"Hello player {count + 1}!");
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                count++;
            }
            else
            {
                if (turn == 0)
                {
                    stream = new NetworkStream(clients[0]);
                    byte[] response = new byte[256];
                    stream.Read(response);
                    string s = Encoding.UTF8.GetString(response);
                    Console.WriteLine(s);
                    turn = 1;
                }
                else if (turn == 1)
                {
                    stream = new NetworkStream(clients[1]);
                    byte[] response = new byte[256];
                    stream.Read(response);
                    string s = Encoding.UTF8.GetString(response);
                    Console.WriteLine(s);
                    turn = 0;
                    stream.Flush();
                }
            }
        }
        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].Close();
        }
        stream.Close();
        socket.Close();
    }
}

[Serializable]
public class TicTacToe
{
    private List<List<string>> board;
    public string WinState { get; private set; }
    
    public TicTacToe()
    {
        board = new List<List<string>>();
        SetUpBoard();
        WinState = "None";
    }

    private void SetUpBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            board.Add(new List<string>());
            for(int j = 0; j < 3; j++)
            {
                board[i].Add("-");
            }
        }
    }

    public void DrawBoard()
    {
        foreach (var row in board)
        {
            foreach(var cell in row)
            {
                Console.Write(cell + " ");

            }
            Console.WriteLine();
        }
    }

    public void SetFigure(string figure, int x, int y)
    {
        figure = figure.ToUpper();
        board[x][y] = figure;
    }

    public void WinX()
    {
        if (board[0][0] == "X" && board[0][1] == "X" && board[0][2] == "X")
        {
            WinState = "X";
        }
        else if (board[1][0] == "X" && board[1][1] == "X" && board[1][2] == "X")
        {
            WinState = "X";
        }
        else if (board[2][0] == "X" && board[2][1] == "X" && board[2][2] == "X")
        {
            WinState = "X";
        }
        else if (board[0][0] == "X" && board[1][0] == "X" && board[2][0] == "X")
        {
            WinState = "X";
        }
        else if (board[0][1] == "X" && board[1][1] == "X" && board[2][1] == "X")
        {
            WinState = "X";
        }
        else if (board[0][2] == "X" && board[1][2] == "X" && board[2][2] == "X")
        {
            WinState = "X";
        }
        else if (board[0][0] == "X" && board[1][1] == "X" && board[2][2] == "X")
        {
            WinState = "X";
        }
        else if (board[0][2] == "X" && board[1][1] == "X" && board[2][0] == "X")
        {
            WinState = "X";
        }
    }

    public void WinO()
    {
        if (board[0][0] == "O" && board[0][1] == "O" && board[0][2] == "O")
        {
            WinState = "O";
        }
        else if (board[1][0] == "O" && board[1][1] == "O" && board[1][2] == "O")
        {
            WinState = "O";
        }
        else if (board[2][0] == "O" && board[2][1] == "O" && board[2][2] == "O")
        {
            WinState = "O";
        }
        else if (board[0][0] == "O" && board[1][0] == "O" && board[2][0] == "O")
        {
            WinState = "O";
        }
        else if (board[0][1] == "O" && board[1][1] == "O" && board[2][1] == "O")
        {
            WinState = "O";
        }
        else if (board[0][2] == "O" && board[1][2] == "O" && board[2][2] == "O")
        {
            WinState = "O";
        }
        else if (board[0][0] == "O" && board[1][1] == "O" && board[2][2] == "O")
        {
            WinState = "O";
        }
        else if (board[0][2] == "O" && board[1][1] == "O" && board[2][0] == "O")
        {
            WinState = "O";
        }
    }
}

