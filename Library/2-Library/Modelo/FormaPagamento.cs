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
    
    public partial class FormaPagamento
    {
        public FormaPagamento()
        {
            this.Parcelamento_FormaPagamento = new HashSet<Parcelamento_FormaPagamento>();
        }
    
        public int forPag_id { get; set; }
        public string forPag_nome { get; set; }
        public Nullable<int> forPag_tipo { get; set; }
        public int forPag_prazoPagamento { get; set; }
        public Nullable<decimal> forPag_valorDesconto { get; set; }
        public Nullable<decimal> forPag_percentualDesconto { get; set; }
        public bool forPag_bloquear { get; set; }
        public System.DateTime forPag_dataHora { get; set; }
        public int loj_id { get; set; }
    
        public virtual LojaCon LojaCon { get; set; }
        public virtual ICollection<Parcelamento_FormaPagamento> Parcelamento_FormaPagamento { get; set; }
    }
}
