using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Beans;
namespace AssistantPedagogique
{
    // Static class 'Program' acting as the application entry point
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {

          
            // Setting up a TCP channel for communication
            TcpChannel ch = new TcpChannel(8086);
            ChannelServices.RegisterChannel(ch);


            // Registering the service types for remote access
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(AssistantPedagogique.Classes.AuthEnsImpl),
                                                               "AuthEns",
                                                             WellKnownObjectMode.Singleton);

            RemotingConfiguration.RegisterWellKnownServiceType(typeof(AssistantPedagogique.Classes.AuthEtuImpl),
                                                               "AuthEtu",
                                                             WellKnownObjectMode.Singleton);

            // Setting up the visual styles and launching the login window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.LoginAssistant());


        }
    }
}
