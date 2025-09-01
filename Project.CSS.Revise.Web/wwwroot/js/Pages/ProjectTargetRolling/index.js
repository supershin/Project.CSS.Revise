// ===== Edit mode globals =====
let isEditMode = false;                       // toggle by Edit/Cancel
/*const pendingEdits = window.pendingEdits || []; // keep your array*/

// format helpers
const toLocaleInt = n => Number(n).toLocaleString(undefined, { maximumFractionDigits: 0 });
const toLocaleMoney = n => Number(n).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });

// mimic your C# ConvertToShortUnit for display mode (K/M)
function formatShort(n) {
    if (n == null || isNaN(n)) return '';
    const x = Number(n);
    const abs = Math.abs(x);
    let out;
    if (abs >= 1_000_000) out = (x / 1_000_000).toFixed(2);
    else if (abs >= 1_000) out = (x / 1_000).toFixed(2);
    else out = x.toFixed(2);
    return out.replace(/\.?0+$/, ''); // trim .00 / trailing zeros
}

// sanitize pasted numeric text
function sanitizeNumericText(t) {
    if (t == null) return '';
    const plain = String(t).replace(/<[^>]*>/g, '');
    const cleaned = plain.replace(/[^\d.\-]/g, '');
    let sign = cleaned.startsWith('-') ? '-' : '';
    let body = cleaned.replace(/^-/, '');
    let parts = body.split('.');
    if (parts.length > 2) body = parts.shift() + '.' + parts.join('');
    return sign + body;
}


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

