var f = GetFrame("/entity/archives/labExamCheckItemM_inputdzh1.action?");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    var times = 0;
    var i = setInterval(function() {
        times++;
        if (times >= MaxRetryTimes) {
            window.clearInterval(i);
            window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
            CloseAllTabs();
            return;
        }

        if (!IsLoadingData(d) && ext.isReady) {
            window.clearInterval(i);
            try {
                ClickButton(d, "全选");

                Enter(d, f, "text", "医师签名", "{医生签名}");
                Enter(d, f, "date", "询问日期", "{检查时间}");

                ClickButton(d, "保存");
                var save = setInterval(function() {
                    if ($(d).find('.x-window:contains("成功！是否要关闭当前页面？")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("成功！是否要关闭当前页面？")').find('button:contains("是")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('完成开单丈夫临床检验结果: 丈夫:{丈夫姓名}, 档案编号: {档案编号}', true, "OpenDocTabForHusClinical");
                    }

                    if ($(d).find('.x-window:contains("失败")').length > 0) {
                        window.clearInterval(save);
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('开单丈夫临床检验失败: 丈夫:{丈夫姓名}, 档案编号: {档案编号}', true, "OpenDocTabForHusClinical");
                    }
                }, 1000);

            } catch (e) {
                window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                CloseAllTabs();
            }
        }
    }, 1000);
});