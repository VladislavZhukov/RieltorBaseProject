(function () {
    'use strict';

    angular
        .module('appRealtor', ['ngRoute'])
        .config(config);

    config.$inject = ['$routeProvider'];

    function config($routeProvide) {
        $routeProvide
		    .when('/', {
		        templateUrl: '/Template/search.html',
		        controller: 'searchCntrl',
		        controllerAs: 'vm'
		    })
            .when('/app/:appId', {
                templateUrl: '/Template/apartment.html',
                controller: 'appCntrl',
                controllerAs: 'vm'
            })
		    .when('/cabinet', {
		        templateUrl: '/Template/cabinet.html',
		        controller: 'cabinetCntrl',
		        controllerAs: 'vm'
		    })
		    .when('/app-add', {
		        templateUrl: '/Template/app-add.html',
		        controller: 'appAddCntrl',
		        controllerAs: 'vm'
		    })
		    .otherwise({
		        redirectTo: '/'
		    });
    }
})();