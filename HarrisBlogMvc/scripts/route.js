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