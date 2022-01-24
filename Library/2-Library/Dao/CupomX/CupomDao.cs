using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Config;
using _2_Library.Modelo;

namespace _2_Library.Dao.CupomX
{
    internal class CupomDao : Repositorio<Cupom>
    {
        public CupomDto SelectCupom(int loj_id, string cup_chave)
        {
            CupomDto cupomDto = (from cup in Select()
                                 where 
                                 cup.cup_chave == cup_chave && 
                                 cup.loj_id == loj_id &&
                                 !cup.cup_excluido.HasValue
                                 select new CupomDto
                                 {
                                     cup_id = cup.cup_id,
                                     cup_chave = cup.cup_chave,
                                     cup_valor = cup.cup_valor,
                                     cup_validade = cup.cup_validade,
                                     cup_quantidade = cup.cup_quantidade,
                                     cup_numPedidos = cup.Pedido.Count(),
                                     cli_id = cup.cli_id
                                 }).FirstOrDefault();
            return cupomDto;
        }
    }
}
