var f = GetFrame("/entity/basic/prePregnancyService.action");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {

    Enter(d, f, "text", "丈夫证件号码", "{丈夫证件号码}");
    ClickButton(d, "搜索");

    var i = setInterval(function() {
        if (!IsLoadingData(d)) {
            window.clearInterval(i);
            if (!CheckDataExist(d, "{丈夫证件号码}")) {
                window.submitSys.popupMsg('数据 - 丈夫姓名: {丈夫姓名}, 丈夫证件号码: {丈夫证件号码} 不存在', true, "{Status}");
                CloseTab("临床医生系统");
                return;
            }

            SelectData(d, f, "{丈夫证件号码}");
            ClickButton(f, "完善档案");
            ClickTab(document, "完善档案");
        }
    }, 1000);
});