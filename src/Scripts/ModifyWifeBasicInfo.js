var f = GetFrame("/entity/basic/generalInfoW_input.action?");
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

        if (ext.isReady) {
            window.clearInterval(i);
            try {
                // 疾病史
                ClickButton(d, "疾病史");
                var fieldSet = GetFieldSet(d, "疾病史");
                var fields = GetFieldSet(fieldSet, "是否患有或曾经患过以下疾病");
                if ("{疾病史}" == "0") {
                    Enter(fields, f, "radio", "是", 1);
                    var list = GetFieldSet(fields, "疾病列表");
                    Enter(list, f, "check", "贫血", eval("{贫血}"));
                    Enter(list, f, "check", "高血压", eval("{高血压}"));
                    Enter(list, f, "check", "心脏病", eval("{心脏病}"));
                    Enter(list, f, "check", "糖尿病", eval("{糖尿病}"));
                    Enter(list, f, "check", "癫痫", eval("{癫痫}"));
                    Enter(list, f, "check", "甲状腺病", eval("{甲状腺疾病}"));
                    Enter(list, f, "check", "慢性肾炎", eval("{慢性肾炎}"));
                    Enter(list, f, "check", "肿瘤", eval("{肿瘤}"));
                    Enter(list, f, "check", "结核", eval("{结核}"));
                    Enter(list, f, "check", "乙型肝炎", eval("{乙型肝炎}"));
                    Enter(list, f, "check", "淋病/梅毒/衣原体感染等", eval("{淋病/梅毒/衣原体感染等}"));
                    Enter(list, f, "check", "精神心理疾患等", eval("{精神心理疾患等}"));
                    Enter(list, f, "check", "其他", eval("{是否有其他疾病}"));
                    if ("{是否有其他疾病}" == "1") Enter(list, f, "text", ":", "{其他疾病}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }

                fields = GetFieldSet(fieldSet, "是否患有出生缺陷,如先天畸形、遗传病等");
                if ("{患有出生缺陷}" == "1") {
                    Enter(fields, f, "radio", "有,注明具体病名", 1);
                    Enter(fields, f, "text", ":", "{出生缺陷病名}");
                } else {
                    Enter(fields, f, "radio", "有,注明具体病名", 0);
                }

                fields = GetFieldSet(fieldSet, "是否有以下妇科疾病");
                if ("{妇科疾病}" == "0") {
                    Enter(fields, f, "radio", "是", 1);
                    list = GetFieldSet(fields, "妇科疾病列表");
                    Enter(list, f, "check", "子宫附件炎症", eval("{子宫附件炎}"));
                    Enter(list, f, "check", "不孕不育症", eval("{不孕不育症}"));
                    Enter(list, f, "check", "其他", eval("{其他妇科疾病}"));
                    if ("{其他妇科疾病}" == "1") Enter(list, f, "text", ":", "{其他妇科疾病描述}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }


                // 用药史
                ClickButton(d, "用药史");
                fieldSet = GetFieldSet(d, "用药史");
                fields = GetFieldSet(fieldSet, "目前是否服药");
                if ("{是否服药}" == "1") {
                    Enter(fields, f, "radio", "是，药物名称", 1);
                    Enter(fields, f, "text", ":", "{药物名称}");
                } else {
                    Enter(fields, f, "radio", "是，药物名称", 0);
                }

                fields = GetFieldSet(fieldSet, "是否注射过疫苗");
                if ("{是否注射过疫苗}" == "0") {
                    Enter(fields, f, "radio", "是", 1);
                    list = GetFieldSet(fields, "疫苗列表");
                    Enter(list, f, "check", "风疹疫苗", eval("{风疹疫苗}"));
                    Enter(list, f, "check", "乙肝疫苗", eval("{乙肝疫苗}"));
                    Enter(list, f, "check", "其他", eval("{是否注射其他疫苗}"));
                    if ("{是否注射其他疫苗}" == "1") Enter(list, f, "text", ":", "{其他疫苗}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }

                fields = GetFieldSet(fieldSet, "是否正在使用或曾经使用避孕措施");
                if ("{是否采用避孕措施}" == "0") {
                    Enter(fields, f, "radio", "是", 1);
                    list = GetFieldSet(fields, "现用避孕措施或目前终止避孕者原避孕措施");
                    Enter(list, f, "check", "宫内节育器", eval("{宫内节育器}"));
                    Enter(list, f, "check", "皮下埋植剂", eval("{皮下埋植剂}"));
                    Enter(list, f, "check", "口服避孕药", eval("{口服避孕药}"));
                    Enter(list, f, "check", "避孕套", eval("{避孕套}"));
                    Enter(list, f, "check", "外用药", eval("{外用药}"));
                    Enter(list, f, "check", "自然避孕", eval("{自然避孕}"));
                    Enter(list, f, "check", "其他", eval("{是否采用其他避孕措施}"));
                    if ("{是否采用其他避孕措施}" == "1") Enter(list, f, "text", ":", "{其他避孕措施说明}");
                    Enter(list, f, "text", "避孕措施持续使用时间", "{避孕时间}");
                    if ("{避孕停止时间}" > "0") {
                        Enter(list, f, "check", "目前是否停用避孕措施", 1);
                        var now = new Date();
                        var date = new Date(now.setMonth(now.getMonth() - eval("{避孕停止时间}")));
                        Enter(list, f, "date", "避孕措施停用时间", ext.util.Format.date(date, "Y-m-d"));
                    } else {
                        Enter(list, f, "check", "目前是否停用避孕措施", 0);
                    }
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }


                // 孕育史
                ClickButton(d, "孕育史");
                fieldSet = GetFieldSet(d, "孕育史");
                Enter(fieldSet, f, "text", "初潮年龄(岁)", "{初潮年龄}");
                Enter(fieldSet, f, "date", "末次月经日期", "{末次月经}");
                fields = GetFieldSet(fieldSet, "月经周期是否规律");
                Enter(fields, f, "radio", "是", eval("{月经是否规律}"));
                fields = GetFieldSet(fieldSet, "月经规律列表");
                Enter(fields, f, "text", "经期", "{经期天数1}");
                Enter(fields, f, "text", "至", "{经期天数2}", 0);
                Enter(fields, f, "text", "周期", "{月经周期1}");
                Enter(fields, f, "text", "至", "{月经周期2}", 1);
                fields = GetFieldSet(fieldSet, "月经量");
                Enter(fields, f, "radio", "多", eval("{月经量}"));
                fields = GetFieldSet(fieldSet, "痛");
                Enter(fields, f, "radio", "无", eval("{痛经}"));

                fields = GetFieldSet(fieldSet, "是否曾经怀孕");
                if ("{是否曾经怀孕}" == "1") {
                    Enter(fields, f, "radio", "是", 1);
                    list = GetFieldSet(fields, "怀孕情况列表");
                    Enter(list, f, "text", "怀孕次数", "{怀孕次数}");
                    Enter(list, f, "text", "活产次数", "{活产次数}");
                    Enter(list, f, "text", "足月活产次数", "{足月次数}");
                    Enter(list, f, "text", "早产次数", "{早产次数}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }

                fields = GetFieldSet(fieldSet, "是否有以下不良妊娠结局");
                if ("{是否有不良妊娠}" == "0") {
                    Enter(fields, f, "radio", "是", 1);
                    list = GetFieldSet(fields, "不良妊娠结局列表");
                    Enter(list, f, "check", "死产死胎", eval("{死胎死产}"));
                    if ("{死胎死产}" != "0") Enter(list, f, "text", ":", "{死产次数}", 0);
                    Enter(list, f, "check", "自然流产", eval("{自然流产}"));
                    if ("{自然流产}" != "0") Enter(list, f, "text", ":", "{自然流产次数}", 1);
                    Enter(list, f, "check", "人工流产", eval("{人工流产}"));
                    if ("{人工流产}" != "0") Enter(list, f, "text", ":", "{人工流产次数}", 2);
                    Enter(list, f, "check", "其他", eval("{其他不良妊娠}"));
                    if ("{其他不良妊娠}" != "0") Enter(list, f, "text", ":", "{其他不良妊娠}", 3);
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }

                fields = GetFieldSet(fieldSet, "是否分娩过出生缺陷儿");
                if ("{是否分娩过缺陷儿}" == "1") {
                    Enter(fields, f, "radio", "是", 1);
                    list = GetFieldSet(fields, "出生缺陷儿情况列表");
                    Enter(list, f, "text", "病种", "{缺陷儿病种}");
                    Enter(list, f, "text", "详细情况", "{缺陷儿详情}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }

                fields = GetFieldSet(fieldSet, "子女情况");
                Enter(fields, f, "text", "现有子女数", "{子女数}");
                list = GetFieldSet(fields, "子女身体状况");
                if ("{子女身体状况}" == "1") {
                    Enter(fields, f, "radio", "疾病，注明具体病名", 1);
                    Enter(list, f, "text", ":", "{子女疾病名}");
                } else {
                    Enter(fields, f, "radio", "疾病，注明具体病名", 0);
                }


                // 家族史
                ClickButton(d, "家族史");
                fieldSet = GetFieldSet(d, "家族史");
                fields = GetFieldSet(fieldSet, "夫妻是否近亲结婚");
                if ("{是否近亲结婚}" == "1") {
                    Enter(fields, f, "radio", "是", 1);
                    Enter(fields, f, "text", ":", "{近亲关系}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }
                fields = GetFieldSet(fieldSet, "祖父母/外祖父母、父母两代家族内近亲结婚史");
                if ("{家族是否有近亲结婚}" == "1") {
                    Enter(fields, f, "radio", "是", 1);
                    Enter(fields, f, "text", ":", "{家族近亲关系}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }
                fields = GetFieldSet(fieldSet, "家族成员是否有人患以下疾病");
                if ("{家庭成员是否有疾病}" == "0") {
                    Enter(fields, f, "radio", "是", 1);
                    list = GetFieldSet(fields, "家族成员疾病列表");
                    Enter(list, f, "check", "地中海贫血", eval("{地中海贫血}"));
                    Enter(list, f, "check", "白化病", eval("{白化病}"));
                    Enter(list, f, "check", "蚕豆病（G6PD缺乏症）", eval("{G6PD缺乏症}"));
                    Enter(list, f, "check", "血友病", eval("{血友病}"));
                    Enter(list, f, "check", "先天性心脏病", eval("{先天性心脏病}"));
                    Enter(list, f, "check", "唐氏综合症", eval("{唐氏综合症}"));
                    Enter(list, f, "check", "糖尿病", eval("{家庭成员有糖尿病}"));
                    Enter(list, f, "check", "先天性智力低下", eval("{先天性智力低下}"));
                    Enter(list, f, "check", "听力障碍(10岁内发生)", eval("{听力障碍}"));
                    Enter(list, f, "check", "视力障碍(10岁内发生)", eval("{视力障碍}"));
                    Enter(list, f, "check", "新生儿或婴儿死亡", eval("{新生儿死亡}"));
                    Enter(list, f, "check", "其他", eval("{其他出生缺陷}"));
                    if ("{其他出生缺陷}" == "1") Enter(list, f, "text", ":", "{家族出生缺陷}");
                    Enter(list, f, "text", "患者与本人关系", "{与患者关系}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }


                // 饮食营养、生活习惯、环境毒害物接触
                ClickButton(d, "食营养、生活习惯、环境毒害物接触");
                fieldSet = GetFieldSet(d, "食营养、生活习惯、环境毒害物接触");
                fields = GetFieldSet(fieldSet, "是否进食肉、蛋类");
                Enter(fields, f, "radio", "是", eval("{进食肉、蛋类}"));
                fields = GetFieldSet(fieldSet, "是否厌食蔬菜");
                Enter(fields, f, "radio", "是", eval("{厌食蔬菜}"));
                fields = GetFieldSet(fieldSet, "是否有食用生肉嗜好");
                Enter(fields, f, "radio", "是", eval("{食用生肉}"));
                fields = GetFieldSet(fieldSet, "是否吸烟");
                if ("{吸烟}" == "1") {
                    Enter(fields, f, "radio", "是", 1);
                    Enter(fields, f, "text", ":", "{吸烟支数}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }
                fields = GetFieldSet(fieldSet, "是否存在被动吸烟");
                if ("{被动吸烟}" != "0") {
                    Enter(fields, f, "radio", "否", eval("{被动吸烟}"));
                    Enter(fields, f, "text", "平均每天被动吸烟时间", "{被动吸烟时间}");
                } else {
                    Enter(fields, f, "radio", "否", 0);
                }
                fields = GetFieldSet(fieldSet, "是否饮酒");
                if ("{饮酒}" != "0") {
                    Enter(fields, f, "radio", "否", eval("{饮酒}"));
                    Enter(fields, f, "text", "平均每天饮酒量", "{饮酒量}");
                } else {
                    Enter(fields, f, "radio", "否", 0);
                }
                fields = GetFieldSet(fieldSet, "是否使用可卡因等毒麻药品");
                if ("{使用毒麻药品}" == "1") {
                    Enter(fields, f, "radio", "是", 1);
                    Enter(fields, f, "text", ":", "{毒麻药品名}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }
                fields = GetFieldSet(fieldSet, "是否口臭");
                Enter(fields, f, "radio", "是", eval("{口臭}"));
                fields = GetFieldSet(fieldSet, "是否牙龈出血");
                Enter(fields, f, "radio", "是", eval("{牙龈出血}"));
                fields = GetFieldSet(fieldSet, "生活或工作环境中是否接触以下因素");
                if ("{未接触不良环境}" == "0") {
                    Enter(fields, f, "radio", "是", 1);
                    list = GetFieldSet(fields, "毒害物质列表");
                    Enter(list, f, "check", "放射线", eval("{放射线}"));
                    Enter(list, f, "check", "高温", eval("{高温}"));
                    Enter(list, f, "check", "噪音", eval("{噪音}"));
                    Enter(list, f, "check", "有机溶剂（如新装修、油漆等）", eval("{有机溶剂}"));
                    Enter(list, f, "check", "密切接触猫狗等家畜、宠物", eval("{接触宠物等}"));
                    Enter(list, f, "check", "振动", eval("{震动}"));
                    Enter(list, f, "check", "重金属（铅、汞等）", eval("{重金属}"));
                    Enter(list, f, "check", "农药", eval("{农药}"));
                    Enter(list, f, "check", "其他", eval("{接触其他不良环境}"));
                    Enter(list, f, "text", ":", "{不良环境}");
                } else {
                    Enter(fields, f, "radio", "是", 0);
                }


                // 社会心理因素
                ClickButton(d, "社会心理因素");
                fieldSet = GetFieldSet(d, "社会心理因素");
                fields = GetFieldSet(fieldSet, "是否感到生活/工作压力");
                Enter(fields, f, "radio", "很少", eval("{生活/工作压力}"));
                fields = GetFieldSet(fieldSet, "与亲友、同事的关系是否紧张");
                Enter(fields, f, "radio", "很少", eval("{人际关系紧张}"));
                fields = GetFieldSet(fieldSet, "是否感到经济压力");
                Enter(fields, f, "radio", "很少", eval("{经济压力}"));
                fields = GetFieldSet(fieldSet, "是否做好怀孕准备");
                Enter(fields, f, "radio", "是", eval("{是否做好怀孕准备}"));
                Enter(fieldSet, f, "text", "其他（请描述）", "{其他心理因素}");


                Enter(d, f, "check", "完成(注意：此项计入工作量统计，一旦选中并保存，则表格不能再修改！)", CompleteFlag);
                Enter(d, f, "text", "医师签名", "{医生签名}");
                Enter(d, f, "date", "询问日期", Today);

                /*
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

                    $(d).find('.x-window:contains("您已确认无误，要提交？")').find('button:contains("是")').click();
                    if ($(d).find('.x-window:contains("成功！关闭当前标签页？")').length > 0) {
                        window.clearInterval(save);
                        $(d).find('.x-window:contains("成功！关闭当前标签页？")').find('button:contains("是")').click();
                        CloseTab("完善档案");
                        CloseTab("临床医生系统");
                        window.submitSys.popupMsg('完成妻子一般情况: 妻子:{妻子姓名}, 档案编号: {档案编号}  的修改', true, "OpenDocTabForWifeBasicInfo");
                    }
                }, 1000);
                */

                window.submitSys.popupMsg('完成妻子一般情况: 妻子:{妻子姓名}, 档案编号: {档案编号}  的修改', true, "OpenDocTabForWifeBasicInfo");

            } catch (e) {
                window.submitSys.popupMsg("发生错误: " + e.name + ":" + e.message, false, "");
                CloseAllTabs();
            }
        }
    }, 1000);
});