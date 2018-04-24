function BlogService($resource, $q) {

    var resource = $resource('', {}, {
        post_items: { method: 'POST', url: '/api/post/items' },
        post_count: { method: 'POST', url: '/api/post/count' },
        post_item: { method: 'POST', url: '/api/post/item' },
        post_create: { method: 'POST', url: '/api/post/create' },
        post_delete: { method: 'POST', url: '/api/post/delete' },
    });

    return {

        post_items: function (request) { var d = $q.defer(); resource.post_items({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_count: function (request) { var d = $q.defer(); resource.post_count({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_item: function (request) { var d = $q.defer(); resource.post_item({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_create: function (request) { var d = $q.defer(); resource.post_create({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        post_delete: function (request) { var d = $q.defer(); resource.post_delete({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; }
    }
}