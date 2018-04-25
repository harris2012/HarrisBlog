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

    //文章
    $stateProvider.state('app.post', {});
    //列表页
    $stateProvider.state('app.post.post-list', { url: 'post-list', templateUrl: '/scripts/views/view_post_list.html?v=' + window.version, controller: PostListController });
    //新建
    $stateProvider.state('app.post.post-new', { url: 'post-new', templateUrl: '/scripts/views/view_post_new.html?v=' + window.version, controller: PostNewController });
    //编辑
    $stateProvider.state('app.post.post-edit', { url: 'post-edit/:id', templateUrl: '/scripts/views/view_post_edit.html?v=' + window.version, controller: PostEditController });

    //说说
    $stateProvider.state('app.talk', {});
    //列表页
    $stateProvider.state('app.talk.post-list', { url: 'talk-list', templateUrl: '/scripts/views/view_talk_list.html?v=' + window.version, controller: TalkListController });
}