var f = GetFrame("/entity/basic/prePregnancyService.action");
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    ClickButton(f, "新建档案");
    ClickTab(document, "新建档案");
});