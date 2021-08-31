var login = {
    nome:"",
    email: "",
    senha: "",
    cod: 0,
    acessos: "",
    ultimo_acesso: new Date(),
    message: "",
    loged: false,
    verify: function () { return (login_refreshLogin()); },
    reset: function () { return (login_resetLogin()); }
};

var usuario = {
    data: [],
    message: "",
    param: {
        set: {},
        filtro: "",
        orderby: "",
        grid: false,
        push: function (field, value) { return usuario_pushUsuario(field, value); }
    },
    load: function (usuarioSuccess, usuarioError) {
        return (sapi.run("loadUsuario", params, usuarioSuccess, usuarioError));
    },
    save: function (usuarioSuccess, usuarioError) {
        return usuario_salvaUsuario(usuarioSuccess, usuarioError);
    },
    get: function (index, col_name) { return usuario_getUsuario(index, col_name); }
};

//#region Renova login

function login_refreshLogin() {
    sapi.get("getLogin", "", onSuccess, onError);

    function onSuccess(retorno) {
        login = retorno;
    }

    function onError(retorno) {
        login_resetLogin();
    }
}

//#endregion

//#region Reseta login

function login_resetLogin() {
    login.email = "";
    login.senha = "";
    login.cod = 0;
    login.acessos = "";
    login.ultimo_acesso = new Date();
    login.message = "";
    login.loged = false
}

//#endregion

//#region Load usuario

function login_loadUsuario() {

    var retorno = sapi.run("loadUsuario", params, usuarioSuccess, usuarioError);
    usuario.data = retorno["data"];
    usuario.message = retorno["message"];

}

//#endregion

//#region Salva usuario

function usuario_salvaUsuario(retornoUsuarioSalvoSuccess, retornoUsuarioSalvoErro) {

    sapi.run("saveUsuario", usuario.param.set, retornoUsuarioSalvoSuccess, retornoUsuarioSalvoErro);
}

//#endregion

//#region Get Value

function usuario_getUsuario(index, col_name) {

    if (index > data.Count) {
        return ("!!Não há registro na posição " + index + "!");
    }

    var value = smartDataStr.get(usuario.data[index], col_name);
    return (value);

}

//#endregion

//#region Push registro

/**
 * Insere dado na propriedade set do param para ser utilizado no salvamento ou atualização de um usuário
 * @param {string} field: Nome do campo
 * @param {string} value: Valor do registro
 */
function usuario_pushUsuario(field, value) {

    if (usuario.param.set == "") {
        usuario.param.set = field + "=" + value;
    } else {
        var dados = "*" + usuario.param.set + "*";
        if (dados.indexOf("*" + field + "=") == -1) {
            usuario.param.set += "*" + field + "=" + value;
        } else {
            var registro = new RegExp("\\*" + field + "=(.*?)\\*")
            dados.replace(registro, `*${field}=${value}*`);
            usuario.param.set = dados;
        }
    }
}

//#endregion