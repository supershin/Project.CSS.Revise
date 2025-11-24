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

    // 🔥 NEW: multi-select ใน modal Create Register
    createChoice("#DDLUnitCode", { placeholderValue: "Select Unit Code for Register…" });

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

function loadQueueBankRegisterTable() {
    if (QueueBankRegisterTableDt) {
        // reload data โดยใช้ filter ปัจจุบัน
        QueueBankRegisterTableDt.ajax.reload();
    }
}


let QueueBankRegisterTableDt = null;
let CreateRegisterTableDt = null;   

function initQueueBankRegisterTable() {
    QueueBankRegisterTableDt = $('#QueueBankRegisterTable').DataTable({
        processing: true,
        serverSide: true,
        searching: true,   // เปิดช่อง search ของ DataTables
        lengthChange: true,
        pageLength: 10,
        ordering: false,
        autoWidth: false,
        scrollX: true,   // ⭐ เพิ่มบรรทัดนี้ถ้าอยากให้มี scrollbar แนวนอน
        ajax: function (data, callback, settings) {
            const filters = qbGetValues();

            let projectId = filters.Project;
            if (Array.isArray(projectId)) {
                projectId = projectId[0] || "";
            }

            // ⭐ รับค่าจากช่อง search ของ DataTables
            const searchValue = (data.search && data.search.value) ? data.search.value.trim() : "";

            const formData = new FormData();
            // ===== DataTables params =====
            formData.append("draw", data.draw);
            formData.append("start", data.start);
            formData.append("length", data.length);
            formData.append("SearchTerm", searchValue);   // ⭐ NEW: ส่งไป backend

            // ===== QueueBank filters =====
            formData.append("L_Act", "RegisterTable");
            formData.append("L_ProjectID", projectId || "");
            formData.append("L_RegisterDateStart", filters.RegisterDateStart || "");
            formData.append("L_RegisterDateEnd", filters.RegisterDateEnd || "");
            formData.append("L_UnitID", (filters.UnitCode || []).join(","));
            formData.append("L_CSResponse", (filters.CSResponsible || []).join(","));
            formData.append("L_UnitCS", (filters.UnitStatusCS || []).join(","));
            formData.append("L_ExpectTransfer", (filters.ExpectTransferBy || []).join(","));

            if (typeof showLoading === "function") {
                showLoading();
            }

            fetch(baseUrl + "QueueBank/GetlistRegisterTable", {
                method: "POST",
                body: formData
            })
                .then(r => r.json())
                .then(res => {
                    callback({
                        draw: res.draw,
                        recordsTotal: res.recordsTotal,
                        recordsFiltered: res.recordsFiltered,
                        data: res.data || []
                    });
                })
                .catch(err => {
                    console.error("GetlistRegisterTable fetch error:", err);
                    callback({
                        draw: data.draw,
                        recordsTotal: 0,
                        recordsFiltered: 0,
                        data: []
                    });
                })
                .finally(() => {
                    if (typeof hideLoading === "function") {
                        hideLoading();
                    }
                });
        },
        columns: [
            { data: "index", name: "index" },
            { data: "UnitCode", name: "UnitCode" },
            {
                data: null,
                name: "CustomerName",
                className: "customer-col",
                render: function (data, type, row) {

                    let badges = "";

                    // 🟩 WiseConnect
                    if (parseInt(row.LineUserContract_Count || "0") > 0) {
                        badges += `
                <span class="svc svc-green" data-title="WiseConnect">
                    <i class="fa fa-comment"></i>
                </span>`;
                    }

                    // 🟦 Register FinPlus
                    if (row.LoanDraftDate && row.LoanDraftDate.trim() !== "") {
                        badges += `
                <span class="svc svc-blue" data-title="Register FinPlus">R</span>`;
                    }

                    // 🟧 Summit Bank
                    if (row.LoanSubmitDate && row.LoanSubmitDate.trim() !== "") {
                        badges += `
                <span class="svc svc-orange" data-title="Summit Bank">B</span>`;
                    }

                    return `
            <div>
                ${row.CustomerName || ""}
                <span class="svc-badges">
                    ${badges}
                </span>
            </div>
        `;
                }
            },

            { data: "Appointment", name: "Appointment" },
            { data: "Status", name: "Status" },
            { data: "StatusTime", name: "StatusTime" },
            { data: "Counter", name: "Counter" },
            { data: "Unitstatus_CS", name: "Unitstatus_CS" },
            { data: "CSResponse", name: "CSResponse" },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: "text-end",
                render: function (data, type, row) {
                    return `
                        <button class="btn btn-icon btn-del"
                                data-id="${row.ID || ""}"
                                title="Delete">
                            <i class="fa fa-trash"></i>
                        </button>`;
                }
            }
        ],
        order: [[1, "asc"]]
    });
}

