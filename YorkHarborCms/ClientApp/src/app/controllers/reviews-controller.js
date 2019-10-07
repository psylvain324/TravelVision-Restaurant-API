var app = angular.module('consumeRestApp', ['ngResource']);

app.factory("reviews", function($resource) {
    return $resource("http://localhost:9080/api/reviews");
});

app.controller("ReviewsController", function($scope, reviews) {
    reviews.query(function(data) {
        $scope.reviews = data;
    }, function(err) {
        console.error("Error occured: ", err);
    });
});

