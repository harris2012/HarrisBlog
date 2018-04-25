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

    $scope.post = {};
    $scope.post.publishTime = new Date();

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

        {
            var request = { };
            request.title = $scope.post.title;
            request.ename = $scope.post.ename;
            request.body = $scope.post.body;
            request.publishTime = $scope.publishTime;

            BlogService.post_create(request).then(post_create_callback);
        }
    };
}