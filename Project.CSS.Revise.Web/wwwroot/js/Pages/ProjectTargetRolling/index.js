/*************************************************
 * Project Target & Rolling — Page Script (clean)
 *************************************************/

/* ===================== Helpers ===================== */

// number formatting
const toLocaleInt = (n) => {
    const num = Number(n);
    return isNaN(num) ? '0' : num.toLocaleString(undefined, { maximumFractionDigits: 0 });
};

const toLocaleMoney = (n) => {
    const num = Number(n);
    return isNaN(num) ? '0.00' : num.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
};

// short K/M formatting (view mode)
function formatShort(n) {
    if (n == null || isNaN(n)) return '';
    const x = Number(n), abs = Math.abs(x);
    let out;
    if (abs >= 1_000_000) out = (x / 1_000_000).toFixed(2);
    else if (abs >= 1_000) out = (x / 1_000).toFixed(2);
    else out = x.toFixed(2);
    return out.replace(/\.?0+$/, '');
}
function formatShortHaveZero(n) {
    if (n == null || isNaN(n)) return '';
    const x = Number(n), abs = Math.abs(x);
    if (abs >= 1_000_000) return (x / 1_000_000).toFixed(2);
    if (abs >= 1_000) return (x / 1_000).toFixed(2);
    return x.toFixed(2);
}

// replace your sanitizeNumericText with this version
function sanitizeNumericText(t) {
    if (t == null) return '';
    const plain = String(t).replace(/<[^>]*>/g, '');
    // keep digits, dot, minus; collapse to a single dot; keep at most one leading minus
    const cleaned = plain.replace(/[^\d.\-]/g, '');
    const sign = cleaned.trim().startsWith('-') ? '-' : '';
    let body = cleaned.replace(/^-/, '');
    const parts = body.split('.');
    if (parts.length > 2) body = parts.shift() + '.' + parts.join('');
    return sign + body;
}


// small utils for Choices
const getChoicesVals = (choicesInstance) => {
    try { return (choicesInstance?.getValue(true) || []).filter(Boolean); }
    catch { return []; }
};

// months
const monthMap = { Q1: [1, 2, 3], Q2: [4, 5, 6], Q3: [7, 8, 9], Q4: [10, 11, 12] };
const monthLabels = { 1: "Jan", 2: "Feb", 3: "Mar", 4: "Apr", 5: "May", 6: "Jun", 7: "Jul", 8: "Aug", 9: "Sep", 10: "Oct", 11: "Nov", 12: "Dec" };

/* ============== Busy overlay (tiny) ============== */

const Busy = (() => {
    let $el, $msg;
    function ensure() {
        if (document.getElementById('uiBusy')) { $el = $('#uiBusy'); $msg = $el.find('.msg'); return; }
        const css = `
      .ui-busy{position:fixed;inset:0;display:flex;align-items:center;justify-content:center;
        background:rgba(255,255,255,.6);backdrop-filter:saturate(180%) blur(2px);z-index:2000}
      .ui-busy.d-none{display:none}`;
        const s = document.createElement('style'); s.textContent = css; document.head.appendChild(s);
        $('body').append(`
      <div id="uiBusy" class="ui-busy d-none" aria-live="polite" aria-busy="true">
        <div class="text-center">
          <div class="spinner-border" role="status" aria-label="Loading"></div>
          <div class="small text-muted mt-2 msg">Loading…</div>
        </div>
      </div>`);
        $el = $('#uiBusy'); $msg = $el.find('.msg');
    }
    function show(text = 'Loading…') { ensure(); $msg.text(text); $el.removeClass('d-none'); }
    function hide() { if ($el) $el.addClass('d-none'); }
    async function withBusy(fn, text = 'Loading…', minMs = 250) {
        show(text); await new Promise(r => setTimeout(r, 0));
        const t0 = performance.now();
        try {
            const out = await Promise.resolve().then(fn);
            const wait = Math.max(0, minMs - (performance.now() - t0)); setTimeout(hide, wait); return out;
        } catch (e) { hide(); throw e; }
    }
    return { show, hide, with: withBusy };
})();

/* ===================== Choices ===================== */

let choicesQuarter, choicesMonth, choicesBu, choicesProjectStatus, choicesProjectPartner, choicesProject;
// ===== put these with other globals =====
let choicesYear, choicesPlanType;

// ===== REPLACE your initYearDropdown with this =====
function initYearDropdown() {
    const ddl = document.getElementById('ddl_year');
    if (!ddl) return;

    // destroy old Choices instance if exists
    if (choicesYear && typeof choicesYear.destroy === 'function') {
        choicesYear.destroy();
    }

    const currentYear = new Date().getFullYear();

    // rebuild options
    ddl.innerHTML = '';
    for (let y = currentYear - 3; y <= currentYear + 3; y++) {
        const opt = document.createElement('option');
        opt.value = String(y);
        opt.text = String(y);
        ddl.appendChild(opt);
    }

    // re-init Choices
    choicesYear = new Choices('#ddl_year', {
        removeItemButton: true,
        placeholderValue: 'Select one or more years',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });

    // ensure ONLY the current year is selected
    try {
        choicesYear.removeActiveItems();
        choicesYear.setChoiceByValue(String(currentYear));
    } catch { }
}


