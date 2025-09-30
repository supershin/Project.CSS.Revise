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
        shouldSort: false, searchEnabled: true, itemSelectText: '', removeItemButton: false,
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
    document.getElementById('btnSearch')?.addEventListener('click', () => {
        const selectedBUs = choicesBug?.getValue(true) ?? [];
        const projectId = choicesProject?.getValue(true) ?? '';
        const unitType = choicesUnitType?.getValue(true) ?? '';
        const unitText = (document.getElementById('txtSearchName')?.value || '').trim();
        console.log('SEARCH:', { selectedBUs, projectId, unitType, unitText });
        // loadTable({ bus:selectedBUs, projectId, unitType, unitText });
    });

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


