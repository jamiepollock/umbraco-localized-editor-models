function dashboardController ($scope, lemConfigResource) {
    lemConfigResource.getConfig().then(function (response) {
        $scope.config = response;
    });
};

angular.module("umbraco").controller("Our.Umbraco.LocalizedEditorModels.DashboardController", dashboardController);