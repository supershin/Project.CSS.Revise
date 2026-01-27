/* =========================================================
   ~/js/Pages/QueueInspect/SearchListQueueInspect.js
   FIX: Header/Body misaligned -> Disable DataTables scrollX
========================================================= */

let QueueInspectRegisterTableDt = null;

function QI_GetChoiceValues(sel, isMulti) {
    const inst = window.QI_CHOICES ? window.QI_CHOICES[sel] : null;

    if (!inst) {
        const v = $(sel).val();
        if (isMulti) return Array.isArray(v) ? v : (v ? [v] : []);
        return Array.isArray(v) ? (v[0] || "") : (v || "");
    }

    const v = inst.getValue(true);

    if (isMulti) {
        if (!v) return [];
        return Array.isArray(v) ? v : [v];
    }

    if (Array.isArray(v)) return v[0] || "";
    return v || "";
}

function QI_GetFilters(done) {
    const f = {
        Bu: QI_GetChoiceValues("#ddl_BUG", true),
        ProjectID: QI_GetChoiceValues("#ddl_Project", false),
        UnitID: QI_GetChoiceValues("#ddl_UnitCode", true),
        Inspect_Round: QI_GetChoiceValues("#ddl_Inspect_Round", true),
        CSResponse: QI_GetChoiceValues("#ddl_CS_Response", true),
        UnitCS: QI_GetChoiceValues("#ddl_Unit_Status_CS", true),
        ExpectTransfer: QI_GetChoiceValues("#ddl_Expect_Transfer_By", true),
        RegisterDateStart: ($("#txt_RegisterDateStart").val() || "").trim(),
        RegisterDateEnd: ($("#txt_RegisterDateEnd").val() || "").trim()
    };

    if (typeof done === "function") done(f);
    return f;
}

function QI_DestroyRegisterTable(done) {
    if (QueueInspectRegisterTableDt) {
        try { QueueInspectRegisterTableDt.destroy(true); } catch (e) { }
        QueueInspectRegisterTableDt = null;
    }
    if (typeof done === "function") done();
}

