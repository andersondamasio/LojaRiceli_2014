using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loja.Modelo.Lojax;
using _2_Library.Modelo;

namespace Loja.Modelo.Entregax
{
    public class EntregaDao
    {

        private Int32 loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;

        public Boolean SelecionarEntregaExiste(int cepInicial, int cepFinal, decimal? prazo, decimal? valor, int excetoEnt_id)
        {
            LojaEntities lojaEntities = new LojaEntities();

            Boolean existe = (from ent in lojaEntities.Entrega
                              where
                              ent.ent_cepInicial == cepInicial &&
                              ent.ent_cepFinal == cepFinal &&
                              (valor.HasValue ? ent.ent_valor == valor.Value : !ent.ent_valor.HasValue) &&
                              (prazo.HasValue ? ent.ent_prazo == prazo.Value : !ent.ent_prazo.HasValue) &&
                              ent.loj_id == loj_id &&
                              ent.ent_id != excetoEnt_id
                              select ent).Count() > 0;

            return existe;
        }

        public _2_Library.Modelo.Entrega SelecionarEntrega(int cep_inicial)
        {
            LojaEntities lojaEntities = new LojaEntities();

            _2_Library.Modelo.Entrega entrega = (from ent in lojaEntities.Entrega
                               where
                               (cep_inicial >= ent.ent_cepInicial &&
                               cep_inicial <= ent.ent_cepFinal) &&
                               ent.loj_id == loj_id
                               select ent).FirstOrDefault();

            return entrega;
        }

        public List<Entrega> SelecionarEntrega()
        {
            LojaEntities lojaEntities = new LojaEntities();

            List<Entrega> entrega = (from ent in lojaEntities.Entrega
                                     where ent.loj_id == loj_id
                                     select ent).ToList();

            return entrega;
        }

    }
}