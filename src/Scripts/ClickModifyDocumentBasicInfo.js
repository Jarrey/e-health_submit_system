var f = GetFrame("/entity/archives/editArchives.action?");
var d = f.contentDocument;
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

        if (!IsLoadingData(d)) {
            window.clearInterval(i);
            var r = $(d).contents().find('.x-grid3-row:contains("基础信息表")');
            if (r.length > 0) {
                $(r).find('a:contains("修改")').click();
                ClickTab(document, "修改基础信息表");
            }
        }
    }, 1000);
});