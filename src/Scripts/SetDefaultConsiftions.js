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
        var ext = f.contentWindow.Ext;
        window.clearInterval(i);
        
        var today = ext.util.Format.date(new Date(), 'Y-m-d');
        Enter(d, f, "date", "建档起始时间", today.toString());
        Enter(d, f, "date", "建档结束时间", today.toString());
    }
}, 1000);