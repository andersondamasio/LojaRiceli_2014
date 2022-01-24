using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using _2_Library.Modelo;
using _2_Library.Utils;

namespace _2_Library.Dao.LojaX
{
    public class LojaTd
    {
        public LojaDto SelectLoja(string loj_dominio)
        {
            if (string.IsNullOrEmpty(loj_dominio))
                loj_dominio = Recursos.SelecionaUrlDominio();

            LojaDto lojaDto = new LojaDao().SelectLoja(loj_dominio);

            if (lojaDto == null)
                lojaDto = new LojaDto() { loj_mensagemErro = "Loja " + loj_dominio + " não configurada." };
            else
                if (lojaDto.loj_bloquear)
                    lojaDto.loj_mensagemErro = "Loja " + loj_dominio + " indisponível no momento.";

            if ((lojaDto == null || lojaDto.loj_bloquear || !string.IsNullOrEmpty(lojaDto.loj_mensagemErro)))
                if (System.Web.HttpContext.Current != null)
                {
                    string applicationPath = HttpContext.Current.Request.ApplicationPath.Replace(@"/", string.Empty);
                    if (applicationPath.EndsWith("painel"))
                        HttpContext.Current.Response.Redirect(Recursos.GetRouteUrl("PaginaInicial", null) + "Redirecionamentos/NaoConfigurado.aspx?mensagem=" + lojaDto.loj_mensagemErro);
                    else System.Web.HttpContext.Current.Response.Redirect(Recursos.GetRouteUrl("NaoConfigurado", new { mensagem = lojaDto.loj_mensagemErro }));
                }
            return lojaDto;
        }

        public int InsertLoja(LojaDto lojaDto)
        {
            using (LojaDao lojaDao = new LojaDao())
            {
                if (lojaDao.Select().Where(s => s.loj_dominio == lojaDto.loj_dominio).Count() == 0)
                {
                    LojaCon lojaCon = ToLojaCon(new LojaCon(), lojaDto);
                    lojaDao.Add(lojaCon);
                    return lojaCon.loj_id;
                }
                else return 0;
            }
        }

        private LojaCon ToLojaCon(LojaCon lojaCon, LojaDto lojaDto)
        {

            Type type = typeof(LojaDto);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                object value = pi.GetValue(lojaDto, null);
                if (value != null && value.GetType().Name == "String")
                {
                    string novoValue = value.ToString().Trim();
                    novoValue = (novoValue == string.Empty) ? null : novoValue;
                    pi.SetValue(lojaDto, novoValue);
                }
            }

            if (lojaCon.loj_nome != lojaDto.loj_nome)
                lojaCon.loj_nome = lojaDto.loj_nome;

            if (lojaCon.loj_dominio != lojaDto.loj_dominio)
                lojaCon.loj_dominio = lojaDto.loj_dominio;

            if (lojaCon.loj_email != lojaDto.loj_email)
                lojaCon.loj_email = lojaDto.loj_email;

            if (lojaCon.loj_cep != lojaDto.loj_cep)
                lojaCon.loj_cep = lojaDto.loj_cep;

            if (lojaCon.loj_subdominio != lojaDto.loj_subdominio)
                lojaCon.loj_subdominio = lojaDto.loj_subdominio;

            if (lojaCon.loj_bloquear != lojaDto.loj_bloquear)
                lojaCon.loj_bloquear = lojaDto.loj_bloquear;

            if (lojaDto.usuarioDto != null && lojaDto.usuarioDto.Count() > 0)
                foreach (var usu in lojaDto.usuarioDto)
                    lojaCon.Usuario.Add(new Usuario()
                    {
                        usu_id = usu.usu_id,
                        usu_nome = usu.usu_nome,
                        usu_senha = usu.usu_senha,
                        usu_admin = false,
                        usuPer_usuarioSelecionar = usu.usuPer_usuarioSelecionar,
                        usuPer_pedidoSelecionar = usu.usuPer_pedidoSelecionar,
                        usuPer_lojaSelecionar = usu.usuPer_lojaSelecionar,
                        usuPer_lojaInserir = usu.usuPer_lojaInserir,
                        usu_dataHora = DateTime.Now,
                        loj_id = lojaDto.loj_id
                    });

            return lojaCon;
        }

    }
}
