// Choices.js init
document.addEventListener("DOMContentLoaded", function () {

    const projectChoices = new Choices('#projectSelect', { placeholderValue: '🔍 พิมพ์ค้นหาโครงการ...', searchEnabled: true, itemSelectText: '', shouldSort: false });

    const firstOption = document.querySelector('#projectSelect option');
    if (firstOption) {
        projectChoices.setChoiceByValue(firstOption.value);
    }

    bindUI();
});

let _tbodyTemplateHTML = "";  // cache initial rows (MON..SUN)
const DAY_ID_MAP = { MON: 215, TUE: 216, WED: 217, THU: 218, FRI: 219, SAT: 220, SUN: 221 };

function bindUI() {
    // cache initial tbody (has <tr data-day="...">)
    const tbody = document.getElementById('Tb_Appointment_body');
    _tbodyTemplateHTML = tbody.innerHTML;

    // numbers only
    document.getElementById('Tb_Appointment')?.addEventListener('input', (e) => {
        const td = e.target.closest('td.editable');
        if (!td) return;
        td.textContent = (td.textContent || '').replace(/[^\d]/g, '').slice(0, 4);
    });

    // fetch on dropdown change
    document.getElementById('projectSelect')?.addEventListener('change', onSearch);

    // initial load
    onSearch();

    // save
    document.getElementById("btnSave")?.addEventListener("click", onSaveAppointmentLimits);
}

function ensureDataAttributes() {
    const headerTimes = getHeaderTimes(); // [{timeId,label}]
    document.querySelectorAll('#Tb_Appointment tbody tr[data-day]').forEach(tr => {
        const dayKey = (tr.getAttribute('data-day') || '').toUpperCase();
        const dayId = DAY_ID_MAP[dayKey] || 0;
        tr.dataset.dayid = dayId;   // <tr data-dayid="215">

        const tds = tr.querySelectorAll('td.editable');
        headerTimes.forEach((h, i) => {
            if (tds[i]) tds[i].dataset.timeid = h.timeId; // <td data-timeid="222">
        });
    });
}

async function onSearch() {
    const projectSelect = document.getElementById('projectSelect');
    const projectId = projectSelect?.value || '';
    const projectName = projectSelect?.options[projectSelect.selectedIndex]?.text || '';

    const nameEl = document.getElementById('name_project_selected');
    const rowCountEl = document.getElementById('row_count');
    const tbody = document.getElementById('Tb_Appointment_body');
    const tableWrap = document.getElementById('div_Tb_Appointment'); // scroll container

    nameEl.textContent = `Project: ${projectName || '-'}`;
    rowCountEl.textContent = '';

    // restore structure if needed + stamp data attributes
    if (!tbody.querySelector('tr[data-day]')) {
        tbody.innerHTML = _tbodyTemplateHTML;
    }
    ensureDataAttributes();
    // clear old numbers
    tbody.querySelectorAll('td.editable').forEach(td => td.textContent = '');

    // show overlay (do NOT replace tbody)
    tableWrap.classList.add('table-loading');

    try {
        const url = baseUrl + 'AppointmentLimit/GetlistAppointmentLimit';
        const resp = await fetch(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ProjectID: projectId })
        });

        const json = await resp.json();
        const rows = Array.isArray(json) ? json : (json?.data || []);

        fillTableFromPivot(rows);
        rowCountEl.textContent = `${rows.length} day(s)`;
    } catch (err) {
        console.error(err);
        errorMessage('Something went wrong!', 'Load failed');
    } finally {
        tableWrap.classList.remove('table-loading');
    }
}

/* ---------- helpers ---------- */
function getHeaderTimes() {
    const ths = Array.from(document.querySelectorAll('#Tb_Appointment thead th[data-time-id]'));
    return ths.map(th => ({
        timeId: parseInt(th.dataset.timeId, 10),
        label: (th.textContent || '').trim()
    })).filter(x => !Number.isNaN(x.timeId));
}

function getRowByDay(dayKey) {
    const key = String(dayKey || '').trim().toUpperCase();
    return document.querySelector(`#Tb_Appointment tbody tr[data-day="${key}"]`);
}

function getPivotValByTimeId(pivotRow, timeId) {
    const prefix = `T${timeId}_`;
    const key = Object.keys(pivotRow).find(k => k.startsWith(prefix));
    const val = key ? Number(pivotRow[key] ?? 0) : 0;
    return Number.isFinite(val) ? val : 0;
}

function fillTableFromPivot(rows) {
    const headerTimes = getHeaderTimes();
    rows.forEach(p => {
        const tr = getRowByDay(p.DaysName); // <tr data-day="MON">
        if (!tr) return;
        const tds = tr.querySelectorAll('td.editable');
        headerTimes.forEach((h, i) => {
            if (!tds[i]) return;
            const val = getPivotValByTimeId(p, h.timeId);
            tds[i].textContent = val > 0 ? String(val) : '';
        });
    });
}

async function onSaveAppointmentLimits() {
    const projectId = document.getElementById("projectSelect")?.value || "";
    if (!projectId) {
        showWarning("กรุณาเลือกโครงการก่อนบันทึก");
        return;
    }

    const rows = document.querySelectorAll("#Tb_Appointment tbody tr[data-day]");
    const payload = [];

    rows.forEach(tr => {
        const dayId = parseInt(tr.getAttribute("data-dayid"), 10) || 0;
        const dayName = tr.getAttribute("data-day") || "";
        tr.querySelectorAll("td.editable").forEach(td => {
            const timeId = parseInt(td.getAttribute("data-timeid"), 10) || 0;
            const val = parseInt((td.textContent || '').trim(), 10) || 0;
            payload.push({
                ProjectID: projectId,
                DayID: dayId,
                DaysName: dayName,
                TimeID: timeId,
                TimesName: "",
                UnitLimitValue: val,
                FlagActive: true,
                UserID: 0 // server sets real user
            });
        });
    });

    try {
        const url = baseUrl + 'AppointmentLimit/InsertOrUpdateProjectAppointLimit';
        const resp = await fetch(url, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        const data = await resp.json();
        if (data?.Issuccess || data?.issuccess) {
            successToast(data?.Message || "Appointment limits saved successfully.");
        } else {
            errorMessage(data?.Message , "Save failed.");
        }
    } catch (err) {
        console.error(err);
        errorMessage("network error", "Save failed.");
    }
}