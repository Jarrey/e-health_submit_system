var f = GetFrame("/entity/archives/editArchives.action?");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    var i = setInterval(function() {
        if (!IsLoadingData(d)) {
            window.clearInterval(i);
            var r = $(d).contents().find('.x-grid3-row:contains("妻子妇科B超检查结果")');
            if (r.length > 0) {
                $(r).find('a:contains("查看")').click();
                ClickTab(document, "查看妻子妇科B超检查结果");
            }
        }
    }, 1000);
});