(function () {
    'use strict';

    var LOCAL_STORAGE_LOGIN = "u";
    var LOCAL_STORAGE_PASSWORD = "p";

    var username = "";
    var password = "";

    init();
    initHandlers();

    // Стартовая инициилизация
    function init() {
        // Загрузка данных из local storage
        if (isThereLogin()) {
            username = localStorage.getItem(LOCAL_STORAGE_LOGIN);
            password = localStorage.getItem(LOCAL_STORAGE_PASSWORD);
        } else {
            $('#login-block').show();
        }

        // Общие настройки ajax-запросов
        $.ajaxSetup({
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + btoa(username + ":" + password));
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
                    } catch (e) { }
                    alert('Ошибка запроса: ' + reason);
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
        if (localStorage.getItem(LOCAL_STORAGE_LOGIN) && localStorage.getItem(LOCAL_STORAGE_PASSWORD)) {
            return true;
        }
        return false;
    }

    // Клик по кнопке "Логин"
    function login() {
        $('#login-block').hide();
        username = $('#login').val();
        password = $('#password').val();

        localStorage.setItem(LOCAL_STORAGE_LOGIN, username);
        localStorage.setItem(LOCAL_STORAGE_PASSWORD, password);
    }

})();