function initProjectstatusDropdown() {
    choicesBug = new Choices('#ddl_project_status', {
        removeItemButton: true,
        placeholderValue: 'เลือกสถานะโครงการได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });

    document.getElementById('ddl_project_status').addEventListener('change', onBuChanged);
}

function initProjectpartnerDropdown() {
    const ddlprojectpartner = document.getElementById('ddl_project_partner');
    if (ddlprojectpartner) {
        new Choices(ddlprojectpartner, {
            removeItemButton: true,
            searchEnabled: true,
            placeholderValue: 'ทั้งหมด',     // ✅ เพิ่ม placeholder ตรงนี้
            noResultsText: 'ไม่พบ Partner',
            noChoicesText: 'ไม่มีตัวเลือก',
            shouldSort: false
        });
    }
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
    initProjectstatusDropdown();
    /*initProjectpartnerDropdown();*/
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
        L_PlanTypeName: $('#ddl_plantype option:selected').map(function () {
            return $(this).text();
        }).get().join(','), // ✅ ดึงเฉพาะ text ของตัวที่เลือก
        L_Bu: $('#ddl_bug').val() ? $('#ddl_bug').val().join(',') : '',
        L_ProjectID: $('#ddl_project').val() ? $('#ddl_project').val().join(',') : '',
        L_ProjectStatus: $('#ddl_project_status').val() ? $('#ddl_project_status').val().join(',') : '',
        L_ProjectPartner: $('#ddl_project_partner').val()
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

searchRollingPlanData();

const pendingEdits = []; // {ProjectID, PlanTypeID, Year, Month, PlanAmountID, OldValue, NewValue}

function renderTableFromJson(data, selectedMonths) {
    const dec = s => (s ?? '').toString().replace(/,/g, ''); // "10,760,000.00" -> "10760000.00"

    let html = `
    <table id="rollingPlanTable" class="table table-bordered table-striped w-auto">
      <thead>
        <tr>
          <th>Project</th>
          <th>Bu</th>
          <th>Plan Type</th>
          <th>Year</th>`;

    selectedMonths.forEach(m => {
        html += `<th colspan="2">${monthLabels[m]}</th>`;
    });

    html += `<th colspan="2">Total</th>`;

    html += `</tr><tr>
      <th></th><th></th><th></th><th></th>`;

    selectedMonths.forEach(() => {
        html += `<th>Unit</th><th>Value (M)</th>`;
    });

    html += `<th>Unit</th><th>Value (M)</th></tr></thead><tbody>`;

    if (Array.isArray(data) && data.length) {
        data.forEach(row => {
            const pid = row.ProjectID ?? '';
            const ptypeId = row.PlanTypeID ?? 0;
            const year = Number(row.PlanYear ?? 0);

            html += `<tr data-projectid="${pid}" data-plantypeid="${ptypeId}" data-year="${year}">
                        <td>${row.ProjectName ?? ''}</td>
                        <td>${row.BuName ?? ''}</td>
                        <td>${row.PlanTypeName ?? ''}</td>
                        <td>${row.PlanYear ?? ''}</td>
                    `;

            selectedMonths.forEach(m => {
                const key = monthLabels[m]; // e.g. "Jan"

                // short display (1, 1.2, etc.)
                const unitShort = row[`${key}_Unit`] ?? '';
                const valueShort = row[`${key}_Value`] ?? '';

                // full with commas ("10,760,000.00"), convert to RAW numeric string for editing
                const unitComma = row[`${key}_Unit_comma`] ?? '';
                const valueComma = row[`${key}_Value_comma`] ?? '';

                const unitRaw = dec(unitComma);   // "10760000.00"
                const valueRaw = dec(valueComma);  // "10760000.00"

                html += `
                          <td class="editable"
                              contenteditable="false"
                              data-field="${key}_Unit"
                              data-month="${m}"
                              data-planamountid="183"
                              data-raw="${unitRaw}"
                              data-old="${unitRaw}">${unitShort}</td>

                          <td class="editable"
                              contenteditable="false"
                              data-field="${key}_Value"
                              data-month="${m}"
                              data-planamountid="184"
                              data-raw="${valueRaw}"
                              data-old="${valueRaw}">${valueShort}</td>`;
                            });

            // Totals (show short, keep raw on attrs too for later if needed)
            const totalUnitShort = row.Total_Unit ?? '-';
            const totalValueShort = row.Total_Value ?? '-';
            const totalUnitRaw = dec(row.Total_Unit_comma ?? '');
            const totalValueRaw = dec(row.Total_Value_comma ?? '');

            html += `
                        <td class="total-unit"  data-raw="${totalUnitRaw}">${totalUnitShort}</td>
                        <td class="total-value" data-raw="${totalValueRaw}">${totalValueShort}</td>
                      </tr>
                    `;
        });
    } else {
        html += `<tr><td colspan="${3 + selectedMonths.length * 2 + 2}" class="text-center">ไม่พบข้อมูล</td></tr>`;
    }

    html += `</tbody></table>`;
    $('#rolling-plan-container').html(html);

    // (Re)bind editing behaviors
    bindEditableHandlers();

    // Initial totals recompute (uses data-raw if your recompute function reads from data-raw)
    $('#rollingPlanTable tbody tr').each(function () {
        recomputeRowTotals($(this));
    });
}

function bindEditableHandlers() {
    const table = $('#rollingPlanTable');

    // highlight row when any editable cell focused
    table.on('focus', 'td.editable', function () {
        if (!isEditMode) { this.blur(); return; }
        $(this).closest('tr').addClass('row-editing');
    });
    table.on('blur', 'td.editable', function () {
        $(this).closest('tr').removeClass('row-editing');
    });

    // Restrict input to numeric (allow dot, minus, backspace, arrows, tab, delete)
    table.on('keydown', 'td.editable', function (e) {
        if (!isEditMode) { e.preventDefault(); return; }
        const meta = e.ctrlKey || e.metaKey;
        const allowedKeys = [
            8, 9, 13, 27, 37, 38, 39, 40, 46, // control keys
            110, 190, 189                      // dot (numpad/main), minus
        ];
        if (allowedKeys.includes(e.keyCode) || (meta && ['a', 'c', 'v', 'x', 'z', 'y'].includes(e.key.toLowerCase()))) return;
        if (e.key >= '0' && e.key <= '9') return;
        e.preventDefault();
    });

    // Enter commits (blur)
    table.on('keydown', 'td.editable', function (e) {
        if (!isEditMode) return;
        if (e.key === 'Enter') { e.preventDefault(); $(this).blur(); }
    });

    // Paste sanitize → numeric only
    table.on('paste', 'td.editable', function (e) {
        if (!isEditMode) { e.preventDefault(); return; }
        e.preventDefault();
        const clipboard = (e.originalEvent || e).clipboardData.getData('text') || '';
        const cleaned = sanitizeNumericText(clipboard);
        document.execCommand('insertText', false, cleaned);
    });

    // Detect changes while typing (real-time)
    table.on('input', 'td.editable', function () {
        if (!isEditMode) return;
        const $cell = $(this);
        let newVal = $cell.text().replace(/,/g, '').trim();
        const oldVal = ($cell.data('raw') ?? '').toString(); // compare against RAW

        if (newVal !== '' && !/^-?\d*\.?\d*$/.test(newVal)) {
            // Not numeric → revert to previous formatted RAW
            const pid = Number($cell.data('planamountid')); // 183 / 184
            const prev = $cell.data('raw');
            $cell.text(prev == null || prev === '' ? '' : (pid === 184 ? toLocaleMoney(prev) : toLocaleInt(prev)));
            return;
        }
        $cell.toggleClass('dirty', newVal !== oldVal);
    });

    // Commit on blur: update data-raw, format display for edit mode, stage change
    table.on('blur', 'td.editable', function () {
        if (!isEditMode) return;

        const $cell = $(this);
        let newVal = $cell.text().replace(/,/g, '').trim();
        if (newVal === '' || newVal === '-') newVal = '';

        // invalid number → revert
        if (newVal !== '' && isNaN(newVal)) {
            const pid = Number($cell.data('planamountid'));
            const prev = $cell.data('raw');
            $cell.text(prev == null || prev === '' ? '' : (pid === 184 ? toLocaleMoney(prev) : toLocaleInt(prev)));
            $cell.removeClass('dirty');
            return;
        }

        const oldRawStr = ($cell.data('raw') ?? '').toString();
        if (oldRawStr === newVal) { $cell.removeClass('dirty'); return; }

        const newRaw = newVal === '' ? null : Number(newVal);
        const oldRaw = oldRawStr === '' ? null : Number(oldRawStr);

        const $row = $cell.closest('tr');
        const payload = {
            ProjectID: $row.data('projectid'),
            PlanTypeID: Number($row.data('plantypeid') || 0),
            Year: Number($row.data('year') || 0),
            Month: Number($cell.data('month')),
            PlanAmountID: Number($cell.data('planamountid')),
            OldValue: oldRaw,
            NewValue: newRaw
        };

        // render formatted RAW (we are in edit mode)
        const pid = payload.PlanAmountID;
        $cell.text(
            newRaw == null ? '' : (pid === 184 ? toLocaleMoney(newRaw) : toLocaleInt(newRaw))
        );

        // keep RAW in data- attributes for later save / cancel
        $cell.data('raw', newRaw == null ? '' : newRaw);
        $cell.removeClass('dirty');

        upsertPendingEdit(payload);
        recomputeRowTotals($row);

        // ❌ No autosave & no success Swal here (you said confirm before save later)
        savePendingEdits(); // <-- disable for now
    });
}

function setEditMode(on) {
    isEditMode = !!on;

    // toggle buttons
    $('#btnEdit').toggleClass('d-none', isEditMode);
    $('#btnCancelEdit').toggleClass('d-none', !isEditMode);

    const $table = $('#rollingPlanTable');
    if (!$table.length) return;

    // switch each editable cell between short display and RAW display
    $table.find('td.editable').each(function () {
        const $cell = $(this);
        const raw = $cell.data('raw');
        const pid = Number($cell.data('planamountid')); // 183 Unit, 184 Value

        if (isEditMode) {
            // Show RAW with thousand separators; enable editing
            const txt = (raw == null || raw === '') ? '' : (pid === 184 ? toLocaleMoney(raw) : toLocaleInt(raw));
            $cell.text(txt);
            this.setAttribute('contenteditable', 'true');
        } else {
            // Back to short; disable editing
            const shortTxt = (raw == null || raw === '') ? '' : formatShort(raw);
            $cell.text(shortTxt);
            this.setAttribute('contenteditable', 'false');
            $cell.removeClass('dirty row-editing');
        }
    });

    // refresh totals with current mode
    $table.find('tbody tr').each(function () { recomputeRowTotals($(this)); });
    $table.toggleClass('editing-active', isEditMode);
}

function cancelEditMode() {
    // discard staged changes
    pendingEdits.length = 0;
    setEditMode(false);
}

// --- Busy overlay (minimal) ---
const Busy = (() => {
    let $el, $msg;
    function ensure() {
        if (document.getElementById('uiBusy')) { $el = $('#uiBusy'); $msg = $el.find('.msg'); return; }
        // style (very small)
        const css = `
      .ui-busy{position:fixed;inset:0;display:flex;align-items:center;justify-content:center;
        background:rgba(255,255,255,.6);backdrop-filter:saturate(180%) blur(2px);z-index:2000}
      .ui-busy.d-none{display:none}
    `;
        const s = document.createElement('style'); s.textContent = css; document.head.appendChild(s);
        // overlay
        $('body').append(`
      <div id="uiBusy" class="ui-busy d-none" aria-live="polite" aria-busy="true">
        <div class="text-center">
          <div class="spinner-border" role="status" aria-label="Loading"></div>
          <div class="small text-muted mt-2 msg">Loading…</div>
        </div>
      </div>
    `);
        $el = $('#uiBusy'); $msg = $el.find('.msg');
    }
    function show(text = 'Loading…') { ensure(); $msg.text(text); $el.removeClass('d-none'); }
    function hide() { if ($el) $el.addClass('d-none'); }
    async function withBusy(fn, text = 'Loading…', minMs = 250) {
        show(text);
        await new Promise(r => setTimeout(r, 0)); // let overlay paint
        const t0 = performance.now();
        try {
            const out = await Promise.resolve().then(fn);
            const elapsed = performance.now() - t0;
            const wait = Math.max(0, minMs - elapsed);
            setTimeout(hide, wait);
            return out;
        } catch (e) {
            hide(); throw e;
        }
    }
    return { show, hide, with: withBusy };
})();


// wire buttons
$(document).on('click', '#btnEdit', () =>
    Busy.with(() => setEditMode(true), 'Entering edit mode…', 250)
);

$(document).on('click', '#btnCancelEdit', () =>
    Busy.with(() => cancelEditMode(), 'Reverting changes…', 250)
);



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

function recomputeRowTotals($row) {
    let totalUnit = 0, totalValue = 0;

    $row.find('td.editable').each(function () {
        const raw = $(this).data('raw');
        if (raw === '' || raw == null || isNaN(raw)) return;
        const n = Number(raw);
        const pid = Number($(this).data('planamountid')); // 183 unit / 184 value
        if (pid === 183) totalUnit += n; else if (pid === 184) totalValue += n;
    });

    // In edit mode show full numbers; in view mode show short (K/M)
    $row.find('td.total-unit').text(
        isEditMode ? (totalUnit ? toLocaleInt(totalUnit) : '-') : (totalUnit ? formatShort(totalUnit) : '-')
    );
    $row.find('td.total-value').text(
        isEditMode ? (totalValue ? toLocaleMoney(totalValue) : '-') : (totalValue ? formatShort(totalValue) : '-')
    );
}


function savePendingEdits() {
    if (!pendingEdits.length) {
        Swal.fire('Nothing to save', '', 'info');
        return;
    }

    Swal.fire({
        title: 'Confirm Save',
        text: 'Do you want to save the changes?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes, save it',
        cancelButtonText: 'Cancel'
    }).then(result => {
        if (!result.isConfirmed) return;

        // take a snapshot so we can commit UI precisely
        const batch = pendingEdits.splice(0, pendingEdits.length);

        fetch(baseUrl + 'Projecttargetrolling/UpsertEdits', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(batch)
        })
            .then(r => r.json())
            .then(res => {
                if (res.success) {
                    // commit UI: set data-old to NewValue and clear dirty flags
                    batch.forEach(ch => {
                        const sel = `tr[data-projectid="${ch.ProjectID}"][data-plantypeid="${ch.PlanTypeID}"][data-year="${ch.Year}"] td.editable[data-month="${ch.Month}"][data-planamountid="${ch.PlanAmountID}"]`;
                        const $cell = $(sel);
                        const committed = ch.NewValue == null ? '' : ch.NewValue;
                        $cell.data('old', committed);
                        $cell.removeClass('dirty');
                    });

                    // ✅ refresh summary cards from server
                    refreshSummaryCards();

                    // top-right toast
                    Swal.fire({
                        toast: true,
                        position: 'top-end',
                        icon: 'success',
                        title: 'Changes saved successfully',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true
                    });
                } else {
                    // put them back so user can retry
                    pendingEdits.unshift(...batch);
                    Swal.fire('Error', res.message || 'Save failed', 'error');
                }
            })
            .catch(e => {
                pendingEdits.unshift(...batch);
                Swal.fire('Error', e.message || 'Save failed', 'error');
            });
    });
}


function refreshSummaryCards() {
    const filter = collectRollingPlanFilters();
    const formData = new FormData();
    for (const k in filter) formData.append(k, filter[k]);

    // reuse the same endpoint; we'll only use `datasum`
    fetch(baseUrl + 'Projecttargetrolling/GetDataTableProjectAndTargetRolling', {
        method: 'POST',
        body: formData
    })
        .then(r => r.json())
        .then(json => {
            if (json?.success) renderSummaryCards(json.datasum || []);
        })
        .catch(() => {/* silently ignore */ });
}


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
        card.style.minWidth = '75px';

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
                                    <h4 class="fw-semibold mb-0" style="color: ${color}">${value}</h4>
                                    <div class="text-muted small">Value</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;

        container.appendChild(card);
    });
}


