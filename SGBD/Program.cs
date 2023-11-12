using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
namespace SGBD
{
    class Program
    {
       
        static void Main(string[] args)
        {
            // Create an instance of the ImplServices class
            ImplServices imp = new ImplServices();

            // Create an instance of the StudentsManagementImpl class
            StudentsManagementImpl sm = new StudentsManagementImpl();

            // Create a new TCP channel on port 8085
            TcpChannel ch = new TcpChannel(8085);
            ChannelServices.RegisterChannel(ch);


            // Register the ImplServices class as a well-known service type with the name "SGBDobj" and singleton mode
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SGBD.ImplServices),
                                                               "SGBDobj",
                                                             WellKnownObjectMode.Singleton);


           // Register the StudentsManagementImpl class as a well-known service type with the name "StudentsManagementObj" and singleton mode
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(SGBD.StudentsManagementImpl),
                                                               "StudentsManagementObj",
                                                             WellKnownObjectMode.Singleton);

            
            //SingleCall
            // Display a message indicating that the server is ready
            Console.Write("Sever is  Ready........");

            // Wait for a key press to exit the program
            Console.Read();
        }
      
        
    }
}
