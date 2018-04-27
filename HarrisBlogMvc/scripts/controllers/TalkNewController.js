function TalkNewController($scope, $state, BlogService) {


    function talk_create_callback(response) {

        if (response.status == 1) {
            $state.go('app.talk.talk-list');
        }
    }

    $scope.openDatePicker = function () {
        $scope.isDatePickerOpen = true;
    }

    $scope.create = function () {

        if ($scope.talk.body == null || $scope.talk.body.length == 0) {
            alert("请填写正文");
            return;
        }

        {
            var request = {};
            request.body = $scope.talk.body;
            request.publishTime = $scope.talk.publishTime;

            BlogService.talk_create(request).then(talk_create_callback);
        }
    };

    {
        $scope.talk = {};
        $scope.talk.publishTime = new Date();
    }
}