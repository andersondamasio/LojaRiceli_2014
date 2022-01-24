using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Dao.LojaX;

namespace _2_Library.Dao.Painel.CarrinhoX
{
    public class CarrinhoTd
    {
        public List<CarrinhoDto> SelectCarrinho(string loj_dominio, int startRowIndex, int maximumRows, string orderBy)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            List<CarrinhoDto> carrinhoDto = null;

            using (CarrinhoDao rep = new CarrinhoDao())
            {
                carrinhoDto = rep.SelectCarrinho(loj_id, startRowIndex, maximumRows, orderBy);
            }
            return carrinhoDto;
        }

        public int SelectCarrinhoCount(string loj_dominio, int startRowIndex, int maximumRows, string orderBy)
        {
            int loj_id = new LojaTd().SelectLoja(loj_dominio).loj_id;
            int count = 0;

             using (CarrinhoDao rep = new CarrinhoDao())
            {
                count = rep.SelectCarrinhoCount(loj_id, startRowIndex, maximumRows, orderBy);
            }

             return count;
        }

        /// <summary>
        /// Remove todos os carrinhos cujo a data é maior que o especificado
        /// </summary>
        /// <param name="car_dataHora"></param>
        public void RemoveCarrinhoDataMaior(DateTime car_dataHora) {

            car_dataHora = car_dataHora.AddDays(1);

            using (CarrinhoDao rep = new CarrinhoDao())
            {
               rep.Remove(rep.Select(s => s.car_dataHora > car_dataHora));
            }
        }

    }
}
