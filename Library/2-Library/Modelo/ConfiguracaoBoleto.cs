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
    
    public partial class ConfiguracaoBoleto
    {
        public int conBol_id { get; set; }
        public string conBol_nome { get; set; }
        public int conBol_codigoBanco { get; set; }
        public string conBol_nossoNumero { get; set; }
        public string conBol_carteira { get; set; }
        public string conBol_cendenteCpfCnpj { get; set; }
        public string conBol_cendenteNome { get; set; }
        public string conBol_cendenteAgencia { get; set; }
        public string conBol_cendenteAgenciaDigito { get; set; }
        public string conBol_cendenteConta { get; set; }
        public string conBol_cendenteContaDigito { get; set; }
        public string conBol_cendenteContaOperacao { get; set; }
        public int conBol_cendenteConvenio { get; set; }
        public string conBol_localPagamento { get; set; }
        public string conBol_instrucao1Descricao { get; set; }
        public int conBol_prazoPagamento { get; set; }
        public bool conBol_ativar { get; set; }
        public int loj_id { get; set; }
    
        public virtual LojaCon LojaCon { get; set; }
    }
}
