using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.Site.CupomX
{
    public class CupomTd
    {
        public CupomDto SelectCupom(string loj_dominio, string cup_chave, int? cli_id)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            CupomDto cupomDto = null;

            using(CupomDao cupomDao = new CupomDao()){

                cupomDto = cupomDao.SelectCupom(loj_id,cup_chave);
            }

            if (cupomDto == null)
            {
                cupomDto = new CupomDto();
                cupomDto.cup_msgErro = "Este cupom não é válido.";
                return cupomDto;
            }
            else
                cupomDto.cup_msgErro = "0";
           
            if (cupomDto.cup_validade < DateTime.Now.Date)
            {
                cupomDto.cup_msgErro = "Cupom expirado. Validade já excedida.";
                return cupomDto;
            }

            if (cupomDto.cup_numPedidos >= cupomDto.cup_quantidade)
            {
                cupomDto.cup_msgErro = "Cupom expirado. Quantidade já excedida.";
                return cupomDto;
            }
           
            if (cupomDto.cli_id.HasValue && !cli_id.HasValue)
            {
                cupomDto.cup_msgErro = "Você deve se logar no site para usar este cupom.";
                return cupomDto;
            }

            if ((cupomDto.cli_id.HasValue && cli_id.HasValue) && cupomDto.cli_id != cli_id)
            {
                cupomDto.cup_msgErro = "Este cupom não é válido para o seu cadastro.";
                return cupomDto;
            }
           return cupomDto;
        }
    }
}
