function configResource($q, $http, umbRequestHelper) {
    return {
        getConfig: function () {
            return umbRequestHelper.resourcePromise(
                $http.get("/Umbraco/backoffice/LocalizedEditorModels/ConfigApi/Get"),
                "Failed to get configuration");
        }
    };
};
angular.module("umbraco.resources").factory("lemConfigResource", configResource);