
//smartSQL | (c) 2021 by goByte Solutions - Fernando Martinelli - Brazil
//Essa biblioteca é parte integrante do projeto "smartSharp" que ainda está em fase pré beta
//de desenvolvimento. 
//Em breve será disponibilizado a 1a versão beta aberta no GitHub e na página goByte Solutions
//Essa biblioteca oferecerá a função de promover uma integração amigável entre o .Net Core WEB API
//e o front-end (JavaScript/Typescript). 
//Principalmente na manipulação de dados, pois oferecerá uma série de automaçãoes para acesso a 
//dados resultando em um objeto container que poderá ser exportado para o t front-end mantendo 
//suas propriedades e eventos criadas no back-end.
//Outro objetivo dessa biblioteca é oferecer funções o mais autosuficiente possível para que atualizações,
// alterações e adições possam ser feitas com a menor necessidade de mexer no código


//#region smartSQLgetDataInPage() - Pega dados nos controles da página marcados por data-"id" e retorna uma string com eles

//- data-form - Group name to identify controls
//- data-field - The data field. Ex:data-field="name".
//If not found data-fields property, so get field name in ID of control removing prefix "txt", "div", "chk", "cmb" or "temp"
//If you want to inform data type, put that together name field in "data-field" property
//Separete field and data type using coma. If it informed, the strings values will be returned between ''
//and date values will be cast to "'yyyy-mm-dd'" format
//- data-filter - To use data in where clausule. Determine criteries to "where" string: "case_sensitive", "exact", "like", like_start', 'like_end'
//Ex: name='John' => data-filter="like" => LOWER(name) LIKE LOWER('%John%')
//- data-format - Format data to informated type: 'titlecase', 'lowercase', 'uppercase', 'currency'


/**
 * Get values of the HTML collection controls in the page. 
 * Search only controls identifyied by id value in property "data-form".
 * @param {string} id:ID group name in data-form property to identify controls
 * @param {string} type_return:Format to return values. The method accepts the following keywords:
 * only_values, sql_insert, sql_update, sql_filter, obejct, data_string, object.
 * @param {any} destino: Variable or object to add values finded in page.
 * If value is empty and "data-empty" is configured in control. The method use value of property
 * to this field. If not find that property, return exeption message identifyind field of the empty value 
 */
