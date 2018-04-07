function BlogService($resource, $q) {

    var resource = $resource('', {}, {
        get: { method: 'GET', url: '/api/blog' },
        create: { method: 'POST', url: 'api/blog' },
        getById: { method: 'GET', url: '/api/blog/:id' },
        update: { method: 'PUT', url: '/api/blog/:id' }
    });

    return {
        get: function () { var d = $q.defer(); resource.get({}, {}, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        create: function (request) { var d = $q.defer(); resource.create({}, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        getById: function (id) { var d = $q.defer(); resource.getById({ id: id }, {}, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; },
        update: function (id, request) { var d = $q.defer(); resource.update({ id: id }, request, function (result) { d.resolve(result); }, function (result) { d.reject(result); }); return d.promise; }
    }
}