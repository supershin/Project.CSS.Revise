/* FurnitureAndUnitFurniture.js */
let choicesBug;        // #ddl_Bu (multi)
let choicesProject;    // #ddl_project (single)
let choicesUnitType;   // #ddl_unittype (single)

let buFetchAborter = null;
let unitTypeFetchAborter = null;

// Helpers
const asArray = (v) => Array.isArray(v) ? v : (v ? [v] : []);
function setLoadingChoices(ci, text = 'กำลังโหลด...') {
    try { ci.clearStore(); ci.setChoices([{ value: '', label: text, disabled: true }], 'value', 'label', true); ci.removeActiveItems(); } catch { }
}
function setEmptyChoices(ci, text = '— ไม่มีข้อมูล —') {
    try { ci.clearStore(); ci.setChoices([{ value: '', label: text, disabled: true }], 'value', 'label', true); ci.removeActiveItems(); } catch { }
}
function fillChoices(ci, items, reset = true) {
    ci.clearStore(); ci.setChoices(items, 'value', 'label', true); if (reset) ci.removeActiveItems();
}
const toFormData = (o = {}) => { const fd = new FormData(); Object.entries(o).forEach(([k, v]) => fd.append(k, v)); return fd; };



document.addEventListener('DOMContentLoaded', () => {
    // Init Choices
    choicesBug = new Choices('#ddl_Bu', {
        shouldSort: false, searchEnabled: true, itemSelectText: '', removeItemButton: true,
        placeholder: true, placeholderValue: '— เลือก BUG —'
    });
    choicesProject = new Choices('#ddl_project', {
        shouldSort: false, searchEnabled: true, itemSelectText: '', removeItemButton: true,
        placeholder: true, placeholderValue: '— เลือก project —'
    });
    choicesUnitType = new Choices('#ddl_unittype', {
        shouldSort: false, searchEnabled: true, itemSelectText: '', removeItemButton: true,
        placeholder: true, placeholderValue: '— เลือก Unit Type —'
    });

    // Start placeholders
    setEmptyChoices(choicesProject, '— เลือก project —');
    setEmptyChoices(choicesUnitType, '— เลือก Unit Type —');

    // Events
    document.getElementById('ddl_Bu')?.addEventListener('change', onBuChanged);
    document.getElementById('ddl_project')?.addEventListener('change', onProjectChanged);

    // Mobile toggle (ถ้ามีปุ่ม)
    document.getElementById('btnToggleFilters')?.addEventListener('click', () => {
        document.getElementById('filterBody')?.classList.toggle('d-none');
    });

    // Search / Clear (ถ้ามีปุ่ม)
    //document.getElementById('btnSearch')?.addEventListener('click', () => {
    //    const selectedBUs = choicesBug?.getValue(true) ?? [];
    //    const projectId = choicesProject?.getValue(true) ?? '';
    //    const unitType = choicesUnitType?.getValue(true) ?? '';
    //    const unitText = (document.getElementById('txtSearchName')?.value || '').trim();
    //    console.log('SEARCH:', { selectedBUs, projectId, unitType, unitText });
    //    // loadTable({ bus:selectedBUs, projectId, unitType, unitText });
    //});

    document.getElementById('btnClear')?.addEventListener('click', () => {
        choicesBug?.removeActiveItems();
        choicesProject?.removeActiveItems();
        setEmptyChoices(choicesProject, '— เลือก project —');
        setEmptyChoices(choicesUnitType, '— เลือก Unit Type —');
        const txt = document.getElementById('txtSearchName'); if (txt) txt.value = '';
        // loadTable({});
    });

    const chkAll = document.getElementById('chkAllFurnitures');
    const list = document.getElementById('furnitureList');

    chkAll?.addEventListener('change', (e) => {
        const checked = e.target.checked;
        list.querySelectorAll('.chkFurniture').forEach(cb => {
            cb.checked = checked;
        });
    });

    // ถ้า checkbox รายการถูก uncheck → uncheck "เลือกทั้งหมด" ด้วย
    list?.addEventListener('change', (e) => {
        if (e.target.classList.contains('chkFurniture')) {
            const all = list.querySelectorAll('.chkFurniture');
            const allChecked = Array.from(all).every(cb => cb.checked);
            chkAll.checked = allChecked;
        }
    });
});



// Handlers
async function onBuChanged() {
    const selectedBUs = asArray(choicesBug.getValue(true));
    setLoadingChoices(choicesProject, 'กำลังโหลดโครงการ...');
    setEmptyChoices(choicesUnitType, '— เลือก Unit Type —');

    // cancel previous request
    buFetchAborter?.abort();
    buFetchAborter = new AbortController();

    await loadProjectFromBU(selectedBUs, buFetchAborter.signal);
}

async function onProjectChanged() {
    const projectId = choicesProject.getValue(true);
    if (!projectId) { setEmptyChoices(choicesUnitType, '— เลือก Unit Type —'); return; }

    setLoadingChoices(choicesUnitType, 'กำลังโหลด Unit Type...');

    // cancel previous unit type request
    unitTypeFetchAborter?.abort();
    unitTypeFetchAborter = new AbortController();

    await loadUnitTypeByProject(projectId, unitTypeFetchAborter.signal);
}





