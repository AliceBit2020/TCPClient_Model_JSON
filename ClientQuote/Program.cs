using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;



namespace ClientQuote
{
    public struct Message
    {
        public string mes; // текст сообщения
        public string host; // имя хоста
        public string user; // имя пользователя
    }
    public class Program
    {
       
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient(IPAddress.Loopback.ToString(),49200);



            NetworkStream netstream = tcpClient.GetStream();

            Message msg= new Message();
            msg.mes = "get quote";
            msg.host=Dns.GetHostName();
            msg.user = Environment.UserName;

       

            string json = JsonConvert.SerializeObject(msg, Formatting.None);
            byte[] bytes=Encoding.Default.GetBytes(json);


             netstream.Write(bytes, 0, bytes.Length); 
            netstream.Close();
            tcpClient.Close();

        }
    }
}
