// Tạo hàm query giống jQuery
function $(selector) {
    let elements;

    if (selector instanceof NodeList) {
        elements = selector;
    } else if (selector instanceof Node) {
        elements = [selector];
    } else {
        elements = document.querySelectorAll(selector);
    }

    return {
        // event handle
        on: function (event, handler) {
            if (event.endsWith("-single")) {
                const getEvent = event.split("-single")[0];
                elements.forEach(function (element) {
                    element.addEventListener(getEvent, () => {
                        handler(element);
                    });
                });
            } else if (Array.isArray(elements))
                elements[0].addEventListener(event, handler);
            else
                elements.forEach(function (element) {
                    element.addEventListener(event, handler);
                });
        },
        removeClass: function (className) {
            if (Array.isArray(elements))
                elements[0].classList.remove(className);
            else
                elements.forEach(function (element) {
                    element.classList.remove(className);
                });
        },
        toggleClass: function (className) {
            if (Array.isArray(elements))
                elements[0].classList.toggle(className);
            else
                elements.forEach(function (element) {
                    element.classList.toggle(className);
                });
        },
        addClass: function (className) {
            if (Array.isArray(elements)) elements[0].classList.add(className);
            else
                elements.forEach(function (element) {
                    element.classList.add(className);
                });
        },

        // return something
        get: () => {
            if (elements.length > 0) {
                return elements[0];
            } else return null;
        },
        getAll: () => {
            if (elements.length > 0) {
                return elements;
            } else return null;
        },
        isNull: () => {
            return elements.length === 0;
        },
        closest: function (selector) {
            if (elements.length > 0) {
                let element = elements[0];
                while (element && !element.matches(selector)) {
                    element = element.parentElement;
                }
                return $(element);
            } else return null;
        },
        next: function () {
            if (elements.length > 0) {
                return $(elements[0].nextElementSibling);
            } else return null;
        },
        prev: function () {
            if (elements.length > 0) {
                return $(elements[0].previousElementSibling);
            } else return null;
        },
        tagName: function () {
            if (elements.length > 0) {
                return elements[0].tagName.toLowerCase();
            } else return null;
        },
    };
}

function executeScript() {
    navLinksActive();
    toggleSidebarMobile();
    navLinksClick();
}

function resetCSSLogin() {
    removeAllStylesheets();
    loadCss("/css/site.css");
    loadCss("/css/flowbite.css");
}

function executeLoginScript() {
    login();
    removeAllStylesheets();
    loadCss("/css/utilLogin.css");
    loadCss("/css/login.css");
}

function toggleSidebarMobile() {
    $("#toggleSidebarMobile, #sidebarBackdrop, #toggleSidebarMobileSearch").on(
        "click",
        handleSidebarToggle
    );
}

function handleSidebarToggle() {
    const sidebar = $("#sidebar").get();

    if (sidebar) {
        $(sidebar).toggleClass("hidden");
        $(
            "#sidebarBackdrop, #toggleSidebarMobileHamburger, #toggleSidebarMobileClose"
        ).toggleClass("hidden");
    }
}

function navLinksActive() {
    const navLinksActive = $(".nav-link.active");

    if (!navLinksActive.isNull()) {
        var parentUI = navLinksActive.closest("ul");
        if (
            parentUI.next().tagName() === "button" ||
            parentUI.prev().tagName() === "button"
        ) {
            parentUI.toggleClass("hidden");
        }
    }
}

function navLinksClick() {
    const button = $('button[type="button"][aria-controls^="dropdown-"]');
    if (!button.isNull() && button.next().tagName() === "ul") {
        button.on("click-single", navLinkClickHandler);
    }

    toggleNavClick("apps-dropdown");
    toggleNavClick("notification-dropdown");
}

function navLinkClickHandler(element) {
    $(element).next().toggleClass("hidden");
}

