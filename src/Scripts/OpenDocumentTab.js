var i = setInterval(function() {
    if (!ClickTab(document, "临床医生系统")) {
        window.clearInterval(i);
        ClickTreeNode(ClickTreeNode(document, "档案管理"), "临床医生系统");
        ClickTab(document, "临床医生系统");
    }
}, 1000);