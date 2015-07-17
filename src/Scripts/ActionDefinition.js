{
    "login_url" : "http://mcm3.pc.e-health.org.cn/login/ssoLogin_doctor.action",
    "steps" : [
        {
            "frame_url" : "entity/manager/index.jsp",
            "script" : "OpenNewDocumentTab.js",
            "pre_status" : "Initialize",
            "next_status" : "OpenNewDocumentTab"
        },
        {
            "frame_url" : "/entity/basic/prePregnancyService.action",
            "script" : "ClickNewDocumentButton.js",
            "pre_status" : "OpenNewDocumentTab",
            "next_status" : "ClickNewDocumentButton"
        },
        {
            "frame_url" : "/entity/archives/basicInfoDetail_input.action?",
            "script" : "EnterNewDocumentBasicInfo.js",
            "pre_status" : "ClickNewDocumentButton",
            "next_status" : "EnterNewDocumentBasicInfo",
            "has_data" : true
        }
     ]
}