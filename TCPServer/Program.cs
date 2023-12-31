﻿using System;
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
    TicTacToe board;

    public Server()
    {
        board = new TicTacToe();
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
                count++;
            }
            else
            {                
                if (turn == 0)
                {
                    stream = new NetworkStream(clients[0]);
                    string desk = board.ConvertBoard();
                    board.DrawBoard();
                    bytes = Encoding.UTF8.GetBytes(desk);
                    stream.Write(bytes, 0, 9);
                    byte[] response = new byte[256];
                    stream.Read(response);
                    string s = Encoding.UTF8.GetString(response);
                    board.SetFigure("X", s);
                    board.WinX();
                    if (board.WinState == "X")
                    {
                        break;
                    }
                    turn = 1;
                }
                else if (turn == 1)
                {
                    stream = new NetworkStream(clients[1]);
                    string desk = board.ConvertBoard();
                    board.DrawBoard();
                    bytes = Encoding.UTF8.GetBytes(desk, 0, 9);
                    stream.Write(bytes, 0, 9);
                    byte[] response = new byte[256];
                    stream.Read(response);
                    string s = Encoding.UTF8.GetString(response);
                    board.SetFigure("O", s);
                    board.WinO();
                    if (board.WinState == "O")
                    {
                        break;
                    }
                    turn = 0;
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

    public void SetFigure(string figure, string mes)
    {
        if (mes[0] == 'l')
        {
            if (mes[1] == 'u')
            {
                board[0][0] = figure;
            }
            else if (mes[1] == 'm')
            {
                board[1][0] = figure;
            }
            else if (mes[1] == 'd')
            {
                board[2][0] = figure;
            }
        }
        else if (mes[0] == 'm')
        {
            if (mes[1] == 'u')
            {
                board[0][1] = figure;
            }
            else if (mes[1] == 'm')
            {
                board[1][1] = figure;
            }
            else if (mes[1] == 'd')
            {
                board[2][1] = figure;
            }
        }
        else if (mes[0] == 'r')
        {
            if (mes[1] == 'u')
            {
                board[0][2] = figure;
            }
            else if (mes[1] == 'm')
            {
                board[1][2] = figure;
            }
            else if (mes[1] == 'd')
            {
                board[2][2] = figure;
            }
        }
    }

    public string ConvertBoard()
    {
        string b = "";
        foreach (var row in this.board)
        {
            foreach (var cell in row)
            {
                b += cell;

            }
        }

        return b;
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

