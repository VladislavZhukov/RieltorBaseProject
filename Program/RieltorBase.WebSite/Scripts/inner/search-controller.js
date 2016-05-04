/* Документация по разным frontend-вещам:

Справка по JQuery (можно найти нужную функцию в поиске): http://jquery.page2page.ru/
Календарь (bootstrap-datepicker): https://eternicode.github.io/bootstrap-datepicker
Руководство по стилям для AngularJS: https://github.com/johnpapa/angular-styleguide/blob/master/a1/i18n/ru-RU.md

Видео-уроки по Bootstrap 3: https://www.youtube.com/watch?v=AYkEfr-5b1o&list=PLypd1VrGv7FPokhw3f5pwBQTHsU9T2mBq&index=1
Видео "ngRoute и AngularUI Router — Открытый вебинар по Angular.js": https://youtube.com/watch?v=0lIoZw_uicA


*/

(function () {
    'use strict';

    angular
        .module('appRealtor')
        .controller('searchCntrl', searchCntrl);

    // Контроллер для главной страницы поиска
    function searchCntrl($scope, $http, $location) {
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

            // Печатаем запрос в консоль
            console.log('Сформирован запрос:');
            console.log(searchRequest);

            // Отправляем запрос на сервер
            $.get(
                GET_REALTY_OBJECTS,
                searchRequest,
                function (data, textStatus) {
                    console.log('С сервера пришёл ответ:');
                    console.log(data);

                    // Здесь мы получаем данные, отправленные сервером и выводим их на экран.
                    $scope.$apply(function () {
                        $scope.appts = processAppartments(data);
                    });
                    $('#searchSubmit').prop('disabled', false);
                },
                "json"
            );
        };

        // Вызов разных методов при инициилизации
        initHTMLProperies();
        initHandlers();
        $scope.searchSubmit();

        // Для сортировки таблицы
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

        // Разворачивает (hide\show) дополнительную информацию о квартире
	    function toggleAppartmentAdditionalInfo() {
	        /* Переключение между мобильным и десктопным разрешением сохраняет какие вкладки были развёрнуты.

            В html надо добавить:
            - class="toggle-appartment-additional" data-id="{{ app.id }}"   // Элемент, который будет "тумблером". {{ app.id }} - какой-либо идентификатор для селекторов (можно id квартиры)
            - class="appartment-additional-{{ app.id }}">                   // Блок, который будет сворачиваться-разворачиваться
            */

	        $('.appartment-additional-' + this.getAttribute('data-toggle-id')).toggle();
	    }

        // Клик по одной из кнопок "За последний: день\неделю\месяц"
	    function searchSpecificDateButton(event) {
	        // Считываем элемент на которую нажали
	        var clickedButton = event.currentTarget;
	        var dateRange = event.currentTarget.dataset.item;

	        // Задаём классы кнопок
	        searchSpecificDateTextField();
	        $(clickedButton).addClass("btn-info");
	        $(clickedButton).removeClass("btn-default");

	        // Задаём дату
	        var dateMax = new Date();
	        var dateMin = dateMax;
	        $('#searchDateMax').val(dateMax.toLocaleDateString());
	        // Находим начальную дату
	        if (dateRange === 'week') {
	            dateMin.setDate(dateMin.getDate() - 7);
	        } else if (dateRange === 'month') {
	            dateMin.setMonth(dateMin.getMonth() - 1);
	        }
	        $('#searchDateMin').val(dateMin.toLocaleDateString());
	    }

        // Панель поиска: пользователь изменил значение даты
	    function searchSpecificDateTextField() {
	        // Сбрасываем классы кнопок
	        $('.searchSpecificDate').removeClass("btn-info");
	        $('.searchSpecificDate').addClass("btn-default");
	    }



        // Обрабатывает квартиры
	    function processAppartments(objects) {
	        var res = [];
	        objects.forEach(function (obj, i, arr) {
	            var newObj = {};
	            newObj.id = obj.RealtyObjectId;
	            newObj.rooms = obj.AdditionalAttributes.Комнат;
	            newObj.address = obj.AdditionalAttributes.Улица + ' ' + obj.AdditionalAttributes.Дом;
	            newObj.numberOfFloors = obj.AdditionalAttributes.Этажи;
	            newObj.area = obj.AdditionalAttributes['Площадь[общ / жил / кух]'];
	            newObj.price = obj.Cost;
	            newObj.firm = obj.FirmName;
	            res.push(newObj);
	        });
	        //console.log('~~~~~~~~');
	        //console.log(res);
	        return res;
	    }

        // meta - Колонки
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
		        'readableName': 'Площадь [общ/жил/кух]'
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

    }
})();
