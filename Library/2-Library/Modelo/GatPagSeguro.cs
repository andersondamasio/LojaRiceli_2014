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
    
    public partial class GatPagSeguro
    {
        public int gatps_id { get; set; }
        public string gatps_email { get; set; }
        public string gatps_token { get; set; }
        public int gatps_parcelasSemJuros { get; set; }
        public decimal gatps_percentualJuro { get; set; }
        public int loj_id { get; set; }
    
        public virtual LojaCon LojaCon { get; set; }
    }
}
