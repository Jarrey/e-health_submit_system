function TypeText(id, text) {
    var textbox = document.getElementById(id);
    if (textbox)
        textbox.setAttribute("value", text);
}

TypeText("username", "");
TypeText("password", "");