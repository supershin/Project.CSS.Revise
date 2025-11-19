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


function loadProjectsByBU() {
    const bugInst = window.QB_CHOICES["#ddl_BUG"];
    const projInst = window.QB_CHOICES["#ddl_Project"];
    if (!projInst) return;

    const selectedBU = bugInst ? bugInst.getValue(true) : [];
    const l_buid = (selectedBU && selectedBU.length > 0)
        ? selectedBU.join(",")
        : "";

    const formData = new FormData();
    formData.append("L_BUID", l_buid);

    fetch(baseUrl + "QueueBank/GetProjectListByBU", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            if (!res || !res.success) return;

            const projects = res.data || [];

            projInst.clearChoices();

            projInst.setChoices(
                projects.map(p => ({
                    value: p.ProjectID,
                    label: p.ProjectNameTH,
                    selected: false
                })),
                "value",
                "label",
                true
            );

            // 🔥 NEW: BU เปลี่ยน -> ล้าง Unit ทั้งหมด
            const unitInst = window.QB_CHOICES["#ddl_UnitCode"];
            if (unitInst) {
                try {
                    unitInst.removeActiveItems();
                    unitInst.clearChoices();
                } catch { }
            }
        })
        .catch(err => console.error("loadProjectsByBU error:", err));
}


// ===== Load UnitCodes by Project =====
function loadUnitsByProject() {
    const projInst = window.QB_CHOICES["#ddl_Project"];
    const unitInst = window.QB_CHOICES["#ddl_UnitCode"];
    if (!unitInst) return;

    let selectedProject = projInst ? projInst.getValue(true) : null;

    // ถ้า Choices คืน array -> ใช้อันแรก (เราใช้ Project เป็น single-select)
    if (Array.isArray(selectedProject)) {
        selectedProject = selectedProject[0] || "";
    }

    // ❗ ถ้าไม่มี project เลย -> เคลียร์ทั้ง choice + selection ของ Unit แล้วจบ
    if (!selectedProject) {
        try {
            unitInst.removeActiveItems(); // ล้าง tag ที่เลือกอยู่
            unitInst.clearChoices();      // ล้างตัวเลือกทั้งหมด
        } catch { }
        return;
    }

    const formData = new FormData();
    formData.append("ProjectID", selectedProject);

    fetch(baseUrl + "QueueBank/GetlistUnitByProject", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            if (!res || !res.success) return;

            const units = res.data || [];

            // ล้าง options เดิม
            unitInst.clearChoices();

            // เติม Unit ใหม่จาก API
            unitInst.setChoices(
                units.map(u => ({
                    value: u.ID,
                    label: u.UnitCode,
                    selected: false
                })),
                "value",
                "label",
                true
            );
        })
        .catch(err => console.error("loadUnitsByProject error:", err));
}

// ===== Init all dropdowns =====
function initFilterDropdowns() {
    const bugInst = createChoice("#ddl_BUG", { placeholderValue: "Select BUG…" });
    const projInst = createChoice("#ddl_Project", { placeholderValue: "Select Project…" });
    createChoice("#ddl_UnitStatusCS", { placeholderValue: "Select Unit Status (CS)..." });
    createChoice("#ddl_CustomerStatus", { placeholderValue: "Select Customer Status…" });
    createChoice("#ddl_CSResponsible", { placeholderValue: "Select CS Responsible…" });
    createChoice("#ddl_UnitCode", { placeholderValue: "Select Unit Code…" });
    createChoice("#ddl_ExpectTransferBy", { placeholderValue: "Select Expect Transfer By…" });

    // ✅ สำคัญ: BUG เปลี่ยน -> โหลด Project ใหม่ (รวมเคส deselect ทั้งหมดด้วย)
    if (bugInst) {
        const bugEl = bugInst.passedElement.element; // original <select>
        bugEl.addEventListener("change", () => {
            loadProjectsByBU();
        });
    }
    // ✅ Project เปลี่ยน -> โหลด UnitCode ใหม่
    if (projInst) {
        const projEl = projInst.passedElement.element;
        projEl.addEventListener("change", () => {
            loadUnitsByProject();
        });
    }
}

