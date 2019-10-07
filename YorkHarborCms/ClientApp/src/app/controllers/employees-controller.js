var app = angular.module('consumeRestApp', ['ngResource']);

app.factory("employees", function($resource) {
    return $resource("http://localhost:9080/api/employees");
});

app.controller("EmployeesController", function($scope, employees) {
    employees.query(function(data) {
        $scope.employees = data;
    }, function(err) {
        console.error("Error occured: ", err);
    });
});

