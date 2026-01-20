// ~/js/Pages/QueueInspect/Page_Load.js

window.QI_CHOICES = window.QI_CHOICES || {};

window.destroyChoice = function (sel) {
    const inst = window.QI_CHOICES[sel];
    if (inst && typeof inst.destroy === "function") {
        try { inst.destroy(); } catch (e) { }
    }
    delete window.QI_CHOICES[sel];
};

window.createChoice = function (sel, opts) {
    const el = document.querySelector(sel);
    if (!el) return null;

    // Destroy existing Choices instance to avoid duplicate initialization
    window.destroyChoice(sel);

    const inst = new Choices(el, Object.assign({
        removeItemButton: true,
        searchEnabled: true,
        searchResultLimit: 100,
        shouldSort: false,
        allowHTML: false,
        itemSelectText: "",
        placeholder: true,
        placeholderValue: "Select...",
        noChoicesText: "No choices available",
        noResultsText: "No results found",
        loadingText: "Loading..."
    }, opts || {}));

    // Store instance globally for later access
    window.QI_CHOICES[sel] = inst;
    return inst;
};

function QI_InitBUG(done) {
    window.createChoice("#ddl_BUG", { placeholderValue: "Select BUG" });
    if (typeof done === "function") done();
}

function QI_InitCSResponse(done) {
    createChoice("#ddl_CS_Response", { placeholderValue: "Select CS Response" });
    if (typeof done === "function") done();
}

function QI_InitUnitStatusCS(done) {
    createChoice("#ddl_Unit_Status_CS", { placeholderValue: "Select Unit Status CS" });
    if (typeof done === "function") done();
}

function QI_InitExpectTransferBy(done) {
    createChoice("#ddl_Expect_Transfer_By", { placeholderValue: "Select Expect Transfer By" });
    if (typeof done === "function") done();
}

function QI_InitInspectRound(done) {
    createChoice("#ddl_Inspect_Round", { placeholderValue: "Select Inspection Round" });
    if (typeof done === "function") done();
}

let fpRegisterStart = null;
let fpRegisterEnd = null;

document.addEventListener("DOMContentLoaded", () => {

    fpRegisterStart = flatpickr("#txt_RegisterDateStart", {
        dateFormat: "d/m/Y",
        altInput: false,
        allowInput: true,
        onChange: function (selectedDates) {
            if (fpRegisterEnd && selectedDates.length) {
                fpRegisterEnd.set("minDate", selectedDates[0]);
            }
        }
    });

    fpRegisterEnd = flatpickr("#txt_RegisterDateEnd", {
        dateFormat: "d/m/Y",
        altInput: false,
        allowInput: true
    });

});

document.addEventListener("DOMContentLoaded", function () {
    const toggles = document.querySelectorAll('[data-bs-toggle="collapse"][data-bs-target]');

    toggles.forEach(btn => {
        const targetSel = btn.getAttribute('data-bs-target');
        const target = document.querySelector(targetSel);
        if (!target) return;

        // icon inside THIS button only
        const icon = btn.querySelector('i');
        if (!icon) return;

        // Sync icon on show/hide
        target.addEventListener('show.bs.collapse', () => {
            icon.classList.remove('fa-chevron-down');
            icon.classList.add('fa-chevron-up');
            btn.setAttribute('aria-expanded', 'true');
        });

        target.addEventListener('hide.bs.collapse', () => {
            icon.classList.remove('fa-chevron-up');
            icon.classList.add('fa-chevron-down');
            btn.setAttribute('aria-expanded', 'false');
        });

        // Initial state
        const isShown = target.classList.contains('show');
        icon.classList.toggle('fa-chevron-up', isShown);
        icon.classList.toggle('fa-chevron-down', !isShown);
        btn.setAttribute('aria-expanded', String(isShown));
    });
});