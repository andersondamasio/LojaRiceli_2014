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
    
    public partial class LOG_FAIXA_BAIRRO
    {
        public int BAI_NU_SEQUENCIAL { get; set; }
        public int FCB_NU_ORDEM { get; set; }
        public string FCB_RAD_INI { get; set; }
        public string FCB_SUF_INI { get; set; }
        public string FCB_RAD_FIM { get; set; }
        public string FCB_SUF_FIM { get; set; }
    
        public virtual LOG_BAIRRO LOG_BAIRRO { get; set; }
    }
}
