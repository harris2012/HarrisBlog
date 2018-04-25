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
    //$scope.deletePostById = function (id) {

    //    var request = {};
    //    request.id = id;

    //    BlogService.talk_delete(request).then(talk_delete_callback);
    //}

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