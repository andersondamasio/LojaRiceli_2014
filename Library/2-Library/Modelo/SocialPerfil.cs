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
    
    public partial class SocialPerfil
    {
        public SocialPerfil()
        {
            this.SocialConexao = new HashSet<SocialConexao>();
            this.SocialConexao1 = new HashSet<SocialConexao>();
        }
    
        public int sp_id { get; set; }
        public string sp_idPerfil { get; set; }
        public string sp_nome { get; set; }
        public string sp_sobrenome { get; set; }
        public string sp_sexo { get; set; }
        public Nullable<System.DateTime> sp_dataNascimento { get; set; }
        public string sp_email { get; set; }
        public string sp_cidade { get; set; }
        public string sp_estado { get; set; }
        public string sp_idioma { get; set; }
        public string sp_amigos { get; set; }
        public string sp_interesses { get; set; }
        public string sp_atividades { get; set; }
        public string sp_curtidas { get; set; }
        public string sp_trabalho { get; set; }
        public string sp_sobre { get; set; }
        public string sp_site { get; set; }
        public string sp_religiao { get; set; }
        public string sp_relacionamentoStatus { get; set; }
        public Nullable<bool> sp_verificado { get; set; }
        public Nullable<System.DateTime> proSku_dataHoraAtualizacao { get; set; }
        public System.DateTime sp_dataHora { get; set; }
        public Nullable<int> cli_id { get; set; }
        public int loj_id { get; set; }
        public int sp_numeroConexoes { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual LojaCon LojaCon { get; set; }
        public virtual ICollection<SocialConexao> SocialConexao { get; set; }
        public virtual ICollection<SocialConexao> SocialConexao1 { get; set; }
    }
}
