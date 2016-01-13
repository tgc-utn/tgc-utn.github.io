(function(){
    var app = angular.module('tgc',[]);

    app.directive('menu',function(){
       return{
           restrict:'E',
           templateUrl:"./template.html"};
    });

})();