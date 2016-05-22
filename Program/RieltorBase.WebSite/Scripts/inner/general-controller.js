(function () {
    'use strict';

    var LOCAL_STORAGE_H = "h";
    var h = btoa(":");

    init();
    initHandlers();

    // Стартовая инициилизация
    function init() {
        // Загрузка данных из local storage
        if (isThereLogin()) {
            h = localStorage.getItem(LOCAL_STORAGE_H);
        } else {
            $('#login-block').show();
        }

        // Общие настройки ajax-запросов
        $.ajaxSetup({
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + h);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status === 401) {
                    // Если ошибка авторизации
                    $('#login-label').text(jqXHR.responseText);
                    $('#login-block').show();
                    window.localStorage.clear();
                } else {
                    // В остальных случаях - показываем ошибку
                    var reason = "";
                    try {
                        reason = JSON.parse(jqXHR.responseText).ExceptionMessage;
                    } catch (e) {}
                    alert('Ошибка при обращении к серверу: ' + reason);
                }
            }
        });
    }

    // Инициилизация обработчиков событий
    function initHandlers() {
        $('#btnLogin').on('click', login);
    }

    // Возвращает true, если сохранен логин и пароль
    function isThereLogin() {
        if (localStorage.getItem(LOCAL_STORAGE_H)) {
            return true;
        }
        return false;
    }

    // Клик по кнопке "Логин"
    function login() {
        $('#login-block').hide();

        var username = $('#login').val();
        var password = $('#password').val();
        h = btoa(username + ":" + password);
        localStorage.setItem(LOCAL_STORAGE_H, h);
    }

})();
