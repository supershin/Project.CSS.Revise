// =========================
// Register Unit - Load Dropdown (WITH CALLBACK)
// =========================

function qiClearRegisterUnitDropdown(callback) {
    const ddl = document.getElementById("ddl_RegisterUnit");
    if (!ddl) {
        if (typeof callback === "function") {
            callback({ ok: false, message: "ddl_RegisterUnit not found", data: null });
        }
        return;
    }

    ddl.innerHTML = "";
    ddl.appendChild(new Option("-- Select Unit Code --", ""));

    if (typeof callback === "function") {
        callback({ ok: true, message: "cleared", data: null });
    }
}

function qiFillRegisterUnitDropdown(list, callback) {
    const ddl = document.getElementById("ddl_RegisterUnit");
    if (!ddl) {
        if (typeof callback === "function") {
            callback({ ok: false, message: "ddl_RegisterUnit not found", data: null });
        }
        return;
    }

    ddl.innerHTML = "";
    ddl.appendChild(new Option("-- Select Unit Code --", ""));

    const safeList = list || [];
    safeList.forEach(x => {
        const val = (x.value ?? x.Value ?? x.id ?? x.ID ?? "").toString();
        const text = (x.text ?? x.Text ?? x.unitCode ?? x.UnitCode ?? "").toString();
        ddl.appendChild(new Option(text, val));
    });

    if (typeof callback === "function") {
        callback({ ok: true, message: "filled", data: { count: safeList.length } });
    }
}

function qiReInitChoicesRegisterUnit(callback) {
    const ddlId = "#ddl_RegisterUnit";

    if (!window.createChoice) {
        if (typeof callback === "function") {
            callback({ ok: false, message: "createChoice helper not found", data: null });
        }
        return;
    }

    try {
        if (window.__qiRegisterUnitChoice && window.__qiRegisterUnitChoice.destroy) {
            window.__qiRegisterUnitChoice.destroy();
        }
    } catch (e) { }

    window.__qiRegisterUnitChoice = window.createChoice(ddlId, {
        placeholderValue: "Select Unit Code",
        removeItemButton: true,
        searchEnabled: true
    });

    if (typeof callback === "function") {
        callback({ ok: true, message: "choices initialized", data: window.__qiRegisterUnitChoice });
    }
}

function qiLoadRegisterUnitList(callback) {
    const projectEl = document.getElementById("ddl_Project");
    const projectID = projectEl ? (projectEl.value || "") : "";

    // 1) Clear dropdown first
    qiClearRegisterUnitDropdown(function (r1) {

        // 2) If no project -> just init choices and return
        if (!projectID) {
            qiReInitChoicesRegisterUnit(function (r2) {
                if (typeof callback === "function") {
                    callback({
                        ok: true,
                        message: "no project selected",
                        projectID: "",
                        data: { list: [] }
                    });
                }
            });
            return;
        }

        // 3) Load via ajax
        $.ajax({
            url: baseUrl + "QueueInspect/GetListUnitForRegisterInspect",
            type: "POST",
            data: { ProjectID: projectID },
            success: function (res) {
                const list = res && res.ListData ? res.ListData : [];

                qiFillRegisterUnitDropdown(list, function (r3) {
                    qiReInitChoicesRegisterUnit(function (r4) {
                        if (typeof callback === "function") {
                            callback({
                                ok: true,
                                message: "loaded",
                                projectID: projectID,
                                data: { list: list, count: (list || []).length }
                            });
                        }
                    });
                });
            },
            error: function (xhr) {
                console.error("GetListUnitForRegisterInspect error", xhr);

                qiClearRegisterUnitDropdown(function () {
                    qiReInitChoicesRegisterUnit(function () {
                        if (typeof callback === "function") {
                            callback({
                                ok: false,
                                message: "ajax error",
                                projectID: projectID,
                                error: xhr,
                                data: { list: [] }
                            });
                        }
                    });
                });
            }
        });
    });
}


// =========================
// Modal Open (WITH CALLBACK)
// =========================
function openCreateRegister(callback) {
    const modalEl = document.getElementById("modalCreateRegister");
    if (!modalEl) {
        if (typeof callback === "function") {
            callback({ ok: false, message: "modalCreateRegister not found", data: null });
        }
        return;
    }

    const modal = new bootstrap.Modal(modalEl, { backdrop: "static" });
    modal.show();

    // ✅ Set project name in header
    const ddlProject = document.getElementById("ddl_Project");
    const projectNameEl = document.getElementById("qiRegProjectName");
    let projectID = "";
    let projectText = "-";

    if (ddlProject) {
        projectID = ddlProject.value || "";
        projectText = ddlProject.options[ddlProject.selectedIndex]?.text || "-";
    }
    if (projectNameEl) projectNameEl.textContent = projectText;

    // ✅ Feather render (Riho required)
    if (window.feather) feather.replace();

    // ✅ Load dropdown data by ProjectID
    qiLoadRegisterUnitList(function (r) {
        if (typeof callback === "function") {
            callback({
                ok: r.ok,
                message: "modal opened + " + r.message,
                projectID: projectID,
                data: r.data,
                error: r.error
            });
        }
    });
}
