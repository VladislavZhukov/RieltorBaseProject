'use strict';

var API_V1 = '/api/v1/';
var GET_REALTY_OBJECTS = API_V1 + 'realtyobjects/';
var GET_REALTY_OBJECT = API_V1 + 'realtyobjects/';

(function () {
    'use strict';

    var username = "vasya";
    var password = "12345v";

    $.ajaxSetup({
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Basic " + btoa(username + ":" + password));
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert('Ошибка запроса: ' + JSON.parse(jqXHR.responseText).ExceptionMessage);
        }
    });

})();
