var f = GetFrame("/entity/exam/labExamM_input.action?");
if (!f) {
    f = GetFrame("/entity/exam/labExamM_toUpdate.action?");
}

if (!f) {
    window.submitSys.popupMsg("发生错误: 找不到页面框架", false, "");
    CloseAllTabs();
} else {
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

                    var fieldSet = GetFieldSet(d, "男性临床检验单");
                    var fields = GetFieldSet(fieldSet, "血型");
                    if ("{血型}" == "0") {
                        Enter(fields, f, "combo", "ABO", "A型");
                    } else if ("{血型}" == "1") {
                        Enter(fields, f, "combo", "ABO", "B型");
                    } else if ("{血型}" == "2") {
                        Enter(fields, f, "combo", "ABO", "AB型");
                    } else if ("{血型}" == "3") {
                        Enter(fields, f, "combo", "ABO", "O型");
                    }

                    if ("{RH}" == "0") {
                        Enter(fields, f, "combo", "Rh", "阴性");
                    } else if ("{RH}" == "1") {
                        Enter(fields, f, "combo", "Rh", "阳性");
                    }

                    fields = GetFieldSet(fieldSet, "尿液常规检查");
                    if ("{尿常规}" == "0") {
                        Enter(fieldSet, f, "combo", "尿液常规检查", "未见异常");
                    } else if ("{尿常规}" == "1") {
                        Enter(fieldSet, f, "combo", "尿液常规检查", "异常");
                        Enter(fieldSet, f, "text", "异常描述", "{尿常规异常}");
                    }

                    if ("{梅毒螺旋体筛查}" == "0") {
                        Enter(fieldSet, f, "combo", "梅毒螺旋体筛查", "阴性");
                    } else if ("{梅毒螺旋体筛查}" == "1") {
                        Enter(fieldSet, f, "combo", "梅毒螺旋体筛查", "阳性");
                    } else if ("{梅毒螺旋体筛查}" == "9") {
                        Enter(fieldSet, f, "combo", "梅毒螺旋体筛查", "可疑");
                    }

                    fields = GetFieldSet(fieldSet, "乙肝血清学检查");
                    if ("{HBs-Ag}" == "0") {
                        Enter(fields, f, "combo", "HBs_Ag", "阴性");
                    } else if ("{HBs-Ag}" == "1") {
                        Enter(fields, f, "combo", "HBs_Ag", "阳性");
                    } else if ("{HBs-Ag}" == "9") {
                        Enter(fields, f, "combo", "HBs_Ag", "可疑");
                    }

                    if ("{HBe-Ab}" == "0") {
                        Enter(fields, f, "combo", "HBe_Ab", "阴性");
                    } else if ("{HBe-Ab}" == "1") {
                        Enter(fields, f, "combo", "HBe_Ab", "阳性");
                    } else if ("{HBe-Ab}" == "9") {
                        Enter(fields, f, "combo", "HBe_Ab", "可疑");
                    }

                    if ("{HBs-Ab}" == "0") {
                        Enter(fields, f, "combo", "HBs_Ab", "阴性");
                    } else if ("{HBs-Ab}" == "1") {
                        Enter(fields, f, "combo", "HBs_Ab", "阳性");
                    } else if ("{HBs-Ab}" == "9") {
                        Enter(fields, f, "combo", "HBs_Ab", "可疑");
                    }

                    if ("{HBc-Ab}" == "0") {
                        Enter(fields, f, "combo", "HBc_Ab", "阴性");
                    } else if ("{HBc-Ab}" == "1") {
                        Enter(fields, f, "combo", "HBc_Ab", "阳性");
                    } else if ("{HBc-Ab}" == "9") {
                        Enter(fields, f, "combo", "HBc_Ab", "可疑");
                    }

                    if ("{HBe-Ag}" == "0") {
                        Enter(fields, f, "combo", "HBe_Ag", "阴性");
                    } else if ("{HBe-Ag}" == "1") {
                        Enter(fields, f, "combo", "HBe_Ag", "阳性");
                    } else if ("{HBe-Ag}" == "9") {
                        Enter(fields, f, "combo", "HBe_Ag", "可疑");
                    }

                    fields = GetFieldSet(fieldSet, "肝肾功能检测");
                    Enter(fields, f, "text", "谷丙转氨酶ALT(U/L)", "{谷丙转氨酶}");
                    Enter(fields, f, "text", "肌酐Cr(umol/L)", "{肌酐}");
                    
                    Enter(fieldSet, f, "textarea", "其他检查", "{其他检查}");
                    Enter(fieldSet, f, "check", "完成", CompleteFlag);
                    Enter(d, f, "text", "医师签名", "{医生签名}");
                    Enter(d, f, "date", "检查日期", Today);

                    ClickButton(d, "保  存");

                    times = 0;
                    var save = setInterval(function() {
                        times++;
                        if (times >= MaxRetryTimes) {
                            window.clearInterval(save);
                            window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
                            CloseAllTabs();
                            return;
                        }
                    
                        if ($(d).find('.x-window:contains("选择框"), .x-window:contains("提示")').length > 0) {
                            window.clearInterval(save);
                    
                            var msgBox = $(d).find('.x-window:contains("选择框"), .x-window:contains("提示")');
                            var msg = msgBox.find('span.ext-mb-text').text();
                            msgBox.find('button:contains("确定"), button:contains("是")').click();
                    
                            times = 0;
                            save = setInterval(function() {
                                times++;
                                if (times >= MaxRetryTimes) {
                                    window.clearInterval(save);
                                    window.submitSys.popupMsg("超时，无法找到页面元素", false, "");
                                    CloseAllTabs();
                                    return;
                                }
                    
                                if ($(d).find('.x-window:contains("提示")').length > 0) {
                                    window.clearInterval(save);
                                    msgBox = $(d).find('.x-window:contains("提示")');
                                    msg = msg + msgBox.find('span.ext-mb-text').text();
                                    msgBox.find('button:contains("确定"), button:contains("是")').click();
                                    window.submitSys.popupMsg('丈夫首诊临床检验表 - 丈夫姓名: {丈夫姓名}, 档案编号: {档案编号} ' + msg, true, "OpenDocTabForHusClinical");
                                    CloseAllTabs();
                                }
                            }, 1000);
                        }
                    }, 1000);
                } catch (e) {
                    window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                    CloseAllTabs();
                }
            }
        }, 1000);
    });
}