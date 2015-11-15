(function () {
    var app = angular.module('FindRoads', []);


    app.controller('FindRoadController', function ($scope, $http, $sce) {

        $scope.Init = function() {
            this.FindResultFlag = false;

            this.HasErrorFlag = false;

            this.OriginCityNodeNameFlag = false;

            this.DestinationCityNodeNameFlag = false;
            
            this.IsRouteValidation = true;

            //$timeout(function() { $scope.UpdateScriptSection(); }, 500);
        }

        $scope.SetContent = function (url) {
            $scope.SershResult = "";
            $('#isRouteValidationPerSearch').prop('checked', $scope.IsRouteValidation);
            var controller = this;

            $http.post(url, { originalCityId: controller.OriginCityNodeId, destinationCityId: controller.DestinationCityNodeId, isRouteValidation: controller.IsRouteValidation }).error(function (msg) {
                controller.SershResult = $sce.trustAsHtml(msg);
            }).success(function (data) {
                controller.SershResult = $sce.trustAsHtml(data);
            });
        }

        $scope.ValidateOriginCityNode = function() {
            this.OriginCityNodeNameFlag = false;

            if (this.OriginCityNodeName == "" || typeof this.OriginCityNodeName === typeof undefined) {
                this.OriginCityNodeNameFlag = true;
            }
        }

        $scope.ValidateDestinationCityNode = function () {
            this.DestinationCityNodeNameFlag = false;

            if (this.DestinationCityNodeName == "" || typeof this.DestinationCityNodeName === typeof undefined) {
                this.DestinationCityNodeNameFlag = true;
            }
        }

        $scope.Validate = function() {
            this.HasErrorFlag = false;

            this.ValidateOriginCityNode();

            this.ValidateDestinationCityNode();

            if (this.DestinationCityNodeId == this.OriginCityNodeId || this.DestinationCityNodeName == this.OriginCityNodeName) {
                this.OriginCityNodeNameFlag = true;
                this.DestinationCityNodeNameFlag = true;
            }

            if (this.DestinationCityNodeNameFlag || this.OriginCityNodeNameFlag) {
                this.HasErrorFlag = true;
                return false;
            }

            return true;
        }

        $scope.Search = function (url) {
            if (this.Validate()) {

                $("#Forhide").hide(1000);

                this.SetContent(url);
            }
        }

    });
})();