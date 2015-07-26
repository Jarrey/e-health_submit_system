function ClickTreeNode(r, n) {
    while (true) {
        var a = $(r).find('.x-tree-node-anchor:contains("' + n + '")');
        if (a.length > 0) {
            a[0].click();
            return a[0].closest('.x-tree-node');
        }
    }
}

function ClickTab(r, n) {
    while (true) {
        var t = $(r).find('.x-tab-strip-closable:contains("' + n + '")');
        if (t.length > 0) {
            t[0].click();
            return t[0];
        }
    }
}

function CloseTab(n) {
    while (true) {
        var tabs = Ext.getCmp('center');
        if (tabs.items && tabs.items.items && tabs.items.items.length > 0) {
            for (var i in tabs.items.items) {
                console.log(tabs.items.items[i]);
                if (tabs.items.items[i].title == n) {
                    closeTab(tabs.items.items[i].id);
                    return;
                }
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

function GetInputControl(r, f, t, n) {
    var ext = f.contentWindow.Ext;
    var item = $(r).contents().find('label:contains("' + n + '")').closest('.x-form-item');
    if (t == "combo") {
        var combo = item.find('.x-combo-noedit');
        if (combo.length > 0) {
            combo = combo[0];
            return ext.getCmp(combo.id);
        }
    }

    if (t == "text" || t == "date") {
        var txt = item.find('.x-form-field');
        if (txt.length > 0) {
            txt = txt[0];
            return ext.getCmp(txt.id);
        }
    }
}

function Enter(r, f, t, n, v) {
    try {
        var c = GetInputControl(r, f, t, n);
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
            v = /[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])/.exec(v)[0];
        }

        c.setValue(v);
    } catch (ex) {
        console.log(ex);
    }
}

function GetFrame(k) {
    while (true) {
        var f = $('iframe[src*="' + k + '"]');
        if (f.length > 0) {
            return f[0];
        }
    }
}
