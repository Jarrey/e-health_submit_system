var times = 0;
var i = setInterval(function () {
    times++;
    if (times >= MaxRetryTimes) {
        window.clearInterval(i);
        window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
        return;
    }

    var f = GetFrame("entity/basic/prePregnancyService.action");
    if (f) {
        var d = f.contentDocument;
        window.clearInterval(i);
        
        Enter(d, f, "date", "建档起始时间", Today.toString());
        Enter(d, f, "date", "建档结束时间", Today.toString());
    }
}, 1000);