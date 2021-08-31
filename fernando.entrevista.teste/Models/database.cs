
//Atenção: A plataforma de banco de dados utilizada para desenvolvimento é o PostgreSQL vs9.
//Como este não é um projeto fechado e definitivo, pode haver a necessidade ou opção de fazer a migração de dados para outra plataforma.
//Sendo assim separei esta camada adicional afim de facilitar a portabilidade (SQL Server, MySQL etc.).
//Por isso, toda manipulação de dados, inclusive a string de conexão está nessa camada. A adequação de instruções SQL feitas aqui, não afeta
//as outras camadas.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using goByte.smartSharp;
using System.Data;
using goByte.teste.Models;
using System.Text.RegularExpressions;

namespace goByte.teste.Models
{
	public static class database
	{
        #region Propriedades
        /// <summary>
        /// Constatnte com a string de conexão ODBC
        /// </summary>
        public const string conection_string = "DRIVER={PostGreSQL Ansi}; SERVER=pgsql.gobyte.com.br;UID=gobyte2;PWD=golab@123;DATABASE=gobyte2;OPTION=3";
        /// <summary>
        /// Campos com valores para UPDATE ou só valores para INSERT.
        /// </summary>
        public static string set { get; set; }
        /// <summary>
        /// Filtro WHERE para consulta
        /// </summary>
		public static string where { get; set; }
        /// <summary>
        /// Para cláusula ORDER BY
        /// </summary>
		public static string orderby { get; set; }
        /// <summary>
        /// Retorno de erro
        /// </summary>
		public static string error { get; set; }
        /// <summary>
        /// Mesnagens sobre retorno de erro e ocorrências
        /// </summary>
		public static string message { get; set; }
        /// <summary>
        /// Tabela com o resultado da consulta
        /// </summary>
		public static DataTable table { get; set; }
        /// <summary>
        /// Dictionary com o resultado da consulta em formato para exportação para o JS.
        /// </summary>
        public static Dictionary<string, object> exportData { get; set; } 

        #endregion

        #region Usuários ===============================================================================================================

        #region Load sexo
        public static string loadSexoUsuario(string sexo)
        {
            DataSet dbSexo = smartDB.openQuery("SELECT * FROM teste_sexo", "sexo", conection_string);

            if(dbSexo.Tables[0].Columns.Contains("erro"))
            {
                return dbSexo.Tables[0].Rows[0]["erro"].ToString();
            }

            string select = "<option value=''>Sexo</option>";

            for (int i = 0; i < dbSexo.Tables[0].Rows.Count; ++i)
            {
                if (dbSexo.Tables[0].Rows[i]["cod"].ToString() == sexo)
                { select += $"<option value={ dbSexo.Tables[0].Rows[i]["cod"]} selected>{dbSexo.Tables[0].Rows[i]["descricao"]}</option>"; }
                else { select += $"<option value={ dbSexo.Tables[0].Rows[i]["cod"]}>{dbSexo.Tables[0].Rows[i]["descricao"]}</option>"; }
            }

            return select;
        }

        #endregion

        #region select_usuarios

