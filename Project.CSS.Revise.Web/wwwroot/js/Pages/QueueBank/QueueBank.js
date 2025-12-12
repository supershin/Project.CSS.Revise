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

    // ✅ ใช้ QueueBankCustomerViewUrl ให้เหมือน QueueBankCounterViewUrl
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


document.querySelectorAll('#EditRegisterLog .er-status-btn').forEach(btn => {
    btn.addEventListener('click', function () {
        if (this.disabled) return;

        // ✅ toggle เปิด/ปิดเอง ไม่ไปยุ่งกับปุ่มอื่น
        this.classList.toggle('active');

        console.log("Selected Status:", this.id, "Active:", this.classList.contains("active"));
    });
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
    createChoice("#ddl_Responsible", { placeholderValue: "Select Responsible..." });
    createChoice("#ddl_Career", { placeholderValue: "Not specified" });
    createChoice("#ddl_Reason", { placeholderValue: "Select Reason..." });

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
            {
                data: "UnitCode",
                name: "UnitCode",
                render: function (data, type, row) {
                    if (type !== "display") return data;

                    const unitCode = data || "";
                    const id = row.ID || "";

                    return `
                            <a href="javascript:void(0)" class="qb-unit-link unit-pill"
                               data-unit="${unitCode}" data-id="${id}">
                               <i class="fa fa-home unit-icon"></i> ${unitCode}
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
    formData.append("ID", registerId);    // ตรงกับ RegisterLog criteria.ID

    if (typeof showLoading === "function") showLoading();

    fetch(baseUrl + "QueueBank/RegisterLogInfo", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(json => {
            if (!json || !json.Success) {
                const msg = (json && json.Message) ? json.Message : "ไม่สามารถดึงข้อมูล RegisterLog ได้";
                if (typeof Swal !== "undefined") {
                    Swal.fire("ข้อผิดพลาด", msg, "error");
                } else if (window.Application && typeof Application.PNotify === "function") {
                    Application.PNotify(msg, "error");
                } else {
                    alert(msg);
                }
                return;
            }

            const data = json.Data || {};

            // ถ้า backend ไม่คืน UnitCode มา ใช้ตัวที่เรามีจาก table
            if (!data.UnitCode && unitCode) {
                data.UnitCode = unitCode;
            }

            bindRegisterLogModal(data);
        })
        .catch(err => {
            console.error("RegisterLogInfo error:", err);
            if (typeof Swal !== "undefined") {
                Swal.fire("ข้อผิดพลาด", "เกิดข้อผิดพลาดขณะดึงข้อมูล RegisterLog", "error");
            } else {
                alert("เกิดข้อผิดพลาดขณะดึงข้อมูล RegisterLog");
            }
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}

function bindRegisterLogModal(data) {
    const modalEl = document.getElementById("EditRegisterLog");
    if (!modalEl) {
        return;
    }

    modalEl.dataset.registerId = data.ID || "";

    const headerEl = document.getElementById("hUnitCode");
    if (headerEl) {
        headerEl.textContent = data.UnitCode
            ? `Unit Code : ${data.UnitCode}`
            : "";
    }

    const setChoiceSingle = (selector, value) => {
        const val = (value === null || value === undefined) ? "" : String(value);

        const inst = window.QB_CHOICES ? window.QB_CHOICES[selector] : null;
        if (inst) {
            try {
                inst.removeActiveItems();
                if (val !== "") {
                    inst.setChoiceByValue(val);
                }
            } catch (e) {
                console.warn("setChoiceSingle (Choices) error:", selector, e);
            }
        } else {
            const el = document.querySelector(selector);
            if (el) {
                el.value = val;
            }
        }
    };

    setChoiceSingle("#ddl_Responsible", data.ResponsibleID);

    (function () {
        let careerValue = "";

        if (data.CareerTypeID !== null && data.CareerTypeID !== undefined && data.CareerTypeID !== 0) {
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
    })();

    setChoiceSingle("#ddl_Reason", data.ReasonID);

    const setStatusBtn = (id, val) => {
        const btn = document.getElementById(id);
        if (!btn) {
            return;
        }

        const isOn = !!val;

        if (isOn) {
            btn.classList.add("active", "btn-success");
            btn.classList.remove("btn-secondary");
        } else {
            btn.classList.remove("active", "btn-success");
            btn.classList.add("btn-secondary");
        }
    };

    setStatusBtn("FlagRegister", data.FlagRegister);
    setStatusBtn("FlagInprocess", data.FlagInprocess);
    setStatusBtn("FlagFinish", data.FlagFinish);

    // ✅ FinPlus Bank List (ใช้ LoanBankList จาก backend)
    renderFinPlusBanks(data.LoanBankList);

    const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
    modal.show();
}



function renderFinPlusBanks(loanBankList) {
    const box = document.getElementById("finplusBankList");
    if (!box) {
        return;
    }

    box.innerHTML = "";

    if (!Array.isArray(loanBankList) || loanBankList.length === 0) {
        box.innerHTML = `<div class="text-muted small">No bank selected.</div>`;
        return;
    }

    // แสดงสูงสุด 5 ธนาคารใน 1 แถว (ตาม requirement)
    const items = loanBankList.slice(0, 5);

    let html = "";
    for (let i = 0; i < items.length; i++) {
        const bankCode = (items[i] && items[i].BankCode) ? String(items[i].BankCode).trim() : "";
        if (!bankCode) {
            continue;
        }

        html += `
  <div class="bank-item" title="${bankCode}">
      <img src="${baseUrl}image/ThaiBankicon/${bankCode}.png"
           alt="${bankCode}"
           class="bank-logo">
      <span class="bank-code">${bankCode}</span>
  </div>
`;

    }

    box.innerHTML = html;
}


//function bindRegisterLogModal(data) {
//    const modalEl = document.getElementById("EditRegisterLog");
//    if (!modalEl) return;

//    // เก็บ ID ไว้ใน data attribute เผื่อใช้ตอน Save
//    modalEl.dataset.registerId = data.ID || "";

//    // Header Unit Code
//    const headerEl = document.getElementById("hUnitCode");
//    if (headerEl) {
//        headerEl.textContent = data.UnitCode
//            ? `Unit Code : ${data.UnitCode}`
//            : "";
//    }

//    // helper: set single-select (รองรับทั้ง Choices.js และ select ปกติ)
//    const setChoiceSingle = (selector, value) => {
//        const val = (value === null || value === undefined) ? "" : String(value);

//        const inst = window.QB_CHOICES ? window.QB_CHOICES[selector] : null;
//        if (inst) {
//            try {
//                inst.removeActiveItems();
//                if (val !== "") {
//                    inst.setChoiceByValue(val);
//                }
//            } catch (e) {
//                console.warn("setChoiceSingle (Choices) error:", selector, e);
//            }
//        } else {
//            const el = document.querySelector(selector);
//            if (el) el.value = val;
//        }
//    };

//    // ===== Dropdowns =====
//    // ResponsibleID
//    setChoiceSingle("#ddl_Responsible", data.ResponsibleID);

//    // CareerTypeID
//    setChoiceSingle("#ddl_Career", data.CareerTypeID);

//    // ReasonID
//    setChoiceSingle("#ddl_Reason", data.ReasonID);

//    // FinPlus → ตอนนี้ map กับ TransferTypeID
//    setChoiceSingle("#ddl_FinPlus", data.TransferTypeID);

//    // ===== Status buttons (multi-select) =====
//    const setStatusBtn = (id, val) => {
//        const btn = document.getElementById(id);
//        if (!btn) return;

//        const isOn = !!val;

//        if (isOn) {
//            btn.classList.add("active", "btn-success");
//            btn.classList.remove("btn-secondary");
//        } else {
//            btn.classList.remove("active", "btn-success");
//            btn.classList.add("btn-secondary");
//        }
//    };

//    setStatusBtn("FlagRegister", data.FlagRegister);
//    setStatusBtn("FlagInprocess", data.FlagInprocess);
//    setStatusBtn("FlagFinish", data.FlagFinish);

//    // ===== Show modal (Bootstrap 5) =====
//    const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
//    modal.show();
//}



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

            // ❌ ไม่ได้เลือก Project → popup error + หยุด
            //if (!hasProject) {
            //    if (typeof Swal !== "undefined") {
            //        Swal.fire({
            //            icon: "error",
            //            title: "Validation Error",
            //            text: "Please select a project before searching.",
            //            buttonsStyling: false,
            //            confirmButtonText: "OK",
            //            customClass: {
            //                confirmButton: "btn btn-danger"
            //            },
            //            allowOutsideClick: false,
            //            didOpen: (popup) => {
            //                popup.parentNode.style.zIndex = 200000;
            //            }
            //        });
            //    } else {
            //        alert("Please select a project before searching.");
            //    }
            //    return; // ⚠️ หยุดไม่ให้ search ต่อ
            //}

            // ✅ ผ่าน validation → ทำงานตามปกติ
            qbUpdateProjectTableHeader();
            loadQueueBankRegisterTable();
            loadSummaryRegisterAll();
            loadSummaryRegisterBank();
        });
    }

    const btnCancel = document.getElementById("btnFilterCancel");
    if (btnCancel) {
        btnCancel.addEventListener("click", () => {
            ClearFilter();
            qbUpdateProjectTableHeader(); // reset header after clear
        });
    }
}


function qbUpdateProjectTableHeader() {
    const headerEl = document.getElementById("project-name-selected");
    const selectEl = document.getElementById("ddl_Project");
    const defaultTitle = "Project Table";

    if (!headerEl) {
        console.warn("❗ #project-name-selected not found");
        return;
    }
    if (!selectEl) {
        console.warn("❗ #ddl_Project not found");
        headerEl.textContent = defaultTitle;
        return;
    }

    // Get selected <option> elements from the real select
    const selectedOptions = Array.from(selectEl.selectedOptions || []);
    console.log("📌 Selected project options:", selectedOptions);

    // Case 1: No selection
    if (selectedOptions.length === 0) {
        console.log("➡ No project selected → use default title");
        headerEl.textContent = defaultTitle;
        return;
    }

    // Helper to get clean text
    const getText = (opt) => (opt?.textContent || "").trim();

    // Case 2: One project selected
    if (selectedOptions.length === 1) {
        const name = getText(selectedOptions[0]) || defaultTitle;
        console.log("➡ One project selected:", name);
        headerEl.textContent = name;
        return;
    }

    // Case 3: Multiple projects selected
    const firstName = getText(selectedOptions[0]) || "Project";
    const moreCount = selectedOptions.length - 1;
    const labelText = `${firstName} (+${moreCount})`;

    console.log(`➡ Multiple selected: ${labelText}`);
    headerEl.textContent = labelText;
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
            window.CreateRegisterTableDt.ajax.reload();
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
                if (typeof CreateRegisterTableDt !== "undefined" && CreateRegisterTableDt) {
                    CreateRegisterTableDt.ajax.reload(null, false);
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
            console.error("SaveRegisterLog error:", err);

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
    const startEl = document.getElementById("txt_RegisterDateStart");
    const endEl = document.getElementById("txt_RegisterDateEnd");
    const spanEl = document.getElementById("sum-register-date");

    if (!spanEl) return;

    const start = (startEl?.value || "").trim();
    const end = (endEl?.value || "").trim();

    // --- CASES ---
    if (!start && !end) {
        spanEl.textContent = "All Days";   // ← default text (you can change anytime)
        return;
    }

    if (start && end) {
        spanEl.textContent = `${start} - ${end}`; // ← Your requested format
        return;
    }

    // Only one side filled
    spanEl.textContent = start || end;
}


function loadSummaryRegisterAll() {
    const filters = qbGetValues();

    let projectId = filters.Project;
    if (Array.isArray(projectId)) {
        projectId = projectId[0] || "";
    }

    // 🔹 อัปเดตหัวข้อวันที่จาก filter
    qbUpdateSummaryRegisterHeaderDate();

    const formData = new FormData();
    // ==== QueueBank filters ====
    formData.append("L_Act", "SummeryRegisterType");
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
                const interestRate = (item.InterestRateAVG || "0") + "%";

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
                        <td>${interestRate}</td>
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
