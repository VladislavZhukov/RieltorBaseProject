(function () {
    'use strict';

    angular
        .module('appRealtor')
        .controller('appAddCntrl', appAddCntrl);

    // Контроллер для главной страницы поиска
    function appAddCntrl($scope, $http, $location) {



        /*
        $scope.sortField = undefined;   // Для сортировки таблицы
        $scope.reverse = false;         // Для сортировки таблицы

        // Событие вызывается при нажатии кнопки "Поиск".
        $scope.searchSubmit = function () {
            $('#searchSubmit').prop('disabled', true);

            // Считываем параметры поиска
            var searchRequest = {};
            searchRequest.searchType = $('#searchType').val();
            searchRequest.searchPriceMin = $('#searchPriceMin').val();
            searchRequest.searchPriceMax = $('#searchPriceMax').val();
            searchRequest.searchDateMin = $('#searchDateMin').val();
            searchRequest.searchDateMax = $('#searchDateMax').val();
            searchRequest.searchAddress = $('#searchAddress').val();

            // Отправляем запрос на сервер
            $.ajax({
                url: GET_REALTY_OBJECTS,
                data: searchRequest,
                success: function (data, textStatus) {
                    console.log('С сервера пришёл ответ:');
                    console.log(data);

                    // Здесь мы получаем данные, отправленные сервером и выводим их на экран.
                    $scope.$apply(function () {
                        $scope.appts = processAppartments(data);
                    });
                },
                complete: function () {
                    $('#searchSubmit').prop('disabled', false);     // Разблокируем кнопку
                },
                dataType: "json"
            });
        };

        // Вызов разных методов при инициилизации
        initHTMLProperies();
        initHandlers();
        $scope.searchSubmit();

        // Для сортировки таблицы
        $scope.sort = function (fieldName) {
            if ($scope.sortField === fieldName) {
                $scope.reverse = !$scope.reverse;
            } else {
                $scope.sortField = fieldName;
                $scope.reverse = false;
            }
        };
        $scope.isSortUp = function (fieldName) {
            return $scope.sortField === fieldName && !$scope.reverse;
        };
        $scope.isSortDown = function (fieldName) {
            return $scope.sortField === fieldName && $scope.reverse;
        };
        $scope.headerClicked = function (col) {
            return col.column;
        };


        ///////////////////////
        function initHTMLProperies() {
            // Задаём дату поиска на сегодня
            var date = new Date();
            $('#searchDateMin, #searchDateMax').val(date.toLocaleDateString());

            // Привязка календаря к панели поиска
            $('#searchDateMin, #searchDateMax').datepicker({
                format: "dd.mm.yyyy",
                todayBtn: "linked",
                todayHighlight: true,
                forceParse: false,
                language: "ru"
            });
        }

        function initHandlers() {
            // Панель поиска: кнопки, текстовые поля даты и пр.
            $('.searchSpecificDate').on('click', searchSpecificDateButton);
            $('#searchDateMin, #searchDateMax').change(searchSpecificDateTextField);

            // Разворачивает (hide\show) дополнительную информацию о квартире
            $('#appartments').on('click', '.toggle-appartment-additional', toggleAppartmentAdditionalInfo);
        }


        // Обрабатывает квартиры
        function processAppartments(objects) {

        }

        */

    }
})();