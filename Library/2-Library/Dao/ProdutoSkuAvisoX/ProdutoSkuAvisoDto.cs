using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Library.Dao.ProdutoSkuAvisoX
{
    public class ProdutoSkuAvisoDto
    {
        public string proSkuAvi_email { get; set; }
        public string proSkuAvi_nome { get; set; }
        public int? cli_id { get; set; }
        public int proSku_id { get; set; }
        public int loj_id { get; set; }
    }
}
