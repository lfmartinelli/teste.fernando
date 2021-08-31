using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using goByte.smartSharp;
using System.Data;
using goByte.teste.Models;

namespace goByte.teste.Models
{
	public class usuario
	{
        #region Propriedades
        public int cod { get; set; }	
		public string email { get; set; }
		public string senha { get; set;  }
		public string acessos { get; set; }
		public string nome { get; set; }
        public int sexo { get; set; }
        public DateTime data_nascimento { get; set; }
		public int ativo { get; set; }
		public string message { get; set; }

        #endregion

        #region Inicializa usuario
        /// <summary>
        /// Inicializa usuário resetando as propriedades
        /// </summary>
        public usuario()
		{
			cod = 0;
			email = string.Empty;
			senha = string.Empty;
			acessos = string.Empty;
			nome = string.Empty;
			data_nascimento = DateTime.Now;
            sexo = 0;
			ativo = 0;
			message = string.Empty;
		}

        #endregion

        #region load() - Carrega usuarios
        /// <summary>
        /// Carrega usuários no banco de dados
        /// </summary>
        /// <param name="filtro">Critérios SQL para busca de registros. A cláusula WHERE é inserida automaticamente pelo metodo</param>
        /// <param name="orderby">Campo referente a ordem alfabética da cunsulta. Se informado "", a ordem será determinada pela consulta.</param>
        /// <returns>Retorna True ou False se houve resultado de uma consulta de usuários baseada no "filtro".
        /// A data table com os registros é armazenada na propriedade "table". 
        private bool _load(string campos, string filtro, string orderby)
        {
            if (filtro != "")
            {
                if (filtro == "all" || filtro =="-")
                { filtro = ""; }
            }

            database.where = filtro;
            if (!database.select_usuarios(campos))
            {
                message = database.message;
                return false;
            }

            //= database.table;

            return true;
        }

        #endregion

        #region loadUsuario()
        public bool loadUsuario(string filtro)
        {
            string campos = "*";

            if (!_load(campos,filtro, ""))
            {
                message = database.message;
                return false;
            }

            if (database.exportData != null)
            {
                database.exportData.Clear();
            }

            foreach (var prop in this.GetType().GetProperties())
            {
                if (database.table.Columns.Contains(prop.Name))
                {
                    string dataType = prop.PropertyType.Name.ToLower(); //database.table.Columns[prop.Name].DataType.ToString().ToLower().Replace("system.", "");

                    if (dataType == "string")
                    { 
                        prop.SetValue(this, database.table.Rows[0][prop.Name].ToString());
                        database.exportData.Add(prop.Name, database.table.Rows[0][prop.Name].ToString());
                    }
                    else if (dataType.Contains("int")) 
                    { 
                        prop.SetValue(this, int.Parse(database.table.Rows[0][prop.Name].ToString()));
                        database.exportData.Add(prop.Name, int.Parse(database.table.Rows[0][prop.Name].ToString()));
                    }
                    else if (dataType == "datetime") 
                    { 
                        prop.SetValue(this, DateTime.Parse(database.table.Rows[0][prop.Name].ToString()));
                        database.exportData.Add(prop.Name, DateTime.Parse(database.table.Rows[0][prop.Name].ToString()));
                    }
                    else if (dataType.Contains("float")) 
                    { 
                        prop.SetValue(this, float.Parse(database.table.Rows[0][prop.Name].ToString()));
                        database.exportData.Add(prop.Name, float.Parse(database.table.Rows[0][prop.Name].ToString()));
                    }
                    else if (dataType == "boolean")
                    {
                        if (database.table.Rows[0][prop.Name].ToString() == "1")
                        { 
                            prop.SetValue(this, true);
                            database.exportData.Add(prop.Name, true);
                        }
                        else { database.exportData.Add(prop.Name, false); }
                    }
                }
            }

            #region Temp
            //cod = int.Parse(database.table.Rows[0]["cod"].ToString());
            //database.exportData.Add("cod", cod);

            //email = database.table.Rows[0]["email"].ToString();
            //database.exportData.Add("email", email);

            //acessos = database.table.Rows[0]["acessos"].ToString();
            //database.exportData.Add("acessos", acessos);

            //nome = database.table.Rows[0]["nome"].ToString();
            //database.exportData.Add("nome", nome);

            //data_nascimento = DateTime.Parse(database.table.Rows[0]["data_nascimento"].ToString());
            //database.exportData.Add("data_nascimento", data_nascimento);

            //sexo = int.Parse(database.table.Rows[0]["sexo"].ToString());
            //database.exportData.Add("sexo", sexo);

            //if(database.table.Rows[0]["ativo"].ToString() =="0")
            //{ ativo = false; }
            //else { ativo = true; }

            //database.exportData.Add("ativo", sexo);
            #endregion

            database.exportData.Add("message", database.message);

            return true;
        }

