using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beans
{
    [Serializable]
    public class ConnectTable
    {
        public int id_etudiant { get; set; }
        public int id_enseignant { get; set; }

        public ConnectTable(int id_etu, int id_ens)
        {
            this.id_enseignant = id_ens;
            this.id_etudiant = id_etu;
        }
        public ConnectTable()
        {
            
        }
            
    }
}
