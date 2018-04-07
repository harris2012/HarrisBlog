/**
 * js网页雪花效果jquery插件 
 */
(function (window) {

    var Snow = function () {

        this.defaults = {
            minSize: 10, //雪花的最小尺寸
            maxSize: 20, //雪花的最大尺寸
            newOn: 300, //雪花出现的频率 这个数值越小雪花越多
            flakeColor: "#FFFFFF"
        };

        this.run = function () {

            var flake = $('<div id="snowbox" />').css({ 'position': 'fixed', 'top': '-50px', 'z-index': 9999 }).html('&#10052;');
            var documentHeight = $(window).height();
            var documentWidth = $(window).width();

            var startPositionLeft = Math.random() * documentWidth - 100;
            var startOpacity = 0.5 + Math.random();
            var sizeFlake = snow.options.minSize + Math.random() * snow.options.maxSize;
            var endPositionTop = documentHeight - 40;
            var endPositionLeft = startPositionLeft - 100 + Math.random() * 500;
            var durationFall = documentHeight * 10 + Math.random() * 5000;

            flake.clone().appendTo('body').css({
                left: startPositionLeft,
                opacity: startOpacity,
                'font-size': sizeFlake,
                color: snow.options.flakeColor
            }).animate({
                top: endPositionTop,
                left: endPositionLeft,
                opacity: 0.2
            }, durationFall, 'linear', function () {
                $(this).remove()
            });
        }

        this.show = function (options) {

            this.options = $.extend({}, this.defaults, options);

            setInterval(this.run, this.options.newOn);
        }
    };

    window.snow = new Snow();

})(window);