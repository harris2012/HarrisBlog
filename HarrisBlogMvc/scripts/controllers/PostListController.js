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