﻿{
    "login_url" : "http://mcm3.pc.e-health.org.cn/login/ssoLogin_doctor.action",
    "steps" :
    {
        "OpenDocumentTab" : {
            "frame_url" : "entity/manager/index.jsp",
            "script" : "OpenDocumentTab.js",
            "pre_status" : "Init"
        },
        "ClickNewDocumentButton" : {
            "frame_url" : "/entity/basic/prePregnancyService.action",
            "script" : "ClickNewDocumentButton.js",
            "pre_status" : "OpenDocTabForNew",
            "next_status" : "ClickNewDoc",
            "has_data" : true,
            "increase" : true
        },
        "EnterNewDocumentBasicInfo" : {
            "frame_url" : "/entity/archives/basicInfoDetail_input.action?",
            "script" : "EnterNewDocumentBasicInfo.js",
            "pre_status" : "ClickNewDoc",
            "next_status" : "Init",
            "has_data" : true
        },
        "ClickModifyDocumentButton" : {
            "frame_url" :  "/entity/basic/prePregnancyService.action",
            "script" : "ClickModifyDocumentButton.js",
            "pre_status" : "OpenDocTabForModify",
            "next_status" : "ClickModifyDoc",
            "has_data" : true,
            "increase" : true
        },
        "ClickModifyDocumentBasicInfo" : {
            "frame_url" : "/entity/archives/editArchives.action?",
            "script" : "ClickModifyDocumentBasicInfo.js",
            "pre_status" : "ClickModifyDoc",
            "next_status" : "ClickModifyDoc"
        },
        "ModifyDocumentBasicInfo" : {
            "frame_url" : "/entity/archives/basicInfoDetail_input.action?",
            "script" : "ModifyDocumentBasicInfo.js",
            "pre_status" : "ClickModifyDoc",
            "next_status" : "Init",
            "has_data" : true
        }
    }
}