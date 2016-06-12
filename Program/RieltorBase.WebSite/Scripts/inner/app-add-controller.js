(function () {
    'use strict';

    angular
        .module('appRealtor')
        .controller('appAddCntrl', appAddCntrl);

    // Контроллер для главной страницы поиска
    function appAddCntrl($scope, $http, $location) {
        var btnAddObject = $('#btnAddObject');

        // загружаем объект user из local Storage
        var user = JSON.parse(localStorage.getItem(LOCAL_STORAGE_USER));
        $scope.user = user;



        // Событие вызывается при нажатии кнопки "Поиск".
        $scope.submitForm = function () {
            btnAddObject.prop('disabled', true);

            var ser = $('#addingForm').serialize();
            console.log('serialize');
            console.log(ser);

            // Отправляем запрос на сервер
            $.ajax({
                url: ADD_REALTY_OBJECTS,
                data: ser,
                type: "POST",
                success: function (data, textStatus) {
                    console.log('С сервера пришёл ответ:');
                    console.log(data);

                    // Здесь мы получаем данные
                    $scope.$apply(function () {
                        //$scope.appts = processAppartments(data);
                    });
                },
                complete: function () {
                    btnAddObject.prop('disabled', false);     // Разблокируем кнопку
                },
                dataType: "json"
            });
        };

    }
})();