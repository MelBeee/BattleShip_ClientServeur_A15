using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace Battleship
{
    class ThreadJeu
    {
        TcpClient unClient;
        NetworkStream netStream;
        string PositionRecu = "";

        public ThreadJeu(TcpClient client, NetworkStream Stream)
        {
            unClient = client;
            netStream = Stream;
        }

        public void Demarrer()
        {
            byte[] bytes = new byte[unClient.ReceiveBufferSize];

            netStream.Read(bytes, 0, (int)unClient.ReceiveBufferSize);

            PositionRecu = Encoding.UTF8.GetString(bytes);
        }

        public string GetPosition()
        {
            return PositionRecu;
        }
    }
}