function login() {
    "use strict";
    function i(t) {
        if (
            t.getAttribute("type") === "email" ||
            t.getAttribute("name") === "email"
        ) {
            if (
                t.value
                    .trim()
                    .match(
                        /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/
                    ) === null
            )
                return false;
        } else if (t.getAttribute("name") === "username") {
            var i = t.value.trim();
            if (i.indexOf("@") !== -1 && i.indexOf("@ueh.edu.vn") === -1)
                return false;
        } else if (t.value.trim() === "") return false;
    }
    function r(t) {
        var i = t.parentElement;
        i.classList.add("alert-validate");
    }
    function u(t) {
        var i = t.parentElement;
        i.classList.remove("alert-validate");
    }
    document.querySelectorAll("input.input100").forEach(function (element) {
        element.addEventListener("blur", function () {
            element.value.trim() !== ""
                ? element.classList.add("has-val")
                : element.classList.remove("has-val");
        });
    });
    document
        .querySelector("#txtusername")
        .addEventListener("keyup", function () {
            var t = this,
                i = t.value;
            i.indexOf("@") !== -1
                ? t.parentElement.classList.remove("txtemail")
                : t.parentElement.classList.add("txtemail");
        });
    var t = document.querySelectorAll(".validate-input .input100");
    // Chặn submit
    // document
    //     .querySelector(".validate-form")
    //     .addEventListener("submit", function () {
    //         for (var u = true, n = 0; n < t.length; n++)
    //             i(t[n]) === false && (r(t[n]), (u = false));
    //         return u;
    //     });
    document
        .querySelectorAll(".validate-form .input100")
        .forEach(function (element) {
            element.addEventListener("focus", function () {
                u(this);
            });
        });
    if (document.querySelector("#forgotpassword") !== null) {
        document
            .querySelector("#forgotpassword")
            .addEventListener("click", function (event) {
                event.preventDefault();
            });
    }
}

function loadScript(scriptPath) {
    var script = document.createElement("script");
    script.src = scriptPath;

    document.head.append(script);
}

function loadCss(cssPath) {
    var link = document.createElement("link");
    link.rel = "stylesheet";
    link.href = cssPath;

    document.head.append(link);
}

function removeAllStylesheets() {
    var links = document.querySelectorAll('link[rel="stylesheet"]');
    for (var i = 0; i < links.length; i++) {
        links[i].parentNode.removeChild(links[i]);
    }
}

function toggleNavClick(id) {
    const selector = 'button[type="button"][data-dropdown-toggle="' + id + '"]';
    const button = $(selector);

    if (!button.isNull()) {
        button.on("click", () => $("#" + id).toggleClass("hidden"));
    }
}

// authenticate

function userLogin(token, minutes) {
    token = encodeMessage(token);
    updateCookie("data-user", token, minutes);
}

function userInfo() {
    token = getCookie("data-user");
    if (token !== null) {
        token = decodeMessage(token);
        return token;
    } else return null;
}

function userExtend(minutes) {
    token = getCookie("data-user");
    if (token !== null) {
        updateCookie("data-user", token, minutes);
    }
}

function userLogout() {
    deleteCookie("data-user");
}

// base function login

function getCookie(name) {
    const cookieName = name + "="; // The name of the cookie to search for
    const cookies = document.cookie.split(";"); // Splitting all the cookies into an array
    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i].trim(); // Trim any whitespace from the cookie string
        if (cookie.indexOf(cookieName) == 0) {
            // Check if the cookie starts with the desired name
            return cookie.substring(cookieName.length); // Extract and return the value of the cookie
        }
    }
    return null; // Return null if the cookie is not found
}

function updateCookie(name, value, minutes) {
    var expires = "";
    if (minutes) {
        var date = new Date();
        date.setTime(date.getTime() + minutes * 60 * 1000);
        expires = "; expires=" + date.toGMTString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function deleteCookie(name) {
    document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";
}

function encodeMessage(message) {
    return btoa(message);
}

function decodeMessage(encodedMessage) {
    return atob(encodedMessage);
}

//

function showElement(selector) {
    const element = $(selector);

    if (!element.isNull()) {
        element.removeClass("hidden");
    }
}

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement("a");
    anchorElement.href = url;
    anchorElement.download = fileName ?? "";
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
};

window.downloadFileExcel = (fileName, content, contentType) => {
    const blob = new Blob([content], { type: contentType });
    const url = URL.createObjectURL(blob);

    const anchorElement = document.createElement("a");
    anchorElement.href = url;
    anchorElement.download = fileName ?? "";
    anchorElement.click();

    URL.revokeObjectURL(url);
};

window.downloadFileFromByteArray = function (fileName, byteArray) {
    const blob = new Blob([byteArray], { type: "application/zip" });
    const url = URL.createObjectURL(blob);
    const link = document.createElement("a");
    link.href = url;
    link.download = fileName ?? "file.zip";
    link.click();
    URL.revokeObjectURL(url);
};

window.downloadFileFromPDf = function (fileName, byteArray) {
    const blob = new Blob([byteArray], { type: "application/pdf" });
    const url = URL.createObjectURL(blob);
    const link = document.createElement("a");
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
};

function getDeviceWidth() {
    return window.innerWidth;
}
