function PostListController($scope, BlogService) {

    function get_post_list_callback(response) {

        if (response.status == 1) {
            $scope.posts = response.posts;
        }
    }

    function delete_post_by_id_callback(response) {

        if (response.status == 1) {
            BlogService.get().then(get_post_list_callback);
        }
    }

    BlogService.get().then(get_post_list_callback)

    $scope.deletePostById = function (id) {

        BlogService.deleteById(id).then(delete_post_by_id_callback);
    }
}