// Loaders
async function loadProjectFromBU(selectedBUs = [], signal) {
    try {
        const resp = await fetch(
            baseUrl + 'FurnitureAndUnitFurniture/GetProjectListByBU',
            { method: 'POST', body: toFormData({ L_BUID: selectedBUs.length ? selectedBUs.join(',') : '' }), signal }
        );
        const json = await resp.json();
        const list = (json && json.success && Array.isArray(json.data)) ? json.data : [];
        const items = list.length ? list.map(p => ({
            value: String(p.ProjectID ?? p.Value ?? p.ValueString ?? ''),
            label: String(p.ProjectNameTH ?? p.ProjectNameEN ?? p.Text ?? p.Name ?? p.ProjectID ?? '')
        })) : [];

        if (!items.length) { setEmptyChoices(choicesProject, '— ไม่พบโครงการ —'); return; }
        fillChoices(choicesProject, items, true);
    } catch (err) {
        if (err.name === 'AbortError') return;
        console.error('Load project failed:', err);
        setEmptyChoices(choicesProject, 'โหลดโครงการล้มเหลว');
    }
}

async function loadUnitTypeByProject(projectId, signal) {
    try {
        const resp = await fetch(
            baseUrl + 'FurnitureAndUnitFurniture/GetUnitTypeListByProjectID',
            { method: 'POST', body: toFormData({ ProjectID: projectId }), signal }
        );
        const json = await resp.json();
        const list = (json && json.success && Array.isArray(json.data)) ? json.data : [];
        const items = list.length ? list.map(u => ({
            value: String(u.ValueString ?? u.Value ?? u.UnitType ?? ''),
            label: String(u.Text ?? u.UnitType ?? '')
        })) : [];

        if (!items.length) { setEmptyChoices(choicesUnitType, '— ไม่พบ Unit Type —'); return; }
        fillChoices(choicesUnitType, items, true);
    } catch (err) {
        if (err.name === 'AbortError') return;
        console.error('Load unit type failed:', err);
        setEmptyChoices(choicesUnitType, 'โหลด Unit Type ล้มเหลว');
    }
}




// ---------- Unit Furniture table ----------
const UF = {
    body: null,
    empty: null,
    loading: null,
    count: null
};

document.addEventListener('DOMContentLoaded', () => {
    UF.body = document.getElementById('tblUnitFurnitureBody');
    UF.empty = document.getElementById('uf_empty');
    UF.loading = document.getElementById('uf_loading');
    UF.count = document.getElementById('uf_count');

    // hook search button -> load table
    document.getElementById('btnSearch')?.addEventListener('click', () => loadUnitFurnitureTable());

    // optional: initial load
    // loadUnitFurnitureTable();
});

function getFilterPayload() {
    const bus = choicesBug?.getValue(true) ?? [];
    const projectId = choicesProject?.getValue(true) ?? '';
    const unitTypes = choicesUnitType?.getValue(true) ?? [];
    const unitText = (document.getElementById('txtSearchName')?.value || '').trim();

    // ensure arrays for join
    const buCsv = Array.isArray(bus) ? bus.join(',') : (bus || '');
    const utCsv = Array.isArray(unitTypes) ? unitTypes.join(',') : (unitTypes || '');

    const fd = new FormData();
    fd.append('L_BUG', buCsv);
    fd.append('L_ProjectID', projectId || '');
    fd.append('L_UnitType', utCsv);
    fd.append('Src_UnitCode', unitText);
    return fd;
}

async function loadUnitFurnitureTable() {
    showUFLoading(true);

    try {
        const resp = await fetch(baseUrl + 'FurnitureAndUnitFurniture/GetListTableUnitFurniture', {
            method: 'POST',
            body: getFilterPayload()
        });
        const json = await resp.json();

        const rows = (json && json.success && Array.isArray(json.data)) ? json.data : [];
        renderUFRows(rows);
    } catch (err) {
        console.error('Load Unit Furniture failed:', err);
        renderUFRows([]); // show empty
    } finally {
        showUFLoading(false);
    }
}

function showUFLoading(isLoading) {
    if (!UF.loading || !UF.empty) return;
    UF.loading.classList.toggle('d-none', !isLoading);
    // while loading, hide empty; tbody will be managed by render
    UF.empty.classList.add('d-none');
}

function renderUFRows(items) {
    if (!UF.body) return;

    if (!items.length) {
        UF.body.innerHTML = '';
        UF.empty?.classList.remove('d-none');
        if (UF.count) UF.count.textContent = '0 items';
        return;
    }

    const html = items.map(toUFRowHTML).join('');
    UF.body.innerHTML = html;
    UF.empty?.classList.add('d-none');
    if (UF.count) UF.count.textContent = `${items.length} items`;
}

function toUFRowHTML(item) {
    // item schema from your model
    const isCheck = !!item?.isCheck || !!item?.ISCheck; // handle json casing
    const unitCode = esc(item?.unitCode ?? item?.UnitCode ?? '');
    const unitType = esc(item?.unitType ?? item?.UnitType ?? '');
    const qty = esc(item?.qTYFurnitureUnit ?? item?.QTYFurnitureUnit ?? '');
    const status = esc(item?.checkStatusName ?? item?.CheckStatusName ?? '');
    const who = esc(item?.fullnameTH ?? item?.FullnameTH ?? '');
    const when = esc(item?.updateDate ?? item?.UpdateDate ?? '');

    const chkBadge = isCheck
        ? '<span class="badge bg-success">✓</span>'
        : '<span class="badge bg-secondary">—</span>';

    return `
    <tr>
      <td class="text-center">${chkBadge}</td>
      <td>${unitCode}</td>
      <td>${unitType}</td>
      <td class="text-end">${qty || '0'}</td>
      <td>${status || '-'}</td>
      <td>${who || '-'}</td>
      <td>${when || '-'}</td>
    </tr>
  `;
}

// tiny escape helper
function esc(s) {
    return String(s ?? '').replaceAll('&', '&amp;').replaceAll('<', '&lt;').replaceAll('>', '&gt;').replaceAll('"', '&quot;').replaceAll("'", '&#39;');
}
