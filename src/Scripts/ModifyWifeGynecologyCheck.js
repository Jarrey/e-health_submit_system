﻿var f = GetFrame("/entity/basic/physicalExamW_physicalExamW.action?");
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
                var t = ext.getCmp($(d).find('.x-tab-panel:contains("女性一般体格检查")')[0].id);
                t.activate(1);
                b = $(d).find('#physical2')[0];

                field = $(d).find(".x-plain-body:contains('阴毛')")[0];
                if ("{阴毛}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "正常"]')[0].id).setValue("正常");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "异常"]')[0].id).setValue("异常");
                    Enter(b, f, "text", "阴毛异常描述", "{阴毛异常}");
                }

                field = $(d).find(".x-plain-body:contains('乳房')")[0];
                if ("{乳房}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "正常"]')[0].id).setValue("正常");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "异常"]')[0].id).setValue("异常");
                    Enter(b, f, "text", "乳房异常描述", "{乳房异常}");
                }

                field = $(d).find(".x-plain-body:contains('外阴')")[0];
                if ("{外阴}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "未见异常"]')[0].id).setValue("未见异常");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "异常"]')[0].id).setValue("异常");
                    Enter(b, f, "text", "外阴异常描述", "{外阴异常}");
                }

                field = $(d).find(".x-plain-body:contains('阴道')")[0];
                if ("{阴道}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "未见异常"]')[0].id).setValue("未见异常");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "异常"]')[0].id).setValue("异常");
                    Enter(b, f, "text", "阴道异常描述", "{阴道异常}");
                }

                field = $(d).find(".x-plain-body:contains('分泌物')")[0];
                if ("{分泌物}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "正常"]')[0].id).setValue("正常");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "异常"]')[0].id).setValue("异常");
                    Enter(b, f, "text", "分泌物异常描述", "{分泌物异常}");
                }

                field = $(d).find(".x-plain-body:contains('宫颈')")[0];
                if ("{宫颈}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "光滑"]')[0].id).setValue("光滑");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "异常"]')[0].id).setValue("异常");
                    Enter(b, f, "text", "宫颈异常描述", "{宫颈异常}");
                }

                field = $(d).find(".x-plain-body:contains('子宫大小')")[0];
                if ("{子宫大小}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "正常"]')[0].id).setValue("正常");
                } else if ("{子宫大小}" == "1") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "大"]')[0].id).setValue("大");
                } else if ("{子宫大小}" == "2") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "小"]')[0].id).setValue("小");
                }

                field = $(d).find(".x-plain-body:contains('子宫活动度')")[0];
                if ("{子宫活动}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "好"]')[0].id).setValue("好");
                } else if ("{子宫活动}" == "1") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "差"]')[0].id).setValue("差");
                }

                field = $(d).find(".x-plain-body:contains('子宫包块')")[0];
                if ("{子宫包块}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "无"]')[0].id).setValue("无");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "有"]')[0].id).setValue("有");
                    Enter(b, f, "text", "包块描述", "{子宫包块异常}");
                }

                field = $(d).find(".x-plain-body:contains('双侧附件')")[0];
                if ("{双侧附件}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "未见异常"]')[0].id).setValue("未见异常");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "异常"]')[0].id).setValue("异常");
                    Enter(b, f, "text", "双侧附件描述", "{附件异常}");
                }

                Enter(b, f, "textarea", "其他检查", "{其他检查}");
                ext.getCmp('done2').setValue(CompleteFlag);
                Enter(b, f, "text", "医师签名", "{医生签名}");
                Enter(b, f, "date", "检查日期", Today);

                ClickButton(b, "保存");
                $(d).find('.x-window:contains("您已确认无误，要提交？")').find('button:contains("是")').click();
                times = 0;
                var save = setInterval(function() {
                    times++;
                    if (times >= MaxRetryTimes) {
                        window.clearInterval(save);
                        window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
                        CloseAllTabs();
                        return;
                    }

                    if ($(d).find('.x-window:contains("保存成功！是否关闭？")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("保存成功！是否关闭？")').find('button:contains("是")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('完成妻子体格检查 - 一般结果: 妻子:{妻子姓名}, 档案编号: {档案编号}  的修改', true, "OpenDocTabForWifeCheck");
                        if(CompleteFlag == "1") window.submitSys.writeBack("{档案编号}", "QZFK");
                    }

                    if ($(d).find('.x-window:contains("提示"), .x-window:contains("失败")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("提示"), .x-window:contains("失败")').find('button:contains("确定")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('妻子体格检查 - 一般结果: 妻子:{妻子姓名}, 档案编号: {档案编号}  的修改失败', true, "OpenDocTabForWifeCheck");
                    }
                }, 1000);
                
                //window.submitSys.popupMsg('完成妻子体格检查 - 一般结果: 妻子:{妻子姓名}, 档案编号: {档案编号}  的修改', true, "OpenDocTabForWifeCheck");

            } catch (e) {
                window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                CloseAllTabs();
            }
        }
    }, 1000);
});