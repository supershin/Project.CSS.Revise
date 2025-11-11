// Works for ALL collapse buttons on the page
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

document.addEventListener("DOMContentLoaded", () => {
    const btnCustomerView = document.getElementById("btnCustomerView");
    if (btnCustomerView && typeof customerViewUrl !== "undefined") {
        btnCustomerView.addEventListener("click", function () {
            window.open(customerViewUrl, "_blank");
        });
    }
});
document.addEventListener("DOMContentLoaded", () => {
    const btnCounter = document.getElementById("btnCounter");
    if (btnCounter && typeof QueueBankCheckerViewUrl !== "undefined") {
        btnCounter.addEventListener("click", function () {
            window.open(QueueBankCheckerViewUrl, "_blank");
        });
    }
});


// /js/Pages/QueueBank/QueueBank.js

// ==== Guard: Choices assets loaded? ====
if (typeof Choices === "undefined") {
    console.warn("Choices.js not found. Include choices.min.js and choices.min.css in _LayoutSite.");
}

// keep instances here (keyed by selector)
window.QB_CHOICES = window.QB_CHOICES || {};

function destroyChoice(sel) {
    const inst = window.QB_CHOICES[sel];
    if (inst && typeof inst.destroy === "function") {
        try { inst.destroy(); } catch { }
    }
    delete window.QB_CHOICES[sel];
}

function createChoice(sel, opts) {
    const el = document.querySelector(sel);
    if (!el) return null;
    destroyChoice(sel);
    const inst = new Choices(el, Object.assign({
        removeItemButton: true,
        searchEnabled: true,
        searchResultLimit: 100,
        shouldSort: false,
        allowHTML: false,
        itemSelectText: "",
        placeholder: true,
        placeholderValue: "Select…",
        noChoicesText: "ไม่มีตัวเลือก",
        noResultsText: "ไม่พบข้อมูล",
        loadingText: "กำลังโหลด…"
    }, opts || {}));
    window.QB_CHOICES[sel] = inst;
    return inst;
}

// ===== Init all dropdowns =====
function initFilterDropdowns() {
    createChoice("#ddl_BUG", { placeholderValue: "Select BUG…" });
    createChoice("#ddl_Project", { placeholderValue: "Select Project…" });
    createChoice("#ddl_UnitStatusCS", { placeholderValue: "Select Unit Status (CS)..." });
    createChoice("#ddl_CustomerStatus", { placeholderValue: "Select Customer Status…" });
    createChoice("#ddl_CSResponsible", { placeholderValue: "Select CS Responsible…" });
    createChoice("#ddl_UnitCode", { placeholderValue: "Select Unit Code…" });
    createChoice("#ddl_ExpectTransferBy", { placeholderValue: "Select Expect Transfer By…" });
}

// ===== Helpers =====
function qbGetValues() {
    const get = (sel) => (window.QB_CHOICES[sel]?.getValue(true)) || [];
    return {
        BUG: get("#ddl_BUG"),
        Project: get("#ddl_Project"),
        UnitStatusCS: get("#ddl_UnitStatusCS"),
        CustomerStatus: get("#ddl_CustomerStatus"),
        CSResponsible: get("#ddl_CSResponsible"),
        UnitCode: get("#ddl_UnitCode"),
        ExpectTransferBy: get("#ddl_ExpectTransferBy"),
        RegisterDateStart: document.getElementById("txt_RegisterDateStart")?.value || "",
        RegisterDateEnd: document.getElementById("txt_RegisterDateEnd")?.value || ""
    };
}

function qbSetValues(map) {
    const set = (sel, vals) => {
        const inst = window.QB_CHOICES[sel];
        if (!inst) return;
        try {
            inst.removeActiveItems();
            (vals || []).forEach(v => inst.setChoiceByValue(String(v)));
        } catch { }
    };
    if (map?.BUG) set("#ddl_BUG", map.BUG);
    if (map?.Project) set("#ddl_Project", map.Project);
    if (map?.UnitStatusCS) set("#ddl_UnitStatusCS", map.UnitStatusCS);
    if (map?.CustomerStatus) set("#ddl_CustomerStatus", map.CustomerStatus);
    if (map?.CSResponsible) set("#ddl_CSResponsible", map.CSResponsible);
    if (map?.UnitCode) set("#ddl_UnitCode", map.UnitCode);
    if (map?.ExpectTransferBy) set("#ddl_ExpectTransferBy", map.ExpectTransferBy);
    if (typeof map?.RegisterDateStart === "string") document.getElementById("txt_RegisterDateStart").value = map.RegisterDateStart;
    if (typeof map?.RegisterDateEnd === "string") document.getElementById("txt_RegisterDateEnd").value = map.RegisterDateEnd;
}

function ClearFilter() {
    // clear multi-selects
    Object.keys(window.QB_CHOICES).forEach(k => {
        try { window.QB_CHOICES[k].removeActiveItems(); } catch { }
    });
    // clear dates
    const s = document.getElementById("txt_RegisterDateStart");
    const e = document.getElementById("txt_RegisterDateEnd");
    if (s) s.value = "";
    if (e) e.value = "";
}

// ===== Events (Search / Cancel) =====
function wireButtons() {
    const btnSearch = document.getElementById("btnSearch");
    if (btnSearch) {
        btnSearch.addEventListener("click", () => {
            const filters = qbGetValues();
            // TODO: call your AJAX here
            console.log("Search with filters:", filters);
        });
    }
    const btnCancel = document.getElementById("btnFilterCancel");
    if (btnCancel) {
        btnCancel.addEventListener("click", () => {
            ClearFilter();
        });
    }
}

function openCreateRegister() {
    const m = new bootstrap.Modal(document.getElementById('modalCreateRegister'));
    m.show();
}

// Optional: init DataTables if available (won't error if not loaded)
document.addEventListener('DOMContentLoaded', function () {
    if (window.jQuery && $.fn.DataTable) {
        $('#crTable').DataTable({
            paging: true,
            pageLength: 5,
            lengthMenu: [5, 10, 25],
            searching: false,
            info: true,
            ordering: true,
            autoWidth: false
        });
    }
});

// Example Save click
document.addEventListener('click', function (e) {
    if (e.target.id === 'crBtnSave') {
        const code = document.getElementById('crUnitCode').value.trim();
        if (!code) { alert('Please enter Unit Code'); return; }
        // TODO: your save logic
        console.log('Save Register for Unit:', code);
    }
});


// ===== Boot =====
(function boot() {
    const run = () => {
        initFilterDropdowns();
        wireButtons();
    };
    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", run);
    else run();
})();

// (Optional) expose helpers
window.QueueBankFilters = { get: qbGetValues, set: qbSetValues, clear: ClearFilter };
