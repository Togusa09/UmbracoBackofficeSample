angular.module('umbraco').directive('umbracoProperty',
    function() {
        return {
            restrict: 'E',
            transclude: true,
            replace: true,
            scope: {
                title: '@',
                subtitle: '@'
            },
            template: '<div class="umb-property">' +
                        '<div class="control-group">' +
                                '<label class="control-label white-space-pre-line">{{title}}<br ng-if="subtitle"/><small ng-if="subtitle" style="font-weight:normal">{{subtitle}}</small></label>' +
                                '<div class="controls">'+
                                    '<div class="umb-property-editor" ng-transclude>' +
                                    '</div>' +
                                '</div>' +
                        '</div>' +
                    '</div>'
        };
    });