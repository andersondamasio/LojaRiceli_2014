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
    
    public partial class Configuracao
    {
        public int con_id { get; set; }
        public Nullable<int> con_fotoVitrineAltura { get; set; }
        public Nullable<int> con_fotoVitrineLargura { get; set; }
        public Nullable<int> con_fotoDetalheAltura { get; set; }
        public Nullable<int> con_fotoDetalheLargura { get; set; }
        public Nullable<int> con_fotoMiniaturaAltura { get; set; }
        public Nullable<int> con_fotoMiniaturaLargura { get; set; }
        public Nullable<int> con_fotoAmpliadaAltura { get; set; }
        public Nullable<int> con_fotoAmpliadaLargura { get; set; }
        public Nullable<int> con_fotoProporcaoAltura { get; set; }
        public Nullable<int> con_fotoProporcaoLargura { get; set; }
        public Nullable<long> con_fotoQualidade { get; set; }
        public string con_emailRecuperarSenha { get; set; }
        public string con_emailPedidoRecebido { get; set; }
        public string con_emailPedidoStatus { get; set; }
        public int loj_id { get; set; }
        public Nullable<int> parc_id { get; set; }
    
        public virtual LojaCon LojaCon { get; set; }
    }
}
