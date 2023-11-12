using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
namespace Beans
{
    [Serializable]
    public class DateTimes
    {
       public int id_enseignant { get; set; }
       public DateTime Date { get; set; }
       public  int port { get; set; }
       public IPAddress localAddr { get; set; }
     //  public List<TcpClient> _client_list { get; set; }

       public DateTimes(int id_ens, DateTime Date, int port, IPAddress localAddr)
        {
            this.id_enseignant = id_ens;
            this.Date = Date;
            this.port = port;
            this.localAddr = localAddr;
            //this._client_list = _client_list;
        }
       public DateTimes()
        {
            
        }
    }
}