function initQuarterDropdown() {
    choicesQuarter = new Choices('#ddl_quarter', { removeItemButton: true, placeholderValue: 'Select one or more quarters', shouldSort: false });
    document.getElementById('ddl_quarter').addEventListener('change', () => updateMonthDropdown(choicesQuarter.getValue(true)));
}

function initMonthDropdown() {
    choicesMonth = new Choices('#ddl_month', { removeItemButton: true, placeholderValue: 'Select one or more months', shouldSort: false });
    updateMonthDropdown([]);
}
function updateMonthDropdown(selectedQuarters) {
    let allowed = [];
    if (!selectedQuarters || selectedQuarters.length === 0) {
        allowed = Array.from({ length: 12 }, (_, i) => i + 1);
    } else {
        selectedQuarters.forEach(q => allowed = allowed.concat(monthMap[q] || []));
        allowed = [...new Set(allowed)].sort((a, b) => a - b);
    }
    choicesMonth.clearStore();
    choicesMonth.setChoices(allowed.map(m => ({ value: m, label: monthLabels[m] })), 'value', 'label', true);
}

// ===== REPLACE your initPlanTypeDropdown with this =====
function initPlanTypeDropdown() {
    // destroy old if exists
    if (choicesPlanType && typeof choicesPlanType.destroy === 'function') {
        choicesPlanType.destroy();
    }
    choicesPlanType = new Choices('#ddl_plantype', {
        removeItemButton: true,
        placeholderValue: 'Select one or more plan types',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });
}

function initBuDropdown() {
    choicesBu = new Choices('#ddl_bug', { removeItemButton: true, placeholderValue: 'Select one or more BUGs', searchEnabled: true, itemSelectText: '', shouldSort: false });
    document.getElementById('ddl_bug').addEventListener('change', onFilterChanged);
}
function initProjectstatusDropdown() {
    const el = document.getElementById('ddl_project_status');
    if (!el) return;

    // ทำลาย Choices เดิมถ้ามี
    if (choicesProjectStatus && typeof choicesProjectStatus.destroy === 'function') {
        choicesProjectStatus.destroy();
    }

    // สร้าง Choices ใหม่
    choicesProjectStatus = new Choices(el, {
        removeItemButton: true,
        placeholderValue: 'Select one or more project statuses',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });

    // ✅ ตั้งค่า default ให้เลือกค่า 283 (RTM) และ 371 (Finish)
    try {
        choicesProjectStatus.removeActiveItems(); // เคลียร์ก่อน
        choicesProjectStatus.setChoiceByValue(['283', '371']); // เลือกหลายค่า
    } catch (err) {
        console.warn('Failed to set default project statuses:', err);
    }

    // ผูก event
    el.addEventListener('change', onFilterChanged);
}

function initProjectpartnerDropdown() {
    const el = document.getElementById('ddl_project_partner'); if (!el) return;
    choicesProjectPartner = new Choices(el, { removeItemButton: false, searchEnabled: true, placeholder: true, placeholderValue: 'All', shouldSort: false });
    // ensure blank default
    choicesProjectPartner.removeActiveItems();
    if (![...el.options].some(o => o.value === '')) {
        choicesProjectPartner.setChoices([{ value: '', label: 'All', selected: true }], 'value', 'label', false);
    } else {
        choicesProjectPartner.setChoiceByValue('');
    }
    el.addEventListener('change', onFilterChanged);
}

let choicesShowType;

function initAllProjectCheckbox() {
    const el = document.getElementById('chk_all_project');
    if (!el) return;

    // default = ไม่ติ๊ก
    el.checked = false;

    // เปลี่ยนแล้วให้รีเฟรชรายการโปรเจ็กต์ (ถ้าต้องการ)
    el.addEventListener('change', onFilterChanged);
}


function initProjectDropdown() {
    choicesProject = new Choices('#ddl_project', { removeItemButton: true, placeholderValue: 'Select one or more projects', searchEnabled: true, itemSelectText: '', shouldSort: false });
    loadProjectFromFilters();
}

function onFilterChanged() { loadProjectFromFilters(); }

/* ============ Load project list by filters ============ */

let projectReqAbort;
function loadProjectFromFilters() {
    const selectedBUs = getChoicesVals(choicesBu);
    const selectedStatuses = getChoicesVals(choicesProjectStatus);

    let partnerVal = '';
    try {
        partnerVal = choicesProjectPartner?.getValue(true) ?? '';
        if (typeof partnerVal !== 'string') partnerVal = String(partnerVal ?? '');
    } catch { partnerVal = ''; }

    const fd = new FormData();
    fd.append('L_BUID', selectedBUs.length ? selectedBUs.join(',') : '');
    fd.append('L_ProjectStatus', selectedStatuses.length ? selectedStatuses.join(',') : '');
    fd.append('L_ProjectPartner', partnerVal || '');

    try { projectReqAbort?.abort(); } catch { }
    projectReqAbort = new AbortController();

    fetch(baseUrl + 'Projecttargetrolling/GetProjectListByBU', { method: 'POST', body: fd, signal: projectReqAbort.signal })
        .then(r => r.json())
        .then(json => {
            const list = json?.data || [];
            choicesProject.clearStore();
            if (!json?.success || list.length === 0) {
                choicesProject.setChoices([{ value: '', label: '— No projects —', disabled: true }], 'value', 'label', true);
                return;
            }
            choicesProject.setChoices(list.map(p => ({ value: p.ProjectID, label: p.ProjectNameTH })), 'value', 'label', true);
        })
        .catch(err => {
            console.error('Load project failed:', err);
            choicesProject.clearStore();
            choicesProject.setChoices([{ value: '', label: 'Failed to load projects', disabled: true }], 'value', 'label', true);
        });
}

