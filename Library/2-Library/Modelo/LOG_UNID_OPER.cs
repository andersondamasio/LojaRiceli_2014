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
    
    public partial class LOG_UNID_OPER
    {
        public LOG_UNID_OPER()
        {
            this.LOG_FAIXA_UOP = new HashSet<LOG_FAIXA_UOP>();
        }
    
        public int UOP_NU_SEQUENCIAL { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU_SEQUENCIAL { get; set; }
        public Nullable<int> LOG_NU_SEQUENCIAL { get; set; }
        public int BAI_NU_SEQUENCIAL { get; set; }
        public string UOP_NO { get; set; }
        public string CEP { get; set; }
        public string UOP_ENDERECO { get; set; }
        public string UOP_IN_CP { get; set; }
        public string UOP_KEY_DNE { get; set; }
    
        public virtual LOG_BAIRRO LOG_BAIRRO { get; set; }
        public virtual ICollection<LOG_FAIXA_UOP> LOG_FAIXA_UOP { get; set; }
        public virtual LOG_LOCALIDADE LOG_LOCALIDADE { get; set; }
        public virtual LOG_LOGRADOURO LOG_LOGRADOURO { get; set; }
    }
}
