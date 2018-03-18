function PostListController($scope, PostService) {

    PostService.posts().then(function (response) {

        $scope.posts = response.posts;
    })
}