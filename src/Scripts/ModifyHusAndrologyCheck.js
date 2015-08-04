var f = GetFrame("/entity/archives/physicalExamM_input.action?");
var d = f.contentDocument;
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    var i = setInterval(function() {
        if (!IsLoadingData(d) && ext.isReady) {
            window.clearInterval(i);
            try {
                var t = ext.getCmp($(d).find('.x-tab-panel:contains("男性生殖系统检查")')[0].id);
                t.activate(1);
                b = $(d).find('#physical2')[0];

                field = $(d).find(".x-plain-body:contains('阴毛')")[0];
                if ("{阴毛}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "阴毛异常描述", "{阴毛异常}");
                }

                field = $(d).find(".x-plain-body:contains('喉结')")[0];
                if ("{喉结}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "喉结异常描述", "{喉结异常}");
                }

                field = $(d).find(".x-plain-body:contains('阴茎')")[0];
                if ("{阴茎}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "阴茎异常描述", "{阴茎异常}");
                }

                field = $(d).find(".x-plain-body:contains('包皮')")[0];
                if ("{包皮}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else if ("{包皮}" == "1") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                } else if ("{包皮}" == "2") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "2"]')[0].id).setValue("2");
                }

                field = $(d).find(".x-plain-body:contains('睾丸')")[0];
                if ("{扪及睾丸}" == "1") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                    Enter(b, f, "text", "左侧睾丸体积(ml)", "{睾丸左体积}");
                    Enter(b, f, "text", "右侧睾丸体积(ml)", "{睾丸右体积}");
                } else if ("{左侧未扪及}" == "1" && "{右侧未扪及}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "右侧睾丸体积(ml)", "{睾丸右体积}");
                } else if ("{左侧未扪及}" == "0" && "{右侧未扪及}" == "1") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "2"]')[0].id).setValue("2");
                    Enter(b, f, "text", "左侧睾丸体积(ml)", "{睾丸左体积}");
                } else if ("{左侧未扪及}" == "1" && "{右侧未扪及}" == "1") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "3"]')[0].id).setValue("3");
                }

                field = $(d).find(".x-plain-body:contains('附睾')")[0];
                if ("{附睾}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "附睾异常描述", "{附睾异常}");
                }

                field = $(d).find(".x-plain-body:contains('输精管')")[0];
                if ("{输精管}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "输精管异常描述", "{输精管异常}");
                }

                field = $(d).find(".x-plain-body:contains('精索静脉曲张')")[0];
                if ("{精索静脉曲张}" == "0") {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "0"]')[0].id).setValue("0");
                } else {
                    ext.getCmp($(field).contents().find('input.x-form-radio[value = "1"]')[0].id).setValue("1");
                    Enter(b, f, "text", "精索静脉曲张部位", "{静脉曲张部位}");
                    Enter(b, f, "text", "精索静脉曲张程度", "{静脉曲张程度}");
                }

                Enter(b, f, "textarea", "其他检查", "{其他检查}");
                ext.getCmp('done2').setValue(eval("{完成标志}"));
                Enter(b, f, "text", "医师签名", "{医生签名}");
                Enter(b, f, "date", "检查日期", "{检查时间}");

                ClickButton(b, "保存");
                $(d).find('.x-window:contains("您已确认无误，要提交？")').find('button:contains("是")').click();
                var save = setInterval(function() {
                    if ($(d).find('.x-window:contains("保存成功！是否关闭？")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("保存成功！是否关闭？")').find('button:contains("是")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('完成丈夫体格检查 - 一般结果: 丈夫:{丈夫姓名}, 丈夫证件号码: {丈夫证件号码}  的修改', true, "OpenDocTabForHusCheck");
                    }
                }, 1000);

            } catch (e) {
                window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                CloseAllTabs();
            }
        }
    }, 1000);
});