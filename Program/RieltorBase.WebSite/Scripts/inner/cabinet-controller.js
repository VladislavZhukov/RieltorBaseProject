(function () {
    'use strict';

    angular
        .module('appRealtor')
        .controller('cabinetCntrl', cabinetCntrl);

    // Контроллер для страницы личного кабинета
    function cabinetCntrl($scope, $http, $location) {

        // загружаем объект user из local Storage
        var user = JSON.parse(localStorage.getItem(LOCAL_STORAGE_USER));
        $scope.user = user;

        // Событие вызывается при нажатии кнопки "Добавить объект недвижимости".
        $scope.addApp = function () {
            $location.path(ADD_APP_REDIRECT);
        };

        // Запрос на получение объектов недвижимости агента
        $.ajax({
            url: GET_REALTY_OBJECTS_BY_AGENT,
            data: { agentId: user.AgentId },
            success: function (data, textStatus) {
                $scope.$apply(function () {
                    $scope.agentApps = data;
                });
            },
            dataType: "json"
        });

    }
})();