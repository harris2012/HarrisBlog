function BlogService($resource, $q) {

    var resource = $resource('', {}, {
        post_items: { method: 'POST', url: '/api/post/items' },
        post_count: { method: 'POST', url: '/api/post/count' },
        post_item: { method: 'POST', url: '/api/post/item' },
        post_create: { method: 'POST', url: '/api/post/create' },
        post_delete: { method: 'POST', url: '/api/post/delete' },
    });

    return {

        post_items: function (request) { var d = $q.defer(); resource.post_items({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_count: function (request) { var d = $q.defer(); resource.post_count({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_item: function (request) { var d = $q.defer(); resource.post_item({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_create: function (request) { var d = $q.defer(); resource.post_create({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_delete: function (request) { var d = $q.defer(); resource.post_delete({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; }
    }
}
function PostListController($scope, BlogService) {

    function post_items_callback(response) {

        if (response.status == 1) {
            $scope.posts = response.posts;
        }
    }

    function post_count_callback(response) {

        if (response.status == 1) {
            $scope.totalCount = response.count;
        }
    }

    //分页
    $scope.pageChanged = function () {

        var request = {};
        request.pageIndex = $scope.currentPage;
        request.pageSize = $scope.pageSize;
        request.keyword = $scope.keyword;
        request.dataStatus = $scope.dataStatus;

        BlogService.post_items(request).then(post_items_callback)
    };

    //重新搜索
    $scope.keywordChanged = $scope.dataStatusChanged = $scope.mappingTypeChanged = function () {

        $scope.currentPage = 1;

        $scope.refresh();
    }

    //刷新
    $scope.refresh = function () {

        {
            var request = {};
            request.pageIndex = $scope.currentPage;
            request.pageSize = $scope.pageSize;
            request.keyword = $scope.keyword;
            request.dataStatus = $scope.dataStatus;

            BlogService.post_items(request).then(post_items_callback)
        }

        {
            var request = {};
            request.keyword = $scope.keyword;
            request.dataStatus = $scope.dataStatus;

            BlogService.post_count(request).then(post_count_callback)
        }
    }

    //删除项
    $scope.deletePostById = function (id) {

        var request = {};
        request.id = id;

        BlogService.post_delete(request).then(post_delete_callback);
    }

    //初始化
    {
        $scope.maxSize = 10;
        $scope.currentPage = 1;
        $scope.pageSize = 5;
        $scope.dataStatus = 0;

        $scope.refresh();
    }

}
function PostNewController($scope, $state, BlogService) {

    $scope.editorOptions = {
        mode: 'gfm',
        styleActiveLine: true,
        lineNumbers: true,
        lineWrapping: true
    };

    $scope.post = {};
    $scope.post.publishTime = new Date();

    $scope.openDatePicker = function () {
        $scope.isDatePickerOpen = true;
    }
    $scope.refreshPublishTime = function () {
        $scope.post.publishTime = new Date();
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

    function post_item_callback(response) {
        if (response.status == 404) {
            $state.go('app.post.post-list');
        } else {
            $scope.post = response.blog;
        }
    }

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

    {
        var request = {};
        request.id = $scope.id;
        BlogService.post_item(request).then(post_item_callback);
    }
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