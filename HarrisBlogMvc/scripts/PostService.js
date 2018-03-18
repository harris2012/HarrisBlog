function PostService($resource, $q) {

    var resource = $resource('', {}, {
        posts: { method: 'POST', url: '/api/post-list' },
        createPost: {method:'POST',url:'api/create-post'}
    });

    return {
        posts: function () { var d = $q.defer(); resource.posts({}, {}, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        createPost: function (post) { var d = $q.defer(); resource.createPost({}, post, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; }
    }
}