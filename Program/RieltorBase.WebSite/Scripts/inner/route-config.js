(function () {
    'use strict';

    angular
        .module('appRealtor', ['ngRoute'])
        .config(config)
        .controller('AboutCtrl', function ($scope, $http, $location) { });

    config.$inject = ['$routeProvider'];

    function config($routeProvide) {
        $routeProvide
		    .when('/', {
		        templateUrl: '../../Template/search.html',
		        controller: 'searchCntrl',
		        controllerAs: 'vm'
		    })
            .when('/app/:appId', {
                templateUrl: '../../Template/apartment.html',
                controller: 'appCntrl',
                controllerAs: 'vm'
            })
		    .when('/about', {
		        templateUrl: '../../Template/about.html',
		        controller: 'AboutCtrl',
		        controllerAs: 'vm'
		    })
		    .when('/contact', {
		        templateUrl: '../../Template/contact.html',
		        controller: 'AboutCtrl',
		        controllerAs: 'vm'
		    })
		    .otherwise({
		        redirectTo: '/'
		    });
    }
})();