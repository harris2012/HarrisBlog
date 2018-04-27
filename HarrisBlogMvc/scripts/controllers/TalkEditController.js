function TalkEditController($scope, $state, $stateParams, BlogService) {

    $scope.id = $stateParams.id;
    if (!$scope.id) {
        $state.go('app.talk.talk-list');
    }

    function talk_item_callback(response) {
        if (response.status == 404) {
            $state.go('app.talk.talk-list');
        } else if (response.status == 1) {
            $scope.talk = response.talk;
        }
    }

    function talk_update_callback(response) {

        if (response.status == 1) {
            alert('success');
        }
    }

    $scope.update = function () {

        if ($scope.talk.body == null || $scope.talk.body.length == 0) {
            alert("body");
            return;
        }

        {
            var request = {};
            request.id = $scope.talk.id;
            request.body = $scope.talk.body;

            BlogService.talk_update(request).then(talk_update_callback);
        }
    }

    {
        var request = {};
        request.id = $scope.id;
        BlogService.talk_item(request).then(talk_item_callback);
    }
}