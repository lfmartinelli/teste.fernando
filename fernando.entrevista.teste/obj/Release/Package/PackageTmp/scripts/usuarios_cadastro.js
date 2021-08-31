var acessos = "";
var cod = "";
var usr = {cod:0};

//#region Load()

function usuariosCadastro_load() {

    var btnSalvar = document.getElementById("btnTesteForm_enviar");
    btnSalvar.addEventListener("click", subSalvaUsuario, false);

    var btnNovo = document.getElementById("btnTesteHeader_novo_usuario");
    btnNovo.addEventListener("click", subNovoUsuario, false);

    sapi.url_router = "/api/usuarios";

    var url = smartURLQuery("cod");
    if (url != "") {
        sapi.get("loadAPIUsuario", url, usuariosCadastro_loadForm, onError);
    } else {
        sapi.get("loadSexo", "-", usuariosCadastro_cmbSexo, onError);
    }
}

//#endregion

//#region Carrega combo sexo

function usuariosCadastro_cmbSexo(data) {
    document.getElementById("cmbsexo").innerHTML = data;
}

//#endregion

//#region Carregar no form os dados de usuário

function usuariosCadastro_loadForm(data) {

    usr = data;
    document.getElementById("cmbsexo").innerHTML = usr.frontend
    smartSQLsetDataInPage("usuario", data);
    document.getElementById("txtnascimento").value = smartString(document.getElementById("txtnascimento").value, "dd/MM/yyyy");

    subCarregaItensModulos(usr.acessos);
}

//#endregion

//#region Insere módulo na div de autorizações de acesso

function subInsereItem(btn, modulo) {

    var objItem = document.getElementById(btn);

    if (objItem.className != "btnTesteAcessos_item_selected") {

        objItem.className = "btnTesteAcessos_item_selected";
        document.getElementById("divTesteAcessos_selecionados").appendChild(objItem);

        var modulo = objItem.value;
        acessos += "[" + modulo.toLowerCase() + "]";

    } else {

        objItem.className = "btnTesteAcessos_item";
        document.getElementById("divTesteAcessos_usuario").appendChild(objItem);

        var modulo = objItem.value;
        acessos = acessos.replace("[" + modulo.toLowerCase() + "]", "");

    }

    document.getElementById("tempAcessos").value = acessos;
}

//#endregion

//#region Carrega os módulos autorizados para acesso na div de selecionados

function subCarregaItensModulos(_acessos) {

    var modulos = _acessos.replace(/\]/g, "").substring(1).split('[');

    for (var i = 0; i < modulos.length; i++) {

        objItem = document.getElementById("btnAcessosItem_" + modulos[i].toLowerCase())
        objItem.className = "btnTesteAcessos_item_selected";
        document.getElementById("divTesteAcessos_selecionados").appendChild(objItem);
    }

    acessos = _acessos;
}

//#endregion

//#region Salva usuário

function subSalvaUsuario() {

    usr = smartSQLgetDataInPage("usuario", "object", usr);

    if (usr["error"]) {
        alert(`O campo '${usr["error"]}' deve ser informado!`)
        return;
    }

    if (usr.cod == 0) {
        usr["ativo"] = "f";
    }

    sapi.run("saveUsuario", usr, retornoUsuarioSalvo, onError);
}

function retornoUsuarioSalvo(retorno) {

    if (retorno.indexOf("!!") > -1) {
        alert(retorno);
    } else {
        if (usr.cod == 0) {
            usr.cod = retorno;
            alert("Usuário cadastro com sucesso!");
        } else {
            alert("Usuário atualizado com sucesso!");
        }
        return;
    }
}

//#endregion

//#region Novo usuário

function subNovoUsuario() {
    smartSQLclearDataInPage("usuario");

    document.getElementById("divTesteAcessos_usuario").innerHTML =
        "<input id='btnAcessosItem_produtos' class='btnTesteAcessos_item' type='button' value='PRODUTOS' onclick='subInsereItem(this.id, 'produtos')'/>" +
        "<input id='btnAcessosItem_clientes' class='btnTesteAcessos_item' type='button' value='CLIENTES' onclick='subInsereItem(this.id, 'clientes')'/>" +
        "<input id='btnAcessosItem_usuarios' class='btnTesteAcessos_item' type='button' value='USUARIOS' onclick='subInsereItem(this.id, 'usuairos')'/>";

    document.getElementById("divTesteAcessos_selecionados").innerHTML = "";
    acessos = "";
    usr.cod = 0;
}

//#endregion

function cadUsuarios_erro(retrono) {
    alert(retrono);
}

function onError(msg) {
    alert(msg);
}