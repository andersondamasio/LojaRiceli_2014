﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CorreiosEntities : DbContext
    {
        public CorreiosEntities()
            : base("name=CorreiosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<FRETE> FRETE { get; set; }
        public DbSet<LOG_BAIRRO> LOG_BAIRRO { get; set; }
        public DbSet<LOG_CONTROLE> LOG_CONTROLE { get; set; }
        public DbSet<LOG_CPC> LOG_CPC { get; set; }
        public DbSet<LOG_FAIXA_BAIRRO> LOG_FAIXA_BAIRRO { get; set; }
        public DbSet<LOG_FAIXA_CPC> LOG_FAIXA_CPC { get; set; }
        public DbSet<LOG_FAIXA_LOCALIDADE> LOG_FAIXA_LOCALIDADE { get; set; }
        public DbSet<LOG_FAIXA_UF> LOG_FAIXA_UF { get; set; }
        public DbSet<LOG_FAIXA_UOP> LOG_FAIXA_UOP { get; set; }
        public DbSet<LOG_GRANDE_USUARIO> LOG_GRANDE_USUARIO { get; set; }
        public DbSet<LOG_LOCALIDADE> LOG_LOCALIDADE { get; set; }
        public DbSet<LOG_LOGRADOURO> LOG_LOGRADOURO { get; set; }
        public DbSet<LOG_TIPO_LOGR> LOG_TIPO_LOGR { get; set; }
        public DbSet<LOG_UNID_OPER> LOG_UNID_OPER { get; set; }
        public DbSet<SERVICO> SERVICO { get; set; }
        public DbSet<ATUALIZACOES_CONFIG> ATUALIZACOES_CONFIG { get; set; }
    }
}