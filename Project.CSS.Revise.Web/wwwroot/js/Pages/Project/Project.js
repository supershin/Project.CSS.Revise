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

    // --- Search button: show loading overlay ---
    const btnSearch = document.getElementById('btnFilterSearch');
    if (btnSearch) {
        btnSearch.addEventListener('click', async () => {
            try {
                showLoading();
                await fetchProjectTable();   // must return a Promise
            } catch (err) {
                console.error('fetchProjectTable failed:', err);
                errorToast('Failed to load project list');
            } finally {
                hideLoading();
            }
        });
    }

    // --- Cancel button: reset filters + clear table ---
    document.getElementById('btnFilterCancel')?.addEventListener('click', () => {
        choicesCompany?.removeActiveItems();
        choicesBu?.removeActiveItems();
        choicesProject?.removeActiveItems();

        loadProjectsByFilters(); // reset all dropdowns
        renderProjectTable([]);  // clear table
    });

    // --- Modal init ---
    _editModalInst = bootstrap.Modal.getOrCreateInstance(document.getElementById('mdlEditProject'));
    initEditProjectModalUI();

    // --- Sticky header visual shadow ---
    const tblWrap = document.querySelector('#card_project_list .table-responsive');
    tblWrap?.addEventListener('scroll', () => {
        tblWrap.classList.toggle('has-sticky', tblWrap.scrollTop > 0);
    }, { passive: true });

    // --- Mark rows that sit under sticky header ---
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
                CompanyID: tr.dataset.companyId || row.CompanyID || '',   // <-- add this
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
    // fixed fields
    document.getElementById('edt_ProjectID').value = row.ProjectID || '';

    // ----- Company toggle -----
    const hasCompanyName = !!(row.CompanyName && row.CompanyName.trim().length);
    const grpInput = document.getElementById('grp_company_input');
    const grpSelect = document.getElementById('grp_company_select');
    const txtCompany = document.getElementById('edt_CompanyName');
    const ddlCompany = document.getElementById('ddl_edit_company');

    if (hasCompanyName) {
        // show disabled text, hide dropdown
        grpInput?.classList.remove('d-none');
        grpSelect?.classList.add('d-none');
        txtCompany.value = row.CompanyName || '';
        // clear select to avoid stale value
        if (ddlCompany) ddlCompany.value = '';
    } else {
        // no company mapped: show dropdown
        grpInput?.classList.add('d-none');
        grpSelect?.classList.remove('d-none');
        txtCompany.value = '';
        // preselect company from dataset (if any)
        if (ddlCompany) {
            const cid = (row.CompanyID ?? '').toString();
            if ([...ddlCompany.options].some(o => o.value === cid)) {
                ddlCompany.value = cid;
            } else {
                ddlCompany.value = '';
            }
        }
    }

    // simple fields
    document.getElementById('txt_edit_projectname').value = row.ProjectName || '';
    document.getElementById('txt_edit_projectname_en').value = row.ProjectName_Eng || '';

    // selects by ID
    setSelectValue('ddl_edit_bu', row.BUID);
    setSelectValue('ddl_edit_partner', row.PartnerID);
    setSelectValue('ddl_edit_landoffice', row.LandOfficeID);

    // status by ID (fallback by text)
    if (row.StatusID) {
        setSelectValue('ddl_edit_status', row.StatusID);
    } else if (row.ProjectStatus) {
        const el = document.getElementById('ddl_edit_status');
        if (el) {
            const match = [...el.options].find(o => (o.textContent || '').trim() === row.ProjectStatus);
            if (match) el.value = match.value;
        }
    }

    // zones (Choices)
    if (choicesZones) {
        const raw = (row.ProjectZoneID || '').toString();
        const values = raw.split(',').map(s => s.trim()).filter(Boolean);
        choicesZones.removeActiveItems();
        if (values.length) {
            if (document.getElementById('ddl_edit_zones').hasAttribute('multiple')) {
                values.forEach(v => choicesZones.setChoiceByValue(v));
            } else {
                choicesZones.setChoiceByValue(values[0]);
            }
        }
    } else {
        setSelectValue('ddl_edit_zones', row.ProjectZoneID);
    }

    // type
    setProjectType((row.ProjectType || '').toUpperCase());

    _editModalInst?.show();
}


async function onSaveProject() {
    // zone (single or multi → first)
    const zoneSelect = document.getElementById('ddl_edit_zones');
    let zoneValue;
    if (choicesZones) {
        const val = choicesZones.getValue(true);
        const arr = Array.isArray(val) ? val : (val ? [val] : []);
        zoneValue = arr.length ? arr[0] : null;
    } else {
        zoneValue = zoneSelect?.multiple
            ? ([...zoneSelect.selectedOptions].map(o => o.value)[0] || null)
            : (zoneSelect?.value || null);
    }

    // Company: if dropdown visible, use it; otherwise null (not edited)
    const grpSelect = document.getElementById('grp_company_select');
    const ddlCompany = document.getElementById('ddl_edit_company');
    let companyId = null;
    if (grpSelect && !grpSelect.classList.contains('d-none') && ddlCompany) {
        companyId = ddlCompany.value ? Number(ddlCompany.value) : null;
    }

    const projectId = (document.getElementById('edt_ProjectID').value || '').trim();
    const buId = document.getElementById('ddl_edit_bu').value || null;
    const partnerId = document.getElementById('ddl_edit_partner').value || null;
    const projectName = (document.getElementById('txt_edit_projectname').value || '').trim();
    const projectNameEn = (document.getElementById('txt_edit_projectname_en').value || '').trim();
    const projectType = (document.getElementById('hid_edit_projecttype').value || '').trim().toUpperCase(); // "C"/"H"
    const statusId = document.getElementById('ddl_edit_status').value || null;
    const landOfficeId = document.getElementById('ddl_edit_landoffice').value || null;

    const payload = {
        ProjectID: projectId,
        CompanyID: companyId, // only sent if dropdown used; otherwise null
        BUID: buId ? Number(buId) : null,
        PartnerID: partnerId ? Number(partnerId) : null,
        ProjectName: projectName,
        ProjectName_Eng: projectNameEn,
        ProjectType: projectType,
        ProjectStatus: statusId ? Number(statusId) : null,
        LandOfficeID: landOfficeId ? Number(landOfficeId) : null,
        ProjectZoneID: zoneValue ? Number(zoneValue) : null
    };

    // Minimal validation (same as before)
    const missing = [];
    if (!payload.ProjectID) missing.push('ProjectID');
    if (!payload.BUID) missing.push('BUID');
    if (!payload.PartnerID) missing.push('PartnerID');
    if (!payload.ProjectName) missing.push('ProjectName');
    if (!payload.ProjectType || !['C', 'H'].includes(payload.ProjectType)) missing.push('ProjectType (C/H)');
    if (!payload.ProjectStatus) missing.push('ProjectStatus');
    if (!payload.ProjectZoneID) missing.push('ProjectZoneID');

    if (missing.length) {
        errorMessage('Missing/invalid: ' + missing.join(', '));
        return;
    }

    showLoading();
    try {
        const res = await fetch(_base + 'Project/SaveEditProject', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (!res.ok) {
            hideLoading();
            errorMessage(`HTTP ${res.status} ${res.statusText}`);
            return;
        }

        const json = await res.json(); // { success, message }
        hideLoading();
        showApiResult(json, { mode: 'modal' });

        if (json && json.success) {
            _editModalInst?.hide();
            await fetchProjectTable();
        }
    } catch (err) {
        hideLoading();
        errorMessage('Network error: ' + (err?.message || err));
    }
}




