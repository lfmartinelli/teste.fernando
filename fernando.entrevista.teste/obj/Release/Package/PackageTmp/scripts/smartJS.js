
/*===========================================================
 *=                      Cookies Tools                      =
 *===========================================================
 */
//#region ...

//#region smartCookieRead

function smartCookieRead(name) {
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

//#endregion

//#region smartCookieWrite

function smartCookieWrite(name, value, seconds) {

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

//#region smartDataStrCookieUpdate

function smartDataStrCookieUpdate(nome, nomeitem, valor, seconds) {

    valoresAtuais = readCookie(nome);

    valoresAtuais = "*" + valoresAtuais + "*";

    valoresAtuais = valoresAtuais.replace("**", "*", "gi");

    var posItem = valoresAtuais.indexOf("*" + nomeitem + "=");

    if (posItem != -1) {

        var valorItem = valoresAtuais.substring(posItem + 1);
        valorItem = valorItem.substring(0, valorItem.indexOf("*"));

        valoresAtuais = valoresAtuais.replace(valorItem, nomeitem + "=" + valor);
    } else {
        valoresAtuais = valoresAtuais + "*" + nomeitem + "=" + valor;
    }

    valoresAtuais = valoresAtuais.replace("**", "*", "gi");

    if (valoresAtuais.substring(valoresAtuais.length - 1) == "*") {
        valoresAtuais = valoresAtuais.substring(0, valoresAtuais.length - 1);
    }
    valoresAtuais = valoresAtuais.substring(1);

    writeCookie(nome, valoresAtuais, seconds);
}

//#endregion

//#region smartDataCookieGet

function smartDataCookieGet(lstCookie, nomeItem) {

    var lstItens = smartCookieRead(lstCookie);

    return smartDataStrGet(lstItens, nomeItem);
}

//#endregion

//#region smartDataCookieSet

function smartDataCookieSet(lstItens, nomeItem, valorItem, duracao) {

    if (duracao == undefined) {
        duracao = 0;
    }

    smartCookieWrite(lstItens, smartDataStrSet(lstItens, nomeItem, valorItem), duracao);
}

//#endregion

//#endregion

/*===========================================================
 *=                HTMl Objects Manipulation                =
 *===========================================================
 */
//#region ...

//#region Select box HTML

//#region smartSelect_indexOf - Retorna o index da option que contém o texto em 'text'
/**
 * Retorna o index de uma option que contém o texto em 'text' em um object 'select'
 * @param {string} text:Texto a ser pesquisado na select
 * @param {any} lstList: Object select ou o ID do obejto select a ser pesquisado
 * @param {boolean} bySimilarity: (false) Se a pesquisa deve comparar frases ou palavras exatas ou (true) se for apenas um trexo
 */
function smartSelect_indexOf(select, text, bySimilarity) {

    var lstOptions = {};

    if ((select == null) || (select == undefined)) {
        alert("SmartJS Error Exeption\nSelect or ID Select control is NULL or undefined!");
        return;
    }

    if (typeof (select) == "object") {
        lstOptions = select;
    } else {
        lstOptions = document.getElementById(select);
    }

    for (var optionIndex = 0; optionIndex < lstOptions.length; optionIndex++) {
        if (bySimilarity == true) {
            //alert(lstOptions.options[optionIndex].value);
            if (lstOptions.options[optionIndex].value.indexOf(text) > -1) {
                //lstOptions.selectedIndex = optionIndex;
                return (optionIndex);
            } else if (lstOptions.options[optionIndex].innerHTML.indexOf(text) > -1) {
                //lstOptions.selectedIndex = optionIndex;
                return (optionIndex);
            }
        } else {
            if (lstOptions.options[optionIndex].value.toLowerCase().replace(/\s/g, "") == text.toLowerCase().replace(/\s/g, "")) {
                //lstOptions.selectedIndex = optionIndex;
                return (optionIndex);
            } else if (lstOptions.options[optionIndex].innerHTML.toLowerCase().replace(/\s/g, "") == text.toLowerCase().replace(/\s/g, "")) {
                //lstOptions.selectedIndex = optionIndex;
                return (optionIndex);
            }
        }
    }

    return (-1);
}
//#endregion

//#region smartSelect_goto - Seleciona o option que contém o texto em 'text'
/**
 * Retorna o index de uma option que contém o texto em 'text' em um object 'select'
 * @param {string} text:Texto a ser pesquisado na select
 * @param {any} lstList: Object select ou o ID do obejto select a ser pesquisado
 * @param {boolean} bySimilarity: (false) Se a pesquisa deve comparar frases ou palavras exatas ou (true) se for apenas um trexo
 */
function smartSelect_goto(select, text, bySimilarity) {

    var index = smartSelect_indexOf(select, text, bySimilarity);

    if (typeof (select) == "object") {
        select.selectedIndex = index;
    } else {
        document.getElementById(select).selectedIndex = index;
    }    
}
//#endregion

//#region smartSelect_selectedIndex - Retorna o index em uma SelectedOptions feferente ao index de uma option geral.
/**
 * Retorna o index em uma SelectedOptions referente ao index de uma option geral.
 * Ou seja, a option com index 10 da select é a 2a linha selecionada e por isso retorna 1.
 * @param {any} select:Controle Select a ser pesquisado
 * @param {number} index:O index da option a ser encontrada na SelectedOptions
 */
function smartSelect_SelectedIndex(select, index) {

    var lstOptions = {};

    if ((select == null) || (select == undefined)) {
        alert("SmartJS Error Exeption\nSelect or ID Select control is NULL or undefined!");
        return;
    }

    if (select.type.indexOf("select") == -1) {
        lstOptions = document.getElementById(select);
    } else {
        lstOptions = select;
    }

    if (lstOptions.selectedOptions == 0) {
        return (-1);
    }

    var firstIndex = lstOptions.selectedOptions[0].index;
    index = index - firstIndex;

    return (index);
}
//#endregion

//#region smartSelectAttr_indexOf - Retorna o index da option que o attributo contém o texto em 'text'
/**
 * Retorna o index de uma option contém no attributo "attribute" o texto em 'text' em um object 'select'
 * @param {string} text:Texto a ser pesquisado na select
 * @param {string} attribute:Atributo para busca.
 * Ex: data-nome
 * @param {any} lstList: Object select ou o ID do obejto select a ser pesquisado
 * @param {boolean} bySimilarity: (false) Se a pesquisa deve comparar frases ou palavras exatas ou (true) se for apenas um trexo
 */
function smartSelectAttr_indexOf(select, attribute, text, bySimilarity) {

    var lstOptions = {};

    if ((select == null) || (select == undefined)) {
        alert("SmartJS Error Exeption\nSelect or ID Select control is NULL or undefined!");
        return;
    }

    if (select.type.indexOf("select") == -1) {
        lstOptions = document.getElementById(select);
    } else {
        lstOptions = select;
    }

    for (var optionIndex = 0; optionIndex < lstOptions.length; optionIndex++) {
        if (bySimilarity == true) {

            if (lstOptions.options[optionIndex].getAttribute(attribute).indexOf(text) > -1) {
                //lstOptions.selectedIndex = optionIndex;
                return (optionIndex);
            }
            
        } else {

            if (lstOptions.options[optionIndex].getAttribute(attribute).toLowerCase().replace(/\s/g, "") == text.toLowerCase().replace(/\s/g, "")) {
                //lstOptions.selectedIndex = optionIndex;
                return (optionIndex);
            }
            
        }
    }

    return (-1);
}
//#endregion

//#region smartSelectAttr_goto - Seleciona a option que o attributo contém o texto em 'text'
/**
 * Seleciona o option (linha) que contém no attributo "attribute" o texto em 'text' em um object 'select'
 * @param {string} text:Texto a ser pesquisado na select
 * @param {string} attribute:Atributo para busca.
 * Ex: data-nome
 * @param {any} lstList: Object select ou o ID do obejto select a ser pesquisado
 * @param {boolean} bySimilarity: (false) Se a pesquisa deve comparar frases ou palavras exatas ou (true) se for apenas um trexo
 */
function smartSelectAttr_goto(select, attribute, text, bySimilarity) {

    var index = smartSelectAttr_indexOf(select, text, bySimilarity);

    if (typeof (select) == "object") {
        select.selectedIndex = index;
    } else {
        document.getElementById(select).selectedIndex = index;
    }    
}
//#endregion

//#region smartSelectReplace - Substituí um texto ou trecho nas options de uma Select
/**
 *Substituí um texto ou trecho nas options de uma Select
 * @param {any} select: Objeto select ou ID do select de onde o texto será substituído
 * @param {string} oldText: Texto ou trecho a ser pesquisado
 * @param {string} newText: Novo texto de substituição
 * @param {boolean} bySimilarity: (false) Se o texto a ser pesquisado tem que ser exato ou (true) se pode apenas ser um trecho.
 */
function smartSelectReplace(select, oldText, newText, bySimilarity) {

    var options = {};

    if (select.type.indexOf("select") == -1) {
        options = document.getElementById(select);
    } else {
        options = select;
    }

    for (var itemIndex = 0; itemIndex < options; itemIndex++) {

        if (bySimilarity == true) {
            if (options.options[itemIndex].value == oldText) {
                options.options[itemIndex].value = newText;
                options.selectedIndex = itemIndex;
            }

            if (options.options[itemIndex].innerHTML == oldText) {
                options.options[itemIndex].innerHTML = newText;
                options.selectedIndex = itemIndex;
            }

        } else {

            if (options.options[itemIndex].value.indexOf(oldText) != -1) {
                var regex = new RegExp("/" + oldText + "/g");
                options.options[itemIndex].value.replace(regex, newText);
                options.selectedIndex = itemIndex;
            }

            if (options.options[itemIndex].innerHTML.indexOf(oldText) != -1) {
                var regex = new RegExp("/" + oldText + "/g");
                options.options[itemIndex].innerHTML.replace(regex, newText);
                options.selectedIndex = itemIndex;
            }
        }
    }
}

//#endregion

//#region smartSelectGetDataStrByIndex - Retorna o valor de uma DataStr key em uma option em um objeto select 
/**
 * Retorna o valor de uma DataStr key em uma option em um objeto select
 * @param {any} select: Object select contendo a DataStr em option[optionIndex].value
 * @param {int} optionIndex: Index do option onde se encontra o DataStr a ser pesquisado
 * @param {string} key: Key do DataStr
 */
function smartSelectGetDataStrByIndex(select, optionIndex, key) {

    var options = {};

    if (select.type.indexOf("select") == -1) {
        options = document.getElementById(select);
    } else {
        options = select;
    }

    var text = getItemDataStr(options.options[optionIndex].value, key, "");
    options.selectedIndex = optionIndex;

    return (text);
}

//#endregion

//#region smartSelectSetDataStrByIndex - Substitui ou cria um valor em uma key da DataStr em uma option[optionIndex].value em um objeto select 
/**
 *Substitui ou cria um valor em uma key da DataStr em uma option[optionIndex].value em um objeto select
 * @param {any} select: Object select contendo a DataStr em option[optionIndex].value
 * @param {int} optionIndex: Index do option onde se encontra o DataStr a ser pesquisado
 * @param {string} key: Key do DataStr
 * @param {string} value: Novo valor para a key da DataStr
 */
function smartSelectSetDataStrByIndex(select, optionIndex, key, value) {

    var options = {};

    if (select.type.indexOf("select") == -1) {
        options = document.getElementById(select);
    } else {
        options = select;
    }

    options.options[optionIndex].value = setItemDataStr(options.options[optionIndex].value, key, value, "");
    options.selectedIndex = optionIndex;
}

//#endregion

//#region smartSelectGetDataStrByText - Retorna o valor de uma DataStr key em uma option em um objeto select 
/**
 * Retorna o valor de uma DataStr key em uma option em um objeto select
 * @param {any} select
 * @param {string} text
 * @param {string} key
 * @param {boolean} bySimilarity
 */
function smartSelectGetDataStrByText(select, text, key, bySimilarity) {

    var options = {};

    if (select.type.indexOf("select") == -1) {
        options = document.getElementById(select);
    } else {
        options = select;
    }

    var value = "";

    for (var itemIndex = 0; itemIndex < options; itemIndex++) {

        if (bySimilarity == true) {
            if (options.options[itemIndex].innerHTML.toLowerCase().replace(/\s/g, "") == text.toLowerCase().replace(/\s/g, "")) {
                value = getItemDataStr(options.options[itemIndex].value, key, "");
            }

        } else {

            if ("*" + options.options[itemIndex].innerHTML.toLowerCase().replace(/\s/g, "*").indexOf("*" + text.toLowerCase().replace(/\s/g, "*")) != -1) {
                value = getItemDataStr(options.options[itemIndex].value, key, "");
            }
        }

        options.selectedIndex = itemIndex;
        return (value);
    }

    return ("");

}

//#endregion

//#region smartSelectSetDataStrByText - Substitui ou cria um valor em uma key da DataStr em uma option.value onde a option.innerHTML contem o texto ou trecho de 'text' em um objeto select. 
/**
 * Substitui ou cria um valor em uma key da DataStr em uma option.value onde a option.innerHTML contem o texto ou trecho de 'text' em um objeto select.
 * @param {any} select: Objeto select destino
 * @param {string} text: Texto ou trecho a ser pesquisado para a busca do DataStr
 * @param {string} key: Key do DataStr
 * @param {boolean} bySimilarity: (false) Se for para pesquisar o texto exato ou (true) se for um trecho.
 */
function smartSelectSetDataStrByText(select, text, key, value, bySimilarity) {

    var options = {};

    if (select.type.indexOf("select") == -1) {
        options = document.getElementById(select);
    } else {
        options = select;
    }

    var value = "";

    for (var itemIndex = 0; itemIndex < options; itemIndex++) {

        if (bySimilarity == true) {
            if (options.options[itemIndex].innerHTML.toLowerCase().replace(/\s/g, "") == text.toLowerCase().replace(/\s/g, "")) {
                options.options[itemIndex].value = setItemDataStr(options.options[itemIndex].value, key, value, "");
                options.selectedIndex = itemIndex;
                return;
            }

        } else {

            if ("*" + options.options[itemIndex].innerHTML.toLowerCase().replace(/\s/g, "*").indexOf("*" + text.toLowerCase().replace(/\s/g, "*")) != -1) {
                options.options[itemIndex].value = setItemDataStr(options.options[itemIndex].value, key, value, "");
                options.selectedIndex = itemIndex;
                return;
            }
        }
    }
}

//#endregion

//#region smartSelectAdd - Adiciona um registro em um objeto select
/**
 * Adiciona um registro em um objeto select
 * @param {any} select: Objeto select ou ID do objeto select destino
 * @param {string} value: Valor a ser adicionado no option.value
 * @param {string} innerHTML: Valor a ser adicionado no option.innerHTML
 */
function smartSelectAdd(select, innerHTML, value) {
    var options = {};

    if (select.type.indexOf("select") == -1) {
        options = document.getElementById(select);
    } else {
        options = select;
    }

    var option = document.createElement("option");
    option.value = value;
    option.innerHTML = innerHTML;

    options.add(option);
}

//#endregion

//#region smartSelectRemoveByValue - Remove um item de uma select que contem o texto ou trecho de 'text' em um option.value do objeto select. 
/**
 * Remove um item de uma select que contem o texto ou trecho de 'text' em um option.value do objeto select.
 * @param {any} select: Objeto select com o item a ser removido
 * @param {string} value: Texto ou trecho da option.value a ser removida o item
 * @param {boolean} bySimilarity: (false) Se for para pesquisar o texto exato ou (true) se for um trecho.
 */
function smartSelectRemoveByValue(select, value, bySimilarity) {

    var options = {};

    if (select.type.indexOf("select") == -1) {
        options = document.getElementById(select);
    } else {
        options = select;
    }

    var value = "";

    for (var itemIndex = 0; itemIndex < options; itemIndex++) {

        if (bySimilarity == true) {
            if (options.options[itemIndex].value.toLowerCase().replace(/\s/g, "") == value.toLowerCase().replace(/\s/g, "")) {
                options.remove(itemIndex);
                options.selectedIndex = itemIndex;
                return;
            }

        } else {

            if ("*" + options.options[itemIndex].value.toLowerCase().replace(/\s/g, "*").indexOf("*" + value.toLowerCase().replace(/\s/g, "*")) != -1) {
                options.remove(itemIndex);
                options.selectedIndex = itemIndex;
                return;
            }
        }
    }
}

//#endregion

//#region smartSelectRemoveByText - Remove um item de uma select que contem o texto ou trecho de 'text' em um option.value do objeto select. 
/**
 * Remove um item de uma select que contem o texto ou trecho de 'text' em um option.value do objeto select.
 * @param {any} select
 * @param {string} text
 * @param {boolean} bySimilarity
 */
function smartSelectRemoveByText(select, text, bySimilarity) {

    var options = {};

    if (select.type.indexOf("select") == -1) {
        options = document.getElementById(select);
    } else {
        options = select;
    }

    var value = "";

    for (var itemIndex = 0; itemIndex < options; itemIndex++) {

        if (bySimilarity == true) {
            if (options.options[itemIndex].innerHTML.toLowerCase().replace(/\s/g, "") == text.toLowerCase().replace(/\s/g, "")) {
                options.remove(itemIndex);
                options.selectedIndex = itemIndex;
                return;
            }

        } else {

            if ("*" + options.options[itemIndex].innerHTML.toLowerCase().replace(/\s/g, "*").indexOf("*" + text.toLowerCase().replace(/\s/g, "*")) != -1) {
                options.remove(itemIndex);
                options.selectedIndex = itemIndex;
                return;
            }
        }
    }
}

//#endregion

//#endregion

//#region smartHTML_setStyles - troca ou insere uma configuração style em uma DataStr para um objeto HTML

/**
 * Troca ou insere uma configuração style em uma DataStr para um objeto HTML
 * @param {string} strList:strList com itens separados por ";" contendo os parâmetros de style a serem inseridas no controle
 * @param {object} object: Object ou o ID do obejto a receber os styles
 */
function smartHTML_setStyles(strList, object) {

    var htmlControle = {};

    if (typeof (object) == "object") {
        htmlControle = object;
    } else {
        htmlControle = document.getElementById(object);
    }

    var styles = strList.split(';');

    for (let i = 0; i < styles.length; i++) {

        if (styles[i].indexOf(":") == -1) {
            break;
        }

        if ((styles[i].indexOf("text:") != -1) || (styles[i].indexOf("value:") != -1)) {

            let item_style = styles[i].split(':');

            if ("value" in htmlControle) {
                htmlControle.value = item_style[1];
            } else {
                htmlControle.innerHTML = item_style[1];
            }
        } else {
            var style_atual = htmlControle.getAttribute("style");

            if (style_atual != "") {
                style_atual += "; ";
            }

            htmlControle.setAttribute("style", style_atual + styles[i]);
        }
    }

}

//#endregion

//#region smartObjGroups

//#region smartObjGroup_new - Cria um novo grupo de objetos do documento
/**
 * Cria um novo grupo de objetos do documento
 * @param {string} data_attribute:O atributo data- como valor referencia. Você pode informar o data- exato ou apenas informar o parametro sem o
 * prefixo data-
 * @param {string} valor:Valor de referencia que indica quais controles devem fazer parte do grupo. Ex: data-state=saved, todos os que tem valor 
 * "saved" no data_attribute "data-state" ou "state".
 */
function smartObjGroup_new(data_attribute, valor) {
    if (data_attribute.indexOf("data-") == -1) {
        data_attribute = "data-" + data_attribute;
    }

    return (document.querySelector(`[${data_attribute}="${valor}"]`));
}

//#endregion

//#region smartObjGroup_indexOf - Index do objeto no grupo com ID solicitado
/**
 * Index do obejeto no grupo com ID solicitado
 * @param {Array} group: Grupo de objetos a serem pesquisados
 * @param {string} id: ID do obejto procurado
 */
function smartObjGroup_indexOf(group, id) {

    for (let i = 0; i < group.length; i++) {

        if (group[i].id == id) {
            return (i);
        }

    }

    return (-1);

}

//#endregion

//#region smartObjGroup_focus - Qual objeto no grupo está com o focus  
/**
 * Qual objeto no grupo está com o focus
 * @param {array} group: Grupo de objects 
 */
function smartObjGroup_focus(group) {

    for (let i = 0; i < group.length; i++) {

        if (group[i].id == document.activeElement.id) {
            return (i);
        }

    }
    return (-1);
}

//#endregion

//#endregion

//#region smartHTMLSection

function getHTMLSection(file, bloco) {

    var html = openTextFile(file);

    var indexBloco = html.indexOf("<section id=\"" + bloco + "\">");
    if (indexBloco == -1) {
        indexBloco = html.indexOf("<bloco  id=\"" + bloco + "\">");
    }
    if (indexBloco == -1) {
        indexBloco = html.indexOf("<section id =\"" + bloco + "\">");
    }
    if (indexBloco == -1) {
        indexBloco = html.indexOf("<section id = \"" + bloco + "\">");
    }
    if (indexBloco == -1) {
        indexBloco = html.indexOf("<section id=\"" + bloco + "\" >");
    }
    if (indexBloco == -1) {
        indexBloco = html.indexOf("<section id = \"" + bloco + "\" >");
    }
    if (indexBloco == -1) {
        indexBloco = html.indexOf("<section id =\"" + bloco + "\" >");
    }
    if (indexBloco == -1) {
        indexBloco = html.indexOf("<section id= \"" + bloco + "\">");
    }

    if (indexBloco == -1) {
        alert("Bloco HTML não encontrado!");
        return;
    }

    html = html.substring(indexBloco);

    html = html.substring(0, html.indexOf("</section>"));

}

//#endregion

//#region smartHTMLDecodeSymbols

function smartHTMLDecodeSymbols(html) {

    if ((html == "") || (html == undefined)) {
        return;
    }

    html = html.replace(/\[equal\]/g, "=").
        replace(/\[quote\]/g, "\"").
        replace(/\[star\]/g, "*").
        replace(/\[otag\]/g, "<").
        replace(/\[ctag\]/g, ">").
        replace(/\[obrace\]/g, "}").
        replace(/\[cbrace\]/g, "{").
        replace(/\[squote\]/g, "'");

    return (html);
}

//#endregion

//#region smartHTMLEncodeSymbols

function smartHTMLEncodeSymbols(html) {

    if ((html == "") || (html == undefined)) {
        return;
    }

    html = html.replace(/=/g, "[equa]").
        replace(/\"/g, "[quote]").
        replace(/\*/g, "[star]").
        replace(/\</g, "[otag]").
        replace(/\>/g, "[ctag]").
        replace(/\>/g, "[ctag]").
        replace(/\{/g, "[obrace]").
        replace(/\}/g, "[cbrace]").
        replace(/\'/g, "[squote]");

    return (html);
}

//#endregion

//#region smartHTMLSupressEnter
function smartHTMLSupressEnter() {
    var eventName = 'onkeydown';
    var handlerFunc = handleKeypress;

    body.detachEvent(eventName, handlerFunc);
    body.attachEvent(eventName, handlerFunc);
}


//#endregion

//#region smartObjcloneValueTo Clona value em outro controle

/**
 * Clone text in value or innerHTML property of one control to another control or multiple controls.
 * @param {string} thisControl: ID of source control
 * @param {string} toControls: ID of destiny control or controls. Can indicate multiple controls using ",". Ex: txtName, txtNick ... 
 */
function smartObjcloneValueTo(thisControl, toControls) {

    var value = "";
    if (document.getElementById(thisControl).value) {
        value = document.getElementById(thisControl).value;
    } else {
        value = document.getElementById(thisControl).innerHTML;
    }

    var destino = toControls.replace(/\s/g, "").split(',');
    for (let i = 0; i < destino.length; i++) {
        if (document.getElementById(destino[i]).value) {
            document.getElementById(destino[i]).value = value;
        } else {
            document.getElementById(destino[i]).innerHTML = value;
        }
    }
}

//#endregion

//#endregion

/*===========================================================
 *=                          JS Object                      =
 *===========================================================
 */
//#region ...

//18/07/2020
//#region smartObjectArrayToDataStrList - Criar uma Data String de um object com propriedades em array

/**
 * Criar um DataString de um object com propriedades em array
 * @param {Object} object:Objeto com propriedades em array
 * Retorna ex: nome=fulano*idade=40|nome=beltrano*idade=20
 */
function smartObjectArrayToDataStrList(object) {
    if (typeof (object) != "object") {
        throw "O parâmetro não é um object";
    }

    var key = [];

    for (var prop in object) {
        key.push(prop);
    }

    var count = 0;

    for (let indexKey = 0; indexKey < key.length; indexKey++) {
        if (object[key[indexKey]].length > count) {
            count = object[key].length;
        }
    }

    var list = "";

    for (let indexItem = 0; indexItem < count; indexItem++) {
        var linha = "";
        for (let IndexKey = 0; indexKey < key.length; indexKey++) {
            linha += "*" + key[indexKey] + "=" + object[key[indexKey]];
        }

        list = "|" + linha.substring(1);
    }

    return (linha.substring(1));

}

//#endregion

//#region smartObjectToDataStr - Converte um object em DataStr

/**
 * Converte um object em DataStr
 * @param {object} object objeto a ser convertido
 */
function smartObjectToDataStr(object) {

    if (!object) {
        return ("");
    }

    var dataList = "";

    for (var propriedade in object) {
        dataList = dataList + "*" + propriedade + "=" + object[propriedade];
    }

    return (dataList.substring(1));
}

//#endregion

//#region smartObjectConcat - Concatena(une)/atualiza o objeto 1 adiconando os campos e valores do objeto 2.
/**
 * Concatena(une)/atualiza o objeto 1 adiconando os campos e valores do objeto 2.
 * @param {object} object1 Objeto destino
 * @param {object} object2 Objeto com os registros origem a serem enviados ao objeto 1
 * @param {boolean} upadateIfExist true/false, se o valor dos registros do obejto 2 que já existem no objeto 1 devem ser atualizados 
 */
function smartObjectConcat(object1, object2, upadateIfExist) {

    for (var campo in object2) {
        if ((!object1[campo]) || (upadateIfExist == true)) {
            object1[campo] = object2[campo];
        }
    }

    return (object1);
}

function updateObjectFromDataStr(object, itemList) {

    var rows = itemList.split('*');

    for (var i = 0; i < rows.length; i++) {

        var item = rows[i].split('=');
        object[item[0]] = item[1];
    }

    return (object);
}

//#endregion

//#endregion

/*===========================================================
 *=                         Data String                     =
 *===========================================================
 */
//#region ...

//#region smartDataStrGet - Pega um valor em uma DataStr referente a coluna em "nomeItem" 
/**
 * Pega o valor em um item(campo) de uma DataStr 
 * @param {string} dataStr DataStr com os dados
 * @param {string} nomeItem Nome item (campo) para busca do valor
 * @param {string} parametros *Opcional - Parametros como chars separadores de key/value e separador de coluna Ex: key=,col* 
 */
function smartDataStrGet(dataStr, nomeItem, parametros) {

    if (dataStr == "") {
        //alert(lstItens + " sem registros!");
        return ("");
    }

    var separadorCol = "*";
    var separadorKey = "=";

    //#region Parâmetros

    if ((parametros != undefined) && (parametros != "")) {

        var col = parametros.match(/(col\W.*?)/);
        if (col[0] != "") { separadorCol = col[0].replace("col", ""); }

        var key = parametros.match(/(key\W.*?)/);
        if (key[0] != "") { separadorKey = key[0].replace("key", ""); }
    }

    //#endregion

    dataStr = separadorCol + dataStr + separadorCol;

    var valorItens = dataStr;

    if (nomeItem != "") {
        nomeItem = separadorCol + nomeItem + separadorKey;

        //alert( nomeItem );
        var itemPos = valorItens.indexOf(nomeItem);

        if (itemPos == -1) {
            return ("");
        }

        valorItens = valorItens.substring(itemPos);
        itemPos = valorItens.indexOf(separadorKey);
        valorItens = valorItens.substring(itemPos + 1);
        itemPos = valorItens.indexOf(separadorCol);
        valorItens = valorItens.substring(0, itemPos);

        return (valorItens);
    }
}

//#endregion

//#region smartDataStrSet - Insere um registro ou altera um valor de um registro já existente na DataStr
/**
 * Insere um registro ou altera um valor de um registro já existente na DataStr
 * @param {string} dataStr DataStr com os dados
 * @param {string} nomeItem Nome item (campo) para busca do valor
 * @param {string} valorItem Novo valor a ser inserido ou substituído
 * @param {string} parametros *Opcional - Parametros como chars separadores de key/value e separador de coluna Ex: key=,col*
 */
function smartDataStrSet(dataStr, nomeItem, valorItem, parametros) {

    var separadorCol = "*";
    var separadorKey = "=";

    //#region Parâmetros

    //var separador = lstItens.match(/(\(.*?\)){/).replace(/\s/g, "");

    if ((parametros != undefined) && (parametros != "")) {

        var col = parametros.match(/(col\W.*?)/).replace("col", "");
        if (col != "") { separadorCol = col; }

        var key = parametros.match(/(key\W.*?)/).replace("key", "");
        if (key != "") { separadorKey = key; }
    }

    //#endregion

    if (dataStr == "") {
        dataStr = nomeItem + "=" + valorItem;
        return dataStr;
    }

    var Itens = separadorCol + dataStr + separadorCol;

    var posIem = Itens.indexOf(separadorCol + nomeItem + separadorKey);

    if (posIem == -1) {
        dataStr = dataStr + separadorCol + nomeItem + separadorKey + valorItem;
        return dataStr;
    }

    Itens = Itens.substring(posIem + 1);
    Itens = Itens.substring(0, Itens.indexOf(separadorCol));

    dataStr = dataStr.replace(Itens, nomeItem + separadorKey + valorItem);

    return (dataStr);
}

//#endregion

//#region smartDataStrUpdateDataStr - Atualiza uma DataStr com valores de outra DataStr
/**
 * Atualiza uma DataStr com valores de outra DataStr
 * @param {string} dataStr DataStr destino para atualização
 * @param {string} update_dataStr DataStr origem com os novos dados
 */
function smartDataStrUpdateDataStr(dataStr, update_dataStr) {

    var separador = "&";

    if (dataStr.indexOf("*") > -1) {
        separador = "*";
    }

    var dataStrOrigem = update_dataStr.split(separador);

    for (var item = 0; item < dataStrOrigem.length; item++) {
        let campo = dataStrOrigem[item].substring(0, dataStrOrigem[item].indexOf("="));
        campo = campo.replace(/\s/g, "");
        let valor = dataStrOrigem[item].substring(dataStrOrigem[item].indexOf("=") + 1);

        setItemDataStr(dataStr, campo, valor);
    }

    return objectToDataStr(object);
}

function dataStrContain(dataStr, item) {

}

//#endregion

//#region smartDataStrToObject - Converte um Data String para um Object
/**
 * Converte o Data String para um object
 * @param {string} dataStr
 * @param {string} parametros
 */
function smartDataStrToObject(dataStr, parametros) {

    if (dataStr == "") {
        return;
    }

    var object = {};

    var separadorCol = "*";
    var separadorKey = "=";

    //#region Parâmetros

    if (parametros != undefined) {

        if (parametros != "") {
            var col = parametros.match(/(col\W.*?)/).replace("col", "");
            if (col != "") { separadorCol = col; }

            var key = parametros.match(/(key\W.*?)/).replace("key", "");
            if (key != "") { separadorKey = key; }
        }

    }

    //#endregion

    if (dataStr.indexOf(separadorCol) == -1) {

        var row = dataStr.split(separadorKey);

        object[row[0]] = row[1];

    } else {

        var list = dataStr.split(separadorCol);

        for (var i = 0; i < list.length; i++) {
            var row = list[i].split(separadorKey);

            object[row[0]] = row[1];
        }
    }

    return (object);
}

//#endregion

//#region smartDataStrToChild - Cria um Data String retirando dados de um Data String pai
/**
 * Cria um Data String retirando dados de um Data String pai
 * @param {string} dataStr: Data string pai
 * @param {string} campos: Campos contidos no pai para gerar o novo Data String
 * @param {string} parametros: *Opcional - Parametros como chars separadores de key/value e separador de coluna Ex: key=,col*
 */
function smartDataStrToChild(dataStr, campos, parametros) {

    campos = campos.replace(/\s/, "");
    lstCampos = campos.split(',');
    var child = "";

    for (var i = 0; i < lstCampos.length; i++) {
        child += "*" + lstCampos[i] + "=" + smartDataStrGet(dataStr, lstCampos[i], parametros);
    }

    return (child.substring(1));
}

//#endregion

//#endregion

/*===========================================================
 *=                          URL Tools                      =
 *===========================================================
 */
//#region ...

//#region getInfoByMapsURL - Pega coordenadas Lat e Long na url do Google Maps

function getInfoByMapsURL(url) {

    if (url == "") { return; }

    if (url.indexOf("goo-gl") != -1) {
        alert("Não utilize o link resumido para compartilhamento do Google Maps\nCopie o endereço completo da barra de endereços.")
    }

    url = url.replace("https://www.google.com.br/maps/", "");

    var strEndereco = "";
    var strBairro = "";
    var strCidade = "";
    var strEstado = "";

    

    //#region CEP

    //    var regexCep = /\+([0-9]){5}\-([0-9]){3}/;
    //    var lstCep = url.match(regexCep);

    //if (lstCep.length != 0) {
    //var cep = lstCep[0].replace("+", "");
    //cep = cep.substring(cep.length - 9);
    //if (cep != "") {
    //    loadCepApiToObject(cep);

    //    if (smartAdress.endereco) {
    //        strBairro = smartAdress.bairro;
    //        strCidade = smartAdress.cidade;
    //        strEstado = smartAdress.estado;
    //        strEndereco = smartAdress.endereco;
    //    }
    //} 
    //#endregion

    var logradouro = "";
    var logradouroSemBairro = "";

    if (url.indexOf("Tv.") != -1) {
        logradouro = /place\/Tv\.(.*)\,\+([0-9]*)\+\-\+(.*)\,\+(.*)\+\-\+([A-Z]){2}/;
        logradouroSemBairro = /place\/Tv\.(.*)\,\+(.*)\+\-\+([A-Z]){2}/;

    } else if (url.indexOf("Av.") != -1) {
        logradouro = /place\/Av\.(.*)\,\+([0-9]*)\+\-\+(.*)\,\+(.*)\+\-\+([A-Z]){2}/;
        logradouroSemBairro = /place\/Av\.(.*)\,\+(.*)\+\-\+([A-Z]){2}/;

    } else if (url.indexOf("R.") != -1) {
        logradouro = /place\/R\.(.*)\,\+([0-9]*)\+\-\+(.*)\,\+(.*)\+\-\+([A-Z]){2}/;
        logradouroSemBairro = /place\/R\.(.*)\,\+(.*)\+\-\+([A-Z]){2}/;

    } else if (url.indexOf("BR-") != -1) {
        logradouro = /place\/BR\-(.*)\,\+([0-9]*)\+\-\+(.*)\,\+(.*)\+\-\+([A-Z]){2}/;
        logradouroSemBairro = /place\/BR\-(.*)\,\+(.*)\+\-\+([A-Z]){2}/;

    } else if (url.indexOf("Rod.") != -1) {
        logradouro = /place\/Rod\.(.*)\,\+([0-9]*)\+\-\+(.*)\,\+(.*)\+\-\+([A-Z]){2}/;
        logradouroSemBairro = /place\/Rod\.(.*)\,\+(.*)\+\-\+([A-Z]){2}/;

    } else if (url.indexOf("Estr.") != -1) {
        logradouro = /place\/Estr\.(.*)\,\+([0-9]*)\+\-\+(.*)\,\+(.*)\+\-\+([A-Z]){2}/;
        logradouroSemBairro = /place\/Estr\.(.*)\,\+(.*)\+\-\+([A-Z]){2}/;
    }

    var tipo_url = "";

    var lstEndereco;

    if (logradouro == "") {
        logradouro = /place\/(.*)\,\+([0-9]*)\+\-\+(.*)\,\+(.*)\+\-\+([A-Z]){2}/;
        lstEndereco = url.match(logradouro);
    } else {
        lstEndereco = url.match(logradouro);
        if (!lstEndereco) {
            lstEndereco = url.match(logradouroSemBairro);
        }
    }

    if (lstEndereco) {

        var dados = lstEndereco[0].replace("place/", "").replace(/\+([0-9]*)\+\-\+/, "").replace(/\+\-\+/g, ",").split(',');

        //lstEndereco[0] = decodeURLLatinChars(lstEndereco[0]);

        if (logradouro != "") {
            strEndereco = decodeURLLatinChars(dados[0]).replace(/\+/g, " ");
        }

        if (dados.length > 3) {
            strBairro = decodeURLLatinChars(dados[1]).replace(/\+/g, " ");
        } else if ((dados.length == 3) && (logradouro == "")) {
            strBairro = decodeURLLatinChars(dados[0]).replace(/\+/g, " ");
        }

        if (dados.length > 3) {
            strCidade = decodeURLLatinChars(dados[2]).replace(/\+/g, " ").trim();
        } else if (dados.length == 3) {
            strCidade = decodeURLLatinChars(dados[1]).replace(/\+/g, " ").trim();
        }

        if (dados.length > 3) {
            strEstado = dados[3].replace(/\+/g, " ");
        } else if (dados.length == 3) {
            strEstado = dados[2].replace(/\+/g, " ");
        } 

    }

    var regexGps = /\!3d(\W\d*.\w\d*)\!4d(\W\d*.\w\d*)/;
    var lstGps = url.match(regexGps);
    var strGps = "";

    if (lstGps.length != 0) {
        
        strGps = lstGps[0];

        strGps = strGps.replace("!3d", "").replace("!4d", ",");
        strGps = strGps.substring(0, strGps.length - 1);
    }

    //var strGps = url.substring(url.length - 23).replace("!4d", ",");
    //strGps = strGps.substring(0, strGps.length - 1);

    var url_dados = {
        endereco : strEndereco,
        bairro : strBairro,
        estado :  strEstado,
        cidade : strCidade,
        gps : strGps
    };

    return (url_dados);
}

//#endregion

//#region getURLQuery - Retorna um paramentro informado na URL

function smartURLQuery(param) {

    var url = window.location.href;

    var param_init = url.indexOf("?");

    if (param_init == -1) {
        return "";
    }

    param = "&" + param + "=";

    url = "&" + url.substring(param_init + 1) + "&";

    param_init = url.indexOf(param);

    if (param_init == -1) {
        return "";
    }

    var filter = new RegExp("\\" + param + "(.?)+?\&");

    var value = url.match(filter);
    var value_param = value[0].substring(value[0].indexOf("=") + 1).replace("&", "");

    return value_param
}

//#endregion

//#endregion

/*===========================================================
 *=                           IO Tools                      =
 *===========================================================
 */
//#region ...

//#region smartIOFileExists File exists

function smartIOFileExists(image_url) {

    var http = new XMLHttpRequest();

    http.open('HEAD', image_url, false);
    http.send();

    return http.status != 404;

}

//#endregion

//#region smartIOImageCheck

function smartIOImageCheck(imageSrc, good, bad) {
    var img = new Image();
    img.onload = good;
    img.onerror = bad;
    img.src = imageSrc;
}

//#endregion

//#region smartIOOTextFileOpen

function smartIOOTextFileOpen(file) {
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", file, true);
    rawFile.onreadystatechange = function () {
        if (rawFile.readyState === 4) {
            if (rawFile.status === 200 || rawFile.status == 0) {
                var allText = rawFile.responseText;
                return (allText);
            }
        }
    }
    rawFile.send(null);

}

//#endregion

//#endregion

/*===========================================================
 *=                         STRING Tools                    =
 *===========================================================
 */
//#region ...

//#region smartString()

/**
 * Format string to especific format indicates in formatProvider 
 * @param {any} value:String to format
 * @param {string} formatProvider:String indicate a new format to string: 'yyyy-MM-dd' to date, 'C' to currency, 
 * 'D2' where 2 is number of zeros include before 'value', 'N' to current language number, 'US' USA, 'EU' European, 'JP' Japan and 'BR' Brazilian format number
 */
function smartString(value, formatProvider) {


    if ((formatProvider.indexOf("yy") > -1) && (formatProvider.indexOf("MM") > -1) && (formatProvider.indexOf("dd") > -1)) {
        stringFinal = smartJSFormatDate(value, formatProvider);
        return (stringFinal);
    }

    if ((formatProvider.indexOf("yy") > -1) && (formatProvider.indexOf("MM") > -1) && (formatProvider.indexOf("dd") > -1)) {
        stringFinal = smartJSFormatDate(value, formatProvider);
        return (stringFinal);
    }

    if (formatProvider.match(/D[0-9]*/g)){
        stringFinal = smartJSFormatPreNumber(value, formatProvider);
        return (stringFinal);
    }

    if ((formatProvider == "N") ||
        (formatProvider == "US") ||
        (formatProvider == "BR") ||
        (formatProvider == "EU") ||
        (formatProvider == "JP")){
        if (value.replace(/[0-9]*/g, "").replace(/\./, "").replace(/\,/, "") == "") {
            stringFinal = smartJSFormatCulture(value, formatProvider);
            return (stringFinal);
        }
    }

    if (formatProvider == "C") {
        if (value.replace(/[0-9]*/g, "").replace(/\./, "").replace(/\,/,"") =="") {
            stringFinal = smartJSFormatCulture(value, "");
            if (stringFinal.indexOf(",") > stringFinal.length - 3) {
                stringFinal += "0";
            } else if (stringFinal.indexOf(".") > stringFinal.length - 3) {
                stringFinal += "0";
            }
            return (stringFinal);
        }
    }

    if ((formatProvider.match(/(0*\.)\b\d/g)) || (formatProvider.match(/(0*\,)\b\d/g))) {
        formatProvider = formatProvider.replace(/0*/g, "").replace("/\./", "").replace("/\,/", "");
        if (formatProvider == "") {
            stringFinal = smartJSFormatNumber(value, formatProvider);
            return (stringFinal);
        }
    }

    return (stringFinal);
}

function smartJSFormatDate(date, format) {

    if (date.length > 10) {
        date = date.substring(0, 10);
    }

    if (typeof (date) != "date") {
        date = date.toString();
    }

    if (typeof (date) != "string") {
        alert("'date' property type mismatch!");
        return;
    }

    if (typeof (format) != "string") {
        alert("'format' property type mismatch!");
        return;
    }

    if (date.indexOf("-") == -1 && date.indexOf("/") == -1) {
        alert("Invalid date format!");
        return;
    }

    var tipo = "br";
    var separador = "/";

    if (!isNaN(date.substring(0, 4))) {
        if (date.indexOf("-") != -1) {
            tipo = "us";
            separador = "-";
        } else if (date.indexOf("/") != -1) {
            tipo = "jp";
            separador = "/";
        }
    }

    date = date.replace(/([A-Z a-z]\D*)/g, "");

    if (date.length > 10) {
        alert("Invalid date");
        return;
    }

    if (date.length < 6) {
        alert("Invalid date!");
        return;
    }

    var ano = "";
    var mes = "";
    var dia = "";


    var lstDate = date.split(separador);
    if (tipo == "br") {
        ano = lstDate[2];
        dia = lstDate[0];
    } else {
        ano = lstDate[0];
        dia = lstDate[2];
    }

    if (parseInt(dia) > 31) {
        ano = lstDate[0];
        dia = lstDate[2];
    }

    mes = lstDate[1];

    if (isNaN(dia) || isNaN(mes) || isNaN(ano)) {
        alert("Invalid date!");
        return;
    }

    if (dia.length == 1) {
        dia = "0" + dia;
    }

    if (mes.length == 1) {
        mes = "0" + dia;
    }

    if (ano.length == 2) {
        var ano_atual = new Date().getFullYear().toString().substring(0, 2);
        ano = ano_atual.toString() + ano.toString();
    }

    if (parseInt(dia) > 31 || parseInt(mes) > 12) {
        alert("Invalid date!");
        return (date);
    }

    var dataFinal = format.replace(/yyyy/g, ano).replace(/MM/g, mes).replace(/dd/g, dia);

    if (dataFinal.indexOf("yy") != -1) {
        ano = ano.substring(2, 4);
        dataFinal.replace(/yy/g, ano);
    }

    if (dataFinal.indexOf("d") != -1) {
        if (parseInt(dia) < 10) {
            dia = dia.replace("0", "");
        }
        dataFinal.replace(/d/g, dia);
    }

    if (dataFinal.indexOf("M") != -1) {
        if (parseInt(mes) < 10) {
            mes = mes.replace("0", "");
        }
        dataFinal.replace(/M/g, mes);
    }

    return (dataFinal);
}

function smartJSFormatPreNumber(num, format) {

    var numero_formatado = num.toString();

    var numbers = parseInt(format.replace("D", "")) - num.toString().length;
    if (numbers > 0) {
        var numero_formatado = ("0".repeat(numbers)).toString() + (num.toString()).toString();
    }
    return (numero_formatado);
}

function smartJSFormatCulture(num, format) {

    var numero_formatado = "";

    if (num.toString().indexOf(".") < num.toString().length - 3) {
        num = num.replace(/\./g, "");
    }

    if (num.toString().indexOf(",") < num.toString().length - 3) {
        num = num.replace(/\,/g, "");
    }

    if (format == "BR" || format == "EU") {
        numero_formatado = parseFloat(num).toLocaleString('de-DE', { style: 'currency', currency: 'USA' }).replace("USA", "");

    } else if (format == "US") {
        numero_formatado = parseFloat(num).toLocaleString('en-IN', { style: 'currency', currency: 'USA' }).replace("USA", "");
    } else if (format == "JP") {
        numero_formatado = parseFloat(num).toLocaleString('ja-JP', { style: 'currency', currency: 'USA' }).replace("USA", "");
    } else {
        numero_formatado = parseFloat(num).toLocaleString();
    }
    
    return (numero_formatado);
}

//#endregion

//#endregion

/*===========================================================
 *=                           Date Tools                    =
 *===========================================================
 */
//#region ...

//#region smartStrIsDate

function smartStrIsDate(data) {

    if (data == undefined) {
        return (false);
    }

    var dia = data.substring(0, 2);
    var mes = data.substring(3, 5);
    var ano = data.substring(6, 10);

    var data_ingles = new Date(mes + "/" + dia + "/" + ano);

    if (toString.call(data_ingles) == "[object Date]") {
        if (!isNaN(data_ingles.getTime())) {
            return (true);
        }
    }

    return (false);
}

//#endregion

//#region smartDateAdd
/**
 * Add day, months or years in date.
 * @param {string} date: String with date. Use / or - to separate day, month and year
 * @param {string} add: Value to add in date. Inform value and which time course want add. 
 * Ex: 'day 4' to add four days in date, 'month 4' to add four months in date or 'year 4' to add four years in date. 
 * @param {any} formatDate: Format of date. if youre date is in US 'yyy-mm-dd' or european 'dd-mm-yyyy' or others formats.
 * The result will be return in that "fromatDate". 
 */
function smartDateAdd(date, add, formatDate) {
    if ((formatDate.indexOf("/") == -1) && (formatDate.indexOf("-") == -1)) {
        throw new Error('Format date inválid! Please use / or - to separate d,m and y');
        return;
    }

    var separador = "/";

    if ((formatDate.indexOf("-") != -1)) {
        separador = "-";
    }

    var ordem_data = formatDate.split(separador);
    var data_separada = date.toString().split(separador);

    var data_final = "";

    if (ordem_data[0].indexOf("m") != -1) {
        data_final = data_separada[0];
    } else if (ordem_data[1].indexOf("m") != -1) {
        data_final = data_separada[1];
    } else {
        data_final = data_separada[2];
    }

    if (ordem_data[0].indexOf("d") != -1) {
        data_final += "/" + data_separada[0];
    } else if (ordem_data[1].indexOf("d") != -1) {
        data_final += "/" + data_separada[1];
    } else {
        data_final += "/" + data_separada[2];
    }

    if (ordem_data[0].indexOf("y") != -1) {
        data_final += "/" + data_separada[0];
    } else if (ordem_data[1].indexOf("y") != -1) {
        data_final += "/" + data_separada[1];
    } else {
        data_final += "/" + data_separada[2];
    }

    var result = new Date(data_final);
    if (add.toLowerCase().indexOf("day") != -1) {
        add = add.replace("days", "").trim();
        result.setDate(result.getDate() + parseInt(add));
    } else if (add.toLowerCase().indexOf("month") != -1) {
        add = add.replace("month", "").trim();
        result.setMonth(result.getMonth() + parseInt(add));
    }

    data_final = formatDate.replace("d", smartString(result.getDate(), "D2"));
    data_final = data_final.replace("m", smartString(result.getMonth() + 1, "D2"));
    data_final = data_final.replace("y", result.getFullYear());
    data_final = data_final.replace(/d/g, "").replace(/m/g, "").replace(/y/g, "");

    return data_final;
}

//#endrgion

//#endregion

//#endregion

/*===========================================================
 *=                           Web Tools                     =
 *===========================================================
 */
//#region ...

//#region smartWebIdentifyBrowser()

function smartWebIdentifyBrowser() {

    // Opera 8.0+
    var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
    if (isOpera) {
        return ("opera");
    }

    // Firefox 1.0+
    var isFirefox = typeof InstallTrigger !== 'undefined';
    if (isFirefox) {
        return ("firefox");
    }

    // At least Safari 3+: "[object HTMLElementConstructor]"
    var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
    if (isSafari) {
        return ("safari");
    }

    // Internet Explorer 6-11
    var isIE = /*@cc_on!@*/false || !!document.documentMode;
    if (isIE) {
        return ("ie");
    }

    // Edge 20+
    var isEdge = !isIE && !!window.StyleMedia;
    if (isEdge) {
        return ("edge");
    }

    // Chrome 1+
    var isChrome = !!window.chrome && !!window.chrome.webstore;
    if (isChrome) {
        return ("chrome");
    }

    return ("desconhecido");
    // Blink engine detection
}

//#endregion

//#region smartWebCheckSession()

function smartWebCheckSession() {

    var sessao = false;

    var request = false;
    if (window.XMLHttpRequest) { // Mozilla/Safari
        request = new XMLHttpRequest();
    } else if (window.ActiveXObject) { // IE
        request = new ActiveXObject("Microsoft.XMLHTTP");
    }
    request.open('POST', 'sessao.xml', true);
    request.onreadystatechange = function () {
        if (request.readyState == 4) {
            var session = eval('(' + request.responseText + ')');
            if (session.valid) {
                sessao = true;
            } else {
                sessao = false;
            }
        }
    }
    request.send(null);

    return (sessao);
}

//#endregion

//#region smartWebDetectmob()

function smartWebDetectmob() {
    if (navigator.userAgent.match(/Android/i)
        || navigator.userAgent.match(/webOS/i)
        || navigator.userAgent.match(/iPhone/i)
        || navigator.userAgent.match(/iPad/i)
        || navigator.userAgent.match(/iPod/i)
        || navigator.userAgent.match(/BlackBerry/i)
        || navigator.userAgent.match(/Windows Phone/i)
    ) {
        return true;
    }
    else {
        return false;
    }
}

//#endregion

//#endregion

/*===========================================================
 *=                            Form Tools                   =
 *===========================================================
 */
//#region ...

//#region smart_subExtrairValorInformado

//O valor pode vir de um objeto ou controle em página
function smart_subExtrairValorInformado(valor) {

    var texto = "";

    if (typeof (valor) == "obejct") {

        //#region Se é algum objeto passado pela function

        if (valor.value) {
            texto = valor.value;
        } else if (valor.innerHTML) {
            texto = valor.innerHTML;
        } else {
            return;
        }

        //#endregion

    } else {
        if (document.getElementById(valor)) {

            //#region Se é um controle na página

            if (document.getElementById(valor).value) {
                texto = document.getElementById(valor).value;
            } else {
                texto = document.getElementById(valor).innerHTML;
            }

            //#endregion

        } else {

            //#region Se é apénas um valor
            texto = tipo_value;

            //#endregion
        }
    }

    return(texto);
}

//#endregion

//#region smart_subRetornoCompativel - Se é um valor, objeto ou controle em página

function smart_subRetornoCompativel(valor, tipo_retorno) {

    var texto = "";
    if (valor == false) {
        valor = "";
    }

    if (typeof (tipo_retorno) == "obejct") {

        //#region Se é algum objeto passado pela function

        if (tipo_retorno.value) {
            tipo_retorno.value = valor;
        } else if (tipo_retorno.innerHTML) {
            tipo_retorno.innerHTML = valor;
        } else {
            return;
        }

        //#endregion

    } else {
        if (document.getElementById(tipo_retorno)) {

            //#region Se é um controle na página

            if (document.getElementById(tipo_retorno).value) {
                document.getElementById(tipo_retorno).value = value;
            } else {
                document.getElementById(tipo_retorno).innerHTML = value;
            }

            //#endregion

        } 
    }

    return (value);
}

//#endregion

//#region smartFormOnlyNumbers - Filtra letras
/**
 * Remove all letters and alpha digits. You can use this in html control to supress letters keys.
 * @param {string} text: Text or control to filter
 */
function smartFormOnlyNumbers(text) {

    //Se text é um obejct ou valor
    var texto = smart_subExtrairValorInformado(text);

    texto = texto.replace(/([A-Z a-z]*)?([\!\?\=\+\_\-\(\)\%\@\*\$\&\#]*)/g, "");

    return (smart_subRetornoCompativel(texto, text));
}
//#endregion

//#region smartFormVerifyCPF - Verifica se o cep é válido
/**
 * Verify if CPF(Brazilian citizen register number) number is valid
 * @param {string} cpf:CPF number
 */
function smartFormIsCPF(cpf, msgbox) {

    //Se text é um obejct ou valor
    var texto = smart_subExtrairValorInformado(text);

    var soNumeros = texto.replace(/\D/g, "");
    if (soNumeros == "") { return (true); }

    var multiplicador1 = new Array(10, 9, 8, 7, 6, 5, 4, 3, 2);
    var multiplicador2 = new Array(11, 10, 9, 8, 7, 6, 5, 4, 3, 2);
    var tempCpf;
    var digito;
    var soma;
    var resto;

    cpf = soNumeros;

    if (cpf.length != 11) { return false; }
    tempCpf = cpf.substring(0, 9);

    soma = 0;

    for (var i = 0; i < 9; ++i) {
        soma += parseInt(tempCpf[i].toString()) * multiplicador1[i];
    }

    resto = soma % 11;

    if (resto < 2) {
        resto = 0;
    } else {
        resto = 11 - resto;
    }

    digito = resto.toString();
    tempCpf = tempCpf + digito;
    soma = 0;

    for (var i = 0; i < 10; ++i) {
        soma += parseInt(tempCpf[i].toString()) * multiplicador2[i];
    }

    resto = soma % 11;

    if (resto < 2) {
        resto = 0;
    } else {
        resto = 11 - resto;
    }

    digito = digito + resto.toString();

    if (msgbox !="") {
        if (!cpf.endsWith(digito)) {
            alert("msgbox");
            if (document.getElementById(cpf).value) {
                document.getElementById(cpf).value == "";
            }
        }
    }

    var result = cpf.endsWith(digito);

    if ((!result) && (msgbox != "")) {
        alert(msgbox);
    }

    return (smart_subRetornoCompativel(result));

}
//#endregion

//#region smartFormIsPhone
//Verifica se o telefone está em formato válido
/**
 * Verify if is a phone number
 * @param {string} phone: Phone number. With or without formatting
 */
function smartFormIsPhone(phone) {

    var tel = phone.replace(/([A-Z a-z]*)\D/g, "");

    if (tel == "") { return (true); }

    //alert(tel.length + " - " + tel);

    if (tel.length < 10) { return (false); }

    var retorno = false;
    retorno = !isNaN(tel);
    return (retorno);
}
//#endregion

//#region smartFormIsEMail
//Verifica se o Email está escrito certo

function smartFormIsEMail(vEmail) {

    if ((vEmail == "") || (vEmail == "-")) { return (true); }

    if (vEmail.Length < 8) { return (false); }

    //alert(vEmail.substring(vEmail.length - 4, vEmail.length - 3));

    if (vEmail.indexOf(".") == -1) { return (false); }

    if (vEmail.indexOf("@") == -1) { return (false); }

    var email_dividido = vEmail.split('@');
    var dominio = email_dividido[1].split('.');

    if (
        (dominio[0] == "gmai") ||
        (dominio[0] == "gemail") ||
        (dominio[0] == "gmaio") ||
        (dominio[0] == "mail") ||
        (dominio[0] == "hmail") ||
        (dominio[0] == "gmaiu") ||
        (dominio[0] == "gemeio") ||
        (dominio[0] == "gmeio") ||
        (dominio[0] == "gmeil")
    ) {
        alert("Atenção: Se esse e-mail é um GMail do Google?\nSe for não está escrito corretamente. O correto após o @ é @gmail.com");
    }

    if (vEmail.indexOf("gmail.com.") > -1) {
        alert("Atenção: E-Mails GMail do Google são .com somente e não utilizam .br ou outras terminações de país.");
    }

    if ((dominio[0] == "gmail.") && (vEmail.indexOf(".com") == -1)) {
        alert("Atenção: E-Mails GMail do Google são .com somente e não utilizam .br ou outras terminações de país.");
    }

    if (
        (dominio[0] == "hotmaiu") ||
        (dominio[0] == "rotmail") ||
        (dominio[0] == "hotmeio") ||
        (dominio[0] == "htomail") ||
        (dominio[0] == "otmail") ||
        (dominio[0] == "hotmai") ||
        (dominio[0] == "hotmei") ||
        (dominio[0] == "rotmei") ||
        (dominio[0] == "rotmeail")
    ) {
        alert("Atenção: Se esse e-mail é um Hotmail da Microsoft?\nSe for não está escrito corretamente. O correto após o @ é @hotmail.com ou hotmail.com.br");
    }

    return (true);
}
//#endregion

//#region Valida CEP
//Verifica se o CEP está em formato corretor
function validaCEP(vCep) {
    //var vCep = document.getElementById(txtCEP);

    if ((vCep == "") || (vCep.replace("_", "", "gi").replace("-", "", "gi") == "")) { return (true); }

    if (vCep.length < 8) { return (false); }
    vCep = vCep.replace("-", "", "gi");
    var numero = 0;
    return (!isNaN(vCep));
}
//#endregion

//#region Valida CNPJ
//Verifica se o CNPJ está em formato correto
function validaCNPJ(CNPJ) {

    //vCNPJ = document.getElementById(txtCnpj);

    CNPJ = CNPJ.replace(/\D/g, "");
    //vCNPJ = vCNPJ.replace(",", ".", "gi");
    //if ((vCNPJ == "") || (vCNPJ.replace("_", "", "gi").replace("/", "", "gi").replace("-", "", "gi").replace(".","", "gi") == "")) { return (true); }
    //var CNPJ = vCNPJ.replace(".", "", "gi");
    //CNPJ = CNPJ.replace("/", "", "gi");
    //CNPJ = CNPJ.replace("-", "", "gi");
    if (CNPJ.length != 14) { return (false); }

    var digitos = new Array;
    var soma = new Array;
    var resultado = new Array;

    var nrDig;
    var ftmt;
    var CNPJOk = new Array;

    ftmt = "6543298765432";

    //digitos = new int[14];
    //soma = new int[2];

    soma[0] = 0;
    soma[1] = 0;

    //resultado = new int[2];

    resultado[0] = 0;
    resultado[1] = 0;

    //CNPJOk = new bool[2];

    CNPJOk[0] = false;
    CNPJOk[1] = false;

    try {
        for (var nrDig = 0; nrDig < 14; ++nrDig) {
            digitos[nrDig] = parseInt(

                CNPJ.substring(nrDig, 1));

            if (nrDig <= 11)

                soma[0] += (digitos[nrDig] *

                    parseInt(ftmt.substring(

                        nrDig + 1, 1)));

            if (nrDig <= 12)

                soma[1] += (digitos[nrDig] *

                    parseInt(ftmt.substring(

                        nrDig, 1)));

        }



        for (var nrDig = 0; nrDig < 2; ++nrDig) {

            resultado[nrDig] = (soma[nrDig] % 11);

            if ((resultado[nrDig] == 0) || (

                resultado[nrDig] == 1))

                CNPJOk[nrDig] = (

                    digitos[12 + nrDig] == 0);

            else

                CNPJOk[nrDig] = (

                    digitos[12 + nrDig] == (

                        11 - resultado[nrDig]));

        }

        //alert(CNPJOk[0] && CNPJOk[1]);

        return (CNPJOk[0] && CNPJOk[1]);

    }

    catch (err) {

        return false;

    }

}
//#endregion

//#region smartFormStoreData

function smartFormStoreData(query_selector) {

    var div = document.querySelectorAll(query_selector);
    var storage = localStorage;


    for (var i = 0; i < div.length; i++) {

        if (div[i].type == "text" || div[i].type == "hidden" || div[i].tagName == "TEXTAREA") {
            if (div[i].value != "") {
                storage.setItem(div[i].id, div[i].value);
            }
        } else if (div[i].tagName == "DIV") {
            if (div[i].innerHTML != "") {
                storage.setItem(div[i].id, div[i].innerHTML);
            }
        } else if (div[i].type == "select-one") {
            if (div[i].size > 0) {
                if (div[i].innerHTML != "") {
                    storage.setItem(div[i].id, div[i].innerHTML);
                }
            } else {
                if (div[i].value != "") {
                    storage.setItem(div[i].id, div[i].value);
                }
            }
        }
    }
}

//#endregion

//#region smartFormStoreDataById

function smartFormStoreDataById(id_control) {

    var storage = localStorage;
    var div = document.getElementById(id_control);

    if (div.type == "text" || div.type == "hidden" || div[i].tagName == "TEXTAREA") {

        storage.setItem(div.id, div.value);

    } else if (div.tagName == "DIV") {

        storage.setItem(div.id, div.innerHTML);

    } else if (div.type == "select-one") {
        if (div.size) {
            storage.setItem(div.id, div.innerHTML);
        } else {
            storage.setItem(div.id, div.value);
        }
    }
}

//#endregion

//#region smartFormRestoreData

function smartFormRestoreData(query_selector) {

    var div = document.querySelectorAll(query_selector);
    var storage = localStorage;

    for (var i = 0; i < div.length; i++) {

        if (storage.getItem(div[i].id) !== null) {

            if (div[i].type == "text" || div[i].type == "hidden" || div[i].tagName == "TEXTAREA") {

                var value = storage.getItem(div[i].id);

                div[i].value = value;

            } else if (div[i].tagName == "DIV") {

                div[i].innerHTML = storage.getItem(div[i].id);

            } else if (div[i].type == "select-one") {
                if (div[i].size > 0) {
                    div[i].innerHTML = storage.getItem(div[i].id);
                } else {
                    div[i].value = value;
                    if (div[i].value != value) {
                        const option = new Option()
                        option.value = value;
                        option.text = value;
                        div[i].add(option);
                    }
                }
            }
        }

        storage.removeItem(div[i].id);
    }
}

//#endregion

//#region smartFormRestoreDataById

function smartFormRestoreDataById(id_control) {

    var div = document.getElementById(id_control);
    var storage = localStorage;

    for (var i = 0; i < div.length; i++) {

        if (storage.getItem(div.id) !== null) {
        
            if (div.type == "text" || div.type == "hidden" || div[i].tagName == "TEXTAREA") {

                var value = storage.getItem(div.id);

                div.value = value;

            } else if (div.tagName == "DIV") {

                div.innerHTML = storage.getItem(div.id);

            } else if (div.type == "select-one") {
                if (div.size) {
                    div.innerHTML = storage.getItem(div.id);
                } else {
                    div.value = value;
                    if (div.value != value) {
                        const option = new Option()
                        option.value = value;
                        option.text = value;
                        div.add(option);
                    }
                }
            }
        }

        storage.removeItem(div.id);
    }
}

//#endregion

//#region smartFormStoreData_clear

function smartFormStoreData_clear() {
    var storage = localStorage;
    storage.clear();
}

//#endregion

//#region smartFromFormatDate

//Enquanto vai sendo digitado pelo usuário, a data vai sendo formatada para a sintaxe certa
function smartFormsFormatDate(textData, naoPermitir) {

    var txtData = document.getElementById(textData);
    var data = txtData.value;
    var testeNumero = data.replace("/", ""); //data.substring(data.length-1);
    testeNumero = testeNumero.replace("/", "");


    if (isNaN(testeNumero) || (testeNumero.length > 8)) {
        txtData.value = data.substring(0, data.length - 1);
        return;
    }

    if (testeNumero.length == 1) {
        if (testeNumero > 3) { txtData.value = ""; }
    }

    if (testeNumero.length == 2) {
        if (testeNumero > 31) { txtData.value = testeNumero.substring(0, 1); } else { txtData.value = testeNumero; }
    }

    if (testeNumero.length == 3) {
        if (testeNumero.substring(2, testeNumero.length) > 1) { txtData.value = data.substring(0, data.length - 1); }
        else { txtData.value = testeNumero.substring(0, 2) + "/" + testeNumero.substring(2, testeNumero.length); }
    }

    if (testeNumero.length == 4) {
        if (testeNumero.substring(2, testeNumero.length) > 12) { txtData.value = data.substring(0, data.length - 1); }
        else { txtData.value = testeNumero.substring(0, 2) + "/" + testeNumero.substring(2, testeNumero.length); }
    }

    if (testeNumero.length == 5) {
        txtData.value = testeNumero.substring(0, 2) + "/" + testeNumero.substring(2, testeNumero.length - 1) + "/" + testeNumero.substring(4, testeNumero.length);
    }

    if (testeNumero.length > 7) {
        if (naoPermitir == 'MenorQueHoje') {

            dataStringMarcada = txtData.value.split('/');
            var dataModificada = dataStringMarcada[2] + '-' + dataStringMarcada[1] + '-' + (parseInt(dataStringMarcada[0]) + 1).toString();

            var dataEspecificada = new Date(dataModificada);
            var dataHoje = new Date();
            //alert(dataEspecificada + " - " + dataHoje);
            if (dataEspecificada < dataHoje) {
                alert("A data " + txtData.value + " está vencida! Confira e informe uma data atual ou futura.");
                txtData.value = "";
                return;
            }
        } else if (naoPermitir == 'MaiorQueHoje') {
            dataStringMarcada = txtData.value.split('/');
            var dataModificada = dataStringMarcada[2] + '-' + dataStringMarcada[1] + '-' + (parseInt(dataStringMarcada[0]) + 1).toString();

            var dataEspecificada = new Date(dataModificada);
            var dataHoje = new Date();
            //alert(dataEspecificada + " - " + dataHoje);
            if (dataEspecificada > dataHoje) {
                alert("Data " + txtData.value + " inválida! Confira e informe uma data atual ou passada.");
                txtData.value = "";
                return;
            }
        }
    }

    function validaData(txtData) {
        var txtData = document.getElementById(textData);
        var data = txtData.value;
        var testeNumero = data.replace("/", "");
        testeNumero = testeNumero.replace("/", "");


        if ((isNaN(testeNumero)) || (testeNumero.length != 8)) {
            alert("Data inválida!");
        }
    }
}
//#endregion

//#region smartFormFormatHour

function smartFormFormatHour(textHour, format) {

    var txtHora = document.getElementById(textHour);
    var digitado = txtHora.value;

    //var ultimoChar = digitado.substring(digitado.length - 2, 1);

    digitado = digitado.replace(/([A-Z a-z]\D*)/g, "").replace(/\:/g, "").replace(/\./g, "").replace(/\-/g, "");

    if (isNaN(digitado)) {
        alert("Formato de hora inválido!");
        return;
    }

    if (parseInt(digitado.substring(0, 1)) > 2 && format.indexOf("hh") != -1) {
        digitado = "0" + digitado;
    }

    //if (digitado.length ==1) {
    //    digitado = "0" + digitado;
    //}


    var horas = "";
    var minutos = "";
    var segundos = "";

    if (digitado.length > 5) {

        horas = digitado.substring(0, 2);
        minutos = digitado.substring(2, 4);
        segundos = digitado.substring(4, 6);

    } else if (digitado.length > 4) {

        horas = digitado.substring(0, 2);
        minutos = digitado.substring(2, 4);
        segundos = digitado.substring(4, 5);
        if (parseInt(segundos) > 5) {
            segundos = "0" + segundos;
        }

    } else if (digitado.length > 3) {

        horas = digitado.substring(0, 2);
        minutos = digitado.substring(2, 4);

    } else if (digitado.length > 2) {
        horas = digitado.substring(0, 2);
        minutos = digitado.substring(2, 3);
        if (parseInt(minutos) > 5) {
            minutos = "0" + minutos;
        }

    } else {
        txtHora.value = digitado;
        return;
    }

    if (parseInt(horas) > 23) {
        horas = "";
    }

    if (parseInt(minutos) > 59) {
        minutos = "";
    }

    if (parseInt(segundos) > 59) {
        segundos = "";
    }

    digitado = format.replace(/hh/g, horas).replace(/mm/g, minutos).replace(/ss/g, segundos);

    if (digitado.length > format.length) {
        digitado = digitado.substring(0, format.length);
    }

    txtHora.value = digitado;
    return;

}

//#endregion

//#region smartFormFormatFone

//Enquanto vai sendo digitado, o telefone vai sendo formatado para a sintaxe certa
function smartFormFormatFone(textFone) {
    var txtFone = document.getElementById(textFone);
    var fone = txtFone.value;
    var testeNumero = fone.replace("(", "").replace(")", "");
    testeNumero = testeNumero.replace("-", "").replace(" ", "").trim();


    if ((isNaN(testeNumero)) || (testeNumero.length > 11)) {
        txtFone.value = fone.substring(0, fone.length - 1);
        return;
    }

    if (testeNumero.length == 1) {
        txtFone.value = "(" + testeNumero;
    }

    if (testeNumero.length == 2) {
        txtFone.value = "(" + testeNumero + ") ";
    }

    if (testeNumero.length == 3) {
        txtFone.value = "(" + testeNumero.substring(0, 2) + ") " + testeNumero.substring(2, testeNumero.length);
    }

    if (testeNumero.length == 6) {
        txtFone.value = "(" + testeNumero.substring(0, 2) + ") " + testeNumero.substring(2, testeNumero.length) + "-";
    }

    if ((testeNumero.length > 6) && (testeNumero.length < 11)) {
        txtFone.value = "(" + testeNumero.substring(0, 2) + ") " + testeNumero.substring(2, 6) + "-" + testeNumero.substring(6);
    }

    if (testeNumero.length == 11) {
        txtFone.value = "(" + testeNumero.substring(0, 2) + ") " + testeNumero.substring(2, 7) + "-" + testeNumero.substring(7);
    }
}
//#endregion

//#region smartFormFormatCPF_CNPJ

//Enquanto vai sendo digitado, o CPF vai sendo formatado para a sintaxe certa
function smartFormFormatCPF_CNPJ(textCPF) {

    //    var tipoPessoa = "";
    var txtCPF = document.getElementById(textCPF);
    var cpf = txtCPF.value;

    //    if (cpf.length < 20) {
    //        tipoPessoa="fisica"
    //    }

    cpf = cpf.replace(/[a-z]|[A-Z][.-\/]/g, "");
    txtCPF.value = cpf;

    var testeNumero = cpf.replace(/[^ 0-9]\g/, "").trim();
    testeNumero = testeNumero.replace(/[.,(/)|-\s]/g, "");

    if (testeNumero.length < 12) {

        //if (testeNumero.length > 11) {
        //    txtCPF.value = cpf.substring(1, cpf.length - 1);
        //    return;
        //}

        if (testeNumero.length == 3) {
            txtCPF.value = testeNumero.substring(0, 3) + ".";
        }

        if (testeNumero.length == 6) {
            txtCPF.value = testeNumero.substring(0, 3) + "." + testeNumero.substring(3) + ".";
        }

        if (testeNumero.length == 9) {
            txtCPF.value = testeNumero.substring(0, 3) + "." + testeNumero.substring(3, 6) + "." + testeNumero.substring(6) + "-";
        }

        if (testeNumero.length == 11) {
            txtCPF.value = testeNumero.substring(0, 3) + "." + testeNumero.substring(3, 6) + "." + testeNumero.substring(6, 9) + "-" + testeNumero.substring(9, 11);
        }
    } else {

        if ((isNaN(testeNumero)) || (testeNumero.length > 14)) {
            txtCPF.value = cpf.substring(0, cpf.length - 1);
            return;
        }

        if (testeNumero.length == 2) {
            txtCPF.value = testeNumero.substring(0, 2) + ".";
        }

        if (testeNumero.length == 5) {
            txtCPF.value = testeNumero.substring(0, 2) + "." + testeNumero.substring(2, 5) + ".";
        }

        if (testeNumero.length == 8) {
            txtCPF.value = testeNumero.substring(0, 2) + "." + testeNumero.substring(2, 5) + "." + testeNumero.substring(5, 8) + "/";
        }

        if (testeNumero.length == 12) {
            txtCPF.value = testeNumero.substring(0, 2) + "." + testeNumero.substring(2, 5) + "." + testeNumero.substring(5, 8) + "/" + testeNumero.substring(8, 12) + "-";
        }

        if (testeNumero.length == 14) {
            txtCPF.value = testeNumero.substring(0, 2) + "." + testeNumero.substring(2, 5) + "." + testeNumero.substring(5, 8) + "/" + testeNumero.substring(8, 12) + "-" + testeNumero.substring(12, 14);
        }

    }
}


//#endregion

//#region toTitleCase - Convert value do controle ou texto em Title text
/**
 * Converte texto em Title Case
 * @param {string} value - Texto para ser convertido.
 */
function toTitleCase(value) {

    if (value == null) { return (""); }

    if (typeof (value) == "object") {
        frase = value.value;
    } else {
        frase = value;
    }

    if (frase.Length < 3) { return (frase); }

    frase = frase.toLowerCase();
    frase = frase.substring(0, 1).toUpperCase() + frase.substring(1);
    frase = frase.replace(" a", " A", "gi")
        .replace(" b", " B", "gi")
        .replace(" c", " C", "gi")
        .replace(" d", " D", "gi")
        .replace(" e", " E", "gi")
        .replace(" f", " F", "gi")
        .replace(" g", " G", "gi")
        .replace(" h", " H", "gi")
        .replace(" i", " I", "gi")
        .replace(" j", " J", "gi")
        .replace(" k", " K", "gi")
        .replace(" l", " L", "gi")
        .replace(" m", " M", "gi")
        .replace(" n", " N", "gi")
        .replace(" o", " O", "gi")
        .replace(" p", " P", "gi")
        .replace(" q", " Q", "gi")
        .replace(" r", " R", "gi")
        .replace(" s", " S", "gi")
        .replace(" t", " T", "gi")
        .replace(" u", " U", "gi")
        .replace(" v", " V", "gi")
        .replace(" x", " X", "gi")
        .replace(" y", " Y", "gi")
        .replace(" w", " W", "gi")
        .replace(" z", " Z", "gi");
    frase = frase.replace(" De ", " de ", "gi")
        .replace(" Do ", " do ", "gi")
        .replace(" Dos ", " dos ", "gi")
        .replace(" Da ", " da ", "gi")
        .replace(" Das ", " das ", "gi")
        .replace(" E ", " e ", "gi")
        .replace(" A ", " a ", "gi")
        .replace(" O ", " o ", "gi")
        .replace(" Para ", " para ", "gi");

    if (typeof (value) == "object") {
        value.value = frase;
    } else {
        return (frase);
    }
}
//#endregion

//#endregion

/*===========================================================
 *=                            Clipboard                    =
 *===========================================================
 */
//#region ...

//#region smartClipboard_copy - Copia para o clipboard

function smartClipboard_copy(text, show_link, text_whats) {

    var copyText = document.createElement("input");
    document.body.appendChild(copyText);

    copyText.value = text;

    copyText.select();
    copyText.setSelectionRange(0, 99999); /*For mobile devices*/

    document.execCommand("copy");

    copyText.remove();

    //#region Se for para mostrar o link
    if (show_link) {

        if (smartWebDetectmob()) {

            var resp = confirm("O link já foi copiado para memória. Gostaria de compartilha-lo via WhatsApp?");
            if (resp) {
                window.open("whatsapp://send?text=" + text_whats.replace(" ", "%20").replace("\n", "") + text.replace(" ", "%20").replace("\n", ""), "_self");
            }

        } else {

            alert("O seguinte texto foi Copiado para memória:\n\n" + copyText.value);

        }
    }
    //#endregion
}

//#endregion

//#endregion

