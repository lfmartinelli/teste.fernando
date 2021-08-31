
/**
 * Simple API v1.0 | (c) 2021 by goByte Solutions - Fernando Martinelli - Brazil
 * 
 * The tool objective is just simplify APIs access like horizontal command line. More Like a procedure.
 * Also with this tool, you can change the way to access APIs right here in this script 
 * and with that all your code in the project will already be adapted
 * Including a script excerpt in end of API response, the method execute this in retun.
 * If your response is a object "return(object)", just create in that object a "script" property and put scripts with "[script]" label in before and after script
 * If your response is a string "return(string)", just put in end of string the script excerpt with "[script]" label in before and after of script
 * Ex: [script]alert(text)[script]
 * 
 * In the end of this file, you see exemples about c# class and js function contruction.
 * 
 * This JS file is a open and free tool and contains scripts using Axio API library.
 * Thanks to autor Matt Zabriskie to your job.
 **/

var smartAxioTimeAccess = 0;

var sapi = {
    url_router:"",
    controller: "",
    idle_time: function () { return (subSmartAxioIdle_time()); },
    run: function (sub, parametros, onSuccess, onError) { return (smartAxioAPI_run(sub, parametros, onSuccess, onError)); },
    set: function (usr, dataStr) { return (smartAxio_set(usr, dataStr)); },
    get: function (sub, parameters, onSuccess, onError) { return (smartAxio_get(sub, parameters, onSuccess, onError)); },
    time: new Date(),
}

//#region 'RUN' Executar funcão no Backend
/**
 * Execute, by post API method, a function in backend 
 * Is possible put in return of C# API a [script]js code[/script] and this script will be execute 
 * @param {string} sub: Method in C# WEB API to run
 * @param {string} parameters: Parameters to send to method. Is a srting with itens separate by comma.
 * @param {any} onSuccess: Javascript function to run when API conection is succefull. The function recive return data 
 * @param {any} onError: Javascript function to run when API conection is fail. The function recive error object
 */

async function smartAxioAPI_run(sub, parameters, onSuccess, onError) {

    var objRetorno = {};

    var url = sapi.url_router + "/" + sub

    if (url == "") {
        throw new Error('url_router is not valid!');
        return;
    }

    var agora = new Date();
    smartAxioTimeAccess = agora.getTime();

    var usuario = sapi.user;
    var senha = sapi.password;

    await axios.post(url, parameters).then(
        function (response) {
            var scripts = subSmartClient(response.data);
            if (typeof (response.data) == "object") {
                onSuccess(response.data);
            } else {
                onSuccess(scripts);
            }
        })
            .catch(function (error) {
                onError(error);
            });
}

//#endregion

//#region "GET" Buscar uma informação do backend aplicando método GET (Acesso mais rápido que POST, mas menos seguro)
/**
 * Get data by 'GET' API method in a backend method
 * Is possible put in return of C# API a [script]js code[/script] and this script will be execute
 * @param {string} sub: Method in C# WEB API to return data
 * @param {string} parameters: Parameters that method use in filter or query to return data. Is a srting with itens separate by comma.
 * @param {any} onSuccess: Javascript function to run when API conection is succefull. The function recive return data 
 * @param {any} onError: Javascript function to run when API conection is fail. The function recive error object
 */
async function smartAxio_get(sub, parameters, onSuccess, onError) {

    var objRetorno = {};

    if (parameters != "") {
        parameters = "/" + parameters;
    }

    var url = sapi.url_router + "/" + sub + parameters

    if (url == "") {
        throw new Error('url_router is not valid!');
        return;
    }

    var agora = new Date();
    smartAxioTimeAccess = agora.getTime();

    await axios.get(url).then(
        function (response) {
            var scripts = subSmartClient(response.data);
            if (typeof (response.data) == "object") {
                onSuccess(response.data);
            } else {
                onSuccess(scripts);
            }
        })
        .catch(function (error) {
            onError(error);
        });
}

//#endregion

//#region Gera API idle_time (tempo sem uso) property

