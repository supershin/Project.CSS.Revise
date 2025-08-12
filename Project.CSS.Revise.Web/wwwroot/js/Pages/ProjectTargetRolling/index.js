// ------------------------ YEAR DROPDOWN ------------------------

function populateYearDropdown() {
    const ddlYear = document.getElementById('ddl_year');
    if (!ddlYear) return;

    const currentYear = new Date().getFullYear();

    ddlYear.innerHTML = ''; // Clear existing options

    for (let i = currentYear - 3; i <= currentYear + 3; i++) {
        const option = document.createElement('option');
        option.value = i;
        option.text = i;

        if (i === currentYear) {
            option.setAttribute('selected', 'selected'); // Mark current year
        }

        ddlYear.appendChild(option);
    }
}

function initYearDropdown() {
    populateYearDropdown();

    new Choices('#ddl_year', {
        removeItemButton: true,
        placeholderValue: 'เลือกปีได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });
}

// ------------------------ QUARTER & MONTH DROPDOWNS ------------------------

let choicesQuarter;
let choicesMonth;

// keep this global so other functions can use it
const monthMap = {
    Q1: [1, 2, 3],
    Q2: [4, 5, 6],
    Q3: [7, 8, 9],
    Q4: [10, 11, 12]
};

const monthLabels = {
    1: "Jan", 2: "Feb", 3: "Mar",
    4: "Apr", 5: "May", 6: "Jun",
    7: "Jul", 8: "Aug", 9: "Sep",
    10: "Oct", 11: "Nov", 12: "Dec"
};

const labelToMonth = Object.fromEntries(Object.entries(monthLabels).map(([k, v]) => [v, +k]));

function initQuarterDropdown() {
    choicesQuarter = new Choices('#ddl_quarter', {
        removeItemButton: true,
        placeholderValue: 'เลือก Quarterได้มากกว่า 1',
        shouldSort: false
    });

    document.getElementById('ddl_quarter').addEventListener('change', updateMonthDropdownFromQuarter);
}

function initMonthDropdown() {
    choicesMonth = new Choices('#ddl_month', {
        removeItemButton: true,
        placeholderValue: 'เลือกเดือนได้มากกว่า 1',
        shouldSort: false
    });

    updateMonthDropdown([]); // Load all months initially
}

function updateMonthDropdownFromQuarter() {
    const selectedQuarters = choicesQuarter.getValue(true); // array of Q1–Q4
    updateMonthDropdown(selectedQuarters);
}

function updateMonthDropdown(selectedQuarters) {
    let allowedMonths = [];

    if (selectedQuarters.length === 0) {
        allowedMonths = Array.from({ length: 12 }, (_, i) => i + 1);
    } else {
        selectedQuarters.forEach(q => {
            allowedMonths = allowedMonths.concat(monthMap[q] || []);
        });
    }

    allowedMonths = [...new Set(allowedMonths)].sort((a, b) => a - b);

    choicesMonth.clearStore();
    choicesMonth.setChoices(
        allowedMonths.map(m => ({
            value: m,
            label: monthLabels[m],
            selected: false
        })),
        'value',
        'label',
        true
    );
}

// ------------------------ PLAN TYPE & BUG DROPDOWN ------------------------

function initPlanTypeDropdown() {
    new Choices('#ddl_plantype', {
        removeItemButton: true,
        placeholderValue: 'เลือกประเภทแผนได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });
}

let choicesBug;
let choicesProject;

function initBuDropdown() {
        choicesBug = new Choices('#ddl_bug', {
        removeItemButton: true,
        placeholderValue: 'เลือกกลุ่มธุรกิจได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });

    document.getElementById('ddl_bug').addEventListener('change', onBuChanged);
}

// ------------------------ PROJECT ------------------------

function initProjectDropdown() {
    choicesProject = new Choices('#ddl_project', {
        removeItemButton: true,
        placeholderValue: 'เลือกโปรเจกต์ได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });

    loadProjectFromBU(); // load all initially
}

function onBuChanged() {
    const selectedBUs = choicesBug.getValue(true); // array of selected BU IDs
    loadProjectFromBU(selectedBUs);
}

function loadProjectFromBU(selectedBUs = []) {
    const formData = new FormData();
    formData.append("L_BUID", selectedBUs.join(",")); // join IDs or blank if none

    fetch( baseUrl + 'Projecttargetrolling/GetProjectListByBU' , {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(json => {
            if (json.success) {
                const projectList = json.data || [];

                choicesProject.clearStore();
                choicesProject.setChoices(
                    projectList.map(p => ({
                        value: p.ProjectID,
                        label: p.ProjectNameTH
                    })),
                    'value',
                    'label',
                    true
                );
            }
        })
        .catch(error => {
            console.error('Load project failed:', error);
        });
}

// ------------------------ INIT ALL ------------------------

function initAllDropdowns() {
    initYearDropdown();
    initQuarterDropdown();
    initMonthDropdown();
    initPlanTypeDropdown();
    initBuDropdown();
    initProjectDropdown();
}

// 🔥 เรียกเลยโดยไม่ต้องใช้ DOMContentLoaded
initAllDropdowns();

// ------------------------ SEARCH TABLE ------------------------
function collectRollingPlanFilters() {
    return {
        L_Year: $('#ddl_year').val().join(','),
        L_Quarter: choicesQuarter.getValue(true).join(','),
        L_Month: choicesMonth.getValue(true).join(','),
        L_PlanTypeID: $('#ddl_plantype').val() ? $('#ddl_plantype').val().join(',') : '',
        L_Bu: $('#ddl_bug').val() ? $('#ddl_bug').val().join(',') : '',
        L_ProjectID: $('#ddl_project').val() ? $('#ddl_project').val().join(',') : ''
    };
}

function searchRollingPlanData() {
    showLoading();
    const filter = collectRollingPlanFilters();

    // ⛳ ตรวจสอบเดือนที่เลือก
    let selectedMonths = $('#ddl_month').val()?.map(Number) || [];

    // ❗ ถ้าไม่ได้เลือกเดือน → ใช้ quarter แทน
    if (selectedMonths.length === 0) {
        const selectedQuarters = choicesQuarter.getValue(true); // ['Q1', 'Q2',...]
        selectedMonths = selectedQuarters.flatMap(q => monthMap[q] || []);
    }

    // 🧼 ถ้ายังไม่มีอะไรเลย → โชว์ทุกเดือน
    if (selectedMonths.length === 0) {
        selectedMonths = Array.from({ length: 12 }, (_, i) => i + 1);
    }

    // ส่งค่า filter ไปยัง API
    const formData = new FormData();
    for (const key in filter) {
        formData.append(key, filter[key]);
    }

    fetch(baseUrl + 'Projecttargetrolling/GetDataTableProjectAndTargetRolling', {
        method: 'POST',
        body: formData
    })
        .then(res => res.json())
        .then(json => {
            if (json.success) {
                renderTableFromJson(json.data, selectedMonths); // ✅ ส่ง selectedMonths
                renderSummaryCards(json.datasum); // <<--- เพิ่มตรงนี้
                hideLoading()
            }
        })
        .catch(err => {
            hideLoading()
            console.error('Error fetching data:', err);
        });
}

const pendingEdits = []; // {ProjectID, PlanTypeID, Year, Month, PlanAmountID, OldValue, NewValue}

function renderTableFromJson(data, selectedMonths) {
    let html = `
        <table id="rollingPlanTable" class="table table-bordered table-striped w-auto">
            <thead>
                <tr>
                    <th>Project Name</th>
                    <th>Plan Type Name</th>
                    <th>Year</th>`;

    selectedMonths.forEach(m => {
        html += `<th colspan="2">${monthLabels[m]}</th>`;
    });

    html += `<th colspan="2">Total</th>`;

    html += `</tr><tr>
        <th></th><th></th><th></th>`;

    selectedMonths.forEach(() => {
        html += `<th>Unit</th><th>Value (M)</th>`;
    });

    html += `<th>Unit</th><th>Value (M)</th></tr></thead><tbody>`;

    if (data?.length) {
        data.forEach(row => {
            // we'll need hidden identifiers for updates
            const pid = row.ProjectID ?? '';
            const ptypeId = row.PlanTypeID ?? 0;
            const year = Number(row.PlanYear ?? 0);

            html += `<tr data-projectid="${pid}" data-plantypeid="${ptypeId}" data-year="${year}">
                <td>${row.ProjectName ?? ''}</td>
                <td>${row.PlanTypeName ?? ''}</td>
                <td>${row.PlanYear ?? ''}</td>`;

            selectedMonths.forEach(m => {
                const key = monthLabels[m]; // e.g. Jan
                const unitVal = row[`${key}_Unit`] ?? '';
                const moneyVal = row[`${key}_Value`] ?? '';

                // editable cells with metadata for later updates
                html += `
                    <td class="editable" contenteditable="true"
                        data-field="${key}_Unit"
                        data-month="${m}"
                        data-planamountid="183"
                        data-old="${unitVal}">${unitVal}</td>
                    <td class="editable" contenteditable="true"
                        data-field="${key}_Value"
                        data-month="${m}"
                        data-planamountid="184"
                        data-old="${moneyVal}">${moneyVal}</td>`;
            });

            // Totals shown, not editable
            html += `<td class="total-unit">${row.Total_Unit ?? '-'}</td>
                     <td class="total-value">${row.Total_Value ?? '-'}</td>
            </tr>`;
        });
    } else {
        html += `<tr><td colspan="${3 + selectedMonths.length * 2 + 2}" class="text-center">ไม่พบข้อมูล</td></tr>`;
    }

    html += `</tbody></table>`;
    $('#rolling-plan-container').html(html);

    // Attach editing behaviors after render
    bindEditableHandlers();
}

function bindEditableHandlers() {
    const table = $('#rollingPlanTable');

    // highlight row when any editable cell focused
    table.on('focus', 'td.editable', function () {
        $(this).closest('tr').addClass('row-editing');
    });
    table.on('blur', 'td.editable', function () {
        $(this).closest('tr').removeClass('row-editing');
    });

    // Restrict input to numeric (allow dot, minus, backspace, arrows, tab, delete)
    table.on('keydown', 'td.editable', function (e) {
        const allowedKeys = [
            8, 9, 13, 27, 37, 38, 39, 40, 46, // control keys
            110, 190, // dot on numpad / main keyboard
            189 // minus
        ];
        if (allowedKeys.includes(e.keyCode)) return;
        if (e.key >= '0' && e.key <= '9') return;
        if (e.ctrlKey || e.metaKey) return;
        e.preventDefault();
    });

    // Detect changes while typing (real-time)
    table.on('input', 'td.editable', function () {
        const $cell = $(this);
        let newVal = $cell.text().replace(/,/g, '').trim();
        const oldVal = ($cell.data('old') ?? '').toString();

        if (newVal !== '' && !/^-?\d*\.?\d*$/.test(newVal)) {
            // Not numeric → revert to old
            $cell.text(oldVal);
            return;
        }

        if (newVal !== oldVal) {
            $cell.addClass('dirty');
        } else {
            $cell.removeClass('dirty');
        }
    });

    // When editing finished → validate, save change, auto-save to server
    table.on('blur', 'td.editable', function () {
        const $cell = $(this);
        let newVal = $cell.text().replace(/,/g, '').trim();
        if (newVal === '' || newVal === '-') newVal = '';

        // if invalid number → revert
        if (newVal !== '' && isNaN(newVal)) {
            $cell.text($cell.data('old'));
            $cell.removeClass('dirty');
            return;
        }

        const oldVal = ($cell.data('old') ?? '').toString();
        if (oldVal === newVal) {
            $cell.removeClass('dirty');
            return;
        }

        const $row = $cell.closest('tr');
        const payload = {
            ProjectID: $row.data('projectid'),
            PlanTypeID: Number($row.data('plantypeid') || 0),
            Year: Number($row.data('year') || 0),
            Month: Number($cell.data('month')),
            PlanAmountID: Number($cell.data('planamountid')),
            OldValue: oldVal === '' ? null : Number(oldVal),
            NewValue: newVal === '' ? null : Number(newVal)
        };

        const isValue = payload.PlanAmountID === 184;
        $cell.text(
            newVal === ''
                ? ''
                : isValue
                    ? Number(newVal).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })
                    : Number(newVal).toLocaleString()
        );

        upsertPendingEdit(payload);
        recomputeRowTotals($row);

        // **Auto-save when change is confirmed**
        savePendingEdits();
    });
}


function upsertPendingEdit(change) {
    const idx = pendingEdits.findIndex(x =>
        x.ProjectID === change.ProjectID &&
        x.PlanTypeID === change.PlanTypeID &&
        x.Year === change.Year &&
        x.Month === change.Month &&
        x.PlanAmountID === change.PlanAmountID
    );
    if (idx >= 0) {
        pendingEdits[idx] = change;
    } else {
        pendingEdits.push(change);
    }
}

// Sum the editable cells in the row to update totals live
function recomputeRowTotals($row) {
    let totalUnit = 0, totalValue = 0;

    $row.find('td.editable').each(function () {
        const pid = Number($(this).data('planamountid')); // 183 unit / 184 value
        const raw = $(this).text().replace(/,/g, '').trim();
        if (raw === '' || isNaN(raw)) return;
        const n = Number(raw);
        if (pid === 183) totalUnit += n;
        else if (pid === 184) totalValue += n;
    });

    // write totals
    $row.find('td.total-unit').text(totalUnit ? totalUnit.toLocaleString() : '-');
    $row.find('td.total-value').text(totalValue ? totalValue.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '-');
}

function savePendingEdits() {
    if (!pendingEdits.length) {
        Swal.fire('Nothing to save', '', 'info');
        return;
    }
    fetch(baseUrl + 'Projecttargetrolling/UpsertEdits', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(pendingEdits)
    })
        .then(r => r.json())
        .then(res => {
            if (res.success) {
                pendingEdits.length = 0;
                $('#rollingPlanTable td.editable.dirty').removeClass('dirty');
                Swal.fire('Saved!', 'Changes have been applied.', 'success');
            } else {
                Swal.fire('Error', res.message || 'Save failed', 'error');
            }
        })
        .catch(e => Swal.fire('Error', e.message, 'error'));
}


//function renderTableFromJson(data, selectedMonths) {
//    const monthLabels = {
//        1: "Jan", 2: "Feb", 3: "Mar",
//        4: "Apr", 5: "May", 6: "Jun",
//        7: "Jul", 8: "Aug", 9: "Sep",
//        10: "Oct", 11: "Nov", 12: "Dec"
//    };

//    let html = `
//        <table id="rollingPlanTable" class="table table-bordered table-striped w-auto">
//            <thead>
//                <tr>
//                    <th>Project Name</th>
//                    <th>Plan Type Name</th>
//                    <th>Year</th>`;

//    // ➕ Header เดือน
//    selectedMonths.forEach(m => {
//        html += `<th colspan="2">${monthLabels[m]}</th>`;
//    });

//    // ➕ Header Total
//    html += `<th colspan="2">Total</th>`;

//    html += `</tr><tr>
//        <th></th><th></th><th></th>`; // dummy cell 3 ช่อง

//    // ➕ Subheader Unit/Value
//    selectedMonths.forEach(() => {
//        html += `<th>Unit</th><th>Value (M)</th>`;
//    });

//    // ➕ Subheader Total
//    html += `<th>Unit</th><th>Value (M)</th>`;

//    html += `</tr></thead><tbody>`;

//    // 🔁 สร้าง rows
//    if (data.length > 0) {
//        data.forEach(row => {
//            html += `<tr>
//                <td>${row.ProjectName ?? ''}</td>
//                <td>${row.PlanTypeName ?? ''}</td>
//                <td>${row.PlanYear ?? ''}</td>`;

//            selectedMonths.forEach(m => {
//                const key = monthLabels[m];
//                html += `<td>${row[`${key}_Unit`] ?? '-'}</td>`;
//                html += `<td>${row[`${key}_Value`] ?? '-'}</td>`;
//            });

//            // ➕ ดึง Total col จาก Model
//            html += `<td>${row.Total_Unit ?? '-'}</td><td>${row.Total_Value ?? '-'}</td>`;

//            html += `</tr>`;
//        });
//    } else {
//        html += `<tr><td colspan="${3 + selectedMonths.length * 2 + 2}" class="text-center">ไม่พบข้อมูล</td></tr>`;
//    }

//    html += `</tbody></table>`;
//    $('#rolling-plan-container').html(html);
//}

function renderSummaryCards(datasum) {
    const container = document.getElementById('cardContainer');
    if (!container || !Array.isArray(datasum)) return;

    container.innerHTML = '';

    datasum.forEach(item => {
        const name = item.PlanTypeName || '';
        const unit = item.Unit?.toLocaleString() || '0';
        const value = item.Value?.toLocaleString(undefined, { minimumFractionDigits: 2 });
        const color = item.ColorClass || 'text-muted';

        const card = document.createElement('div');
        card.className = 'card small-widget flex-shrink-0 company-card clickable-card';
        card.style.minWidth = '300px';

        card.innerHTML = `
            <div class="card o-hidden small-widget">
                <div class="card-body total-project border-b-primary border-2">
                    <span class="f-light f-w-500 f-14">${name}</span>
                    <div class="project-details">
                        <div class="project-counter">
                            <div class="card-body d-flex justify-content-around align-items-center py-0">
                                <div>
                                    <h4 class="fw-semibold mb-0" style="color: ${color}">${unit}</h4>
                                    <div class="text-muted small">Unit</div>
                                </div>
                                <div class="vr mx-3"></div>
                                <div>
                                    <h4 class="fw-semibold mb-0" style="color: ${color}">${value}</h4>
                                    <div class="text-muted small">Value</div>
                                </div>
                            </div>
                        </div>
                        <ul class="bubbles">
                            ${'<li class="bubble"></li>'.repeat(9)}
                        </ul>
                    </div>
                </div>
            </div>
        `;

        container.appendChild(card);
    });
}


$('button:contains("Search")').on('click', function () {
    searchRollingPlanData();
});

initImportExcelHandler();

function initImportExcelHandler() {
    const form = document.getElementById('form-import-project-target-rolling');

    if (!form) return;

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        const fileInput = document.getElementById('excelFile');
        if (!fileInput || !fileInput.files.length) {
            Swal.fire('No File', 'Please select a file to upload.', 'warning');
            return;
        }

        const formData = new FormData();
        formData.append('file', fileInput.files[0]);

        fetch(baseUrl + 'Projecttargetrolling/ImportExcel', {
            method: 'POST',
            body: formData
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    Swal.fire('Success'
                             /*, `Imported ${res.count} rows successfully.`*/
                             , `${res.message}`
                             , 'success');
                    const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById('modalImportTargetRolling'));
                    modal.hide();

                    // ⏬ reload function ถ้ามีตาราง
                    // loadTableProjectAndTargetRolling(); 
                } else {
                    Swal.fire('Error', 'Something went wrong during import.', 'error');
                }
            })
            .catch(err => {
                console.error(err);
                Swal.fire('Error', 'An error occurred.', 'error');
            });
    });
}


window.exportExcelProjectAndTargetRolling = function () {
    const filter = collectRollingPlanFilters();

    // ⛳ ตรวจสอบเดือนที่เลือก
    let selectedMonths = $('#ddl_month').val()?.map(Number) || [];

    // ❗ ถ้าไม่ได้เลือกเดือน → ใช้ quarter แทน
    if (selectedMonths.length === 0) {
        const selectedQuarters = choicesQuarter.getValue(true); // ['Q1', 'Q2',...]
        selectedMonths = selectedQuarters.flatMap(q => monthMap[q] || []);
    }

    // 🧼 ถ้ายังไม่มีอะไรเลย → โชว์ทุกเดือน
    if (selectedMonths.length === 0) {
        selectedMonths = Array.from({ length: 12 }, (_, i) => i + 1);
    }

    // ส่งค่า filter ไปยัง API
    const formData = new FormData();
    for (const key in filter) {
        formData.append(key, filter[key]);
    }

    fetch(baseUrl + 'Projecttargetrolling/ExportProjectAndTargetRolling', {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Excel export failed.");
            }
            return response.blob();
        })
        .then(blob => {
            const url = window.URL.createObjectURL(blob);
            const link = document.createElement('a');
            link.href = url;
            const fileName = `ProjectAndTargetRollin_${new Date().toISOString().slice(0, 10)}.xlsx`;
            link.download = fileName;
            document.body.appendChild(link);
            link.click();
            link.remove();
        })
        .catch(error => {
            Swal.fire("Error", error.message, "error");
        });
};
