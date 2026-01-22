/* =========================================================
  QueueBankCounterView.js  (FULL FILE - upgraded like Checker)
  - Adds: Ding + Blink + Confirm remove + Stop Call Staff UI
  - Keeps: Collapse toggle, Fullscreen, Counter Grid, Right panel detail,
           Bank/QR mode, Remove badge, Update unit register
  - Adds: Safe single-bind (dataset flags) to avoid double events after re-render
  - Supports: Old SignalR (jQuery hub) via window.ChatProxy (if exists)

  ✅ IMPORTANT:
  - Endpoints used (CounterView):
      QueueBankCounterView/GetCounterList
      QueueBankCounterView/GetCounterDetailsList
      QueueBankCounterView/CounterQr
      QueueBankCounterView/RemoveUnitRegister
      QueueBankCounterView/CheckoutBankCounter
      QueueBankCounterView/UpdateUnitRegister

  - Stop Call Staff API:
      ✅ REAL = QueueBankCheckerView/SaveRegisterCallStaffCounter (because controller has it)
      If you move action to CounterViewController -> change QB_STOP_CALL_API back.

  - Hub URL:
      ✅ Prefer window.__HUB_URL set by Razor: '@Url.Content("~/notifyHub")'
========================================================= */

/* =========================
   [A] Global State
========================= */
let currentCounterNo = null;
let unitRegisterChoices = null;

let qbLastDingAt = 0;
let qbBlinkSet = new Set();
let qbBlinkTimers = new Map(); // counterNo -> timeoutId

// counterNo -> { projectId, registerLogId }
let qbCallStaffMap = new Map();

/* =========================
   [A.1] Config (edit if needed)
========================= */
const QB_QUEUE_TYPE_ID = 48;

// ✅ Stop Call Staff API (Controller จริงอยู่ที่ CheckerViewController)
const QB_STOP_CALL_API = "QueueBankCheckerView/SaveRegisterCallStaffCounter";

/* =========================
   [A.2] Helpers
========================= */
function qbRootPath() {
    // Prefer value injected from Razor
    if (window.__APP_BASE) return window.__APP_BASE;
    return (typeof baseUrl !== "undefined" ? baseUrl : "/");
}
function qbHubUrl() {
    // Prefer value injected from Razor: window.__HUB_URL = '@Url.Content("~/notifyHub")'
    if (window.__HUB_URL) return window.__HUB_URL;

    // fallback
    const rootPath = qbRootPath();
    return rootPath.replace(/\/?$/, "/") + "notifyHub";
}
function qbNormalizeCounterNo(counterNo) {
    return String(counterNo ?? "").trim();
}
function qbNormStatus(s) {
    return (s ?? "").toString().trim().toLowerCase();
}
function qbClickIfExists(id) {
    document.getElementById(id)?.click();
}
function qbHasValue(v) {
    if (v === null || v === undefined) return false;
    const s = String(v).trim();
    if (s === "" || s.toLowerCase() === "null" || s.toLowerCase() === "undefined") return false;
    return true;
}

/* =========================
   [SOUND] Ding (safe unlock)
========================= */
let qbSoundUnlocked = false;
const qbDingAudio = new Audio(qbRootPath() + "sounds/counter-ding.mp3");
qbDingAudio.preload = "auto";

function qbUnlockSoundOnce() {
    qbDingAudio.muted = true;

    qbDingAudio.play().then(() => {
        qbDingAudio.pause();
        qbDingAudio.currentTime = 0;
        qbDingAudio.muted = false;
        qbSoundUnlocked = true;

        document.removeEventListener("click", qbUnlockSoundOnce);
        document.removeEventListener("keydown", qbUnlockSoundOnce);
        console.log("✅ Sound unlocked");
    }).catch(() => { });
}

document.addEventListener("click", qbUnlockSoundOnce, { once: false });
document.addEventListener("keydown", qbUnlockSoundOnce, { once: false });

function qbPlayDingSafe() {
    if (!qbSoundUnlocked) return;
    try {
        qbDingAudio.currentTime = 0;
        qbDingAudio.play().catch(() => { });
    } catch { }
}

function qbPlayDingCooldown(ms = 1500) {
    const now = Date.now();
    if (now - qbLastDingAt < ms) return;
    qbLastDingAt = now;
    qbPlayDingSafe();
}

/* =========================
   [BLINK] Counter Blink Engine
========================= */
function qbApplyBlinkToGrid() {
    const grid = document.getElementById("counterGrid");
    if (!grid) return;

    grid.querySelectorAll(".qb-counter").forEach(box => {
        const no = qbNormalizeCounterNo(box.dataset.counter);
        const shouldBlink = qbBlinkSet.has(no);
        if (shouldBlink) box.classList.add("blink");
        else box.classList.remove("blink");
    });
}

function qbBlinkCounters(counterList, { durationMs = 15000, replace = false } = {}) {
    const list = (counterList || []).map(qbNormalizeCounterNo).filter(Boolean);
    if (!list.length) return;

    let hasNew = false;
    if (replace) qbBlinkClearAll();

    list.forEach(no => {
        if (!qbBlinkSet.has(no)) hasNew = true;
        qbBlinkSet.add(no);

        if (qbBlinkTimers.has(no)) {
            clearTimeout(qbBlinkTimers.get(no));
            qbBlinkTimers.delete(no);
        }

        if (durationMs > 0) {
            const t = setTimeout(() => {
                qbBlinkSet.delete(no);
                qbBlinkTimers.delete(no);
                qbApplyBlinkToGrid();
            }, durationMs);
            qbBlinkTimers.set(no, t);
        }
    });

    if (hasNew) qbPlayDingSafe();
    qbApplyBlinkToGrid();
}