/* ===================== Filters ===================== */

function collectRollingPlanFilters() {
    const allProjectChecked = document.getElementById('chk_all_project')?.checked;
    return {
        L_Year: ($('#ddl_year').val() || []).join(','),
        L_Quarter: (choicesQuarter?.getValue(true) || []).join(','),
        L_Month: (choicesMonth?.getValue(true) || []).join(','),
        L_PlanTypeID: ($('#ddl_plantype').val() || []).join(','),
        L_PlanTypeName: $('#ddl_plantype option:selected').map(function () { return $(this).text(); }).get().join(','),
        L_Bu: ($('#ddl_bug').val() || []).join(','),
        L_ProjectID: ($('#ddl_project').val() || []).join(','),
        L_ProjectStatus: ($('#ddl_project_status').val() || []).join(','),
        L_ProjectPartner: $('#ddl_project_partner').val() || '',
        /*L_Act: $('#ddl_showtype').val() || ''*/
        L_Act: allProjectChecked ? 'GetListTargetRollingPlan' : 'GetListTargetRollingPlanCuttoltal'
    };
}

/* ===================== Data Table ===================== */

let isEditMode = false;
const pendingEdits = []; // {ProjectID, PlanTypeID, Year, Month, PlanAmountID(183/184), OldValue, NewValue}

const $btnEdit = $('#btnEdit');
const $btnSave = $('#btnSaveEdit');
const $btnCancel = $('#btnCancelEdit');

function setEditMode(on) {
    isEditMode = !!on;

    $btnEdit.toggleClass('d-none', isEditMode);
    $btnSave.toggleClass('d-none', !isEditMode).prop('disabled', pendingEdits.length === 0);
    $btnCancel.toggleClass('d-none', !isEditMode);

    const $table = $('#rollingPlanTable'); if (!$table.length) return;

    $table.find('td.editable, td.actualrow').each(function () {
        const $cell = $(this);
        const raw = $cell.data('raw');
        const pid = Number($cell.data('planamountid')); // 183=Unit, 184=Value
        if (isEditMode) {
            const txt = (raw == null || raw === '') ? '' : (pid === 184 ? toLocaleMoney(raw) : toLocaleInt(raw));
            $cell.text(txt);
            this.setAttribute('contenteditable', $cell.hasClass('editable') ? 'true' : 'false');
        } else {
            const txt = (raw == null || raw === '') ? '' : (pid === 184 ? formatShortHaveZero(raw) : formatShort(raw));
            $cell.text(txt);
            this.setAttribute('contenteditable', 'false');
            if ($cell.hasClass('editable')) $cell.removeClass('dirty row-editing');
        }
    });

    $table.find('tbody tr').each(function () { recomputeRowTotals($(this)); });
}

function cancelEdits() {
    const $table = $('#rollingPlanTable'); if (!$table.length) return;

    $table.find('td.editable').each(function () {
        const $cell = $(this);
        const pid = Number($cell.data('planamountid'));
        const oldRawStr = ($cell.data('old') ?? '').toString();
        const oldRaw = oldRawStr === '' ? null : Number(oldRawStr);

        if (oldRaw == null) $cell.text('');
        else $cell.text(pid === 184 ? toLocaleMoney(oldRaw) : toLocaleInt(oldRaw));

        $cell.data('raw', oldRaw == null ? '' : oldRaw);
        $cell.removeClass('dirty');
    });

    pendingEdits.length = 0;
    $('#rollingPlanTable tbody tr').each(function () { recomputeRowTotals($(this)); });

    setEditMode(false);
}

