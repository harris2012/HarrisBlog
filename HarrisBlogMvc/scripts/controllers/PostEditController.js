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