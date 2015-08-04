var f = GetFrame("/entity/exam/labExamReport.action");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {

    Enter(d, f, "text", "女方证件号码", "{妻子证件号码}");
    ClickButton(d, "搜索");

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
            if (!CheckDataExist(d, "{妻子证件号码}")) {
                window.submitSys.popupMsg('数据 - 妻子姓名: {妻子姓名}, 妻子证件号码: {妻子证件号码} 不存在', true, "{Status}");
                CloseAllTabs();
                return;
            }

            SelectData(d, f, "{妻子证件号码}");
            ClickButton(f, "完善检验结果");
            ClickTab(document, "完善检验结果", 1);
        }
    }, 1000);
});