var times = 0;
var i = setInterval(function() {
    times++;
    if (times >= MaxRetryTimes) {
        window.clearInterval(i);
        window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
        CloseAllTabs();
        return;
    }

    CloseAllTabs();
    if (!ClickTab(document, "完善检验结果")) {
        window.clearInterval(i);
        ClickTreeNode(ClickTreeNode(document, "检验系统"), "完善检验结果");
        ClickTab(document, "完善检验结果");
    }
}, 1000);