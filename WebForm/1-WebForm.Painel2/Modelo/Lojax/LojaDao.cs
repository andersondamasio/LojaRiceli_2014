using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2_Library.Modelo;

namespace Loja.Modelo.Lojax
{
    public class LojaDao
    {
        public int SelecionarLojaPainel()
        {
            var loj_id = HttpContext.Current.Session["loj_id"];
            if (loj_id == null)
                HttpContext.Current.Response.Redirect("Login.aspx");

            return Convert.ToInt32(loj_id);
        }



        public int SelecionarLojaSite()
        {
            var loj_id = HttpContext.Current.Session["loj_id"];
            if (loj_id == null)
                HttpContext.Current.Response.Redirect("Login.aspx");

            return Convert.ToInt32(loj_id);
        }

        public LojaCon SelecionarLojaDominio()
        {
            string loj_dominio = HttpContext.Current.Request.Url.Host;
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            if (cache.Get("Loja" + loj_dominio) == null)
            {
                LojaCon loja = new LojaCon();
                using (LojaEntities lojaEntities = new LojaEntities())
                {
                    loja = (from loj in lojaEntities.LojaCon
                            where loj.loj_dominio == loj_dominio
                            select loj).FirstOrDefault();
                }
                if (loja == null)
                {
                    HttpContext.Current.Response.Redirect("/Redirecionamento/LojaNaoConfigurada");
                    return null;
                }
                else
                    if (loja.loj_bloquear)
                    {
                        HttpContext.Current.Response.Redirect("/Redirecionamento/LojaNaoConfigurada?status=bloqueada");
                        return null;
                    }
                    else
                        cache.Insert("Loja" + loj_dominio, loja);
            }
            
                return (LojaCon)cache.Get("Loja" + loj_dominio);
        }
    }
}