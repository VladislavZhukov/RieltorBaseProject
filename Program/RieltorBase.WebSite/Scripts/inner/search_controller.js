'use strict';

/* Доки:
Справка по методу 'on', если чё: http://jquery.page2page.ru/index.php5/On
Ajax GET - http://jquery.page2page.ru/index.php5/Ajax_%D0%B7%D0%B0%D0%BF%D1%80%D0%BE%D1%81_%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D0%BE%D0%BC_GET

Календарь (bootstrap-datepicker): https://eternicode.github.io/bootstrap-datepicker
*/

// Контроллер для главной страницы поиска
function searchCntrl($scope, $http, $location) {

    // Событие вызывается при нажатии кнопки "Поиск".
    $scope.searchSubmit = function () {
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
              $scope.appts = processAppartments(data);
              console.log('$scope.appts:');
              console.log($scope.appts);
          },
          "json"
        );
    };

    // Вызов разных методов при инициилизации
    initHTMLProperies();
    initHandlers();

    $scope.appts = [];

    //$scope.searchSubmit();
    //$scope.searchSubmit();

    // Для сортировки таблицы
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
	    //$('#searchSubmit').on('click', searchSubmit);   // Вешаем обработчик клика на кнопке "Поиск" ('#searchSubmit')
	    $('.searchSpecificDate').on('click', searchSpecificDateButton);
	    $('#searchDateMin, #searchDateMax').change(searchSpecificDateTextField);
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


