'use strict';

var testApp = angular.module('testApp', []);

testApp.config(['$routeProvider', function($routeProvide) {
	$routeProvide
		.when('/', {
		    templateUrl: '../../Template/home.html',
			controller: 'test1Ctrl'
		})
		.when('/about', {
		    templateUrl: '../../Template/about.html',
			controller: 'AboutCtrl'
		})
		.when('/contact', {
		    templateUrl: '../../Template/contact.html',
			controller: 'AboutCtrl'
		})
		.otherwise({
			redirectTo: '/'
		});
}]);

testApp.controller('test1Ctrl', function($scope, $http, $location) {
	// $http.get('appts/appts.json').success(function(data) {
	// 	$scope.appts = data;
	// });


	// Колонки
	$scope.apptsColumns = [
		{
			'column': 'rooms',
			'readableName': 'Ком.'
		},
		{
			'column': 'address',
			'readableName': 'Адрес'
		},
		{
			'column': 'numberOfFloors',
			'readableName': 'Этажность '
		},
		{
			'column': 'area',
			'readableName': 'Площадь'
		},
		{
			'column': 'price',
			'readableName': 'Цена'
		},
		{
			'column': 'firm',
			'readableName': 'Фирма'
		},
	];

	// Псевдоданные
	$scope.appts = [
		{
			'rooms': 1,
			'address': 'Автозаводской, 1 квартал Революционная 50',
			'numberOfFloors': "5/16",
			'area': "32/17/8 ТАШ/ПАН",
			'price': 'договорная',
			'firm': '61-70-29 Рус. традиции'
		},
		{
			'rooms': 1,
			'address': 'Автозаводской, 1 квартал Революционная 50',
			'numberOfFloors': "11/16",
			'area': "35/17/9 ТАШ/ПАН",
			'price': '1250 тыс. p',
			'firm': '41-69-77 ФЛ 41-69-77'
		},
		{
			'rooms': 1,
			'address': 'Автозаводской, 1 квартал Свердлова 47',
			'numberOfFloors': "4/5",
			'area': "31/17/6 СТМ/ПАН",
			'price': '1300 тыс. p',
			'firm': '77-94-84 ЛЭНД Недвижимость'
		},
		{
			'rooms': 1,
			'address': 'Автозаводской, 1 квартал Ленинский пр-т 36',
			'numberOfFloors': "5/5",
			'area': "32/17/6 СТМ/ПАН",
			'price': '1400 тыс. p',
			'firm': '77-52-00 Волж.проспект'
		},
		{
			'rooms': 3,
			'address': 'Автозаводской, 1 квартал Московский пр-т 23',
			'numberOfFloors': "5/5",
			'area': "33/17/7 СТМ/ПАН",
			'price': '1350 тыс. p',
			'firm': '31-51-31 Фаворит'
		},

	];

	$scope.sortField = undefined;
	$scope.reverse = false;

	$scope.sort = function(fieldName) {
		if ($scope.sortField === fieldName) {
			$scope.reverse = !$scope.reverse;
		} else {
			$scope.sortField = fieldName;
			$scope.reverse = false;
		}
	};
	$scope.isSortUp = function(fieldName) {
		return $scope.sortField === fieldName && !$scope.reverse;
	};
	$scope.isSortDown = function(fieldName) {
		return $scope.sortField === fieldName && $scope.reverse;
	};

	$scope.headerClicked = function(col) {
		return col.column;
	};

});

testApp.controller('AboutCtrl', function ($scope, $http, $location) {
});
