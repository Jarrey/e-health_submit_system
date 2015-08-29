var f = GetFrame("/entity/basic/labExamW_inputF.action?");
if (!f) {
    f = GetFrame("/entity/basic/labExamW_toUpdate.action?");
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

                    var fieldSet = GetFieldSet(d, "女性临床检验单");
                    var fields = GetFieldSet(fieldSet, "白带检查");
                    if ("{线索细胞}" == "0") {
                        Enter(fields, f, "combo", "线索细胞", "阴性");
                    } else if ("{线索细胞}" == "1") {
                        Enter(fields, f, "combo", "线索细胞", "阳性");
                    } else if ("{线索细胞}" == "9") {
                        Enter(fields, f, "combo", "线索细胞", "可疑");
                    }

                    if ("{念珠菌感染}" == "0") {
                        Enter(fields, f, "combo", "念珠菌感染", "阴性");
                    } else if ("{念珠菌感染}" == "1") {
                        Enter(fields, f, "combo", "念珠菌感染", "阳性");
                    } else if ("{念珠菌感染}" == "9") {
                        Enter(fields, f, "combo", "念珠菌感染", "可疑");
                    }

                    if ("{滴虫感染}" == "0") {
                        Enter(fields, f, "combo", "滴虫感染", "阴性");
                    } else if ("{滴虫感染}" == "1") {
                        Enter(fields, f, "combo", "滴虫感染", "阳性");
                    } else if ("{滴虫感染}" == "9") {
                        Enter(fields, f, "combo", "滴虫感染", "可疑");
                    }

                    if ("{清洁度}" == "0") {
                        Enter(fields, f, "combo", "清洁度", "Ⅰ");
                    } else if ("{清洁度}" == "1") {
                        Enter(fields, f, "combo", "清洁度", "Ⅱ");
                    } else if ("{清洁度}" == "2") {
                        Enter(fields, f, "combo", "清洁度", "Ⅲ");
                    } else if ("{清洁度}" == "3") {
                        Enter(fields, f, "combo", "清洁度", "Ⅳ");
                    }

                    if ("{胺臭味实验}" == "0") {
                        Enter(fields, f, "combo", "胺臭味实验", "阴性");
                    } else if ("{胺臭味实验}" == "1") {
                        Enter(fields, f, "combo", "胺臭味实验", "阳性");
                    }

                    if ("{PH值}" == "0") {
                        Enter(fields, f, "combo", "PH值", "<4.5");
                    } else if ("{PH值}" == "1") {
                        Enter(fields, f, "combo", "PH值", ">=4.5");
                    }

                    if ("{淋球菌筛查}" == "0") {
                        Enter(fieldSet, f, "combo", "淋球菌筛查", "阴性");
                    } else if ("{淋球菌筛查}" == "1") {
                        Enter(fieldSet, f, "combo", "淋球菌筛查", "阳性");
                    } else if ("{淋球菌筛查}" == "9") {
                        Enter(fieldSet, f, "combo", "淋球菌筛查", "可疑");
                    }

                    if ("{沙眼衣原体筛查}" == "0") {
                        Enter(fieldSet, f, "combo", "沙眼衣原体筛查", "阴性");
                    } else if ("{沙眼衣原体筛查}" == "1") {
                        Enter(fieldSet, f, "combo", "沙眼衣原体筛查", "阳性");
                    } else if ("{沙眼衣原体筛查}" == "9") {
                        Enter(fieldSet, f, "combo", "沙眼衣原体筛查", "可疑");
                    }

                    fields = GetFieldSet(fieldSet, "血细胞分析");
                    Enter(fields, f, "text", "Hb(g/L)", "{Hb}");
                    Enter(fields, f, "text", "RBC(1012/L)", "{RBC}");
                    Enter(fields, f, "text", "PLT(109/L)", "{PLT}");
                    Enter(fields, f, "text", "WBC(109/L)", "{WBC}");
                    Enter(fields, f, "text", "N(%)", "{N}");
                    Enter(fields, f, "text", "E(%)", "{E}");
                    Enter(fields, f, "text", "B(%)", "{B}");
                    Enter(fields, f, "text", "L(%)", "{L}");
                    Enter(fields, f, "text", "M(%)", "{M}");
                    Enter(fields, f, "text", "中值细胞(%)", "{中间细胞数(MID#)}");

                    fields = GetFieldSet(fieldSet, "尿液常规检查");
                    if ("{尿常规}" == "0") {
                        Enter(fields, f, "combo", "尿液常规检查", "未见异常");
                    } else if ("{尿常规}" == "1") {
                        Enter(fields, f, "combo", "尿液常规检查", "异常");
                        Enter(fields, f, "text", "异常描述", "{尿常规异常}");
                    }

                    fields = GetFieldSet(fieldSet, "血型");
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

                    Enter(fieldSet, f, "text", "血糖（mmol/L）", "{血糖}");

                    fields = GetFieldSet(fieldSet, "乙肝血清学检查");
                    if ("{HBs-Ag}" == "0") {
                        Enter(fields, f, "combo", "HBs-Ag", "阴性");
                    } else if ("{HBs-Ag}" == "1") {
                        Enter(fields, f, "combo", "HBs-Ag", "阳性");
                    } else if ("{HBs-Ag}" == "9") {
                        Enter(fields, f, "combo", "HBs-Ag", "可疑");
                    }

                    if ("{HBe-Ab}" == "0") {
                        Enter(fields, f, "combo", "HBe-Ab", "阴性");
                    } else if ("{HBe-Ab}" == "1") {
                        Enter(fields, f, "combo", "HBe-Ab", "阳性");
                    } else if ("{HBe-Ab}" == "9") {
                        Enter(fields, f, "combo", "HBe-Ab", "可疑");
                    }

                    if ("{HBs-Ab}" == "0") {
                        Enter(fields, f, "combo", "HBs-Ab", "阴性");
                    } else if ("{HBs-Ab}" == "1") {
                        Enter(fields, f, "combo", "HBs-Ab", "阳性");
                    } else if ("{HBs-Ab}" == "9") {
                        Enter(fields, f, "combo", "HBs-Ab", "可疑");
                    }

                    if ("{HBc-Ab}" == "0") {
                        Enter(fields, f, "combo", "HBc-Ab", "阴性");
                    } else if ("{HBc-Ab}" == "1") {
                        Enter(fields, f, "combo", "HBc-Ab", "阳性");
                    } else if ("{HBc-Ab}" == "9") {
                        Enter(fields, f, "combo", "HBc-Ab", "可疑");
                    }

                    if ("{HBe-Ag}" == "0") {
                        Enter(fields, f, "combo", "HBe-Ag", "阴性");
                    } else if ("{HBe-Ag}" == "1") {
                        Enter(fields, f, "combo", "HBe-Ag", "阳性");
                    } else if ("{HBe-Ag}" == "9") {
                        Enter(fields, f, "combo", "HBe-Ag", "可疑");
                    }

                    fields = GetFieldSet(fieldSet, "肝肾功能检测");
                    Enter(fields, f, "text", "谷丙转氨酶ALT(U/L)", "{谷丙转氨酶}");
                    Enter(fields, f, "text", "肌酐Cr(umol/L)", "{肌酐}");
                    fields = GetFieldSet(fieldSet, "甲状腺功能检测");
                    Enter(fields, f, "text", "促甲状腺激素TSH(ulU/ml)", "{促甲状腺激素}");

                    if ("{风疹病毒抗体IgG测定}" == "0") {
                        Enter(fieldSet, f, "combo", "风疹病毒IgG", "阴性");
                    } else if ("{风疹病毒抗体IgG测定}" == "1") {
                        Enter(fieldSet, f, "combo", "风疹病毒IgG", "阳性");
                    } else if ("{风疹病毒抗体IgG测定}" == "9") {
                        Enter(fieldSet, f, "combo", "风疹病毒IgG", "可疑");
                    }

                    if ("{巨细胞病毒抗体IgG测定}" == "0") {
                        Enter(fieldSet, f, "combo", "巨细胞病毒IgG", "阴性");
                    } else if ("{巨细胞病毒抗体IgG测定}" == "1") {
                        Enter(fieldSet, f, "combo", "巨细胞病毒IgG", "阳性");
                    } else if ("{巨细胞病毒抗体IgG测定}" == "9") {
                        Enter(fieldSet, f, "combo", "巨细胞病毒IgG", "可疑");
                    }

                    if ("{弓形虫抗体IgG测定}" == "0") {
                        Enter(fieldSet, f, "combo", "弓形虫IgG", "阴性");
                    } else if ("{弓形虫抗体IgG测定}" == "1") {
                        Enter(fieldSet, f, "combo", "弓形虫IgG", "阳性");
                    } else if ("{弓形虫抗体IgG测定}" == "9") {
                        Enter(fieldSet, f, "combo", "弓形虫IgG", "可疑");
                    }

                    if ("{梅毒螺旋体筛查}" == "0") {
                        Enter(fieldSet, f, "combo", "梅毒螺旋体筛查", "阴性");
                    } else if ("{梅毒螺旋体筛查}" == "1") {
                        Enter(fieldSet, f, "combo", "梅毒螺旋体筛查", "阳性");
                    } else if ("{梅毒螺旋体筛查}" == "9") {
                        Enter(fieldSet, f, "combo", "梅毒螺旋体筛查", "可疑");
                    }

                    if ("{巨细胞病毒抗体IgM测定}" == "0") {
                        Enter(fieldSet, f, "combo", "巨细胞病毒IgM", "阴性");
                    } else if ("{巨细胞病毒抗体IgM测定}" == "1") {
                        Enter(fieldSet, f, "combo", "巨细胞病毒IgM", "阳性");
                    } else if ("{巨细胞病毒抗体IgM测定}" == "9") {
                        Enter(fieldSet, f, "combo", "巨细胞病毒IgM", "可疑");
                    }

                    if ("{弓形虫抗体IgM测定}" == "0") {
                        Enter(fieldSet, f, "combo", "弓形体IgM", "阴性");
                    } else if ("{弓形虫抗体IgM测定}" == "1") {
                        Enter(fieldSet, f, "combo", "弓形体IgM", "阳性");
                    } else if ("{弓形虫抗体IgM测定}" == "9") {
                        Enter(fieldSet, f, "combo", "弓形体IgM", "可疑");
                    }

                    Enter(fieldSet, f, "textarea", "其他（请描述）", "{其他检查}");
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
                                    window.submitSys.popupMsg('妻子首诊临床检验表 - 妻子姓名: {妻子姓名}, 档案编号: {档案编号} ' + msg, true, "OpenDocTabForWifeClinical");
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