function renderTableFromJson(data, selectedMonths) {
    const dec = s => (s ?? '').toString().replace(/,/g, '');

    let html = `
  <table id="rollingPlanTable" class="table table-bordered table-striped w-auto">
    <thead>
      <tr>
        <th>Project</th>
        <th>Bug</th>
        <th>Plan Type</th>
        <th>Year</th>`;
    selectedMonths.forEach(m => { html += `<th colspan="2">${monthLabels[m]}</th>`; });
    html += `<th colspan="2">Total</th></tr><tr>
        <th></th><th></th><th></th><th></th>`;
    selectedMonths.forEach(() => { html += `<th>Unit</th><th>Value (M)</th>`; });
    html += `<th>Unit</th><th>Value (M)</th></tr></thead><tbody>`;

    if (Array.isArray(data) && data.length) {
        data.forEach(row => {
            const pid = row.ProjectID ?? '';
            const ptypeId = row.PlanTypeID ?? 0;
            const year = Number(row.PlanYear ?? 0);
            const isActual = String(row.PlanTypeName ?? '').trim().toLowerCase() === 'actual';

            html += `<tr data-projectid="${pid}" data-plantypeid="${ptypeId}" data-year="${year}">
        <td>${row.ProjectName ?? ''}</td>
        <td>${row.BuName ?? ''}</td>
        <td>${row.PlanTypeName ?? ''}</td>
        <td>${row.PlanYear ?? ''}</td>`;

            selectedMonths.forEach(m => {
                const key = monthLabels[m];
                const unitShort = row[`${key}_Unit`] ?? '';
                const valueShort = row[`${key}_Value`] ?? '';
                const unitRaw = dec(row[`${key}_Unit_comma`] ?? '');
                const valueRaw = dec(row[`${key}_Value_comma`] ?? '');
                const cls = isActual ? 'actualrow' : 'editable';

                html += `
          <td class="${cls} unit-cell"
              contenteditable="false"
              data-field="${key}_Unit"
              data-month="${m}"
              data-planamountid="183"
              data-raw="${unitRaw}"
              data-old="${unitRaw}">${unitShort}</td>

          <td class="${cls} value-cell"
              contenteditable="false"
              data-field="${key}_Value"
              data-month="${m}"
              data-planamountid="184"
              data-raw="${valueRaw}"
              data-old="${valueRaw}">${valueShort}</td>`;
            });

            const totalUnitShort = row.Total_Unit ?? '0.00';
            const totalValueShort = row.Total_Value ?? '0.00';
            const totalUnitRaw = dec(row.Total_Unit_comma ?? '0.00');
            const totalValueRaw = dec(row.Total_Value_comma ?? '0.00');

            html += `
        <td class="total-unit"  data-raw="${totalUnitRaw}">${totalUnitShort}</td>
        <td class="total-value" data-raw="${totalValueRaw}">${totalValueShort}</td>
      </tr>`;
        });
    } else {
        html += `<tr><td colspan="${3 + selectedMonths.length * 2 + 2}" class="text-center">ไม่พบข้อมูล</td></tr>`;
    }

    html += `</tbody></table>`;
    $('#rolling-plan-container').html(html);

    bindEditableHandlers(); // (re)bind
    $('#rollingPlanTable tbody tr').each(function () { recomputeRowTotals($(this)); });
}

function recomputeRowTotals($row) {
    let totalUnit = 0, totalValue = 0;
    $row.find('td.editable, td.actualrow').each(function () {
        const raw = $(this).data('raw');
        if (raw === '' || raw == null || isNaN(raw)) return;
        const n = Number(raw);
        const pid = Number($(this).data('planamountid')); // 183=unit, 184=value
        if (pid === 183) totalUnit += n;
        else if (pid === 184) totalValue += n;
    });
    $row.find('td.total-unit').text(
        isEditMode ? (totalUnit ? toLocaleInt(totalUnit) : '0.00') : (totalUnit ? formatShort(totalUnit) : '0.00')
    );
    $row.find('td.total-value').text(
        isEditMode ? (totalValue ? toLocaleMoney(totalValue) : '0.00') : (totalValue ? formatShortHaveZero(totalValue) : '0.00')
    );
}

/* =============== Editing Handlers =============== */