function initCreateRegisterTable() {
    if (CreateRegisterTableDt) {
        // ถ้าเคย init แล้ว แค่ reload ด้วย filter ปัจจุบัน
        CreateRegisterTableDt.ajax.reload();
        return;
    }

    CreateRegisterTableDt = $('#crTable').DataTable({
        processing: true,
        serverSide: true,     // ✅ ใช้ server-side เหมือน RegisterTable
        searching: true,
        lengthChange: true,
        pageLength: 10,
        ordering: false,
        autoWidth: false,
        scrollX: true,
        ajax: function (data, callback, settings) {

            const filters = qbGetValues();
            let projectId = filters.Project;
            if (Array.isArray(projectId)) {
                projectId = projectId[0] || "";
            }

            const formData = new FormData();
            // ===== DataTables params =====
            formData.append("draw", data.draw);
            formData.append("start", data.start);
            formData.append("length", data.length);
            formData.append("SearchTerm",
                (data.search && data.search.value)
                    ? data.search.value.trim()
                    : ""
            );

            // ===== Filters =====
            formData.append("L_ProjectID", projectId || "");

            if (typeof showLoading === "function") {
                showLoading();
            }

            fetch(baseUrl + "QueueBank/GetlistCreateRegisterTable", {
                method: "POST",
                body: formData
            })
                .then(r => r.json())
                .then(res => {
                    callback({
                        draw: res.draw,
                        recordsTotal: res.recordsTotal || 0,
                        recordsFiltered: res.recordsFiltered || 0,
                        data: res.data || []
                    });
                })
                .catch(err => {
                    console.error("GetlistCreateRegisterTable error:", err);
                    callback({
                        draw: data.draw,
                        recordsTotal: 0,
                        recordsFiltered: 0,
                        data: []
                    });
                })
                .finally(() => {
                    if (typeof hideLoading === "function") {
                        hideLoading();
                    }
                });
        },
        columns: [
            {
                data: null,
                name: "index",
                render: function (data, type, row, meta) {
                    // row index ในหน้านั้น ๆ
                    return meta.row + 1 + meta.settings._iDisplayStart;
                }
            },
            { data: "UnitCode", name: "UnitCode" },
            { data: "CustomerName", name: "CustomerName" },
            { data: "CSResponse", name: "CSResponse" },
            { data: "RegisterDate", name: "RegisterDate" },
            { data: "WaitDate", name: "WaitDate" },
            { data: "InprocessDate", name: "InprocessDate" },
            { data: "FastFixDate", name: "FastFixDate" },
            { data: "FixedDuration", name: "FixedDuration" },
            { data: "FastFixFinishDate", name: "FastFixFinishDate" },
            { data: "Done", name: "Done" },
            { data: "ReasonName", name: "ReasonName" },
            { data: "Counter", name: "Counter" },
            { data: "CreateBy", name: "CreateBy" },
            { data: "UpdateDate", name: "UpdateDate" },
            { data: "UpdateBy", name: "UpdateBy" }
        ],
        order: [[1, "asc"]]
    });
}


