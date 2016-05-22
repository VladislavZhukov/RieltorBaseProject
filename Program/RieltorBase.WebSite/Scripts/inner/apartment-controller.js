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
        $.ajax({
            url: GET_REALTY_OBJECT + $scope.appId,
            success: function (data, textStatus) {
                // Здесь мы получаем данные, отправленные сервером и выводим их на экран.
                $scope.$apply(function () {
                    $scope.app = data;
                });
            },
            dataType: "json"
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
