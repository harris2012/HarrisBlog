function TalkCategoryDirective() {

    return {
        restrict: 'E',
        template: '<span>{{talkCategory}}</span>',
        replace: true,
        scope: {},
        link: function (scope, element, attrs) {

            switch (attrs.type) {

                case "1":
                    scope.talkCategory = "普通说说";
                    break;
                case "2":
                    scope.talkCategory = "技术宅";
                    break;
                case "3":
                    scope.talkCategory = "我写的诗";
                    break;
                case "4":
                    scope.talkCategory = "摘录";
                    break;
                case "20":
                    scope.talkCategory = "修改重发";
                    break;
                case "5":
                    scope.talkCategory = "待定";
                    break;
                default:
                    scope.talkCategory = "未定义";
                    break;
            }
        }
    }
}