function bindEditableHandlers() {
    const table = $('#rollingPlanTable');

    table.on('focus', 'td.editable', function () {
        if (!isEditMode) { this.blur(); return; }
        $(this).closest('tr').addClass('row-editing');
    });

    table.on('blur', 'td.editable', function () {
        $(this).closest('tr').removeClass('row-editing');
    });

    // restrict input
    table.on('keydown', 'td.editable', function (e) {
        if (!isEditMode) { e.preventDefault(); return; }
        const meta = e.ctrlKey || e.metaKey;
        const allowed = [8, 9, 13, 27, 37, 38, 39, 40, 46, 110, 190, 189];
        if (allowed.includes(e.keyCode) || (meta && ['a', 'c', 'v', 'x', 'z', 'y'].includes(e.key.toLowerCase()))) return;
        if (e.key >= '0' && e.key <= '9') return;
        e.preventDefault();
    });

    table.on('keydown', 'td.editable', function (e) {
        if (!isEditMode) return;
        if (e.key === 'Enter') { e.preventDefault(); $(this).blur(); }
    });

    table.on('paste', 'td.editable', function (e) {
        if (!isEditMode) { e.preventDefault(); return; }
        e.preventDefault();
        const txt = (e.originalEvent || e).clipboardData.getData('text') || '';
        const cleaned = sanitizeNumericText(txt);
        document.execCommand('insertText', false, cleaned);
    });

    // live dirty mark
    table.on('input', 'td.editable', function () {
        if (!isEditMode) return;
        const $cell = $(this);
        let newVal = $cell.text().replace(/,/g, '').trim();
        const oldVal = ($cell.data('raw') ?? '').toString();
        if (newVal !== '' && !/^-?\d*\.?\d*$/.test(newVal)) {
            const pid = Number($cell.data('planamountid'));
            const prev = $cell.data('raw');
            $cell.text(prev == null || prev === '' ? '' : (pid === 184 ? toLocaleMoney(prev) : toLocaleInt(prev)));
            return;
        }
        $cell.toggleClass('dirty', newVal !== oldVal);
    });

    // commit to buffer (no server call)
    table.on('blur', 'td.editable', function () {
        if (!isEditMode) return;
        const $cell = $(this);
        let newVal = $cell.text().replace(/,/g, '').trim();

        if (newVal !== '' && isNaN(newVal)) {
            const pid = Number($cell.data('planamountid'));
            const prev = $cell.data('raw');
            $cell.text(prev == null || prev === '' ? '' : (pid === 184 ? toLocaleMoney(prev) : toLocaleInt(prev)));
            $cell.removeClass('dirty'); return;
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
            PlanAmountID: Number($cell.data('planamountid')), // 183 unit, 184 value
            OldValue: oldRaw,
            NewValue: newRaw
        };

        // render formatted, update raw, stage
        const pid = payload.PlanAmountID;
        $cell.text(newRaw == null ? '' : (pid === 184 ? toLocaleMoney(newRaw) : toLocaleInt(newRaw)));
        $cell.data('raw', newRaw == null ? '' : newRaw);
        $cell.removeClass('dirty');

        upsertPendingEdit(payload);
        recomputeRowTotals($row);

        $btnSave.prop('disabled', pendingEdits.length === 0);
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
    if (idx >= 0) pendingEdits[idx] = change;
    else pendingEdits.push(change);
}

/* ===================== Save / Cancel ===================== */

function savePendingEdits() {
    if (!pendingEdits.length) { Swal.fire('Nothing to save', '', 'info'); return; }

    // snapshot for precise commit
    const batch = pendingEdits.splice(0, pendingEdits.length);

    fetch(baseUrl + 'Projecttargetrolling/UpsertEdits', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(batch)
    })
        .then(r => r.json())
        .then(res => {
            if (!res?.success) {
                pendingEdits.unshift(...batch);
                $btnSave.prop('disabled', pendingEdits.length === 0);
                Swal.fire('Error', res?.message || 'Save failed', 'error');
                return;
            }

            // commit UI: data-old & data-raw = NewValue
            batch.forEach(ch => {
                const sel = `tr[data-projectid="${ch.ProjectID}"][data-plantypeid="${ch.PlanTypeID}"][data-year="${ch.Year}"] td.editable[data-month="${ch.Month}"][data-planamountid="${ch.PlanAmountID}"]`;
                const $cell = $(sel);
                const committed = ch.NewValue == null ? '' : ch.NewValue;
                $cell.data('old', committed);
                $cell.data('raw', committed);
                $cell.removeClass('dirty');
            });

            // refresh totals (local) + summary (server)
            $('#rollingPlanTable tbody tr').each(function () { recomputeRowTotals($(this)); });
            if (typeof refreshSummaryCards === 'function') refreshSummaryCards();

            Swal.fire({ toast: true, position: 'top-end', icon: 'success', title: 'Saved', showConfirmButton: false, timer: 1600 });

            // keep edit mode ON (so user can continue). Disable Save until new edits.
            $btnSave.prop('disabled', pendingEdits.length === 0);
        })
        .catch(e => {
            pendingEdits.unshift(...batch);
            $btnSave.prop('disabled', pendingEdits.length === 0);
            Swal.fire('Error', e?.message || 'Save failed', 'error');
        });
}

/* ===================== Summary ===================== */

function refreshSummaryCards() {
    const filter = collectRollingPlanFilters();
    const fd = new FormData(); for (const k in filter) fd.append(k, filter[k]);
    fetch(baseUrl + 'Projecttargetrolling/GetDataTableProjectAndTargetRolling', { method: 'POST', body: fd })
        .then(r => r.json())
        .then(json => { if (json?.success) renderSummaryCards(json.datasum || []); })
        .catch(() => { });
}

// helpers used by summary styling (provide simple fallbacks)
function normalizeBg(s) { return (typeof s === 'string' && s.trim()) ? s.trim() : ''; }
function idealTextColor(bg) {
    // very simple contrast heuristic: default to white if dark
    try {
        const hex = bg.startsWith('#') ? bg.substring(1) : bg;
        const r = parseInt(hex.substring(0, 2), 16);
        const g = parseInt(hex.substring(2, 4), 16);
        const b = parseInt(hex.substring(4, 6), 16);
        const yiq = (r * 299 + g * 587 + b * 114) / 1000;
        return yiq >= 128 ? '#000' : '#fff';
    } catch { return '#fff'; }
}
function hexToRgba(hex, a = 0.35) {
    try {
        const h = hex.replace('#', '');
        const r = parseInt(h.substring(0, 2), 16);
        const g = parseInt(h.substring(2, 4), 16);
        const b = parseInt(h.substring(4, 6), 16);
        return `rgba(${r},${g},${b},${a})`;
    } catch { return 'rgba(255,255,255,0.35)'; }
}
function renderSummaryCards(datasum) {
    const container = document.getElementById('cardSummary');
    if (!container) return;
    container.innerHTML = '';

    // helpers
    const normKey = s => (s || '').toLowerCase().replace(/\s/g, '');
    const toNum = v => {
        if (typeof v === 'number') return isFinite(v) ? v : 0;
        if (typeof v === 'string') {
            const cleaned = v.replace(/mb$/i, '').replace(/[, ]+/g, '').trim();
            const n = parseFloat(cleaned);
            return isNaN(n) ? 0 : n;
        }
        return 0;
    };

    const safeDiv = (a, b) => {
        const A = toNum(a), B = toNum(b);
        const result = A - B;
        const formatted = result.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        return result < 0 ? `<span style="color:red;">${formatted}</span>` : formatted;
    };

    const safeDiv2 = (a, b) => {
        const A = toNum(a), B = toNum(b);
        const result = A - B;
        const formatted = result.toLocaleString('en-US', { maximumFractionDigits: 0 });
        return result < 0 ? `<span style="color:red;">${formatted}</span>` : formatted;
    };

    // % formatter from values
    const pctByValue = (num, den, frac = 2) => {
        const N = toNum(num), D = toNum(den);
        if (!isFinite(N) || !isFinite(D) || D === 0) return (0).toFixed(frac) + ' %';
        const r = (N / D) * 100;
        return r.toLocaleString(undefined, { minimumFractionDigits: frac, maximumFractionDigits: frac }) + ' %';
    };

    const fmtInt = n => toNum(n).toLocaleString();
    const fmtMoney = n => toNum(n).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    const fmtPct = (r, frac = 2) =>
        (r * 100).toLocaleString(undefined, { minimumFractionDigits: frac, maximumFractionDigits: frac }) + '%';

    // ====== dynamic color for percent (const in-scope) ======
    // Rule: <50 = red, 50–80 = yellow, 80–100 = blue, >100 = neon green with strong white border
    const colorPercent = (pctText) => {
        const num = parseFloat(String(pctText).replace(/[^\d.-]/g, '')) || 0;
        let color = '#dc3545'; // red (<50)
        let extraStyle = '';

        if (num > 100) {
            color = '#00ff88';
        }
        else if (num > 80) color = '#0d6efd';   // blue (80–100)
        else if (num >= 50) color = '#ffc107';  // yellow (50–80)

        return `<span style="color:${color}; font-weight:600; font-size:1.05em; ${extraStyle}">${pctText}</span>`;
    };

    // index by PlanTypeName
    const idx = {};
    (datasum || []).forEach(it => { idx[normKey(it?.PlanTypeName)] = it || {}; });

    const target = idx['target'] || {};
    const workingTarget = idx['workingtarget'] || {};
    const mll = idx['mll'] || {};
    const rolling = idx['rolling'] || {};
    const workingRolling = idx['workingrolling'] || {};
    const actual = idx['actual'] || {};

    // computed ratios
    const achieveTargetUnitRatio = safeDiv2(actual.Unit, target.Unit);
    const achieveTargetValueRatio = safeDiv(actual.Value, target.Value);
    const achieveWorkUnitRatio = safeDiv2(actual.Unit, workingTarget.Unit);
    const achieveWorkValueRatio = safeDiv(actual.Value, workingTarget.Value);

    // compute % AFTER actual/target/workingTarget exist
    const pctTarget = pctByValue(actual.Value, target.Value);
    const pctWork = pctByValue(actual.Value, workingTarget.Value);

    // === renderer (updated to support Achieve layout) ===
    const renderCard = ({ label, unitText, valueText, colorClass, isAchieve, percentHTML }) => {
        const bg = normalizeBg(colorClass) || '#ffffff';

        // force white text for Achieve cards and Actual card
        const mustWhite = isAchieve || (label === 'Actual');
        const fg = mustWhite ? '#ffffff' : idealTextColor(bg);
        const div = hexToRgba(fg, 0.35);

        const card = document.createElement('div');
        card.className = 'summary-card';
        card.style.setProperty('--bg', bg);
        card.style.setProperty('--fg', fg);
        card.style.setProperty('--divider', div);

        const unitLabel = isAchieve ? 'Diff Unit' : 'Unit';
        const valueLabel = isAchieve ? 'Diff Value' : 'Value (MB)';

        if (isAchieve) {
            // === Achieve layout ===
            card.innerHTML = `
          <div class="sc-title">${label}</div>
          <div class="sc-percent" style="margin:0.1rem; font-weight:700;">
            ${percentHTML || ''}
          </div>
          <div class="sc-grid">
            <div class="sc-cell">
              <div class="sc-label">${unitLabel}</div>
              <div class="sc-value">${unitText}</div>
            </div>
            <div class="sc-cell">
              <div class="sc-label">${valueLabel}</div>
              <div class="sc-value">${valueText}</div>
            </div>
          </div>
        `;
        } else {
            // === normal layout ===
            card.innerHTML = `
            <div class="sc-title">${label}</div> 
            <div class="sc-grid"> 
                <div class="sc-cell">
                    <div class="sc-label">${unitLabel}</div> 
                    <div class="sc-value">${unitText}</div> 
                </div>
                <div class="sc-cell">
                    <div class="sc-label">${valueLabel}</div> 
                    <div class="sc-value">${valueText}</div> 
                </div> 
            </div>
        `;
        }

        container.appendChild(card);
    };


    // ---- ORDER ----
    // Row 1
    renderCard({
        label: 'Target',
        unitText: fmtInt(target?.Unit ?? 0),
        valueText: fmtMoney(target?.Value ?? 0),
        colorClass: target?.ColorClass,
        isAchieve: false
    });
    renderCard({
        label: 'Working Target',
        unitText: fmtInt(workingTarget?.Unit ?? 0),
        valueText: fmtMoney(workingTarget?.Value ?? 0),
        colorClass: workingTarget?.ColorClass,
        isAchieve: false
    });
    renderCard({
        label: 'MLL',
        unitText: fmtInt(mll?.Unit ?? 0),
        valueText: fmtMoney(mll?.Value ?? 0),
        colorClass: mll?.ColorClass,
        isAchieve: false
    });
    // Achieve Target (new layout)
    renderCard({
        label: 'Achieve Target',
        percentHTML: colorPercent(pctTarget),   // 👈 percent line (colored)
        unitText: achieveTargetUnitRatio,       // Diff Unit
        valueText: achieveTargetValueRatio,     // Diff Value
        colorClass: '#20c997',
        isAchieve: true
    });

    // Row 2
    renderCard({
        label: 'Rolling',
        unitText: fmtInt(rolling?.Unit ?? 0),
        valueText: fmtMoney(rolling?.Value ?? 0),
        colorClass: rolling?.ColorClass,
        isAchieve: false
    });
    renderCard({
        label: 'Working Rolling',
        unitText: fmtInt(workingRolling?.Unit ?? 0),
        valueText: fmtMoney(workingRolling?.Value ?? 0),
        colorClass: workingRolling?.ColorClass,
        isAchieve: false
    });
    renderCard({
        label: 'Actual',
        unitText: fmtInt(actual?.Unit ?? 0),
        valueText: fmtMoney(actual?.Value ?? 0),
        colorClass: actual?.ColorClass,
        isAchieve: false // but forced white by mustWhite
    });
    // Achieve Working Target (new layout)
    renderCard({
        label: 'Achieve Working Target',
        percentHTML: colorPercent(pctWork),     // 👈 percent line (colored)
        unitText: achieveWorkUnitRatio,         // Diff Unit
        valueText: achieveWorkValueRatio,       // Diff Value
        colorClass: '#20c997',
        isAchieve: true
    });
}





/* ===================== Search / Load ===================== */
// ===== SEARCH =====
$(document).on('click', '#btnSearch', function () {
    searchRollingPlanData();
});

function searchRollingPlanData() {
    Busy.with(async () => {
        const filter = collectRollingPlanFilters();
        // months from Month dropdown or fallback to Quarter or all
        let selectedMonths = ($('#ddl_month').val() || []).map(Number);
        if (selectedMonths.length === 0) {
            const q = choicesQuarter?.getValue(true) || [];
            selectedMonths = q.flatMap(x => monthMap[x] || []);
        }
        if (selectedMonths.length === 0) selectedMonths = Array.from({ length: 12 }, (_, i) => i + 1);

        const fd = new FormData(); for (const k in filter) fd.append(k, filter[k]);

        const res = await fetch(baseUrl + 'Projecttargetrolling/GetDataTableProjectAndTargetRolling', { method: 'POST', body: fd });
        const json = await res.json();
        if (json?.success) {
            renderTableFromJson(json.data || [], selectedMonths);
            renderSummaryCards(json.datasum || []);
            setEditMode(false); // reset to view mode after reload
        }
    }, 'Loading data…', 300);
}

// ===== UPDATE your ClearFilter like this (only the parts for year & plantype changed) =====
function ClearFilter() {
    const currentYear = new Date().getFullYear();

    // --- Year (via Choices instance) ---
    try {
        if (choicesYear) {
            choicesYear.removeActiveItems();
            choicesYear.setChoiceByValue(String(currentYear));
        } else {
            initYearDropdown(); // fallback if not initialized
        }
    } catch { }

    // --- Quarter ---
    try { choicesQuarter?.removeActiveItems(); } catch { }

    // --- Month ---
    try {
        choicesMonth?.clearStore();
        updateMonthDropdown([]); // 1–12, none selected
    } catch { }

    // --- Plan Type (use Choices instance) ---
    try { choicesPlanType?.removeActiveItems(); } catch { }

    // --- BUG ---
    try { choicesBu?.removeActiveItems(); } catch { }

    // --- Project Status ---
    try { choicesProjectStatus?.removeActiveItems(); } catch { }

    // --- Project Partner ---
    (function resetPartner() {
        const el = document.getElementById('ddl_project_partner');
        if (choicesProjectPartner) {
            try {
                choicesProjectPartner.removeActiveItems();
                choicesProjectPartner.setChoiceByValue('');
            } catch { }
        } else if (el) {
            el.value = '';
            el.dispatchEvent(new Event('change'));
        }
    })();

    // --- Project list UI state ---
    try {
        choicesProject?.removeActiveItems();
        choicesProject?.clearStore();
        choicesProject?.setChoices(
            [{ value: '', label: '— No projects —', disabled: true }],
            'value', 'label', true
        );
    } catch { }

    // --- Show Type default ---
    //(function resetShowType() {
    //    const defaultVal = 'GetListTargetRollingPlanCuttoltal';
    //    const el = document.getElementById('ddl_showtype');
    //    if (choicesShowType) {
    //        try { choicesShowType.setChoiceByValue(defaultVal); } catch { }
    //    }
    //    if (el) {
    //        el.value = defaultVal;
    //        el.dispatchEvent(new Event('change'));
    //    }
    //})();
    // --- Show Type (All project checkbox) default = not checked ---



    // reload projects with cleared filters
    loadProjectFromFilters();

    // Optional: auto search after clear
    // searchRollingPlanData();
}


// ===== EXPORT =====
function exportExcelProjectAndTargetRolling() {
    const filter = collectRollingPlanFilters();
    const fd = new FormData();
    for (const k in filter) fd.append(k, filter[k]);

    Busy.with(async () => {
        const res = await fetch(baseUrl + 'Projecttargetrolling/ExportProjectAndTargetRolling', {
            method: 'POST',
            body: fd
        });

        if (!res.ok) {
            const txt = await res.text().catch(() => '');
            Swal.fire('Export failed', txt || `HTTP ${res.status}`, 'error');
            return;
        }

        const blob = await res.blob();
        // filename from header or fallback
        const cd = res.headers.get('Content-Disposition') || '';
        const m = cd.match(/filename\*=UTF-8''([^;]+)|filename="?([^"]+)"?/i);
        const fname = decodeURIComponent(m?.[1] || m?.[2] || 'ProjectTargetRolling.xlsx');

        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url; a.download = fname;
        document.body.appendChild(a); a.click();
        setTimeout(() => { URL.revokeObjectURL(url); a.remove(); }, 0);
    }, 'Exporting…', 300);
}

