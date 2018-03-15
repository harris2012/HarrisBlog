function PostsController($scope) {

    $scope.editorOptions = {
        mode: 'gfm',
        styleActiveLine: true,
        lineNumbers: true,
        lineWrapping: true
    };

    $scope.testChanged = function () {

        if (window.searchTime != null) {
            window.clearTimeout(searchTime);
        }
        window.searchTime = window.setTimeout(function () {

            $scope.postBodyHtml = marked($scope.post.body);
            $scope.$apply();
        }, 200)
    }

    $scope.convert = function () {

        $scope.postBodyHtml = marked('# Marked in browser\n\nRendered by **marked**.');
    }

    $scope.create = function () {

        if ($scope.blog.title == null || $scope.blog.title.length == 0) {
            alert("填写文章标题");
            return;
        }

        if ($scope.blog.body == null || $scope.blog.body.length == 0) {
            alert("请填写正文");
            return;
        }

        var request = $scope.blog;
        //request.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();

        BlogService.create(request).then(function (result) {
            console.log(result);
        })
    };
}