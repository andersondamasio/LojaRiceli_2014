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
    
    public partial class LOG_BAIRRO
    {
        public LOG_BAIRRO()
        {
            this.LOG_FAIXA_BAIRRO = new HashSet<LOG_FAIXA_BAIRRO>();
            this.LOG_GRANDE_USUARIO = new HashSet<LOG_GRANDE_USUARIO>();
            this.LOG_LOGRADOURO = new HashSet<LOG_LOGRADOURO>();
            this.LOG_UNID_OPER = new HashSet<LOG_UNID_OPER>();
        }
    
        public int BAI_NU_SEQUENCIAL { get; set; }
        public string UFE_SG { get; set; }
        public int LOC_NU_SEQUENCIAL { get; set; }
        public string BAI_NO { get; set; }
        public string BAI_NO_ABREV { get; set; }
    
        public virtual LOG_LOCALIDADE LOG_LOCALIDADE { get; set; }
        public virtual ICollection<LOG_FAIXA_BAIRRO> LOG_FAIXA_BAIRRO { get; set; }
        public virtual ICollection<LOG_GRANDE_USUARIO> LOG_GRANDE_USUARIO { get; set; }
        public virtual ICollection<LOG_LOGRADOURO> LOG_LOGRADOURO { get; set; }
        public virtual ICollection<LOG_UNID_OPER> LOG_UNID_OPER { get; set; }
    }
}
