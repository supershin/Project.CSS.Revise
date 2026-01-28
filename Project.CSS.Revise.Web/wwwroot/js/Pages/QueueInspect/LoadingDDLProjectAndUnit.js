// ~/js/Pages/QueueInspect/LoadingDDLProjectAndUnit.js

function QI_ApplyDefaultProjectFromCookie(done) {
    const savedProjectId = (typeof QI_GetCookie === "function") ? QI_GetCookie(QI_COOKIE_LAST_PROJECT) : "";
    if (!savedProjectId) { if (done) done(false); return; }

    // ต้องมี option ก่อน (กัน cookie เก่า)
    const exists = $("#ddl_Project option[value='" + savedProjectId.replace(/'/g, "\\'") + "']").length > 0;
    if (!exists) { if (done) done(false); return; }

    const ch = window.QI_CHOICES["#ddl_Project"];
    if (ch && typeof ch.setChoiceByValue === "function") {
        ch.setChoiceByValue(savedProjectId);
    } else {
        $("#ddl_Project").val(savedProjectId);
    }

    // ✅ โหลด unit ตาม project ที่ set เสร็จ
    QI_LoadUnitByProject(savedProjectId, function () {
        if (done) done(true);
    });
}

function QI_InitProject(done) {
    window.createChoice("#ddl_Project", {
        placeholderValue: "Select Project",
        removeItemButton: true
    });
    if (typeof done === "function") done();
}

// ✅ ตั้งค่า options ของ Project แบบถูกวิธี (destroy -> set html -> create)
function QI_SetProjectOptions(html, done) {
    window.destroyChoice("#ddl_Project");
    $("#ddl_Project").html(html);

    // สร้าง Choices ใหม่
    QI_InitProject(function () {
        if (typeof done === "function") done(true);
    });
}


// ✅ Unit ต้องเริ่มต้นเป็น Please Select Project (ไม่มี list)
function QI_ResetUnit(done) {
    window.destroyChoice("#ddl_UnitCode");
    $("#ddl_UnitCode").empty();
    window.createChoice("#ddl_UnitCode", { placeholderValue: "Select Unit Code" });

    if (typeof done === "function") done();
}

function QI_LoadProjectByBU(buIds, done) {

    // When no BUG selected → restore original projects from Razor
    if (!buIds || buIds.length === 0) {
        QI_SetProjectOptions(window.QI_ORG_PROJECT_HTML, function () {
            QI_ResetUnit(function () {
                // ✅ apply cookie default (ถ้ามี) แล้วค่อย done
                QI_ApplyDefaultProjectFromCookie(function () {
                    if (typeof done === "function") done(true);
                });
            });
        });
        return;
    }

    $.ajax({
        url: baseUrl + "QueueInspect/GetProjectListByBU",
        type: "POST",
        data: { L_BUID: buIds.join(",") }
    })
        .done(function (res) {
            const list = res?.data || [];

            let html = ``;
            for (let i = 0; i < list.length; i++) {
                html += `<option value="${list[i].ProjectID}">${list[i].ProjectNameTH}</option>`;
            }

            QI_SetProjectOptions(html, function () {
                QI_ResetUnit(function () {
                    // ✅ apply cookie default (ถ้ามี) แล้วค่อย done
                    QI_ApplyDefaultProjectFromCookie(function () {
                        if (typeof done === "function") done(true);
                    });
                });
            });
        })
        .fail(function (xhr) {
            console.log("QI_LoadProjectByBU error", xhr);

            if (typeof errorToast === "function") {
                errorToast("Failed to load project data. Please try again.");
            }

            // ✅ fallback: restore original + still apply cookie
            QI_SetProjectOptions(window.QI_ORG_PROJECT_HTML, function () {
                QI_ResetUnit(function () {
                    QI_ApplyDefaultProjectFromCookie(function () {
                        if (typeof done === "function") done(false);
                    });
                });
            });
        });
}

function QI_LoadUnitByProject(projectId, done) {
    QI_ResetUnit(); // always reset first

    if (!projectId) {
        if (typeof done === "function") done([]);
        return;
    }

    $.ajax({
        url: baseUrl + "QueueInspect/GetUnitListByProject",
        type: "POST",
        data: { ProjectID: projectId }
    })
        .done(function (res) {

            const list = res?.data || [];

            window.destroyChoice("#ddl_UnitCode");
            const $ddl = $("#ddl_UnitCode");
            $ddl.empty();

            for (let i = 0; i < list.length; i++) {
                const code = list[i].UnitCode;
                if (!code) continue;
                $ddl.append(`<option value="${code}">${code}</option>`);
            }

            window.createChoice("#ddl_UnitCode", { placeholderValue: "Select Unit Code" });

            if (typeof done === "function") done(list);

        })
        .fail(function (xhr) {
            console.log("QI_LoadUnitByProject error", xhr);

            // ✅ show toast
            if (typeof errorToast === "function") {
                errorToast("Failed to load unit data. Please try again.");
            }

            if (typeof done === "function") done([]);
        });
}

function QI_BindDropdownEvents() {

    $("#ddl_BUG").off("change.qi").on("change.qi", function () {
        const buIds = window.QI_CHOICES["#ddl_BUG"]?.getValue(true) || [];
        QI_LoadProjectByBU(buIds, function () {
            // done
        });
    });

    $("#ddl_Project").off("change.qi").on("change.qi", function () {
        const projectId = window.QI_CHOICES["#ddl_Project"]?.getValue(true) || "";

        // ✅ save cookie 30 days
        if (typeof QI_SetCookie === "function") {
            QI_SetCookie(QI_COOKIE_LAST_PROJECT, projectId, 30);
        }

        QI_LoadUnitByProject(projectId, function () {
            // done
        });
    });
}

