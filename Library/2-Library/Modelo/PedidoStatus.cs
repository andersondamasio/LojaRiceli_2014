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
    
    public partial class PedidoStatus
    {
        public int pedStat_id { get; set; }
        public string pedStat_descricao { get; set; }
        public string pedStat_observacao { get; set; }
        public System.DateTime pedStat_dataHora { get; set; }
        public int ped_id { get; set; }
        public int stat_id { get; set; }
        public int loj_id { get; set; }
    
        public virtual Pedido Pedido { get; set; }
        public virtual Status Status { get; set; }
    }
}
