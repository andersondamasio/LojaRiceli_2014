using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.CorreioX
{
    internal class FreteDao : RepositorioCorr<FRETE>
    {

        public FreteDao() { }
        public FreteDao(CorreiosEntities correiosEntities) : base(correiosEntities) { }


        public List<FreteDto> SelectFrete(string fre_servico, string fre_cepOrigem, int fre_cepDestino, decimal fre_peso)
        {
            List<FreteDto> frete = null;

            if (string.IsNullOrEmpty(fre_servico))
            {

                frete = (from fre in Select()
                         where fre.FRE_CEPORIGEM == fre_cepOrigem &&
                         fre.FRE_CEPDESTINO_INI <= fre_cepDestino &&
                         fre.FRE_CEPDESTINO_FIM >= fre_cepDestino &&
                        (fre.FRE_PESO >= 0 &&
                         fre.FRE_PESO >= fre_peso)
                         select new FreteDto
                         {
                             fre_id = fre.FRE_ID,
                             fre_nome = fre.FRE_NOME,
                             fre_regiao = fre.FRE_REGIAO,
                             fre_prazo = fre.FRE_PRAZO,
                             fre_valor = fre.FRE_VALOR,
                             fre_peso = fre.FRE_PESO,
                             fre_dataHoraAtualizacao = fre.FRE_DATAHORAATUALIZACAO
                         }).OrderBy(s => s.fre_peso).Take(1).ToList();
            }
            else
            {

                frete = (from fre in Select()
                         where fre.FRE_SERVICO == fre_servico && 
                         (fre.FRE_CEPORIGEM == fre_cepOrigem) &&
                         (fre.FRE_CEPDESTINO_INI <= fre_cepDestino && 
                          fre.FRE_CEPDESTINO_FIM >= fre_cepDestino) &&
                         (fre.FRE_PESO >= 0 &&
                          fre.FRE_PESO >= fre_peso)
                         select new FreteDto
                         {
                             fre_id = fre.FRE_ID,
                             fre_nome = fre.FRE_NOME,
                             fre_regiao = fre.FRE_REGIAO,
                             fre_prazo = fre.FRE_PRAZO,
                             fre_valor = fre.FRE_VALOR,
                             fre_peso = fre.FRE_PESO,
                             fre_dataHoraAtualizacao = fre.FRE_DATAHORAATUALIZACAO
                         }).OrderBy(s => s.fre_peso).Take(1).ToList();

            }

            return frete;
        }
    }
}
