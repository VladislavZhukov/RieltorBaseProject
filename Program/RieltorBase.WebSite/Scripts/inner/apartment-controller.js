(function () {
    'use strict';

    angular
        .module('appRealtor')
        .controller('appCntrl', appCntrl);

    // Контроллер для страницы объекта недвижимости
    function appCntrl($scope, $http, $location, $routeParams) {

        // Считываем параметр id квартиры
        $scope.appId = $routeParams.appId;

        // Запрашиваем данные об объекте
        $http.get(GET_REALTY_OBJECT + $scope.appId).success(function (data) {
            $scope.app = data;
            console.log(data);
        });

        // Колонки
        $scope.apptsColumns = [
		    {
		        'column': 'Cost',
		        'readableName': 'Цена'
		    },
		    {
		        'column': 'Phone',
		        'readableName': 'Телефон'
		    },
		    {
		        'column': 'FirmName',
		        'readableName': 'Фирма '
		    }
        ];
    }
})();
