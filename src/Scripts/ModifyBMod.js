var f = GetFrame("entity/image/bmodResult_input.action?");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function () {
    var times = 0;
    var i = setInterval(function () {
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
                if ("{妇科B超结果}" == "0") {
                    Enter(d, f, "text", "妇科B超检查", "正常");
                }
                if ("{妇科B超结果}" == "1") {
                    Enter(d, f, "text", "妇科B超检查", "异常");
                }
                if ("{妇科B超结果}" == "2") {
                    Enter(d, f, "text", "妇科B超检查", "不能确定");
                }

                if ("{妇科B超结果}" != "3") {
                    Enter(d, f, "textarea", "B超描述", "{妇科B超描述}");
                }

                Enter(d, f, "text", "医师签名", "{医生签名}");
                Enter(d, f, "date", "检查日期", Today);
                Enter(d, f, "check", "完成(注意：此项计入工作量统计，一旦选中并保存，则表格不能再修改！)", CompleteFlag);

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
    
                    if ($(d).find('.x-window:contains("成功，是否要关闭当前页面？")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("成功，是否要关闭当前页面？")').find('button:contains("是")').click();
                        CloseTab("填写妻子妇科B超检查结果");
                        CloseTab("影像上报管理");
                        window.submitSys.popupMsg('完成妻子妇科B超检查结果: 妻子:{妻子姓名}, 档案编号: {档案编号} 的修改', true, "OpenDocTabForBMod");
                        if(CompleteFlag == "1") window.submitSys.writeBack("{档案编号}", "FKBC");
                    }
    
                    if ($(d).find('.x-window:contains("提示")').length > 0) {
                        window.clearInterval(save);
                        CloseTab("填写妻子妇科B超检查结果");
                        CloseTab("影像上报管理");
                        window.submitSys.popupMsg('完成妻子妇科B超检查结果失败: 妻子:{妻子姓名}, 档案编号: {档案编号}', true, "OpenDocTabForBMod");
                    }
                }, 1000);
                
                /*window.submitSys.popupMsg('完成妻子妇科B超检查结果: 妻子:{妻子姓名}, 档案编号: {档案编号} 的修改', true, "OpenDocTabForBMod");*/

            } catch (e) {
                window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                CloseAllTabs();
            }
        }
    }, 1000);
});