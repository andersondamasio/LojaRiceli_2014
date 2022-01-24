using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;
using EntityFramework.Extensions;
using System.Linq.Dynamic;
using EntityFramework.Caching;

namespace _2_Library.Dao.Site.EntregaX
{
    internal class EntregaDao : Repositorio<Entrega>
    {

        public List<EntregaDto> SelectEntregaGlobal(int loj_id)
        {
            List<EntregaDto> entregaDto = (from ent in Select()
                                           where ent.ent_global
                                           orderby ent.ent_valor descending
                                           orderby ent.ent_valor.HasValue
                                           select new EntregaDto
                                           {
                                               ent_descricao = ent.ent_descricao,
                                               ent_cepInicial = ent.ent_cepInicial,
                                               ent_cepFinal = ent.ent_cepFinal,
                                               ent_dataHoraInicial = ent.ent_dataHoraInicial,
                                               ent_dataHoraFinal = ent.ent_dataHoraFinal,
                                               ent_prazo = ent.ent_prazo,
                                               ent_valor = ent.ent_valor,
                                               ent_prazoAdicional = ent.ent_prazoAdicional
                                           }).ToList();//.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(300000))).ToList();

            return entregaDto;
        }
    }
}
