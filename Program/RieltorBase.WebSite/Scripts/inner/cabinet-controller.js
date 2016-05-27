(function () {
    'use strict';

    angular
        .module('appRealtor')
        .controller('cabinetCntrl', cabinetCntrl);

    // Контроллер для страницы личного кабинета
    function cabinetCntrl($scope, $http, $location) {

        $scope.user = localStorage.getItem("ff");

        console.log(window.user);

        // Событие вызывается при нажатии кнопки "Добавить объект недвижимости".
        $scope.addApp = function () {
            $location.path(ADD_APP_REDIRECT);
        };

    }
})();