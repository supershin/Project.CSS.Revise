// /js/Pages/QueueBank/QueueBankCustomerView.js

(function () {

    let CustomerQueueTableDt = null;

    // =========================
    // Helpers
    // =========================

    function getProjectId() {
        const hid = document.getElementById("hidProjectId");
        return hid ? (hid.value || "") : "";
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

    // format "xx.xx M" เผื่อใช้ทีหลัง
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
        const unit = data?.Unit ?? "0";
        if (unitEl) unitEl.textContent = unit;
    }

    // =========================
    // DataTable: Customer Queue
    // =========================
    function initCustomerQueueTable() {
        const tbl = $('#customerQueueTable');
        if (!tbl.length || !$.fn.DataTable) {
            console.warn("CustomerQueueTable: table or DataTables not found.");
            return;
        }

        CustomerQueueTableDt = tbl.DataTable({
            processing: true,
            serverSide: true,
            searching: true,
            lengthChange: true,
            pageLength: 10,
            ordering: false,
            autoWidth: false,
            scrollX: true,
            ajax: function (data, callback, settings) {

                const projectId = getProjectId() || "";

                const searchValue = (data.search && data.search.value)
                    ? data.search.value.trim()
                    : "";

                const formData = new FormData();
                // DataTables params
                formData.append("draw", data.draw);
                formData.append("start", data.start);
                formData.append("length", data.length);
                formData.append("SearchTerm", searchValue);

                // QueueBank filters
                formData.append("L_Act", "RegisterTable");
                formData.append("L_ProjectID", projectId);
                formData.append("L_RegisterDateStart", "");
                formData.append("L_RegisterDateEnd", "");
                formData.append("L_UnitID", "");
                formData.append("L_CSResponse", "");
                formData.append("L_UnitCS", "");
                formData.append("L_ExpectTransfer", "");

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
                            recordsTotal: res.recordsTotal || 0,
                            recordsFiltered: res.recordsFiltered || 0,
                            data: res.data || []
                        });
                    })
                    .catch(err => {
                        console.error("CustomerView GetlistRegisterTable error:", err);
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
                    data: "CustomerName",
                    name: "CustomerName"
                },
                {
                    data: "Appointment",
                    name: "Appointment",
                    render: function (data, type, row) {
                        if (type !== "display") return data;

                        return (data && String(data).trim() !== "") ? "Y" : "N";
                    }
                },
                {
                    data: "Status",
                    name: "Status",
                    render: function (data, type, row) {
                        if (type !== "display") return data;

                        const text = (data || "").trim();
                        const lower = text.toLowerCase();

                        let cls = "bg-secondary text-white";
                        if (lower === "register") {
                            cls = "bg-success text-white";
                        } else if (lower === "queue") {
                            cls = "bg-info text-white";
                        } else if (lower === "in process") {
                            cls = "bg-warning text-dark";
                        } else if (lower === "done") {
                            cls = "bg-success text-white";
                        }

                        return `<span class="status-pill ${cls}">${text}</span>`;
                    }
                },
                {
                    data: "StatusTime",
                    name: "StatusTime"
                }
            ],
            order: [[0, "asc"]]
        });
    }

    // =========================
    // Summary Register (4 กล่อง)
    // =========================
    function loadSummaryRegisterAllCustomer() {

        console.log("loadSummaryRegisterAllCustomer called");

        const projectId = getProjectId() || "";

        const formData = new FormData();
        formData.append("L_Act", "SummeryRegisterType");
        formData.append("L_ProjectID", projectId);
        formData.append("L_RegisterDateStart", "");
        formData.append("L_RegisterDateEnd", "");
        formData.append("L_UnitID", "");
        formData.append("L_CSResponse", "");
        formData.append("L_UnitCS", "");
        formData.append("L_ExpectTransfer", "");
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
                const typeList = res.listDataSummeryRegisterType || [];
                const typeMap = qbMapByTopic(typeList);

                qbUpdateSummaryBox("register", typeMap["register"]);
                qbUpdateSummaryBox("queue", typeMap["queue"]);
                qbUpdateSummaryBox("inprocess", typeMap["in process"]);
                qbUpdateSummaryBox("done", typeMap["done"]);
            })
            .catch(err => {
                console.error("CustomerView GetlistSummeryRegister error:", err);

                qbUpdateSummaryBox("register", null);
                qbUpdateSummaryBox("queue", null);
                qbUpdateSummaryBox("inprocess", null);
                qbUpdateSummaryBox("done", null);
            })
            .finally(() => {
                if (typeof hideLoading === "function") hideLoading();
            });
    }

    // =========================
    // Summary Bank
    // =========================
    function loadSummaryRegisterBankCustomer() {
        const projectId = getProjectId() || "";

        const formData = new FormData();
        formData.append("L_Act", "SummeryRegisterBank");
        formData.append("L_ProjectID", projectId);
        formData.append("L_RegisterDateStart", "");
        formData.append("L_RegisterDateEnd", "");
        formData.append("L_UnitID", "");
        formData.append("L_CSResponse", "");
        formData.append("L_UnitCS", "");
        formData.append("L_ExpectTransfer", "");
        formData.append("L_QueueTypeID", "48");

        formData.append("draw", "1");
        formData.append("start", "0");
        formData.append("length", "1000");
        formData.append("SearchTerm", "");

        const tbody = document.getElementById("summary-bank-body");
        if (!tbody) return;

        if (typeof showLoading === "function") showLoading();

        fetch(baseUrl + "QueueBank/GetlistSummeryRegisterBank", {
            method: "POST",
            body: formData
        })
            .then(r => r.json())
            .then(res => {
                const list = res.listDataSummeryRegisterType || [];

                if (!list.length) {
                    tbody.innerHTML = `
                        <tr>
                            <td colspan="3" class="text-center text-muted">No data</td>
                        </tr>`;
                    return;
                }

                const rowsHtml = list.map(item => {
                    const bankCode = (item.BankCode || "").trim();
                    const bankName = item.BankName || "";
                    const unit = item.Unit || "0";
                    const percentText = (item.Percent || "0") + "%";

                    let bankCellHtml = "";
                    if (bankCode && bankCode.toLowerCase() !== "no data") {
                        bankCellHtml = `
                            <div class="d-flex align-items-center gap-2">
                                <img src="${baseUrl}image/ThaiBankicon/${bankCode}.png"
                                     alt="${bankCode}"
                                     width="26"
                                     class="me-2">
                                <span>${bankName || bankCode}</span>
                            </div>`;
                    } else {
                        bankCellHtml = `<span>${bankName || "No data"}</span>`;
                    }

                    return `
                        <tr>
                            <td class="text-start">${bankCellHtml}</td>
                            <td>${unit}</td>
                            <td>${percentText}</td>
                        </tr>`;
                }).join("");

                tbody.innerHTML = rowsHtml;
            })
            .catch(err => {
                console.error("CustomerView GetlistSummeryRegisterBank error:", err);
                tbody.innerHTML = `
                    <tr>
                        <td colspan="3" class="text-center text-danger">
                            Error loading Summary Bank
                        </td>
                    </tr>`;
            })
            .finally(() => {
                if (typeof hideLoading === "function") hideLoading();
            });
    }

    function bindRefreshButton() {
        const btn = document.getElementById("btnRefreshCustomerView");
        if (!btn) return;

        btn.addEventListener("click", function () {
            // reload table
            if (CustomerQueueTableDt) {
                CustomerQueueTableDt.ajax.reload(null, false); // false = ไม่เด้งกลับหน้าแรก
            }

            // reload summary
            loadSummaryRegisterAllCustomer();
            loadSummaryRegisterBankCustomer();
        });
    }


    // =========================
    // Boot
    // =========================
    function boot() {
        initCustomerQueueTable();
        loadSummaryRegisterAllCustomer();
        loadSummaryRegisterBankCustomer();
        bindRefreshButton(); // ✅ เพิ่มบรรทัดนี้
    }


    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", boot);
    } else {
        boot();
    }

    window.QueueBankCustomerView = {
        reloadTable: function () {
            if (CustomerQueueTableDt) {
                CustomerQueueTableDt.ajax.reload();
            }
        },
        reloadSummary: function () {
            loadSummaryRegisterAllCustomer();
            loadSummaryRegisterBankCustomer();
        }
    };

})();
