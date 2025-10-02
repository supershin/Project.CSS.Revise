/* FurnitureAndUnitFurniture.js (cleaned + QTY modal) */
(() => {
    // -------------------- DOM Refs --------------------
    const $ = (sel) => document.querySelector(sel);
    const $$ = (sel, ctx = document) => Array.from(ctx.querySelectorAll(sel));

    // choices instances
    let choicesBug;      // #ddl_Bu (multi)
    let choicesProject;  // #ddl_project (single)
    let choicesUnitType; // #ddl_unittype (single)

    // abort controllers
    let acProject = null;
    let acUnitType = null;

    // unit-furniture table refs
    const UF = { body: null, empty: null, loading: null, count: null };

    // -------------------- Utils --------------------
    const asArray = (v) => Array.isArray(v) ? v : (v ? [v] : []);
    const esc = (s) => String(s ?? '')
        .replaceAll('&', '&amp;').replaceAll('<', '&lt;')
        .replaceAll('>', '&gt;').replaceAll('"', '&quot;')
        .replaceAll("'", '&#39;');

    const toFormData = (obj = {}) => {
        const fd = new FormData();
        Object.entries(obj).forEach(([k, v]) => fd.append(k, v));
        return fd;
    };

    const setChoicesLoading = (inst, text = 'กำลังโหลด...') => {
        try {
            inst.clearStore();
            inst.setChoices([{ value: '', label: text, disabled: true }], 'value', 'label', true);
            inst.removeActiveItems();
        } catch { }
    };

    const setChoicesEmpty = (inst, text = '— ไม่มีข้อมูล —') => {
        try {
            inst.clearStore();
            inst.setChoices([{ value: '', label: text, disabled: true }], 'value', 'label', true);
            inst.removeActiveItems();
        } catch { }
    };

    const fillChoices = (inst, items, reset = true) => {
        inst.clearStore();
        inst.setChoices(items, 'value', 'label', true);
        if (reset) inst.removeActiveItems();
    };

    // -------------------- Init --------------------
    document.addEventListener('DOMContentLoaded', () => {
        // init Choices
        choicesBug = new Choices('#ddl_Bu', {
            shouldSort: false, searchEnabled: true, itemSelectText: '',
            removeItemButton: true, placeholder: true, placeholderValue: '— เลือก BUG —'
        });

        choicesProject = new Choices('#ddl_project', {
            shouldSort: false, searchEnabled: true, itemSelectText: '',
            removeItemButton: true, placeholder: true, placeholderValue: '— เลือก project —'
        });

        choicesUnitType = new Choices('#ddl_unittype', {
            shouldSort: false, searchEnabled: true, itemSelectText: '',
            removeItemButton: true, placeholder: true, placeholderValue: '— เลือก Unit Type —'
        });

        // start placeholders
        setChoicesEmpty(choicesProject, '— เลือก project —');
        setChoicesEmpty(choicesUnitType, '— เลือก Unit Type —');

        // events
        $('#ddl_Bu')?.addEventListener('change', onBuChanged);
        $('#ddl_project')?.addEventListener('change', onProjectChanged);

        // filter panel toggle (mobile)
        const body = document.getElementById('filterBody');
        const chev = document.getElementById('filterChevron');

        body?.addEventListener('shown.bs.collapse', () => chev?.classList.replace('bi-chevron-down', 'bi-chevron-up'));
        body?.addEventListener('hidden.bs.collapse', () => chev?.classList.replace('bi-chevron-up', 'bi-chevron-down'))

        // clear
        $('#btnClear')?.addEventListener('click', onClearFilters);

        // furniture check-all
        const chkAll = $('#chkAllFurnitures');
        const list = $('#furnitureList');

        chkAll?.addEventListener('change', (e) => {
            const checked = e.target.checked;
            list?.querySelectorAll('.chkFurniture').forEach(cb => cb.checked = checked);
        });

        list?.addEventListener('change', (e) => {
            if (e.target.classList.contains('chkFurniture')) {
                const all = list.querySelectorAll('.chkFurniture');
                chkAll.checked = Array.from(all).every(cb => cb.checked);
            }
        });

        // Unit Furniture table refs
        UF.body = $('#tblUnitFurnitureBody');
        UF.empty = $('#uf_empty');
        UF.loading = $('#uf_loading');
        UF.count = $('#uf_count');
        
        // Search / load table
        document.getElementById('btnSearch')?.addEventListener('click', () => {
            const projectId = choicesProject?.getValue(true) ?? '';
            const projectName = getSelectedProjectLabelDOM(); // ← from native <select>
            console.log("ProjectID:", projectId, "ProjectName:", projectName);
            document.getElementById('uf_project_name').textContent = projectName || '';
            document.getElementById('hfProjectID').value = projectId || '';

            loadUnitFurnitureTable();
        });

        // --- QTY modal wiring: open when Save Mapping clicked ---
    /*    $('#btnSaveMapping')?.addEventListener('click', () => openFurnitureQtyModal(1));*/
        $('#btnQtySave')?.addEventListener('click', onQtyModalSave);

        // optional initial load
        // loadUnitFurnitureTable();
    });

    // -------------------- Event Handlers --------------------
    async function onBuChanged() {
        const selectedBUs = asArray(choicesBug.getValue(true));
        setChoicesLoading(choicesProject, 'กำลังโหลดโครงการ...');
        setChoicesEmpty(choicesUnitType, '— เลือก Unit Type —');

        acProject?.abort();
        acProject = new AbortController();
        await loadProjectsByBU(selectedBUs, acProject.signal);
    }

    async function onProjectChanged() {
        const projectId = choicesProject.getValue(true);
        if (!projectId) { setChoicesEmpty(choicesUnitType, '— เลือก Unit Type —'); return; }

        setChoicesLoading(choicesUnitType, 'กำลังโหลด Unit Type...');
        acUnitType?.abort();
        acUnitType = new AbortController();
        await loadUnitTypesByProject(projectId, acUnitType.signal);
    }

    function onClearFilters() {
        choicesBug?.removeActiveItems();
        choicesProject?.removeActiveItems();
        setChoicesEmpty(choicesProject, '— เลือก project —');
        setChoicesEmpty(choicesUnitType, '— เลือก Unit Type —');
        const txt = $('#txtSearchName'); if (txt) txt.value = '';
    }

    // -------------------- Loaders --------------------
    async function loadProjectsByBU(buIds = [], signal) {
        try {
            const resp = await fetch(
                baseUrl + 'FurnitureAndUnitFurniture/GetProjectListByBU',
                { method: 'POST', body: toFormData({ L_BUID: buIds.length ? buIds.join(',') : '' }), signal }
            );
            const json = await resp.json();
            const list = (json?.success && Array.isArray(json.data)) ? json.data : [];

            const items = list.map(p => ({
                value: String(p.ProjectID ?? ''),                            // ← value
                label: String(p.ProjectNameTH ?? p.ProjectNameEN ??          // ← label
                    p.ProjectID ?? '')
            }));

            items.length
                ? fillChoices(choicesProject, items, true)
                : setChoicesEmpty(choicesProject, '— ไม่พบโครงการ —');

        } catch (err) {
            if (err?.name === 'AbortError') return;
            console.error('Load project failed:', err);
            setChoicesEmpty(choicesProject, 'โหลดโครงการล้มเหลว');
        }
    }


    async function loadUnitTypesByProject(projectId, signal) {
        try {
            const resp = await fetch(
                baseUrl + 'FurnitureAndUnitFurniture/GetUnitTypeListByProjectID',
                { method: 'POST', body: toFormData({ ProjectID: projectId }), signal }
            );
            const json = await resp.json();
            const list = (json?.success && Array.isArray(json.data)) ? json.data : [];

            const items = list.map(u => ({
                value: String(u.ValueString ?? u.Value ?? u.UnitType ?? ''),
                label: String(u.Text ?? u.UnitType ?? '')
            }));

            items.length ? fillChoices(choicesUnitType, items, true)
                : setChoicesEmpty(choicesUnitType, '— ไม่พบ Unit Type —');

        } catch (err) {
            if (err?.name === 'AbortError') return;
            console.error('Load unit type failed:', err);
            setChoicesEmpty(choicesUnitType, 'โหลด Unit Type ล้มเหลว');
        }
    }

    // -------------------- Unit Furniture Table --------------------
    function getFilterPayload() {
        const buCsv = (choicesBug?.getValue(true) ?? []).join(',');
        const projId = choicesProject?.getValue(true) ?? '';
        const utCsv = (choicesUnitType?.getValue(true) ?? []).join(',');
        const unitTxt = ($('#txtSearchName')?.value || '').trim();

        const fd = new FormData();
        fd.append('L_BUG', buCsv);
        fd.append('L_ProjectID', projId || '');
        fd.append('L_UnitType', utCsv);
        fd.append('Src_UnitCode', unitTxt);
        return fd;
    }

    async function loadUnitFurnitureTable() {
        showUFLoading(true);
        try {
            const resp = await fetch(baseUrl + 'FurnitureAndUnitFurniture/GetListTableUnitFurniture', {
                method: 'POST', body: getFilterPayload()
            });
            const json = await resp.json();
            const rows = (json?.success && Array.isArray(json.data)) ? json.data : [];
            renderUFRows(rows);
        } catch (err) {
            console.error('Load Unit Furniture failed:', err);
            renderUFRows([]);
        } finally {
            showUFLoading(false);
        }
    }

    function showUFLoading(show) {
        if (!UF.loading || !UF.empty) return;
        UF.loading.classList.toggle('d-none', !show);
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

        UF.body.innerHTML = items.map(toUFRowHTML).join('');
        UF.empty?.classList.add('d-none');
        if (UF.count) UF.count.textContent = `${items.length} items`;
    }

    function toUFRowHTML(item) {
        const id = esc(item?.id ?? item?.ID ?? '');
        const unitCode = esc(item?.unitCode ?? item?.UnitCode ?? '');
        const unitType = esc(item?.unitType ?? item?.UnitType ?? '');
        const qty = esc(item?.qTYFurnitureUnit ?? item?.QTYFurnitureUnit ?? '');
        const statusId = item?.checkStatusID ?? item?.CheckStatusID ?? '';
        const who = esc(item?.fullnameTH ?? item?.FullnameTH ?? '');
        const when = esc(item?.updateDate ?? item?.UpdateDate ?? '');

        // ✅ Show checkbox only if statusId == '1'
        const checkboxCol = statusId !== '309'
            ? `<input class="form-check-input chkUnitItem" type="checkbox" value="${id}" style="border: 2px solid grey; accent-color: #0d6efd;" />`
            : '';

        // ✅ Status column: ✔ if 1, else ✘
        let statusCol = '';
        if (statusId === '309') {
            statusCol = `<span class="text-success">✔</span>`;
        } else if (statusId === '310') {
            statusCol = `<span class="text-danger">✘</span>`;
        } else {
            statusCol = ''; // ไม่แสดงอะไร
        }


        return `
      <tr data-id="${id}">
        <td class="text-center">${checkboxCol}</td>
        <td>${unitCode}</td>
        <td>${unitType}</td>
        <td class="text-center">${qty || '0'}</td>
        <td class="text-center">${statusCol}</td>
        <td>${who || '-'}</td>
        <td>${when || '-'}</td>
      </tr>`;
    }

    // helper: get selected project's label from Choices
    function getSelectedProjectLabelDOM() {
        const sel = document.getElementById('ddl_project');
        if (!sel) return '';
        const i = sel.selectedIndex;
        if (i < 0) return '';
        return (sel.options[i]?.text || '').trim();
    }


    // select all in Unit table
    document.addEventListener('DOMContentLoaded', () => {
        const chkAllUnit = document.getElementById('chkAllUnit');
        const tbody = document.getElementById('tblUnitFurnitureBody');

        chkAllUnit?.addEventListener('change', (e) => {
            const checked = e.target.checked;
            tbody.querySelectorAll('.chkUnitItem').forEach(cb => cb.checked = checked);
        });

        tbody?.addEventListener('change', (e) => {
            if (e.target.classList.contains('chkUnitItem')) {
                const all = tbody.querySelectorAll('.chkUnitItem');
                const allChecked = Array.from(all).every(cb => cb.checked);
                chkAllUnit.checked = allChecked;
            }
        });
    });




    // --------------------  Modal (mapping) --------------------
    // ---------- OPEN STEPPER ----------
    document.addEventListener('DOMContentLoaded', () => {
        document.getElementById('btnSaveMapping')?.addEventListener('click', () => {
            openMappingWizard();
        });

        // footer buttons
        document.getElementById('btnMWNext')?.addEventListener('click', onStepperNext);
        document.getElementById('btnMWBack')?.addEventListener('click', () => gotoStep(1));

        // delete handlers (delegated)
        document.getElementById('mw_furnBody')?.addEventListener('click', onFurnRowClick);
        document.getElementById('mw_unitBody')?.addEventListener('click', onUnitRowClick);
    });

    let MW_state = {
        step: 1,
        furn: [], // [{id,name,qty}]
        units: [] // [{id,code,type}]
    };

    function openMappingWizard(defaultQty = 1) {
        MW_state.furn = getCheckedFurnitures().map(x => ({ id: x.id, name: x.name, qty: defaultQty }));
        MW_state.units = getCheckedUnits();
        renderStep1();
        renderStep2(); // pre-render so step 2 is ready
        gotoStep(1);

        bootstrap.Modal.getOrCreateInstance(document.getElementById('mdlMapWizard')).show();
    }

    // ---------- GATHER SELECTIONS ----------
    function getCheckedFurnitures() {
        // expects .list-group-item[data-id][data-name] with .chkFurniture
        return Array.from(document.querySelectorAll('#furnitureList .chkFurniture:checked'))
            .map(cb => {
                const row = cb.closest('.list-group-item');
                return {
                    id: row?.dataset.id ?? cb.value ?? '',
                    name: row?.dataset.name ?? ''
                };
            }).filter(x => x.id);
    }

    function getCheckedUnits() {
        // expects unit table rows <tr data-id> with .chkUnitItem
        return Array.from(document.querySelectorAll('#tblUnitFurnitureBody .chkUnitItem:checked')).map(cb => {
            const tr = cb.closest('tr');
            return {
                id: tr?.dataset.id ?? cb.value ?? '',
                code: tr?.children?.[1]?.textContent?.trim() ?? '',
                type: tr?.children?.[2]?.textContent?.trim() ?? ''
            };
        }).filter(x => x.id);
    }

    // ---------- RENDER STEP 1 (FURN + QTY) ----------
    function renderStep1() {
        const tbody = document.getElementById('mw_furnBody');
        const empty = document.getElementById('mw_furnEmpty');
        const cnt = document.getElementById('cntFurnSel');

        if (!MW_state.furn.length) {
            tbody.innerHTML = '';
            empty.classList.remove('d-none');
        } else {
            empty.classList.add('d-none');
            tbody.innerHTML = MW_state.furn.map((it, i) => `
      <tr data-id="${escapeHtml(it.id)}">
        <td class="text-center">${i + 1}</td>
        <td>${escapeHtml(it.name)}</td>
        <td class="text-end pe-3">
          <input type="number" class="form-control form-control-sm qty-input" min="0" step="1" value="${Number(it.qty) || 0}">
        </td>
        <td class="text-center">
          <button class="btn btn-light btn-icon-xs mw-del-furn" title="Remove"><i class="bi bi-x-lg text-danger"></i></button>
        </td>
      </tr>
    `).join('');
        }
        cnt.textContent = MW_state.furn.length.toString();
        // disable Next if no furniture
        document.getElementById('btnMWNext').disabled = MW_state.furn.length === 0;
    }

    // ---------- RENDER STEP 2 (UNITS) ----------
    function renderStep2() {
        const tbody = document.getElementById('mw_unitBody');
        const empty = document.getElementById('mw_unitEmpty');
        const cnt = document.getElementById('cntUnitSel');

        if (!MW_state.units.length) {
            tbody.innerHTML = '';
            empty.classList.remove('d-none');
        } else {
            empty.classList.add('d-none');
            tbody.innerHTML = MW_state.units.map((it, i) => `
      <tr data-id="${escapeHtml(it.id)}">
        <td class="text-center">${i + 1}</td>
        <td>${escapeHtml(it.code)}</td>
        <td>${escapeHtml(it.type)}</td>
        <td class="text-center">
          <button class="btn btn-light btn-icon-xs mw-del-unit" title="Remove"><i class="bi bi-x-lg text-danger"></i></button>
        </td>
      </tr>
    `).join('');
        }
        cnt.textContent = MW_state.units.length.toString();
        // save enabled only if at least 1 unit + 1 furniture
        const canSave = MW_state.units.length > 0 && MW_state.furn.length > 0;
        document.getElementById('btnMWNext').disabled = !canSave && MW_state.step === 2;
    }

    // ---------- DELETE HANDLERS ----------
    function onFurnRowClick(e) {
        const btn = e.target.closest('.mw-del-furn');
        if (!btn) return;

        const tr = btn.closest('tr');
        const id = tr?.getAttribute('data-id');
        MW_state.furn = MW_state.furn.filter(x => x.id !== id);
        renderStep1();
    }

    function onUnitRowClick(e) {
        const btn = e.target.closest('.mw-del-unit');
        if (!btn) return;

        const tr = btn.closest('tr');
        const id = tr?.getAttribute('data-id');
        MW_state.units = MW_state.units.filter(x => x.id !== id);

        // also uncheck in main table
        const mainCb = document.querySelector(`#tblUnitFurnitureBody .chkUnitItem[value="${CSS.escape(id)}"]`);
        if (mainCb) mainCb.checked = false;

        renderStep2();
    }

    // ---------- NAVIGATION ----------
    function gotoStep(n) {
        MW_state.step = n;
        const s1 = document.getElementById('stepPane1');
        const s2 = document.getElementById('stepPane2');
        const li1 = document.getElementById('stp1');
        const li2 = document.getElementById('stp2');
        const back = document.getElementById('btnMWBack');
        const next = document.getElementById('btnMWNext');

        if (n === 1) {
            s1.classList.remove('d-none'); s2.classList.add('d-none');
            li1.classList.add('active'); li2.classList.remove('active');
            back.classList.add('d-none');
            next.textContent = 'ถัดไป';
            next.disabled = MW_state.furn.length === 0;
        } else {
            // before entering step 2, sync qtys from inputs
            syncQtyFromInputs();
            s1.classList.add('d-none'); s2.classList.remove('d-none');
            li1.classList.remove('active'); li2.classList.add('active');
            back.classList.remove('d-none');
            next.textContent = 'บันทึก';
            const canSave = MW_state.units.length > 0 && MW_state.furn.length > 0;
            next.disabled = !canSave;
        }
    }

    function onStepperNext() {
        if (MW_state.step === 1) {
            // ensure at least 1 furniture with valid qty (>=0 allowed; adjust if you require >0)
            syncQtyFromInputs();
            if (MW_state.furn.length === 0) return;
            gotoStep(2);
        } else {
            // SAVE
            const payload = buildMappingPayload();
            // TODO: POST to your endpoint
            // Example:
            // const fd = new FormData();
            // fd.append('ProjectID', choicesProject?.getValue(true) ?? '');
            // fd.append('ItemsJson', JSON.stringify(payload));
            // await fetch(baseUrl + 'FurnitureAndUnitFurniture/SaveFurnitureProjectMapping', { method:'POST', body: fd });

            console.log('SAVE PAYLOAD →', payload);
            bootstrap.Modal.getInstance(document.getElementById('mdlMapWizard'))?.hide();
        }
    }

    function syncQtyFromInputs() {
        const rows = Array.from(document.querySelectorAll('#mw_furnBody tr'));
        MW_state.furn = rows.map(tr => {
            const id = tr.getAttribute('data-id') || '';
            const name = tr.children?.[1]?.textContent?.trim() || '';
            const qtyInput = tr.querySelector('.qty-input');
            let qty = Number(qtyInput?.value ?? 0);
            if (!Number.isFinite(qty) || qty < 0) qty = 0;
            return { id, name, qty };
        });
    }

    function buildMappingPayload() {
        // prefer hidden field (locked by Search) for consistent save
        const projectId = document.getElementById('hfProjectID')?.value || '';
        return {
            ProjectID: projectId,
            Furnitures: MW_state.furn, // [{id,name,qty}]
            Units: MW_state.units      // [{id,code,type}]
        };
    }


    // ---------- Misc ----------
    function escapeHtml(s) {
        return String(s ?? '')
            .replaceAll('&', '&amp;').replaceAll('<', '&lt;')
            .replaceAll('>', '&gt;').replaceAll('"', '&quot;')
            .replaceAll("'", '&#39;');
    }


})();
