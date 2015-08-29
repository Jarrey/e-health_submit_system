var f = GetFrame("/entity/image/bmodResult.action");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function () {

    Enter(d, f, "text", "编号", "{档案编号}");
    ClickButton(d, "搜索");

    var times = 0;
    var i = setInterval(function () {
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
                window.submitSys.popupMsg('数据 - 妻子姓名: {妻子姓名}, 档案编号: {档案编号} 不存在', true, "{Status}");
                CloseTab("影像上报管理");
                return;
            }

            SelectData(d, f, "{档案编号}");
            var r = $(d).contents().find('.x-grid3-row:contains("{档案编号}")');

            if (r.length > 0) {
                if ($(r).find('a:contains("查看")').length > 0) {
                    window.submitSys.popupMsg('妇科B超检验结果 妻子姓名: {妻子姓名}, 档案编号: {档案编号} 已填写完成', true, "{Status}");
                    CloseTab("影像上报管理");
                    return;
                }
                $(r).find('a:contains("填写"), a:contains("完善")').click();
                ClickTab(document, "填写妻子妇科B超检查结果");
            }
        }

    }, 1000);
});