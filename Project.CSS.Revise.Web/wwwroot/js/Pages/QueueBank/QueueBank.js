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

// ==============================
// Open Customer View
// ==============================
document.addEventListener("DOMContentLoaded", () => {

    const btnCustomerView = document.getElementById("btnCustomerView");

    if (btnCustomerView && typeof QueueBankCustomerViewUrl !== "undefined") {

        btnCustomerView.addEventListener("click", function () {

            // 1) ดึง projectId จาก Choices.js ผ่าน qbGetValues()
            const filters = qbGetValues();
            let projectId = filters.Project;

            if (Array.isArray(projectId)) {
                projectId = projectId[0] || "";
            }

            // ถ้ายังไม่ได้เลือกโปรเจกต์ → ห้ามกด
            if (!projectId) {
                if (typeof Swal !== "undefined") {
                    Swal.fire({
                        icon: "error",
                        title: "Validation Error",
                        text: "Please select a project before open Customer view.",
                        buttonsStyling: false,
                        confirmButtonText: "OK",
                        customClass: {
                            confirmButton: "btn btn-danger"
                        },
                        allowOutsideClick: false,
                        didOpen: (popup) => {
                            popup.parentNode.style.zIndex = 200000;
                        }
                    });
                } else {
                    alert("Please select a project before open Customer view.");
                }
                return;
            }

            // 2) ดึงชื่อโปรเจกต์จาก select จริง (#ddl_Project)
            let projectName = "";
            const projSelect = document.getElementById("ddl_Project");
            if (projSelect && projSelect.selectedOptions.length > 0) {
                projectName = (projSelect.selectedOptions[0].textContent || "").trim();
            }

            if (!projectName) {
                projectName = "Project";
            }

            // 3) สร้าง URL สำหรับ Customer View (เหมือน Counter)
            const url =
                QueueBankCustomerViewUrl +
                "?projectId=" + encodeURIComponent(projectId) +
                "&projectName=" + encodeURIComponent(projectName);

            // 4) เปิดแท็บใหม่
            window.open(url, "_blank");
        });
    }
});

// ==============================
// Open Checker View
// ==============================
document.addEventListener("DOMContentLoaded", () => {
    const btnChecker = document.getElementById("btnChecker");

    if (btnChecker && typeof QueueBankCheckerViewUrl !== "undefined") {
        btnChecker.addEventListener("click", function () {

            // 1) เอา projectId จาก Choices.js (ฟังก์ชัน qbGetValues ที่พ่อใหญ่มีอยู่แล้ว)
            const filters = qbGetValues();
            let projectId = filters.Project;

            if (Array.isArray(projectId)) {
                projectId = projectId[0] || "";
            }

            // ถ้ายังไม่ได้เลือก Project → ไม่ให้ไปหน้า Counter
            if (!projectId) {
                if (typeof Swal !== "undefined") {
                    Swal.fire({
                        icon: "error",
                        title: "Validation Error",
                        text: "Please select a project before open Counter view.",
                        buttonsStyling: false,
                        confirmButtonText: "OK",
                        customClass: {
                            confirmButton: "btn btn-danger"
                        },
                        allowOutsideClick: false,
                        didOpen: (popup) => {
                            popup.parentNode.style.zIndex = 200000;
                        }
                    });
                } else {
                    alert("Please select a project before open Counter view.");
                }
                return;
            }

            // 2) เอา Project Name จาก select จริง (#ddl_Project)
            let projectName = "";
            const projSelect = document.getElementById("ddl_Project");
            if (projSelect && projSelect.selectedOptions.length > 0) {
                projectName = (projSelect.selectedOptions[0].textContent || "").trim();
            }

            // fallback ถ้าไม่มีชื่อ
            if (!projectName) {
                projectName = "Project";
            }

            // 3) ประกอบ URL ส่งไปหน้า Counter
            const url =
                QueueBankCheckerViewUrl +
                "?projectId=" + encodeURIComponent(projectId) +
                "&projectName=" + encodeURIComponent(projectName);

            // 4) เปิดหน้า Counter ในแท็บใหม่
            window.open(url, "_blank");
        });
    }
});

// ==============================
// Open Counter View
// ==============================
document.addEventListener("DOMContentLoaded", () => {
    const btnCounter = document.getElementById("btnCounter");

    if (btnCounter && typeof QueueBankCounterViewUrl !== "undefined") {
        btnCounter.addEventListener("click", function () {

            // 1) เอา projectId จาก Choices.js (ฟังก์ชัน qbGetValues ที่พ่อใหญ่มีอยู่แล้ว)
            const filters = qbGetValues();
            let projectId = filters.Project;

            if (Array.isArray(projectId)) {
                projectId = projectId[0] || "";
            }

            // ถ้ายังไม่ได้เลือก Project → ไม่ให้ไปหน้า Counter
            if (!projectId) {
                if (typeof Swal !== "undefined") {
                    Swal.fire({
                        icon: "error",
                        title: "Validation Error",
                        text: "Please select a project before open Counter view.",
                        buttonsStyling: false,
                        confirmButtonText: "OK",
                        customClass: {
                            confirmButton: "btn btn-danger"
                        },
                        allowOutsideClick: false,
                        didOpen: (popup) => {
                            popup.parentNode.style.zIndex = 200000;
                        }
                    });
                } else {
                    alert("Please select a project before open Counter view.");
                }
                return;
            }

            // 2) เอา Project Name จาก select จริง (#ddl_Project)
            let projectName = "";
            const projSelect = document.getElementById("ddl_Project");
            if (projSelect && projSelect.selectedOptions.length > 0) {
                projectName = (projSelect.selectedOptions[0].textContent || "").trim();
            }

            // fallback ถ้าไม่มีชื่อ
            if (!projectName) {
                projectName = "Project";
            }

            // 3) ประกอบ URL ส่งไปหน้า Counter
            const url =
                QueueBankCounterViewUrl +
                "?projectId=" + encodeURIComponent(projectId) +
                "&projectName=" + encodeURIComponent(projectName);

            // 4) เปิดหน้า Counter ในแท็บใหม่
            window.open(url, "_blank");
        });
    }
});


//document.querySelectorAll('#EditRegisterLog .er-status-btn').forEach(btn => {
//    btn.addEventListener('click', function () {
//        if (this.disabled) return;

//        // ✅ toggle เปิด/ปิดเอง ไม่ไปยุ่งกับปุ่มอื่น
//        this.classList.toggle('active');

