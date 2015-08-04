var f = GetFrame("/entity/image/bmodResult_watch.action?");
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    var times = 0;
    var i = setInterval(function() {
        times++;
        if (times >= MaxRetryTimes) {
            window.clearInterval(i);
            window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
            CloseAllTabs();
            return;
        }

        if (!IsLoadingData(d) && ext.isReady) {
            window.clearInterval(i);

            // TODO: Enter b-mod result

            CloseTab("查看妻子妇科B超检查结果");
            CloseTab("完善档案");
            CloseTab("临床医生系统");
            window.submitSys.popupMsg('完成妻子妇科B超检查结果: 妻子:{妻子姓名} 的修改', true, "OpenDocTabForBMod");
        }
    }, 1000);
});