function initQueueInspectRegisterTable(done) {

    QI_DestroyRegisterTable(function () {

        QueueInspectRegisterTableDt = $("#QueueBankRegisterTable").DataTable({
            processing: true,
            serverSide: true,
            searching: true,
            lengthChange: true,
            pageLength: 10,
            ordering: false,

            // ✅ สำคัญ: ปิด autoWidth + ปิด scrollX เพื่อกันเหลื่อม
            autoWidth: false,
            scrollX: false,
            scrollCollapse: false,
            responsive: false,
            deferRender: true,

            ajax: function (dtData, callback) {
                QI_GetFilters(function (filters) {

                    const searchValue = (dtData.search && dtData.search.value) ? dtData.search.value.trim() : "";
                    const fd = new FormData();

                    // DataTables params
                    fd.append("Draw", dtData.draw);
                    fd.append("Start", dtData.start);
                    fd.append("Length", dtData.length);
                    fd.append("SearchText", searchValue);

                    // Filters
                    fd.append("Bu", (filters.Bu || []).join(","));
                    fd.append("ProjectID", filters.ProjectID || "");
                    fd.append("RegisterDateStart", filters.RegisterDateStart || "");
                    fd.append("RegisterDateEnd", filters.RegisterDateEnd || "");
                    fd.append("UnitID", (filters.UnitID || []).join(","));
                    fd.append("Inspect_Round", (filters.Inspect_Round || []).join(","));
                    fd.append("CSResponse", (filters.CSResponse || []).join(","));
                    fd.append("UnitCS", (filters.UnitCS || []).join(","));
                    fd.append("ExpectTransfer", (filters.ExpectTransfer || []).join(","));

                    fetch(baseUrl + "QueueInspect/GetlistDataQueueInspect", {
                        method: "POST",
                        body: fd
                    })
                        .then(r => r.json())
                        .then(res => {
                            QI_InitSummaryToggle();
                            QI_InitCheckingToggle();
                            QI_InitTransferToggle(); // ✅ เพิ่ม

                            QI_RenderSummaryAll(res.data2 || []);
                            QI_RenderCheckingAll(res.data3 || []);
                            QI_RenderTransferAll(res.data4 || []); // ✅ เพิ่ม

                            callback(res);
                        })

                        .catch(err => {
                            console.error("QueueInspect table error:", err);

                            QI_RenderSummaryAll([]); // ✅ clear summary
                            QI_RenderCheckingAll([]); // ✅ clear checking
                            QI_RenderTransferAll([]);
                            callback({
                                draw: dtData.draw,
                                recordsTotal: 0,
                                recordsFiltered: 0,
                                data: []
                            });
                        });

                });
            },

            columns: [
                { data: "Index", className: "text-center col-no" },
                {
                    data: "UnitCode",
                    className: "text-center col-unit",
                    render: function (data, type, row) {
                        if (type !== "display") return data;
                        return `<a href="#" class="qi-unit-link" data-unit="${data || ""}" data-id="${row.ID || ""}">${data || ""}</a>`;
                    }
                },
                {
                    data: null,
                    className: "col-customer",
                    render: function (data, type, row) {

                        let badges = "";

                        // 🟩 WiseConnect
                        if (parseInt(row.LineUserContract_Count || "0") > 0) {
                            badges += `
                                    <span class="svc svc-green" data-title="WiseConnect">
                                        <i class="fa fa-comment"></i>
                                    </span>`;
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
                { data: "AppointmentType", className: "text-center col-appoint" },
                {
                    data: "Status",
                    className: "text-center col-status",
                    render: function (data, type) {
                        if (type !== "display") return data;
                        const s = (data || "").trim();
                        if (s === "Register") return `<span style="color:red;">${s}</span>`;
                        if (s === "Inprocess") return `<span style="color:green;">${s}</span>`;
                        return `<span style="color:black;">${s}</span>`;
                    }
                },
                { data: "RegisterDate", className: "text-center col-time" },
                { data: "Counter", className: "text-center col-counter" },
                { data: "UnitStatus_CS", className: "text-center col-unitstatus" },
                { data: "Responsible", className: "col-resp" },
                { data: "CSRespons", className: "col-csresp" },
                {
                    data: null,
                    orderable: false,
                    searchable: false,
                    className: "text-center col-action",
                    render: function (data, type, row) {
                        return `
                            <button class="btn btn-icon btn-del qi-btn-delete"
                                    data-id="${row.ID || ""}"
                                    title="Delete" type="button">
                                <i class="fa fa-trash"></i>
                            </button>`;
                    }
                }
            ]
        });

        if (typeof done === "function") done(QueueInspectRegisterTableDt);
    });


}

/* =========================================================
   Queue Inspect - Summary + Checking (separate toggles)
   - Summary uses data2
   - Checking uses data3
   - All functions support callback
========================================================= */

function QI_InitSummaryToggle(done) {
    const btn = document.getElementById("btnSummaryToggle");
    const card = document.getElementById("qi-summary-card-view");
    const table = document.getElementById("qi-summary-table-view");

    if (!btn || !card || !table) {
        if (typeof done === "function") done(false);
        return;
    }

    if (btn.dataset.bound === "1") {
        if (typeof done === "function") done(true);
        return;
    }
    btn.dataset.bound = "1";

    btn.addEventListener("click", function () {
        QI_ToggleView(btn, card, table, done);
    });

    if (typeof done === "function") done(true);
}

function QI_InitCheckingToggle(done) {
    const btn = document.getElementById("btnCheckingToggle");
    const card = document.getElementById("qi-checking-card-view");
    const table = document.getElementById("qi-checking-table-view");

    if (!btn || !card || !table) {
        if (typeof done === "function") done(false);
        return;
    }

    if (btn.dataset.bound === "1") {
        if (typeof done === "function") done(true);
        return;
    }
    btn.dataset.bound = "1";

    btn.addEventListener("click", function () {
        QI_ToggleView(btn, card, table, done);
    });

    if (typeof done === "function") done(true);
}

/* shared toggle */
function QI_ToggleView(btn, cardView, tableView, done) {
    const icon = btn.querySelector("i");
    const isCardVisible = !cardView.classList.contains("d-none");

    if (isCardVisible) {
        cardView.classList.add("d-none");
        tableView.classList.remove("d-none");

        if (icon) {
            icon.classList.remove("fa-table");
            icon.classList.add("fa-th-large");
        }
        btn.setAttribute("title", "Change to card view");
        btn.setAttribute("aria-label", "Change to card view");
    } else {
        tableView.classList.add("d-none");
        cardView.classList.remove("d-none");

        if (icon) {
            icon.classList.remove("fa-th-large");
            icon.classList.add("fa-table");
        }
        btn.setAttribute("title", "Change to table view");
        btn.setAttribute("aria-label", "Change to table view");
    }

    if (typeof done === "function") done(true);
}

/* =========================
   Render Summary (data2)
========================= */
function QI_RenderSummaryAll(summaryList, done) {
    QI_RenderSummaryCards(summaryList, function () {
        QI_RenderSummaryTable(summaryList, function () {
            if (typeof done === "function") done(true);
        });
    });
}

function QI_RenderSummaryCards(summaryList, done) {
    const wrap = document.getElementById("qi-summary-5box");
    if (!wrap) {
        if (typeof done === "function") done(false);
        return;
    }

    const list = Array.isArray(summaryList) ? summaryList : [];
    const order = ["Register", "Wait", "In Process", "Fast Fix", "Done"];

    const map = {};
    list.forEach(x => {
        const k = (x.Topic || "").toString().trim().toLowerCase();
        if (k) map[k] = x;
    });

    let html = "";
    order.forEach(topic => {
        const k = topic.toLowerCase();
        const x = map[k] || { Topic: topic, Unit: "0", Value: "0.00", PercentUnit: "0%", Colorcode: "f5f6f8" };

        const color = (x.Colorcode || "").toString().trim();
        const bg = color ? `#${color.replace("#", "")}` : "#f5f6f8";

        html += `
            <div class="qi-sum-card">
                <div class="qi-sum-head" style="background:${bg}">
                    ${x.Topic || topic}
                </div>
                <div class="qi-sum-body">
                    <div class="qi-sum-cell">
                        <div class="qi-sum-num">${x.Unit ?? "0"}</div>
                        <div class="qi-sum-lbl">Unit</div>
                    </div>
                    <div class="qi-sum-cell">
                        <div class="qi-sum-num">${x.Value ?? "0.00"}</div>
                        <div class="qi-sum-lbl">Value</div>
                    </div>
                </div>
                <div class="qi-sum-foot">${x.PercentUnit ?? "0%"}</div>
            </div>
        `;
    });

    wrap.innerHTML = html;
    if (typeof done === "function") done(true);
}

function QI_RenderSummaryTable(summaryList, done) {
    const body = document.getElementById("qi-summary-tbody");
    if (!body) {
        if (typeof done === "function") done(false);
        return;
    }

    const list = Array.isArray(summaryList) ? summaryList : [];
    const order = ["Register", "Wait", "In Process", "Fast Fix", "Done"];

    const map = {};
    list.forEach(x => {
        const k = (x.Topic || "").toString().trim().toLowerCase();
        if (k) map[k] = x;
    });

    let rows = "";
    order.forEach(topic => {
        const k = topic.toLowerCase();
        const x = map[k] || { Topic: topic, Unit: "0", Value: "0.00", PercentUnit: "0%", Colorcode: "" };

        const color = (x.Colorcode || "").toString().trim();
        const dotBg = color ? `#${color.replace("#", "")}` : "#ddd";

        rows += `
            <tr>
                <td>
                    <span class="qi-topic-dot">
                        <span class="qi-dot" style="background:${dotBg}"></span>
                        <span>${x.Topic || topic}</span>
                    </span>
                </td>
                <td class="text-center">${x.Unit ?? "0"}</td>
                <td class="text-center">${x.Value ?? "0.00"}</td>
                <td class="text-center">${x.PercentUnit ?? "0%"}</td>
            </tr>
        `;
    });

    body.innerHTML = rows;
    if (typeof done === "function") done(true);
}

/* =========================
   Render Checking (data3)
   - card + table
========================= */
function QI_RenderCheckingAll(list, done) {
    QI_RenderCheckingCards(list, function () {
        QI_RenderCheckingTable(list, function () {
            if (typeof done === "function") done(true);
        });
    });
}

function QI_RenderCheckingCards(list, done) {
    const wrap = document.getElementById("qi-checking-5box");
    if (!wrap) {
        if (typeof done === "function") done(false);
        return;
    }

    const rows = Array.isArray(list) ? list : [];
    if (!rows.length) {
        wrap.innerHTML = `<div class="text-muted text-center p-3">No data</div>`;
        if (typeof done === "function") done(true);
        return;
    }

    let html = "";
    rows.forEach(x => {
        const topic = (x.Checking_type || "ไม่ระบุ");
        const cnt = (x.Cnt_unit || "0");
        const sum = (x.Sum_unit || "0.00");
        const pct = (x.Percent_Unit || "0%");
        // ถ้าไม่มี colorcode ใน checking ก็ให้ default สีอ่อน
        const bg = "#f5f6f8";

        html += `
            <div class="qi-sum-card">
                <div class="qi-sum-head" style="background:${bg}">
                    ${topic}
                </div>
                <div class="qi-sum-body">
                    <div class="qi-sum-cell">
                        <div class="qi-sum-num">${cnt}</div>
                        <div class="qi-sum-lbl">Unit</div>
                    </div>
                    <div class="qi-sum-cell">
                        <div class="qi-sum-num">${sum}</div>
                        <div class="qi-sum-lbl">Value</div>
                    </div>
                </div>
                <div class="qi-sum-foot">${pct}</div>
            </div>
        `;
    });

    wrap.innerHTML = html;
    if (typeof done === "function") done(true);
}

function QI_RenderCheckingTable(list, done) {
    const tbody = document.getElementById("qi-checking-tbody");
    if (!tbody) {
        if (typeof done === "function") done(false);
        return;
    }

    const rows = Array.isArray(list) ? list : [];
    if (!rows.length) {
        tbody.innerHTML = `<tr><td colspan="4" class="text-center text-muted">No data</td></tr>`;
        if (typeof done === "function") done(true);
        return;
    }

    const esc = (s) => String(s ?? "").replace(/[&<>"']/g, (m) => ({
        "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;"
    }[m]));

    let html = "";
    rows.forEach(x => {
        html += `
            <tr>
                <td>${esc(x.Checking_type || "ไม่ระบุ")}</td>
                <td class="text-center">${esc(x.Cnt_unit || "0")}</td>
                <td class="text-center">${esc(x.Sum_unit || "0.00")}</td>
                <td class="text-center">${esc(x.Percent_Unit || "0%")}</td>
            </tr>
        `;
    });

    tbody.innerHTML = html;
    if (typeof done === "function") done(true);
}

function QI_InitTransferToggle(done) {
    const btn = document.getElementById("btnTransferToggle");
    const card = document.getElementById("qi-transfer-card-view");
    const table = document.getElementById("qi-transfer-table-view");

    if (!btn || !card || !table) { if (done) done(false); return; }

    if (btn.dataset.bound === "1") { if (done) done(true); return; }
    btn.dataset.bound = "1";

    btn.addEventListener("click", function () {
        QI_ToggleView(btn, card, table, function (ok) {
            if (done) done(ok);
        });
    });

    if (done) done(true);
}

function QI_RenderTransferAll(list, done) {
    QI_RenderTransferCards(list, function () {
        QI_RenderTransferTable(list, function () {
            if (done) done(true);
        });
    });
}

function QI_RenderTransferCards(list, done) {
    const wrap = document.getElementById("qi-transfer-5box");
    if (!wrap) { if (done) done(false); return; }

    const rows = Array.isArray(list) ? list : [];
    if (!rows.length) {
        wrap.innerHTML = `<div class="text-muted text-center p-3">No data</div>`;
        if (done) done(true);
        return;
    }

    let html = "";
    rows.forEach(x => {
        const topic = (x.TransferType || "ไม่ระบุ");
        const cnt = (x.Cnt_unit || "0");
        const sum = (x.Sum_unit || "0.00");
        const pct = (x.Percent_Unit || "0%");
        const bg = "#f5f6f8"; // ถ้าไม่มีสี ก็ใช้สีอ่อน

        html += `
            <div class="qi-sum-card">
                <div class="qi-sum-head" style="background:${bg}">
                    ${topic}
                </div>
                <div class="qi-sum-body">
                    <div class="qi-sum-cell">
                        <div class="qi-sum-num">${cnt}</div>
                        <div class="qi-sum-lbl">Unit</div>
                    </div>
                    <div class="qi-sum-cell">
                        <div class="qi-sum-num">${sum}</div>
                        <div class="qi-sum-lbl">Value</div>
                    </div>
                </div>
                <div class="qi-sum-foot">${pct}</div>
            </div>
        `;
    });

    wrap.innerHTML = html;
    if (done) done(true);
}

function QI_RenderTransferTable(list, done) {
    const tbody = document.getElementById("qi-transfer-tbody");
    if (!tbody) { if (done) done(false); return; }

    const rows = Array.isArray(list) ? list : [];
    if (!rows.length) {
        tbody.innerHTML = `<tr><td colspan="4" class="text-center text-muted">No data</td></tr>`;
        if (done) done(true);
        return;
    }

    const esc = (s) => String(s ?? "").replace(/[&<>"']/g, (m) => ({
        "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;"
    }[m]));

    let html = "";
    rows.forEach(x => {
        html += `
            <tr>
                <td>${esc(x.TransferType || "ไม่ระบุ")}</td>
                <td class="text-center">${esc(x.Cnt_unit || "0")}</td>
                <td class="text-center">${esc(x.Sum_unit || "0.00")}</td>
                <td class="text-center">${esc(x.Percent_Unit || "0%")}</td>
            </tr>
        `;
    });

    tbody.innerHTML = html;
    if (done) done(true);
}


// Search button
$(document).off("click.qi", "#btnSearch").on("click.qi", "#btnSearch", function () {
    if (!QueueInspectRegisterTableDt) return initQueueInspectRegisterTable();
    QueueInspectRegisterTableDt.ajax.reload();
});
