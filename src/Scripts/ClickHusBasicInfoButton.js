var f = GetFrame("/entity/archives/editArchives.action?");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    var i = setInterval(function() {
        if (!IsLoadingData(d)) {
            window.clearInterval(i);
            var r = $(d).contents().find('.x-grid3-row:contains("丈夫一般情况")');
            var status = $(r).find('td:contains("未")');
            if (status.length == 0) {
                window.submitSys.popupMsg('丈夫一般情况 - 丈夫姓名: {丈夫姓名}, 丈夫证件号码: {丈夫证件号码}  已完成填写', true, "OpenDocTabForHusBasicInfo");
                CloseTab("完善档案");
                CloseTab("临床医生系统");
                return;
            }

            if (r.length > 0) {
                $(r).find('a:contains("创建"), a:contains("修改")').click();
                ClickTab(document, "丈夫一般情况");
            }
        }
    }, 1000);
});