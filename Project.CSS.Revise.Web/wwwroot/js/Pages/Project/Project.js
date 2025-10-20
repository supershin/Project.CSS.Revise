/*************************************************
 * Project Page – Company, BUG, Project (linked)
 *************************************************/

// ===== Utilities =====
const getChoicesVals = (inst) => {
    try {
        const v = inst?.getValue(true);
        return Array.isArray(v) ? v.filter(Boolean) : (v ? [v] : []);
    } catch { return []; }
};

// ===== Choices instances =====
let choicesCompany, choicesBu, choicesProject;
let projectAbortCtrl;

// base URL helper (ถ้าโปรเจกต์มี baseUrl อยู่แล้ว ใช้ตัวนั้นได้เลย)
const _post = (url, body, signal) => fetch(url, { method: 'POST', body, signal });

// ===== Init dropdowns =====
function initCompanyDropdown() {
    const el = document.getElementById('ddl_company');
    if (!el) return;
    choicesCompany = new Choices(el, {
        removeItemButton: true,
        searchEnabled: true,
        placeholderValue: 'Select company',
        itemSelectText: '',
        shouldSort: false
    });

    el.addEventListener('change', onFilterChanged);
}

function initBuDropdown() {
    const el = document.getElementById('ddl_bug');
    if (!el) return;
    choicesBu = new Choices(el, {
        removeItemButton: true,
        searchEnabled: true,
        placeholderValue: 'Select BU',
        itemSelectText: '',
        shouldSort: false
    });

    el.addEventListener('change', onFilterChanged);
}

function initProjectDropdown() {
    const el = document.getElementById('ddl_project');
    if (!el) return;
    choicesProject = new Choices(el, {
        removeItemButton: true,
        searchEnabled: true,
        placeholderValue: 'Select project',
        itemSelectText: '',
        shouldSort: false
    });

    // ครั้งแรกโหลดตามค่าเริ่มต้น (ว่าง = ทั้งหมด)
    loadProjectsByFilters();
}

function onFilterChanged() {
    loadProjectsByFilters();
}

// ===== Fetch project by (Company + BU) =====
function loadProjectsByFilters() {
    const companies = getChoicesVals(choicesCompany);
    const bus = getChoicesVals(choicesBu);

    const fd = new FormData();
    fd.append('L_Company', companies.join(',')); // <-- แม็ปตรงกับ Controller
    fd.append('L_BUID', bus.join(','));          // <-- แม็ปตรงกับ Controller

    try { projectAbortCtrl?.abort(); } catch { }
    projectAbortCtrl = new AbortController();

    _post(baseUrl + 'Project/GetProjectListByBU', fd, projectAbortCtrl.signal)
        .then(r => r.json())
        .then(json => {
            const list = json?.data || [];
            choicesProject.clearStore();

            if (!json?.success || list.length === 0) {
                choicesProject.setChoices([{ value: '', label: '— No projects —', disabled: true }], 'value', 'label', true);
                return;
            }

            // mapping: ProjectID / ProjectNameTH (หรือ EN ถ้าชอบ)
            const items = list.map(p => ({
                value: p.ProjectID,
                label: p.ProjectNameTH || p.ProjectNameEN || p.ProjectID
            }));

            choicesProject.setChoices(items, 'value', 'label', true);
        })
        .catch(err => {
            if (err?.name === 'AbortError') return;
            console.error('Load projects failed:', err);
            choicesProject.clearStore();
            choicesProject.setChoices([{ value: '', label: 'Failed to load projects', disabled: true }], 'value', 'label', true);
        });
}

// ===== Boot =====
document.addEventListener('DOMContentLoaded', () => {
    initCompanyDropdown();
    initBuDropdown();
    initProjectDropdown();
});
