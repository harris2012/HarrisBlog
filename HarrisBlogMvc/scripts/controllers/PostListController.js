function PostListController($scope, BlogService) {

    BlogService.get().then(function (response) {

        $scope.posts = response.posts;
    })
}