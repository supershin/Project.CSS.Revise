// ======================
// Collapse chevron toggle (ส่วนหัว card)
// ======================
document.addEventListener("DOMContentLoaded", function () {
    const toggles = document.querySelectorAll('[data-bs-toggle="collapse"][data-bs-target]');

    toggles.forEach(btn => {
        const targetSel = btn.getAttribute('data-bs-target');
        const target = document.querySelector(targetSel);
        if (!target) return;

        const icon = btn.querySelector('i');
        if (!icon) return;

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

        const isShown = target.classList.contains('show');
        icon.classList.toggle('fa-chevron-up', isShown);
        icon.classList.toggle('fa-chevron-down', !isShown);
        btn.setAttribute('aria-expanded', String(isShown));
    });
});

// ======================
// Toggle Summary view: Card (Box) <-> Table
// ======================
document.addEventListener("DOMContentLoaded", function () {
    const btnToggle = document.getElementById("btnSummaryRegisterToggle");
    const boxView = document.getElementById("summary-register-box-view");
    const tableView = document.getElementById("summary-register-table-view");

    if (!btnToggle || !boxView || !tableView) return;

    btnToggle.addEventListener("click", function () {
        const icon = btnToggle.querySelector("i");

        const isBoxVisible = !boxView.classList.contains("d-none");

        if (isBoxVisible) {
            // 👉 สลับไป TABLE
            boxView.classList.add("d-none");
            tableView.classList.remove("d-none");

            if (icon) {
                icon.classList.remove("fa-table");
                icon.classList.add("fa-th-large"); // icon สำหรับ card view
            }
            btnToggle.setAttribute("title", "Change to card view");
            btnToggle.setAttribute("aria-label", "Change to card view");
        } else {
            // 👉 สลับกลับไป CARD
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


// ======================
// Summary helpers (copy from QueueBank.js)
// ======================

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

// ✅ เวอร์ชันสำหรับหน้า Counter View: ดึงค่าแค่ Project จาก hidProjectId
function qbGetValuesCounterView() {
    const projectId = document.getElementById("hidProjectId")?.value || "";
    return {
        Project: projectId,
        RegisterDateStart: "",
        RegisterDateEnd: "",
        UnitCode: [],
        CSResponsible: [],
        UnitStatusCS: [],
        ExpectTransferBy: []
    };
}

// ✅ เวอร์ชันง่าย ๆ: ขึ้นว่า All Days เสมอ (หน้า Counter ไม่มี date filter)
function qbUpdateSummaryRegisterHeaderDate() {
    const spanEl = document.getElementById("sum-register-date");
    if (!spanEl) return;
    spanEl.textContent = "All Days";
}


// ======================
// Full screen Container counter
// ======================

document.addEventListener("DOMContentLoaded", function () {

    const btnFull = document.getElementById("btnFullScreen");
    const container = document.getElementById("Container_counter");

    if (!btnFull || !container) return;

    function enterFullScreen() {
        if (container.requestFullscreen) {
            container.requestFullscreen();
        }
        container.classList.add("fullscreen-mode");
        btnFull.innerHTML = '<i class="fa fa-compress"></i>';

        if (typeof updateCounterGridLayout === "function") {
            updateCounterGridLayout();
        }
    }

    function exitFullScreen() {
        if (document.fullscreenElement) {
            document.exitFullscreen();
        }
        container.classList.remove("fullscreen-mode");
        btnFull.innerHTML = '<i class="fa fa-expand"></i>';

        if (typeof updateCounterGridLayout === "function") {
            updateCounterGridLayout();
        }
    }

    btnFull.addEventListener("click", function () {
        const isFull = container.classList.contains("fullscreen-mode");
        if (!isFull) {
            enterFullScreen();
        } else {
            exitFullScreen();
        }
    });

    // ปิด fullscreen เมื่อ user กด ESC
    document.addEventListener("fullscreenchange", function () {
        if (!document.fullscreenElement && container.classList.contains("fullscreen-mode")) {
            // เผื่อกรณีกด ESC แล้ว class ยังค้าง
            container.classList.remove("fullscreen-mode");
            btnFull.innerHTML = '<i class="fa fa-expand"></i>';

            if (typeof updateCounterGridLayout === "function") {
                updateCounterGridLayout();
            }
        }
    });
});



// ======================
// ปรับ layout ของ grid ตามสถานะ panel ขวา
// ======================
function updateCounterGridLayout() {
    const grid = document.getElementById("counterGrid");
    const detailCol = document.getElementById("counterDetailColumn");
    if (!grid) return;

    const cols = grid.querySelectorAll(".counter-col");

    // ถ้าไม่มี detailCol หรือ detail ถูกซ่อน → ซ้ายเต็มพื้นที่
    const isDetailHidden = !detailCol || detailCol.classList.contains("d-none");

    cols.forEach(col => {
        // reset class ที่เกี่ยวกับ column ก่อน
        col.classList.remove("col-md-2", "col-lg-2", "col-md-3", "col-lg-3");

        // base: มือถือให้ 2 ต่อแถวเหมือนเดิม
        if (!col.classList.contains("col-6")) {
            col.classList.add("col-6");
        }

        if (isDetailHidden) {
            // ✅ ปิด panel ขวา → แถวแน่นขึ้น (6 ใบ/แถว)
            col.classList.add("col-md-2", "col-lg-2");
        } else {
            // ✅ เปิด panel ขวา → 4 ใบ/แถว เพื่อให้การ์ดอ่านง่าย
            col.classList.add("col-md-3", "col-lg-3");
        }
    });
}



// ======================
// Load counter list from backend (JSON)
// ======================
async function loadCounterList() {
    const grid = document.getElementById("counterGrid");
    if (!grid) return;

    const projInput = document.getElementById("hidProjectId");
    const projectId = projInput ? projInput.value : "";

    if (!projectId) {
        grid.innerHTML = `
            <div class="col-12 text-center text-danger">
                Project ID not found.
            </div>`;
        return;
    }

    grid.innerHTML = `
        <div class="col-12 text-center text-muted" id="counterGridLoading">
            Loading counters...
        </div>`;

    try {
        const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");
        const url = `${rootPath}QueueBankCounterView/GetCounterList?projectId=${encodeURIComponent(projectId)}`;

        const resp = await fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        });

        if (!resp.ok) {
            throw new Error("HTTP " + resp.status);
        }

        const json = await resp.json();
        if (!json.success) {
            console.error("GetCounterList error:", json.message);
            renderCounterGrid([]);
            return;
        }

        renderCounterGrid(json.data || []);
    } catch (err) {
        console.error("Fetch counter list failed:", err);
        grid.innerHTML = `
            <div class="col-12 text-center text-danger">
                Cannot load counter list.
            </div>`;
    }
}


// ======================
// Render counter boxes จาก JSON
// ======================
function renderCounterGrid(items) {
    const grid = document.getElementById("counterGrid");
    const loadingEl = document.getElementById("counterGridLoading");

    if (!grid) return;

    if (loadingEl) {
        loadingEl.remove();
    }

    if (!items || !items.length) {
        grid.innerHTML = `
            <div class="col-12 text-center text-muted">
                No counters configured for this project.
            </div>`;
        return;
    }

    const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");
    let html = "";

    const hasValue = (v) => {
        if (v === null || v === undefined) return false;
        const s = String(v).trim();
        if (s === "" || s.toLowerCase() === "null" || s.toLowerCase() === "undefined") return false;
        return true;
    };

    items.forEach(item => {
        const counterNo = item.Counter || item.counter || "";
        const bankCode = item.BankCode || item.bankCode || "";
        const bankName = item.BankName || item.bankName || "";
        const unitCode = item.UnitCode || item.unitCode || "";
        const registerLogID = item.RegisterLogID ?? item.registerLogID ?? "";

        const inProcessDate = item.InProcessDate ?? item.inProcessDate ?? "";
        const hasInProcess = hasValue(inProcessDate);

        // ✅ FIXED
        const isActive = hasValue(registerLogID);

        const boxClass =
            "counter-box qb-counter " +
            (isActive ? "active" : "empty") +
            (hasInProcess ? " inprocess" : "");

        const bankLogoHtml = bankCode
            ? `<img src="${rootPath}image/ThaiBankicon/${bankCode}.png" alt="${bankCode}" width="26" class="me-2">`
            : "";

        const bodyContent = isActive
            ? `${bankLogoHtml}${unitCode || "-"}`
            : "";

        let headerStyle = "";
        if (hasInProcess) {
            headerStyle = "background-color:#198754;color:#ffffff;";
        } else if (isActive) {
            headerStyle = "background-color:#dc3545;color:#ffffff;";
        } else {
            headerStyle = "background-color:#6c757d;color:#ffffff;";
        }

        html += `
        <div class="counter-col col-6">
            <div class="${boxClass}"
                 data-counter="${counterNo}"
                 data-bank="${bankCode}"
                 data-bankname="${bankName}"
                 data-unit="${unitCode}"
                 data-registerid="${registerLogID}">
         
                <div class="counter-header" style="${headerStyle}">
                    Counter : ${counterNo}
                </div>

                <div class="counter-body">
                    ${bodyContent}
                </div>
            </div>
        </div>
    `;
    });


    grid.innerHTML = html;

    // เก็บ state เดิม ไว้ให้ปุ่ม Bank/QR ใช้ restore
    const boxes = grid.querySelectorAll(".counter-box");
    boxes.forEach(box => {
        const header = box.querySelector(".counter-header");
        const body = box.querySelector(".counter-body");
        if (!header || !body) return;

        box.dataset.originalBoxClass = box.className;
        box.dataset.originalHeaderHtml = header.innerHTML;
        box.dataset.originalBodyHtml = body.innerHTML;

        header.dataset.originalClass = header.className;
        body.dataset.originalClass = body.className;

        // ✅ ADD: จำ inline style เดิมของ header (สีแดง/เขียว/เทา)
        box.dataset.originalHeaderStyle = header.getAttribute("style") || "";
    });


    // init behaviour หลังจาก render เสร็จ
    initCounterModeButtons();
    initCounterCardClick();

    // ปรับ layout ของ grid ตามสถานะ panel ขวา
    updateCounterGridLayout();
}




// ======================
// Counter View: toggle Bank / QR mode
// ======================
function initCounterModeButtons() {
    const grid = document.getElementById("counterGrid");
    const btnBank = document.getElementById("btnBankCounter");
    const btnQR = document.getElementById("btnQRCounter");

    if (!grid || !btnBank || !btnQR) return;

    function setButtonsMode(mode) {
        btnBank.classList.remove("btn-primary", "btn-outline-primary");
        btnQR.classList.remove("btn-warning", "btn-outline-warning");

        if (mode === "bank") {
            btnBank.classList.add("btn-primary");
            btnQR.classList.add("btn-outline-warning");
        } else {
            btnBank.classList.add("btn-outline-primary");
            btnQR.classList.add("btn-warning");
        }
    }

    // 🔵 โหมด Bank → คืน layout เดิมที่ save ไว้ใน dataset
    function setBankMode() {
        const boxes = Array.from(grid.querySelectorAll(".counter-box"));

        boxes.forEach(box => {
            const header = box.querySelector(".counter-header");
            const body = box.querySelector(".counter-body");
            if (!header || !body) return;

            if (box.dataset.originalBoxClass) box.className = box.dataset.originalBoxClass;

            if (header.dataset.originalClass) header.className = header.dataset.originalClass;
            if (body.dataset.originalClass) body.className = body.dataset.originalClass;

            if (box.dataset.originalHeaderHtml != null) header.innerHTML = box.dataset.originalHeaderHtml;
            if (box.dataset.originalBodyHtml != null) body.innerHTML = box.dataset.originalBodyHtml;

            // ✅ ADD: คืน inline style กลับ (ลบสีเทาที่ QR mode ใส่ไว้)
            header.setAttribute("style", box.dataset.originalHeaderStyle || "");
        });

        setButtonsMode("bank");

        if (typeof updateCounterGridLayout === "function") {
            updateCounterGridLayout();
        }
    }


    // 🟡 โหมด QR → เรียก /QueueBankCounterView/CounterQr ต่อ counter
    function setQRMode() {
        const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");

        const projectIdInput = document.getElementById("hidProjectId");
        const projectNameEl = document.getElementById("project_name");

        const projectId = projectIdInput ? projectIdInput.value : "";
        const projectName = projectNameEl ? projectNameEl.textContent.trim() : "";

        const encodedProjectId = encodeURIComponent(projectId);
        const encodedProjectName = encodeURIComponent(projectName);

        const boxes = Array.from(grid.querySelectorAll(".counter-box"));

        boxes.forEach(box => {
            const header = box.querySelector(".counter-header");
            const body = box.querySelector(".counter-body");
            if (!header || !body) return;

            const counterNo = box.dataset.counter || "";
            if (!counterNo) return;

            // ✅ เอาสถานะจาก class เดิมที่เคย render ไว้ (active / empty / inprocess)
            // ใช้ originalBoxClass เป็นหลัก (เพราะตอนนี้เราอาจอยู่โหมดอื่นแล้ว)
            const originalClass = box.dataset.originalBoxClass || box.className;

            const hasInProcess = originalClass.includes("inprocess");
            const isActive = originalClass.includes("active");
            const isEmpty = originalClass.includes("empty");

            // ✅ คง class เดิมไว้ เพื่อให้ CSS สี body ตรงตามสถานะ
            // แค่เติม flag ว่าอยู่โหมด QR
            box.className = originalClass;
            box.classList.add("qr-mode");

            const qrUrl =
                `${rootPath}QueueBankCounterView/CounterQr` +
                `?projectId=${encodedProjectId}` +
                `&projectName=${encodedProjectName}` +
                `&queueType=bank` +
                `&counterNo=${encodeURIComponent(counterNo)}`;

            // ✅ Header: ตั้งสีตามสถานะ (เขียว/แดง/เทา)
            header.className = "counter-header";
            header.style.color = "#ffffff";
            header.textContent = `Counter : ${counterNo}`;

            if (hasInProcess) {
                header.style.backgroundColor = "#198754"; // green
            } else if (isActive) {
                header.style.backgroundColor = "#dc3545"; // red
            } else if (isEmpty) {
                header.style.backgroundColor = "#6c757d"; // grey
            } else {
                header.style.backgroundColor = "#6c757d";
            }

            // ✅ Body: ใส่ QR แต่ยังให้พื้นหลังสีเดิมทำงานจาก CSS (.active/.inprocess/.empty)
            body.className = "counter-body";
            body.innerHTML = `
            <div class="d-flex justify-content-center align-items-center" style="min-height:60px;">
                <div class="qr-wrap">
                    <img src="${qrUrl}"
                         class="counter-qr"
                         alt="QR Code for Counter ${counterNo}"
                         style="width:64px; height:auto;">
                </div>
            </div>
        `;
        });

        setButtonsMode("qr");

        if (typeof updateCounterGridLayout === "function") {
            updateCounterGridLayout();
        }
    }



    if (!btnBank.dataset.bound) {
        btnBank.addEventListener("click", function (e) {
            e.preventDefault();
            setBankMode();
        });
        btnBank.dataset.bound = "1";
    }

    if (!btnQR.dataset.bound) {
        btnQR.addEventListener("click", function (e) {
            e.preventDefault();
            setQRMode();
        });
        btnQR.dataset.bound = "1";
    }

    // เริ่มที่โหมด Bank
    setBankMode();

    // เผื่อไว้ ถ้า render เสร็จแล้ว detail ปิดอยู่ → ใช้ layout col-2
    if (typeof updateCounterGridLayout === "function") {
        updateCounterGridLayout();
    }
}


function initCounterCardClick() {
    const grid = document.getElementById("counterGrid");
    const detailCol = document.getElementById("counterDetailColumn");
    const titleEl = document.getElementById("counterDetailTitle");
    const closeBtn = document.getElementById("btnCloseCounterDetail");
    const leftCol = document.getElementById("counterGridColumn");

    if (!grid || !detailCol) return;

    if (leftCol && detailCol.classList.contains("d-none")) {
        leftCol.classList.remove("col-lg-8");
        leftCol.classList.add("col-lg-12");
        updateCounterGridLayout();
    }

    grid.addEventListener("click", function (e) {
        const box = e.target.closest(".qb-counter");
        if (!box || !grid.contains(box)) return;

        const counterNo = box.dataset.counter || "";
        const unitCode = box.dataset.unit || "";

        currentCounterNo = counterNo;              // ⭐ จำ counter ที่เลือกไว้

        detailCol.classList.remove("d-none");

        if (leftCol) {
            leftCol.classList.remove("col-lg-12");
            leftCol.classList.add("col-lg-8");
        }

        if (titleEl) {
            titleEl.textContent = counterNo
                ? `Counter : ${counterNo}`
                : "Counter";
        }

        grid.querySelectorAll(".qb-counter.selected").forEach(el => {
            el.classList.remove("selected");
        });
        box.classList.add("selected");

        updateCounterGridLayout();

        // ⭐ โหลด detail จริง
        if (typeof loadCounterDetail === "function" && counterNo) {
            loadCounterDetail(counterNo);
        }

        // ⭐⭐⭐ RESET DROPDOWN WHEN CHANGE COUNTER ⭐⭐⭐
        const ddl = document.getElementById("ddlUnitRegister");
        if (window.unitRegisterChoices) {
            unitRegisterChoices.removeActiveItems();
            unitRegisterChoices.setChoiceByValue('');
        } else if (ddl) {
            ddl.value = "";
        }
    });

    if (closeBtn && !closeBtn.dataset.bound) {
        closeBtn.addEventListener("click", function () {
            detailCol.classList.add("d-none");

            if (leftCol) {
                leftCol.classList.remove("col-lg-8");
                leftCol.classList.add("col-lg-12");
            }

            grid.querySelectorAll(".qb-counter.selected").forEach(el => {
                el.classList.remove("selected");
            });

            currentCounterNo = null;   // ปิด panel ล้างค่า counter
            updateCounterGridLayout();
        });
        closeBtn.dataset.bound = "1";
    }
}



// ======================
// Load Counter Detail (Right Panel) — Unit badge + Bank badge
// ======================
async function loadCounterDetail(counterNo) {
    const projectIdInput = document.getElementById("hidProjectId");
    const projectNameEl = document.getElementById("project_name");
    const tagArea = document.getElementById("counterTagArea");
    const qrBox = document.getElementById("counterQrBox");

    if (!projectIdInput || !tagArea || !qrBox) return;

    const projectId = projectIdInput.value || "";
    const projectName = projectNameEl ? projectNameEl.textContent.trim() : "";

    const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");

    const url =
        `${rootPath}QueueBankCounterView/GetCounterDetailsList` +
        `?projectId=${encodeURIComponent(projectId)}` +
        `&counter=${encodeURIComponent(counterNo)}`;

    tagArea.innerHTML = `<span class="text-muted">Loading counter details...</span>`;
    qrBox.innerHTML = "";

    try {
        const resp = await fetch(url, {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

        if (!resp.ok) {
            throw new Error("HTTP " + resp.status);
        }

        const json = await resp.json();

        if (!json.success) {
            tagArea.innerHTML = `<span class="text-danger">Cannot load details.</span>`;
            return;
        }

        const items = json.data || [];

        if (!items.length) {
            const qrUrl =
                `${rootPath}QueueBankCounterView/CounterQr` +
                `?projectId=${encodeURIComponent(projectId)}` +
                `&projectName=${encodeURIComponent(projectName)}` +
                `&queueType=bank` +
                `&counterNo=${encodeURIComponent(counterNo)}`;

            tagArea.innerHTML = `<span class="text-muted">No register on this counter.</span>`;
            qrBox.innerHTML = `<img src="${qrUrl}" alt="QR" width="180">`;
            return;
        }

        // ===== Unit badges (หลายตัว) =====
        const unitSet = new Set();
        let tagHtml = "";

        items.forEach(it => {
            const registerLogId = it.ID || it.id || "";           // RL.ID
            const unitCode = it.UnitCode || it.unitCode || "";
            const unitId = it.UnitID || it.unitID || "";          // TR_RegisterLog.UnitID (Guid string)

            if (unitCode) {
                unitSet.add(unitCode);

                tagHtml += `
            <span class="badge bg-info text-white p-2 me-1 mb-1 counter-badge"
                  data-type="unit"
                  data-projectid="${projectId}"
                  data-id="${registerLogId}"
                  data-unitid="${unitId}"
                  data-counter="${counterNo}"
                  data-unitcode="${unitCode}"
                  data-bankid=""
                  data-bankcode="">
                ${unitCode}
                <i class="fa fa-times ms-1 badge-remove" role="button"></i>
            </span>
        `;
            }
        });

        const first = items[0] || {};
        const bankCode = first.BankCode || first.bankCode || "";
        const bankName = first.BankName || first.bankName || "";
        const bankId = first.BankID || first.bankId || ""; // int
        const firstRegisterLogId = first.ID || first.id || "";

        if (bankCode) {
            const logoHtml = bankCode
                ? `<img src="${rootPath}image/ThaiBankicon/${bankCode}.png" width="20" class="me-1">`
                : "";

            tagHtml += `
        <span class="badge bg-light border text-dark p-2 me-1 mb-1 counter-badge"
              data-type="bank"
              data-projectid="${projectId}"
              data-id="${firstRegisterLogId}"
              data-unitid=""
              data-counter="${counterNo}"
              data-unitcode=""
              data-bankid="${bankId}"
              data-bankcode="${bankCode}">
            ${logoHtml}${bankCode}
            <i class="fa fa-times ms-1 badge-remove" role="button"></i>
        </span>
    `;
        }

        tagArea.innerHTML = tagHtml || `<span class="text-muted">No detail data.</span>`;


        // ===== QR =====
        const qrUrl =
            `${rootPath}QueueBankCounterView/CounterQr` +
            `?projectId=${encodeURIComponent(projectId)}` +
            `&projectName=${encodeURIComponent(projectName)}` +
            `&queueType=bank` +
            `&counterNo=${encodeURIComponent(counterNo)}`;

        qrBox.innerHTML = `<img src="${qrUrl}" alt="QR" width="180">`;

        // ===== Click handler ปุ่ม x (bind แค่ครั้งแรก) =====
        if (!tagArea.dataset.boundClick) {
            tagArea.addEventListener("click", onCounterBadgeClicked);
            tagArea.dataset.boundClick = "1";
        }

    } catch (err) {
        console.error("❌ loadCounterDetail error:", err);
        tagArea.innerHTML = `<span class="text-danger">Error loading details.</span>`;
    }
}


// ======================
// Click handler: remove badge (unit / bank)
// ======================
async function onCounterBadgeClicked(e) {
    const icon = e.target.closest(".badge-remove");
    if (!icon) return;

    const badge = icon.closest(".counter-badge");
    if (!badge) return;

    const type = badge.dataset.type || "unit";   // "unit" | "bank"
    const projectId = badge.dataset.projectid || "";
    const registerLogId = parseInt(badge.dataset.id || "0", 10);
    const unitId = badge.dataset.unitid || "";
    const counterNo = badge.dataset.counter || "";
    const bankIdRaw = badge.dataset.bankid || "";
    const bankId = parseInt(bankIdRaw || "0", 10);
    const bankCode = badge.dataset.bankcode || "";

    console.log("🔥 badge clicked =", {
        type,
        projectId,
        registerLogId,
        unitId,
        counterNo,
        bankIdRaw,
        bankId,
        bankCode
    });

    // ---------- เคส BANK: CheckoutBankCounter ----------
    if (type === "bank") {

        if (!registerLogId || !bankId) {
            errorMessage("Bank or register is invalid.");
            return;
        }

        await callCheckoutBankCounter({
            RegisterLogID: registerLogId,
            BankID: bankId,
            ContactDetail: "" // ถ้าอยากให้กรอกโน้ตไว้ทีหลังค่อยมาเพิ่ม flow ตรงนี้
        }, badge, counterNo);

        return;
    }

    // ---------- เคส UNIT: RemoveUnitRegister ----------
    if (!projectId || !unitId) {
        errorMessage("Project or Unit is invalid.");
        return;
    }

    await callRemoveUnitRegister(
        {
            ProjectID: projectId,
            UnitID: unitId,
            Counter: parseInt(counterNo || "0", 10)
        },
        badge,
        counterNo
    );
}

async function callRemoveUnitRegister(payload, badge, counterNo) {
    const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");
    const url = `${rootPath}QueueBankCounterView/RemoveUnitRegister`;

    try {
        const resp = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) {
            throw new Error("HTTP " + resp.status);
        }

        const json = await resp.json();
        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        if (success) {
            successMessage(text);

            if (badge) {
                badge.remove();
            }

            if (typeof loadCounterList === "function") {
                loadCounterList();
            }
            if (typeof loadCounterDetail === "function" && counterNo) {
                loadCounterDetail(counterNo);
            }
        } else {
            errorMessage(text);
        }

    } catch (err) {
        console.error("❌ Error calling RemoveUnitRegister:", err);
        errorMessage("Error while removing unit from counter.");
    }
}

async function callCheckoutBankCounter(payload, badge, counterNo) {
    const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");
    const url = `${rootPath}QueueBankCounterView/CheckoutBankCounter`;

    try {
        const resp = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) {
            throw new Error("HTTP " + resp.status);
        }

        const json = await resp.json();
        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        if (success) {
            successMessage(text, "Bank Checked Out");

            if (badge) {
                badge.remove();
            }

            if (typeof loadCounterList === "function") {
                loadCounterList();
            }
            if (typeof loadCounterDetail === "function" && counterNo) {
                loadCounterDetail(counterNo);
            }
        } else {
            errorMessage(text);
        }

    } catch (err) {
        console.error("❌ Error calling CheckoutBankCounter:", err);
        errorMessage("Error while checking out bank counter.");
    }
}

async function onSaveUnitRegisterClicked() {
    const projectIdInput = document.getElementById("hidProjectId");
    const ddl = document.getElementById("ddlUnitRegister");

    const projectId = projectIdInput ? projectIdInput.value : "";
    const unitId = ddl ? ddl.value : "";
    const counterNo = currentCounterNo;

    if (!projectId) {
        errorMessage("Project is invalid.");
        return;
    }

    if (!counterNo) {
        errorMessage("Please select a counter first.");
        return;
    }

    if (!unitId) {
        errorMessage("Please select a unit.");
        return;
    }

    const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");
    const url = `${rootPath}QueueBankCounterView/UpdateUnitRegister`;

    const payload = {
        ProjectID: projectId,
        UnitID: unitId,
        Counter: parseInt(counterNo, 10)
    };

    console.log(">>> POST UpdateUnitRegister payload =", payload);

    try {
        const resp = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) {
            throw new Error("HTTP " + resp.status);
        }

        const json = await resp.json();
        console.log(">>> UpdateUnitRegister response =", json);

        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        // ---------------------
        // ⭐ SHOW MESSAGE
        // ---------------------
        if (success) {
            successMessage(text, "Completed");
        } else {
            errorMessage(text);
        }

        // ---------------------
        // ⭐ SUCCESS WORKFLOW
        // ---------------------
        if (success) {
            // Reload left card counters
            if (typeof loadCounterList === "function") {
                loadCounterList();
            }

            // Reload right detail panel
            if (typeof loadCounterDetail === "function") {
                loadCounterDetail(counterNo);
            }

            // Clear dropdown
            const ddlUnit = document.getElementById("ddlUnitRegister");

            if (window.unitRegisterChoices) {
                unitRegisterChoices.removeActiveItems();
                unitRegisterChoices.setChoiceByValue('');
            } else if (ddlUnit) {
                ddlUnit.value = "";
            }
        }

    } catch (err) {
        console.error("❌ Error calling UpdateUnitRegister:", err);
        errorMessage("Error while updating unit register.", "Request Failed");
    }
}

// ======================
// Summary Register (Register / Queue / Inprocess / Done + Loan + Career)
// ======================
function loadSummaryRegisterAll() {
    const filters = qbGetValuesCounterView();

    let projectId = filters.Project;
    if (Array.isArray(projectId)) {
        projectId = projectId[0] || "";
    }

    // 🔹 อัปเดตหัวข้อวันที่จาก filter (ของหน้า counter = "All Days")
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

            // 3) CareerType — ครบ 5 อาชีพ
            const careerList = res.listDataSummeryRegisterCareerTyp || [];
            const careerMap = qbMapByTopic(careerList);

            // พนักงานบริษัทเอกชนรายได้ประจำ
            qbUpdateSummaryBox(
                "career-freelance",
                careerMap["พนักงานบริษัทเอกชนรายได้ประจำ"]
            );

            // รายได้ประจำ
            qbUpdateSummaryBox(
                "career-salary",
                careerMap["รายได้ประจำ"]
            );

            // เจ้าของกิจการ
            qbUpdateSummaryBox(
                "career-owner",
                careerMap["เจ้าของกิจการ"]
            );

            // รัฐวิสาหกิจ
            qbUpdateSummaryBox(
                "career-soe",
                careerMap["รัฐวิสาหกิจ"]
            );

            // ราชการ
            qbUpdateSummaryBox(
                "career-government",
                careerMap["ราชการ"]
            );

        })
        .catch(err => {
            console.error("GetlistSummeryRegister error:", err);

            qbUpdateSummaryBox("register", null);
            qbUpdateSummaryBox("queue", null);
            qbUpdateSummaryBox("inprocess", null);
            qbUpdateSummaryBox("done", null);

            qbUpdateSummaryBox("loan-yes", null);
            qbUpdateSummaryBox("loan-no", null);

            // Career 5
            qbUpdateSummaryBox("career-freelance", null);
            qbUpdateSummaryBox("career-salary", null);
            qbUpdateSummaryBox("career-owner", null);
            qbUpdateSummaryBox("career-soe", null);
            qbUpdateSummaryBox("career-government", null);
        })
        .finally(() => {
            if (typeof hideLoading === "function") {
                hideLoading();
            }
        });
}


