//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Beans
{
    using System;
    using System.Collections.Generic;
    [Serializable]
    public partial class Enseignant
    {
        public Enseignant()
        {
            this.Seance2 = new HashSet<Seance2>();
        }
    
        public int id_enseignant { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<int> id_module { get; set; }
    
        public virtual module2 module2 { get; set; }
        public virtual ICollection<Seance2> Seance2 { get; set; }
    }
}
