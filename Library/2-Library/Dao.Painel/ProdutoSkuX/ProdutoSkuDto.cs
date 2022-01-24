using System;

namespace _2_Library.Dao.Painel.ProdutoSkuX
{
    public class ProdutoSkuDto
    {
        public Int32 proSku_id { get; set; }
        public String proSku_idReferencia { get; set; }
        public String proSku_nome { get; set; }
        public Decimal proSku_precoAnterior { get; set; }
        public Decimal proSku_precoVenda { get; set; }
        public Decimal proSku_precoCusto { get; set; }
        public Decimal proSku_peso { get; set; }
        public Decimal proSku_altura { get; set; }
        public Decimal proSku_largura { get; set; }
        public Decimal proSku_comprimento { get; set; }
        public int? proSku_prazoEntregaAdicional { get; set; }
        public int? proSku_quantidadeMaxima { get; set; }
        public int? proSku_quantidadeDisponivel { get; set; }
        public Boolean proSku_disponivel { get; set; }
        public bool? proSku_destaque { get; set; }
        public Int32 proSku_posicao { get; set; }
        public Boolean proSku_bloquear { get; set; }
        public int? proSku_excluidoX { get; set; }
        public DateTime? proSku_dataHoraAtualizacao { get; set; }
        public DateTime proSku_dataHora { get; set; }
        public int? proSkuTam_id { get; set; }
        public int? proSkuCor_id { get; set; }
        public Int32 parc_id { get; set; }
        public int? ent_id { get; set; }
        public Int32 pro_id { get; set; }
        public Int32 loj_id { get; set; }
        public ProdutoSkuCorDto produtoSkuCorDto { get; set; }
        public ProdutoSkuTamanhoDto produtoSkuTamanhoDto { get; set; }
        public ProdutoSkuFotoDto produtoSkuFotoDto { get; set; }
        
    }

    public partial class ProdutoSkuCorDto
    {
        public Int32 proSkuCor_id { get; set; }
        public String proSkuCor_nome { get; set; }
        public String proSkuCor_imagem { get; set; }
        public Boolean proSkuCor_bloquear { get; set; }
        public DateTime? proSkuCor_dataHora { get; set; }
        public Int32 loj_id { get; set; }
    }

    public partial class ProdutoSkuTamanhoDto
    {

        public Int32 proSkuTam_id { get; set; }
        public String proSkuTam_nome { get; set; }
        public String proSkuTam_imagem { get; set; }
        public Boolean proSkuTam_bloquear { get; set; }
        public DateTime proSkuTam_dataHora { get; set; }
        public Int32 loj_id { get; set; }
    }

   /* public partial class ProdutoSkuFotoDto
    {
        public Int32 proSkuFot_id { get; set; }
        public String proSkuFot_nome { get; set; }
        public String proSkuFot_extensao { get; set; }
        public Int32 proSkuFot_posicao { get; set; }
        public String proSkuFot_titulo { get; set; }
        public DateTime? proSkuFot_dataHoraAtualizacao { get; set; }
        public DateTime proSkuFot_dataHora { get; set; }
        public Int32 proSku_id { get; set; }
        public Int32 loj_id { get; set; }
    }*/

}



