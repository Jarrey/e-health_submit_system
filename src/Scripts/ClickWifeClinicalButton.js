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
            var r = $(d).contents().find('.x-grid3-row:contains("妻子临床检验结果")');
            var status = $(r).find('td:contains("开单")');
            if (status.length == 0) {
                window.submitSys.popupMsg('妻子临床检验结果 - 妻子姓名: {妻子姓名}, 档案编号: {档案编号}  已开单', true, "OpenDocTabForWifeClinical");
                CloseTab("完善档案");
                CloseTab("临床医生系统");
                return;
            }

            if (r.length > 0) {
                $(r).find('a:contains("开单")').click();
                ClickTab(document, "开单妻子临床检验结果");
            }
        }
    }, 1000);
});