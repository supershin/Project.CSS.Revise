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
                        .then(res => callback(res))
                        .catch(err => {
                            console.error("QueueInspect table error:", err);
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

// Search button
$(document).off("click.qi", "#btnSearch").on("click.qi", "#btnSearch", function () {
    if (!QueueInspectRegisterTableDt) return initQueueInspectRegisterTable();
    QueueInspectRegisterTableDt.ajax.reload();
});
