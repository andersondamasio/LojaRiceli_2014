﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LojaEntities : DbContext
    {
        public LojaEntities()
            : base("name=LojaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BoletimInfo> BoletimInfo { get; set; }
        public virtual DbSet<Carrinho> Carrinho { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<ClienteEnderecoAdicional> ClienteEnderecoAdicional { get; set; }
        public virtual DbSet<Configuracao> Configuracao { get; set; }
        public virtual DbSet<ConfiguracaoBoleto> ConfiguracaoBoleto { get; set; }
        public virtual DbSet<Cupom> Cupom { get; set; }
        public virtual DbSet<Entrega> Entrega { get; set; }
        public virtual DbSet<FormaPagamento> FormaPagamento { get; set; }
        public virtual DbSet<GatPagSeguro> GatPagSeguro { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<LojaCon> LojaCon { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<MensagemSistema> MensagemSistema { get; set; }
        public virtual DbSet<Parcelamento> Parcelamento { get; set; }
        public virtual DbSet<Parcelamento_FormaPagamento> Parcelamento_FormaPagamento { get; set; }
        public virtual DbSet<ParcelamentoParcela> ParcelamentoParcela { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoFeed> PedidoFeed { get; set; }
        public virtual DbSet<PedidoProduto> PedidoProduto { get; set; }
        public virtual DbSet<PedidoStatus> PedidoStatus { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Produto_Grupo> Produto_Grupo { get; set; }
        public virtual DbSet<ProdutoInfo> ProdutoInfo { get; set; }
        public virtual DbSet<ProdutoInfoItem> ProdutoInfoItem { get; set; }
        public virtual DbSet<ProdutoSku> ProdutoSku { get; set; }
        public virtual DbSet<ProdutoSkuAviso> ProdutoSkuAviso { get; set; }
        public virtual DbSet<ProdutoSkuCor> ProdutoSkuCor { get; set; }
        public virtual DbSet<ProdutoSkuFoto> ProdutoSkuFoto { get; set; }
        public virtual DbSet<ProdutoSkuTamanho> ProdutoSkuTamanho { get; set; }
        public virtual DbSet<SocialConexao> SocialConexao { get; set; }
        public virtual DbSet<SocialConfig> SocialConfig { get; set; }
        public virtual DbSet<SocialPerfil> SocialPerfil { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    
        public virtual ObjectResult<Nullable<int>> gru_SelecionarNosFinais(string gru_nomeAmigavel, Nullable<int> loj_id)
        {
            var gru_nomeAmigavelParameter = gru_nomeAmigavel != null ?
                new ObjectParameter("gru_nomeAmigavel", gru_nomeAmigavel) :
                new ObjectParameter("gru_nomeAmigavel", typeof(string));
    
            var loj_idParameter = loj_id.HasValue ?
                new ObjectParameter("loj_id", loj_id) :
                new ObjectParameter("loj_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("gru_SelecionarNosFinais", gru_nomeAmigavelParameter, loj_idParameter);
        }
    }
}
