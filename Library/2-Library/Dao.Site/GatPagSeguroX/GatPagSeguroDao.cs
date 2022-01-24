using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.Site.GatPagSeguroX
{
    internal class GatPagSeguroDao : Repositorio<GatPagSeguro>
    {
        public GatPagSeguroDto SelectGatPagSeguro(int loj_id)
        {
            GatPagSeguroDto gatPagSeguroDto = (from gat in Select()
                                               where gat.loj_id == loj_id
                                               select new GatPagSeguroDto
                                               {
                                                   gatps_email = gat.gatps_email,
                                                   gatps_token = gat.gatps_token,
                                                   gatps_parcelasSemJuros = gat.gatps_parcelasSemJuros,
                                                   gatps_percentualJuro = gat.gatps_percentualJuro,
                                                   loj_id = gat.loj_id
                                               }).FirstOrDefault();

            return gatPagSeguroDto;
        }

    }
}
