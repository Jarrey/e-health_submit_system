var f = GetFrame("/entity/basic/prePregnancyService.action");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {

    Enter(d, f, "text", "丈夫证件号码", "{丈夫证件号码}");
    Enter(d, f, "text", "妻子证件号码", "{妻子证件号码}");
    ClickButton(d, "搜索");

    var i = setInterval(function() {
        if (!IsLoadingData(d)) {
            window.clearInterval(i);
            if (CheckDataExist(d, "{丈夫证件号码}") && CheckDataExist(d, "{妻子证件号码}")) {
                window.submitSys.popupMsg('数据 - 丈夫姓名: {丈夫姓名}, 丈夫证件号码: {丈夫证件号码}, 妻子姓名: {妻子姓名}, 妻子证件号码: {妻子证件号码} 已存在', true, "OpenDocTabForNew");
                CloseTab("临床医生系统");
                return;
            }

            ClickButton(f, "新建档案");
            ClickTab(document, "新建档案");
        }
    }, 1000);
});