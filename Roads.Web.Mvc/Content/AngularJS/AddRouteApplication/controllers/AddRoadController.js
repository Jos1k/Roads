(function () {
    var app = angular.module('AddRoads', []);

    app.controller('AddRoadController', function($scope, $timeout) {

        $scope.TotalRouteElements = 0;
        $scope.SearchingDepth = 0;
        $scope.CityLabel = "";
        $scope.CityNodes = [];

        $scope.Init = function(totalRouteElements, searchingDepth, cityLabel, palceholderOne, palceholderTwo) {
            this.TotalRouteElements = totalRouteElements;
            this.SearchingDepth = searchingDepth;
            this.CityLabel = cityLabel;
            this.CityNodes = [
                {
                    CityId: '',
                    CityName: '',
                    Placeholder: palceholderOne,
                    WithDeleteButton: false,
                    WithAddButton: true,
                    Index: 0
                }, {
                    CityId: '',
                    CityName: '',
                    Placeholder: palceholderTwo,
                    WithDeleteButton: false,
                    WithAddButton: false,
                    Index: 1
                }
            ];
            this.HideErrorMesages();
            $timeout(function() { $scope.UpdateScriptSection(); }, 500);
        }

        $scope.DeleteCityNode = function(id) {

            this.CityNodes.splice(id, 1);
            this.UpdateIndexes(false);
            this.TurnOffAutocomplete();
            $timeout(function() { $scope.UpdateScriptSection(); }, 500);
        }

        $scope.AddCityNode = function(id) {

            this.HideErrorMesages();

            if (this.SearchingDepth == this.TotalRouteElements) {
                $('#ErrorWindowSearchOfDepth').modal({
                    keyboard: false
                });
                return false;
            }

            id++;
            var newCityNode = {
                CityId: '',
                CityName: '',
                Placeholder: '',
                WithDeleteButton: true,
                WithAddButton: true,
                Index: id
            };
            this.CityNodes.splice(id, 0, newCityNode);
            this.UpdateIndexes(true);
            this.TurnOffAutocomplete();
            $timeout(function() { $scope.UpdateScriptSection(); }, 500);
        }

        $scope.UpdateIndexes = function(addElement) {
            for (var i = 0; i < this.CityNodes.length; i++) {
                this.CityNodes[i].Index = i;
            }
            if (this.CityNodes.length != this.TotalRouteElements) {
                if (addElement) {
                    this.TotalRouteElements++;
                } else {
                    this.TotalRouteElements--;
                }
                if (typeof totalRouteElements !== 'undefined') {
                    if (addElement) {
                        totalRouteElements++;
                    } else {
                        totalRouteElements--;
                    }
                }
            }
        }

        $scope.TurnOffAutocomplete = function() {

            for (var i = 0; i < this.CityNodes.length; i++) {
                $("#autocomplete" + i).autocomplete({ source: [] });
            }
        };

        $scope.UpdateScriptSection = function() {
            var result = "";
            for (var i = 0; i < this.CityNodes.length; i++) {
                result = result + this.InitSuggestionEngine(i);
            }
            $('#scriptSection').replaceWith("<div id=\"scriptSection\"><script>" + result + "</script></div>");
        };

        $scope.InitSuggestionEngine = function(id) {
            return "$(document).ready(function () { \n" +
                "\t$('#autocomplete" + id + "').autocomplete({\n" +
                "\t\tserviceUrl: '/api/Ajax/GetSuggestion'," +
                "\t\tappendTo: '#autocompleteAppendTo" + id + "'," +
                "\t\tparamName: 'searchString',\n" +
                "\t\tautoSelectFirst: true,\n" +
                "\t\tonSelect: function(suggestion) {\n" +
                "\t\t\t$('#autocompleteHidden" + id + "').val(suggestion.data);\n" +
                "\t\t}\n" +
                "\t\t});\n" +
                "});\n" +
                "$(document).ready(function() {\n" +
                "\t$('#autocomplete" + id + "').on('onkeypress', function() {\n" +
                "\t\t$('#autocompleteHidden" + id + "').val(\"\");\n" +
                "\t});\n" +
                "});\n";
        };

        $scope.HideErrorMesages = function () {
            $("#equalCitiesError").hide();
            $("#emptyCityError").hide();
        };

    });
})();