// เรียกตอน page load
//document.addEventListener("DOMContentLoaded", function () {
//    initQueueBankRegisterTable();
//});

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
            loadSummaryRegisterAll();   // ⭐ ดึงสรุปใหม่ตาม filter ทุกครั้งที่กด Search
            loadSummaryRegisterBank();
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
    const modalEl = document.getElementById('modalCreateRegister');
    const m = new bootstrap.Modal(modalEl);
    m.show();

    // โหลด Unit สำหรับ DDLUnitCode ตาม Project ที่เลือก
    loadUnitForRegisterBank();

    // init หรือ reload DataTable
    if (window.jQuery && $.fn.DataTable) {
        if (!CreateRegisterTableDt) {
            initCreateRegisterTable();
        } else {
            CreateRegisterTableDt.ajax.reload();
        }
    } else {
        console.warn("DataTables not loaded. Please include jquery.dataTables.js and css.");
    }
}



// Example Save click
document.addEventListener('click', function (e) {
    if (e.target.id === 'crBtnSave') {
        const inst = window.QB_CHOICES["#DDLUnitCode"];
        const selected = inst ? inst.getValue(true) : [];

        if (!selected || selected.length === 0) {
            alert('Please select at least 1 Unit Code');
            return;
        }

        console.log('Save Register for Units:', selected);
        // TODO: call API สำหรับ Save Register ตาม selected IDs
    }
});




// ===== Summary Register (Top 4 boxes) =====
// ===== Summary Register (ทั้ง 2 แถว) =====

// format ตัวเลขมูลค่าให้เป็น "xx.xx M"
function qbFormatValueM(raw) {
    if (raw == null || raw === "") return "0.00 M";
    const num = Number(raw);
    if (Number.isNaN(num)) return raw;

    const m = num / 1_000_000;

    return m.toLocaleString("en-US", {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    }) + " M";
}

function qbUpdateSummaryBox(prefix, data) {
    const unitEl = document.getElementById(`sum-${prefix}-unit`);
    const valueEl = document.getElementById(`sum-${prefix}-value`);
    const percentEl = document.getElementById(`sum-${prefix}-percent`);

    const unit = data?.Unit ?? "0";
    const value = data?.Value ?? "0";
    const percent = data?.Percent ?? "0";

    if (unitEl) unitEl.textContent = unit;
    if (valueEl) valueEl.textContent = qbFormatValueM(value);
    if (percentEl) percentEl.textContent = `${percent}%`;
}

// helper: map list ตาม Topic (lowercase + trim)
function qbMapByTopic(list) {
    const map = {};
    (list || []).forEach(x => {
        const key = (x.Topic || "").trim().toLowerCase();
        if (key) map[key] = x;
    });
    return map;
}

