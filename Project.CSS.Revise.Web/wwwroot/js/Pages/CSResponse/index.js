(function () {
    const block = e => { e.preventDefault(); e.stopPropagation(); };
    document.addEventListener('contextmenu', block, { capture: true });
    document.addEventListener('dragstart', block, { capture: true });
    document.addEventListener('copy', block, { capture: true });
    document.addEventListener('keydown', function (e) {
        const k = e.key?.toUpperCase();
        if (k === 'F12') return block(e);
        if (e.ctrlKey && e.shiftKey && ['I', 'J', 'C'].includes(k)) return block(e);
        if (e.ctrlKey && (k === 'U' || k === 'S' || k === 'P')) return block(e);
    }, { capture: true });
})();

document.querySelectorAll('.hover-icon-swap').forEach(container => {
    const icon = container.querySelector('.icon-toggle');
    const def = icon.dataset.default;
    const hov = icon.dataset.hover;
    container.addEventListener('mouseenter', () => { icon.classList.remove(def); icon.classList.add(hov); });
    container.addEventListener('mouseleave', () => { icon.classList.remove(hov); icon.classList.add(def); });
});

document.addEventListener("DOMContentLoaded", function () {
    // ====== Choices instances ===================================================
    const csChoices = new Choices('#csUserSelect', { placeholderValue: '🔍 พิมพ์ค้นหาชื่อพนักงาน...', searchEnabled: true, itemSelectText: '', shouldSort: false });
    const projectChoices = new Choices('#projectSelect', { placeholderValue: '🔍 พิมพ์ค้นหาโครงการ...', searchEnabled: true, itemSelectText: '', shouldSort: false });
    const buildChoices = new Choices('#buildingMultiSelect', { removeItemButton: true, placeholderValue: '🔍 เลือกอาคาร', searchEnabled: true, itemSelectText: '', shouldSort: false });
    const floorChoices = new Choices('#floorMultiSelect', { removeItemButton: true, placeholderValue: '🔍 เลือกชั้น (เช่น B-2)', searchEnabled: true, itemSelectText: '', shouldSort: false });
    const unitChoices = new Choices('#roomMultiSelect', { removeItemButton: true, placeholderValue: '🔍 เลือกยูนิต', searchEnabled: true, itemSelectText: '', shouldSort: false });
    const UnitStatusChoices = new Choices('#ddlUnitStatus', { removeItemButton: true, placeholderValue: '🔍 เลือกสถานะยูนิต', searchEnabled: true, itemSelectText: '', shouldSort: false });

    // CS tab project multi-select
    const projectChoicesCs = new Choices('#ddlProject_cs', {
        removeItemButton: true,
        searchEnabled: true,
        itemSelectText: '',
        placeholder: true,
        placeholderValue: '— เลือกโครงการ —',
        shouldSort: false
    });


    // auto-select first option
    const firstOption = document.querySelector('#projectSelect option');
    if (firstOption) {
        projectChoices.setChoiceByValue(firstOption.value);
    }

    const firstOptioncsUser = document.querySelector('#csUserSelect option');
    if (firstOptioncsUser) {
        csChoices.setChoiceByValue(firstOptioncsUser.value);
    }

    // ====== Helpers =============================================================
    const $ = (sel) => document.querySelector(sel);

    function formPost(url, data) {
        return fetch(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },
            body: new URLSearchParams(data)
        }).then(r => r.json());
    }

    function setLoading(selectEl, choicesInst, msg = 'กำลังโหลด...') {
        choicesInst.clearStore();
        choicesInst.clearChoices();
        choicesInst.setChoices([{ value: '', label: msg, disabled: true }], 'value', 'label', true);
        selectEl.setAttribute('disabled', 'disabled');
    }
    function clearSelect(selectEl, choicesInst, placeholder = '— เลือก —') {
        choicesInst.clearStore();
        choicesInst.clearChoices();
        choicesInst.setChoices([{ value: '', label: placeholder, disabled: true }], 'value', 'label', true);
        selectEl.setAttribute('disabled', 'disabled');
    }
    function enableSelect(selectEl) { selectEl.removeAttribute('disabled'); }
    function getSelectedValues(choicesInst) {
        const val = choicesInst.getValue(true);
        return Array.isArray(val) ? val.filter(v => v !== '') : (val ? [val] : []);
    }
    const csv = arr => (arr && arr.length ? arr.join(',') : '');

    // robust project getter (fallback to native select if needed)
    function getProjectId() {
        // Choices
        let pid = projectChoices?.getValue?.(true);
        if (!pid || (Array.isArray(pid) && !pid.length)) {
            // native <select>
            pid = $('#projectSelect')?.value || '';
        }
        // ensure string
        if (Array.isArray(pid)) pid = pid[0] || '';
        return (pid || '').toString();
    }

    // ====== Loaders =============================================================
    async function loadBuilds(projectId) {
        const el = $('#buildingMultiSelect');
        setLoading(el, buildChoices);
        try {
            const res = await formPost(baseUrl + 'CSResponse/GetlistBuildInProject', { ProjectID: projectId });
            const items = (res?.data || []).map(x => ({ value: String(x.ValueString), label: String(x.Text) })).filter(o => o.value);
            buildChoices.clearStore();
            buildChoices.setChoices(items, 'value', 'label', true);
            enableSelect(el);
        } catch (e) {
            console.error(e);
            clearSelect(el, buildChoices, 'โหลดอาคารไม่สำเร็จ');
        }
    }

    async function loadFloors(projectId, buildsCsv) {
        const el = $('#floorMultiSelect');
        setLoading(el, floorChoices);
        try {
            const res = await formPost(baseUrl + 'CSResponse/GetListFloorInBuildInProject', { ProjectID: projectId, Builds: buildsCsv });
            const items = (res?.data || []).map(x => ({ value: String(x.ValueString), label: String(x.Text) })).filter(o => o.value);
            floorChoices.clearStore();
            floorChoices.setChoices(items, 'value', 'label', true);
            enableSelect(el);
        } catch (e) {
            console.error(e);
            clearSelect(el, floorChoices, 'โหลดชั้นไม่สำเร็จ');
        }
    }

    async function loadUnits(projectId, buildsCsv, pairsCsv) {
        const el = $('#roomMultiSelect');
        setLoading(el, unitChoices);
        try {
            const res = await formPost(baseUrl + 'CSResponse/GetListUnitInFloorInBuildInProject', { ProjectID: projectId, Builds: buildsCsv, Floors: pairsCsv });
            const items = (res?.data || []).map(x => ({ value: String(x.ValueString), label: String(x.Text) })).filter(o => o.value);
            unitChoices.clearStore();
            unitChoices.setChoices(items, 'value', 'label', true);
            enableSelect(el);
        } catch (e) {
            console.error(e);
            clearSelect(el, unitChoices, 'โหลดยูนิตไม่สำเร็จ');
        }
    }

    // ====== Chain handlers ======================================================
    async function onProjectChange() {
        const projectId = getProjectId();

        clearSelect($('#buildingMultiSelect'), buildChoices, '— เลือกอาคาร —');
        clearSelect($('#floorMultiSelect'), floorChoices, '— เลือกชั้น —');
        clearSelect($('#roomMultiSelect'), unitChoices, '— เลือกยูนิต —');

        if (!projectId) return;

        await loadBuilds(projectId);
        await loadFloors(projectId, '');
        await loadUnits(projectId, '', '');
    }

    async function onBuildChange() {
        const projectId = getProjectId();
        if (!projectId) return;

        const buildsCsv = csv(getSelectedValues(buildChoices));
        setLoading($('#floorMultiSelect'), floorChoices);
        setLoading($('#roomMultiSelect'), unitChoices);

        await loadFloors(projectId, buildsCsv);
        const pairsCsv = csv(getSelectedValues(floorChoices));
        await loadUnits(projectId, buildsCsv, pairsCsv);
    }

    async function onFloorChange() {
        const projectId = getProjectId();
        if (!projectId) return;

        const buildsCsv = csv(getSelectedValues(buildChoices));
        const pairsCsv = csv(getSelectedValues(floorChoices));

        setLoading($('#roomMultiSelect'), unitChoices);
        await loadUnits(projectId, buildsCsv, pairsCsv);
    }

    // ====== RENDERING ===========================================================
    function setTableLoading() {
        const tbody = document.getElementById('Tb_CS_Response_body');
        if (!tbody) return;
        tbody.innerHTML = `
      <tr>
        <td colspan="4" class="text-center">
          <div class="d-flex align-items-center justify-content-center gap-2 py-3">
            <div class="spinner-border" role="status" style="width:1.5rem;height:1.5rem;"></div>
            <span>กำลังโหลดข้อมูล...</span>
          </div>
        </td>
      </tr>`;
    }
    function renderEmpty() {
        const tbody = document.getElementById('Tb_CS_Response_body');
        if (!tbody) return;
        tbody.innerHTML = `
      <tr>
        <td colspan="4" class="text-center text-muted py-3">ไม่พบข้อมูล</td>
      </tr>`;
    }
    function safe(v) { return (v ?? '').toString().trim(); }
    function renderRows(list) {
        const tbody = document.getElementById('Tb_CS_Response_body');
        if (!tbody) return;
        if (!list || !list.length) {
            renderEmpty();
            document.getElementById('row_count').textContent = "0 records";
            return;
        }

        const rows = list.map((x, idx) => {
            const unitCode = safe(x.UnitCode);
            const unitStatus = safe(x.UnitStatusName);
            const build = safe(x.Build);
            const floor = safe(x.Floor);
            const addr = safe(x.AddrNo);
            const csFullName = safe(x.CSFullNameThai); // already updated by you
            const updateBy = safe(x.UpdateBy);       // 👈 from your payload
            const updateDate = safe(x.UpdateDate);     // 👈 from your payload (already formatted server-side)
            const checked = Number(x.IsCheck) === 1 ? 'checked' : '';
            const chkId = `chk_${unitCode || ('row' + idx)}`;

            const unitDisplay = `
            <small class="d-block">${unitCode || '-'} / ${unitStatus || ''} </small>
            <small class="d-block text-muted">${addr ? `${addr} / ` : ''}อาคาร ${build || '-'} / ชั้น ${floor || '-'}</small>
        `;

            // 🔹 Build the "Update" column (show dash when missing)
            const updateCol = (updateBy || updateDate)
                ? `
                <div class="d-flex flex-column">
                    ${updateBy ? `<span><i class="icofont icofont-user"></i> ${updateBy}</span>` : ``}
                    ${updateDate ? `<small class="mt-1 text-muted"><i class="icofont icofont-calendar"></i> ${updateDate}</small>` : ``}
                </div>
              `
                : `<span class="text-muted">-</span>`;

            return `
            <tr>
              <td class="text-center">
                <input class="form-check-input row-check" id="${chkId}" type="checkbox" ${checked} data-unit="${unitCode}">
              </td>
              <td class="text-start">${unitDisplay}</td>
              <td class="text-start"><i class="icofont icofont-user"></i> ${csFullName || '-'}</td>
              <td class="text-start">${updateCol}</td>
            </tr>
        `;
        });

        tbody.innerHTML = rows.join('');
        // ✅ Update row count under project name
        document.getElementById('row_count').textContent = `${list.length} บรรทัด`;
    }


    // ====== API CALL ============================================================
    function mapShowType(val) {
        // UI: ""=ทั้งหมด, "1"=CS, "2"=Other
        // SQL: "-1"=ทั้งหมด, "1"=ของฉัน, "0"=ของคนอื่น
        if (!val) return '-1';
        if (val === '1') return '1';
        if (val === '2') return '0';
        return '-1';
    }

    async function fetchTable() {
        const userId = csChoices?.getValue?.(true) || $('#csUserSelect')?.value || '';
        const showTypeUi = $('#csUserSelectview')?.value || '';
        const showType = mapShowType(showTypeUi);
        const projectId = getProjectId();
        const unitStatus = csv(getSelectedValues(UnitStatusChoices));
        const buildsCsv = csv(getSelectedValues(buildChoices));
        const floorsCsv = csv(getSelectedValues(floorChoices));
        const unitsCsv = csv(getSelectedValues(unitChoices));

        if (!projectId) { renderEmpty(); return; }

        setTableLoading();
        try {
            const res = await formPost(baseUrl + 'CSResponse/GetlistDataTableUnitCSResponse', {
                USerID: userId,
                Showtype: showType,
                ProjectID: projectId,
                UnitStatus: unitStatus,
                Builds: buildsCsv,
                Floors: floorsCsv,
                Units: unitsCsv
            });
            renderRows(res?.data || []);
        } catch (err) {
            console.error(err);
            renderEmpty();
        }
    }

    // ====== EVENTS ==============================================================
    $('#projectSelect').addEventListener('change', onProjectChange);
    $('#buildingMultiSelect').addEventListener('change', onBuildChange);
    $('#floorMultiSelect').addEventListener('change', onFloorChange);


    // helper: get current project's label
    function getProjectLabel() {
        // Choices.js selected item
        const selected = projectChoices?.getValue?.();
        if (selected && selected.label) return selected.label;

        // fallback to native <select>
        const opt = document.querySelector('#projectSelect option:checked');
        return opt ? opt.textContent : '';
    }

    // set initial header to first option (if you auto-select it)
    (function initProjectHeader() {
        const h = document.getElementById('name_project_selected');
        if (!h) return;
        const firstOpt = document.querySelector('#projectSelect option');
        if (firstOpt) h.textContent = firstOpt.textContent;
    })();

    // on Search click: update header, then fetch table
    const btnSearch = document.getElementById('btnSearch');
    if (btnSearch) {
        btnSearch.addEventListener('click', async () => {
            const h = document.getElementById('name_project_selected');
            if (h) h.textContent = getProjectLabel() || '';
            await fetchTable();  // your existing function
        });
    }

    const checkAll = document.getElementById('checkAll');
    if (checkAll) {
        checkAll.addEventListener('change', () => {
            document.querySelectorAll('#Tb_CS_Response_body .row-check').forEach(chk => { chk.checked = checkAll.checked; });
        });
    }

    // initial state
    clearSelect($('#buildingMultiSelect'), buildChoices, '— เลือกอาคาร —');
    clearSelect($('#floorMultiSelect'), floorChoices, '— เลือกชั้น —');
    clearSelect($('#roomMultiSelect'), unitChoices, '— เลือกยูนิต —');

    const initialProject = getProjectId();
    if (initialProject) {
        onProjectChange().then(fetchTable);
    }


    // === helpers ===
    function getCSUserId() {
        const el = document.getElementById('csUserSelect');
        const n = parseInt(el?.value || '0', 10);
        return isNaN(n) ? 0 : n;
    }

    function getCheckedUnitCodes() {
        return Array.from(document.querySelectorAll('#Tb_CS_Response_body .row-check:checked'))
            .map(chk => (chk.dataset.unit || '').trim())
            .filter(Boolean);
    }

    // === POST (x-www-form-urlencoded) — NO UpdateBy ===
    async function postUpdateInsertCsmapping(payload) {
        const url = (typeof baseUrl !== 'undefined' ? baseUrl : '/') + 'CSResponse/UpdateorInsertCsmapping';
        const form = new URLSearchParams();
        form.append('ProjectID', payload.ProjectID);
        form.append('CSUserID', String(payload.CSUserID));
        (payload.ListUnitCode || []).forEach(code => form.append('ListUnitCode', code));

        const res = await fetch(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },
            body: form
        });
        return res.json();
    }

    // === wire button ===
    (function wireSaveBtn() {
        const btn = document.getElementById('save_change_cs_responsive');
        if (!btn) { return; }

        btn.addEventListener('click', async () => {
            const ProjectID = getProjectId();          // your existing function
            const CSUserID = getCSUserId();
            const ListUnitCode = getCheckedUnitCodes();

            // validate
            if (!ProjectID) { showApiResult({ success: false, message: 'กรุณาเลือกโครงการ' }, { mode: 'toast' }); return; }
            if (!CSUserID || CSUserID <= 0) { showApiResult({ success: false, message: 'กรุณาเลือกพนักงาน CS' }, { mode: 'toast' }); return; }
            if (!ListUnitCode.length) { showApiResult({ success: false, message: 'กรุณาเลือกยูนิตอย่างน้อย 1 รายการ' }, { mode: 'toast' }); return; }

            const originalHTML = btn.innerHTML;
            btn.disabled = true;
            btn.innerHTML = 'กำลังบันทึก...';
            showLoading();

            try {
                const resp = await postUpdateInsertCsmapping({ ProjectID, CSUserID, ListUnitCode });
                showApiResult(resp, { mode: 'toast' });
                if (typeof fetchTable === 'function') { fetchTable(); }
            } catch (err) {
                console.error(err);
                showApiResult({ success: false, message: err?.message || 'Request failed' }, { mode: 'toast' });
            } finally {
                hideLoading();
                btn.disabled = false;
                btn.innerHTML = originalHTML;
            }
        });
    })();


    // === BU -> Project relation =====================================

    function setLoadingChoices(choicesInst, placeholder = 'กำลังโหลด...') {
        choicesInst.clearStore();
        choicesInst.clearChoices();
        choicesInst.setChoices([{ value: '', label: placeholder, disabled: true }], 'value', 'label', true);
    }

    function fillChoices(choicesInst, items, placeholder = '— เลือกโครงการ —') {
        choicesInst.clearStore();
        choicesInst.clearChoices();
        choicesInst.setChoices(
            [{ value: '', label: placeholder, disabled: true }].concat(items),
            'value', 'label', true
        );
    }

    // get selected project IDs as CSV
    function getSelectedProjectsCsv() {
        const vals = projectChoicesCs.getValue(true); // returns array for multi-select
        const arr = Array.isArray(vals) ? vals.filter(v => v) : (vals ? [vals] : []);
        return arr.join(',');
    }

    async function ReadyuseProjectListByBU() {
        try {
            const list = await fetchProjectListByBU('');

            if (!list.length) {
                fillChoices(projectChoicesCs, [], '— ไม่พบโครงการ —');
                return;
            }

            const items = list.map(p => ({
                value: String(p.ProjectID),
                label: String(p.ProjectNameTH || p.ProjectNameEN || p.ProjectID)
            }));

            fillChoices(projectChoicesCs, items);
        } catch (err) {
            console.error("ReadyuseProjectListByBU error:", err);
            return [];
        }
    }


    document.querySelector('#ddlBU_cs')?.addEventListener('change', async () => {
        const buid = document.querySelector('#ddlBU_cs')?.value || '';
        setLoadingChoices(projectChoicesCs);

        const list = await fetchProjectListByBU(buid);

        if (!list.length) {
            fillChoices(projectChoicesCs, [], '— ไม่พบโครงการ —');
            return;
        }

        const items = list.map(p => ({
            value: String(p.ProjectID),
            label: String(p.ProjectNameTH || p.ProjectNameEN || p.ProjectID)
        }));

        fillChoices(projectChoicesCs, items);
    });


    // 🔹 reusable: fetch project list by BU
    async function fetchProjectListByBU(buid) {
        try {
            const res = await formPost(baseUrl + 'CSResponse/GetProjectListByBU', { L_BUID: buid });
            return res?.data || [];
        } catch (err) {
            console.error("fetchProjectListByBU error:", err);
            return [];
        }
    }



    // === Get CSSummary ===
    async function fetchCSSummary() {
        const buid = document.querySelector('#ddlBU_cs')?.value || '';
        const projectsCsv = getSelectedProjectsCsv();
        const csName = document.querySelector('#txtCsName_cs')?.value || '';

        const res = await formPost(baseUrl + 'CSResponse/GetListCSSummary', {
            BUID: buid,
            ProjectID: projectsCsv,   // ⬅️ CSV of multi-selected projects
            CsName: csName
        });

        renderSummary(res?.data || []);
    }

    let __csStatuses = null;

    async function getCSStatuses() {
        if (Array.isArray(__csStatuses)) return __csStatuses;
        const res = await formPost(baseUrl + 'CSResponse/GetListUnitStatusCS', {});
        __csStatuses = (res?.data || []).map(x => ({
            id: Number(x.ValueInt),
            name: String(x.Text || '')
        }));
        return __csStatuses;
    }
    function buildChildHeaderRow(statuses) {
        const statusThs = statuses
            .map(s => `<th class="text-center text-nowrap status-col" title="${s.name}">${s.name}</th>`)
            .join('');

        return `
    <tr class="table-secondary">
      <th class="text-nowrap col-index" id="headerOrder">ลำดับ</th>
      <th class="text-nowrap col-project" id="headerProject">Project</th>
      <th class="text-end text-nowrap">Unit</th>
      ${statusThs}
    </tr>`;
    }








    function childColspan(statuses) {
        return 3 + (statuses?.length || 0);
    }

    async function renderSummary(list) {
        const tbody = document.getElementById("tbCSSummaryBody");
        if (!tbody) return;

        if (!list?.length) {
            tbody.innerHTML = `<tr><td colspan="5" class="text-center text-muted">ไม่พบข้อมูล</td></tr>`;
            return;
        }

        tbody.innerHTML = list.map(u => {
            return `
                      <tr>
                        <td>
                          <button class="btn btn-outline-primary btn-sm btn-open-child"
                                  type="button"
                                  data-id="${u.ID}"
                                  data-name="${(u.FullnameTH || '').replace(/"/g, '&quot;')}">
                            <i class="bi bi-table"></i>
                          </button>
                        </td>
                        <td>
                          <div class="fw-semibold">${u.FullnameTH}</div>
                          <div class="text-muted small">${u.FullnameEN}</div>
                        </td>
                        <td>${u.Email || '-'}</td>
                        <td>${u.Mobile || '-'}</td>
                        <td class="text-end">
                            ${u.Cnt_Project} / ${u.Cnt_UnitCode}
                        </td>
                      </tr>`;
        }).join("");

        // เปิด modal และโหลดข้อมูลเมื่อคลิก
        tbody.querySelectorAll('.btn-open-child').forEach(btn => {
            btn.addEventListener('click', async (e) => {
                const userId = e.currentTarget.getAttribute('data-id');
                const nameTH = e.currentTarget.getAttribute('data-name') || '';
                await openChildModal(userId, nameTH);
            });
        });
    }

    async function openChildModal(userId, fullNameTH) {
        const statuses = await getCSStatuses(); // [{id, name}, ...] ExtTypeID=16
        const thead = document.getElementById('csChildModalThead');
        const tbody = document.getElementById('csChildModalTbody');
        const title = document.getElementById('csChildModalTitle');

        title.textContent = `${fullNameTH}`;
        thead.innerHTML = buildChildHeaderRow(statuses);
        tbody.innerHTML = `<tr><td colspan="${childColspan(statuses)}" class="text-center text-muted">กำลังโหลด...</td></tr>`;

        // เปิด Modal
        const modalEl = document.getElementById('csChildModal');
        const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
        modal.show();

        // โหลดข้อมูลลง tbody
        await loadChildModalRows(userId, statuses);
    }

    function numberOrBlank(n) {
        const v = Number(n ?? 0);
        return v > 0 ? String(v) : '';
    }


    async function loadChildModalRows(userId, statuses) {
        const tbody = document.getElementById('csChildModalTbody');
        if (!tbody) return;

        const colspan = childColspan(statuses);
        tbody.innerHTML = `<tr><td colspan="${colspan}" class="text-center">กำลังโหลด...</td></tr>`;

        try {
            const buid = document.querySelector('#ddlBU_cs')?.value || '';
            const projectsCsv = (typeof getSelectedProjectsCsv === 'function') ? getSelectedProjectsCsv() : '';

            const res = await formPost(baseUrl + "CSResponse/GetListCountUnitStatus", {
                UserID: userId,
                BUID: buid,
                ProjectID: projectsCsv
            });

            const list = res?.data || [];
            if (!list.length) {
                tbody.innerHTML = `<tr><td colspan="${colspan}" class="text-center text-muted">ไม่พบข้อมูล</td></tr>`;
                return;
            }

            // build rows + compute totals
            const totals = {
                Total: 0,
                byStatus: Object.fromEntries(statuses.map(s => [s.id, 0]))
            };

            const rowsHtml = list.map(p => {
                // accumulate totals
                const totalThisRow = toInt(p.Total);
                totals.Total += totalThisRow;

                const statusTds = statuses.map(s => {
                    const key = `ID_${s.id}`;          // e.g., ID_62
                    const val = toInt(p[key]);
                    totals.byStatus[s.id] += val;
                    return `<td class="text-center status-col">${numberOrBlank(p[key])}</td>`;
                }).join('');

                return `
                      <tr>
                        <td class="col-index">${p.index}</td>
                        <td class="col-project" title="${escapeHtml(p.ProjectName)}">${escapeHtml(p.ProjectName)}</td>
                        <td class="text-end">${numberOrBlank(p.Total)}</td>
                        ${statusTds}
                      </tr>`;

            }).join('');

            // totals row
            const totalsStatusTds = statuses.map(s => {
                const sum = totals.byStatus[s.id] || 0;
                return `<td class="text-center fw-semibold">${sum.toLocaleString()}</td>`;
            }).join('');

            const totalsRow = `
                              <tr class="table-secondary fw-semibold">
                                <td class="col-index"></td>
                                <td class="col-project" title="รวมทั้งหมด">รวมทั้งหมด</td>
                                <td class="text-end">${totals.Total.toLocaleString()}</td>
                                ${totalsStatusTds}
                              </tr>`;

            tbody.innerHTML = rowsHtml + totalsRow;

        } catch (err) {
            console.error(err);
            tbody.innerHTML = `<tr><td colspan="${colspan}" class="text-center text-danger">โหลดผิดพลาด</td></tr>`;
        }
    }

    // helpers (keep if not already defined elsewhere)
    function toInt(v) {
        const n = Number(v);
        return Number.isFinite(n) ? Math.trunc(n) : 0;
    }
    function escapeHtml(s) {
        return String(s ?? '')
            .replaceAll('&', '&amp;').replaceAll('<', '&lt;')
            .replaceAll('>', '&gt;').replaceAll('"', '&quot;')
            .replaceAll("'", '&#39;');
    }





    // 🔎 Event Search
    $("#btnSearch_cs")?.addEventListener("click", async () => {
        await fetchCSSummary();
    });

    fetchCSSummary();

    ReadyuseProjectListByBU();
});



