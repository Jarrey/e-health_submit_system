function TypeText(id, text) {
    var textbox = document.getElementById(id);
    if (textbox)
        textbox.setAttribute("value", text);
}

function TypeAccount(a, p) {
    TypeText("username", a);
    TypeText("password", p);
}
