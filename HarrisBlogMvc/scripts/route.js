var route = function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.when('', '/welcome').when('/', '/welcome');

    //基础框架
    $stateProvider.state('app', {
        url: '/',
        views: {
            'header': { templateUrl: '/scripts/views/view_header.html' },
            'menu': { templateUrl: '/scripts/views/view_menu.html' },
            'main-body': {template:'<div ui-view><div>'}
        }
    });

    //欢迎页
    $stateProvider.state('app.welcome', {
        url: 'welcome',
        templateUrl: '/scripts/views/view_welcome.html'
    });

    //文章列表页
    $stateProvider.state('app.posts', {
        url: 'posts',
        templateUrl: '/scripts/views/view_posts.html',
        controller: PostsController
    });
}