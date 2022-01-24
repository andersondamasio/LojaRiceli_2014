using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Cupom
{
    public class CupomDao
    {
        private Int32 loj_id = Loja.Modelo.Lojax.LojaConsulta.SelecionarLojaDominio().loj_id;
        public Retorno SelecionarCupom(string cup_chave, int? cli_id)
        {
            Retorno retorno = new Retorno();
            LojaEntities lojaEntities = new LojaEntities();

            _2_Library.Modelo.Cupom cupom = (from cup in lojaEntities.Cupom
                                where
                                 cup.cup_chave == cup_chave && !cup.cup_excluido.HasValue &&
                                 cup.loj_id == loj_id
                                select cup).FirstOrDefault();


            //Este cupom não é valido.
            if (cupom == null)
            {
                retorno = Static.MensagemSistema(20);
                return retorno;
            }

            //Cupom expirado. Validade já excedida.
            if (cupom.cup_validade < DateTime.Now.Date)
            {
                retorno = Static.MensagemSistema(19);
                return retorno;
            }


            //Cupom expirado. Quantidade já excedida.
            if (cupom.Pedido.Count >= cupom.cup_quantidade)
            {
                retorno = Static.MensagemSistema(18);
                return retorno;
            }

            //Você deve se logar no site para usar este cupom.
            if (cupom.cli_id.HasValue && !cli_id.HasValue)
            {
                retorno = Static.MensagemSistema(21);
                return retorno;
            }

            //Este cupom não é válido para o seu usuário.
            if ((cupom.cli_id.HasValue && cli_id.HasValue) && cupom.cli_id != cli_id)
            {
                retorno = Static.MensagemSistema(22);
                return retorno;
            }

 
            retorno.objeto = cupom;

            return retorno;
        }


        public void ExcluirCupom(int cup_id)
        {
            Retorno retorno = new Retorno();
            LojaEntities lojaEntities = new LojaEntities();

            _2_Library.Modelo.Cupom cupom = (from cup in lojaEntities.Cupom
                                where
                                 cup.cup_id == cup_id &&
                                 cup.loj_id == loj_id
                                select cup).FirstOrDefault();

            cupom.cup_excluido = cup_id;

            lojaEntities.SaveChanges();
        }
    }
}