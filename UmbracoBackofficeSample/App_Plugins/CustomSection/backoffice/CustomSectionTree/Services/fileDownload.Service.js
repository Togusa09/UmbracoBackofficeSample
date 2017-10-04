//adds the resource to umbraco.resources module:
angular.module('umbraco').factory('fileDownloadService',
    function() {
        //the factory object returned
        return {
            createDownloadLinkFromHttpResponse: function(httpResponse, anchor, data) {
                var fileName = this.getFileNameFromHttpResponse(httpResponse);
                this.createDownloadLink(anchor, data, fileName);
            },

            createDownloadLink: function (anchor, data, fileName) {
                // Wasn't able to get the anchor.click callback working in ie11, so changed behaviour to just download
                if (window.navigator.msSaveOrOpenBlob) {
                    blobObject = new Blob([data]);
                        window.navigator.msSaveOrOpenBlob(blobObject, fileName);
                } else {
                    var file = new Blob([data], { type: 'text/json' });
                    var fileURL = window.URL.createObjectURL(file);
                    anchor.download = fileName;
                    anchor.href = fileURL;
                    anchor.click();
                }
            },

            getFileNameFromHttpResponse: function(httpResponse) {
                var contentDispositionHeader = httpResponse.headers('Content-Disposition');
                var result = contentDispositionHeader.split(';')[1].trim().split('=')[1];
                return result.replace(/"/g, '');
            }
        };
    });