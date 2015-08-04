Ext.onReady(function() {
    var i = setInterval(function() {
        if (ClickTreeNode(document, "个人信息操作")) {
            window.clearInterval(i);
            if (ClickTreeNode(document, "档案管理")) {
                window.submitSys.login("档案管理帐号登陆,可进行档案管理,检验开单，评估报告等操作", "Admin");
                return;
            }

            if (ClickTreeNode(document, "检验系统")) {
                window.submitSys.login("临床检验系统帐号登陆,可进行临床检验报告完善操作", "Clinical");
                return;
            }
        }
    }, 1000);
});