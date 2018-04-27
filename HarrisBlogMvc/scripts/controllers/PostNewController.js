function PostNewController($scope, $state, BlogService) {

    function post_create_callback(response) {

        if (response.status == 1) {
            $state.go('app.post.post-list');
        }
    }

    $scope.editorOptions = {
        mode: 'gfm',
        styleActiveLine: true,
        lineNumbers: true,
        lineWrapping: true
    };

    $scope.openDatePicker = function () {
        $scope.isDatePickerOpen = true;
    }

    $scope.postBodyChanged = function () {

        if (window.searchTime != null) {
            window.clearTimeout(searchTime);
        }
        window.searchTime = window.setTimeout(function () {

            $('.md-editor-preview').html(marked($scope.post.body));

            //$scope.$apply();
        }, 200)
    }

    // 与CodeMirror编辑器交互 start
    $scope.md_click_h1 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "# ", 2); }); }//H1
    $scope.md_click_h2 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "## ", 3); }); }//H2
    $scope.md_click_h3 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "### ", 4); }); }//H3
    $scope.md_click_h4 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "### ", 5); }); }//H4
    $scope.md_click_h5 = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "#### ", 6); }); }//H5
    $scope.md_click_b = function () { this.$broadcast('CodeMirror', function (cm) { insertAround(cm, "**", "**"); }); }//加粗
    $scope.md_click_i = function () { this.$broadcast('CodeMirror', function (cm) { insertAround(cm, "*", "*"); }); }//斜体
    $scope.md_click_image = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "![](http://)", 2); }); }//插入图片
    $scope.md_click_link = function () { this.$broadcast('CodeMirror', function (cm) { insertAround(cm, "[", "](http://)"); }); }//插入超链接
    $scope.md_click_blockquote = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "> ", 2); }); }//插入超链接
    $scope.md_click_ul = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "* ", 2); }); }//UL
    $scope.md_click_ol = function () { this.$broadcast('CodeMirror', function (cm) { insertBefore(cm, "1. ", 3); }); }//OL
    $scope.md_click_code = function () { this.$broadcast('CodeMirror', function (cm) { if (cm.getDoc().getSelection().indexOf("\n") > 0) { insertAround(cm, "```\n", "\n```"); } else { insertAround(cm, "`", "`"); } }); }//插入代码

    function insertBefore(cm, insertion, cursorOffset) {
        var doc = cm.getDoc();
        var cursor = doc.getCursor();

        if (doc.somethingSelected()) {
            var selections = doc.listSelections();
            selections.forEach(function (selection) {
                var pos = [selection.head.line, selection.anchor.line].sort();

                for (var i = pos[0]; i <= pos[1]; i++) {
                    doc.replaceRange(insertion, { line: i, ch: 0 });
                }

                doc.setCursor({ line: pos[0], ch: cursorOffset || 0 });
            });
        } else {
            doc.replaceRange(insertion, { line: cursor.line, ch: 0 });
            doc.setCursor({ line: cursor.line, ch: cursorOffset || 0 });
        }
        cm.focus();
    }
    function insertAround(cm, start, end) {
        var doc = cm.getDoc();
        var cursor = doc.getCursor();

        if (doc.somethingSelected()) {
            var selection = doc.getSelection();
            doc.replaceSelection(start + selection + end);
        } else {
            doc.replaceRange(start + end, { line: cursor.line, ch: cursor.ch });
            doc.setCursor({ line: cursor.line, ch: cursor.ch + start.length });
        }
        cm.focus();
    }
    // 与CodeMirror编辑器交互 end

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

        {
            var request = {};
            request.title = $scope.post.title;
            request.ename = $scope.post.ename;
            request.body = $scope.post.body;
            request.publishTime = $scope.publishTime;

            BlogService.post_create(request).then(post_create_callback);
        }
    };

    {
        $scope.post = {};
        $scope.post.publishTime = new Date();
    }
}