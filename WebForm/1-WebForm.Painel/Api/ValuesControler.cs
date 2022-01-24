using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using _2_Library.Modelo;

namespace Loja
{
    public class ValuesController : ApiController
    {
        // GET api/<controller>

        public IQueryable<MensagemSistema> Get()
        {
            LojaEntities lojaEntities = new LojaEntities();

            var teste = lojaEntities.MensagemSistema;

            return teste;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post(string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}