var f = GetFrame("/entity/basic/prePregnancyService.action");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {

    Enter(d, f, "text", "编号", "{档案编号}");
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
            if (!CheckDataExist(d, "{档案编号}")) {
                window.submitSys.popupMsg('数据 - 丈夫姓名: {丈夫姓名}, 档案编号: {档案编号} 不存在', true, "{Status}");
                CloseTab("临床医生系统");
                return;
            }

            SelectData(d, f, "{档案编号}");
            ClickButton(f, "完善档案");
            ClickTab(document, "完善档案");
        }
    }, 1000);
});