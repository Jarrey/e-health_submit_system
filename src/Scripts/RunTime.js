function ClickTreeNode(root, nodeName) {
    while (true) {
        var treeNodes = root.getElementsByClassName('x-tree-node');
        for (var i in treeNodes) {
            if (treeNodes[i].getElementsByClassName) {
                var anchorNode = treeNodes[i].getElementsByClassName('x-tree-node-anchor')[0];
                if (anchorNode && anchorNode.innerText == nodeName) {
                    anchorNode.click();
                    return treeNodes[i];
                }
            }
        }
    }
}

function ClickTab(root, tabName) {
    while (true) {
        var tabs = root.getElementsByClassName('x-tab-strip-closable');
        for (var i in tabs) {
            if (tabs[i].getElementsByClassName) {
                var tab = tabs[i].getElementsByClassName('x-tab-strip-text')[0];
                if (tab && tab.innerText == tabName) {
                    tab.click();
                    return tabs[i];
                }
            }
        }
    }
}

function ClickToolbarButton(root, buttonName) {
    while (true) {
        var buttons = root.getElementsByTagName('button');
        for (var i in buttons) {
            var button = buttons[i];
            if (button && button.innerText == buttonName) {
                button.click();
                return;
            }
        }
    }
}

function GetInputControl(root, type, labelName) {
    while (true) {
        var labels = root.getElementsByTagName("label");
        for(var i in labels) {
            if (labels[i] && labels[i].innerText && labels[i].innerText.indexOf(labelName) >= 0) {
                var p = labels[i].parentElement;
                if (p && p.getElementsByClassName) {
                    if (type == "ComboBox") {
                        var comboBox = p.getElementsByClassName('x-form-text x-form-field x-combo-noedit')[0];
                        if (comboBox) return comboBox;
                    }

                    if (type == "TextBox") {
                        var textbox = p.getElementsByClassName('x-form-text x-form-field')[0];
                        if (textbox) return textbox;
                    }
                }
            }
        }
    }
}

function SelectComboBoxItem(root, combobox, selectText) {
    combobox.nextElementSibling.click();
    var items = root.getElementsByClassName("x-combo-list-item");
    for (var i in items) {
        if (items[i] && items[i].innerText && items[i].innerText.indexOf(selectText) >= 0) {
            items[i].click();
        }
    }
}

function TypeTextBox(textbox, text) {
    textbox.value = text;
}

function Enter(root, type, labelName, value) {
    var control = GetInputControl(root, type, labelName);
    if (type == "ComboBox") SelectComboBoxItem(root, control, value);
    else if (type == "TextBox") TypeTextBox(control, value);
}

function GetFrame(frameKey) {
    while (true) {
        var iFrames = document.getElementsByTagName('iframe');
        for (var i in iFrames) {
            if (iFrames[i].src.indexOf(frameKey) >= 0) {
                return iFrames[i].contentDocument;
            }
        }
    }
}
