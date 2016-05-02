'use strict';

// Контроллер для страницы объекта недвижимости
function appCntrl($scope, $http, $location) {
    $http.get(GET_REALTY_OBJECT + 13).success(function (data) {
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
