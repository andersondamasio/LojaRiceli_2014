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
    
    public partial class LOG_FAIXA_UF
    {
        public LOG_FAIXA_UF()
        {
            this.LOG_LOCALIDADE = new HashSet<LOG_LOCALIDADE>();
        }
    
        public string UFE_SG { get; set; }
        public string UFE_NO { get; set; }
        public string UFE_RAD1_INI { get; set; }
        public string UFE_SUF1_INI { get; set; }
        public string UFE_RAD1_FIM { get; set; }
        public string UFE_SUF1_FIM { get; set; }
        public string UFE_RAD2_INI { get; set; }
        public string UFE_SUF2_INI { get; set; }
        public string UFE_RAD2_FIM { get; set; }
        public string UFE_SUF2_FIM { get; set; }
    
        public virtual ICollection<LOG_LOCALIDADE> LOG_LOCALIDADE { get; set; }
    }
}
