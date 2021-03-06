﻿var f = GetFrame("/entity/archives/physicalExamM_input.action?");
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
                var t = ext.getCmp($(d).find('.x-tab-panel:contains("男性一般体格检查")')[0].id);
                t.activate(0);
                var b = $(d).find('#physical1')[0];
                Enter(b, f, "text", "身高(cm)", "{身高}");
                Enter(b, f, "text", "体重指数", eval("({体重} * 10000 / ({身高} * {身高}))").toString());
                Enter(b, f, "text", "体重(kg)", "{体重}");
                Enter(b, f, "text", "收缩压(mmHg)", "{收缩压}");
                Enter(b, f, "text", "心率(次/分)", "{心率}");
                Enter(b, f, "text", "舒张压(mmHg)", "{舒张压}");

                var field = $(d).find(".x-plain-body:contains('精神状态')")[0];
                if ("{精神状态}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "精神状态异常描述", "{精神异常描述}");
                }

                field = $(d).find(".x-plain-body:contains('智力')")[0];
                if ("{智力}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    ext.getCmp('knowledge').setValue(eval("{常识}"));
                    ext.getCmp('judge').setValue(eval("{判断}"));
                    ext.getCmp('memory').setValue(eval("{记忆}"));
                    ext.getCmp('caculate').setValue(eval("{计算}"));
                }

                field = $(d).find(".x-plain-body:contains('五官')")[0];
                if ("{五官}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "五官异常描述", "{五官异常描述}");
                }

                field = $(d).find(".x-plain-body:contains('特殊体态')")[0];
                if ("{特殊体态}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "特殊体态描述", "{体态异常}");
                }

                field = $(d).find(".x-plain-body:contains('特殊面容')")[0];
                if ("{特殊面容}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "特殊面容描述", "{面容异常}");
                }

                field = $(d).find(".x-plain-body:contains('皮肤毛发')")[0];
                if ("{皮肤毛发}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "皮肤毛发异常描述", "{皮肤异常}");
                }

                field = $(d).find(".x-plain-body:contains('甲状腺')")[0];
                if ("{甲状腺}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "甲状腺异常描述", "{甲状腺异常}");
                }

                field = $(d).find(".x-plain-body:contains('肺部')")[0];
                if ("{肺部}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "肺部异常描述", "{肺部异常}");
                }

                field = $(d).find(".x-plain-body:contains('心脏节律是否整齐')")[0];
                if ("{心脏节律}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "心脏节律描述", "{心脏节律异常}");
                }

                field = $(d).find(".x-plain-body:contains('心脏杂音')")[0];
                if ("{心脏杂音}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "心脏杂音描述", "{心脏杂音异常}");
                }

                field = $(d).find(".x-plain-body:contains('肝脾检查')")[0];
                if ("{肝脾}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "肝脾检查异常描述", "{肝脾异常}");
                }

                field = $(d).find(".x-plain-body:contains('四肢脊柱')")[0];
                if ("{四肢脊柱}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "四肢脊柱异常描述", "{四肢脊柱异常}");
                }

                Enter(b, f, "textarea", "其他检查", "{其他检查}");
                ext.getCmp('done').setValue(CompleteFlag);
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

                    if ($(d).find('.x-window:contains("成功！继续填写生殖系统检查？")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("成功！继续填写生殖系统检查？")').find('button:contains("否")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('完成丈夫体格检查 - 一般结果: 丈夫:{丈夫姓名}, 档案编号: {档案编号}  的修改', true, "OpenDocTabForHusCheck");
                        if(CompleteFlag == "1") window.submitSys.writeBack("{档案编号}", "ZFTG");
                    }

                    if ($(d).find('.x-window:contains("提示"), .x-window:contains("失败")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("提示"), .x-window:contains("失败")').find('button:contains("确定")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('丈夫体格检查 - 一般结果: 丈夫:{丈夫姓名}, 档案编号: {档案编号}  的修改失败', true, "OpenDocTabForHusCheck");
                    }
                }, 1000);
                
                //window.submitSys.popupMsg('完成丈夫体格检查 - 一般结果: 丈夫:{丈夫姓名}, 档案编号: {档案编号}  的修改', true, "OpenDocTabForHusCheck");

            } catch (e) {
                window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                CloseAllTabs();
            }
        }
    }, 1000);
});