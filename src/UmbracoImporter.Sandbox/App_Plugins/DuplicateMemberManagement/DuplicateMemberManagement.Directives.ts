///
// Used for the date picker
///
duplicateMemberManagementApp.directive('ngDate', function ($timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        transclude: true,
        link: function (scope: any, element: any, attrs: any, ctrl: any) {
            $timeout(function () {
                element.datepicker({
                    dateFormat: 'dd/mm/yy',
                    onSelect: function (date: any) {
                        scope.$apply(function () {
                            ctrl.$setViewValue(date);
                        });
                    },
                    changeMonth: true,
                    changeYear: true,
                    maxDate: 0, //today is max date
                    yearRange: ((new Date().getFullYear() - 100).toString()) + ":" + (new Date().getFullYear().toString())
                });
            });
        }
    };
});

duplicateMemberManagementApp.directive('onEnter', function () {

    var linkFn = function (scope, element, attrs) {
        element.bind("keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.onEnter);
                });
                event.preventDefault();
            }
        });
    };

    return {
        link: linkFn
    };
});