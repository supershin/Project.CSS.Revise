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
// Counter View: toggle Bank / QR mode
// ======================
function initCounterModeButtons() {
    const grid = document.getElementById("counterGrid");
    const btnBank = document.getElementById("btnBankCounter");
    const btnQR = document.getElementById("btnQRCounter");

    if (!grid || !btnBank || !btnQR) return;

    const boxes = Array.from(grid.querySelectorAll(".counter-box"));

    // เก็บ state เดิม
    boxes.forEach(box => {
        const header = box.querySelector(".counter-header");
        const body = box.querySelector(".counter-body");
        if (!header || !body) return;

        box.dataset.originalBoxClass = box.className;
        box.dataset.originalHeaderClass = header.className;
        box.dataset.originalBodyClass = body.className;
        box.dataset.originalHeaderHtml = header.innerHTML;
        box.dataset.originalBodyHtml = body.innerHTML;
    });

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

    // 🔵 โหมด Bank → คืน layout เดิม
    function setBankMode() {
        boxes.forEach(box => {
            const header = box.querySelector(".counter-header");
            const body = box.querySelector(".counter-body");
            if (!header || !body) return;

            if (box.dataset.originalBoxClass) box.className = box.dataset.originalBoxClass;
            if (box.dataset.originalHeaderClass) header.className = box.dataset.originalHeaderClass;
            if (box.dataset.originalBodyClass) body.className = box.dataset.originalBodyClass;
            if (box.dataset.originalHeaderHtml != null) header.innerHTML = box.dataset.originalHeaderHtml;
            if (box.dataset.originalBodyHtml != null) body.innerHTML = box.dataset.originalBodyHtml;
        });

        setButtonsMode("bank");
    }

    // 🟡 โหมด QR → ใช้ธีม counter ว่าง + รูป QR
    function setQRMode() {
        const rootPath = (typeof baseUrl !== "undefined" ? baseUrl : "/");

        boxes.forEach(box => {
            const header = box.querySelector(".counter-header");
            const body = box.querySelector(".counter-body");
            if (!header || !body) return;

            const headerText = (header.textContent || box.dataset.originalHeaderHtml || "").trim();

            box.classList.remove("active");
            if (!box.classList.contains("empty")) {
                box.classList.add("empty");
            }

            header.className = "counter-header bg-primary text-white";
            header.textContent = headerText;

            body.className = "counter-body";
            body.innerHTML = `
                <div class="d-flex justify-content-center align-items-center" style="min-height:60px;">
                    <img src="${rootPath}image/ThaiBankicon/QRCODE.png"
                         alt="QR Code"
                         style="width:64px; height:auto;">
                </div>
            `;
        });

        setButtonsMode("qr");
    }

    btnBank.addEventListener("click", function (e) {
        e.preventDefault();
        setBankMode();
    });

    btnQR.addEventListener("click", function (e) {
        e.preventDefault();
        setQRMode();
    });

    // เริ่มด้วยโหมด Bank
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

    // ✅ ตั้งค่าเริ่มต้น: ถ้า detail ปิดอยู่ → ซ้ายเต็มหน้าจอ col-lg-12
    if (leftCol && detailCol.classList.contains("d-none")) {
        leftCol.classList.remove("col-lg-8");
        leftCol.classList.add("col-lg-12");
    }

    // คลิกการ์ดฝั่งซ้าย → เปิด/อัปเดต panel ขวา + ย่อซ้ายเป็น 8
    grid.addEventListener("click", function (e) {
        const box = e.target.closest(".qb-counter");
        if (!box || !grid.contains(box)) return;

        const counterNo = box.dataset.counter || "";
        const unitCode = box.dataset.unit || "";

        // 1) แสดง column ขวา
        detailCol.classList.remove("d-none");

        // 2) ซีกซ้ายกลับมา col-lg-8
        if (leftCol) {
            leftCol.classList.remove("col-lg-12");
            leftCol.classList.add("col-lg-8");
        }

        // 3) อัปเดต title
        if (titleEl) {
            titleEl.textContent = counterNo
                ? `Counter : ${counterNo}`
                : "Counter";
        }

        // 4) เติม Unit Code
        if (unitInput) {
            unitInput.value = unitCode || "";
        }

        // 5) Highlight การ์ดที่เลือก
        grid.querySelectorAll(".qb-counter.selected").forEach(el => {
            el.classList.remove("selected");
        });
        box.classList.add("selected");
    });

    // ปุ่มปิด → ซ่อน panel ขวา + ขยายซ้ายเป็น col-lg-12
    if (closeBtn) {
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
    }
}


// ======================
// Boot all
// ======================
document.addEventListener("DOMContentLoaded", function () {
    initCounterModeButtons();   // ปุ่ม Bank / QR
    initCounterCardClick();     // คลิกการ์ด + resize col ซ้าย/ขวา
});
