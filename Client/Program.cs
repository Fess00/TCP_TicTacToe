// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

Client client = new Client();

//TicTacToe tic = new TicTacToe();
//tic.DrawBoard();
//tic.UpdateBoard("X--------");
//tic.DrawBoard();

class Client
{
    IPEndPoint ipPoint;
    Socket socket;
    NetworkStream stream;
    TicTacToe board;

    public Client()
    {
        board = new TicTacToe();
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect("127.0.0.1", 8080);
        NetworkStream stream = new NetworkStream(socket);
        byte[] bytesWrite = new byte[256];
        byte[] bytesRead = new byte[256];
        stream.Read(bytesRead);
        Console.WriteLine(Encoding.UTF8.GetString(bytesRead));
        string message = "";
        while (true)
        {
            stream.Flush();
            stream.Read(bytesRead, 0, 9);
            message = Encoding.UTF8.GetString(bytesRead);
            board.UpdateBoard(message);
            board.DrawBoard();
            string s = Console.ReadLine();
            bytesWrite = Encoding.UTF8.GetBytes(s);
            stream.Write(bytesWrite);
        }
    }
}

public class TicTacToe
{
    private List<List<string>> board;

    public TicTacToe()
    {
        board = new List<List<string>>();
        SetUpBoard();
    }

    private void SetUpBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            board.Add(new List<string>());
            for (int j = 0; j < 3; j++)
            {
                board[i].Add("-");
            }
        }
    }

    public void DrawBoard()
    {
        foreach (var row in board)
        {
            foreach (var cell in row)
            {
                Console.Write(cell + " ");

            }
            Console.WriteLine();
        }
        Console.WriteLine("======================================");
    }


    public void UpdateBoard(string uBoard)
    {
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i][j] = uBoard[count].ToString();
                count++;
            }
        }
    }
}