        #endregion

        #region loadInGrid() - Carrega dados em um grid de HTML

        public string loadInGrid(string filtro, string orderby)
        {
            string grid_usuarios = "";

            if(filtro =="-"){ filtro = ""; }

            string campos = "teste_usuarios.cod, nome, to_char(data_nascimento, 'DD/MM/YYYY') as data_nascimento , email, sexo.descricao as sexo, ativo";

            if (_load(campos, filtro, orderby))
            {
                string colunaZero = "<input class=\"btnSelect\" type=\"button\" title=\"Abrir cadastro do usuário\" onclick=\"usuarioRelatorio_select('[cod]')\"/>" + 
                                    "((ativo=1)){<input id='btnAtivo_[cod]' class=\"btnAtivo\" title=\"Desativar acesso do usuário\" type=\"button\" onclick=\"subAtivar('[cod]', 'desativar')\"/>}" +
                                    "((ativo=0)){<input id='btnAtivo_[cod]' class=\"btnInativo\" title=\"Ativar acesso do usuário\" type=\"button\" onclick=\"subAtivar('[cod]', 'ativar')\"/>}";
                string cssCase = "((ativo=1)){cssgridRelatorio_row};((ativo=0)){cssgridRelatorio_inativo_row}";

                 grid_usuarios = smartHTML.createTableHTML(database.table, "teste_usuarios", colunaZero, cssCase, "[cod][ativo]", "cod");
            }

            return grid_usuarios;
        }

        #endregion

        #region ativarUsuario()

        public string ativar()
        {
            Dictionary<string, object> dicAtivo = new Dictionary<string, object>();
            if (ativo ==0)
            {
                ativo = 1;
                dicAtivo["ativo"] =1;
            }
            else
            {
                ativo = 0;
                dicAtivo["ativo"] =0;
            }

            database.where = $"cod={cod}";
            return database.update_usuarios(dicAtivo);

        }

        #endregion

        #region Insert e Update usuarios
        /// <summary>
        /// <para>Salva ou atualiza usuário no banco.</para>
        /// <para>Se na DataStr "strUser" for encontrado um valor para "cod", atualiza usuário referente a esse código</para>
        /// <para>Se não foi atribuído um cod de usuario, então insere como novo registro na tabela prover_usuario.</para>
        /// </summary>
        /// <param name="dados">Dictionary com os dados do usuário</param>
        /// <returns><para>String. No caso de atualização bem sucedida retorna "". No caso de insert bem sucedido, retorna o cod do novo usuário</para>
        /// <para>Ser ocorrer algum problema com a solicitação, retorna uma mensagem.</para>
        /// </returns>
        public string save(string filtro)
        {
            Dictionary<string, object> dados = smartObject.toDictionary(this);

			if (filtro =="")
            { return database.insert_usuarios(dados); }
			else
            {
				database.where = filtro;
				return database.update_usuarios(dados);
			}
        }

        #endregion
    }
}