// ===== IMPORT =====
$(document).on('submit', '#form-import-project-target-rolling', function (e) {
    e.preventDefault();
    const form = this;
    const fd = new FormData(form);
    const file = fd.get('file');

    if (!file || (file instanceof File && file.size === 0)) {
        Swal.fire('Please choose a file', '', 'info');
        return;
    }

    Busy.with(async () => {
        const res = await fetch(baseUrl + 'Projecttargetrolling/ImportExcel', {
            method: 'POST',
            body: fd
        });

        if (res.status === 403) {
            // permission denied by controller
            let msg = 'No permission to import.';
            try { const j = await res.json(); if (j?.message) msg = j.message; } catch { }
            Swal.fire('Forbidden', msg, 'error');
            return;
        }

        if (!res.ok) {
            const txt = await res.text().catch(() => '');
            Swal.fire('Upload failed', txt || `HTTP ${res.status}`, 'error');
            return;
        }

        const json = await res.json();
        if (json?.success) {
            // close modal
            $('#modalImportTargetRolling').modal('hide');
            form.reset();

            Swal.fire({
                icon: 'success',
                title: 'Import completed',
                text: json.message || `Imported ${json.count ?? ''} rows`,
                timer: 1800,
                showConfirmButton: false
            });

            // refresh data + summary
            searchRollingPlanData();
        } else {
            Swal.fire('Import error', json?.message || 'Unknown error', 'error');
        }
    }, 'Uploading…', 300);
});

