//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class SeanceAssiste
    {
        public int id_seanceAssiste { get; set; }
        public Nullable<int> id_seance { get; set; }
        public Nullable<int> id_etudiant { get; set; }
    
        public virtual Etudiant Etudiant { get; set; }
        public virtual Seance2 Seance2 { get; set; }
    }
}
