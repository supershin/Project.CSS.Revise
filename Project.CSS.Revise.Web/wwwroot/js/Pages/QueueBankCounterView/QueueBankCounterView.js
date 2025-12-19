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

    document.addEventListener("fullscreenchange", function () {
        if (!document.fullscreenElement && container.classList.contains("fullscreen-mode")) {
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

    const isDetailHidden = !detailCol || detailCol.classList.contains("d-none");

    cols.forEach(col => {
        col.classList.remove("col-md-2", "col-lg-2", "col-md-3", "col-lg-3");

        if (!col.classList.contains("col-6")) {
            col.classList.add("col-6");
        }

        if (isDetailHidden) {
            col.classList.add("col-md-2", "col-lg-2");
        } else {
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
            headers: { "Accept": "application/json" }
        });

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

    if (loadingEl) loadingEl.remove();

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

        const inProcessDate = item.InProcessDate ?? item.inprocessDate ?? "";
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

                <div class="counter-header" style="${headerStyle}">
                    Counter : ${counterNo}
                </div>

                <div class="counter-body">
                    ${bodyContent}
                </div>
            </div>
        </div>`;
    });

    grid.innerHTML = html;

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

            const originalClass = box.dataset.originalBoxClass || box.className;

            const hasInProcess = originalClass.includes("inprocess");
            const isActive = originalClass.includes("active");
            const isEmpty = originalClass.includes("empty");

            box.className = originalClass;
            box.classList.add("qr-mode");

            const qrUrl =
                `${rootPath}QueueBankCounterView/CounterQr` +
                `?projectId=${encodedProjectId}` +
                `&projectName=${encodedProjectName}` +
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
                    <img src="${qrUrl}"
                         class="counter-qr"
                         alt="QR Code for Counter ${counterNo}"
                         style="width:64px; height:auto;">
                </div>
            </div>`;
        });

        setButtonsMode("qr");
        updateCounterGridLayout();
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

    setBankMode();
    updateCounterGridLayout();
}


// ======================
// Click counter -> open right panel
// ======================
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

        if (titleEl) {
            titleEl.textContent = counterNo ? `Counter : ${counterNo}` : "Counter";
        }

        grid.querySelectorAll(".qb-counter.selected").forEach(el => el.classList.remove("selected"));
        box.classList.add("selected");

        updateCounterGridLayout();

        if (typeof loadCounterDetail === "function" && counterNo) {
            loadCounterDetail(counterNo);
        }

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

            grid.querySelectorAll(".qb-counter.selected").forEach(el => el.classList.remove("selected"));

            currentCounterNo = null;
            updateCounterGridLayout();
        });
        closeBtn.dataset.bound = "1";
    }
}


// ======================
// Load Counter Detail (Right Panel)
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

        if (!resp.ok) throw new Error("HTTP " + resp.status);

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
            const logoHtml = `<img src="${rootPath}image/ThaiBankicon/${bankCode}.png" width="20" class="me-1">`;
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
            `${rootPath}QueueBankCounterView/CounterQr` +
            `?projectId=${encodeURIComponent(projectId)}` +
            `&projectName=${encodeURIComponent(projectName)}` +
            `&queueType=bank` +
            `&counterNo=${encodeURIComponent(counterNo)}`;

        qrBox.innerHTML = `<img src="${qrUrl}" alt="QR" width="180">`;

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

    const type = badge.dataset.type || "unit";
    const projectId = badge.dataset.projectid || "";
    const registerLogId = parseInt(badge.dataset.id || "0", 10);
    const unitId = badge.dataset.unitid || "";
    const counterNo = badge.dataset.counter || "";
    const bankId = parseInt(badge.dataset.bankid || "0", 10);

    if (type === "bank") {
        if (!registerLogId || !bankId) {
            errorMessage("Bank or register is invalid.");
            return;
        }

        await callCheckoutBankCounter({
            RegisterLogID: registerLogId,
            BankID: bankId,
            ContactDetail: ""
        }, badge, counterNo);

        return;
    }

    if (!projectId || !unitId) {
        errorMessage("Project or Unit is invalid.");
        return;
    }

    await callRemoveUnitRegister(
        { ProjectID: projectId, UnitID: unitId, Counter: parseInt(counterNo || "0", 10) },
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
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        if (success) {
            successMessage(text);

            if (badge) badge.remove();

            if (typeof loadCounterList === "function") loadCounterList();
            if (typeof loadCounterDetail === "function" && counterNo) loadCounterDetail(counterNo);
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
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        if (success) {
            successMessage(text, "Bank Checked Out");
            if (badge) badge.remove();

            if (typeof loadCounterList === "function") loadCounterList();
            if (typeof loadCounterDetail === "function" && counterNo) loadCounterDetail(counterNo);
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

    if (!projectId) { errorMessage("Project is invalid."); return; }
    if (!counterNo) { errorMessage("Please select a counter first."); return; }
    if (!unitId) { errorMessage("Please select a unit."); return; }

    const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");
    const url = `${rootPath}QueueBankCounterView/UpdateUnitRegister`;

    const payload = {
        ProjectID: projectId,
        UnitID: unitId,
        Counter: parseInt(counterNo, 10)
    };

    try {
        const resp = await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!resp.ok) throw new Error("HTTP " + resp.status);

        const json = await resp.json();
        const success = json.Issucces ?? json.issucces ?? false;
        const text = json.TextResult ?? json.textResult ?? "No message from server.";

        if (success) successMessage(text, "Completed");
        else errorMessage(text);

        if (success) {
            if (typeof loadCounterList === "function") loadCounterList();
            if (typeof loadCounterDetail === "function") loadCounterDetail(counterNo);

            if (window.unitRegisterChoices) {
                unitRegisterChoices.removeActiveItems();
                unitRegisterChoices.setChoiceByValue('');
            } else if (ddl) {
                ddl.value = "";
            }
        }

    } catch (err) {
        console.error("❌ Error calling UpdateUnitRegister:", err);
        errorMessage("Error while updating unit register.", "Request Failed");
    }
}


// ======================
// Init page
// ======================
document.addEventListener("DOMContentLoaded", function () {
    loadCounterList();

    const btnRefresh = document.getElementById("btnRefreshCounter");
    if (btnRefresh && !btnRefresh.dataset.bound) {
        btnRefresh.addEventListener("click", function (e) {
            e.preventDefault();
            loadCounterList();
        });
        btnRefresh.dataset.bound = "1";
    }

    const ddl = document.getElementById("ddlUnitRegister");
    if (ddl && window.Choices) {
        unitRegisterChoices = new Choices(ddl, {
            searchEnabled: true,
            itemSelectText: "",
            shouldSort: false,
            removeItemButton: false
        });
    }

    const btnSave = document.getElementById("btnSaveUnitRegister");
    if (btnSave && !btnSave.dataset.bound) {
        btnSave.addEventListener("click", onSaveUnitRegisterClicked);
        btnSave.dataset.bound = "1";
    }
});
