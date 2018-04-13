function BlogService($resource, $q) {

    var resource = $resource('', {}, {
        get: { method: 'GET', url: '/api/blog' },
        create: { method: 'POST', url: 'api/blog' },
        getById: { method: 'GET', url: '/api/blog/:id' },
        update: { method: 'PUT', url: '/api/blog/:id' }
    });

    return {
        get: function () { var d = $q.defer(); resource.get({}, {}, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        create: function (request) { var d = $q.defer(); resource.create({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        getById: function (id) { var d = $q.defer(); resource.getById({ id: id }, {}, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        update: function (id, request) { var d = $q.defer(); resource.update({ id: id }, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; }
    }
}
function PostListController($scope, BlogService) {

    BlogService.get().then(function (response) {

        $scope.posts = response.posts;
    })
}
function PostNewController($scope, $state, BlogService) {

    $scope.editorOptions = {
        mode: 'gfm',
        styleActiveLine: true,
        lineNumbers: true,
        lineWrapping: true
    };

    $scope.post = {};
    $scope.post.createTime = new Date();

    $scope.openDatePicker = function () {
        $scope.isDatePickerOpen = true;
    }

    $scope.postBodyChanged = function () {

        if (window.searchTime != null) {
            window.clearTimeout(searchTime);
        }
        window.searchTime = window.setTimeout(function () {

            $scope.post.htmlBody = marked($scope.post.body);
            $scope.$apply();
        }, 200)
    }

    $scope.create = function () {

        if ($scope.post.title == null || $scope.post.title.length == 0) {
            alert("填写文章标题");
            return;
        }

        if ($scope.post.ename == null || $scope.post.title.ename == 0) {
            alert("填写文章路径");
            return;
        }

        if ($scope.post.body == null || $scope.post.body.length == 0) {
            alert("请填写正文");
            return;
        }

        var request = { blog: $scope.post, version: 12345 };
        //request.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();

        BlogService.create(request).then(function (response) {

            if (response.status == 1) {
                $state.go('app.post.post-list');
            }
        })
    };
}
function PostEditController($scope, $state, $stateParams, BlogService) {

    $scope.id = $stateParams.id;
    if (!$scope.id) {
        $state.go('app.post.post-list');
    }

    BlogService.getById($scope.id).then(function (response) {

        if (response.status == 404) {
            $state.go('app.post.post-list');
        } else {
            $scope.post = response.blog;
        }
    })

    $scope.editorOptions = {
        mode: 'gfm',
        styleActiveLine: true,
        lineNumbers: true,
        lineWrapping: true
    };

    $scope.postBodyChanged = function () {

        if (window.searchTime != null) {
            window.clearTimeout(searchTime);
        }
        window.searchTime = window.setTimeout(function () {

            $scope.post.htmlBody = marked($scope.post.body);
            $scope.$apply();
        }, 200)
    }

    $scope.create = function () {

        if ($scope.post.title == null || $scope.post.title.length == 0) {
            alert("填写文章标题");
            return;
        }

        if ($scope.post.ename == null || $scope.post.title.ename == 0) {
            alert("填写文章路径");
            return;
        }

        if ($scope.post.body == null || $scope.post.body.length == 0) {
            alert("请填写正文");
            return;
        }

        var request = { blog: $scope.post, version: 67890 };

        BlogService.update($scope.id, request).then(function (result) {

            console.log(result);
            if (result.status == 1) {

            }
        })
    };
}
var route = function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.when('', '/welcome').when('/', '/welcome');

    //基础框架
    $stateProvider.state('app', {
        url: '/',
        views: {
            'header': { templateUrl: '/scripts/views/view_header.html' },
            'menu': { templateUrl: '/scripts/views/view_menu.html' },
            'main-body': { template: '<div ui-view><div>' }
        }
    });

    //欢迎页
    $stateProvider.state('app.welcome', {
        url: 'welcome',
        templateUrl: '/scripts/views/view_welcome.html?v=' + window.version
    });

    $stateProvider.state('app.post', {});

    //文章列表页
    $stateProvider.state('app.post.post-list', {
        url: 'post-list',
        templateUrl: '/scripts/views/view_post_list.html?v=' + window.version,
        controller: PostListController
    });

    //新建文章页
    $stateProvider.state('app.post.post-new', {
        url: 'post-new',
        templateUrl: '/scripts/views/view_post_new.html?v=' + window.version,
        controller: PostNewController
    });

    //编辑文章页
    $stateProvider.state('app.post.post-edit', {
        url: 'post-edit/:id',
        templateUrl: '/scripts/views/view_post_edit.html?v=' + window.version,
        controller: PostEditController
    });
}
var app = angular.module('app', ['ngResource', 'ui.router', 'ui.bootstrap', 'ui.codemirror']);

app.config(route);

app.service('BlogService', ['$resource', '$q', BlogService]);