using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Services;
using _1_WebForm.App_Code.Utils;
using _2_Library.Dao.Site.BoletimInfoX;
using _2_Library.Dao.Site.CarrinhoX;
using _2_Library.Dao.Site.CorreioX;
using _2_Library.Dao.Site.EntregaX;
using _2_Library.Dao.Site.ProdutoSkuAvisoX;
using _2_Library.Dao.Site.ProdutoSkuX;

namespace _1_WebForm.Service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceRiceli
    {
        [OperationContract]
        public void InsertProdutoSkuAviso(ProdutoSkuAvisoDto produtoSkuAvisoDto)
        {
            new ProdutoSkuAvisoTd().InsertProdutoSkuAviso(null, produtoSkuAvisoDto);
        }

        [OperationContract]
        public void InsertBoletimInfo(BoletimInfoDto boletimInfoDto)
        {
            new BoletimInfoTd().InsertBoletimInfo(null, boletimInfoDto);
        }

        [OperationContract]
        public CorreioDto SelectCorreioCalcPrazo(string ent_cep, int? proSku_id)
        {
            CorreioDto correioDto = new CorreioDto();
            List<CarrinhoDto> carrinhoListDto = null;

            int ent_cepDest = 0;
            if (!string.IsNullOrEmpty(ent_cep) && _2_Library.Utils.Validacao.ValidaInteiro(ent_cep))
                ent_cepDest = Convert.ToInt32(ent_cep);

            if (proSku_id.HasValue && ent_cepDest != 0)
            {   
                CarrinhoDto carrinhoDto = new CarrinhoDto();
                ProdutoSkuDto produtoSkuDto = new ProdutoSkuTd().SelectByProdutoSkuProdutoDetalhe(null, proSku_id.Value, ent_cepDest);
                if (produtoSkuDto.entregaDto != null)
                {
                    carrinhoListDto = new List<CarrinhoDto>();
                    carrinhoDto.entregaDto = produtoSkuDto.entregaDto;
                    carrinhoDto.parcelamentoDto = produtoSkuDto.parcelamentoDto;
                    carrinhoListDto.Add(carrinhoDto);
                }
            }

            CarrinhoTotaisDto carrinhoTotaisDto = new CarrinhoTd().SelectCarrinhoTotais(carrinhoListDto, ent_cep, null, null);

            if (carrinhoTotaisDto.cart_entregaPrazoTotal.HasValue && carrinhoTotaisDto.cart_entregaPrazoTotal.HasValue)
            {
                correioDto.co_prazoEntrega = carrinhoTotaisDto.cart_entregaPrazoTotal.ToString();
                correioDto.co_msgErro = null;
            }
            else
                correioDto = new CorreioTd().SelectCorreioCalcPrazo(null, "40010", null, ent_cep);

            return correioDto;
        }


        [OperationContract]
        public CarrinhoTotaisDto SelectCarrinhoTotais(string sCepDestino, string cu_chave)
        {
            _1_WebForm.App_Code.Utils.CustomPrincipal customPrincipal = _1_WebForm.App_Code.Utils.Aut.AutenticacaoDados();

            int? cli_id = null;
            if (customPrincipal != null && customPrincipal.CliId != 0)
            {
                cli_id = customPrincipal.CliId;
            }

            CarrinhoTotaisDto carrinhoTotaisDto = new CarrinhoTd().SelectCarrinhoTotais(null, sCepDestino, cu_chave, cli_id);

            System.Web.HttpContext.Current.Session.Add("carrinhoTotaisDto", carrinhoTotaisDto);

            return carrinhoTotaisDto;
        }

        [OperationContract]
        public CorreioDto SelectCorreioLocalidade(string sCepDestino)
        {
            return new CorreioTd().SelectCorreioLocalidade(sCepDestino);
        }

    }
}
