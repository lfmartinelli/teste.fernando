var teste = {};

//#region Load

function index_load() {

    sapi.url_router = "/api/login";

    document.body.style.cursor = "default";

    var btnLogin = document.getElementById("btnProverLogin_entrar");
    btnLogin.addEventListener("click", subLogin_send, false);

    var btnLogin = document.getElementById("btnProverLogin_esqueci");
    btnLogin.addEventListener("click", function () { subLogin_tempSenha(false) }, false);

    var btnLogin = document.getElementById("btnProverLogin_sounovo");
    btnLogin.addEventListener("click", function () { subLogin_tempSenha(true) }, false);
}

//#endregion

//#region Login

function subLogin_send() {

    document.body.style.cursor = "wait";
    document.getElementById("btnProverLogin_entrar").style.cursor = "wait";

    //#region Envia login

    var email = document.getElementById("txtProverLogin_email").value;
    var senha = document.getElementById("txtProverLogin_senha").value;
    sapi.get("send_login", email.replace("@", "___").replace(/\./g, "__") + "_" + senha, onSucces, onError);

    //#endregion

    //#region Retorno com sucesso

    function onSucces(retorno) {
        //teste = JSON.parse(retorno);

        teste = retorno;
        
        alert(teste.data.login.nome + " - " + teste.data.login.nome_maxLenght + " - " + teste.data.login.nome_dataType + "\n" +
            teste.data.login.email + " - " + teste.data.login.email_maxLenght + " - " + teste.data.login.email_dataType + "\n" +
            teste.data.produto.descricao + " - " + teste.data.produto.marca + "\n" +
            teste.dataCollections.login.fields);

        teste.alerta();

        document.body.style.cursor = "default";
        //document.getElementById("btnProverLogin_entrar").style.cursor = "pointer";

        //login = smartDataStrToObject(retorno);

        if (teste.all.error.number == 0) {

            //#region Remove login e mostra Boas Vindas
            document.getElementById("lblProverLogin_bemvindo").innerHTML = teste.data.login.nome;
            document.getElementById("divProverLogin").remove();
            document.getElementById("divProverLogin_bemvindo").style.display = "";
            //#endregion

            //#region Libera links da faixa de atalho

            document.getElementById("divCategorias01").remove();
            document.getElementById("divProverFaixa_servicos01").addEventListener("click", function () { subAbreLink("produtos.html"); }, false);

            document.getElementById("divCategorias02").remove();
            document.getElementById("divProverFaixa_servicos02").addEventListener("click", function () { subAbreLink("clientes.html"); }, false);

            document.getElementById("divCategorias03").remove();
            document.getElementById("divProverFaixa_servicos03").addEventListener("click", function () { subAbreLink("usuarios_cadastro.html"); }, false);

            //#endregion

        } else {

            alert(login.message);
        }
        return;
    }

    //#endregion

    //#region Retorno com exeption

    function onError(error) {
        document.body.style.cursor = "default";
        if (document.getElementById("btnProverLogin_entrar")) {
            document.getElementById("btnProverLogin_entrar").style.cursor = "pointer";
        }

        alert(error);
        return;
    }
    //#endregion
}

//#endregion

//#region Senha temporária
function subLogin_tempSenha(novo) {

    document.body.style.cursor = "wait";
    document.getElementById("btnProverLogin_esqueci").style.cursor = "wait";

    //#region Envia E-Mail

    login.email = document.getElementById("txtProverLogin_email").value;
    login.senha = "";

    if (novo) {
        login.acessos = "novo";
        login.nome = prompt("Por favor informe seu nome.");
    } else {
        login.acessos = "";
    }

    if (login.email == "") {
        alert("Informe o seu e-mail.")
        return;
    }

    sapi.run("senha_temporaria", login, onSucces, onError);

    //#endregion

    //#region Retorno com sucesso

    function onSucces(retorno) {

        document.body.style.cursor = "default";
        document.getElementById("btnProverLogin_esqueci").style.cursor = "pointer";

        if (retorno.indexOf("!!") ==-1) {

            alert("Um senha temporária foi enviada para o seu e-mail.\n\nSe não estiver em sua caixa de entrada por favor verifique sua caixa de spam e lixeira.");

        } else {

            alert(retorno);

        }

        return;
    }

    //#endregion

    //#region Retorno com exeption

    function onError(error) {
        document.body.style.cursor = "default";
        document.getElementById("btnProverLogin_entrar").style.cursor = "pointer";

        alert(error);
        return;
    }
    //#endregion
}

//#endregion

//#region Abre links de páginas

function subAbreLink(link) {

    localStorage.setItem("login", smartObjectToDataStr(login));

    window.open(link, "_self");

}

//#endregion

function subTeste() {
    alert("teste");
}