$('button:contains("Search")').on('click', function () {
    // Always reset button state before searching
    $('#btnEdit').removeClass('d-none');
    $('#btnCancelEdit').addClass('d-none');
    searchRollingPlanData();
});


initImportExcelHandler();

function initImportExcelHandler() {
    const form = document.getElementById('form-import-project-target-rolling');
    if (!form) return;

    const modalEl = document.getElementById('modalImportTargetRolling');
    const submitBtn = form.querySelector('button[type="submit"]');
    const fileInput = document.getElementById('excelFile');

    const setImportLoading = (on) => {
        if (!submitBtn) return;
        submitBtn.disabled = on;
        if (on) {
            submitBtn.dataset.originalHtml = submitBtn.innerHTML;
            submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>Uploading...';
        } else {
            submitBtn.innerHTML = submitBtn.dataset.originalHtml || '<i class="fa fa-upload me-1"></i> Upload & Save';
        }
    };

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        if (!fileInput || !fileInput.files.length) {
            Swal.fire('No File', 'Please select a file to upload.', 'warning');
            return;
        }

        const formData = new FormData();
        formData.append('file', fileInput.files[0]);

        // start loading
        setImportLoading(true);
        if (typeof showLoading === 'function') showLoading();

        fetch(baseUrl + 'Projecttargetrolling/ImportExcel', {
            method: 'POST',
            body: formData
        })
            .then(res => res.json())
            .then(res => {
                if (res.success) {
                    // success toast (top-right) – optional: keep your modal version if you prefer
                    Swal.fire({
                        toast: true,
                        position: 'top-end',
                        icon: 'success',
                        title: res.message || 'Imported successfully',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true
                    });

                    // close modal
                    const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
                    modal.hide();

                    // ✅ clear file input & form
                    form.reset();
                    // also clear the file input explicitly for older browsers
                    fileInput.value = '';

                    // (optional) refresh table & summary after import
                    // searchRollingPlanData();
                } else {
                    Swal.fire('Error', res.message || 'Something went wrong during import.', 'error');
                }
            })
            .catch(err => {
                console.error(err);
                Swal.fire('Error', 'An error occurred.', 'error');
            })
            .finally(() => {
                setImportLoading(false);
                if (typeof hideLoading === 'function') hideLoading();
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
            if (!response.ok) throw new Error("Excel export failed.");

            // read filename from Content-Disposition
            const dispo = response.headers.get('content-disposition') || "";
            let serverFileName = "export.xlsx";
            const m = dispo.match(/filename\*=UTF-8''([^;]+)|filename="?([^"]+)"?/i);
            if (m) serverFileName = decodeURIComponent(m[1] || m[2]);

            return response.blob().then(blob => ({ blob, serverFileName }));
        })
        .then(({ blob, serverFileName }) => {
            const url = window.URL.createObjectURL(blob);
            const link = document.createElement('a');
            link.href = url;
            link.download = serverFileName;   // ✅ use name returned by IActionResult
            document.body.appendChild(link);
            link.click();
            link.remove();
            setTimeout(() => URL.revokeObjectURL(url), 0);
        })
        .catch(error => {
            Swal.fire("Error", error.message, "error");
        });

};