function smartSQLgetDataInPage(id, type_return, destino) {

    var controles = document.querySelectorAll(`[data-form="${id}"]`);
    var values = "";
    var fields = "";
    var object = {};

    if (destino != undefined) {
        if (type_return == "object") {
            object = destino;
        } else {
            values = destino;
        }
    } 
 
    for (var i = 0; i < controles.length; i++) {

        let value = "";
        let field = "";
        let dataType = "";

        //#region Campo

        if (controles[i].getAttribute("data-field")) {
            field = controles[i].getAttribute("data-field");
        } else {
            field = controles[i].id.replace("txt", "");
        }

        if (field.indexOf(",") > -1) {
            var data_info = field.split(',');
            field = data_info[0];
            dataType = data_info[1];
        } 

        //#endregion

        //#region Tipo de controle

        switch (controles[i].tagName.toUpperCase()) {
            case "INPUT":
            case "SELECT":
            case "HIDDEN":
            case "TEXTAREA":
                value = controles[i].value;
                break;
            case "DIV":
            case "SPAN":
                value = controles[i].innerHTML;
                break;
            case "CHECKBOX":
                value = controles[i].checked;
                break;
        }

        //#endregion

        //#region Formato de valor

        if (controles[i].getAttribute("data-format")) {
            var formato = controles[i].getAttribute("data-format");

            if (formato.indexOf("titlecase") != -1) {
                value = toTitleCase(value);
            }
            
            if (formato.indexOf("lowercase") != -1) {
                value = value.toLowerCase();
            }
             
            if (formato.indexOf("uppercase") != -1) {
                    value = value.toUpperCase();
            }
        }

        //#endregion

        //#region Tipo da string com retorno

        switch (type_return) {
            case "only_values":
                if (values != "") { values += ","; }
                values += value;
                break;
            case "sql_insert":
                //#region Se o value é vazio

                if (value == "") {
                    if (controles[i].getAttribute("data-empty") != null) {
                        value = controles[i].getAttribute("data-empty");
                    } else {
                        return "!!" + field;
                    }
                }

                //#endregion

                //#region Tipo de dados

                switch (dataType) {
                    case "string":
                        value = "[squote]" + value + "[squote]";
                        break;
                    case "datetime":
                        if (value != "") {
                         value = "[squote]" + smartJSFormatDate(value, "yyyy-MM-dd") + "[squote]";
                        }
                        break;
                }

                //#endregion

                if (fields != "") { fields += ","; }
                fields += field;
                if (values != "") { values += ","; }
                values += value;
                break;
            case "sql_update":

                if (value == "") {
                    if (controles[i].getAttribute("data-empty") != null) {
                        value = controles[i].getAttribute("data-empty");
                    }
                } else {

                    //#region Tipo de dados

                    switch (dataType) {
                        case "string":
                            value = "[squote]" + value + "[squote]";
                            break;
                        case "datetime":
                            if (value != "") {
                                value = "[squote]" + smartJSFormatDate(value, "yyyy-MM-dd") + "[squote]";
                            }
                            break;
                    }

                    //#endregion

                    if (values != "") { values += ","; }
                    values += `${field}=${value}`;
                }
                break;
            case "data_string":

                if (value == "") {
                    if (controles[i].getAttribute("data-empty") != null) {
                        value = controles[i].getAttribute("data-empty");
                    }
                } else {
                    if (values != "") { values += "*"; }
                    values += `${field}=${value}`;
                }
                break;
            case "object":
                if (value == "") {
                    if (controles[i].getAttribute("data-empty") != null) {
                        value = controles[i].getAttribute("data-empty");
                    } else {
                        object["error"] = field;
                    }
                } else {
                    if (values != "") { values += "*"; }
                    object[field]=value;
                }
                break;
            case "sql_filter":

                if (value != "") {
                    if (controles[i].getAttribute("data-filter")) {

                        if (values != "") { values += " AND "; }

                        if (controles[i].getAttribute("data-filter") == "case_sensetive") {
                            values += `LOWER(REPLACE(${field}, [squote] [squote], [squote][squote])) = [squote]${value.toLowerCase().replace(/\s/g, "")}[squote]`;
                        } else if (controles[i].getAttribute("data-filter") == "like") {
                            values += `LOWER(REPLACE(${field}, [squote] [squote], [squote][squote])) LIKE [squote][percent]${value.toLowerCase().replace(/\s/g, "")}[percent][squote]`;
                        } else if (controles[i].getAttribute("data-filter") == "like_start") {
                            values += `LOWER(REPLACE(${field}, [squote] [squote], [squote][squote])) LIKE [squote][percent]${value.toLowerCase().replace(/\s/g, "")}[squote]`;
                        } else if (controles[i].getAttribute("data-filter") == "like_end") {
                            values += `LOWER(REPLACE(${field}, [squote] [squote], [squote][squote])) LIKE [squote]${value.toLowerCase().replace(/\s/g, "")}[percent][squote]`;
                        } else {
                            //#region Tipo de dados

                            switch (dataType) {
                                case "string":
                                    value = "[squote]" + value + "[squote]";
                                    break;
                                case "datetime":
                                    if (value != "") {
                                        value = "[squote]" + smartJSFormatDate(value, "yyyy-MM-dd") + "[squote]";
                                    }
                                    break;
                            }

                            //#endregion
                            values += `${field}=${value}`;
                        }
                    } else {
                        //#region Tipo de dados

                        switch (dataType) {
                            case "string":
                                value = "[squote]" + value + "[squote]";
                                break;
                            case "datetime":
                                if (value != "") {
                                    value = "[squote]" + smartJSFormatDate(value, "yyyy-MM-dd") + "[squote]";
                                }
                                break;
                        }

                         //#endregion
                        values += `${field}=${value}`;
                    }
                }
                break;
        }

        //#endregion
    }

    if (type_return == "object") {
        return object;
    }

    if (fields != "") {
        fields = `(${fields})`;
    }

    if (values != "" && type_return == "sql_insert") {
        values = ` VALUES(${values})`;
    }

    return fields + values;
}

//#endregion

/**
 * Set values to the HTML collection controls in the page.
 * Populate only controls identifyied by id value in property "data-form".
 * @param {string} id:ID group name in data-form property to identify controls
 * @param {any} source: Data String or object with values to populate controls in page.
 */
function smartSQLsetDataInPage(id, source) {

    var data = {};
    if (typeof (source) == "string") {
        data = smartDataStrToObject(source);
    } else {
        data = source;
    }

    var controles = document.querySelectorAll(`[data-form="${id}"]`);

    for (var i = 0; i < controles.length; i++) {

        //#region Nome do campo

        var field = controles[i].getAttribute("data-field");
        var coma_pos = field.indexOf(",");

        if (coma_pos != -1) {
            field = field.substring(0, coma_pos);
            field = field.trim();
        }
        //#endregion

        //#region Popula controle

        if (data[field]) {

            //#region Tipo de controle
            switch (controles[i].tagName.toUpperCase()) {
                case "INPUT":
                case "SELECT":
                case "HIDDEN":
                case "TEXTAREA":
                case "PASSWORD":
                    controles[i].value = data[field];
                    break;
                case "DIV":
                case "SPAN":
                    controles[i].innerHTML = data[field];
                    break;
                case "CHECKBOX":
                    controles[i].checked = data[field];
                    break;
            }

        //#endregion
        }

        //#endregion
    }

}

/**
 * Clear all values to the HTML collection controls in the page.
 * Clear only controls identifyied by id value in property "data-form".
 * @param {string} id:ID group name in data-form property to identify controls
 */
function smartSQLclearDataInPage(id) {

    var controles = document.querySelectorAll(`[data-form="${id}"]`);

    for (var i = 0; i < controles.length; i++) {

            //#region Tipo de controle
            switch (controles[i].tagName.toUpperCase()) {
                case "INPUT":
                case "SELECT":
                case "HIDDEN":
                case "TEXTAREA":
                case "PASSWORD":
                    controles[i].value = "";
                    break;
                case "DIV":
                case "SPAN":
                    controles[i].innerHTML = "";
                    break;
                case "CHECKBOX":
                    controles[i].checked = "";
                    break;
            }

            //#endregion
        }

}