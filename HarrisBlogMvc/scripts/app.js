var app = angular.module('app', ['ngResource', 'ui.router', 'ui.bootstrap', 'ui.codemirror']);

app.config(route);

app.service('PostService', ['$resource', '$q', PostService]);