document.addEventListener("DOMContentLoaded", function () {
    const toggleBtn = document.querySelector('[data-bs-target="#fltCollapse"]');
    const icon = toggleBtn.querySelector("i");
    const collapse = document.getElementById("fltCollapse");

    collapse.addEventListener("show.bs.collapse", () => {
        icon.classList.replace("fa-chevron-down", "fa-chevron-up");
    });
    collapse.addEventListener("hide.bs.collapse", () => {
        icon.classList.replace("fa-chevron-up", "fa-chevron-down");
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const summaryToggle = document.querySelector('[data-bs-target="#collapseSummary"]');
    const summaryIcon = summaryToggle.querySelector("i");
    const summaryCollapse = document.getElementById("collapseSummary");

    summaryCollapse.addEventListener("show.bs.collapse", () => {
        summaryIcon.classList.replace("fa-chevron-down", "fa-chevron-up");
    });
    summaryCollapse.addEventListener("hide.bs.collapse", () => {
        summaryIcon.classList.replace("fa-chevron-up", "fa-chevron-down");
    });
});

/* ===================== Wire Buttons & Init ===================== */

$(document).on('click', '#btnEdit', () => Busy.with(() => setEditMode(true), 'Entering edit mode…', 200));
$(document).on('click', '#btnCancelEdit', () => Busy.with(() => cancelEdits(), 'Reverting changes…', 200));
$(document).on('click', '#btnSaveEdit', () => Busy.with(() => savePendingEdits(), 'Saving…', 200));

function initAllDropdowns() {
    initYearDropdown();
    initQuarterDropdown();
    initMonthDropdown();
    initPlanTypeDropdown();
    initBuDropdown();
    initProjectstatusDropdown();
    initProjectpartnerDropdown();
    /*initShowtypeDropdown()*/
    initAllProjectCheckbox()
    initProjectDropdown();
}

// boot
initAllDropdowns();
searchRollingPlanData();
