function BlogService($resource, $q) {

    var resource = $resource('', {}, {
        post_items: { method: 'POST', url: '/api/post/items' },
        post_count: { method: 'POST', url: '/api/post/count' },
        post_item: { method: 'POST', url: '/api/post/item' },
        post_create: { method: 'POST', url: '/api/post/create' },
        post_update: { method: 'POST', url: '/api/post/update' },
        post_delete: { method: 'POST', url: '/api/post/delete' },

        talk_items: { method: 'POST', url: '/api/talk/items' },
        talk_count: { method: 'POST', url: '/api/talk/count' },
        talk_create: { method: 'POST', url: '/api/talk/create' },
        talk_item: { method: 'POST', url: '/api/talk/item' },
        talk_update: { method: 'POST', url: '/api/talk/update' },
        talk_delete: { method: 'POST', url: '/api/talk/delete' },
    });

    return {

        post_items: function (request) { var d = $q.defer(); resource.post_items({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_count: function (request) { var d = $q.defer(); resource.post_count({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_item: function (request) { var d = $q.defer(); resource.post_item({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_create: function (request) { var d = $q.defer(); resource.post_create({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_update: function (request) { var d = $q.defer(); resource.post_update({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_delete: function (request) { var d = $q.defer(); resource.post_delete({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },

        talk_items: function (request) { var d = $q.defer(); resource.talk_items({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        talk_count: function (request) { var d = $q.defer(); resource.talk_count({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        talk_create: function (request) { var d = $q.defer(); resource.talk_create({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        talk_item: function (request) { var d = $q.defer(); resource.talk_item({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        talk_update: function (request) { var d = $q.defer(); resource.talk_update({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        talk_delete: function (request) { var d = $q.defer(); resource.talk_delete({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
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
            $scope.totalCount = response.totalCount;
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
        $scope.pageSize = 10;
        $scope.dataStatus = 0;

        $scope.refresh();
    }

}
function PostNewController($scope, $state, BlogService) {

    function post_create_callback(response) {

        if (response.status == 1) {
            $state.go('app.post.post-list');
        }
    }

    $scope.editorOptions = {
        mode: 'gfm',
        styleActiveLine: true,
        lineNumbers: true,
        lineWrapping: true
    };

    $scope.openDatePicker = function () {
        $scope.isDatePickerOpen = true;
    }

    $scope.postBodyChanged = function () {

        if (window.searchTime != null) {
            window.clearTimeout(searchTime);
        }
        window.searchTime = window.setTimeout(function () {

            $('.md-editor-preview').html(marked($scope.post.body));

            //$scope.$apply();
        }, 200)
    }

    // 与CodeMirror编辑器交互 start
    $scope.md_click_h1 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "# ", 2); }); }//H1
    $scope.md_click_h2 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "## ", 3); }); }//H2
    $scope.md_click_h3 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "### ", 4); }); }//H3
    $scope.md_click_h4 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "### ", 5); }); }//H4
    $scope.md_click_h5 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "#### ", 6); }); }//H5
    $scope.md_click_b = function () { this.$broadcast('CodeMirror', function (cm) { insertAround(cm, "**", "**"); }); }//加粗
    $scope.md_click_i = function () { this.$broadcast('CodeMirror', function (cm) { insertAround(cm, "*", "*"); }); }//斜体
    $scope.md_click_image = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "![](http://)", 2); }); }//插入图片
    $scope.md_click_link = function () { this.$broadcast('CodeMirror', function (cm) { insertAround(cm, "[", "](http://)"); }); }//插入超链接
    $scope.md_click_blockquote = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "> ", 2); }); }//插入超链接
    $scope.md_click_ul = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "* ", 2); }); }//UL
    $scope.md_click_ol = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "1. ", 3); }); }//OL
    $scope.md_click_code = function () { this.$broadcast('CodeMirror', function (cm) { if (cm.getDoc().getSelection().indexOf("\n") > 0) { insertAround(cm, "```\n", "\n```"); } else { insertAround(cm, "`", "`"); } }); }//插入代码

    function insertBefore(cm, insertion, cursorOffset) {
        var doc = cm.getDoc();
        var cursor = doc.getCursor();

        if (doc.somethingSelected()) {
            var selections = doc.listSelections();
            selections.forEach(function (selection) {
                var pos = [selection.head.line, selection.anchor.line].sort();

                for (var i = pos[0]; i <= pos[1]; i++) {
                    doc.replaceRange(insertion, { line: i, ch: 0 });
                }

                doc.setCursor({ line: pos[0], ch: cursorOffset || 0 });
            });
        } else {
            doc.replaceRange(insertion, { line: cursor.line, ch: 0 });
            doc.setCursor({ line: cursor.line, ch: cursorOffset || 0 });
        }
        cm.focus();
    }
    function insertAround(cm, start, end) {
        var doc = cm.getDoc();
        var cursor = doc.getCursor();

        if (doc.somethingSelected()) {
            var selection = doc.getSelection();
            doc.replaceSelection(start + selection + end);
        } else {
            doc.replaceRange(start + end, { line: cursor.line, ch: cursor.ch });
            doc.setCursor({ line: cursor.line, ch: cursor.ch + start.length });
        }
        cm.focus();
    }
    // 与CodeMirror编辑器交互 end

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

        {
            var request = {};
            request.title = $scope.post.title;
            request.ename = $scope.post.ename;
            request.body = $scope.post.body;
            request.publishTime = $scope.publishTime;

            BlogService.post_create(request).then(post_create_callback);
        }
    };

    {
        $scope.post = {};
        $scope.post.publishTime = new Date();
    }
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

    function post_update_callback(response) {

        if (response.status == 1) {
            alert('success');
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

        {
            var request = { blog: $scope.post, version: 67890 };
            request.id = $scope.post.id;
            request.title = $scope.post.title;
            request.ename = $scope.post.ename;
            request.body = $scope.post.body;

            BlogService.post_update(request).then(post_update_callback)
        }
    };

    {
        var request = {};
        request.id = $scope.id;
        BlogService.post_item(request).then(post_item_callback);
    }
}
function TalkListController($scope, BlogService) {

    function talk_items_callback(response) {

        if (response.status == 1) {
            $scope.talkList = response.talkList;
        }
    }

    function talk_count_callback(response) {

        if (response.status == 1) {
            $scope.totalCount = response.totalCount;
        }
    }

    function talk_delete_callback(response) {

        if (response.status == 1) {
            swal("成功", "该说说已被成功删除", "success");
            $scope.refresh();
        } else {
            swal("失败", response.message, "error");
        }
    }

    //分页
    $scope.pageChanged = function () {

        var request = {};
        request.pageIndex = $scope.currentPage;
        request.pageSize = $scope.pageSize;
        request.keyword = $scope.keyword;
        request.dataStatus = $scope.dataStatus;
        request.categoryId = $scope.categoryId;

        BlogService.talk_items(request).then(talk_items_callback)
    };

    //重新搜索
    $scope.keywordChanged = $scope.categoryChanged = $scope.dataStatusChanged = function () {

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
            request.categoryId = $scope.categoryId;

            BlogService.talk_items(request).then(talk_items_callback)
        }

        {
            var request = {};
            request.keyword = $scope.keyword;
            request.dataStatus = $scope.dataStatus;
            request.categoryId = $scope.categoryId;

            BlogService.talk_count(request).then(talk_count_callback)
        }
    }

    //删除项
    $scope.deleteById = function (id) {

        swal({ title: "确定要说说吗？", text: "该说说将被删除", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "确定删除！", closeOnConfirm: false }, function () {

            var request = {};
            request.id = id;

            BlogService.talk_delete(request).then(talk_delete_callback);
        });


    }

    //初始化
    {
        $scope.maxSize = 10;
        $scope.currentPage = 1;
        $scope.pageSize = 10;
        $scope.dataStatus = 0;
        $scope.categoryId = 0;

        $scope.refresh();
    }

}
function TalkNewController($scope, $state, BlogService) {


    function talk_create_callback(response) {

        if (response.status == 1) {
            $state.go('app.talk.talk-list');
        }
    }

    $scope.openDatePicker = function () {
        $scope.isDatePickerOpen = true;
    }

    $scope.create = function () {

        if ($scope.talk.body == null || $scope.talk.body.length == 0) {
            alert("请填写正文");
            return;
        }
        if ($scope.talk.location != null && !/^\d+\.\d+,\d+\.\d+$/.test($scope.talk.location)) {

            alert('经纬度格式不正确');
            return;
        }

        {
            var request = {};
            request.body = $scope.talk.body;
            request.location = $scope.talk.location;
            request.locationName = $scope.talk.locationName;
            request.publishTime = $scope.talk.publishTime;

            BlogService.talk_create(request).then(talk_create_callback);
        }
    };

    {
        $scope.talk = {};
        $scope.talk.publishTime = new Date();
    }
}
function TalkEditController($scope, $state, $stateParams, BlogService) {

    $scope.id = $stateParams.id;
    if (!$scope.id) {
        $state.go('app.talk.talk-list');
    }

    function talk_item_callback(response) {
        if (response.status == 404) {
            $state.go('app.talk.talk-list');
        } else if (response.status == 1) {
            $scope.talk = response.talk;
        }
    }

    function talk_update_callback(response) {

        if (response.status == 1) {
            alert('success');
        }
    }

    $scope.update = function () {

        if ($scope.talk.body == null || $scope.talk.body.length == 0) {
            alert("body");
            return;
        }

        {
            var request = {};
            request.id = $scope.talk.id;
            request.body = $scope.talk.body;

            BlogService.talk_update(request).then(talk_update_callback);
        }
    }

    {
        var request = {};
        request.id = $scope.id;
        BlogService.talk_item(request).then(talk_item_callback);
    }
}
function TalkCategoryDirective() {

    return {
        restrict: 'E',
        template: '<span>{{talkCategory}}</span>',
        replace: true,
        scope: {},
        link: function (scope, element, attrs) {

            switch (attrs.type) {

                case "1":
                    scope.talkCategory = "普通说说";
                    break;
                case "2":
                    scope.talkCategory = "技术宅";
                    break;
                case "3":
                    scope.talkCategory = "我写的诗";
                    break;
                case "4":
                    scope.talkCategory = "摘录";
                    break;
                case "20":
                    scope.talkCategory = "修改重发";
                    break;
                case "5":
                    scope.talkCategory = "待定";
                    break;
                default:
                    scope.talkCategory = "未定义";
                    break;
            }
        }
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
    $stateProvider.state('app.talk.talk-list', { url: 'talk-list', templateUrl: '/scripts/views/view_talk_list.html?v=' + window.version, controller: TalkListController });
    //列表页
    $stateProvider.state('app.talk.talk-new', { url: 'talk-new', templateUrl: '/scripts/views/view_talk_new.html?v=' + window.version, controller: TalkNewController });
    //列表页
    $stateProvider.state('app.talk.talk-edit', { url: 'talk-edit/:id', templateUrl: '/scripts/views/view_talk_edit.html?v=' + window.version, controller: TalkEditController });
}
var app = angular.module('app', ['ngResource', 'ui.router', 'ui.bootstrap', 'ui.codemirror']);

app.config(route);

app.service('BlogService', ['$resource', '$q', BlogService]);

app.directive('talkcategory', TalkCategoryDirective);