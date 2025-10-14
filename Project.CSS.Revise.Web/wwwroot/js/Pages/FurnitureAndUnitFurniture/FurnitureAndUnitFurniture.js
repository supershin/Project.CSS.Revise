/* FurnitureAndUnitFurniture.js (cleaned + QTY modal) */
(() => {
    // -------------------- DOM Refs --------------------
    const $ = (sel) => document.querySelector(sel);
    const $$ = (sel, ctx = document) => Array.from(ctx.querySelectorAll(sel));

    // choices instances
    let choicesBug;      // #ddl_Bu (multi)
    let choicesProject;  // #ddl_project (single)
    let choicesUnitType; // #ddl_unittype (single)
    let choicesCheckstatus; // #ddl_checkstatus (multi)

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
            removeItemButton: true, placeholder: true, placeholderValue: 'Select one or more BUs'
        });

        choicesProject = new Choices('#ddl_project', {
            shouldSort: false, searchEnabled: true, itemSelectText: '',
            removeItemButton: true, placeholder: true, placeholderValue: 'Select one or more projects'
        });

        choicesUnitType = new Choices('#ddl_unittype', {
            shouldSort: false, searchEnabled: true, itemSelectText: '',
            removeItemButton: true, placeholder: true, placeholderValue: 'Select one or more Unit Types'
        });

        choicesCheckstatus = new Choices('#ddl_checkstatus', {
            shouldSort: false, searchEnabled: true, itemSelectText: '',
            removeItemButton: true, placeholder: true, placeholderValue: 'Select check status'
        });

        // start placeholders
        setChoicesEmpty(choicesProject, 'Select one or more projects');
        setChoicesEmpty(choicesUnitType, 'Select one or more Unit Types');


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
        const chklCsv = (choicesCheckstatus?.getValue(true) ?? []).join(',');
        const fd = new FormData();
        fd.append('L_BUG', buCsv);
        fd.append('L_ProjectID', projId || '');
        fd.append('L_UnitType', utCsv);
        fd.append('Src_UnitCode', unitTxt);
        fd.append('L_CheckStatus', chklCsv);
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

        const checkboxCol = statusId !== '309'
            ? `<input class="form-check-input chkUnitItem" type="checkbox" value="${id}" style="border: 2px solid grey; accent-color: #0d6efd;" />`
            : '';

        let statusCol = '';
        if (statusId === '309') statusCol = `<span class="text-success">✔</span>`;
        else if (statusId === '310') statusCol = `<span class="text-danger">✘</span>`;

        return `
      <tr data-id="${id}">
        <td class="text-center">${checkboxCol}</td>
        <td>
            <a href="javascript:void(0)"
               class="edit-unit-furniture text-primary"
               data-id="${id}"
               data-code="${unitCode}"
               onclick="openModalEditUnitFurniture(this.dataset.id, this.dataset.code)">
              ${unitCode} <i class="bi bi-pencil"></i>
            </a>
        </td>
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


    // make it global so inline onclick can find it
    window.openModalEditUnitFurniture = async function (unitId, unitCode) {
        const projectName = (document.getElementById('uf_project_name')?.textContent || '').trim() || '-';

        // fill header (even though modal not shown yet)
        document.getElementById('edit_unit_id').value = unitId || '';
        document.getElementById('edit_unit_code').textContent = unitCode || '-';
        document.getElementById('edit_project_name').textContent = projectName;

        // prep content so it's clean when it opens
        try {
            window.__ufDeck?.clear();
            window.__ufDeck?.setRemark('');
            const totalsI = document.getElementById('mf_total_items');
            const totalsQ = document.getElementById('mf_total_qty');
            if (totalsI) totalsI.textContent = ' 0';
            if (totalsQ) totalsQ.textContent = ' 0';
            const empty = document.getElementById('mf_empty');
            if (empty) { empty.textContent = 'Loading…'; empty.classList.remove('d-none'); }
        } catch { }

        // optional: global spinner if you already have showLoading/hideLoading helpers
        try { showLoading?.('กำลังโหลดข้อมูลยูนิต…'); } catch { }

        // 1) load + render FIRST
        const ok = await loadAndRenderUnitFurniture(unitId);

        // 2) hide global spinner
        try { hideLoading?.(); } catch { }

        // 3) finally show modal (only after data is ready)
        const el = document.getElementById('mdlEditUnitFurniture');
        const modal = bootstrap.Modal.getOrCreateInstance(el);
        modal.show();

        // if failed, keep modal content in a friendly error state
        if (!ok) {
            const empty = document.getElementById('mf_empty');
            if (empty) { empty.textContent = 'Failed to load'; empty.classList.remove('d-none'); }
        }
    };

    // POST -> GetDataUnitFurniture, then render to deck
    async function loadAndRenderUnitFurniture(unitId) {
        try {
            const fd = new FormData();
            fd.append('UnitID', unitId);

            const resp = await fetch(baseUrl + 'FurnitureAndUnitFurniture/GetDataUnitFurniture', {
                method: 'POST',
                body: fd
            });
            const json = await resp.json();
            if (!resp.ok || json?.success === false) throw new Error(json?.message || 'Load failed');

            const data = json?.data || {};
            const details = data?.details ?? data?.Details ?? [];
            const remark = data?.checkRemark ?? data?.CheckRemark ?? '';
            const status = Number(data?.checkStatusID ?? data?.CheckStatusID ?? 0);

            // render
            window.__ufDeck?.render(details);
            window.__ufDeck?.setRemark(remark);

            // NEW: lock the UI when status === 309
            window.__ufDeck?.setReadOnly(status === 309);

            // hide the inline empty/loading state
            const empty = document.getElementById('mf_empty');
            if (empty) empty.classList.add('d-none');

            return true;
        } catch (err) {
            console.error('GetDataUnitFurniture error:', err);
            return false;
        }
    }


    // -------------------- Modal (mapping via tabs) --------------------
    document.addEventListener('DOMContentLoaded', () => {
        // Open modal only when selections valid
        document.getElementById('btnSaveMapping')?.addEventListener('click', (e) => {
            const projectId = document.getElementById('hfProjectID')?.value?.trim() || '';
            const furn = getCheckedFurnitures(); // [{id,name}]
            const units = getCheckedUnits();      // [{id,code,type}]
            let ok = true;

            if (!projectId) { showWarning('Please select a project first.'); ok = false; }
            if (furn.length === 0) { showWarning('Please select at least one furniture item.'); ok = false; }
            if (units.length === 0) { showWarning('Please select at least one unit.'); ok = false; }

            if (!ok) { e.preventDefault(); e.stopPropagation(); return; }

            openMappingWizard();
        });

        // Save
        document.getElementById('btnMWSave')?.addEventListener('click', onSaveMappingFromModal);

        // delegated deletes
        document.getElementById('mw_furnBody')?.addEventListener('click', onFurnRowClick);
        document.getElementById('mw_unitBody')?.addEventListener('click', onUnitRowClick);

        // When switching tabs away from Furniture, sync qty inputs
        const tabEl = document.getElementById('tab-units-tab');
        if (tabEl) {
            tabEl.addEventListener('show.bs.tab', () => {
                syncQtyFromInputs(); // keep state accurate before viewing units tab
            });
        }
    });

    let MW_state = {
        furn: [],  // [{id,name,qty}]
        units: []  // [{id,code,type}]
    };

    function openMappingWizard(defaultQty = 1) {
        MW_state.furn = getCheckedFurnitures().map(x => ({ id: x.id, name: x.name, qty: defaultQty }));
        MW_state.units = getCheckedUnits();
        renderStep1();
        renderStep2();

        const modalEl = document.getElementById('mdlMapWizard');
        bootstrap.Modal.getOrCreateInstance(modalEl).show();

        // Ensure tab #1 is active when opening
        const firstTab = document.querySelector('#tab-furn-tab');
        if (firstTab) new bootstrap.Tab(firstTab).show();
    }

    // ---------- gather selections (unchanged) ----------
    function getCheckedFurnitures() {
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
        return Array.from(document.querySelectorAll('#tblUnitFurnitureBody .chkUnitItem:checked'))
            .map(cb => {
                const tr = cb.closest('tr');
                return {
                    id: tr?.dataset.id ?? cb.value ?? '',
                    code: tr?.children?.[1]?.textContent?.trim() ?? '',
                    type: tr?.children?.[2]?.textContent?.trim() ?? ''
                };
            }).filter(x => x.id);
    }

    // ---------- render furniture tab ----------
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
          <button class="btn btn-light btn-icon-xs mw-del-furn" title="Remove">
            <i class="bi bi-x-lg text-danger"></i>
          </button>
        </td>
      </tr>
    `).join('');
        }
        cnt.textContent = MW_state.furn.length.toString();
    }

    // ---------- render units tab ----------
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
          <button class="btn btn-light btn-icon-xs mw-del-unit" title="Remove">
            <i class="bi bi-x-lg text-danger"></i>
          </button>
        </td>
      </tr>
    `).join('');
        }
        cnt.textContent = MW_state.units.length.toString();
    }

    // ---------- delete handlers (same behavior) ----------
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
        const mainCb = document.querySelector(`#tblUnitFurnitureBody .chkUnitItem[value="${CSS.escape(id)}"]`);
        if (mainCb) mainCb.checked = false;
        renderStep2();
    }

    // ---------- qty sync ----------
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

    // ---------- save ----------
    async function onSaveMappingFromModal() {
        // Make sure latest qty values are captured
        syncQtyFromInputs();

        // basic validation
        if (MW_state.furn.length === 0) { showWarning('Please select at least one furniture item.'); return; }
        if (MW_state.units.length === 0) { showWarning('Please select at least one unit.'); return; }

        const payload = buildMappingPayload();

        const btn = document.getElementById('btnMWSave');
        const prevHtml = btn ? btn.innerHTML : '';
        if (btn) {
            btn.disabled = true;
            btn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Saving...';
        }

        try {
            showLoading?.();
            const resp = await fetch(baseUrl + 'FurnitureAndUnitFurniture/SaveFurnitureProjectMapping', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });
            const json = await resp.json().catch(() => ({}));

            if (!resp.ok || json?.success === false) {
                errorMessage?.(json?.message || 'Save failed.');
                return;
            }

            await loadUnitFurnitureTable?.();
            successMessage?.('Saved successfully.');
            bootstrap.Modal.getInstance(document.getElementById('mdlMapWizard'))?.hide();

        } catch (err) {
            console.error(err);
            errorMessage?.('Save failed. Please try again.');
        } finally {
            hideLoading?.();
            if (btn) {
                btn.disabled = false;
                btn.innerHTML = prevHtml || 'Save';
            }
        }
    }

    function buildMappingPayload() {
        const projectId = document.getElementById('hfProjectID')?.value || '';
        return {
            ProjectID: projectId,
            Furnitures: MW_state.furn, // [{id,name,qty}]
            Units: MW_state.units      // [{id,code,type}]
        };
    }
    // -------------------- END --------------------



    // ---------- Misc ----------
    function escapeHtml(s) {
        return String(s ?? '')
            .replaceAll('&', '&amp;').replaceAll('<', '&lt;')
            .replaceAll('>', '&gt;').replaceAll('"', '&quot;')
            .replaceAll("'", '&#39;');
    }



    // -------------------- DnD Modal (enhanced catalog) -----------------
    (function () {
        const wrap = document.getElementById('mf_catalog_wrap');
        const catalog = document.getElementById('mf_catalog');
        const catMissBl = document.getElementById('mf_catalog_missing_block');
        const catMiss = document.getElementById('mf_catalog_missing');
        const deck = document.getElementById('unitDeck');
        const totalsI = document.getElementById('mf_total_items');
        const totalsQ = document.getElementById('mf_total_qty');
        const search = document.getElementById('mf_search');

        if (!catalog || !deck) return; // modal not present

        let dragData = null;
        let READONLY = false; // <--- central flag

        // ---------- helpers ----------
        const getDeckIds = () => new Set([...deck.querySelectorAll('.dnd-item')].map(x => x.dataset.id));
        const getCatalogIds = () => new Set([...wrap.querySelectorAll('.dnd-src')].map(x => x.dataset.id));

        function esc(s) {
            return String(s ?? '')
                .replaceAll('&', '&amp;').replaceAll('<', '&lt;')
                .replaceAll('>', '&gt;').replaceAll('"', '&quot;')
                .replaceAll("'", '&#39;');
        }

        function updateTotals() {
            const cards = deck.querySelectorAll('.dnd-item');
            totalsI.textContent = String(cards.length);
            let sum = 0;
            cards.forEach(c => { const q = c.querySelector('.qty-input'); sum += Number(q?.value || 0); });
            totalsQ.textContent = String(sum);
        }

        function makeDeckRow({ id, name, qty = 1 }) {
            const row = document.createElement('div');
            row.className = 'list-group-item d-flex align-items-center justify-content-between gap-3 dnd-item';
            row.setAttribute('draggable', String(!READONLY));   // respect READONLY
            row.dataset.id = id;
            row.innerHTML = `
      <div class="d-flex align-items-center gap-2 flex-grow-1">
        <i class="bi bi-couch text-secondary"></i>
        <span class="fw-semibold">${esc(name)}</span>
      </div>
      <div class="d-flex align-items-center gap-2">
        <input type="number" class="form-control form-control-sm qty-input" min="0" step="1" value="${qty}">
        <button class="btn btn-light btn-sm dnd-remove" title="Remove">
          <i class="bi bi-trash text-danger"></i>
        </button>
      </div>`;

            row.addEventListener('dragstart', (e) => {
                if (READONLY) { e.preventDefault(); return; }     // guard
                row.classList.add('dragging');
                dragData = { type: 'reorder', el: row };
            });
            row.addEventListener('dragend', () => { row.classList.remove('dragging'); dragData = null; });

            // make qty/removable reflect current state
            const qtyInput = row.querySelector('.qty-input');
            const rmBtn = row.querySelector('.dnd-remove');
            qtyInput.disabled = READONLY; qtyInput.readOnly = READONLY;
            rmBtn.disabled = READONLY;

            return row;
        }

        function makeMissingChip(id, name) {
            const div = document.createElement('div');
            div.className = 'chip chip-ghost chip-missing dnd-src';
            div.setAttribute('draggable', String(!READONLY));
            div.dataset.id = id;
            div.dataset.name = name || id;
            div.title = 'This item is not in catalog — auto detected from Unit items';
            div.innerHTML = `<i class="bi bi-exclamation-circle me-1"></i>${esc(name || id)}`;
            return div;
        }

        // Build/refresh "missing" lane from deck
        function ensureMissingCatalogChips() {
            const deckIds = getDeckIds();
            const catIds = getCatalogIds();

            const missing = [];
            deckIds.forEach(id => {
                if (!catIds.has(id)) {
                    const row = deck.querySelector(`.dnd-item[data-id="${CSS.escape(id)}"]`);
                    const name = row?.querySelector('.fw-semibold')?.textContent?.trim() || id;
                    missing.push({ id, name });
                }
            });

            if (!missing.length) {
                catMiss.innerHTML = '';
                catMissBl.classList.add('d-none');
            } else {
                catMiss.innerHTML = '';
                missing.forEach(m => catMiss.appendChild(makeMissingChip(m.id, m.name)));
                catMissBl.classList.remove('d-none');
            }

            wrap.querySelectorAll('.dnd-src').forEach(chip => {
                chip.classList.toggle('in-use', deckIds.has(chip.dataset.id));
                chip.setAttribute('draggable', String(!READONLY));
            });
        }

        // ---------- Catalog: drag & dblclick (delegated + guarded) ----------
        wrap.addEventListener('dragstart', e => {
            if (READONLY) return;
            const chip = e.target.closest('.dnd-src'); if (!chip) return;
            dragData = { type: 'add', id: chip.dataset.id, name: chip.dataset.name };
            e.dataTransfer.setData('text/plain', chip.dataset.id);
        });
        wrap.addEventListener('dragend', () => dragData = null);

        wrap.addEventListener('dblclick', e => {
            if (READONLY) return;
            const chip = e.target.closest('.dnd-src'); if (!chip) return;
            if (deck.querySelector(`.dnd-item[data-id="${CSS.escape(chip.dataset.id)}"]`)) return;
            deck.appendChild(makeDeckRow({ id: chip.dataset.id, name: chip.dataset.name, qty: 1 }));
            updateTotals();
            ensureMissingCatalogChips();
        });

        // ---------- Deck: drop + reorder (guarded) ----------
        deck.addEventListener('dragover', e => {
            if (READONLY) return;
            e.preventDefault();
            deck.classList.add('dragover');
            const afterEl = getAfterElement(deck, e.clientY);
            const dragging = deck.querySelector('.dnd-item.dragging');
            if (dragging && afterEl == null) deck.appendChild(dragging);
            else if (dragging && afterEl) deck.insertBefore(dragging, afterEl);
        });
        deck.addEventListener('dragleave', () => deck.classList.remove('dragover'));

        deck.addEventListener('drop', e => {
            if (READONLY) return;
            e.preventDefault(); deck.classList.remove('dragover'); if (!dragData) return;
            if (dragData.type === 'add') {
                if (deck.querySelector(`.dnd-item[data-id="${CSS.escape(dragData.id)}"]`)) return;
                const card = makeDeckRow({ id: dragData.id, name: dragData.name, qty: 1 });
                const afterEl = getAfterElement(deck, e.clientY);
                if (afterEl == null) deck.appendChild(card); else deck.insertBefore(card, afterEl);
                updateTotals();
                ensureMissingCatalogChips();
            }
        });

        // remove + qty change
        deck.addEventListener('click', e => {
            if (READONLY) return;
            const btn = e.target.closest('.dnd-remove');
            if (btn) { btn.closest('.dnd-item')?.remove(); updateTotals(); ensureMissingCatalogChips(); }
        });
        deck.addEventListener('input', e => {
            if (READONLY) { // keep displayed value unchanged if someone tries to type
                if (e.target.classList.contains('qty-input')) {
                    e.target.value = e.target.getAttribute('value') || e.target.value;
                }
                return;
            }
            if (e.target.classList.contains('qty-input')) updateTotals();
        });

        // search filter (filters both lanes)
        search?.addEventListener('input', e => {
            const term = e.target.value.trim().toLowerCase();
            wrap.querySelectorAll('.dnd-src').forEach(chip => {
                const name = (chip.dataset.name || chip.textContent || '').toLowerCase();
                chip.style.display = name.includes(term) ? '' : 'none';
            });
        });

        function getAfterElement(container, y) {
            const els = [...container.querySelectorAll('.dnd-item:not(.dragging)')];
            return els.reduce((closest, child) => {
                const box = child.getBoundingClientRect();
                const offset = y - box.top - box.height / 2;
                if (offset < 0 && offset > closest.offset) { return { offset, element: child }; }
                else { return closest; }
            }, { offset: Number.NEGATIVE_INFINITY }).element || null;
        }

        // ---- Deck render helpers ----
        function clearDeck() {
            deck.querySelectorAll('.dnd-item').forEach(n => n.remove());
        }

        function renderDeck(details = []) {
            clearDeck();
            details.forEach(d => {
                const id = String(d.furnitureID ?? d.FurnitureID ?? '');
                const name = String(d.furnitureName ?? d.FurnitureName ?? `ID ${id}`);
                const qty = Number(d.amount ?? d.Amount ?? 1);
                if (!id) return;
                deck.appendChild(makeDeckRow({ id, name, qty }));
            });
            updateTotals();
            ensureMissingCatalogChips();
            const empty = document.getElementById('mf_empty');
            if (empty) empty.classList.toggle('d-none', deck.querySelector('.dnd-item') !== null);
        }

        function setRemark(text) {
            const ta = document.getElementById('mf_remark');
            if (ta) ta.value = text ?? '';
        }

        // central read-only toggler
        function setReadOnly(ro) {
            READONLY = !!ro;

            const modal = document.getElementById('mdlEditUnitFurniture');
            modal?.classList.toggle('uf-readonly', READONLY);

            // disable qty + remove + draggable on rows
            deck.querySelectorAll('.dnd-item').forEach(r => r.setAttribute('draggable', String(!READONLY)));
            deck.querySelectorAll('.qty-input').forEach(i => { i.disabled = READONLY; i.readOnly = READONLY; });
            deck.querySelectorAll('.dnd-remove').forEach(b => { b.disabled = READONLY; });

            // freeze catalog chips
            wrap.querySelectorAll('.dnd-src').forEach(chip => chip.setAttribute('draggable', String(!READONLY)));

            // disable search while locked (optional UX)
            if (search) search.disabled = READONLY;

            // NEW: disable remark + save
            const remark = document.getElementById('mf_remark');
            if (remark) {
                remark.disabled = READONLY;
                remark.readOnly = READONLY;
            }

            const saveBtn = document.getElementById('btnEditUFSave');
            if (saveBtn) {
                saveBtn.disabled = READONLY;
                // optional visual cue
                saveBtn.classList.toggle('disabled', READONLY);
                saveBtn.title = READONLY ? 'Approved unit — cannot save' : '';
            }

            // lock hint
            const hdr = modal?.querySelector('.modal-header');
            let hint = hdr?.querySelector('.uf-lock-hint');
            if (hdr && !hint) {
                hint = document.createElement('div');
                hint.className = 'uf-lock-hint ms-3 small text-muted';
                hint.innerHTML = '<i class="bi bi-lock-fill me-1"></i> This unit is approved (check status is pass) and cannot be edited.';
                hdr.appendChild(hint);
            }
            hint?.classList.toggle('d-none', !READONLY);
        }

        // expose to opener
        window.__ufDeck = { render: renderDeck, clear: clearDeck, setRemark: setRemark, setReadOnly: setReadOnly };

        // initial paint
        updateTotals();
        ensureMissingCatalogChips();
    })();

    // ===== Save Change (Edit Unit Furniture modal) =====
    document.addEventListener('DOMContentLoaded', () => {
        const btnSave = document.getElementById('btnEditUFSave');
        if (!btnSave) return;

        btnSave.addEventListener('click', onEditUFSave);
    });

    async function onEditUFSave() {
        // guard: if modal is read-only, do nothing
        if (document.getElementById('mdlEditUnitFurniture')?.classList.contains('uf-readonly')) return;

        const projectId = document.getElementById('hfProjectID')?.value?.trim() || '';
        const unitIdStr = document.getElementById('edit_unit_id')?.value?.trim() || '';
        const remark = document.getElementById('mf_remark')?.value ?? '';

        // collect deck items → [{ id: "1", qty: 2 }, ...]
        const deck = document.getElementById('unitDeck');
        const items = [...deck.querySelectorAll('.dnd-item')].map(r => {
            const id = r.dataset.id ?? '';
            const qty = Number(r.querySelector('.qty-input')?.value ?? 0);
            return { id, qty: Number.isFinite(qty) ? qty : 0 };
        }).filter(x => x.id && x.qty > 0);

        // basic validation to match controller rules
        if (!projectId) { showWarning?.('Please select a project first.'); return; }
        if (!unitIdStr) { showWarning?.('No UnitID found.'); return; }
        if (items.length === 0) { showWarning?.('At least one item is required (Qty > 0).'); return; }

        // payload must match UpdateFurnitureProjectMappingRequest
        const payload = {
            ProjectID: projectId,
            UnitID: unitIdStr,      // Guid as string is fine for [FromBody]
            Remark: remark,
            Furnitures: items       // [{ id, qty }]
        };

        // lock UI
        const btn = document.getElementById('btnEditUFSave');
        const prevHTML = btn.innerHTML;
        btn.disabled = true;
        btn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Saving...';

        try {
            const resp = await fetch(baseUrl + 'FurnitureAndUnitFurniture/SaveChangeFurnitureProjectMapping', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            const json = await resp.json().catch(() => ({}));

            if (resp.status === 401) {
                errorMessage?.(json?.message || 'Unauthorized');
                return;
            }
            if (!resp.ok || json?.success === false) {
                errorMessage?.(json?.message || 'Save failed');
                return;
            }

            // success
            successMessage?.('Saved successfully.');
            // refresh the main table (if you have this function)
            typeof loadUnitFurnitureTable === 'function' && loadUnitFurnitureTable();

            // close modal
            const el = document.getElementById('mdlEditUnitFurniture');
            bootstrap.Modal.getInstance(el)?.hide();

        } catch (err) {
            console.error('SaveChangeFurnitureProjectMapping error:', err);
            errorMessage?.('เกิดข้อผิดพลาดระหว่างบันทึก');
        } finally {
            btn.disabled = false;
            btn.innerHTML = prevHTML;
        }
    }


    // ===========================
    // Furniture CRUD (front-end)
    // ===========================
    (function () {
        const furnitureList = document.getElementById('furnitureList'); // left panel list
        const catalogLane = document.getElementById('mf_catalog');    // chips in the edit modal

        // --- helpers ---
        function escHtml(s) {
            return String(s ?? '')
                .replaceAll('&', '&amp;').replaceAll('<', '&lt;')
                .replaceAll('>', '&gt;').replaceAll('"', '&quot;')
                .replaceAll("'", '&#39;');
        }

        function updateTotalBadge() {
            try {
                const el = document.querySelector('.card-header small.text-muted');
                if (!el) return;
                const count = furnitureList?.querySelectorAll('.list-group-item').length ?? 0;
                el.textContent = `Total ${count}`;
            } catch { }
        }

        // ===========================
        // Row / Chip builders
        // ===========================
        function buildFurnitureRow(id, name) {
            const div = document.createElement('div');
            div.className = 'list-group-item d-flex justify-content-between align-items-center';
            div.dataset.id = String(id);
            div.dataset.name = String(name);
            div.id = `frow_${id}`;
            div.innerHTML = `
      <div class="d-flex align-items-center gap-2">
        <input type="checkbox"
               class="form-check-input chkFurniture"
               value="${escHtml(id)}"
               style="border: 2px solid grey; accent-color: #0d6efd;" />
        <span class="f-name">${escHtml(name)}</span>
      </div>
      <div class="btn-group">
        <button type="button"
                class="btn btn-light rounded-circle btn-icon btn-edit"
                id="btnEditFurniture_${escHtml(id)}"
                title="Edit"
                onclick="openFurnitureEdit('${String(id).replace(/'/g, "\\'")}')">
          <i class="bi bi-pencil"></i>
        </button>
        &nbsp;
        <button type="button"
                class="btn btn-light rounded-circle btn-icon text-danger btn-delete"
                id="btnDeleteFurniture_${escHtml(id)}"
                title="Delete"
                onclick="openFurnitureDelete('${String(id).replace(/'/g, "\\'")}')">
          <i class="bi bi-trash"></i>
        </button>
      </div>`;
            return div;
        }

        function buildCatalogChip(id, name) {
            const chip = document.createElement('div');
            chip.className = 'chip chip-ghost dnd-src';
            chip.setAttribute('draggable', 'true');
            chip.dataset.id = String(id);
            chip.dataset.name = String(name);
            chip.textContent = name;
            return chip;
        }


        // ===========================
        // FULL refresh from server
        // ===========================
        async function fetchFurnitureList() {
            // CHANGE this route if your API is different
            const resp = await fetch(baseUrl + 'FurnitureAndUnitFurniture/GetFurnitureList', { method: 'POST' });
            const json = await resp.json().catch(() => ({}));
            if (!resp.ok || json?.success === false || !Array.isArray(json?.data)) {
                throw new Error(json?.message || 'Fetch list failed');
            }
            // normalize: [{id,name}]
            return json.data.map(x => ({
                id: String(x.id ?? x.ID ?? x.ValueInt ?? x.Value ?? ''),
                name: String(x.name ?? x.Name ?? x.Text ?? '')
            })).filter(x => x.id);
        }

        function rebuildFurnitureUIFromList(items) {
            // clear
            if (furnitureList) furnitureList.innerHTML = '';
            if (catalogLane) catalogLane.innerHTML = '';

            // build
            const fragList = document.createDocumentFragment();
            const fragChips = document.createDocumentFragment();

            items.forEach(it => {
                fragList.appendChild(buildFurnitureRow(it.id, it.name));
                fragChips.appendChild(buildCatalogChip(it.id, it.name));
            });

            furnitureList?.appendChild(fragList);
            catalogLane?.appendChild(fragChips);
            updateTotalBadge();
        }

        async function refreshFurnitureUI() {
            try {
                const items = await fetchFurnitureList();
                rebuildFurnitureUIFromList(items);
            } catch (e) {
                // if no endpoint yet, fallback to page reload
                console.warn('refreshFurnitureUI failed, fallback reload:', e?.message);
                location.reload();
            }
        }

        // ===========================
        // Inline onclick helpers (global)
        // ===========================
        function getFurnitureRowAndName(id) {
            const row = document.querySelector(`#furnitureList .list-group-item[data-id="${CSS.escape(String(id))}"]`);
            const name = row?.dataset?.name || row?.querySelector('.f-name')?.textContent?.trim() || '';
            return { row, name };
        }

        window.openFurnitureEdit = function (id) {
            const { name } = getFurnitureRowAndName(id);
            const m = document.getElementById('mdlFurnitureUpsert');
            if (!m) return;

            m.querySelector('#fu_modal_title').textContent = id ? 'Edit furniture' : 'Add furniture';
            m.querySelector('#fu_id').value = id || '';
            const input = m.querySelector('#fu_name');
            input.value = name || '';
            input.classList.remove('is-invalid');

            bootstrap.Modal.getOrCreateInstance(m).show();
        };

        window.openFurnitureDelete = function (id) {
            const { name } = getFurnitureRowAndName(id);
            const m = document.getElementById('mdlFurnitureDelete');
            if (!m) return;

            m.querySelector('#fd_id').value = id;
            m.querySelector('#fd_name').textContent = name || '';

            bootstrap.Modal.getOrCreateInstance(m).show();
        };

        // ===========================
        // Add button (open Upsert as Create)
        // ===========================
        document.getElementById('btnFurnitureAdd')?.addEventListener('click', () => {
            openFurnitureEdit(''); // empty id => create
        });

        // ===========================
        // Save (Create/Update)
        // ===========================
        document.getElementById('btnFurnitureSave')?.addEventListener('click', async (ev) => {
            const btn = ev.currentTarget;
            const m = document.getElementById('mdlFurnitureUpsert');
            const id = (m.querySelector('#fu_id')?.value || '').trim();
            const nameInput = m.querySelector('#fu_name');
            const name = (nameInput?.value || '').trim();

            if (!name) {
                nameInput?.classList.add('is-invalid');
                return;
            }
            nameInput?.classList.remove('is-invalid');

            // loading UI
            const label = btn.querySelector('.label') || btn;
            const spin = btn.querySelector('.spinner-border');
            label.classList?.add('d-none'); spin?.classList?.remove('d-none'); btn.disabled = true;

            try {
                const fd = new FormData();
                if (id) fd.append('id', id);
                fd.append('name', name);

                const url = id
                    ? (baseUrl + 'FurnitureAndUnitFurniture/UpdateFurniture')
                    : (baseUrl + 'FurnitureAndUnitFurniture/CreateFurniture');

                const resp = await fetch(url, { method: 'POST', body: fd });
                const json = await resp.json().catch(() => ({}));

                if (!resp.ok || json?.success === false) {
                    throw new Error(json?.message || 'Save failed');
                }

                bootstrap.Modal.getInstance(m)?.hide();

                // 🔄 always refresh from server so ordering/count/chips stay correct
                await refreshFurnitureUI();

                successMessage('Saved successfully');
            } catch (err) {
                showWarning(err?.message || 'Save failed');
            } finally {
                label.classList?.remove('d-none'); spin?.classList?.add('d-none'); btn.disabled = false;
            }
        });

        // ===========================
        // Delete (Confirm)
        // ===========================
        document.getElementById('btnFurnitureDeleteConfirm')?.addEventListener('click', async (ev) => {
            const btn = ev.currentTarget;
            const m = document.getElementById('mdlFurnitureDelete');
            const id = (m.querySelector('#fd_id')?.value || '').trim();

            const label = btn.querySelector('.label') || btn;
            const spin = btn.querySelector('.spinner-border');
            label.classList?.add('d-none'); spin?.classList?.remove('d-none'); btn.disabled = true;

            try {
                const fd = new FormData();
                fd.append('id', id);

                const resp = await fetch(baseUrl + 'FurnitureAndUnitFurniture/DeleteFurniture', {
                    method: 'POST', body: fd
                });
                const json = await resp.json().catch(() => ({}));

                if (!resp.ok || json?.success === false) {
                    throw new Error(json?.message || 'Delete failed');
                }

                bootstrap.Modal.getInstance(m)?.hide();

                // 🔄 refresh from server after delete
                await refreshFurnitureUI();

                successMessage('Deleted');
            } catch (err) {
                showWarning(err?.message || 'Delete failed');
            } finally {
                label.classList?.remove('d-none'); spin?.classList?.add('d-none'); btn.disabled = false;
            }
        });

    })();


})();
