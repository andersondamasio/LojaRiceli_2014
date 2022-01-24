using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Lojax
{
    public class LojaConsulta
    {
        public static LojaCon SelecionarLojaDominioX()
        {
            return new Loja.Modelo.Lojax.LojaDao().SelecionarLojaDominio();
        }
    }
}