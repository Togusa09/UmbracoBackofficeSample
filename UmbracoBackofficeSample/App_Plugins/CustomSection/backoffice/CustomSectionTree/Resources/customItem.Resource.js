angular.module('umbraco.resources').factory('customItemResource',
    function ($q, $http, umbRequestHelper) {
        //the factory object returned
        return {
            getCustomItems: function (request) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/api/CustomItem/GetCustomItems",
                        {
                            params: request
                        }),
                    "Failed to retrieve Items");
            },
            getCustomItem: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/api/CustomItem/GetCustomItem/" + id),
                    "Failed to retrieve CustomItem");
            },
            updateCustomItem: function (id, updatedCustomItem) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/api/Brand/UpdateCustomItem/" + id, updatedCustomItem),
                    "Failed to update CustomItem");
            },
        }
    });