// ===== Call API & Render QueueBankRegisterTable =====
function loadQueueBankRegisterTable() {
    const filters = qbGetValues();

    // Project เป็น single select → เอาค่าแรกพอ
    let projectId = filters.Project;
    if (Array.isArray(projectId)) {
        projectId = projectId[0] || "";
    }

    const formData = new FormData();
    formData.append("L_Act", "RegisterTable");
    formData.append("L_ProjectID", projectId || "");
    formData.append("L_RegisterDateStart", filters.RegisterDateStart || "");
    formData.append("L_RegisterDateEnd", filters.RegisterDateEnd || "");
    formData.append("L_UnitID", (filters.UnitCode || []).join(","));
    formData.append("L_CSResponse", (filters.CSResponsible || []).join(","));
    formData.append("L_UnitCS", (filters.UnitStatusCS || []).join(","));
    formData.append("L_ExpectTransfer", (filters.ExpectTransferBy || []).join(","));

    // ถ้ามี loading overlay ให้ใช้ด้วย
    if (typeof showLoading === "function") {
        showLoading();
    }

    fetch(baseUrl + "QueueBank/GetlistRegisterTable", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            if (!res || res.success !== true) {
                console.error("GetlistRegisterTable error:", res);
                renderQueueBankRegisterTable([]); // เคลียร์ table
                return;
            }
            const rows = res.Listdata || [];
            renderQueueBankRegisterTable(rows);
        })
        .catch(err => {
            console.error("GetlistRegisterTable fetch error:", err);
            renderQueueBankRegisterTable([]);
        })
        .finally(() => {
            if (typeof hideLoading === "function") {
                hideLoading();
            }
        });
}

// ===== Render rows to table =====
function renderQueueBankRegisterTable(rows) {
    const tbody = document.getElementById("QueueBankRegisterTableBody");
    const summaryEl = document.getElementById("QueueBankRegisterTableSummary");

    if (!tbody) return;

    tbody.innerHTML = "";

    if (!rows || rows.length === 0) {
        const tr = document.createElement("tr");
        tr.innerHTML = `<td colspan="10" class="text-center text-muted">No data found</td>`;
        tbody.appendChild(tr);

        if (summaryEl) {
            summaryEl.textContent = "Showing 0 to 0 of 0 entries";
        }
        return;
    }

    rows.forEach((r, idx) => {
        const no = r.index || (idx + 1); // เผื่อมี index จาก SP / reader

        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${no}</td>
            <td>${r.UnitCode || ""}</td>
            <td class="customer-col">
                ${r.CustomerName || ""}
            </td>
            <td>${r.Appointment || ""}</td>
            <td>${r.Status || ""}</td>
            <td>${r.StatusTime || ""}</td>
            <td>${r.Counter || ""}</td>
            <td>${r.Unitstatus_CS || ""}</td>
            <td>${r.CSResponse || ""}</td>
            <td class="text-end">
                <button class="btn btn-icon btn-del" data-id="${r.ID || ""}" title="Delete">
                    <i class="fa fa-trash"></i>
                </button>
            </td>
        `;
        tbody.appendChild(tr);
    });

    if (summaryEl) {
        const total = rows.length;
        summaryEl.textContent = `Showing 1 to ${total} of ${total} entries`;
    }
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

    // สำคัญ: ตอนนี้ BUG = ว่าง -> L_BUID = "" -> loadProjectsByBU() = ทุก Project
    loadProjectsByBU();
    // เคลียร์ UnitCode เพราะยังไม่มี Project
    loadUnitsByProject();
}

// ===== Events (Search / Cancel) =====
function wireButtons() {
    const btnSearch = document.getElementById("btnSearch");
    if (btnSearch) {
        btnSearch.addEventListener("click", () => {
            loadQueueBankRegisterTable();
        });
    }

    const btnCancel = document.getElementById("btnFilterCancel");
    if (btnCancel) {
        btnCancel.addEventListener("click", () => {
            ClearFilter();
            // ถ้าหลังเคลียร์ filter อยากโหลด table new ด้วยก็ปลดคอมเมนต์
            // loadQueueBankRegisterTable();
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
        loadProjectsByBU();   // NEW: initial load (L_BUID = "" → all projects)
        loadUnitsByProject();  // initial: ยังไม่มี Project -> เคลียร์ Unit
    };
    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", run);
    else run();
})();

// (Optional) expose helpers
window.QueueBankFilters = { get: qbGetValues, set: qbSetValues, clear: ClearFilter };