//        console.log("Selected Status:", this.id, "Active:", this.classList.contains("active"));
//    });
//});

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
    if (!projInst) return Promise.resolve();

    const selectedBU = bugInst ? bugInst.getValue(true) : [];
    const l_buid = (selectedBU && selectedBU.length > 0) ? selectedBU.join(",") : "";

    const formData = new FormData();
    formData.append("L_BUID", l_buid);

    return fetch(baseUrl + "QueueBank/GetProjectListByBU", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            if (!res || !res.success) return;

            const projects = res.data || [];

            // ล้าง selections + choices เก่า
            projInst.removeActiveItems();
            projInst.clearChoices();

            // ✅ ใส่เฉพาะรายการ project (ไม่ต้องยัด placeholder ซ้ำ)
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

            // ✅ บังคับให้กลับไปโชว์ placeholder
            projInst.removeActiveItems();
            projInst.passedElement.element.value = "";

            // 🔥 BU เปลี่ยน -> ล้าง Unit
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

function setChoiceEnabled(selector, enabled) {
    const el = document.querySelector(selector);
    const inst = window.QB_CHOICES ? window.QB_CHOICES[selector] : null;

    if (el) el.disabled = !enabled;

    if (inst) {
        try {
            inst.passedElement.element.disabled = !enabled;
            if (enabled) inst.enable();
            else inst.disable();
        } catch (e) {
            console.warn("setChoiceEnabled error:", selector, e);
        }
    }
}


function clearChoice(selector) {
    const el = document.querySelector(selector);
    const inst = window.QB_CHOICES ? window.QB_CHOICES[selector] : null;

    if (inst) {
        try {
            inst.removeActiveItems();
        } catch (e) {
            console.warn("clearChoice (Choices) error:", selector, e);
        }
    }
    if (el) el.value = "";
}

/** Rule: Reason 50=ยื่น => disable non-submission + clear, 51=ไม่ยื่น => enable */
function syncNonSubmissionByReason(reasonValue) {
    const val = (reasonValue ?? "").toString().trim();

    const isSubmit = (val === "50");       // ยื่น
    const isNotSubmit = (val === "51");    // ไม่ยื่น

    if (isSubmit) {
        clearChoice("#ddl_BankNonSubmissionReason");
        setChoiceEnabled("#ddl_BankNonSubmissionReason", false);
    } else {
        // ค่าอื่นๆ + ไม่ยื่น => เปิดให้ใช้งาน
        setChoiceEnabled("#ddl_BankNonSubmissionReason", true);

        // ถ้าอยากบังคับว่า "ไม่ยื่น" เท่านั้นถึงใช้ได้จริง ให้เปิดด้วย isNotSubmit แทน
        // setChoiceEnabled("#ddl_BankNonSubmissionReason", isNotSubmit);
        // if (!isNotSubmit) clearChoice("#ddl_BankNonSubmissionReason");
    }
}



// ===== Init all dropdowns =====
function initFilterDropdowns() {
    const bugInst = createChoice("#ddl_BUG", { placeholderValue: "Select BUG…" });
    const projInst = createChoice("#ddl_Project", { placeholderValue: "Select Project…" });
    createChoice("#ddl_UnitStatusCS", { placeholderValue: "Select Unit Status (CS)..." });
    createChoice("#ddl_CustomerStatus", { placeholderValue: "Select Customer Status…" });
    createChoice("#ddl_CSResponsible", { placeholderValue: "Select CS Response…" });
    createChoice("#ddl_UnitCode", { placeholderValue: "Select Unit Code…" });
    createChoice("#ddl_ExpectTransferBy", { placeholderValue: "Select Expect Transfer By…" });

    createChoice("#DDLUnitCode", { placeholderValue: "Select Unit Code for Register…" });
    createChoice("#ddl_Responsible", { placeholderValue: "Select Response..." });
    createChoice("#ddl_Career", { placeholderValue: "Not specified" });
    createChoice("#ddl_Reason", { placeholderValue: "Select Reason..." });
    createChoice("#ddl_BankNonSubmissionReason", { placeholderValue: "Select Non-Submission Reason..." });

    // ✅ hook Reason -> NonSubmissionReason
    const reasonEl = document.querySelector("#ddl_Reason");
    if (reasonEl) {
        reasonEl.addEventListener("change", () => {
            syncNonSubmissionByReason(reasonEl.value);
        });

        // set initial state (ตอนหน้าเพิ่งโหลด)
        syncNonSubmissionByReason(reasonEl.value);
    }

    if (bugInst) {
        const bugEl = bugInst.passedElement.element;
        bugEl.addEventListener("change", () => {
            loadProjectsByBU();
        });
    }

    if (projInst) {
        const projEl = projInst.passedElement.element;
        projEl.addEventListener("change", () => {
            loadUnitsByProject();
        });
    }
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
            qbUpdateSummaryRegisterHeaderDate(); // ✅ เพิ่ม
        }
    });

    fpRegisterEnd = flatpickr("#txt_RegisterDateEnd", {
        dateFormat: "d/m/Y",
        altInput: false,
        allowInput: true,
        onChange: function () {
            qbUpdateSummaryRegisterHeaderDate(); // ✅ เพิ่ม
        }
    });

    // ✅ ตั้งค่า header ครั้งแรกตอนโหลดหน้า
    qbUpdateSummaryRegisterHeaderDate();

});




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
            {
                data: "UnitCode",
                name: "UnitCode",
                className: "text-center",
                render: function (data, type, row) {
                    if (type !== "display") return data;

                    const unitCode = data || "";
                    const id = row.ID || "";

                    return `
                            <a href="#" class="qb-unit-link"
                               data-unit="${unitCode}" data-id="${id}">
                               ${unitCode}
                            </a>`;
                }
            },
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
                        badges += `<span class="svc svc-blue" data-title="Register FinPlus">R</span>`;
                    }

                    // 🟧 Summit Bank
                    if (row.LoanSubmitDate && row.LoanSubmitDate.trim() !== "") {
                        badges += `<span class="svc svc-orange" data-title="Summit Bank">B</span>`;
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

            { data: "Appointment", name: "Appointment", className: "text-center" },
            //{
            //    data: "ReasonName",
            //    name: "ReasonName",
            //    className: "text-center",
            //    render: function (data, type, row) {

            //        if (type !== "display") return row.ReasonName;

            //        const reason = row.ReasonName || "";
            //        const remark = row.ReasonRemarkName || "";

            //        if (reason === "ไม่ยื่น" && remark) {
            //            return `
            //    <div>
            //        ${reason}
            //        <small class="text-muted d-block">${remark}</small>
            //    </div>
            //`;
            //        }

            //        return reason;
            //    }
            //},
            {
                data: "Status",
                name: "Status",
                className: "text-center",
                render: function (data, type) {
                    if (type !== "display") return data;

                    const status = (data || "").toString().trim();

                    if (status === "Register") {
                        return `<span style="color:red;">${status}</span>`;
                    }

                    if (status === "Inprocess") {
                        return `<span style="color:green;">${status}</span>`;
                    }

                    return `<span style="color:black;">${status}</span>`;
                }
            },
            { data: "StatusTime", name: "StatusTime" ,className: "text-center" },
            { data: "Counter", name: "Counter", className: "text-center" },
            { data: "Unitstatus_CS", name: "Unitstatus_CS", className: "text-center" },
            { data: "CSResponse", name: "CSResponse" },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: "text-center",
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

// 🗑️ Delete Register Log (using common confirmMessage)
$(document).on("click", ".btn-del", async function () {
    const id = $(this).data("id");

    if (!id) {
        errorToast("Invalid record ID");
        return;
    }

    const confirmed = await confirmMessage(
        "Do you want to delete this register log?",
        {
            title: "Confirm Delete",
            confirmText: "Delete",
            cancelText: "Cancel",
            icon: "warning"
        }
    );

    if (!confirmed) return;

    removeRegisterLog(id);
});

function removeRegisterLog(id) {
    const formData = new FormData();
    formData.append("ID", id);

    showLoading();

    fetch(baseUrl + "QueueBank/RemoveRegisterLog", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {

            if (res && res.result === "SUCCESS") {
                successToastV2("Deleted successfully");

                // 🔄 Reload DataTable (stay on same page)
                $('#QueueBankRegisterTable')
                    .DataTable()
                    .ajax.reload(null, false);
            }
            else if (res && res.result === "NOT_FOUND") {
                errorToast("Record not found");
            }
            else {
                errorToast("Delete failed");
            }
        })
        .catch(err => {
            console.error("RemoveRegisterLog error:", err);
            errorToast("System error occurred");
        })
        .finally(() => {
            hideLoading();
        });
}

// ===============================
// Create Register (DataTable)
// ===============================
function initCreateRegisterTable() {

    // ✅ already init -> just reload + re-calc width
    if (window.CreateRegisterTableDt) {
        window.CreateRegisterTableDt.ajax.reload(function () {
            window.CreateRegisterTableDt.columns.adjust().draw(false);
        }, false);
        return;
    }

    const $table = $('#crTable');

    // ✅ escape HTML (avoid XSS)
    function escapeHtml(str) {
        if (str === null || str === undefined) return '';
        return String(str)
            .replaceAll('&', '&amp;')
            .replaceAll('<', '&lt;')
            .replaceAll('>', '&gt;')
            .replaceAll('"', '&quot;')
            .replaceAll("'", '&#039;');
    }

    // ✅ tooltip text by status -> show date-time
    function getStatusDateText(row) {
        const st = (row.Status || '').toLowerCase();

        if (st.startsWith('reg')) return row.RegisterDate ? `Register at: ${row.RegisterDate}` : 'Register at: -';
        if (st.startsWith('inp')) return row.InprocessDate ? `Inprocess at: ${row.InprocessDate}` : 'Inprocess at: -';
        if (st.startsWith('do')) return row.Done ? `Done at: ${row.Done}` : 'Done at: -';

        return '';
    }

    // ✅ status pill class
    function getStatusClass(status) {
        const st = (status || '').toLowerCase();
        if (st.startsWith('reg')) return 'status-pill status-reg';
        if (st.startsWith('inp')) return 'status-pill status-proc';
        if (st.startsWith('do')) return 'status-pill status-done';
        return 'status-pill';
    }

    window.CreateRegisterTableDt = $table.DataTable({
        processing: true,
        serverSide: true,
        searching: true,
        lengthChange: true,
        pageLength: 10,
        ordering: false,

        autoWidth: false,
        scrollX: true,
        scrollCollapse: true,
        deferRender: true,

        ajax: function (data, callback, settings) {

            const filters = qbGetValues();
            let projectId = filters.Project;
            if (Array.isArray(projectId)) projectId = projectId[0] || "";

            const formData = new FormData();
            formData.append("draw", data.draw);
            formData.append("start", data.start);
            formData.append("length", data.length);
            formData.append("SearchTerm", (data.search && data.search.value) ? data.search.value.trim() : "");
            formData.append("L_ProjectID", projectId || "");
            formData.append("L_RegisterDateStart", filters.RegisterDateStart || "");
            formData.append("L_RegisterDateEnd", filters.RegisterDateEnd || "");

            if (typeof showLoading === "function") showLoading();

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
                    callback({
                        draw: data.draw,
                        recordsTotal: 0,
                        recordsFiltered: 0,
                        data: []
                    });
                })
                .finally(() => {
                    if (typeof hideLoading === "function") hideLoading();

                    // ✅ after ajax render, force width sync
                    setTimeout(() => window.CreateRegisterTableDt?.columns.adjust(), 0);
                    setTimeout(() => window.CreateRegisterTableDt?.columns.adjust(), 120);
                });
        },

        columns: [
            // No.
            {
                data: null,
                name: "index",
                width: "60px",
                render: function (data, type, row, meta) {
                    return meta.row + 1 + meta.settings._iDisplayStart;
                }
            },

            // New select fields
            {
                data: "UnitCode",
                name: "UnitCode",
                className: "tbody-center",
                defaultContent: "",
                render: function (data, type, row) {

                    // ✅ search/sort ใช้ text ปกติ
                    if (type !== "display") return data;

                    const unitCode = escapeHtml(data || "");
                    const id = escapeHtml(row.RegisterLogID || "");

                    return `<a href="#" class="qb-unit-link" data-id="${id}">${unitCode}</a>`;
                }
            },
            { data: "CustomerName", name: "CustomerName", defaultContent: "", render: (d, t) => t === "display" ? escapeHtml(d) : d },
            { data: "ResponsibleName", name: "ResponsibleName", defaultContent: "", render: (d, t) => t === "display" ? escapeHtml(d) : d },
            { data: "CSResponseName", name: "CSResponseName", defaultContent: "", render: (d, t) => t === "display" ? escapeHtml(d) : d },
            {
                data: "Status",
                name: "Status",
                defaultContent: "",               
                render: function (data, type, row) {
                    if (type !== "display") return data;

                    const statusText = escapeHtml(data || '');
                    const tip = escapeHtml(getStatusDateText(row));
                    const cls = getStatusClass(data);

                    return `
                                <div class="d-flex justify-content-center">
                                    <span class="cr-status ${cls}" title="${tip}">
                                        ${statusText}
                                    </span>
                                </div>`;
                }

            },
            {
                data: "Counter",
                name: "Counter",
                className: "tbody-center",   // 👈 เพิ่ม
                defaultContent: "",
                render: (d, t) => t === "display" ? escapeHtml(d) : d
            },
            {
                data: null,
                name: "ReasonName",
                defaultContent: "",
                className: "text-center",
                render: function (d, type, row) {

                    const reason = row.ReasonName || "";
                    const remark = row.ReasonRemarkName || "";

                    // ✅ สำหรับ search/sort → ต้องเป็น text ล้วน
                    if (type === "filter" || type === "sort") {
                        return reason + " " + remark;
                    }

                    // ✅ display → ใส่ HTML ได้
                    if (type === "display") {

                        if (reason === "ไม่ยื่น" && remark) {
                            return `
                    <div class="text-danger fw-semibold">
                        ${escapeHtml(reason)}
                        <small class="d-block text-muted fw-normal">
                            ${escapeHtml(remark)}
                        </small>
                    </div>
                `;
                        }

                        return escapeHtml(reason);
                    }

                    return reason;
                }
            },

            { data: "UpdateBy", name: "UpdateBy", defaultContent: "", render: (d, t) => t === "display" ? escapeHtml(d) : d }
        ]
        // ❌ no drawCallback needed (we use native title tooltip)
    });

    // ✅ when modal is fully visible -> recalc once more
    const modalEl = document.getElementById('modalCreateRegister');
    if (modalEl && !modalEl.dataset.crAlignBound) {
        modalEl.dataset.crAlignBound = "1";
        modalEl.addEventListener('shown.bs.modal', function () {
            window.CreateRegisterTableDt?.columns.adjust().draw(false);
        });
    }
}

