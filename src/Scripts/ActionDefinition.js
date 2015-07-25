{
    "login_url" : "http://mcm3.pc.e-health.org.cn/login/ssoLogin_doctor.action",
    "steps" :
    {
        "OpenNewDocumentTab" : {
            "frame_url" : "entity/manager/index.jsp",
            "script" : "OpenNewDocumentTab.js",
            "pre_status" : 0,
            "next_status" : 1
        },
        "ClickNewDocumentButton" : {
            "frame_url" : "/entity/basic/prePregnancyService.action",
            "script" : "ClickNewDocumentButton.js",
            "pre_status" : 1,
            "next_status" : 2
        },
        "EnterNewDocumentBasicInfo" : {
            "frame_url" : "/entity/archives/basicInfoDetail_input.action?",
            "script" : "EnterNewDocumentBasicInfo.js",
            "pre_status" : 2,
            "next_status" : 3,
            "has_data" : true
        }
    }
}