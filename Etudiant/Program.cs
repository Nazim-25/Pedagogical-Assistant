using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Etudiant
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable visual styles for the application
            Application.EnableVisualStyles();

            // Set default text rendering compatibility
            Application.SetCompatibleTextRenderingDefault(false);

            // Run the application, starting with the EtudiantLogin form
            Application.Run(new EtudiantLogin());
        }
    }
}
