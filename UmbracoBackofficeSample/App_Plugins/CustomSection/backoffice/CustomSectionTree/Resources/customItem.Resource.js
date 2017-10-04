angular.module('umbraco.resources').factory('customItemResource',
    function ($q, $http, umbRequestHelper) {
        //the factory object returned
        return {
            getCustomItems: function () {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/api/CustomItem/GetItems"),
                    "Failed to retrieve Items");
            },
            getCustomItemsCsv: function () {
                return $http.get("backoffice/api/CustomItem/GetItemsCsv");
            },
            getCustomItem: function (id) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/api/CustomItem/GetItem/" + id),
                    "Failed to retrieve CustomItem");
            },
            updateCustomItem: function (id, updatedCustomItem) {
                return umbRequestHelper.resourcePromise(
                    $http.get("backoffice/api/Brand/CustomItem/" + id, updatedCustomItem),
                    "Failed to update CustomItem");
            },
        }
    });