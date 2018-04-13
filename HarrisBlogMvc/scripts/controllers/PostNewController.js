﻿function PostNewController($scope, $state, BlogService) {

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
    $scope.refreshPublishTime = function () {
        $scope.post.publishTime = new Date();
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

        var request = { blog: $scope.post, version: 12345 };
        //request.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();

        BlogService.create(request).then(function (response) {

            if (response.status == 1) {
                $state.go('app.post.post-list');
            }
        })
    };
}