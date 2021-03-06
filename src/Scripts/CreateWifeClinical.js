﻿var f = GetFrame("/entity/basic/labCheckItemW_inputdzh1.action?");
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
                Enter(d, f, "date", "询问日期", Today);
                Enter(d, f, "radio", "血常规（白细胞五分类）", "no");
                //Enter(d, f, "check", "中值细胞", 1);

                ClickButton(d, "保存");
                times = 0;
                var save = setInterval(function() {
                    times++;
                    if (times >= MaxRetryTimes) {
                        window.clearInterval(save);
                        window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
                        CloseAllTabs();
                        return;
                    }

                    if ($(d).find('.x-window:contains("成功！是否要关闭当前页面？")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("成功！是否要关闭当前页面？")').find('button:contains("是")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('完成开单妻子临床检验结果: 妻子:{妻子姓名}, 档案编号: {档案编号}', true, "OpenDocTabForWifeClinical");
                    }

                    if ($(d).find('.x-window:contains("失败")').length > 0) {
                        window.clearInterval(save);
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('开单妻子临床检验失败: 妻子:{妻子姓名}, 档案编号: {档案编号}', true, "OpenDocTabForWifeClinical");
                    }
                }, 1000);

            } catch (e) {
                window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                CloseAllTabs();
            }
        }
    }, 1000);
});