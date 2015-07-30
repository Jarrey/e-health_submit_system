var f = GetFrame("/entity/archives/basicInfoDetail_input.action?");
var ext = f.contentWindow.Ext;
ext.onReady(function() {
    var i = setInterval(function() {
        if (!IsLoadingData(d)) {
            window.clearInterval(i);
            Enter(f, f, "combo", "是否签署知情同意书", "男女双方都签署");

            var h = GetFieldSet(f, "男方信息");
            Enter(h, f, "text", "丈夫姓名", "{丈夫姓名}");
            Enter(h, f, "combo", "民族", "{丈夫民族}");
            Enter(h, f, "combo", "证件类型", "{丈夫证件类型}");
            Enter(h, f, "text", "证件号码", "{丈夫证件号码}");
            Enter(h, f, "date", "出生年月", "{丈夫出生年月}");
            Enter(h, f, "text", "年龄", "{丈夫年龄}");
            Enter(h, f, "combo", "文化程度", "{丈夫文化程度}");
            Enter(h, f, "combo", "职业", "{丈夫职业}");
            Enter(h, f, "combo", "户口性质", "{丈夫户口性质}");
            Enter(h, f, "text", "手机号码", "{丈夫手机}");

            h = GetFieldSet(f, "女方信息");
            Enter(h, f, "text", "妻子姓名", "{妻子姓名}");
            Enter(h, f, "combo", "民族", "{妻子民族}");
            Enter(h, f, "combo", "证件类型", "{妻子证件类型}");
            Enter(h, f, "text", "证件号码", "{妻子证件号码}");
            Enter(h, f, "date", "出生年月", "{妻子出生年月}");
            Enter(h, f, "text", "年龄", "{妻子年龄}");
            Enter(h, f, "combo", "文化程度", "{妻子文化程度}");
            Enter(h, f, "combo", "职业", "{妻子职业}");
            Enter(h, f, "combo", "户口性质", "{妻子户口性质}");
            Enter(h, f, "text", "手机号码", "{妻子手机}");

            h = GetFieldSet(f, "丈夫户口所在地");
            Enter(h, f, "combo", "省", "{省(丈夫户籍)}");
            Enter(h, f, "combo", "市", "{市(丈夫户籍)}");
            Enter(h, f, "combo", "县", "{县(丈夫户籍)}");
            Enter(h, f, "text", "乡", "{镇(丈夫户籍)}");
            Enter(h, f, "text", "村", "{村(丈夫户籍)}");
            Enter(h, f, "text", "户口所在地", "{户籍地址(丈夫)}");

            h = GetFieldSet(f, "妻子户口所在地");
            Enter(h, f, "combo", "省", "{省(妻子户籍)}");
            Enter(h, f, "combo", "市", "{市(妻子户籍)}");
            Enter(h, f, "combo", "县", "{县(妻子户籍)}");
            Enter(h, f, "text", "乡", "{镇(妻子户籍)}");
            Enter(h, f, "text", "村", "{村(妻子户籍)}");
            Enter(h, f, "text", "户口所在地", "{户籍地址(妻子)}");

            h = GetFieldSet(f, "妻子现住址");
            Enter(h, f, "combo", "省", "{省(妻子现住址)}");
            Enter(h, f, "combo", "市", "{市(妻子现住址)}");
            Enter(h, f, "combo", "县", "{县(妻子现住址)}");
            Enter(h, f, "text", "乡", "{镇(妻子现住址)}");
            Enter(h, f, "text", "村", "{村(妻子现住址)}");


            Enter(f, f, "date", "结婚时间", "{结婚时间}");
            Enter(f, f, "text", "邮编", "{邮编}");
            Enter(f, f, "text", "座机号码", "{固定电话}");
            Enter(f, f, "text", "医师签名", "{医生签名}");
            Enter(f, f, "date", "填写日期", "{填写日期}");

            CloseTab("修改基础信息表");
            CloseTab("完善档案");
            CloseTab("临床医生系统");
            window.submitSys.popupMsg('完成档案: {丈夫姓名}, {妻子姓名} 的创建', true, "OpenDocTabForModify");
        }
    }, 1000);
});