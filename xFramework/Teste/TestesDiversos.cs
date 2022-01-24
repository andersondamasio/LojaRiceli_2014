using System;
using System.Linq;
using _2_Library.Dao.Site.CorreioX;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Teste
{
    [TestClass]
    public class TestesDiversos
    {
       /* [TestMethod]
        public void TestMethod1()
        {
            var lojaEntities =  new _2_Library.Dao.Produto_GrupoX.Produto_GrupoDao().Select().Select(s=>s.pro_id);
            foreach (var cliente in lojaEntities)
            {
                System.Diagnostics.Debug.WriteLine("cliente: " + cliente);
            }
        }
        */
        [TestMethod]
        public void TestSelect()
        {
            var teste = new CorreioTd().SelectCorreioCalcPrazo("localhost", "40010", null, "85806320");
        }
    }
}
