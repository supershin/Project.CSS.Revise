/*************************************************
 * Project Page – Filter + Table + Edit Modal
 * Full JS (drop-in replacement)
 *************************************************/

/* =========================
   Utilities & Globals
   ========================= */
const _base = (typeof baseUrl !== 'undefined' && baseUrl) ? baseUrl : '/';
const _post = (url, body, signal) => fetch(url, { method: 'POST', body, signal });
const getChoicesVals = (inst) => {
    try {
        const v = inst?.getValue(true);
        return Array.isArray(v) ? v.filter(Boolean) : (v ? [v] : []);
    } catch { return []; }
};
function escapeHtml(s) {
    return (s ?? '').replace(/[&<>"']/g, m => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[m]));
}

/* =========================
   Choices instances
   ========================= */
let choicesCompany, choicesBu, choicesProject, choicesZones;
let projectAbortCtrl;   // abort load for project by BU/Company
let _editModalInst;

/* =========================
   Init dropdowns (filter)
   ========================= */
function initCompanyDropdown() {
    const el = document.getElementById('ddl_company');
    if (!el) return;
    choicesCompany = new Choices(el, {
        removeItemButton: true, searchEnabled: true,
        placeholderValue: 'Select company', itemSelectText: '', shouldSort: false
    });
    el.addEventListener('change', onFilterChanged);
}
function initBuDropdown() {
    const el = document.getElementById('ddl_bug');
    if (!el) return;
    choicesBu = new Choices(el, {
        removeItemButton: true, searchEnabled: true,
        placeholderValue: 'Select BU', itemSelectText: '', shouldSort: false
    });
    el.addEventListener('change', onFilterChanged);
}
function initProjectDropdown() {
    const el = document.getElementById('ddl_project');
    if (!el) return;
    choicesProject = new Choices(el, {
        removeItemButton: true, searchEnabled: true,
        placeholderValue: 'Select project', itemSelectText: '', shouldSort: false
    });
    loadProjectsByFilters(); // first time
}
function onFilterChanged() { loadProjectsByFilters(); }

/* =========================
   Fetch project dropdown (by Company + BU)
   ========================= */
function loadProjectsByFilters() {
    const companies = getChoicesVals(choicesCompany);
    const bus = getChoicesVals(choicesBu);

    const fd = new FormData();
    fd.append('L_Company', companies.join(',')); // maps to controller
    fd.append('L_BUID', bus.join(','));

    try { projectAbortCtrl?.abort(); } catch { }
    projectAbortCtrl = new AbortController();

    _post(_base + 'Project/GetProjectListByBU', fd, projectAbortCtrl.signal)
        .then(r => r.json())
        .then(json => {
            const list = json?.data || [];
            choicesProject.clearStore();

            if (!json?.success || list.length === 0) {
                choicesProject.setChoices([{ value: '', label: '— No projects —', disabled: true }], 'value', 'label', true);
                return;
            }

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

/* =========================
   Table: fetch & render
   ========================= */
document.addEventListener('DOMContentLoaded', () => {
    initCompanyDropdown();
    initBuDropdown();
    initProjectDropdown();

    document.getElementById('btnFilterSearch')?.addEventListener('click', fetchProjectTable);
    document.getElementById('btnFilterCancel')?.addEventListener('click', () => {
        // clear filters
        choicesCompany?.removeActiveItems();
        choicesBu?.removeActiveItems();
        choicesProject?.removeActiveItems();
        // reload projects (all)
        loadProjectsByFilters();
        // clear table
        renderProjectTable([]);
    });

    // modal init
    _editModalInst = bootstrap.Modal.getOrCreateInstance(document.getElementById('mdlEditProject'));
    initEditProjectModalUI();

    // sticky shadow toggler (visual only)
    const tblWrap = document.querySelector('#card_project_list .table-responsive');
    tblWrap?.addEventListener('scroll', () => {
        tblWrap.classList.toggle('has-sticky', tblWrap.scrollTop > 0);
    }, { passive: true });

    // mark rows that sit under the sticky header (so we only lower those buttons)
    const thead = tblWrap?.querySelector('thead');
    if (tblWrap && thead) {
        const markRowsUnderHeader = () => {
            const headerBottom = thead.getBoundingClientRect().bottom;
            tblWrap.querySelectorAll('tbody tr').forEach(tr => {
                const r = tr.getBoundingClientRect();
                if (r.top < headerBottom) tr.classList.add('under-header');
                else tr.classList.remove('under-header');
            });
        };
        tblWrap.addEventListener('scroll', markRowsUnderHeader, { passive: true });
        window.addEventListener('resize', markRowsUnderHeader);
        markRowsUnderHeader();
    }
});

async function fetchProjectTable() {
    const companies = getChoicesVals(choicesCompany);
    const bus = getChoicesVals(choicesBu);
    const projects = getChoicesVals(choicesProject);

    const fd = new FormData();
    fd.append('L_Company', companies.join(','));
    fd.append('L_BUID', bus.join(','));
    fd.append('L_ProjectID', projects.join(','));
    fd.append('L_ProjectStatus', '');
    fd.append('L_ProjectPartner', '');

    try {
        const res = await _post(_base + 'Project/GetListProjectTable', fd);
        const json = await res.json();
        const list = json?.data || [];
        renderProjectTable(list);
    } catch (err) {
        console.error('GetListProjectTable failed', err);
        renderProjectTable([]);
    }
}

function renderProjectTable(list) {
    const tbody = document.querySelector('#tbl_projects tbody');
    const counter = document.getElementById('count_projects');
    if (!tbody) return;

    tbody.innerHTML = '';
    counter && (counter.textContent = Array.isArray(list) ? list.length : 0);

    if (!Array.isArray(list) || list.length === 0) {
        tbody.innerHTML = `<tr><td colspan="11" class="text-center text-muted py-4">— No data —</td></tr>`;
        return;
    }

    list.forEach(row => {
        const tr = document.createElement('tr');
        tr.dataset.companyId = row.CompanyID ?? '';
        tr.dataset.buId = row.BUID ?? '';
        tr.dataset.partnerId = row.PartnerID ?? '';
        tr.dataset.landOfficeId = row.LandOfficeID ?? '';
        tr.dataset.projectZoneId = row.ProjectZoneID ?? '';   // CSV e.g. "206,450"
        tr.dataset.statusId = row.ProjectStatusID ?? row.StatusID ?? ''; // if available

        // zone tags
        const zoneNames = (row.ProjectZonename || '').split(',').map(s => s.trim()).filter(Boolean);
        const zoneHtml = zoneNames.length
            ? zoneNames.map(z => `<span class="tag"><i class="bi bi-geo-alt"></i>${escapeHtml(z)}</span>`).join('')
            : '';

        // type icon
        const type = (row.ProjectType || '').trim().toUpperCase();
        let typeIcon = '';
        if (type === 'C') typeIcon = `<i class="bi bi-building text-primary me-1"></i>`;
        else if (type === 'H') typeIcon = `<i class="bi bi-house-door text-success me-1"></i>`;

        // company + icon (only when has value)
        const companyHtml = row.CompanyName
            ? `<i class="bi bi-building me-1 text-secondary"></i>${escapeHtml(row.CompanyName)}`
            : '';

        // land office + icon (only when has value)
        const landOfficeHtml = row.LandOfficeName
            ? `<i class="bi bi-geo text-warning me-1"></i>${escapeHtml(row.LandOfficeName)}`
            : '';

        tr.innerHTML = `
      <td>${escapeHtml(row.ProjectID)}</td>
      <td>${companyHtml}</td>
      <td>${escapeHtml(row.BUName || '')}</td>
      <td>${escapeHtml(row.PartnerName || '')}</td>
      <td>${escapeHtml(row.ProjectName || '')}</td>
      <td>${escapeHtml(row.ProjectName_Eng || '')}</td>
      <td>${typeIcon}</td>
      <td>${escapeHtml(row.ProjectStatus || '')}</td>
      <td>${landOfficeHtml}</td>
      <td>${zoneHtml}</td>
      <td class="text-end">
        <button type="button" class="btn btn-secondary btn-icon rounded-circle me-1 btn-edit" title="Edit">
          <i class="bi bi-pencil"></i>
        </button>
        <button type="button" class="btn btn-primary btn-icon rounded-circle btn-sync" title="Sync">
          <i class="bi bi-arrow-repeat"></i>
        </button>
      </td>
    `;

        // Edit click → open modal with this row
        tr.querySelector('.btn-edit')?.addEventListener('click', () => {
            const rowData = {
                ProjectID: tr.querySelector('td:nth-child(1)')?.textContent.trim() || row.ProjectID || '',
                CompanyName: tr.querySelector('td:nth-child(2)')?.innerText.trim() || row.CompanyName || '',
                BUID: tr.dataset.buId || row.BUID || '',
                PartnerID: tr.dataset.partnerId || row.PartnerID || '',
                ProjectName: tr.querySelector('td:nth-child(5)')?.textContent.trim() || row.ProjectName || '',
                ProjectName_Eng: tr.querySelector('td:nth-child(6)')?.textContent.trim() || row.ProjectName_Eng || '',
                ProjectType: (row.ProjectType || '').toUpperCase(),
                StatusID: tr.dataset.statusId || row.ProjectStatusID || row.StatusID || '',
                ProjectStatus: tr.querySelector('td:nth-child(8)')?.textContent.trim() || row.ProjectStatus || '',
                LandOfficeID: tr.dataset.landOfficeId || row.LandOfficeID || '',
                ProjectZoneID: tr.dataset.projectZoneId || row.ProjectZoneID || ''
            };
            openEditProjectModal(rowData);
        });

        // Optional: Sync
        tr.querySelector('.btn-sync')?.addEventListener('click', () => {
            console.log('Sync clicked:', row.ProjectID);
            // TODO: implement your sync call
        });

        tbody.appendChild(tr);
    });
}

/* =========================
   Edit Project Modal
   ========================= */
function initEditProjectModalUI() {
    // Initialize Choices for Zones (works for single or multiple)
    const zonesSelect = document.getElementById('ddl_edit_zones');
    if (zonesSelect && !choicesZones) {
        choicesZones = new Choices(zonesSelect, {
            removeItemButton: zonesSelect.hasAttribute('multiple'), // chips only for multi
            searchEnabled: true,
            shouldSort: false,
            placeholder: true,
            placeholderValue: 'Select zone…',
        });
    }

    // Type toggle (C/H)
    document.querySelectorAll('#typeToggle .btn-type').forEach(btn =>
        btn.addEventListener('click', () => setProjectType(btn.getAttribute('data-val')))
    );

    // Save
    document.getElementById('btnSaveProject')?.addEventListener('click', onSaveProject);
}

function setProjectType(val) {
    const hidden = document.getElementById('hid_edit_projecttype');
    const hint = document.getElementById('typeHint');
    hidden.value = val || '';
    document.querySelectorAll('#typeToggle .btn-type').forEach(b => b.classList.remove('active'));
    if (val) {
        document.querySelector(`#typeToggle .btn-type[data-val="${val}"]`)?.classList.add('active');
        hint.textContent = val === 'C' ? 'Condominium' : (val === 'H' ? 'House' : '');
    } else {
        hint.textContent = '';
    }
}

function setSelectValue(selectId, value) {
    const el = document.getElementById(selectId);
    if (!el) return;
    const v = (value ?? '').toString();
    if ([...el.options].some(o => (o.value ?? '') == v)) el.value = v;
}

function openEditProjectModal(row) {
    // fixed / disabled fields
    document.getElementById('edt_ProjectID').value = row.ProjectID || '';
    document.getElementById('edt_CompanyName').value = row.CompanyName || '';

    // simple fields
    document.getElementById('txt_edit_projectname').value = row.ProjectName || '';
    document.getElementById('txt_edit_projectname_en').value = row.ProjectName_Eng || '';

    // selects by ID
    setSelectValue('ddl_edit_bu', row.BUID);
    setSelectValue('ddl_edit_partner', row.PartnerID);
    setSelectValue('ddl_edit_landoffice', row.LandOfficeID);

    // status by ID (fallback: match by name)
    if (row.StatusID) {
        setSelectValue('ddl_edit_status', row.StatusID);
    } else if (row.ProjectStatus) {
        const el = document.getElementById('ddl_edit_status');
        if (el) {
            const match = [...el.options].find(o => (o.textContent || '').trim() === row.ProjectStatus);
            if (match) el.value = match.value;
        }
    }

    // Project Zone(s) – supports single or multiple
    if (choicesZones) {
        const raw = (row.ProjectZoneID || '').toString();
        const values = raw.split(',').map(s => s.trim()).filter(Boolean);

        choicesZones.removeActiveItems(); // clear previous

        if (values.length === 0) {
            // nothing to select
        } else if (document.getElementById('ddl_edit_zones').hasAttribute('multiple')) {
            // multiple
            values.forEach(v => choicesZones.setChoiceByValue(v));
        } else {
            // single
            choicesZones.setChoiceByValue(values[0]);
        }
    } else {
        // fallback if Choices not initialized
        setSelectValue('ddl_edit_zones', row.ProjectZoneID);
    }

    // type
    setProjectType((row.ProjectType || '').toUpperCase());

    _editModalInst?.show();
}

function onSaveProject() {
    const zoneSelect = document.getElementById('ddl_edit_zones');
    let zoneValue;
    if (choicesZones) {
        const val = choicesZones.getValue(true);
        zoneValue = Array.isArray(val) ? val : (val ? [val] : []);
    } else {
        zoneValue = zoneSelect?.multiple
            ? [...zoneSelect.selectedOptions].map(o => o.value)
            : [zoneSelect?.value ?? ''];
    }

    const payload = {
        ProjectID: document.getElementById('edt_ProjectID').value.trim(),
        BUID: document.getElementById('ddl_edit_bu').value,
        PartnerID: document.getElementById('ddl_edit_partner').value,
        ProjectName: document.getElementById('txt_edit_projectname').value.trim(),
        ProjectName_Eng: document.getElementById('txt_edit_projectname_en').value.trim(),
        ProjectType: document.getElementById('hid_edit_projecttype').value,
        StatusID: document.getElementById('ddl_edit_status').value,
        LandOfficeID: document.getElementById('ddl_edit_landoffice').value,
        ProjectZoneIDs: zoneValue  // array (works for single or multiple)
    };

    console.log('Save payload:', payload);
    // TODO: POST to your save endpoint, then close modal + refresh table
    // fetch(_base + 'Project/SaveProject', { method:'POST', body: toFormData(payload) })
    //   .then(r => r.json()).then(json => { if(json.success){ _editModalInst.hide(); fetchProjectTable(); } });
}

/* Optional: convert plain object to FormData
function toFormData(obj) {
  const fd = new FormData();
  Object.entries(obj).forEach(([k, v]) => {
    if (Array.isArray(v)) v.forEach(x => fd.append(k, x));
    else fd.append(k, v ?? '');
  });
  return fd;
}
*/