// ✅ delegate click on UnitCode link (Create Register table)
$('#crTable').on('click', '.qb-unit-link', function (e) {
    e.preventDefault();

    const unitCode = this.getAttribute('data-unit') || "";
    const registerId = this.getAttribute('data-id') || "";

    // ✅ use same function + same modal as main table
    loadRegisterLogForEdit(registerId, unitCode);
});

document.getElementById('modalCreateRegister')
    ?.addEventListener('shown.bs.modal', () => {
        document.querySelectorAll('.modal-backdrop')
            .forEach(b => b.classList.add('cr-backdrop'));
    });


// delegate click on UnitCode link
$('#QueueBankRegisterTable').on('click', '.qb-unit-link', function (e) {
    e.preventDefault();

    const unitCode = this.getAttribute('data-unit') || "";
    const registerId = this.getAttribute('data-id') || "";

    // แค่เรียกโหลดข้อมูล → จะ bind + show modal ข้างในเอง
    loadRegisterLogForEdit(registerId, unitCode);
});

function loadRegisterLogForEdit(registerId, unitCode) {
    if (!registerId) {
        console.warn("No registerId to load.");
        return;
    }

    const formData = new FormData();
    formData.append("ID", registerId);    // match RegisterLog criteria.ID

    if (typeof showLoading === "function") showLoading();

    fetch(baseUrl + "QueueBank/RegisterLogInfo", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(json => {
            if (!json || !json.Success) {
                const msg = (json && json.Message)
                    ? json.Message
                    : "Unable to load RegisterLog data.";

                if (typeof Swal !== "undefined") {
                    Swal.fire("Error", msg, "error");
                } else if (window.Application && typeof Application.PNotify === "function") {
                    Application.PNotify(msg, "error");
                } else {
                    alert(msg);
                }
                return;
            }

            const data = json.Data || {};

            // If backend does not return UnitCode, use the one from table
            if (!data.UnitCode && unitCode) {
                data.UnitCode = unitCode;
            }

            bindRegisterLogModal(data);
        })
        .catch(err => {
            console.error("RegisterLogInfo error:", err);

            const msg = "An error occurred while loading RegisterLog data.";

            if (typeof Swal !== "undefined") {
                Swal.fire("Error", msg, "error");
            } else {
                alert(msg);
            }
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}

function bindRegisterLogModal(data) {
    const modalEl = document.getElementById("EditRegisterLog");
    if (!modalEl) return;

    const editModal = bootstrap.Modal.getOrCreateInstance(modalEl);

    // ===== set registerId / header =====
    modalEl.dataset.registerId = data.ID || "";

    const headerEl = document.getElementById("hUnitCode");
    if (headerEl) headerEl.textContent = data.UnitCode ? `Unit Code : ${data.UnitCode}` : "";

    // =========================
    // helper: set Choices single
    // =========================
    const setChoiceSingle = (selector, value) => {
        const val = (value === null || value === undefined) ? "" : String(value);

        const inst = window.QB_CHOICES ? window.QB_CHOICES[selector] : null;
        if (inst) {
            try {
                inst.removeActiveItems();
                if (val !== "") inst.setChoiceByValue(val);
            } catch (e) {
                console.warn("setChoiceSingle error:", selector, e);
            }
        } else {
            const el = document.querySelector(selector);
            if (el) el.value = val;
        }
    };

    // Responsible: ถ้าอยาก "เคารพ backend ก่อน" ใช้แบบนี้
    const responsibleValue =
        (data.ResponsibleID && String(data.ResponsibleID) !== "0")
            ? String(data.ResponsibleID)
            : (window.CURRENT_LOGIN_ID ? String(window.CURRENT_LOGIN_ID) : "");
    setChoiceSingle("#ddl_Responsible", responsibleValue);

    // Career
    let careerValue = "";
    if (data.CareerTypeID && String(data.CareerTypeID) !== "0") {
        careerValue = String(data.CareerTypeID);
    } else if (data.QuestionAnswersName) {
        const targetName = String(data.QuestionAnswersName).trim();
        const selectEl = document.querySelector("#ddl_Career");
        if (selectEl) {
            const options = selectEl.options;
            for (let i = 0; i < options.length; i++) {
                const optText = options[i].text ? options[i].text.trim() : "";
                if (optText === targetName) {
                    careerValue = options[i].value;
                    break;
                }
            }
        }
    }
    setChoiceSingle("#ddl_Career", careerValue);

    // Reason + NonSubmissionReason
    setChoiceSingle("#ddl_Reason", data.ReasonID);
    if (typeof syncNonSubmissionByReason === "function") {
        syncNonSubmissionByReason(data.ReasonID);
    }

    if (String(data.ReasonID || "") === "51") {
        setChoiceSingle("#ddl_BankNonSubmissionReason", data.ReasonRemarkID);
    } else {
        setChoiceSingle("#ddl_BankNonSubmissionReason", "");
    }

    // =========================
    // Status helpers
    // =========================
    const setStatusBtn = (id, on) => {
        const btn = document.getElementById(id);
        if (!btn) return;

        if (on) {
            btn.classList.add("active", "btn-success");
            btn.classList.remove("btn-secondary");
        } else {
            btn.classList.remove("active", "btn-success");
            btn.classList.add("btn-secondary");
        }
    };

    const getOn = (id) =>
        document.getElementById(id)?.classList.contains("active") === true;

    // init from backend
    setStatusBtn("FlagRegister", !!data.FlagRegister);
    setStatusBtn("FlagInprocess", !!data.FlagInprocess);
    setStatusBtn("FlagFinish", !!data.FlagFinish);

    // bind once
    if (modalEl.dataset.statusBound !== "1") {
        modalEl.dataset.statusBound = "1";

        document.getElementById("FlagRegister")?.addEventListener("click", () => {
            setStatusBtn("FlagRegister", !getOn("FlagRegister"));
        });

        document.getElementById("FlagInprocess")?.addEventListener("click", () => {
            const nextIn = !getOn("FlagInprocess");
            setStatusBtn("FlagInprocess", nextIn);
            if (!nextIn) setStatusBtn("FlagFinish", false);
        });

        document.getElementById("FlagFinish")?.addEventListener("click", () => {
            const nextDone = !getOn("FlagFinish");
            setStatusBtn("FlagFinish", nextDone);
            if (nextDone) setStatusBtn("FlagInprocess", true);
        });
    }

    // =========================
    // shown handler (cleanup each time)
    // =========================
    const onShown = () => {
        modalEl.removeEventListener("shown.bs.modal", onShown);

        // backdrop class
        const backdrops = document.querySelectorAll(".modal-backdrop");
        const lastBackdrop = backdrops[backdrops.length - 1];
        if (lastBackdrop) {
            lastBackdrop.classList.remove("cr-backdrop");
            lastBackdrop.classList.add("edit-backdrop");
        }

        // ensure z-index
        modalEl.style.zIndex = "1055";
        if (lastBackdrop) lastBackdrop.style.zIndex = "1050";

        // FinPlus banks
        if (typeof renderFinPlusBanks === "function") {
            renderFinPlusBanks(data);
        }

        // ✅ show submit date (if any)
        renderFinPlusSubmitDate(data);

        // ✅ render submit button (only if allowed)
        renderCustomerSubmitFinPlus(data);

        // ✅ Contract (show/hide by Done)
        renderContractBlock(data);

    };

    modalEl.addEventListener("shown.bs.modal", onShown);

    // show once (พอ) — อย่า show ซ้ำสองรอบแบบ backup
    editModal.show();
}

function renderFinPlusBanks(data) {
    const box = document.getElementById("finplusBankList");
    if (!box) return;

    box.innerHTML = "";

    const loanBankList = data?.LoanBankList;
    if (!Array.isArray(loanBankList) || loanBankList.length === 0) {
        box.innerHTML = `<div class="text-muted small">No bank selected.</div>`;
        return;
    }

    const isDone = !!data?.FlagFinish; // ✅ Done

    const items = loanBankList.slice(0, 5);

    let html = "";
    for (let i = 0; i < items.length; i++) {
        const bank = items[i] || {};
        const bankCode = bank.BankCode ? String(bank.BankCode).trim() : "";
        const loanBankId = bank.LoanBankID ? String(bank.LoanBankID) : "";

        if (!bankCode) continue;

        html += `
            <div class="bank-item" title="${bankCode}">
                <img src="${baseUrl}image/ThaiBankicon/${bankCode}.png"
                     alt="${bankCode}"
                     class="bank-logo">
                <span class="bank-code">${bankCode}</span>

                ${(!isDone && loanBankId)
                ? `<button type="button"
                               class="bank-remove"
                               data-loanbankid="${loanBankId}"
                               data-bankcode="${bankCode}"
                               aria-label="Remove ${bankCode}"
                               title="Remove ${bankCode}">×</button>`
                : ``}
            </div>
        `;
    }

    box.innerHTML = html;

    qbBindFinPlusRemoveOnce();
}


function qbBindFinPlusRemoveOnce() {
    const box = document.getElementById("finplusBankList");
    if (!box) return;

    if (box.dataset.removeBound === "1") return;
    box.dataset.removeBound = "1";

    box.addEventListener("click", async (e) => {
        const btn = e.target.closest(".bank-remove");
        if (!btn) return;

        const modalEl = document.getElementById("EditRegisterLog");
        const registerLogId = modalEl?.dataset?.registerId || "";
        const loanBankId = btn.dataset.loanbankid || "";
        const bankCode = btn.dataset.bankcode || "";

        if (!registerLogId || !loanBankId) {
            Swal.fire("Error", "Invalid RegisterLogID or LoanBankID.", "error");
            return;
        }

        const res = await Swal.fire({
            icon: "warning",
            title: "Remove bank?",
            text: `Remove ${bankCode} from FINPlus list?`,
            showCancelButton: true,
            confirmButtonText: "Remove",
            cancelButtonText: "Cancel"
        });
        if (!res.isConfirmed) return;

        btn.disabled = true;

        try {
            const fd = new FormData();
            fd.append("RegisterLogID", String(registerLogId)); // ✅ match model
            fd.append("LoanBankID", String(loanBankId));       // ✅ match model
            fd.append("BankCode", String(bankCode));           // optional

            const resp = await fetch(baseUrl + "QueueBank/RemoveFinPlusBank", {
                method: "POST",
                body: fd
            });

            const json = await resp.json();

            if (!json || json.Success !== true) {
                btn.disabled = false;
                Swal.fire("Error", json?.Message || "Remove failed", "error");
                return;
            }

            // ✅ remove pill from UI
            btn.closest(".bank-item")?.remove();

            // if empty -> show message
            if (box.querySelectorAll(".bank-item").length === 0) {
                box.innerHTML = `<div class="text-muted small">No bank selected.</div>`;
            }

            // ✅ re-check submit button rule (banks count affects it)
            qbRecalcSubmitButtonFromDom();

            Swal.fire("Success", "Bank removed.", "success");

        } catch (err) {
            console.error("RemoveFinPlusBank error:", err);
            btn.disabled = false;
            Swal.fire("Error", "An error occurred while removing the bank.", "error");
        }
    });
}


function qbRecalcSubmitButtonFromDom() {
    const slot = document.getElementById("finplusSubmitSlot");
    if (!slot) return;

    const modalEl = document.getElementById("EditRegisterLog");
    const registerLogId = modalEl?.dataset?.registerId || "";
    const loanId = modalEl?.dataset?.loanId || "";

    // ถ้ามี submit date card แสดงอยู่ = submit แล้ว -> ไม่ให้มีปุ่ม
    const hasSubmitDateCard = (document.getElementById("Show_Submit_FinplusDate")?.innerText || "").trim().length > 0;
    if (hasSubmitDateCard) {
        slot.innerHTML = "";
        return;
    }

    // เช็คจำนวน bank ที่เหลือใน DOM
    const bankCount = document.querySelectorAll("#finplusBankList .bank-item").length;

    // ถ้าไม่มี bank -> ไม่ render ปุ่ม
    if (bankCount === 0) {
        slot.innerHTML = "";
        return;
    }

    // ถ้ามี bank และยังไม่ submit -> render ปุ่ม submit
    slot.innerHTML = "";
    const btn = document.createElement("button");
    btn.type = "button";
    btn.className = "btn btn-dark mt-3 w-100";
    btn.textContent = "📤 Customer Submit FINPlus";
    btn.addEventListener("click", () => qbSubmitFinPlus(registerLogId, loanId));
    slot.appendChild(btn);
}



//function renderFinPlusBanks(loanBankList) {
//    const box = document.getElementById("finplusBankList");
//    if (!box) {
//        return;
//    }

//    box.innerHTML = "";

//    if (!Array.isArray(loanBankList) || loanBankList.length === 0) {
//        box.innerHTML = `<div class="text-muted small">No bank selected.</div>`;
//        return;
//    }

//    // แสดงสูงสุด 5 ธนาคารใน 1 แถว (ตาม requirement)
//    const items = loanBankList.slice(0, 5);

//    let html = "";
//    for (let i = 0; i < items.length; i++) {
//        const bankCode = (items[i] && items[i].BankCode) ? String(items[i].BankCode).trim() : "";
//        if (!bankCode) {
//            continue;
//        }

//        html += `
//                <div class="bank-item" title="${bankCode}">
//                    <img src="${baseUrl}image/ThaiBankicon/${bankCode}.png"
//                        alt="${bankCode}"
//                        class="bank-logo">
//                    <span class="bank-code">${bankCode}</span>
//                </div>
//            `;

//    }

//    box.innerHTML = html;
//}

function renderCustomerSubmitFinPlus(data) {
    const slot = document.getElementById("finplusSubmitSlot");
    if (!slot) return;

    slot.innerHTML = ""; // ล้างก่อนทุกครั้ง

    const submitDate = data?.Loan?.SubmitDate ?? null;
    const banks = data?.LoanBankList;

    const hasBanks = Array.isArray(banks) && banks.length > 0;
    const notSubmitted = submitDate === null; // ตรง Razor

    // ถ้า submit แล้ว หรือไม่มี bank → ไม่ render ปุ่ม
    if (!(notSubmitted && hasBanks)) return;

    const registerLogId = data?.ID || "";          // int
    const loanId = data?.Loan?.ID || "";           // Guid

    const btn = document.createElement("button");
    btn.type = "button";
    btn.className = "btn btn-dark mt-3 w-100";
    btn.innerHTML = `<i class="fa fa-paper-plane me-1"></i> Customer Submit FINPlus`;

    btn.dataset.registerLogId = String(registerLogId);
    btn.dataset.loanId = String(loanId);

    btn.addEventListener("click", () => qbSubmitFinPlus(btn.dataset.registerLogId, btn.dataset.loanId));

    slot.appendChild(btn);
}


async function saveEditRegisterLog() {
    const modalEl = document.getElementById("EditRegisterLog");
    if (!modalEl) return;

    const registerId = Number(modalEl.dataset.registerId || 0);

    /* =========================
       Helper
       ========================= */
    const getChoiceValue = (selector) => {
        const inst = window.QB_CHOICES ? window.QB_CHOICES[selector] : null;
        if (inst) {
            const val = inst.getValue(true);
            return val ? String(val) : "";
        }
        const el = document.querySelector(selector);
        return el ? (el.value || "") : "";
    };

    const isActive = (id) =>
        document.getElementById(id)?.classList.contains("active") ?? false;

    /* =========================
       Read values
       ========================= */
    const responsibleId = getChoiceValue("#ddl_Responsible");
    const careerTypeId = getChoiceValue("#ddl_Career");
    const reasonId = getChoiceValue("#ddl_Reason"); // 50/51 or ""
    const remarkId = getChoiceValue("#ddl_BankNonSubmissionReason");

    const flagRegister = isActive("FlagRegister");
    const flagInprocess = isActive("FlagInprocess");
    const flagFinish = isActive("FlagFinish");

    // ✅ Validate Finance only when Done
    const mustValidateFinance = flagFinish === true;

    /* =========================
       Validate
       ========================= */
    if (!careerTypeId) {
        Swal.fire("Warning", "Please select Career", "warning");
        return;
    }

    // ✅ Finance validate เฉพาะตอน Done
    if (mustValidateFinance) {
        if (!reasonId || reasonId === "0") {
            Swal.fire("Warning", "Please select Reason", "warning");
            return;
        }

        if (reasonId === "51" && (!remarkId || remarkId === "0")) {
            Swal.fire("Warning", "Please select Non-Submission Reason", "warning");
            return;
        }
    }

    /* =========================
       FormData (FromForm)
       ========================= */
    const fd = new FormData();

    fd.append("ID", registerId);
    fd.append("ResponsibleID", responsibleId || 0);
    fd.append("CareerTypeID", careerTypeId || 0);

    fd.append("FlagRegister", flagRegister);
    fd.append("FlagInprocess", flagInprocess);
    fd.append("FlagFinish", flagFinish);
    fd.append("ReasonID", reasonId);
    fd.append("ReasonRemarkID", remarkId);

    /* =========================
       POST
       ========================= */
    try {
        const res = await fetch(baseUrl + "QueueBank/SaveRegisterLog", {
            method: "POST",
            body: fd
        });

        const json = await res.json();

        if (!json.Success) {
            Swal.fire("Error", json.Message || "Save failed", "error");
            return;
        }

        Swal.fire("Success", json.Message, "success");

        bootstrap.Modal.getOrCreateInstance(modalEl).hide();

        // ✅ หลังปิด Edit -> เปิด Create แล้ว reload table
        modalEl.addEventListener('hidden.bs.modal', function onHidden() {
            modalEl.removeEventListener('hidden.bs.modal', onHidden);

            // 1) show Create modal อีกครั้ง (ถ้ามันถูกปิดอยู่)
            //const crEl = document.getElementById("modalCreateRegister");
            //if (crEl) {
            //    bootstrap.Modal.getOrCreateInstance(crEl).show();
            //}

            // 2) reload Create Register table
            if (window.CreateRegisterTableDt) {
                window.CreateRegisterTableDt.ajax.reload(function () {
                    window.CreateRegisterTableDt.columns.adjust().draw(false);
                }, false);
            } else if (typeof initCreateRegisterTable === "function") {
                // ถ้ายังไม่ init (กันพลาด)
                initCreateRegisterTable();
            }
        });

        if (typeof loadUnitsByProject === "function") {
            loadUnitsByProject();
        }
        const btn = document.getElementById("btnSearch");
        if (btn) {
            btn.click();
        } else {
            console.warn("btnSearch not found");
        }
    } catch (err) {
        console.error(err);
        Swal.fire("Error", "System error", "error");
    }
}


async function qbSubmitFinPlus(registerLogId, loanId) {

    if (!registerLogId || !loanId) {
        Swal.fire("Error", "Invalid register or loan id.", "error");
        return;
    }

    const confirm = await Swal.fire({
        icon: "warning",
        title: "Customer Submit FINPlus?",
        text: "This will submit FINPlus for this customer.",
        showCancelButton: true,
        confirmButtonText: "Yes, submit",
        cancelButtonText: "Cancel"
    });
    if (!confirm.isConfirmed) return;

    try {
        const fd = new FormData();
        fd.append("RegisterLogID", String(registerLogId));
        fd.append("ID", String(loanId));   // LoanModel.ID

        const resp = await fetch(baseUrl + "QueueBank/CustomerSubmitFinPlus", {
            method: "POST",
            body: fd
        });

        const json = await resp.json();

        if (!json || json.Success !== true) {
            Swal.fire("Error", json?.Message || "Submit failed", "error");
            return;
        }

        Swal.fire("Success", "FINPlus submitted.", "success");

        // ✅ ใช้เวลาปัจจุบันของ browser เป็น SubmitDate สำหรับ UI
        const nowIso = new Date().toISOString();

        // Render submit date
        renderFinPlusSubmitDate({
            Loan: { SubmitDate: nowIso }
        });

        // ลบปุ่มออก (เพราะ submit แล้ว)
        const slot = document.getElementById("finplusSubmitSlot");
        if (slot) slot.innerHTML = "";

    } catch (err) {
        console.error("CustomerSubmitFinPlus error:", err);
        Swal.fire("Error", "An error occurred while submitting FINPlus.", "error");
    }
}

function renderFinPlusSubmitDate(data) {
    const box = document.getElementById("Show_Submit_FinplusDate");
    if (!box) return;

    box.innerHTML = "";

    const submitDate = data?.Loan?.SubmitDate ?? null;
    if (!submitDate) return; // ไม่มีวัน → ไม่ต้องโชว์อะไร

    const show = qbFormatSubmitDate(submitDate);

    box.innerHTML = `
        <div class="finplus-submit-card mt-2">
            <div class="finplus-submit-title">
                <i class="fa fa-check-circle text-success"></i>
                FINPlus Submitted
            </div>
            <div class="finplus-submit-date">${show}</div>
            <div class="finplus-submit-sub">This customer has already submitted FINPlus.</div>
        </div>
    `;
}

function qbFormatSubmitDate(val) {
    if (!val) return "";

    const d = new Date(val);
    if (Number.isNaN(d.getTime())) return String(val);

    // show as: 15 Jan 2026, 13:25
    return new Intl.DateTimeFormat("en-GB", {
        day: "2-digit",
        month: "short",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    }).format(d);
}


function renderContractBlock(data) {
    const section = document.getElementById("Show_Contract_Number");
    const slot = document.getElementById("contractSlot");
    if (!section || !slot) return;

    slot.innerHTML = "";

    const isDone = !!data?.FlagFinish;

    if (!isDone) {
        section.style.display = "none";
        return;
    }

    section.style.display = "";

    const contractNo = (data?.ContractNumber || "").trim();
    const redirectUrlRaw = (data?.RedirectHousingLoan || "").trim();

    // ✅ validate url (basic)
    let redirectUrl = redirectUrlRaw;
    if (redirectUrl && !/^https?:\/\//i.test(redirectUrl) && !redirectUrl.startsWith("/")) {
        // ถ้า backend ส่งมาเป็นโดเมน/พาธแปลก ๆ ให้ prefix baseUrl ไว้กันพัง
        redirectUrl = baseUrl.replace(/\/+$/, "") + "/" + redirectUrl.replace(/^\/+/, "");
    }

    // Done แต่ไม่พบเลขสัญญา
    if (!contractNo) {
        slot.innerHTML = `
            <div class="contract-card mt-2">
                <div class="contract-title">
                    <i class="fa fa-exclamation-triangle text-warning"></i>
                    Contract missing
                </div>
                <div class="contract-sub">This register is marked as Done, but no contract number was found.</div>
            </div>
        `;
        return;
    }

    // Done + มี contract แต่ไม่มี redirect url
    if (!redirectUrl) {
        slot.innerHTML = `
            <div class="contract-card mt-2">
                <div class="contract-title">
                    Contract Number
                </div>
                <div class="contract-sub">No contract link is available.</div>
            </div>
        `;

        return;
    }

    // ✅ Normal: open new tab ไปอีกระบบพร้อม param (ตาม RedirectHousingLoan)
    slot.innerHTML = `
    <div class="contract-card mt-2">
        <div class="contract-title">
            📄 <strong>Contract Number</strong>
        </div>
        <div class="contract-sub">Open contract details in a new tab.</div>

        <div class="d-flex gap-2 flex-wrap">
            <a class="btn btn-primary"
               href="${redirectUrl}"
               target="_blank"
               rel="noopener">
                🔗 ${contractNo}
            </a>
        </div>
    </div>
`;


}

function getFlatpickrISO(fp) {
    if (!fp || !fp.selectedDates?.length) return "";
    const d = fp.selectedDates[0];

    // shift by timezone offset to keep the local date
    const local = new Date(d.getTime() - d.getTimezoneOffset() * 60000);
    return local.toISOString().split("T")[0];
}


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

        // 🔥 date จาก flatpickr
        RegisterDateStart: getFlatpickrISO(fpRegisterStart),
        RegisterDateEnd: getFlatpickrISO(fpRegisterEnd)
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

async function ClearFilter() {
    Object.keys(window.QB_CHOICES).forEach(k => {
        try { window.QB_CHOICES[k].removeActiveItems(); } catch { }
    });

    if (fpRegisterStart) fpRegisterStart.clear();
    if (fpRegisterEnd) fpRegisterEnd.clear();

    // ✅ รอให้ project โหลดใหม่ + ใส่ placeholder ก่อน
    await loadProjectsByBU();

    // ✅ project ยังไม่เลือก -> unit ต้อง clear แน่ๆ
    loadUnitsByProject();

    // ✅ รีเซ็ตหัวตารางหลังจาก select อยู่ใน state ใหม่แล้ว
    qbUpdateProjectTableHeader();
}



// ===== Events (Search / Cancel) =====
function wireButtons() {
    const btnSearch = document.getElementById("btnSearch");
    if (btnSearch) {
        btnSearch.addEventListener("click", () => {

            // ✅ ใช้ qbGetValues() อ่านค่า Project จาก Choices.js
            const filters = qbGetValues();
            let projectVal = filters.Project;
            let hasProject = false;

            if (Array.isArray(projectVal)) {
                // กรณีเป็น array (multi-select)
                hasProject = projectVal.length > 0 && projectVal[0] !== "";
            } else {
                // กรณีเป็น string (single-select)
                hasProject = !!(projectVal && projectVal.toString().trim() !== "");
            }

            // ✅ ผ่าน validation → ทำงานตามปกติ
            qbUpdateProjectTableHeader();
            loadQueueBankRegisterTable();
            loadSummaryRegisterAll();
            loadSummaryRegisterBank();
        });
    }

    const btnCancel = document.getElementById("btnFilterCancel");
    if (btnCancel) {
        btnCancel.addEventListener("click", async () => {
            await ClearFilter();
        });
    }

}

function qbUpdateProjectTableHeader() {
    const headerEl = document.getElementById("project-name-selected");
    const summaryEl1 = document.getElementById("Show_Name_selected");
    const summaryEl2 = document.getElementById("Show_Name_selected2");
    const selectEl = document.getElementById("ddl_Project");

    const defaultProjectTitle = "Project Table";
    const projectTitleDefaultWhenNone = "Project"; // (ถ้าอยากให้แสดงเป็น "Project" เฉยๆ)

    const prefix1 = "Summary Register Bank : ";
    const prefix2 = "Summary Bank & Non-Submission Reason : ";

    if (!headerEl) return;

    const val = (selectEl?.value || "").toString().trim();

    // ✅ ไม่เลือก Project -> reset
    if (!selectEl || val === "") {
        headerEl.textContent = defaultProjectTitle;

        if (summaryEl1) summaryEl1.textContent = prefix1;
        if (summaryEl2) summaryEl2.textContent = prefix2;

        return;
    }

    // ✅ ใช้ชื่อที่เลือกจริง
    const opt = selectEl.selectedOptions?.[0];
    const projectText =
        (opt?.textContent || "").trim() ||
        projectTitleDefaultWhenNone;

    headerEl.textContent = projectText;

    if (summaryEl1) summaryEl1.textContent = prefix1 + projectText;
    if (summaryEl2) summaryEl2.textContent = prefix2 + projectText;
}

function openCreateRegister() {
    // 1) เช็คก่อนว่ามี Project ไหม
    const filters = qbGetValues();
    let projectVal = filters.Project;
    let hasProject = false;

    if (Array.isArray(projectVal)) {
        hasProject = projectVal.length > 0 && projectVal[0] !== "";
    } else {
        hasProject = !!(projectVal && projectVal.toString().trim() !== "");
    }

    if (!hasProject) {
        // ❌ ยังไม่เลือก Project → เด้งเตือนแล้วหยุด
        if (typeof Swal !== "undefined") {
            Swal.fire({
                icon: "error",
                title: "Validation Error",
                text: "Please select a project before creating a register.",
                buttonsStyling: false,
                confirmButtonText: "OK",
                customClass: {
                    confirmButton: "btn btn-danger"
                },
                allowOutsideClick: false,
                didOpen: (popup) => {
                    popup.parentNode.style.zIndex = 200000;
                }
            });
        } else {
            alert("Please select a project before creating a register.");
        }
        return; // ❗ ไม่ต้องเปิด modal ถ้าไม่มี Project
    }

    // 2) ✅ ผ่านแล้ว → เปิด modal ตามปกติ
    const modalEl = document.getElementById('modalCreateRegister');
    if (!modalEl) {
        console.warn("❗ modalCreateRegister not found in DOM");
        return;
    }

    const m = new bootstrap.Modal(modalEl);
    m.show();

    // 3) โหลด Unit สำหรับ DDLUnitCode ตาม Project ที่เลือก
    loadUnitForRegisterBank();

    // 4) init หรือ reload DataTable
    if (window.jQuery && $.fn.DataTable) {
        if (!window.CreateRegisterTableDt) {
            initCreateRegisterTable();
        } else {
            window.CreateRegisterTableDt.ajax.reload(null, true);   // true = reset paging
        }
    } else {
        console.warn("DataTables not loaded. Please include jquery.dataTables.js and css.");
    }
}


document.addEventListener("DOMContentLoaded", function () {
    const btnSave = document.getElementById("crBtnSave");
    if (btnSave) {
        btnSave.addEventListener("click", crSaveCreateRegister);
    }
});

function crSaveCreateRegister() {
    const filters = qbGetValues();
    let projectId = filters.Project;
    if (Array.isArray(projectId)) {
        projectId = projectId[0] || "";
    }

    if (!projectId) {
        Swal.fire("แจ้งเตือน", "กรุณาเลือก Project ก่อนสร้าง Register", "warning");
        return;
    }

    const ddl = document.getElementById("DDLUnitCode");
    if (!ddl || ddl.selectedIndex < 0) {
        Swal.fire("แจ้งเตือน", "กรุณาเลือก Unit Code", "warning");
        return;
    }

    const opt = ddl.options[ddl.selectedIndex];
    const unitCode = (opt && opt.text) ? opt.text.trim() : "";

    if (!unitCode) {
        Swal.fire("แจ้งเตือน", "ไม่พบ Unit Code จากตัวเลือก", "error");
        return;
    }

    const queueTypeId = 48;

    const formData = new FormData();
    formData.append("ProjectID", projectId);
    formData.append("UnitCode", unitCode);
    formData.append("QueueTypeID", queueTypeId);

    if (typeof showLoading === "function") showLoading();

    fetch(baseUrl + "QueueBank/GetMessageAppointmentInspect", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            if (res.Success) {

                // ============================
                // 🔥 SweetAlert Confirm
                // ============================
                Swal.fire({
                    title: "Confirm?",
                    html: res.Message,        // ใช้ข้อความจาก backend
                    icon: "question",
                    showCancelButton: true,
                    confirmButtonColor: "#3085d6",
                    cancelButtonColor: "#d33",
                    confirmButtonText: "Yes",
                    cancelButtonText: "Cancel"
                }).then(result => {
                    if (result.isConfirmed) {
                        crSaveRegisterLog(projectId, unitCode, queueTypeId);
                    }
                });

            } else {
                if (window.Application && typeof Application.PNotify === "function") {
                    Application.PNotify(res.Message, "error");
                } else {
                    Swal.fire("ข้อผิดพลาด", res.Message, "error");
                }
            }
        })
        .catch(err => {
            console.error("GetMessageAppointmentInspect error:", err);
            Swal.fire("ข้อผิดพลาด", "เกิดข้อผิดพลาดขณะตรวจสอบข้อมูลนัดหมาย", "error");
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}

function crSaveRegisterLog(projectId, unitCode, queueTypeId) {
    const formData = new FormData();
    formData.append("ProjectID", projectId || "");
    formData.append("UnitCode", unitCode || "");
    formData.append("QueueTypeID", queueTypeId || 0);

    if (typeof showLoading === "function") showLoading();

    return fetch(baseUrl + "QueueBank/SaveRegisterLog", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            if (res.Success) {

                // 🔥 ใช้ toast ของพ่อใหญ่
                successToastV2(res.Message || "บันทึกสำเร็จ");

                // ===============================
                // 🔄 Reload ตารางทั้งหมดที่เกี่ยวข้อง
                // ===============================
                if (window.CreateRegisterTableDt) {
                    window.CreateRegisterTableDt.ajax.reload(null, true); // true = reset paging
                }


                if (typeof QueueBankRegisterTableDt !== "undefined" && QueueBankRegisterTableDt) {
                    QueueBankRegisterTableDt.ajax.reload(null, false);
                }

                if (typeof RegisterTableDt !== "undefined" && RegisterTableDt) {
                    RegisterTableDt.ajax.reload(null, false);
                }

                // 🔄 Reload Summary
                if (typeof loadSummaryRegisterAll === "function") {
                    loadSummaryRegisterAll();
                }
                if (typeof loadSummaryRegisterBank === "function") {
                    loadSummaryRegisterBank();
                }

            } else {
                // ❌ Error toast
                Swal.fire({
                    toast: true,
                    icon: "error",
                    title: res.Message || "บันทึกไม่สำเร็จ",
                    position: "top-end",
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true
                });
            }
        })
        .catch(err => {
            //console.error("SaveRegisterLog error:", err);

            Swal.fire({
                toast: true,
                icon: "error",
                title: "เกิดข้อผิดพลาดขณะบันทึกคิว",
                position: "top-end",
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true
            });
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}

// format ตัวเลขมูลค่าให้เป็น "xx.xx M"
function qbFormatValueM(raw) {
    if (raw == null || raw === "") return "0.00";
    const num = Number(raw);
    if (Number.isNaN(num)) return raw;

    const m = num / 1_000_000;

    return m.toLocaleString("en-US", {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
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

    // 🔹 อัปเดต TABLE ถ้ามี element นั้นอยู่
    const tUnitEl = document.getElementById(`tbl-${prefix}-unit`);
    const tValueEl = document.getElementById(`tbl-${prefix}-value`);
    const tPercentEl = document.getElementById(`tbl-${prefix}-percent`);

    if (tUnitEl) tUnitEl.textContent = unit;
    if (tValueEl) tValueEl.textContent = qbFormatValueM(value);
    if (tPercentEl) tPercentEl.textContent = `${percent}%`;
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


document.addEventListener("DOMContentLoaded", function () {
    const btnToggle = document.getElementById("btnSummaryRegisterToggle");
    const boxView = document.getElementById("summary-register-box-view");
    const tableView = document.getElementById("summary-register-table-view");

    if (!btnToggle || !boxView || !tableView) return;

    btnToggle.addEventListener("click", function () {
        const icon = btnToggle.querySelector("i");

        const isBoxVisible = !boxView.classList.contains("d-none");

        if (isBoxVisible) {
            // สลับไป TABLE
            boxView.classList.add("d-none");
            tableView.classList.remove("d-none");

            if (icon) {
                icon.classList.remove("fa-table");
                icon.classList.add("fa-th-large"); // icon สำหรับ card view
            }
            btnToggle.setAttribute("title", "Change to card view");
            btnToggle.setAttribute("aria-label", "Change to card view");
        } else {
            // สลับกลับไป CARD
            tableView.classList.add("d-none");
            boxView.classList.remove("d-none");

            if (icon) {
                icon.classList.remove("fa-th-large");
                icon.classList.add("fa-table"); // icon สำหรับ table view
            }
            btnToggle.setAttribute("title", "Change to table view");
            btnToggle.setAttribute("aria-label", "Change to table view");
        }
    });
});

function qbUpdateSummaryRegisterHeaderDate() {
    const spanEl1 = document.getElementById("sum-register-date");
    const spanEl2 = document.getElementById("sum-register-date2");
    if (!spanEl1 && !spanEl2) return;

    const s = (fpRegisterStart && fpRegisterStart.selectedDates?.length)
        ? fpRegisterStart.selectedDates[0]
        : null;

    const e = (fpRegisterEnd && fpRegisterEnd.selectedDates?.length)
        ? fpRegisterEnd.selectedDates[0]
        : null;

    const toDMY = (dt) => {
        const dd = String(dt.getDate()).padStart(2, "0");
        const mm = String(dt.getMonth() + 1).padStart(2, "0");
        const yyyy = dt.getFullYear();
        return `${dd}/${mm}/${yyyy}`;
    };

    let text = "";
    if (!s && !e) text = "All Days";
    else if (s && e) text = `${toDMY(s)} - ${toDMY(e)}`;
    else text = toDMY(s || e);

    if (spanEl1) spanEl1.textContent = text;
    if (spanEl2) spanEl2.textContent = text;
}

function loadSummaryRegisterAll() {
    const filters = qbGetValues();

    let projectId = filters.Project;
    if (Array.isArray(projectId)) {
        projectId = projectId[0] || "";
    }

    qbUpdateSummaryRegisterHeaderDate();

    const formData = new FormData();
    formData.append("L_Act", "SummeryRegisterType");
    formData.append("L_ProjectID", projectId || "");
    formData.append("L_RegisterDateStart", filters.RegisterDateStart || "");
    formData.append("L_RegisterDateEnd", filters.RegisterDateEnd || "");
    formData.append("L_UnitID", (filters.UnitCode || []).join(","));
    formData.append("L_CSResponse", (filters.CSResponsible || []).join(","));
    formData.append("L_UnitCS", (filters.UnitStatusCS || []).join(","));
    formData.append("L_ExpectTransfer", (filters.ExpectTransferBy || []).join(","));
    formData.append("L_QueueTypeID", "48");

    formData.append("draw", "1");
    formData.append("start", "0");
    formData.append("length", "10");
    formData.append("SearchTerm", "");

    if (typeof showLoading === "function") showLoading();

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

            qbUpdateSummaryBox("loan-yes", loanMap["ยื่น"]);
            qbUpdateSummaryBox("loan-no", loanMap["ไม่ยื่น"]);

            // 3) CareerType: render BOTH box + table (dynamic by CareerTypeID)
            //const careerList = res.listDataSummeryRegisterCareerTyp || [];
            //qbRenderCareerBox(careerList);
            //qbRenderCareerTable(careerList);
        })
        .catch(err => {
            console.error("GetlistSummeryRegister error:", err);

            qbUpdateSummaryBox("register", null);
            qbUpdateSummaryBox("queue", null);
            qbUpdateSummaryBox("inprocess", null);
            qbUpdateSummaryBox("done", null);

            qbUpdateSummaryBox("loan-yes", null);
            qbUpdateSummaryBox("loan-no", null);

            //qbResetCareerBox();
            //qbResetCareerTable();
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}

// ===== Career: BOX view =====
function qbRenderCareerBox(list) {
    qbResetCareerBox();

    (list || []).forEach(x => {
        const id = (x.CareerTypeID || "").toString().trim();
        if (!id) return;

        const unit = x.Unit ?? "0";
        const value = x.Value ?? "0";
        const percent = x.Percent ?? "0";

        const unitEl = document.getElementById(`sum-career-${id}-unit`);
        const valueEl = document.getElementById(`sum-career-${id}-value`);
        const percentEl = document.getElementById(`sum-career-${id}-percent`);

        if (unitEl) unitEl.textContent = unit;
        if (valueEl) valueEl.textContent = qbFormatValueM(value);
        if (percentEl) percentEl.textContent = `${percent}%`;
    });
}

function qbResetCareerBox() {
    // reset เฉพาะ career boxes เท่านั้น
    document.querySelectorAll('[id^="sum-career-"][id$="-unit"]').forEach(el => el.textContent = "0");
    document.querySelectorAll('[id^="sum-career-"][id$="-value"]').forEach(el => el.textContent = qbFormatValueM("0"));
    document.querySelectorAll('[id^="sum-career-"][id$="-percent"]').forEach(el => el.textContent = "0%");
}

// ===== Career: TABLE view =====

function qbRenderCareerTable(list) {
    qbResetCareerTable();

    (list || []).forEach(x => {
        const id = (x.CareerTypeID || "").toString().trim();
        if (!id) return;

        const unit = x.Unit ?? "0";
        const value = x.Value ?? "0";
        const percent = x.Percent ?? "0";

        const unitEl = document.getElementById(`tbl-${id}-unit`);
        const valueEl = document.getElementById(`tbl-${id}-value`);
        const percentEl = document.getElementById(`tbl-${id}-percent`);

        if (unitEl) unitEl.textContent = unit;
        if (valueEl) valueEl.textContent = qbFormatValueM(value);
        if (percentEl) percentEl.textContent = `${percent}%`;
    });
}




function qbResetCareerTable() {
    // reset เฉพาะ tbody career table
    const body = document.getElementById("tbl-career-body");
    if (!body) return;

    body.querySelectorAll('[id^="tbl-"][id$="-unit"]').forEach(el => el.textContent = "0");
    body.querySelectorAll('[id^="tbl-"][id$="-value"]').forEach(el => el.textContent = qbFormatValueM("0"));
    body.querySelectorAll('[id^="tbl-"][id$="-percent"]').forEach(el => el.textContent = "0%");
}


// ===== Summary Bank (table) =====
function loadSummaryRegisterBank() {
    const filters = qbGetValues();

    let projectId = filters.Project;
    if (Array.isArray(projectId)) projectId = projectId[0] || "";

    const formData = new FormData();
    formData.append("L_ProjectID", projectId || "");
    formData.append("L_RegisterDateStart", filters.RegisterDateStart || "");
    formData.append("L_RegisterDateEnd", filters.RegisterDateEnd || "");
    formData.append("L_UnitID", (filters.UnitCode || []).join(","));
    formData.append("L_CSResponse", (filters.CSResponsible || []).join(","));
    formData.append("L_UnitCS", (filters.UnitStatusCS || []).join(","));
    formData.append("L_ExpectTransfer", (filters.ExpectTransferBy || []).join(","));
    formData.append("L_QueueTypeID", "48");

    // model params
    formData.append("draw", "1");
    formData.append("start", "0");
    formData.append("length", "1000");
    formData.append("SearchTerm", "");

    const tbodyBank = document.getElementById("summary-bank-body");
    const tbodyNon = document.getElementById("summary-banknonsubmissionreason-body");
    const tbodyCareer = document.getElementById("summary-RegisterCareerType-body");

    if (tbodyBank) {
        tbodyBank.innerHTML = `
            <tr><td colspan="5" class="text-center text-muted">Loading...</td></tr>`;
    }
    if (tbodyNon) {
        tbodyNon.innerHTML = `
            <tr><td colspan="3" class="text-center text-muted">Loading...</td></tr>`;
    }
    if (tbodyCareer) {
        tbodyCareer.innerHTML = `
            <tr><td colspan="4" class="text-center text-muted">Loading...</td></tr>`;
    }

    if (typeof showLoading === "function") showLoading();

    fetch(baseUrl + "QueueBank/GetlistSummeryRegisterBank", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {

            /* =========================
               1) Summary Bank (ซ้าย)
               ========================= */
            if (tbodyBank) {
                const listBank = res.listDataSummeryRegisterBank || [];

                if (!listBank.length) {
                    tbodyBank.innerHTML = `
            <tr><td colspan="5" class="text-center text-muted">No data</td></tr>`;
                } else {

                    let totalUnit = 0;
                    let totalValue = 0;

                    const rowsHtml = listBank.map(item => {
                        const bankCode = (item.BankCode || "").trim();
                        const bankName = item.BankName || "";

                        const unit = Number(item.Unit ?? 0);
                        const value = Number(item.Value ?? 0);

                        totalUnit += unit;
                        totalValue += value;

                        const valueText = (typeof qbFormatValueM === "function")
                            ? qbFormatValueM(value)
                            : value.toString();

                        // ✅ แถวปกติ: ใช้ percent จาก data (ถ้าไม่มีให้เป็น "-")
                        let percentText = (item.Percent ?? "").toString().trim();
                        if (percentText === "" || percentText.toLowerCase() === "no data") {
                            percentText = "-";
                        } else if (!percentText.endsWith("%")) {
                            percentText += "%";
                        }

                        let bankCellHtml = "";
                        if (bankCode && bankCode.toLowerCase() !== "no data") {
                            bankCellHtml = `
                    <div class="d-flex align-items-center gap-2">
                        <img src="${baseUrl}image/ThaiBankicon/${bankCode}.png"
                             alt="${bankCode}"
                             class="bank-logo"
                             onerror="this.style.display='none'">
                        <span>${bankName || bankCode}</span>
                    </div>`;
                        } else {
                            bankCellHtml = `<span>${bankName || "No data"}</span>`;
                        }

                        return `
                <tr>
                    <td>${bankCellHtml}</td>
                    <td class="text-center">${item.InterestRateAVG}</td>
                    <td class="text-center">${parseFloat(unit || 0).toLocaleString()}</td>
                    <td class="text-end">${valueText}</td>
                    <td class="text-center">${percentText}</td>
                </tr>`;
                    }).join("");

                    // ✅ Total row: % = 100% เท่านั้น
                    const totalRowHtml = `
            <tr class="fw-bold">
                <td class="text-start">Total</td>
                <td class="text-center">-</td>
                <td class="text-center">${parseFloat(totalUnit || 0).toLocaleString()}</td>
                <td class="text-end">
                    ${typeof qbFormatValueM === "function"
                            ? qbFormatValueM(totalValue)
                            : totalValue}
                </td>
                <td class="text-center">100%</td>
            </tr>`;

                    tbodyBank.innerHTML = rowsHtml + totalRowHtml;
                }
            }

            /* =======================================
               2) Summary Non-Submission Reason (ขวา)
               ======================================= */
            if (tbodyNon) {
                const listNon = res.listDataSummeryRegisterBankNonSubmissionReason || [];

                if (!listNon.length) {
                    tbodyNon.innerHTML = `
            <tr><td colspan="3" class="text-center text-muted">No data</td></tr>`;
                } else {

                    // ✅ total count
                    const totalCount = listNon.reduce((sum, x) => {
                        const n = Number(x?.Count ?? 0);
                        return sum + (Number.isFinite(n) ? n : 0);
                    }, 0);

                    // ✅ rows
                    const rowsHtml = listNon.map(item => {
                        const name = item.Name || "-";
                        const count = item.Count ?? 0;

                        let percent = (item.Percent ?? "0").toString().trim();
                        if (percent !== "" && !percent.endsWith("%")) percent += "%";

                        return `
                            <tr>
                                <td>${name}</td>
                                <td class="text-center">${count}</td>
                                <td class="text-center">${percent}</td>
                            </tr>`;
                    }).join("");

                    // ✅ total row (Count sum + 100%)
                    const totalRowHtml = `
                    <tr class="fw-bold">
                        <td class="text-start">Total</td>
                        <td class="text-center">${totalCount}</td>
                        <td class="text-center">100%</td>
                    </tr>`;


                    tbodyNon.innerHTML = rowsHtml + totalRowHtml;
                }
            }

            /* =======================================
              3) Summary Career Type (ขวาล่าง) ✅✅✅
              ใช้: res.listDataSummerycareerTask
              ======================================= */
            if (tbodyCareer) {
                const listCareer = res.listDataSummerycareerTask || [];

                if (!listCareer.length) {
                    tbodyCareer.innerHTML = `<tr><td colspan="4" class="text-center text-muted">No data</td></tr>`;
                } else {

                    // helper: parse string -> number (รองรับ "1,234" / "1.2K" / "3M")
                    const parseSmartNumber = (input) => {
                        if (input === null || input === undefined) return 0;
                        let s = input.toString().trim().toUpperCase();
                        if (!s) return 0;

                        s = s.replace(/,/g, "");

                        let mul = 1;
                        const last = s.slice(-1);

                        if (last === "K") { mul = 1000; s = s.slice(0, -1); }
                        else if (last === "M") { mul = 1000000; s = s.slice(0, -1); }
                        else if (last === "B") { mul = 1000000000; s = s.slice(0, -1); }

                        const n = parseFloat(s);
                        if (!Number.isFinite(n)) return 0;
                        return n * mul;
                    };

                    let totalUnit = 0;
                    let totalValue = 0;

                    const rowsHtml = listCareer.map(item => {
                        const careerName = item.Topic || "-";

                        const unitRaw = item.Unit ?? "0";
                        const valueRaw = item.Value ?? "0";
                        const percentRaw = (item.Percent ?? "0").toString().trim();

                        const unitNum = parseSmartNumber(unitRaw);
                        const valueNum = parseSmartNumber(valueRaw);

                        totalUnit += unitNum;
                        totalValue += valueNum;

                        const unitText = (unitRaw ?? "0").toString(); // โชว์ตาม backend (ถ้าเป็น short-name ก็ยังสวย)
                        const valueText = (typeof qbFormatValueM === "function")
                            ? qbFormatValueM(valueNum)
                            : valueNum.toLocaleString();

                        const percentText = percentRaw.endsWith("%") ? percentRaw : `${percentRaw}%`;

                        return `
                            <tr>
                                <td class="text-start">${careerName}</td>
                                <td class="text-center">${unitText}</td>
                                <td class="text-end">${valueText}</td>
                                <td class="text-center">${percentText}</td>
                            </tr>`;
                    }).join("");

                    const totalRowHtml = `
                        <tr class="fw-bold">
                            <td class="text-start">Total</td>
                            <td class="text-center">${totalUnit.toLocaleString()}</td>
                            <td class="text-end">
                                ${typeof qbFormatValueM === "function"
                            ? qbFormatValueM(totalValue)
                            : totalValue.toLocaleString()}
                            </td>
                            <td class="text-center">100%</td>
                        </tr>`;

                    tbodyCareer.innerHTML = rowsHtml + totalRowHtml;
                }
            }

        })
        .catch(err => {
            console.error("GetlistSummeryRegisterBank error:", err);

            if (tbodyBank) {
                tbodyBank.innerHTML = `
                    <tr><td colspan="5" class="text-center text-danger">Error loading Summary Bank</td></tr>`;
            }
            if (tbodyNon) {
                tbodyNon.innerHTML = `
                    <tr><td colspan="3" class="text-center text-danger">Error loading Non-Submission Reason</td></tr>`;
            }
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
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




// ---------- helper: download ----------
function downloadBlob(blob, filename) {
    const url = URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    a.remove();
    URL.revokeObjectURL(url);
}

// ---------- helper: wait fonts/images (ลดโอกาสแคปก่อนโหลดเสร็จ) ----------
async function waitAssets(el) {
    try {
        if (document.fonts && document.fonts.ready) {
            await document.fonts.ready;
        }
    } catch { }

    const imgs = el.querySelectorAll("img");
    const promises = [];
    imgs.forEach(img => {
        if (img.complete) return;
        promises.push(new Promise(res => {
            img.onload = img.onerror = () => res();
        }));
    });
    await Promise.all(promises);
}

// ---------- helper: capture element to PNG blob ----------
async function captureElementToPng(el, filename) {
    if (!el) return;

    await waitAssets(el);

    const canvas = await html2canvas(el, {
        backgroundColor: "#ffffff",
        scale: 2,
        useCORS: true,
        logging: false,
        windowWidth: document.documentElement.scrollWidth,
        windowHeight: document.documentElement.scrollHeight
    });

    return new Promise((resolve) => {
        canvas.toBlob((blob) => {
            if (blob) downloadBlob(blob, filename);
            resolve();
        }, "image/png");
    });
}

// ---------- capture cards into ONE image ----------
async function captureCardsAsOneImage() {
    const card2 = document.getElementById("cardSummaryRegister");
    const card3 = document.getElementById("cardSummaryBank");
    if (!card2 || !card3) return;

    // ตั้งชื่อไฟล์ตามเวลา (กันชนกัน)
    const now = new Date();
    const stamp =
        now.getFullYear().toString() +
        String(now.getMonth() + 1).padStart(2, "0") +
        String(now.getDate()).padStart(2, "0") + "_" +
        String(now.getHours()).padStart(2, "0") +
        String(now.getMinutes()).padStart(2, "0");

    // ✅ สร้าง wrapper ชั่วคราว (วาง offscreen)
    const wrapper = document.createElement("div");
    wrapper.id = "qb-capture-wrapper";
    wrapper.style.position = "fixed";
    wrapper.style.left = "-99999px";
    wrapper.style.top = "0";
    wrapper.style.background = "#ffffff";
    wrapper.style.padding = "12px";
    wrapper.style.width = Math.max(card2.offsetWidth, card3.offsetWidth) + "px";

    // ✅ Clone การ์ด (เอาของจริงมาวางรวมกัน)
    const c2 = card2.cloneNode(true);
    const c3 = card3.cloneNode(true);

    // ✅ กัน margin แปลก ๆ + จัด spacing ระหว่างการ์ด
    c2.style.margin = "0";
    c3.style.margin = "12px 0 0 0";

    wrapper.appendChild(c2);
    wrapper.appendChild(c3);
    document.body.appendChild(wrapper);

    try {
        await captureElementToPng(wrapper, `Summary_${stamp}.png`);
    } finally {
        wrapper.remove();
    }
}

// ---------- bind button ----------
document.addEventListener("DOMContentLoaded", () => {
    const btn = document.getElementById("btnCapture");
    if (!btn) return;

    btn.addEventListener("click", async () => {
        try {
            btn.disabled = true;
            await captureCardsAsOneImage();  // ✅ รูปเดียว
        } catch (e) {
            console.error("Capture error:", e);
            alert("Capture failed. Please check console.");
        } finally {
            btn.disabled = false;
        }
    });
});




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

(function initQueueBankSignalR() {
    if (typeof signalR === "undefined") {
        console.warn("SignalR client not loaded");
        return;
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(baseUrl + "hubs/queuebank") // baseUrl ของพ่อใหญ่มีอยู่แล้ว
        .withAutomaticReconnect()
        .build();

    let currentProjectId = "";

    function getSelectedProjectId() {
        const filters = qbGetValues();
        let pid = filters.Project;
        if (Array.isArray(pid)) pid = pid[0] || "";
        return (pid || "").toString().trim();
    }

    async function switchProjectGroup() {
        const pid = getSelectedProjectId();

        // ไม่เปลี่ยนก็ไม่ทำอะไร
        if (pid === currentProjectId) return;

        // leave old
        if (currentProjectId) {
            try { await connection.invoke("LeaveProject", currentProjectId); } catch { }
        }

        currentProjectId = pid;

        // join new
        if (currentProjectId) {
            try { await connection.invoke("JoinProject", currentProjectId); } catch { }
        }
    }

    function refreshAll() {
        // ✅ เหมือนกด Search
        if (typeof loadQueueBankRegisterTable === "function") loadQueueBankRegisterTable();
        if (typeof loadSummaryRegisterAll === "function") loadSummaryRegisterAll();
        if (typeof loadSummaryRegisterBank === "function") loadSummaryRegisterBank();
    }

    // รับ event จาก server
    connection.on("QueueBankChanged", (payload) => {
        // ถ้าเปิด modal edit อยู่ ไม่ต้องเด้งทันที (กัน UX พัง)
        const modalEl = document.getElementById("EditRegisterLog");
        const isEditOpen = modalEl && modalEl.classList.contains("show");

        // ถ้า payload projectId ไม่ตรงกับที่เลือกอยู่ ก็ไม่ต้องทำอะไร
        const pidNow = getSelectedProjectId();
        if (payload && payload.projectId && payload.projectId !== pidNow) return;

        if (isEditOpen) {
            // แจ้งเฉยๆ แล้วให้ user กด search / หรือปิด modal ก่อน
            if (typeof infoToast === "function") infoToast("Data updated by another user. Please refresh after closing modal.");
            return;
        }

        refreshAll();
    });

    // start
    connection.start()
        .then(async () => {
            await switchProjectGroup();
        })
        .catch(err => console.error("SignalR start error:", err));

    // ✅ เมื่อ Project เปลี่ยน → switch group
    document.addEventListener("DOMContentLoaded", () => {
        const projInst = window.QB_CHOICES && window.QB_CHOICES["#ddl_Project"];
        const projEl = projInst ? projInst.passedElement.element : document.getElementById("ddl_Project");
        if (!projEl) return;

        projEl.addEventListener("change", async () => {
            await switchProjectGroup();
        });
    });

    // เผื่อ reconnect → join group ใหม่
    connection.onreconnected(async () => {
        await switchProjectGroup();
    });

})();