// ======================
// Summary Bank (table)
// ======================
// ===== Summary Bank (table) =====
function loadSummaryRegisterBank() {
    const filters = qbGetValuesCounterView(); // ✅ ใช้ของหน้า CounterView

    let projectId = filters.Project;
    if (Array.isArray(projectId)) projectId = projectId[0] || "";

    const formData = new FormData();

    // ==== QueueBank filters ====
    formData.append("L_Act", "SummeryRegisterBank"); // ✅ ให้เหมือนของเดิม
    formData.append("L_ProjectID", projectId || "");
    formData.append("L_RegisterDateStart", filters.RegisterDateStart || "");
    formData.append("L_RegisterDateEnd", filters.RegisterDateEnd || "");
    formData.append("L_UnitID", (filters.UnitCode || []).join(","));
    formData.append("L_CSResponse", (filters.CSResponsible || []).join(","));
    formData.append("L_UnitCS", (filters.UnitStatusCS || []).join(","));
    formData.append("L_ExpectTransfer", (filters.ExpectTransferBy || []).join(","));

    // Queue type ของหน้า Bank = 48
    formData.append("L_QueueTypeID", "48");

    // model params
    formData.append("draw", "1");
    formData.append("start", "0");
    formData.append("length", "1000");
    formData.append("SearchTerm", "");

    const tbodyBank = document.getElementById("summary-bank-body");
    const tbodyNon = document.getElementById("summary-banknonsubmissionreason-body");

    if (tbodyBank) {
        tbodyBank.innerHTML = `<tr><td colspan="5" class="text-center text-muted">Loading...</td></tr>`;
    }
    if (tbodyNon) {
        tbodyNon.innerHTML = `<tr><td colspan="3" class="text-center text-muted">Loading...</td></tr>`;
    }

    if (typeof showLoading === "function") showLoading();

    fetch(baseUrl + "QueueBank/GetlistSummeryRegisterBank", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {

            /* =========================
               1) Summary Bank
               ========================= */
            if (tbodyBank) {
                const listBank = res.listDataSummeryRegisterBank || [];

                if (!listBank.length) {
                    tbodyBank.innerHTML = `<tr><td colspan="5" class="text-center text-muted">No data</td></tr>`;
                } else {
                    tbodyBank.innerHTML = listBank.map(item => {
                        const bankCode = (item.BankCode || "").trim();
                        const bankName = item.BankName || "";
                        const unit = item.Unit ?? 0;

                        const valueText = (typeof qbFormatValueM === "function")
                            ? qbFormatValueM(item.Value)
                            : (item.Value ?? "0");

                        const percentText = (item.Percent ?? "0") + "%";
                        const interestRate = (item.InterestRateAVG ?? "0") + "%";

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
                                <td class="text-center">${interestRate}</td>
                                <td class="text-center">${unit}</td>
                                <td class="text-end">${valueText}</td>
                                <td class="text-center">${percentText}</td>
                            </tr>`;
                    }).join("");
                }
            }

            /* =======================================
               2) Non-Submission Reason
               ======================================= */
            if (tbodyNon) {
                const listNon = res.listDataSummeryRegisterBankNonSubmissionReason || [];

                if (!listNon.length) {
                    tbodyNon.innerHTML = `<tr><td colspan="3" class="text-center text-muted">No data</td></tr>`;
                } else {
                    tbodyNon.innerHTML = listNon.map(item => {
                        // รองรับได้ทั้ง Name/Topic และ Count/Unit แล้วแต่ backend ส่งมา
                        const name = item.Name ?? item.Topic ?? "-";
                        const count = item.Count ?? item.Unit ?? 0;

                        let percent = (item.Percent ?? "0").toString().trim();
                        if (percent !== "" && !percent.endsWith("%")) percent += "%";

                        return `
                            <tr>
                                <td>${name}</td>
                                <td class="text-center">${count}</td>
                                <td class="text-center">${percent}</td>
                            </tr>`;
                    }).join("");
                }
            }
        })
        .catch(err => {
            console.error("GetlistSummeryRegisterBank error:", err);

            if (tbodyBank) {
                tbodyBank.innerHTML = `<tr><td colspan="5" class="text-center text-danger">Error loading Summary Bank</td></tr>`;
            }
            if (tbodyNon) {
                tbodyNon.innerHTML = `<tr><td colspan="3" class="text-center text-danger">Error loading Non-Submission Reason</td></tr>`;
            }
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}




document.addEventListener("DOMContentLoaded", function () {
    // โหลด counter list ครั้งแรก
    loadCounterList();

    // ⭐ Summary Register + Summary Bank (ใช้ฟังก์ชันเดียวกับหน้าหลัก)
    loadSummaryRegisterAll();
    loadSummaryRegisterBank();

    // ปุ่ม Refresh → reload counters + summary
    const btnRefresh = document.getElementById("btnRefreshCounter");
    if (btnRefresh && !btnRefresh.dataset.bound) {
        btnRefresh.addEventListener("click", function (e) {
            e.preventDefault();
            loadCounterList();
            loadSummaryRegisterAll();
            loadSummaryRegisterBank();
        });
        btnRefresh.dataset.bound = "1";
    }

    // ⭐ Init Choices.js (ddlUnitRegister)
    const ddl = document.getElementById("ddlUnitRegister");
    if (ddl && window.Choices) {
        unitRegisterChoices = new Choices(ddl, {
            searchEnabled: true,
            itemSelectText: "",
            shouldSort: false,
            removeItemButton: false
        });
    }

    // ⭐ Bind ปุ่ม OK
    const btnSave = document.getElementById("btnSaveUnitRegister");
    if (btnSave && !btnSave.dataset.bound) {
        btnSave.addEventListener("click", onSaveUnitRegisterClicked);
        btnSave.dataset.bound = "1";
    }
});

