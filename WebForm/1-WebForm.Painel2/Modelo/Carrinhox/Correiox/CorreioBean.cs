using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Modelo.Correiox
{
    public class CorreioBean
    {
        public string corr_cidade { get; set; }
        public string corr_estado { get; set; }
        public string corr_endereco { get; set; }
        public string corr_bairro { get; set; }
        public string corr_complemento { get; set; }

    }

    public class Medidas
    {
        public decimal totalComprimento { get; set; }
        public decimal totalLargura { get; set; }
        public decimal totalAltura { get; set; }
        public decimal totalPeso { get; set; }
        public decimal totalDiametro { get; set; }
    }
}