﻿var f = GetFrame("/entity/exam/editExamResult.action?");
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
            var r = $(d).contents().find('.x-grid3-row:contains("女方首诊临床检验表")');
            var status = $(r).find('td:contains("无操作")');
            if (status.length != 0) {
                window.submitSys.popupMsg('妻子首诊临床检验表 - 妻子姓名: {妻子姓名}, 档案编号: {档案编号}  未开单', true, "OpenDocTabForWifeClinical");
                CloseAllTabs();
                return;
            }

            if (r.length > 0) {
                $(r).find('a:contains("添加"), a:contains("完善")').click();
                ClickTab(document, "女方首诊临床检验表");
            }
        }
    }, 1000);
});