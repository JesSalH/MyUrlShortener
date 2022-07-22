
var urlInput = document.querySelector("#urlShortener");
var submitBtn = document.querySelector("#submitBtn");
var outcome = document.querySelector("#outcome");

submitBtn.onclick = function (ev) {
    let url = urlInput.value;
    let stringifiedUrl = JSON.stringify(url)
    stringifiedUrl = stringifiedUrl.replace(/\\\\/g, '\\');

    if (!validateURL(url)) {
        OutcomeMessage("Could not validate url");
        return null;
    }
    fetch("/", {
        method: "POST",
        body: stringifiedUrl,
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => res.json())
        .then(response => {
            if (response.result == "OK") {
                OutcomeMessage("Url shortened: " + new URL(window.location) + response.code )
            } else if (response.result == "Fail") {
                OutcomeMessage(response.message);
            }
        }).catch(error => console.log(error));
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