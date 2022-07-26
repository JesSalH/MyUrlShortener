﻿
var urlInput = document.querySelector("#urlShortener");
var submitBtn = document.querySelector("#submitBtn");
var outcome = document.querySelector("#outcome");

submitBtn.onclick = async function (ev) {
    let url = urlInput.value;
    let stringifiedUrl = JSON.stringify(url)
    stringifiedUrl = stringifiedUrl.replace(/\\\\/g, '\\');

    if (!validateURL(url)) {
        OutcomeMessage("Could not validate url");
        return null;
    }
    let response = await fetch("/", {
        method: "POST",
        body: stringifiedUrl,
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (response.ok) {
        let json = await response.json();
        OutcomeMessage("Url shortened: " + new URL(window.location) + json)
    } else if (response.status == 500) {
        let json = await response.json();
        OutcomeMessage(json.message);
    } else {
        OutcomeMessage("Could not shorten url");
    }
}

function validateURL(str) {
    var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
        '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
        '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
        '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
        '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
        '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator
    return !!pattern.test(str);
}

function OutcomeMessage(text) {
    outcome.classList.remove("inactive");
    outcome.classList.add("active");
    outcome.innerHTML = text;
}