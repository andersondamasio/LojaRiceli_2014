using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.Painel.ClienteX;

namespace _2_Library.Dao.Painel.CarrinhoX
{
    public class CarrinhoDto
    {

        public int car_id { get; set; }
        public string car_sessionId { get; set; }
        public int car_quantidade { get; set; }
        public int? cli_id { get; set; }
        public int proSku_id { get; set; }
        public int pro_id { get; set; }
        public string pro_nome { get; set; }
        public string pro_nomeAmigavel { get; set; }
        public string proSku_nome { get; set; }
        public decimal proSku_altura { get; set; }
        public decimal proSku_largura { get; set; }
        public decimal proSku_comprimento { get; set; }
        public decimal proSku_peso { get; set; }
        public string proSkuCor_nome { get; set; }
        public string proSkuTam_nome { get; set; }
        public int? proSku_prazoEntregaAdicional { get; set; }
        public decimal proSku_precoCusto { get; set; }
        public decimal proSku_precoAnterior { get; set; }
        public decimal proSku_precoVenda { get; set; }
        public decimal car_itemSubTotal { get; set; }
        public decimal proSku_percDesconto { get; set; }
        public int? proSku_quantidadeDisponivel { get; set; }
        public bool proSku_disponivel { get; set; }
        public ClienteDto cliente { get; set; }
        public DateTime car_dataHora { get; set; }
        public int loj_id { get; set; }
    }
}