function qbBlinkStop(counterNo) {
    const no = qbNormalizeCounterNo(counterNo);
    if (!no) return;

    qbBlinkSet.delete(no);
    if (qbBlinkTimers.has(no)) {
        clearTimeout(qbBlinkTimers.get(no));
        qbBlinkTimers.delete(no);
    }
    qbApplyBlinkToGrid();
}

function qbBlinkClearAll() {
    qbBlinkSet.clear();
    qbBlinkTimers.forEach(t => clearTimeout(t));
    qbBlinkTimers.clear();
    qbApplyBlinkToGrid();
}

/* =========================
   [CONFIRM] SweetAlert2 fallback
========================= */
async function qbConfirm({ title, text, confirmText = "Yes", cancelText = "Cancel" }) {
    if (window.Swal && typeof Swal.fire === "function") {
        const res = await Swal.fire({
            icon: "warning",
            title: title || "Confirm",
            text: text || "Are you sure?",
            showCancelButton: true,
            confirmButtonText: confirmText,
            cancelButtonText: cancelText,
            reverseButtons: true,
            allowOutsideClick: false
        });
        return res.isConfirmed === true;
    }
    return window.confirm(`${title ? title + "\n" : ""}${text || "Are you sure?"}`);
}

/* =========================
   [UI] Collapse Chevron Toggle
========================= */
function initCollapseChevronToggle() {
    const toggles = document.querySelectorAll('[data-bs-toggle="collapse"][data-bs-target]');
    toggles.forEach(btn => {
        if (btn.dataset.boundChevron === "1") return;
        btn.dataset.boundChevron = "1";

        const targetSel = btn.getAttribute("data-bs-target");
        const target = document.querySelector(targetSel);
        if (!target) return;

        const icon = btn.querySelector("i");
        if (!icon) return;

        target.addEventListener("show.bs.collapse", () => {
            icon.classList.remove("fa-chevron-down");
            icon.classList.add("fa-chevron-up");
            btn.setAttribute("aria-expanded", "true");
        });

        target.addEventListener("hide.bs.collapse", () => {
            icon.classList.remove("fa-chevron-up");
            icon.classList.add("fa-chevron-down");
            btn.setAttribute("aria-expanded", "false");
        });

        const isShown = target.classList.contains("show");
        icon.classList.toggle("fa-chevron-up", isShown);
        icon.classList.toggle("fa-chevron-down", !isShown);
        btn.setAttribute("aria-expanded", String(isShown));
    });
}

/* =========================
   [UI] Fullscreen
========================= */
function initFullscreen() {
    const btnFull = document.getElementById("btnFullScreen");
    const container = document.getElementById("Container_counter");
    if (!btnFull || !container) return;

    if (btnFull.dataset.bound === "1") return;
    btnFull.dataset.bound = "1";

    function enterFullScreen() {
        if (container.requestFullscreen) container.requestFullscreen();
        container.classList.add("fullscreen-mode");
        btnFull.innerHTML = '<i class="fa fa-compress"></i>';
        updateCounterGridLayout();
    }

    function exitFullScreen() {
        if (document.fullscreenElement) document.exitFullscreen();
        container.classList.remove("fullscreen-mode");
        btnFull.innerHTML = '<i class="fa fa-expand"></i>';
        updateCounterGridLayout();
    }

    btnFull.addEventListener("click", function () {
        const isFull = container.classList.contains("fullscreen-mode");
        if (!isFull) enterFullScreen();
        else exitFullScreen();
    });

    if (container.dataset.boundFullscreenChange !== "1") {
        container.dataset.boundFullscreenChange = "1";
        document.addEventListener("fullscreenchange", function () {
            if (!document.fullscreenElement && container.classList.contains("fullscreen-mode")) {
                container.classList.remove("fullscreen-mode");
                btnFull.innerHTML = '<i class="fa fa-expand"></i>';
                updateCounterGridLayout();
            }
        });
    }
}

/* =========================
   [UI] Layout grid (right panel show/hide)
========================= */
function updateCounterGridLayout() {
    const grid = document.getElementById("counterGrid");
    const detailCol = document.getElementById("counterDetailColumn");
    if (!grid) return;

    const cols = grid.querySelectorAll(".counter-col");
    const isDetailHidden = !detailCol || detailCol.classList.contains("d-none");

    cols.forEach(col => {
        col.classList.remove("col-md-2", "col-lg-2", "col-md-3", "col-lg-3");
        if (!col.classList.contains("col-6")) col.classList.add("col-6");

        if (isDetailHidden) col.classList.add("col-md-2", "col-lg-2");
        else col.classList.add("col-md-3", "col-lg-3");
    });
}

/* =========================
   [DATA] Load Counter List
========================= */
async function loadCounterList() {
    const grid = document.getElementById("counterGrid");
    if (!grid) return;

    const projectId = document.getElementById("hidProjectId")?.value || "";
    if (!projectId) {
        grid.innerHTML = `<div class="col-12 text-center text-danger">Project ID not found.</div>`;
        return;
    }

    grid.innerHTML = `<div class="col-12 text-center text-muted" id="counterGridLoading">Loading counters...</div>`;

    try {
        const url = `${qbRootPath()}QueueBankCounterView/GetCounterList?projectId=${encodeURIComponent(projectId)}`;
        const resp = await fetch(url, { method: "GET", headers: { "Accept": "application/json" } });
        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        if (!json.success) {
            console.error("GetCounterList error:", json.message);
            renderCounterGrid([]);
            return;
        }

        renderCounterGrid(json.data || []);
    } catch (err) {
        console.error("Fetch counter list failed:", err);
        grid.innerHTML = `<div class="col-12 text-center text-danger">Cannot load counter list.</div>`;
    }
}

