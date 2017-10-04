angular.module("umbraco")
    .controller("CustomSection.ListCustomItemController", function ($scope, umbSessionStorage, customItemResource, fileDownloadService) {
        var self = this;
        self.pageName = "Find Custom Items";

        self.sortByProperty = 'id';
        self.sortDirection = "ASC";
        self.currentPage = 1;
        self.pageCount = 1;
        self.totalItems = 0;

        self.getDefaulSearchCriteria = function() {
            return {
                searchProperty: ""
            };
        }

        self.searchCriteria = self.getDefaulSearchCriteria();

        var existingSearchCriteria = umbSessionStorage.get('customItemSearchCriteria');
        if (existingSearchCriteria) {
            self.searchCriteria = existingSearchCriteria;
        }

        self.goToPage = function (pageNumber) {
            self.currentPage = pageNumber;
            self.search();
        };
        self.next = function () {
            self.currentPage++;
            self.goToPage(self.currentPage);
        };
        self.back = function () {
            self.currentPage--;
            self.goToPage(self.currentPage);
        };

        self.sortBy = function (val) {
            if (self.sortByProperty === val) {
                self.direction = self.direction === 'ASC' ? 'DESC' : 'ASC';
            } else {
                self.sortByProperty = val;
                self.direction = 'ASC';
            }

            self.search();
        };

        self.search = function () {
            self.loading = true;
            umbSessionStorage.set('customItemSearchCriteria', self.searchCriteria);

            return customItemResource.getCustomItems(self.searchCriteria.searchProperty)
                .then(function (response) {
                    self.searchResults = response.List;
                    self.currentPage = response.PageNumber;
                    self.totalItems = response.TotalItems;
                    if (response.PageCount >= 0) {
                        self.pageCount = response.PageCount;
                    }
                    self.loading = false;
                });
        }

        self.clear = function () {
            self.searchCriteria = self.getDefaulSearchCriteria();
            umbSessionStorage.set('customItemSearchCriteria', self.searchCriteria);
        };

        self.exportCSV = function () {
            self.loading = true;
            var a = document.createElement("a");
            document.body.appendChild(a);
            salesRepResource.getCustomItemsAsCsv(self.searchCriteria.searchProperty)
                .then(function (response) {
                    self.loading = false;
                    fileDownloadService.createDownloadLinkFromHttpResponse(response, a, response.data);
                });
        }

        self.search();
    });
