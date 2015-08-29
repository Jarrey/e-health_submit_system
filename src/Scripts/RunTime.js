var MaxRetryTimes = 10;
var CompleteFlag = "0";
var Today = Ext.util.Format.date(new Date(), 'Y-m-d').toString();

function ClickTreeNode(r, n) {
    var a = $(r).find('.x-tree-node-anchor:contains("' + n + '")');
    if (a.length > 0) {
        a[0].click();
        return a[0].closest('.x-tree-node');
    } else {
        return undefined;
    }
}

function ClickTab(r, n, i) {
    i = typeof i !== 'undefined' ? i : 0;
    var t = $(r).find('.x-tab-strip').find('li:contains("' + n + '")');
    if (t.length > i) {
        t[i].click();
        return t[i];
    } else {
        return undefined;
    }
}

function CloseAllTabs() {
    var tabs = Ext.getCmp('center');
    if (tabs.items && tabs.items.items && tabs.items.items.length > 0) {
        for (var i in tabs.items.items) {
            // console.log(tabs.items.items[i]);
            closeTab(tabs.items.items[i].id);
        }
    }
}

function CloseTab(n) {
    var tabs = Ext.getCmp('center');
    if (tabs.items && tabs.items.items && tabs.items.items.length > 0) {
        for (var i in tabs.items.items) {
            // console.log(tabs.items.items[i]);
            if (tabs.items.items[i].title.indexOf(n) >= 0) {
                closeTab(tabs.items.items[i].id);
                return;
            }
        }
    }
}

function ClickButton(r, n) {
    var b = $(r).contents().find('button:contains("' + n + '")');
    if (b.length > 0) {
        b[0].click();
    }
}

function GetFieldSet(r, n) {
    var fs = $(r).contents().find('fieldset:contains("' + n + '")');
    if (fs.length > 0) {
        return fs[0];
    }
}

function GetInputControl(r, f, t, n, i) {
    var ext = f.contentWindow.Ext;
    var item = $(r).contents().find('label:contains("' + n + '")').closest('.x-form-item');
    if (t == "combo") {
        var combo = item.find('.x-combo-noedit');
        if (combo.length > i) {
            combo = combo[i];
            return ext.getCmp(combo.id);
        }
    }

    if (t == "text" || t == "date") {
        var txt = item.find('.x-form-text.x-form-field');
        if (txt.length > i) {
            txt = txt[i];
            return ext.getCmp(txt.id);
        }
    }

    if (t == "textarea") {
        var txtarea = item.find('.x-form-textarea.x-form-field');
        if (txtarea.length > i) {
            txtarea = txtarea[i];
            return ext.getCmp(txtarea.id);
        }
    }

    if (t == "radio") {
        var radio = item.find('input.x-form-radio');
        if (radio.length > i) {
            radio = radio[i];
            return ext.getCmp(radio.id);
        }
    }

    if (t == "check") {
        var check = item.find('input.x-form-checkbox.x-form-field');
        if (check.length > i) {
            check = check[i];
            return ext.getCmp(check.id);
        }
    }
}

function Enter(r, f, t, n, v, i) {
    var ext = f.contentWindow.Ext;
    i = typeof i !== 'undefined' ? i : 0;
    try {
        var c = GetInputControl(r, f, t, n, i);
        if (t == 'combo') {
            try {
                c.initValue();
                c.expand();
            } catch (ex) {
                console.log(ex);
            }
            var i = c.store.find(c.displayField, new RegExp('.*' + v + '.*'));
            var field = c.store.data.itemAt(i);
            if (field && field.data) {
                v = field.data[c.valueField];
            }
                c.collapse();
        }

        if (t == "date") {
            v = ext.util.Format.date(new Date(v), "Y-m-d");
        }

        c.setValue(v);
    } catch (ex) {
        console.log(ex);
    }
}

function GetFrame(k) {
    var f = $('iframe[src*="' + k + '"]');
    if (f.length > 0) {
        return f[0];
    } else {
        return undefined;
    }
}

function IsLoadingData(r) {
    var l = $(r).find('.x-mask-loading');
    return l && l.length > 0;
}

/* Grid data operations */
function CheckDataExist(r, d) {
    var g = $(r).find('.x-grid3');
    if (g.length > 0) {
        var td = $(g[0]).find('td:contains("' + d + '")');
        return td && td.length > 0;
    }
}

function SelectData(r, f, d) {
    var ext = f.contentWindow.Ext;
    var t = $(r).find('.x-toolbar');
    var rs = $(r).find('.x-grid3-row').find('tr');
    for (var i in rs) {
        if ($(rs[i]).find('td:contains("' + d + '")').length > 0) {
            if (t.length > 0) {
                var g = ext.getCmp(t[0].id).ownerCt;
                if (g && g.selModel) {
                    g.selModel.selectRow(i);
                }
            }
            break;
        }
    }
}
