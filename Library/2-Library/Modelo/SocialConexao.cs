//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _2_Library.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class SocialConexao
    {
        public int sp_id_conexao { get; set; }
        public int sp_id_conectado { get; set; }
        public bool sco_ativo { get; set; }
        public System.DateTime sco_dataHora { get; set; }
        public int loj_id { get; set; }
    
        public virtual SocialPerfil SocialPerfil { get; set; }
        public virtual SocialPerfil SocialPerfil1 { get; set; }
    }
}
