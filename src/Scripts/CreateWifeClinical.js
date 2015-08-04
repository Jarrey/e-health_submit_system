var f = GetFrame("/entity/basic/labCheckItemW_inputdzh1.action?");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    var i = setInterval(function() {
        if (!IsLoadingData(d) && ext.isReady) {
            window.clearInterval(i);
            try {
                ClickButton(d, "全选");

                Enter(d, f, "text", "医师签名", "{医生签名}");
                Enter(d, f, "date", "询问日期", "{检查时间}");
                Enter(d, f, "radio", "血常规（白细胞五分类）", "no");
                Enter(d, f, "check", "中值细胞", 1);

                ClickButton(d, "保存");
                var save = setInterval(function() {
                    if ($(d).find('.x-window:contains("成功！是否要关闭当前页面？")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("成功！是否要关闭当前页面？")').find('button:contains("是")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('完成开单妻子临床检验结果: 妻子:{妻子姓名}, 妻子证件号码: {妻子证件号码}', true, "OpenDocTabForWifeClinical");
                    }
                }, 1000);

            } catch (e) {
                window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                CloseAllTabs();
            }
        }
    }, 1000);
});