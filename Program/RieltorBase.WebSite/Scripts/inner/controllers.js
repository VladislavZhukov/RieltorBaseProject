'use strict';

var testApp = angular.module('testApp', []);
var API_V1 = '/api/v1/';
var GET_REALTY_OBJECTS = API_V1 + 'realtyobjects';
var GET_REALTY_OBJECT = API_V1 + 'realtyobjects/';

testApp.config(['$routeProvider', function($routeProvide) {
	$routeProvide
		.when('/', {
		    templateUrl: '../../Template/search.html',
		    controller: 'searchCntrl'
		})
		.when('/about', {
		    templateUrl: '../../Template/about.html',
			controller: 'AboutCtrl'
		})
		.when('/contact', {
		    templateUrl: '../../Template/contact.html',
			controller: 'AboutCtrl'
		})
        .when('/app', {
            templateUrl: '../../Template/app.html',
            controller: 'appCntrl'
        })
		.otherwise({
			redirectTo: '/'
		});
}]);

testApp.controller('searchCntrl', searchCntrl);
testApp.controller('appCntrl', appCntrl);
testApp.controller('AboutCtrl', function ($scope, $http, $location) {});
