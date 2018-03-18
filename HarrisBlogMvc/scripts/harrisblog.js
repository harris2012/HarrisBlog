function PostService($resource, $q) {

    var resource = $resource('', {}, {
        posts: { method: 'POST', url: '/api/post-list' },
        createPost: {method:'POST',url:'api/create-post'}
    });

    return {
        posts: function () { var d = $q.defer(); resource.posts({}, {}, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        createPost: function (post) { var d = $q.defer(); resource.createPost({}, post, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; }
    }
}
function PostListController($scope, PostService) {

    PostService.posts().then(function (response) {

        $scope.posts = response.posts;
    })
}
function PostNewController($scope, PostService) {

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

        var request = $scope.post;
        //request.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();

        PostService.createPost(request).then(function (result) {
            console.log(result);
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
        templateUrl: '/scripts/views/view_welcome.html'
    });

    $stateProvider.state('app.post', {});

    //文章列表页
    $stateProvider.state('app.post.post-list', {
        url: 'post-list',
        templateUrl: '/scripts/views/view_post_list.html',
        controller: PostListController
    });

    //新建文章页
    $stateProvider.state('app.post.post-new', {
        url: 'post-new',
        templateUrl: '/scripts/views/view_post_new.html',
        controller: PostNewController
    })
}
var app = angular.module('app', ['ngResource', 'ui.router', 'ui.bootstrap', 'ui.codemirror']);

app.config(route);

app.service('PostService', ['$resource', '$q', PostService]);