        /// <summary>
        /// Carrega uma consulta da tabela usuarios baseada na query da propriedade 'where' com sort baseada na propriedade orderby
        /// </summary>
        /// <returns>Retorna true se for uma consulta válida e carrega a consulta na propriedade "table". 
        /// Retorna false se encontar algum problema ou se resultar em uma consulta vazia
        /// Obs: No acaso de retornar false, informa o ocorrido na propriedade menssagem e error.</returns>
        public static bool select_usuarios(string campos)
        {
			if(where !="")
            {
				where = " WHERE " + where.Replace("[quote]", "");
            }

            //(SELECT COALESCE(cod,0) as cod FROM clientes ORDER BY cod DESC LIMIT 1) +1

            DataSet dbUsuarios = smartSharp.smartDB.openQuery($@"SELECT {campos}
                                                                FROM teste_usuarios
                                                                INNER JOIN teste_sexo as sexo ON teste_usuarios.sexo = sexo.cod
                                                                {where}","usuarios", conection_string);

            Dictionary<string, object> data = new Dictionary<string, object>{ {"message", string.Empty } };

            exportData = data;

            if (dbUsuarios.Tables.Count ==0)
            {
				error = "503";
				message = "!!Ocorreu um problema na conexão com o servidor! Por favor verifique sua internet e tente novamente.";
                exportData["message"] = message;
				return false;
            }

			if(dbUsuarios.Tables[0].Rows.Count ==0)
            {
				error = "";
				message = "!!Usuário não encontrado ou senha inválida!";
                exportData["message"] = message;
                return false;
            }

			table = dbUsuarios.Tables[0];

            dbUsuarios.Dispose();

			return true;
        }

        #endregion

        #region update_usuarios
        /// <summary>
        /// <para>Atualiza usuário na tabela prover_usuarios.</para>
        /// <para>Campos = valores devem se informados na propriedade "set" separados por vírgula ou em formato DataStr ou seja separados por "*".</para>
        /// O filtro deve ser informado na propriedade "where". Não necessita incluir o comando "WHERE".
        /// "orderby" não é utilizado.
        /// </summary>
        /// <returns>Retorna "" se a atualização for bem sucedida e uma mensagem se ocorrer algum problema.</returns>
        public static string update_usuarios(Dictionary<string, object> dados)
        {
 
            #region Insere o WHERE no filtro
            if (where != "")
            {
                where = where;
            }

            #endregion

            DataSet dbUsuarios = smartDB.openQuery($"SELECT cod FROM teste_usuarios {where}", "", conection_string);

            if (dbUsuarios.Tables.Count == 0)
            {
                return ("!!Ocorreu algum problema na conexão com o servidor! Por favor verifique sua conexão de internet e tente novamente");
            }

            if (dbUsuarios.Tables[0].Rows.Count == 0)
            {
                return ("!!Não é possível atualizar o cadastro, pois este usuário não foi encontrado em nosso banco!");
            }

            #region Atualiza no banco de dados e retorna uma SELECT com o registro com mesmo filtro para conferencia
            
            DataTable tabUsuario = smartDB.update("teste_usuarios", dados, $"{where}", true, conection_string);

            if(tabUsuario.Rows[0]["error"].ToString() !="")
            {
                return tabUsuario.Rows[0]["msg"].ToString();
            }

            if(tabUsuario.Rows.Count ==0)
            {
                return "!!Usuário não encontrado! Por favor verifique as informações e tente novamente";
            }

            dbUsuarios.Dispose();

            #endregion

            return ("");
        }

        #endregion

        #region insert_usuarios

        /// <summary>
        /// Salva um novo usuário no banco de dados na tabela "prover_usuarios".
        /// Os valores devem ser informados na propriedade "set" separados por "," e só os valores.
        /// Não será utilizado cláusula WHERE.
        /// </summary>
        /// <returns>Retorna uma string com o código do novo usuário. Se algo estiver errado, será retornado uma mensagem.</returns>
        public static string insert_usuarios(Dictionary<string, object> dados)
        {
            if (where != "")
            {
                where = " WHERE " + where;
            }

            DataSet dbUsuarios = smartDB.openQuery($"SELECT email FROM teste_usuarios WHERE email='{dados["email"]}'", "", conection_string);

           if(dbUsuarios.Tables.Count ==0)
            {
                return ("!!Ocorreu algum problema na conexão com o servidor! Por favor verifique sua conexão de internet e tente novamente");
            }

           if(dbUsuarios.Tables[0].Rows.Count > 0)
            {
                return ("!!Este usuário já está cadastrado!");
            }

            DataTable tabUsuarios = smartDB.insert("teste_usuarios", dados, "[cod]['email']", "cod", "", conection_string);

            if (tabUsuarios.Rows[0]["error"].ToString() != "")
            {
                return tabUsuarios.Rows[0]["msg"].ToString();
            }

            string cod = tabUsuarios.Rows[0]["cod"].ToString();

            dbUsuarios.Dispose();

            return (cod);
        }

        #endregion

        #endregion
    }
}