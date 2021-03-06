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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
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
    
        public DbSet<BoletimInfo> BoletimInfo { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ClienteEnderecoAdicional> ClienteEnderecoAdicional { get; set; }
        public DbSet<Configuracao> Configuracao { get; set; }
        public DbSet<ConfiguracaoBoleto> ConfiguracaoBoleto { get; set; }
        public DbSet<Cupom> Cupom { get; set; }
        public DbSet<Entrega> Entrega { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<LojaCon> LojaCon { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<MensagemSistema> MensagemSistema { get; set; }
        public DbSet<Parcelamento> Parcelamento { get; set; }
        public DbSet<Parcelamento_FormaPagamento> Parcelamento_FormaPagamento { get; set; }
        public DbSet<ParcelamentoParcela> ParcelamentoParcela { get; set; }
        public DbSet<PedidoStatus> PedidoStatus { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Produto_Grupo> Produto_Grupo { get; set; }
        public DbSet<ProdutoInfo> ProdutoInfo { get; set; }
        public DbSet<ProdutoInfoItem> ProdutoInfoItem { get; set; }
        public DbSet<ProdutoSkuAviso> ProdutoSkuAviso { get; set; }
        public DbSet<ProdutoSkuCor> ProdutoSkuCor { get; set; }
        public DbSet<ProdutoSkuFoto> ProdutoSkuFoto { get; set; }
        public DbSet<ProdutoSkuTamanho> ProdutoSkuTamanho { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Carrinho> Carrinho { get; set; }
        public DbSet<PedidoProduto> PedidoProduto { get; set; }
        public DbSet<ProdutoSku> ProdutoSku { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<GatPagSeguro> GatPagSeguro { get; set; }
    
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
