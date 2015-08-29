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
    if (!ClickTab(document, "影像上报管理")) {
        window.clearInterval(i);
        ClickTreeNode(ClickTreeNode(document, "影像系统"), "影像上报管理");
        ClickTab(document, "影像上报管理");
    }
}, 1000);