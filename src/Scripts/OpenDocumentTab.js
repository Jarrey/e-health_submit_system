﻿var times = 0;
var i = setInterval(function() {
    times++;
    if (times >= MaxRetryTimes) {
        window.clearInterval(i);
        window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
        CloseAllTabs();
        return;
    }

    CloseAllTabs();
    if (!ClickTab(document, "临床医生系统")) {
        window.clearInterval(i);
        ClickTreeNode(ClickTreeNode(document, "档案管理"), "临床医生系统");
        ClickTab(document, "临床医生系统");
    }
}, 1000);