var f = GetFrame("/entity/archives/editArchives.action?");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    var i = setInterval(function() {
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