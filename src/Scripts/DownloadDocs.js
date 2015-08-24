var DownloadDocs = function () {
    var f = GetFrame("/entity/basic/prePregnancyService.action");
    var d = f.contentDocument;
    var dataTable = "[";

    var rows = $(d).contents().find(".x-grid3-body").find(".x-grid3-row");
    if (!rows || rows.length == 0) {
        window.submitSys.popupMsg("没有找到任何记录, 请先搜索", false, "");
        return;
    }

    var headers = [];
    // Read header of grid
    var hs = $(d).contents().find(".x-grid3-header").find("td.x-grid3-hd");
    for (var hi = 0; hi < hs.length; hi++) {
        h = hs[hi];
        headers.push($(h).text());
    }

    for (var i = 0; i < rows.length; i++) {
        var r = rows[i];
        var cols = $(r).find("td.x-grid3-col");
        var data = "{";
        for (var j = 0; j < headers.length; j++) {
            var h = headers[j].replace(/\s/g, '');
            var c = cols[j];
            if (h != "")
                data += "\"" + h + "\": \"" + $(c).text() + "\", ";
        }
        dataTable += data +"}, ";
    }
    dataTable += "]";

    window.submitSys.continue(dataTable);
}();