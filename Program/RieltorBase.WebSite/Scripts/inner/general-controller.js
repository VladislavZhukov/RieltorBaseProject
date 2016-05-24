(function () {
    'use strict';

    var LOCAL_STORAGE_H = "h";
    var LOCAL_STORAGE_U = "u";
    var h = btoa(":");
    var u = "";

    var $userInfoPanel = $('#user-summary');
    var $userInfoName = $('#user-name');

    init();
    initHandlers();

    // Стартовая инициилизация
    function init() {
        // Загрузка данных из local storage
        if (isThereLogin()) {
            h = localStorage.getItem(LOCAL_STORAGE_H);
            u = localStorage.getItem(LOCAL_STORAGE_U);
            showUserSummary();
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
                    logout();
                    $('#login-label').text(jqXHR.responseText);
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
        $('#user-logout').on('click', logout);
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

        // Формируем строку с именем пользователя
        u = "Вы вошли как " + username;
        localStorage.setItem(LOCAL_STORAGE_U, u);

        showUserSummary();
    }

    // Клик по кнопке "Выйти"
    function logout() {
        $userInfoPanel.hide();
        $('#login-label').text("");
        $('#login-block').show();
        window.localStorage.clear();

        h = btoa(":");
        u = "";
    }

    // Показывает панель с краткой информацией о пользователе
    function showUserSummary() {
        $userInfoPanel.show();
        $userInfoName.text(u);
    }

})();