function loadSummaryRegisterAll() {
    const filters = qbGetValues();

    let projectId = filters.Project;
    if (Array.isArray(projectId)) {
        projectId = projectId[0] || "";
    }

    const formData = new FormData();
    // ==== QueueBank filters ====
    formData.append("L_Act", "SummeryRegisterType"); // controller จะ clone model เองอยู่แล้ว
    formData.append("L_ProjectID", projectId || "");
    formData.append("L_RegisterDateStart", filters.RegisterDateStart || "");
    formData.append("L_RegisterDateEnd", filters.RegisterDateEnd || "");
    formData.append("L_UnitID", (filters.UnitCode || []).join(","));
    formData.append("L_CSResponse", (filters.CSResponsible || []).join(","));
    formData.append("L_UnitCS", (filters.UnitStatusCS || []).join(","));
    formData.append("L_ExpectTransfer", (filters.ExpectTransferBy || []).join(","));

    // QueueTypeID หน้า Bank = 48
    formData.append("L_QueueTypeID", "48");

    // dataTables params (SP ไม่ได้ใช้ แต่ model ต้องมี)
    formData.append("draw", "1");
    formData.append("start", "0");
    formData.append("length", "10");
    formData.append("SearchTerm", "");

    if (typeof showLoading === "function") {
        showLoading();
    }

    fetch(baseUrl + "QueueBank/GetlistSummeryRegister", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            // 1) Type: Register / Queue / In Process / Done
            const typeList = res.listDataSummeryRegisterType || [];
            const typeMap = qbMapByTopic(typeList);

            qbUpdateSummaryBox("register", typeMap["register"]);
            qbUpdateSummaryBox("queue", typeMap["queue"]);
            qbUpdateSummaryBox("inprocess", typeMap["in process"]);
            qbUpdateSummaryBox("done", typeMap["done"]);

            // 2) LoanType: ยื่น / ไม่ยื่น
            const loanList = res.listDataSummeryRegisterLoanTyp || [];
            const loanMap = qbMapByTopic(loanList);

            // key เป็นภาษาไทยใน Topic จาก SP
            qbUpdateSummaryBox("loan-yes", loanMap["ยื่น"]);
            qbUpdateSummaryBox("loan-no", loanMap["ไม่ยื่น"]);

            // 3) CareerType: รายได้ประจำ / เจ้าของกิจการ / อาชีพอิสระ
            const careerList = res.listDataSummeryRegisterCareerTyp || [];
            const careerMap = qbMapByTopic(careerList);

            qbUpdateSummaryBox("career-freelance", careerMap["อาชีพอิสระ"]);
            qbUpdateSummaryBox("career-salary", careerMap["รายได้ประจำ"]);
            qbUpdateSummaryBox("career-owner", careerMap["เจ้าของกิจการ"]);
        })
        .catch(err => {
            console.error("GetlistSummeryRegister error:", err);
            // ถ้า error ให้เคลียร์ทุกกล่องเป็น 0 กันค่าค้าง

            // แถวบน
            qbUpdateSummaryBox("register", null);
            qbUpdateSummaryBox("queue", null);
            qbUpdateSummaryBox("inprocess", null);
            qbUpdateSummaryBox("done", null);

            // แถวล่าง loan
            qbUpdateSummaryBox("loan-yes", null);
            qbUpdateSummaryBox("loan-no", null);

            // แถวล่าง career
            qbUpdateSummaryBox("career-freelance", null);
            qbUpdateSummaryBox("career-salary", null);
            qbUpdateSummaryBox("career-owner", null);
        })
        .finally(() => {
            if (typeof hideLoading === "function") {
                hideLoading();
            }
        });
}

// ===== Summary Bank (table) =====

function loadSummaryRegisterBank() {
    const filters = qbGetValues();

    let projectId = filters.Project;
    if (Array.isArray(projectId)) {
        projectId = projectId[0] || "";
    }

    const formData = new FormData();

    // ==== QueueBank filters (เหมือนตัวอื่น) ====
    formData.append("L_Act", "SummeryRegisterBank");
    formData.append("L_ProjectID", projectId || "");
    formData.append("L_RegisterDateStart", filters.RegisterDateStart || "");
    formData.append("L_RegisterDateEnd", filters.RegisterDateEnd || "");
    formData.append("L_UnitID", (filters.UnitCode || []).join(","));
    formData.append("L_CSResponse", (filters.CSResponsible || []).join(","));
    formData.append("L_UnitCS", (filters.UnitStatusCS || []).join(","));
    formData.append("L_ExpectTransfer", (filters.ExpectTransferBy || []).join(","));

    // Queue type ของหน้า Bank = 48
    formData.append("L_QueueTypeID", "48");

    // ==== DataTables params (ให้ model ครบเฉย ๆ) ====
    formData.append("draw", "1");
    formData.append("start", "0");
    formData.append("length", "1000"); // เอามาหมดพอใช้ summary
    formData.append("SearchTerm", "");

    if (typeof showLoading === "function") {
        showLoading();
    }

    fetch(baseUrl + "QueueBank/GetlistSummeryRegisterBank", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            const tbody = document.getElementById("summary-bank-body");
            if (!tbody) return;

            // ⚠️ controller คืนชื่อ property ว่า listDataSummeryRegisterType
            const list = res.listDataSummeryRegisterType || [];

            if (!list.length) {
                tbody.innerHTML = `
                    <tr>
                        <td colspan="5" class="text-center text-muted">No data</td>
                    </tr>`;
                return;
            }

            const rowsHtml = list.map(item => {
                const bankCode = (item.BankCode || "").trim();
                const bankName = item.BankName || "";
                const unit = item.Unit || "0";          // จำนวนยูนิต
                const valueText = qbFormatValueM(item.Value); // มูลค่า → xx,xxx.xx M
                const percentText = (item.Percent || "0") + "%";

                // ช่องธนาคาร: ถ้า BankCode = 'No data' ไม่ต้องโชว์โลโก้
                let bankCellHtml = "";
                if (bankCode && bankCode.toLowerCase() !== "no data") {
                    bankCellHtml = `
                        <div class="d-flex align-items-center gap-2">
                            <img src="${baseUrl}image/ThaiBankicon/${bankCode}.png"
                                 alt="${bankCode}"
                                 class="bank-logo">
                            <span>${bankName || bankCode}</span>
                        </div>`;
                } else {
                    bankCellHtml = `<span>${bankName || "No data"}</span>`;
                }

                // ตอนนี้คอลัมน์ "อัตราดอกเบี้ยเฉลี่ย 3 ปี" ยังไม่มีข้อมูลจาก SP
                // เลยใส่ "Waiting ask user" ไว้เหมือน mock เดิม
                return `
                    <tr>
                        <td>${bankCellHtml}</td>
                        <td>Waiting ask user</td>
                        <td>${unit}</td>
                        <td>${valueText}</td>
                        <td>${percentText}</td>
                    </tr>`;
            }).join("");

            tbody.innerHTML = rowsHtml;
        })
        .catch(err => {
            console.error("GetlistSummeryRegisterBank error:", err);
            const tbody = document.getElementById("summary-bank-body");
            if (tbody) {
                tbody.innerHTML = `
                    <tr>
                        <td colspan="5" class="text-center text-danger">
                            Error loading Summary Bank
                        </td>
                    </tr>`;
            }
        })
        .finally(() => {
            if (typeof hideLoading === "function") {
                hideLoading();
            }
        });
}


