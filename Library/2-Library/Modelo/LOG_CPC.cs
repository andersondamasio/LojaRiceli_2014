//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _2_Library.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOG_CPC
    {
        public LOG_CPC()
        {
            this.LOG_FAIXA_CPC = new HashSet<LOG_FAIXA_CPC>();
        }
    
        public int CPC_NU_SEQUENCIAL { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU_SEQUENCIAL { get; set; }
        public string CEP { get; set; }
        public string CPC_NO { get; set; }
        public string CPC_ENDERECO { get; set; }
        public string CPC_TIPO { get; set; }
        public string CPC_ABRANGENCIA { get; set; }
        public string CPC_KEY_DNE { get; set; }
    
        public virtual LOG_LOCALIDADE LOG_LOCALIDADE { get; set; }
        public virtual ICollection<LOG_FAIXA_CPC> LOG_FAIXA_CPC { get; set; }
    }
}
