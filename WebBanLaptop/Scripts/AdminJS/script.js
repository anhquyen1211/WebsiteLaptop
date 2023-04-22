const body = document.querySelector("body"),
    sidebar = body.querySelector(".sidebars"),
    toggle = body.querySelector(".toggle"),
    searchBtn = body.querySelector(".search-box"),
    modeSwitch = body.querySelector(".toggle-switch"),
    modeText = body.querySelector(".mode-text");

toggle.addEventListener("click", () => {
    sidebar.classList.toggle("close");
    toggle.classList.toggle("bx-chevron-right");
    toggle.classList.toggle("bx-chevron-left");
});

searchBtn.addEventListener("click", () => {
    sidebar.classList.remove("close");
});

modeSwitch.addEventListener("click", () => {
    body.classList.toggle("dark");
    if (body.classList.contains("dark")) {
        modeText.innerHTML = "Light Mode";
    }
    else {
        modeText.innerHTML = "Dark Mode";

    }
});