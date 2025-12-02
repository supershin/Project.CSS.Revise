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

    const icon = btnFull.querySelector("i");

    function enterFullScreen() {
        if (container.requestFullscreen) {
            container.requestFullscreen();
        }
        container.classList.add("fullscreen-mode");
        icon.classList.remove("fa-expand");
        icon.classList.add("fa-compress");
        btnFull.innerHTML = '<i class="fa fa-compress"></i>';
    }

    function exitFullScreen() {
        if (document.fullscreenElement) {
            document.exitFullscreen();
        }
        container.classList.remove("fullscreen-mode");
        icon.classList.remove("fa-compress");
        icon.classList.add("fa-expand");
        btnFull.innerHTML = '<i class="fa fa-expand"></i>';
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
        if (!document.fullscreenElement) {
            exitFullScreen();
        }
    });
});


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

    items.forEach(item => {
        // รองรับ property แบบ C# PascalCase และ JS camelCase
        const counterNo = item.Counter || item.counter || "";
        const bankCode = item.BankCode || item.bankCode || "";
        const bankName = item.BankName || item.bankName || "";
        const unitCode = item.UnitCode || item.unitCode || "";
        const registerLogID = item.RegisterLogID || item.registerLogID || "";

        const isActive = registerLogID && registerLogID !== "";

        const boxClass = "counter-box qb-counter " + (isActive ? "active" : "empty");

        const bankLogoHtml = bankCode
            ? `<img src="${rootPath}image/ThaiBankicon/${bankCode}.png" alt="${bankCode}" width="26" class="me-2">`
            : "";

        const bodyContent = isActive
            ? `${bankLogoHtml}${unitCode || "-"}`
            : "";

        html += `
            <div class="col-6 col-md-3">
                <div class="${boxClass}"
                     data-counter="${counterNo}"
                     data-bank="${bankCode}"
                     data-bankname="${bankName}"
                     data-unit="${unitCode}"
                     data-registerid="${registerLogID}">
                    <div class="counter-header ${isActive ? "bg-danger text-white" : "bg-primary text-white"}">
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
    });

    // init behaviour หลังจาก render เสร็จ
    initCounterModeButtons();
    initCounterCardClick();
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

            if (box.dataset.originalBoxClass) {
                box.className = box.dataset.originalBoxClass;
            }
            if (header.dataset.originalClass) {
                header.className = header.dataset.originalClass;
            }
            if (body.dataset.originalClass) {
                body.className = body.dataset.originalClass;
            }
            if (box.dataset.originalHeaderHtml != null) {
                header.innerHTML = box.dataset.originalHeaderHtml;
            }
            if (box.dataset.originalBodyHtml != null) {
                body.innerHTML = box.dataset.originalBodyHtml;
            }
        });

        setButtonsMode("bank");
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

            const headerText = (header.textContent || box.dataset.originalHeaderHtml || "").trim();

            const counterNo = box.dataset.counter || "";
            if (!counterNo) return;

            const qrUrl =
                `${rootPath}QueueBankCounterView/CounterQr` +
                `?projectId=${encodedProjectId}` +
                `&projectName=${encodedProjectName}` +
                `&queueType=bank` +
                `&counterNo=${encodeURIComponent(counterNo)}`;

            box.classList.remove("active");
            if (!box.classList.contains("empty")) {
                box.classList.add("empty");
            }

            header.className = "counter-header bg-primary text-white";
            header.textContent = headerText;

            body.className = "counter-body";
            body.innerHTML = `
                <div class="d-flex justify-content-center align-items-center" style="min-height:60px;">
                    <img src="${qrUrl}"
                         alt="QR Code for Counter ${counterNo}"
                         style="width:64px; height:auto;">
                </div>
            `;
        });

        setButtonsMode("qr");
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
}


// ======================
// Counter Detail Panel (Right Side) + resize left col
// ======================
function initCounterCardClick() {
    const grid = document.getElementById("counterGrid");
    const detailCol = document.getElementById("counterDetailColumn");
    const titleEl = document.getElementById("counterDetailTitle");
    const unitInput = document.getElementById("txtUnitCode");
    const closeBtn = document.getElementById("btnCloseCounterDetail");
    const leftCol = document.getElementById("counterGridColumn");

    if (!grid || !detailCol) return;

    // เริ่มต้น: ถ้า detail ปิด → ซ้ายเต็ม col-12
    if (leftCol && detailCol.classList.contains("d-none")) {
        leftCol.classList.remove("col-lg-8");
        leftCol.classList.add("col-lg-12");
    }

    // คลิกการ์ดฝั่งซ้าย → เปิด panel ขวา + ย่อซ้าย
    grid.addEventListener("click", function (e) {
        const box = e.target.closest(".qb-counter");
        if (!box || !grid.contains(box)) return;

        const counterNo = box.dataset.counter || "";
        const unitCode = box.dataset.unit || "";

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

        if (unitInput) {
            unitInput.value = unitCode || "";
        }

        grid.querySelectorAll(".qb-counter.selected").forEach(el => {
            el.classList.remove("selected");
        });
        box.classList.add("selected");
    });

    // ปิด panel ขวา → ซ้ายกลับมาเต็ม
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
        });
        closeBtn.dataset.bound = "1";
    }
}


// ======================
// Boot all
// ======================
document.addEventListener("DOMContentLoaded", function () {
    // โหลด counter list ครั้งแรก
    loadCounterList();

    // ปุ่ม Refresh → reload counters
    const btnRefresh = document.getElementById("btnRefreshCounter");
    if (btnRefresh && !btnRefresh.dataset.bound) {
        btnRefresh.addEventListener("click", function (e) {
            e.preventDefault();
            loadCounterList();
        });
        btnRefresh.dataset.bound = "1";
    }
});
