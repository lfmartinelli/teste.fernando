using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using goByte.smartSharp;
using goByte.teste.Models;
using System.Web.Script.Serialization;

namespace fernando.entrevista.teste.Controllers
{
    public class usuariosController : ApiController
    {
        #region Load Listagem Usuários

        [HttpGet]
        public string load_listagem_usuarios(string filtro)
        {
            usuario usr = new usuario();

            string grid = usr.loadInGrid(filtro, "nome");

            return grid;
        }

        #endregion

        #region Load Sexo

        [HttpGet]
        public string loadSexo(string filtro)
        {
            return database.loadSexoUsuario(filtro);
        }

        #endregion

        #region Load Usuário

        [HttpGet]
        public Dictionary<string, object> loadAPIUsuario(string filtro)
        {
            usuario usr = new usuario();

            usr.loadUsuario($"teste_usuarios.cod={filtro}");

            database.exportData.Add("frontend", loadSexo(usr.sexo.ToString()));

            return database.exportData;
        }

        #endregion

        #region Save Usuário

        [HttpPost]
        public string saveUsuario([FromBody] usuario dados_usuario)
        {
            #region Confere Login

        //    if (!login.acessos.Contains("[usuarios]"))
        //    {
        //        //HttpContext.Current.Response.Redirect("index.html");
        //        return "!!Usuário não autorizado para esta ação!";
        //    }

           #endregion
            
            //usuario usr = new usuario();

            //usr = dados_usuario;

            if(dados_usuario.cod ==0)
            { return dados_usuario.save(""); }
            else { return dados_usuario.save($"cod={dados_usuario.cod}"); }
        }
        #endregion

        #region Ativar/Desativar usuário

        [HttpPost]
        public string ativarUsuario([FromBody] usuario dados_usuario)
        {
            return dados_usuario.ativar();
        }

            #endregion
    }
}