function subSmartAxioIdle_time(smartAxioTimeAccess) {

    var hoje = new Date();
    var now = hoje.getTime();

    if (smartAxioTimeAccess == 0) {
        smartAxioTimeAccess = now;
        return (0);
    }

    var tempo_corrido = now - parseInt(smartAxioTimeAccess);
    tempo_corrido = Math.round(((tempo_corrido / 100) / 60) / 10);

    return (tempo_corrido);
}

//#endregion

//#region Roda o script encontrado no retorno

function subSmartClient(script_retorno) {

    var scripts = "";

    var tipo_retorno = typeof (script_retorno);

    if (tipo_retorno == "string") {
        try {
            var retorno_json = JSON.parse(script_retorno);
            script_retorno = retorno_json;
            tipo_retorno = "object";
        } catch (e) {
            tipo_retorno = "string";
        }
    }

    if (tipo_retorno == "object") {
        if (script_retorno) {
            if (script_retorno.script) {
                if (script_retorno.script != "") {
                    scripts = script_retorno.script;
                    script_retorno.script = "";
                } else { return script_retorno; }
            } else if (script_retorno.all) {
                if (script_retorno.all.frontend.script) {
                    if (script_retorno.all.frontend.script != "") {
                        scripts = script_retorno.all.frontend.script;
                        script_retorno.all.frontend.script = "";
                    } else { return script_retorno; }
                }
            }
        } else {
            return script_retorno;
        }
    } else if (typeof (script_retorno) != "string") {
        return script_retorno;
    } else {
        scripts = script_retorno;
    }

    if (scripts == "") {
        return scripts;
    }

    var inicio_script = scripts.indexOf("<script>");

    if (inicio_script == -1) {
        return scripts;
    }

    scripts = scripts.substring(inicio_script);
    scripts = scripts.replaceAll("<script>", "");
    scripts = scripts.split("</script>");

    //scripts = scripts.replace(/\s/g, "");

    //var script = scripts.match(/_iniciodoscriptjs_(.\n?)*_finaldoscriptjs_/g);

    if (scripts.length == 0) {
        return script_retorno;
    }

    for (var i = 0; i < scripts.length; i++) {
        //if (script[i].indexOf("<script>") != -1) {
        var fun = scripts[i]; //.replace(/\<script\>/g, "");
        if (fun != "") {

            eval(fun);

            if (tipo_retorno == "string") {
                script_retorno = script_retorno.replace(`<script>${fun}<\script>`, "");
            }
        }
        //}
    }

    return (script_retorno);
}

//#endregion

//#region 'Upload' Subir arquivo 
function subSmartUpload(inputboxUpload) {

    const inputFiles = document.getElementById(inputboxUpload);
    const formData = new FormData();

    for (const file of inputFiles) {
        formData.append(file.name, file.files[0]);
    }

    fetch(url, {
        method: 'POST',
        body: formData
    })

}

//#endregion

//#region Examples

/*
 Javascript
sapi.run("init", "John,20,male", onSucces, onError);

C#
Controller Header
public class initController : ApiController

Class
[HttpGet]
public object login(string value)
{ 
    //You can declare your object to send return to javascript
    returnObject initial = new returnObject();
    return initial;
}

[HttpPost]
public object init([FromBody] object param)
{
    return value;
}

WebApiConfig
 public static void Register(HttpConfiguration config)
{
    // Rotas da API da Web
       config.MapHttpAttributeRoutes();

       config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{action}/{value}",
          defaults: new { value = RouteParameter.Optional }
       );

 */

//#endregion

//#region Read and Write cookie
function readCookie(name) {
    var i, c, ca, nameEQ = name + "=";
    ca = document.cookie.split(';');
    for (i = 0; i < ca.length; i++) {
        c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1, c.length);
        }
        if (c.indexOf(nameEQ) == 0) {
            return c.substring(nameEQ.length, c.length);
        }
    }
    return '';
}

function writeCookie(name, value, seconds) {

    var date, expires;
    if (seconds != 0) {
        date = new Date();
        date.setSeconds(date.getSeconds() + parseInt(seconds));
        //date.setTime( date.getTime() + ( seconds * 60 * 1000 ) );
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";

    //alert( name + "=" + value + expires + "; path=/" );
}

//#endregion