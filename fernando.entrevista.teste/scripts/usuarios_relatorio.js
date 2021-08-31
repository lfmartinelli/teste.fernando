
//#region Load()

function usuariosRelatorio_load() {

    sapi.url_router = "/api/usuarios";
    document.body.style.cursor = "default";
    usuarioRelatorio_grid("-");

    var btnLogin = document.getElementById("btnUsuarios_filtro_enviar");
    btnLogin.addEventListener("click", usuarioRelatorio_filtrar, false);


}

//#endregion

//#region Load Grid

function usuarioRelatorio_grid(filtro) {

    if (filtro == "") { filtro = "-"; }

    sapi.get("load_listagem_usuarios", filtro, onSuccess, onError);

    function onSuccess(grid) {
        document.getElementById("divUsuarios_relatorio").innerHTML = grid;
    }

    function onError(msg) {
        alert(msg);
    }

}

//#endregion

//#region Filtrar

function usuarioRelatorio_filtrar() {

    var filtro = smartSQLgetDataInPage("usuario", "sql_filter");

    usuarioRelatorio_grid(filtro);
}

//#endregion

//#region Ativar/Desativar usuário

function subAtivar(cod, acao) {

    var usr = {"cod":cod };

    if (acao =="desativar") {
        var resp = confirm("Tem certeza que deseja desativar esse usuário?");
        if (!resp) {
            return;
        }
        usr["ativo"] = 1;
    } else {
        var resp = confirm("Tem certeza que deseja ativar o acesso desse usuário?");
        if (!resp) {
            return;
        }
        usr["ativo"] = 0;
    }

    sapi.run("ativarUsuario", usr, retornoAtivaSucess, onError);

    function retornoAtivaSucess(msg) {

        if (msg.indexOf("!!") != -1) {
            alert(msg);
        } else {
            if (acao=='desativar') {
                document.getElementById("btnAtivo_" + cod).className = "btnInativo";
                document.getElementById("btnAtivo_" + cod).onclick = function () { subAtivar(cod, 'ativar'); }
                document.getElementById("btnAtivo_" + cod).title = "Ativar acesso do usuário";
                document.getElementById("rowteste_usuarios[" + cod + "]").className = "cssgridRelatorio_inativo_row";
            } else {
                document.getElementById("btnAtivo_" + cod).className = "btnAtivo";
                document.getElementById("btnAtivo_" + cod).onclick = function () { subAtivar(cod, 'desativar'); }
                document.getElementById("btnAtivo_" + cod).title = "Desativar acesso do usuário";
                document.getElementById("rowteste_usuarios[" + cod + "]").className = "cssgridRelatorio_row";

            }
        }
    }
}

//#endregion

function usuarioRelatorio_select(cod) {
    window.open('usuarios_cadastro.html?cod=' + cod, "_self");
}

function onError(msg) {
    alert(msg);
}