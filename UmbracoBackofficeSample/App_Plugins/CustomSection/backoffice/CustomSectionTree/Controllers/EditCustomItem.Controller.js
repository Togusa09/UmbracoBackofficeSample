angular.module("umbraco")
    .controller("CustomSection.EditCustomItemController", function ($scope, $routeParams, customItemResource) {
        var self = this;
        self.customItem = {};
        self.pageName = "Edit Custom Item";
        var id = $routeParams.id;

        self.isLoginActive = true;
        self.noChange = true;
        self.removePassword = false;
        self.newPassword = false;
        self.message = "";

        self.loading = true;
        customItemResource.getCustomItem(id).then(function (response) {
            self.customItem = response;
            self.loading = false;
            self.pageName = "Edit Custom Item: " + response.Name;
        });

        self.loading = true;
        self.save = function (salesRepForm) {
            customItemResource.updateCustomItem(id, self.customItem).then(function (response) {
                self.message = response;
                self.loading = false;
                salesRepForm.$setPristine();
            });
        };
    });