/* =========================
   [UI] Render counter grid
========================= */
function renderCounterGrid(items) {
    const grid = document.getElementById("counterGrid");
    const loadingEl = document.getElementById("counterGridLoading");
    if (!grid) return;
    if (loadingEl) loadingEl.remove();

    if (!items || !items.length) {
        grid.innerHTML = `<div class="col-12 text-center text-muted">No counters configured for this project.</div>`;
        return;
    }

    const rootPath = qbRootPath();
    let html = "";

    items.forEach(item => {
        const counterNo = item.Counter || item.counter || "";
        const bankCode = item.BankCode || item.bankCode || "";
        const bankName = item.BankName || item.bankname || item.bankName || "";
        const unitCode = item.UnitCode || item.unitCode || "";
        const registerLogID = item.RegisterLogID ?? item.registerLogID ?? "";
        const inProcessDate = item.InProcessDate ?? item.inprocessDate ?? item.inProcessDate ?? "";

        const hasInProcess = qbHasValue(inProcessDate);
        const isActive = qbHasValue(registerLogID);
        const hasLogo = qbHasValue(bankCode);

        const boxClass =
            "counter-box qb-counter " +
            (isActive ? "active" : "empty") +
            (hasInProcess ? " inprocess" : "") +
            (!hasLogo ? " no-logo" : "");

        // Header color
        let headerStyle = "";
        if (hasInProcess) headerStyle = "background-color:#198754;color:#ffffff;";
        else if (isActive) headerStyle = "background-color:#dc3545;color:#ffffff;";
        else headerStyle = "background-color:#6c757d;color:#ffffff;";

        // Bank logo 55px (override CSS)
        const bankLogoHtml = bankCode
            ? `<img src="${rootPath}image/ThaiBankicon/${bankCode}.png"
                    alt="${bankCode}"
                    class="me-2"
                    style="width:55px !important;height:auto;object-fit:contain;">`
            : "";

        // ✅ Body content: keep SAME height for all cards, but no visible "-" for empty
        let bodyContent = "";
        let bodyStyle = "";

        if (isActive) {
            // normal active
            bodyContent = `
                <div class="d-flex align-items-center justify-content-center gap-2 w-100">
                    ${hasLogo ? `<span class="d-inline-flex align-items-center">${bankLogoHtml}</span>` : ""}
                    <span style="font-size:1.80rem;">
                        ${unitCode || ""}
                    </span>

                </div>
            `;
            bodyStyle = "";
        } else {
            // ✅ empty: put invisible spacer that matches the visual height of "logo + text"
            // - no "-" shown
            // - card height stays consistent
            bodyContent = `
                <div class="d-flex align-items-center justify-content-center w-100"
                     style="min-height:40px;">
                    <span style="opacity:0; user-select:none;">SPACER</span>
                </div>
            `;
            bodyStyle = ""; // keep CSS theme (blue/grey) and size
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
            ${isActive
                ? `<span class="counter-label">Counter</span>
                       <span class="counter-sep">:</span>
                       <span class="counter-no">${counterNo}</span>`
                : `<span class="counter-no">${counterNo}</span>`
            }
        </div>
      <div class="counter-body" style="${bodyStyle}">${bodyContent}</div>
    </div>
  </div>
`;

    });

    grid.innerHTML = html;

    // Save original state for mode switch
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
        box.dataset.originalHeaderStyle = header.getAttribute("style") || "";
    });

    initCounterModeButtons();
    initCounterCardClick();
    updateCounterGridLayout();
    qbApplyBlinkToGrid();
}



/* =========================
   [UI] Counter Mode Buttons (Bank / QR)
========================= */
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
            header.setAttribute("style", box.dataset.originalHeaderStyle || "");
        });

        setButtonsMode("bank");
        updateCounterGridLayout();
        qbApplyBlinkToGrid();
    }

    function setQRMode() {
        const rootPath = qbRootPath();
        const projectId = document.getElementById("hidProjectId")?.value || "";
        const projectName = document.getElementById("project_name")?.textContent?.trim() || "";

        const boxes = Array.from(grid.querySelectorAll(".counter-box"));
        boxes.forEach(box => {
            const header = box.querySelector(".counter-header");
            const body = box.querySelector(".counter-body");
            if (!header || !body) return;

            const counterNo = box.dataset.counter || "";
            if (!counterNo) return;

            const originalClass = box.dataset.originalBoxClass || box.className;
            const hasInProcess = originalClass.includes("inprocess");
            const isActive = originalClass.includes("active");
            const isEmpty = originalClass.includes("empty");

            box.className = originalClass;
            box.classList.add("qr-mode");

            const qrUrl =
                `${rootPath}QueueBankCounterView/CounterQr` +
                `?projectId=${encodeURIComponent(projectId)}` +
                `&projectName=${encodeURIComponent(projectName)}` +
                `&queueType=bank` +
                `&counterNo=${encodeURIComponent(counterNo)}`;

            header.className = "counter-header";
            header.style.color = "#ffffff";
            header.textContent = `Counter : ${counterNo}`;

            if (hasInProcess) header.style.backgroundColor = "#198754";
            else if (isActive) header.style.backgroundColor = "#dc3545";
            else if (isEmpty) header.style.backgroundColor = "#6c757d";
            else header.style.backgroundColor = "#6c757d";

            body.className = "counter-body";
            body.innerHTML = `
        <div class="d-flex justify-content-center align-items-center" style="min-height:60px;">
          <div class="qr-wrap">
            <img src="${qrUrl}" class="counter-qr" alt="QR Code for Counter ${counterNo}" style="width:64px;height:auto;">
          </div>
        </div>
      `;
        });

        setButtonsMode("qr");
        updateCounterGridLayout();
        qbApplyBlinkToGrid();
    }

    if (!btnBank.dataset.bound) {
        btnBank.addEventListener("click", e => { e.preventDefault(); setBankMode(); });
        btnBank.dataset.bound = "1";
    }
    if (!btnQR.dataset.bound) {
        btnQR.addEventListener("click", e => { e.preventDefault(); setQRMode(); });
        btnQR.dataset.bound = "1";
    }

    // default
    setBankMode();
}

/* =========================
   [CALL STAFF] Stop button injection + UI sync
========================= */
function qbEnsureStopButtonUI() {
    const detailBox = document.querySelector("#counterDetailColumn .counter-detail-box");
    if (!detailBox) return;

    if (detailBox.dataset.boundStopBtn === "1") return;
    detailBox.dataset.boundStopBtn = "1";

    let footer = document.getElementById("counterDetailFooter");
    if (!footer) {
        footer = document.createElement("div");
        footer.id = "counterDetailFooter";
        footer.className = "d-flex justify-content-center gap-2 mt-3 pt-2 border-top";
        detailBox.appendChild(footer);
    }

    if (!document.getElementById("btnStopCallStaff")) {
        const btn = document.createElement("button");
        btn.type = "button";
        btn.id = "btnStopCallStaff";
        btn.className = "btn btn-sm btn-danger d-none";
        btn.innerHTML = `<i class="fa fa-bell-slash me-1"></i> Stop Call Staff`;
        btn.addEventListener("click", qbOnStopCallStaffClicked);
        footer.appendChild(btn);
    }
}

function qbUpdateStopButtonUI(counterNo) {
    qbEnsureStopButtonUI();

    const stopBtn = document.getElementById("btnStopCallStaff");
    if (!stopBtn) return;

    const no = qbNormalizeCounterNo(counterNo);
    const info = qbCallStaffMap.get(no);

    if (info) {
        stopBtn.classList.remove("d-none");
        stopBtn.dataset.counter = no;
        stopBtn.dataset.projectid = info.projectId || "";
        stopBtn.dataset.registerlogid = String(info.registerLogId || "");
    } else {
        stopBtn.classList.add("d-none");
        stopBtn.dataset.counter = "";
        stopBtn.dataset.projectid = "";
        stopBtn.dataset.registerlogid = "";
    }
}

async function qbResolveRegisterLogId(projectId, counterNo) {
    const url =
        `${qbRootPath()}QueueBankCounterView/GetCounterDetailsList` +
        `?projectId=${encodeURIComponent(projectId)}` +
        `&counter=${encodeURIComponent(counterNo)}`;

    const resp = await fetch(url, { method: "GET", headers: { "Accept": "application/json" } });
    if (!resp.ok) throw new Error("HTTP " + resp.status);

    const json = await resp.json();
    if (!json.success) return 0;

    const items = json.data || [];
    const first = items[0] || {};
    const id = parseInt(first.ID || first.id || "0", 10);
    return Number.isFinite(id) ? id : 0;
}

async function qbOnStopCallStaffClicked() {
    const stopBtn = document.getElementById("btnStopCallStaff");
    if (!stopBtn) return;

    const projectId = stopBtn.dataset.projectid || (document.getElementById("hidProjectId")?.value || "");
    const counterNo = stopBtn.dataset.counter || (currentCounterNo || "");
    let registerLogId = parseInt(stopBtn.dataset.registerlogid || "0", 10);

    // fallback 1: selected card
    if (!registerLogId) {
        const selectedBox = document.querySelector("#counterGrid .qb-counter.selected");
        const cardRegId = selectedBox?.dataset?.registerid || "";
        registerLogId = parseInt(cardRegId || "0", 10);
    }

    // fallback 2: map
    if (!registerLogId) {
        const info = qbCallStaffMap.get(qbNormalizeCounterNo(counterNo));
        registerLogId = parseInt(info?.registerLogId || "0", 10);
    }

    // fallback 3: fetch
    if (!registerLogId) {
        try {
            registerLogId = await qbResolveRegisterLogId(projectId, counterNo);
            if (registerLogId) {
                stopBtn.dataset.registerlogid = String(registerLogId);
                const no = qbNormalizeCounterNo(counterNo);
                const info = qbCallStaffMap.get(no) || { projectId: projectId, registerLogId: 0 };
                info.projectId = projectId;
                info.registerLogId = registerLogId;
                qbCallStaffMap.set(no, info);
            }
        } catch (e) {
            console.error("Resolve RegisterLogID failed:", e);
        }
    }

    if (!projectId) { errorMessage?.("Project is invalid."); return; }
    if (!counterNo) { errorMessage?.("Counter is invalid."); return; }
    if (!registerLogId) { errorMessage?.("RegisterLogID is invalid."); return; }

    const ok = await qbConfirm({
        title: "Stop call staff?",
        text: `Counter ${counterNo} will stop calling staff.`,
        confirmText: "Yes, stop",
        cancelText: "Cancel"
    });
    if (!ok) return;

    const url = qbRootPath() + QB_STOP_CALL_API;

    const payload = {
        ProjectID: projectId,
        QueueTypeID: QB_QUEUE_TYPE_ID,
        Counter: parseInt(counterNo, 10),
        RegisterLogID: registerLogId,
        CallStaffStatus: "stop",
        RegisterLogList: []
    };

    try {
        if (typeof showLoading === "function") showLoading();

        const resp = await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        if (!json.Success) {
            errorMessage?.(json.Message || "Stop call staff failed.");
            return;
        }

        successMessage?.(json.Message || "Stopped.");

        qbCallStaffMap.delete(qbNormalizeCounterNo(counterNo));
        qbUpdateStopButtonUI(counterNo);
        qbBlinkStop(counterNo);

        loadCounterList?.();
        if (currentCounterNo === counterNo) loadCounterDetail?.(counterNo);

        // ✅ Broadcast ให้ Checker/Counter ทุกจอรู้ว่าหยุดแล้ว
        await qbStopCallStaffViaNewHub({
            ProjectID: projectId,
            QueueTypeID: QB_QUEUE_TYPE_ID,
            Counter: parseInt(counterNo, 10),
            RegisterLogID: registerLogId,
            CallStaffStatus: "stop",
            Action: "StopCallStaff"
        });

        // (ถ้าอยากให้ refresh ทุกหน้าเพิ่ม)
        await qbNotifyCounterViaNewHub({
            ProjectID: projectId,
            QueueTypeID: QB_QUEUE_TYPE_ID,
            Counter: parseInt(counterNo, 10),
            Action: "StopCallStaff"
        });


    } catch (err) {
        console.error("❌ StopCallStaff error:", err);
        errorMessage?.("Error while stopping call staff.");
    } finally {
        if (typeof hideLoading === "function") hideLoading();
    }
}

/* =========================
   [UI] Click counter -> open right panel
   ✅ Prevent double binding
========================= */
function initCounterCardClick() {
    const grid = document.getElementById("counterGrid");
    const detailCol = document.getElementById("counterDetailColumn");
    const titleEl = document.getElementById("counterDetailTitle");
    const closeBtn = document.getElementById("btnCloseCounterDetail");
    const leftCol = document.getElementById("counterGridColumn");
    if (!grid || !detailCol) return;

    // initial: right hidden => left full width
    if (leftCol && detailCol.classList.contains("d-none")) {
        leftCol.classList.remove("col-lg-8");
        leftCol.classList.add("col-lg-12");
        updateCounterGridLayout();
    }

    // ✅ bind grid click once
    if (grid.dataset.boundCounterClick !== "1") {
        grid.dataset.boundCounterClick = "1";

        grid.addEventListener("click", function (e) {
            const box = e.target.closest(".qb-counter");
            if (!box || !grid.contains(box)) return;

            const counterNo = box.dataset.counter || "";
            if (!counterNo) return;

            currentCounterNo = counterNo;

            detailCol.classList.remove("d-none");

            // left 8
            if (leftCol) {
                leftCol.classList.remove("col-lg-12");
                leftCol.classList.add("col-lg-8");
            }

            if (titleEl) titleEl.textContent = `Counter : ${counterNo}`;

            grid.querySelectorAll(".qb-counter.selected").forEach(el => el.classList.remove("selected"));
            box.classList.add("selected");

            qbBlinkStop(counterNo); // ✅ click stops blink for this counter
            updateCounterGridLayout();

            // ✅ stop button sync
            qbUpdateStopButtonUI(counterNo);

            // load detail
            if (typeof loadCounterDetail === "function") loadCounterDetail(counterNo);

            // reset dropdown selection
            const ddl = document.getElementById("ddlUnitRegister");
            if (unitRegisterChoices) {
                unitRegisterChoices.removeActiveItems();
                unitRegisterChoices.setChoiceByValue("");
            } else if (ddl) ddl.value = "";
        });
    }

    // ✅ bind close once
    if (closeBtn && closeBtn.dataset.bound !== "1") {
        closeBtn.addEventListener("click", function () {
            detailCol.classList.add("d-none");

            if (leftCol) {
                leftCol.classList.remove("col-lg-8");
                leftCol.classList.add("col-lg-12");
            }

            grid.querySelectorAll(".qb-counter.selected").forEach(el => el.classList.remove("selected"));
            currentCounterNo = null;

            updateCounterGridLayout();
        });
        closeBtn.dataset.bound = "1";
    }
}

/* =========================
   [DATA] Load Counter Detail (Right Panel)
========================= */
async function loadCounterDetail(counterNo) {
    const projectIdInput = document.getElementById("hidProjectId");
    const projectNameEl = document.getElementById("project_name");
    const tagArea = document.getElementById("counterTagArea");
    const qrBox = document.getElementById("counterQrBox");

    if (!projectIdInput || !tagArea || !qrBox) return;

    const projectId = projectIdInput.value || "";
    const projectName = projectNameEl ? projectNameEl.textContent.trim() : "";

    const url =
        `${qbRootPath()}QueueBankCounterView/GetCounterDetailsList` +
        `?projectId=${encodeURIComponent(projectId)}` +
        `&counter=${encodeURIComponent(counterNo)}`;

    tagArea.innerHTML = `<span class="text-muted">Loading counter details...</span>`;
    qrBox.innerHTML = "";

    try {
        const resp = await fetch(url, { method: "GET", headers: { "Accept": "application/json" } });
        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        if (!json.success) {
            tagArea.innerHTML = `<span class="text-danger">Cannot load details.</span>`;
            return;
        }

        const items = json.data || [];

        // no items => show QR only
        if (!items.length) {
            const qrUrl =
                `${qbRootPath()}QueueBankCounterView/CounterQr` +
                `?projectId=${encodeURIComponent(projectId)}` +
                `&projectName=${encodeURIComponent(projectName)}` +
                `&queueType=bank` +
                `&counterNo=${encodeURIComponent(counterNo)}`;

            tagArea.innerHTML = `<span class="text-muted">No register on this counter.</span>`;
            qrBox.innerHTML = `<img src="${qrUrl}" alt="QR" width="180">`;

            qbUpdateStopButtonUI(counterNo);
            return;
        }

        let tagHtml = "";

        items.forEach(it => {
            const registerLogId = it.ID || it.id || "";
            const unitCode = it.UnitCode || it.unitCode || "";
            const unitId = it.UnitID || it.unitID || "";

            if (unitCode) {
                tagHtml += `
          <span class="badge bg-info text-white p-2 me-1 mb-1 counter-badge"
                data-type="unit"
                data-projectid="${projectId}"
                data-id="${registerLogId}"
                data-unitid="${unitId}"
                data-counter="${counterNo}">
            ${unitCode}
            <i class="fa fa-times ms-1 badge-remove" role="button"></i>
          </span>`;
            }
        });

        const first = items[0] || {};
        const bankCode = first.BankCode || first.bankCode || "";
        const bankId = first.BankID || first.bankId || "";
        const firstRegisterLogId = first.ID || first.id || "";

        if (bankCode) {
            const logoHtml = `<img src="${qbRootPath()}image/ThaiBankicon/${bankCode}.png" width="20" class="me-1">`;
            tagHtml += `
        <span class="badge bg-light border text-dark p-2 me-1 mb-1 counter-badge"
              data-type="bank"
              data-projectid="${projectId}"
              data-id="${firstRegisterLogId}"
              data-counter="${counterNo}"
              data-bankid="${bankId}"
              data-bankcode="${bankCode}">
          ${logoHtml}${bankCode}
          <i class="fa fa-times ms-1 badge-remove" role="button"></i>
        </span>`;
        }

        tagArea.innerHTML = tagHtml || `<span class="text-muted">No detail data.</span>`;

        const qrUrl =
            `${qbRootPath()}QueueBankCounterView/CounterQr` +
            `?projectId=${encodeURIComponent(projectId)}` +
            `&projectName=${encodeURIComponent(projectName)}` +
            `&queueType=bank` +
            `&counterNo=${encodeURIComponent(counterNo)}`;

        qrBox.innerHTML = `<img src="${qrUrl}" alt="QR" width="180">`;

        // bind click once
        if (!tagArea.dataset.boundClick) {
            tagArea.addEventListener("click", onCounterBadgeClicked);
            tagArea.dataset.boundClick = "1";
        }

        // ✅ fill registerLogId into map if needed
        const no = qbNormalizeCounterNo(counterNo);
        if (qbCallStaffMap.has(no)) {
            const info = qbCallStaffMap.get(no);
            if (!info.registerLogId && firstRegisterLogId) {
                info.registerLogId = parseInt(firstRegisterLogId, 10) || 0;
                qbCallStaffMap.set(no, info);
            }
        }

        qbUpdateStopButtonUI(counterNo);

    } catch (err) {
        console.error("❌ loadCounterDetail error:", err);
        tagArea.innerHTML = `<span class="text-danger">Error loading details.</span>`;
    }
}

/* =========================
   [ACTION] Remove badge click (unit/bank) + CONFIRM
========================= */
async function onCounterBadgeClicked(e) {
    const icon = e.target.closest(".badge-remove");
    if (!icon) return;

    const badge = icon.closest(".counter-badge");
    if (!badge) return;

    const type = badge.dataset.type || "unit";
    const projectId = badge.dataset.projectid || "";
    const registerLogId = parseInt(badge.dataset.id || "0", 10);
    const unitId = badge.dataset.unitid || "";
    const counterNo = badge.dataset.counter || "";
    const bankId = parseInt(badge.dataset.bankid || "0", 10);

    if (type === "bank") {
        const bankCode = badge.dataset.bankcode || "";
        if (!registerLogId || !bankId) {
            errorMessage?.("Bank or register is invalid.");
            return;
        }

        const ok = await qbConfirm({
            title: "Remove bank from counter?",
            text: `Counter ${counterNo} : ${bankCode || "Bank"} will be checked out.`,
            confirmText: "Yes, remove",
            cancelText: "No"
        });
        if (!ok) return;

        await callCheckoutBankCounter({ RegisterLogID: registerLogId, BankID: bankId, ContactDetail: "" }, badge, counterNo);
        return;
    }

    if (!projectId || !unitId) {
        errorMessage?.("Project or Unit is invalid.");
        return;
    }

    const unitCodeText = (badge.textContent || "").replace("×", "").trim();
    const ok = await qbConfirm({
        title: "Remove unit from counter?",
        text: `Counter ${counterNo} : ${unitCodeText || "This unit"} will be removed.`,
        confirmText: "Yes, remove",
        cancelText: "No"
    });
    if (!ok) return;

    await callRemoveUnitRegister(
        { ProjectID: projectId, UnitID: unitId, Counter: parseInt(counterNo || "0", 10) },
        badge,
        counterNo
    );
}

async function callRemoveUnitRegister(payload, badge, counterNo) {
    const url = `${qbRootPath()}QueueBankCounterView/RemoveUnitRegister`;

    try {
        if (typeof showLoading === "function") showLoading();

        const resp = await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        if (success) {
            successMessage?.(text);
            badge?.remove();

            await loadCounterList();
            if (typeof loadCounterDetail === "function" && counterNo) loadCounterDetail(counterNo);

            await qbNotifyCounterViaNewHub({
                ProjectID: payload?.ProjectID || "",
                QueueTypeID: QB_QUEUE_TYPE_ID,
                Counter: parseInt(counterNo || "0", 10),
                Action: "RemoveUnitRegister"
            });

        } else {
            errorMessage?.(text);
        }
    } catch (err) {
        console.error("❌ Error calling RemoveUnitRegister:", err);
        errorMessage?.("Error while removing unit from counter.");
    } finally {
        if (typeof hideLoading === "function") hideLoading();
    }
}

async function callCheckoutBankCounter(payload, badge, counterNo) {
    const url = `${qbRootPath()}QueueBankCounterView/CheckoutBankCounter`;

    try {
        if (typeof showLoading === "function") showLoading();

        const resp = await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        if (success) {
            successMessage?.(text, "Bank Checked Out");
            badge?.remove();

            await loadCounterList();
            if (typeof loadCounterDetail === "function" && counterNo) loadCounterDetail(counterNo);

            await qbNotifyCounterViaNewHub({
                QueueTypeID: QB_QUEUE_TYPE_ID,
                Counter: parseInt(counterNo || "0", 10),
                Action: "CheckoutBankCounter"
            });

        } else {
            errorMessage?.(text);
        }
    } catch (err) {
        console.error("❌ Error calling CheckoutBankCounter:", err);
        errorMessage?.("Error while checking out bank counter.");
    } finally {
        if (typeof hideLoading === "function") hideLoading();
    }
}

/* =========================
   [ACTION] Save Unit Register
========================= */
async function onSaveUnitRegisterClicked() {
    const projectId = document.getElementById("hidProjectId")?.value || "";
    const ddl = document.getElementById("ddlUnitRegister");
    const unitId = ddl ? ddl.value : "";
    const counterNo = currentCounterNo;

    if (!projectId) { errorMessage?.("Project is invalid."); return; }
    if (!counterNo) { errorMessage?.("Please select a counter first."); return; }
    if (!unitId) { errorMessage?.("Please select a unit."); return; }

    const url = `${qbRootPath()}QueueBankCounterView/UpdateUnitRegister`;

    const payload = {
        ProjectID: projectId,
        UnitID: unitId,
        Counter: parseInt(counterNo, 10)
    };

    try {
        if (typeof showLoading === "function") showLoading();

        const resp = await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        if (success) {
            successMessage?.(text, "Completed");

            await loadCounterList();
            if (typeof loadCounterDetail === "function") loadCounterDetail(counterNo);

            await qbNotifyCounterViaNewHub({
                ProjectID: projectId,
                QueueTypeID: QB_QUEUE_TYPE_ID,
                Counter: parseInt(counterNo, 10),
                Action: "UpdateUnitRegister"
            });

            if (unitRegisterChoices) {
                unitRegisterChoices.removeActiveItems();
                unitRegisterChoices.setChoiceByValue("");
            } else if (ddl) {
                ddl.value = "";
            }
        } else {
            errorMessage?.(text);
        }
    } catch (err) {
        console.error("❌ Error calling UpdateUnitRegister:", err);
        errorMessage?.("Error while updating unit register.", "Request Failed");
    } finally {
        if (typeof hideLoading === "function") hideLoading();
    }
}

/* =========================
   [SignalR OLD] Hook to existing ChatProxy if present
========================= */
function qbBindOldSignalRIfAvailable() {
    if (!window.ChatProxy || typeof window.ChatProxy.on !== "function") return;
    const ChatProxy = window.ChatProxy;

    if (ChatProxy.__qbCounterViewBound === true) return;
    ChatProxy.__qbCounterViewBound = true;

    ChatProxy.on("sendCallStaff", function (data) {
        const status = qbNormStatus(data?.CallStaffStatus);
        const counterNo = qbNormalizeCounterNo(data?.Counter);
        const projectId = (data?.ProjectID ?? "").toString();
        const registerLogId = parseInt(data?.RegisterLogID ?? "0", 10);

        if (!counterNo) return;

        if (status === "start") {
            qbCallStaffMap.set(counterNo, { projectId: projectId, registerLogId: registerLogId });
            qbBlinkCounters([counterNo], { durationMs: 15000 });
            qbPlayDingCooldown(1500);
        }

        if (status === "stop") {
            qbCallStaffMap.delete(counterNo);
            qbBlinkStop(counterNo);
        }

        if (String(currentCounterNo ?? "") === counterNo) {
            qbUpdateStopButtonUI(counterNo);
        }
    });

    ChatProxy.on("notifyCounter", async function (data) {
        console.log("📡 [SignalR] notifyCounter received:", data);

        qbPlayDingCooldown(1500);

        qbClickIfExists("btnSearch");
        qbClickIfExists("btnRefreshChecker");
        qbClickIfExists("btnRefreshCounter");

        try { await loadCounterList(); } catch { }

        try {
            const detailCol = document.getElementById("counterDetailColumn");
            const isRightOpen = detailCol && !detailCol.classList.contains("d-none");

            const serverCounter = data?.Counter?.toString?.() || data?.counter?.toString?.();
            const counterToReload = serverCounter || currentCounterNo;

            if (isRightOpen && counterToReload && typeof loadCounterDetail === "function") {
                console.log("🔄 Reload right panel for counter:", counterToReload);
                await loadCounterDetail(counterToReload);
                qbUpdateStopButtonUI(counterToReload);
            }
        } catch (e) {
            console.error("notifyCounter -> reload counter detail failed:", e);
        }
    });

    console.log("✅ qbBindOldSignalRIfAvailable: bound to ChatProxy events");
}

// =========================================================
// [NEW HUB] Helpers (ASP.NET Core SignalR) - CounterView
// =========================================================
async function startNotifyHub() {
    if (window._notifyHubConnection) return;

    const hubUrl = qbHubUrl();

    if (!window.signalR || !signalR.HubConnectionBuilder) {
        console.warn("signalR client not found. Please ensure signalr.min.js is loaded.");
        return;
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .withAutomaticReconnect()
        .build();

    window._notifyHubConnection = connection;

    connection.on("notifyCounter", async (data) => {
        console.log("📡 [notifyHub] notifyCounter:", data);

        qbPlayDingCooldown(1500);

        qbClickIfExists("btnSearch");
        qbClickIfExists("btnRefreshChecker");
        qbClickIfExists("btnRefreshCounter");

        try { await loadCounterList(); } catch { }

        try {
            const detailCol = document.getElementById("counterDetailColumn");
            const isRightOpen = detailCol && !detailCol.classList.contains("d-none");

            const serverCounter = (data?.Counter ?? data?.counter ?? "").toString().trim();
            const counterToReload = serverCounter || currentCounterNo;

            if (isRightOpen && counterToReload && typeof loadCounterDetail === "function") {
                await loadCounterDetail(counterToReload);
                qbUpdateStopButtonUI(counterToReload);
            }
        } catch (e) {
            console.error("[notifyHub] notifyCounter -> reload detail failed:", e);
        }
    });

    connection.on("stopCallStaff", (data) => {
        console.log("📡 [notifyHub] stopCallStaff:", data);

        const counterNo = (data?.Counter ?? data?.counter ?? "").toString().trim();
        if (!counterNo) return;

        qbCallStaffMap.delete(qbNormalizeCounterNo(counterNo));
        qbUpdateStopButtonUI(counterNo);
        qbBlinkStop(counterNo);

        loadCounterList?.();
        if (typeof loadCounterDetail === "function" && currentCounterNo === counterNo) {
            loadCounterDetail(counterNo);
        }
    });

    connection.on("sendCallStaff", (data) => {
        console.log("📡 [notifyHub] sendCallStaff:", data);

        const status = qbNormStatus(data?.CallStaffStatus);
        const counterNo = qbNormalizeCounterNo(data?.Counter);
        const projectId = (data?.ProjectID ?? "").toString();
        const registerLogId = parseInt(data?.RegisterLogID ?? "0", 10);

        if (!counterNo) return;

        if (status === "start") {
            qbCallStaffMap.set(counterNo, { projectId, registerLogId });
            qbBlinkCounters([counterNo], { durationMs: 15000 });
            qbPlayDingCooldown(1500);
        } else if (status === "stop") {
            qbCallStaffMap.delete(counterNo);
            qbBlinkStop(counterNo);
        }

        if (String(currentCounterNo ?? "") === counterNo) {
            qbUpdateStopButtonUI(counterNo);
        }
    });

    try {
        await connection.start();
        console.log("✅ notifyHub connected:", connection.state, hubUrl);
    } catch (err) {
        console.error("❌ notifyHub start failed:", err);
    }
}

async function qbEnsureNotifyHubConnected() {
    if (!window._notifyHubConnection) {
        await startNotifyHub();
    }
    const conn = window._notifyHubConnection;
    if (!conn) return null;

    if (conn.state !== signalR.HubConnectionState.Connected) {
        try { await conn.start(); } catch { }
    }
    return conn;
}

/**
 * ยิงให้ทุก client ได้รับ event "notifyCounter"
 * NOTE: ต้องมีเมธอดใน C# Hub ชื่อ "NotifyCounter" (optional)
 */
async function qbNotifyCounterViaNewHub(payload) {
    const conn = await qbEnsureNotifyHubConnected();
    if (!conn) return;

    try {
        await conn.invoke("NotifyCounter", payload);
        console.log("✅ notifyHub -> NotifyCounter invoked", payload);
    } catch (e) {
        // Not fatal if hub doesn't have method
        console.warn("⚠️ notifyHub invoke NotifyCounter failed:", e);
    }
}

async function qbStopCallStaffViaNewHub(payload) {
    const conn = await qbEnsureNotifyHubConnected();
    if (!conn) return;

    try {
        await conn.invoke("StopCallStaff", payload);
        console.log("✅ notifyHub -> StopCallStaff invoked", payload);
    } catch (e) {
        console.warn("⚠️ notifyHub invoke StopCallStaff failed:", e);
    }
}


/* =========================
   [INIT] DOMContentLoaded (single)
========================= */
document.addEventListener("DOMContentLoaded", function () {
    initCollapseChevronToggle();
    initFullscreen();

    loadCounterList();

    const btnRefresh = document.getElementById("btnRefreshCounter");
    if (btnRefresh && btnRefresh.dataset.bound !== "1") {
        btnRefresh.addEventListener("click", function (e) {
            e.preventDefault();
            loadCounterList();
        });
        btnRefresh.dataset.bound = "1";
    }

    const ddl = document.getElementById("ddlUnitRegister");
    if (ddl && window.Choices && ddl.dataset.boundChoices !== "1") {
        unitRegisterChoices = new Choices(ddl, {
            searchEnabled: true,
            itemSelectText: "",
            shouldSort: false,
            removeItemButton: false
        });
        ddl.dataset.boundChoices = "1";
    }

    const btnSave = document.getElementById("btnSaveUnitRegister");
    if (btnSave && btnSave.dataset.bound !== "1") {
        btnSave.addEventListener("click", onSaveUnitRegisterClicked);
        btnSave.dataset.bound = "1";
    }

    qbEnsureStopButtonUI();

    // ✅ NEW HUB
    startNotifyHub();

    // ✅ OLD HUB
    qbBindOldSignalRIfAvailable();
});
