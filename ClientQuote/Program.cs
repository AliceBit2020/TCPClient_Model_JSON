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
       
        static async Task Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient(IPAddress.Loopback.ToString(),49200);

            NetworkStream netstream = tcpClient.GetStream();

            ////Модель
            Message msg= new Message();
            msg.mes = "get quote";
            msg.host=Dns.GetHostName();
            msg.user = Environment.UserName;

       
            ///Підготовка моделі до відправки
            string json = JsonConvert.SerializeObject(msg, Formatting.None);
            byte[] bytes=Encoding.Default.GetBytes(json);


            ////Відправка сповіщення
             netstream.Write(bytes, 0, bytes.Length); 
             await  netstream.WriteAsync(bytes, 0, bytes.Length);

            ///Вивільнення ресурсів
            netstream.Close();
            tcpClient.Close();

        }
    }
}