// โหลด Unit สำหรับใช้ใน modal Create Register (DDLUnitCode)
function loadUnitForRegisterBank() {
    const filters = qbGetValues();
    let projectId = filters.Project;
    if (Array.isArray(projectId)) {
        projectId = projectId[0] || "";
    }

    const ddlInst = window.QB_CHOICES["#DDLUnitCode"];
    if (!ddlInst) return;

    // ถ้าไม่มี project → clear choices แล้วไม่เรียก API
    if (!projectId) {
        try {
            ddlInst.removeActiveItems();
            ddlInst.clearChoices();
        } catch { }
        return;
    }

    const formData = new FormData();
    formData.append("ProjectID", projectId);

    if (typeof showLoading === "function") showLoading();

    fetch(baseUrl + "QueueBank/GetListUnitForRegisterBankTable", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            const list = res.ListUnitForRegisterBankTable || [];

            ddlInst.clearChoices();
            ddlInst.setChoices(
                list.map(u => ({
                    value: u.ID,        // GUID
                    label: u.UnitCode,  // text แสดง
                    selected: false
                })),
                "value",
                "label",
                true
            );
        })
        .catch(err => {
            console.error("GetListUnitForRegisterBankTable error:", err);
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}



// ===== Boot =====
(function boot() {
    const run = () => {
        initFilterDropdowns();
        wireButtons();
        loadProjectsByBU();   // NEW: initial load (L_BUID = "" → all projects)
        loadUnitsByProject();  // initial: ยังไม่มี Project -> เคลียร์ Unit

        // ⭐️ IMPORTANT: init the main QueueBank DataTable
        if (window.jQuery && $.fn.DataTable) {
            initQueueBankRegisterTable();
        } else {
            console.warn("DataTables not loaded. Please include jquery.dataTables.js and css.");
        }

        // ⭐ โหลด summary ครั้งแรกตอนเปิดหน้า (ใช้ค่า default filter)
        loadSummaryRegisterAll();
        loadSummaryRegisterBank();
    };
    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", run);
    else run();
})();

// (Optional) expose helpers
window.QueueBankFilters = { get: qbGetValues, set: qbSetValues, clear: ClearFilter };
