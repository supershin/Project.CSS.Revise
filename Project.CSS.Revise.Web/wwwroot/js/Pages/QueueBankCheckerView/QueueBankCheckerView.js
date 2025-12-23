/* =========================================================
   QueueBankCheckerView.js
   - Focus: Summary Register Career (Dynamic by ext.ID)
   - Also keeps: collapse toggle, summary toggle, fullscreen,
     counter grid, counter detail, remove badge, update unit
========================================================= */

/* =========================
   [A] Global State / Helpers
========================= */
let currentCounterNo = null;
let unitRegisterChoices = null;

function qbNorm(s) {
    return (s ?? "").toString().trim().toLowerCase();
}

function qbSafeNum(v, fallback = 0) {
    const n = Number(v);
    return Number.isFinite(n) ? n : fallback;
}

// format value -> "xx.xx M"
function qbFormatValueM(raw) {
    if (raw == null || raw === "") return "0.00 M";
    const num = Number(raw);
    if (Number.isNaN(num)) return "0.00 M";

    const m = num / 1_000_000;
    return (
        m.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + " M"
    );
}

function qbSetText(id, text) {
    const el = document.getElementById(id);
    if (el) el.textContent = text;
}

/* =========================
   [B] Collapse Chevron Toggle
========================= */
function initCollapseChevronToggle() {
    const toggles = document.querySelectorAll('[data-bs-toggle="collapse"][data-bs-target]');
    toggles.forEach(btn => {
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
   [C] Toggle Summary View (Box <-> Table)
========================= */
function initSummaryToggleView() {
    const btnToggle = document.getElementById("btnSummaryRegisterToggle");
    const boxView = document.getElementById("summary-register-box-view");
    const tableView = document.getElementById("summary-register-table-view");
    if (!btnToggle || !boxView || !tableView) return;

    btnToggle.addEventListener("click", function () {
        const icon = btnToggle.querySelector("i");
        const isBoxVisible = !boxView.classList.contains("d-none");

        if (isBoxVisible) {
            boxView.classList.add("d-none");
            tableView.classList.remove("d-none");

            if (icon) {
                icon.classList.remove("fa-table");
                icon.classList.add("fa-th-large");
            }
            btnToggle.setAttribute("title", "Change to card view");
            btnToggle.setAttribute("aria-label", "Change to card view");
        } else {
            tableView.classList.add("d-none");
            boxView.classList.remove("d-none");

            if (icon) {
                icon.classListremove?.("fa-th-large");
                icon.classList.add("fa-table");
            } else if (icon) {
                icon.classList.remove("fa-th-large");
                icon.classList.add("fa-table");
            }

            btnToggle.setAttribute("title", "Change to table view");
            btnToggle.setAttribute("aria-label", "Change to table view");
        }
    });
}

/* =========================
   [D] Summary Register (Header / Filters for CheckerView)
========================= */
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

function qbUpdateSummaryRegisterHeaderDate() {
    const spanEl = document.getElementById("sum-register-date");
    if (!spanEl) return;
    spanEl.textContent = "All Days";
}

/* =========================
   [E] Summary Register (Core Update Functions)
========================= */

// ใช้กับ Register/Queue/Inprocess/Done และ Loan yes/no
function qbUpdateSummaryBox(prefix, data) {
    const unit = (data?.Unit ?? "0").toString();
    const value = (data?.Value ?? "0").toString();
    const percent = (data?.Percent ?? "0").toString();

    // box view
    qbSetText(`sum-${prefix}-unit`, unit);
    qbSetText(`sum-${prefix}-value`, qbFormatValueM(value));
    qbSetText(`sum-${prefix}-percent`, `${percent}%`);

    // table view (ถ้ามี)
    qbSetText(`tbl-${prefix}-unit`, unit);
    qbSetText(`tbl-${prefix}-value`, qbFormatValueM(value));
    qbSetText(`tbl-${prefix}-percent`, `${percent}%`);
}

// map list by Topic (lowercase)
function qbMapByTopic(list) {
    const map = {};
    (list || []).forEach(x => {
        const key = qbNorm(x.Topic || x.Name || x.CareerName || "");
        if (key) map[key] = x;
    });
    return map;
}

/* =========================
   [F] ✅ Career Summary (FIXED)
   - UI ids: sum-career-{extId}-unit/value/percent
   - Table ids: tbl-{extId}-unit/value/percent
   - Uses window.CAREER_UI_MAP from Razor
========================= */
function qbBuildCareerNameToIdMap() {
    const arr = Array.isArray(window.CAREER_UI_MAP) ? window.CAREER_UI_MAP : [];
    const nameToId = {};
    arr.forEach(x => {
        const id = (x?.id ?? "").toString();
        const name = qbNorm(x?.name);
        if (id && name) nameToId[name] = id;
    });
    return { arr, nameToId };
}

function qbClearAllCareerUI() {
    const arr = Array.isArray(window.CAREER_UI_MAP) ? window.CAREER_UI_MAP : [];
    arr.forEach(x => {
        const id = (x?.id ?? "").toString();
        if (!id) return;

        qbSetText(`sum-career-${id}-unit`, "0");
        qbSetText(`sum-career-${id}-value`, "0.00 M");
        qbSetText(`sum-career-${id}-percent`, "0%");

        qbSetText(`tbl-${id}-unit`, "0");
        qbSetText(`tbl-${id}-value`, "0.00 M");
        qbSetText(`tbl-${id}-percent`, "0%");
    });
}

function qbUpdateCareerUIById(careerId, data) {
    const id = (careerId ?? "").toString();
    if (!id) return;

    const unit = (data?.Unit ?? "0").toString();
    const value = (data?.Value ?? "0").toString();
    const percent = (data?.Percent ?? "0").toString();

    qbSetText(`sum-career-${id}-unit`, unit);
    qbSetText(`sum-career-${id}-value`, qbFormatValueM(value));
    qbSetText(`sum-career-${id}-percent`, `${percent}%`);

    qbSetText(`tbl-${id}-unit`, unit);
    qbSetText(`tbl-${id}-value`, qbFormatValueM(value));
    qbSetText(`tbl-${id}-percent`, `${percent}%`);
}

function qbUpdateCareerSummaryDynamic(apiCareerList) {
    // 1) clear first (so old data won't remain)
    qbClearAllCareerUI();

    // 2) build name->id map from UI
    const { nameToId } = qbBuildCareerNameToIdMap();

    // 3) apply data from API
    (apiCareerList || []).forEach(item => {
        // backend usually sends Topic = career name
        const topicName = qbNorm(item.Topic || item.Name || "");
        if (!topicName) return;

        const uiId = nameToId[topicName];
        if (!uiId) return; // not found in UI

        qbUpdateCareerUIById(uiId, item);
    });
}

/* =========================
   [G] Load Summary Register All (Type + Loan + Career)
========================= */
function loadSummaryRegisterAll() {
    const filters = qbGetValuesCounterView();

    let projectId = filters.Project;
    if (Array.isArray(projectId)) projectId = projectId[0] || "";

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

    // datatables placeholder
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
            // 1) Register Type
            const typeMap = qbMapByTopic(res.listDataSummeryRegisterType || []);
            qbUpdateSummaryBox("register", typeMap["register"]);
            qbUpdateSummaryBox("queue", typeMap["queue"]);
            qbUpdateSummaryBox("inprocess", typeMap["in process"]);
            qbUpdateSummaryBox("done", typeMap["done"]);

            // 2) Loan Type
            const loanMap = qbMapByTopic(res.listDataSummeryRegisterLoanTyp || []);
            qbUpdateSummaryBox("loan-yes", loanMap["ยื่น"]);
            qbUpdateSummaryBox("loan-no", loanMap["ไม่ยื่น"]);

            // 3) ✅ Career Type (DYNAMIC by UI map)
            qbUpdateCareerSummaryDynamic(res.listDataSummeryRegisterCareerTyp || []);
        })
        .catch(err => {
            console.error("GetlistSummeryRegister error:", err);

            qbUpdateSummaryBox("register", null);
            qbUpdateSummaryBox("queue", null);
            qbUpdateSummaryBox("inprocess", null);
            qbUpdateSummaryBox("done", null);

            qbUpdateSummaryBox("loan-yes", null);
            qbUpdateSummaryBox("loan-no", null);

            qbClearAllCareerUI();
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}

/* =========================
   [H] Summary Bank (table)
========================= */
function loadSummaryRegisterBank() {
    const filters = qbGetValuesCounterView();

    let projectId = filters.Project;
    if (Array.isArray(projectId)) projectId = projectId[0] || "";

    const formData = new FormData();
    formData.append("L_Act", "SummeryRegisterBank");
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
    formData.append("length", "1000");
    formData.append("SearchTerm", "");

    const tbodyBank = document.getElementById("summary-bank-body");
    const tbodyNon = document.getElementById("summary-banknonsubmissionreason-body");

    if (tbodyBank) tbodyBank.innerHTML = `<tr><td colspan="5" class="text-center text-muted">Loading...</td></tr>`;
    if (tbodyNon) tbodyNon.innerHTML = `<tr><td colspan="3" class="text-center text-muted">Loading...</td></tr>`;

    if (typeof showLoading === "function") showLoading();

    fetch(baseUrl + "QueueBank/GetlistSummeryRegisterBank", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(res => {
            // 1) Summary Bank
            if (tbodyBank) {
                const listBank = res.listDataSummeryRegisterBank || [];
                if (!listBank.length) {
                    tbodyBank.innerHTML = `<tr><td colspan="5" class="text-center text-muted">No data</td></tr>`;
                } else {
                    tbodyBank.innerHTML = listBank.map(item => {
                        const bankCode = (item.BankCode || "").trim();
                        const bankName = item.BankName || "";
                        const unit = item.Unit ?? 0;
                        const valueText = qbFormatValueM(item.Value);
                        const percentText = (item.Percent ?? "0") + "%";
                        const interestRate = (item.InterestRateAVG ?? "0") + "%";

                        let bankCellHtml = "";
                        if (bankCode && qbNorm(bankCode) !== "no data") {
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

            // 2) Non-Submission Reason
            if (tbodyNon) {
                const listNon = res.listDataSummeryRegisterBankNonSubmissionReason || [];
                if (!listNon.length) {
                    tbodyNon.innerHTML = `<tr><td colspan="3" class="text-center text-muted">No data</td></tr>`;
                } else {
                    tbodyNon.innerHTML = listNon.map(item => {
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
            if (tbodyBank) tbodyBank.innerHTML = `<tr><td colspan="5" class="text-center text-danger">Error loading Summary Bank</td></tr>`;
            if (tbodyNon) tbodyNon.innerHTML = `<tr><td colspan="3" class="text-center text-danger">Error loading Non-Submission Reason</td></tr>`;
        })
        .finally(() => {
            if (typeof hideLoading === "function") hideLoading();
        });
}

/* =========================
   [I] Fullscreen
========================= */
function initFullscreen() {
    const btnFull = document.getElementById("btnFullScreen");
    const container = document.getElementById("Container_counter");
    if (!btnFull || !container) return;

    function enterFullScreen() {
        if (container.requestFullscreen) container.requestFullscreen();
        container.classList.add("fullscreen-mode");
        btnFull.innerHTML = '<i class="fa fa-compress"></i>';
        if (typeof updateCounterGridLayout === "function") updateCounterGridLayout();
    }

    function exitFullScreen() {
        if (document.fullscreenElement) document.exitFullscreen();
        container.classList.remove("fullscreen-mode");
        btnFull.innerHTML = '<i class="fa fa-expand"></i>';
        if (typeof updateCounterGridLayout === "function") updateCounterGridLayout();
    }

    btnFull.addEventListener("click", function () {
        const isFull = container.classList.contains("fullscreen-mode");
        if (!isFull) enterFullScreen();
        else exitFullScreen();
    });

    document.addEventListener("fullscreenchange", function () {
        if (!document.fullscreenElement && container.classList.contains("fullscreen-mode")) {
            container.classList.remove("fullscreen-mode");
            btnFull.innerHTML = '<i class="fa fa-expand"></i>';
            if (typeof updateCounterGridLayout === "function") updateCounterGridLayout();
        }
    });
}

/* =========================
   [J] Counter Grid Layout
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
   [K] Load Counter List + Render Grid
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
        const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");
        const url = `${rootPath}QueueBankCheckerView/GetCounterList?projectId=${encodeURIComponent(projectId)}`;
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

function renderCounterGrid(items) {
    const grid = document.getElementById("counterGrid");
    const loadingEl = document.getElementById("counterGridLoading");
    if (!grid) return;
    if (loadingEl) loadingEl.remove();

    if (!items || !items.length) {
        grid.innerHTML = `<div class="col-12 text-center text-muted">No counters configured for this project.</div>`;
        return;
    }

    const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");

    const hasValue = (v) => {
        if (v === null || v === undefined) return false;
        const s = String(v).trim();
        if (s === "" || s.toLowerCase() === "null" || s.toLowerCase() === "undefined") return false;
        return true;
    };

    let html = "";
    items.forEach(item => {
        const counterNo = item.Counter || item.counter || "";
        const bankCode = item.BankCode || item.bankCode || "";
        const bankName = item.BankName || item.bankName || "";
        const unitCode = item.UnitCode || item.unitCode || "";
        const registerLogID = item.RegisterLogID ?? item.registerLogID ?? "";
        const inProcessDate = item.InProcessDate ?? item.inprocessDate ?? item.inProcessDate ?? "";
        const hasInProcess = hasValue(inProcessDate);
        const isActive = hasValue(registerLogID);

        const boxClass =
            "counter-box qb-counter " +
            (isActive ? "active" : "empty") +
            (hasInProcess ? " inprocess" : "");

        const bankLogoHtml = bankCode
            ? `<img src="${rootPath}image/ThaiBankicon/${bankCode}.png" alt="${bankCode}" width="26" class="me-2">`
            : "";

        const bodyContent = isActive ? `${bankLogoHtml}${unitCode || "-"}` : "";

        let headerStyle = "";
        if (hasInProcess) headerStyle = "background-color:#198754;color:#ffffff;";
        else if (isActive) headerStyle = "background-color:#dc3545;color:#ffffff;";
        else headerStyle = "background-color:#6c757d;color:#ffffff;";

        html += `
            <div class="counter-col col-6">
                <div class="${boxClass}"
                     data-counter="${counterNo}"
                     data-bank="${bankCode}"
                     data-bankname="${bankName}"
                     data-unit="${unitCode}"
                     data-registerid="${registerLogID}">
                    <div class="counter-header" style="${headerStyle}">Counter : ${counterNo}</div>
                    <div class="counter-body">${bodyContent}</div>
                </div>
            </div>
        `;
    });

    grid.innerHTML = html;

    // save original state for mode switch
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
}

/* =========================
   [L] Counter Mode Buttons (Bank / QR)
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
    }

    function setQRMode() {
        const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");
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
                `${rootPath}QueueBankCheckerView/CounterQr` +
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
    }

    if (!btnBank.dataset.bound) {
        btnBank.addEventListener("click", e => { e.preventDefault(); setBankMode(); });
        btnBank.dataset.bound = "1";
    }
    if (!btnQR.dataset.bound) {
        btnQR.addEventListener("click", e => { e.preventDefault(); setQRMode(); });
        btnQR.dataset.bound = "1";
    }

    setBankMode();
}

/* =========================
   [M] Counter Detail Click / Load / Remove badge / Update unit
   (kept from your original; no change to business logic)
========================= */
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
        currentCounterNo = counterNo;

        detailCol.classList.remove("d-none");
        if (leftCol) {
            leftCol.classList.remove("col-lg-12");
            leftCol.classList.add("col-lg-8");
        }

        if (titleEl) titleEl.textContent = counterNo ? `Counter : ${counterNo}` : "Counter";

        grid.querySelectorAll(".qb-counter.selected").forEach(el => el.classList.remove("selected"));
        box.classList.add("selected");

        updateCounterGridLayout();

        if (typeof loadCounterDetail === "function" && counterNo) loadCounterDetail(counterNo);

        const ddl = document.getElementById("ddlUnitRegister");
        if (window.unitRegisterChoices) {
            unitRegisterChoices.removeActiveItems();
            unitRegisterChoices.setChoiceByValue("");
        } else if (ddl) ddl.value = "";
    });

    if (closeBtn && !closeBtn.dataset.bound) {
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

/* --- your existing loadCounterDetail / remove badge / update unit functions
   Keep using exactly the same as your original file ---
   (If you want me to merge them in too, paste the tail-end of your file again)
*/

/* =========================
   [N] Init Page
========================= */
document.addEventListener("DOMContentLoaded", function () {
    initCollapseChevronToggle();
    initSummaryToggleView();
    initFullscreen();

    // first load
    loadCounterList();
    loadSummaryRegisterAll();
    loadSummaryRegisterBank();

    // refresh button
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

    // init Choices for ddlUnitRegister
    const ddl = document.getElementById("ddlUnitRegister");
    if (ddl && window.Choices) {
        unitRegisterChoices = new Choices(ddl, {
            searchEnabled: true,
            itemSelectText: "",
            shouldSort: false,
            removeItemButton: false
        });
        window.unitRegisterChoices = unitRegisterChoices;
    }

    // bind OK button
    const btnSave = document.getElementById("btnSaveUnitRegister");
    if (btnSave && !btnSave.dataset.bound) {
        btnSave.addEventListener("click", onSaveUnitRegisterClicked);
        btnSave.dataset.bound = "1";
    }
});
