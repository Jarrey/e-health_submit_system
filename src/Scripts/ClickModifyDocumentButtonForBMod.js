﻿var f = GetFrame("/entity/basic/prePregnancyService.action");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {

    Enter(d, f, "text", "妻子姓名", "{妻子姓名}");
    ClickButton(d, "搜索");

    var i = setInterval(function() {
        if (!IsLoadingData(d)) {
            window.clearInterval(i);
            if (!CheckDataExist(d, "{妻子姓名}")) {
                window.submitSys.popupMsg('数据 - 妻子姓名: {妻子姓名} 不存在', true, "OpenDocTabForBMod");
                CloseTab("临床医生系统");
                return;
            }

            SelectData(d, f, "{妻子姓名}");
            ClickButton(f, "完善档案");
            ClickTab(document, "完善档案");
        }
    }, 1000);
});