// wwwroot/js/Pages/Projectandunitfloorplan/Projectandunitfloorplan.js
// Project & Unit Floorplan – filter relation (Project -> UnitType) + Search & Render Cards
// Requires: Choices.js

(function () {
    // ====== private state ======
    let choicesProject;
    let choicesUnitType;
    let acUnitType; // AbortController
    let acSearch;   // AbortController for search

    // ====== utils ======
    const $id = (x) => document.getElementById(x);

    const toFormData = (obj) => {
        const fd = new FormData();
        Object.entries(obj || {}).forEach(([k, v]) => fd.append(k, v ?? ''));
        return fd;
    };

    const setChoicesEmpty = (inst, text = '— ไม่มีข้อมูล —') => {
        if (!inst) return;
        try {
            inst.clearStore();
            inst.setChoices([{ value: '', label: text, disabled: true }], 'value', 'label', true);
            inst.removeActiveItems();
        } catch { }
    };

    const setChoicesLoading = (inst, text = 'กำลังโหลด...') => {
        if (!inst) return;
        try {
            inst.clearStore();
            inst.setChoices([{ value: '', label: text, disabled: true }], 'value', 'label', true);
            inst.removeActiveItems();
        } catch { }
    };

    const fillChoices = (inst, items, reset = true) => {
        if (!inst) return;
        inst.clearStore();
        inst.setChoices(items, 'value', 'label', true);
        if (reset) inst.removeActiveItems();
    };

    const baseUrl = (typeof window.baseUrl === 'string' && window.baseUrl) ? window.baseUrl : '/';

    const escapeHtml = (s) => String(s ?? '')
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');

    const slug = (s) => String(s ?? '').replace(/[^a-zA-Z0-9_-]/g, '_');

    // ====== filter chain: Project -> UnitType ======
    async function onProjectChanged() {
        const projectId = choicesProject?.getValue(true);
        if (!projectId) {
            setChoicesEmpty(choicesUnitType, '— เลือก Unit Type —');
            return;
        }

        setChoicesLoading(choicesUnitType, 'กำลังโหลด Unit Type...');

        if (acUnitType) acUnitType.abort();
        acUnitType = new AbortController();

        try {
            const resp = await fetch(
                baseUrl + 'Projectandunitfloorplan/GetUnitTypeListByProjectID',
                { method: 'POST', body: toFormData({ ProjectID: projectId }), signal: acUnitType.signal }
            );
            if (!resp.ok) throw new Error('HTTP ' + resp.status);
            const json = await resp.json();

            const list = (json?.success && Array.isArray(json.data)) ? json.data : [];
            const items = list.map(u => ({
                value: String(u.ValueString ?? u.Value ?? u.UnitType ?? ''),
                label: String(u.Text ?? u.UnitType ?? '')
            })).filter(x => x.value && x.label);

            if (items.length) {
                fillChoices(choicesUnitType, items, true);
            } else {
                setChoicesEmpty(choicesUnitType, '— ไม่พบ Unit Type —');
            }
        } catch (err) {
            if (err?.name === 'AbortError') return;
            console.error('Load unit type failed:', err);
            setChoicesEmpty(choicesUnitType, 'โหลด Unit Type ล้มเหลว');
        }
    }

    // ====== SEARCH ======
    function getFilters() {
        const projectId = choicesProject?.getValue(true) || '';
        // Choices.js (multiple) -> getValue(true) ให้ array ของ value
        let unitTypeArr = [];
        try {
            const val = choicesUnitType?.getValue(true);
            unitTypeArr = Array.isArray(val) ? val : (val ? [val] : []);
        } catch { unitTypeArr = []; }
        const unitTypeCsv = unitTypeArr.join(',');

        return { projectId, unitTypeCsv };
    }

    async function onSearchClick(e) {
        e?.preventDefault?.();

        const btn = $id('BtnSearch');
        const { projectId, unitTypeCsv } = getFilters();

        if (!projectId) {
            // เลือก project ก่อน
            btn && btn.blur();
            alert('กรุณาเลือกโครงการ');
            return;
        }

        try {
            // cancel previous search if running
            if (acSearch) acSearch.abort();
            acSearch = new AbortController();

            // disable button + show busy
            if (btn) { btn.disabled = true; btn.dataset.originalHtml = btn.innerHTML; btn.innerHTML = '<i class="bi bi-hourglass-split"></i> Searching...'; }

            // parallel fetch: floorplans + units
            const [resFloor, resUnit] = await Promise.all([
                fetch(baseUrl + 'Projectandunitfloorplan/GetlistProjectFloorPlan', {
                    method: 'POST', body: toFormData({ ProjectID: projectId }), signal: acSearch.signal
                }),
                fetch(baseUrl + 'Projectandunitfloorplan/GetlistUnit', {
                    method: 'POST', body: toFormData({ ProjectID: projectId, UnitType: unitTypeCsv }), signal: acSearch.signal
                })
            ]);

            if (!resFloor.ok) throw new Error('Floorplan HTTP ' + resFloor.status);
            if (!resUnit.ok) throw new Error('Unit HTTP ' + resUnit.status);

            const [jFloor, jUnit] = await Promise.all([resFloor.json(), resUnit.json()]);

            const floorList = (jFloor?.success && Array.isArray(jFloor.data)) ? jFloor.data : [];
            const unitList = (jUnit?.success && Array.isArray(jUnit.data)) ? jUnit.data : [];

            renderFloorPlans(floorList, jFloor?.count ?? floorList.length);
            renderUnits(unitList, jUnit?.count ?? unitList.length);
        } catch (err) {
            if (err?.name !== 'AbortError') {
                console.error('Search failed:', err);
                toastError('ค้นหาล้มเหลว กรุณาลองใหม่');
            }
        } finally {
            if (btn) { btn.disabled = false; btn.innerHTML = btn.dataset.originalHtml || '<i class="icofont icofont-search"></i> Search'; }
        }
    }

    // ====== RENDER: card_project_floor_plan ======
    function renderFloorPlans(list, totalCount) {
        const card = $id('card_project_floor_plan');
        if (!card) return;

        const counter = $id('count_Floor_plan_lists');
        if (counter) counter.textContent = `ทั้งหมด ${totalCount ?? (Array.isArray(list) ? list.length : 0)}`;

        const container = card.querySelector('.vertical-scroll .list-group');
        if (!container) return;

        if (!Array.isArray(list) || list.length === 0) {
            container.innerHTML = `<div class="list-group-item text-muted">— ไม่มีไฟล์ Floor plan —</div>`;
            bindCheckAll(false);
            refreshMappingState();
            return;
        }

        const rows = list.map(item => {
            const id = escapeHtml(item.ID ?? '');
            const fileName = escapeHtml(item.FileName ?? '');

            // IMPORTANT: use raw path for encoding, then a separate HTML-escaped label
            const rawPath = item.FilePath ?? '';
            const mime = String(item.MimeType ?? '').toLowerCase();

            // encodeURI handles spaces/(), etc. (DON'T use encodeURIComponent on the whole path)
            const url = encodeURI(rawPath);

            const thumbHtml = isImageMime(mime)
                ? `<img src="${url}" width="50" height="50" class="img-thumbnail thumb-img" alt="${fileName}" />`
                : thumbIconHtml(fileName);

            const thumbWrapper = isImageMime(mime)
                ? `<a href="javascript:void(0);" onclick='openViewer && openViewer("${url}")'>${thumbHtml}</a>`
                : thumbHtml;

            return `
      <div class="list-group-item d-flex justify-content-between align-items-center list-hover-primary" data-id="${id}">
        <div class="d-flex align-items-center gap-2">
          <input class="form-check-input chk-floorplan" type="checkbox" />
          ${thumbWrapper}
          <span title="${fileName}">${fileName}</span>
        </div>
        <div class="btn-group">
          <button type="button" class="btn-e btn-edit" title="Edit">
            <i class="bi bi-pencil"></i>
          </button>
          &nbsp;
          <button type="button" class="btn-c btn-delete" title="Delete">
            <i class="bi bi-trash"></i>
          </button>
        </div>
      </div>`;
        }).join('');

        container.innerHTML = rows;

        if (typeof wireThumbFallback === 'function') wireThumbFallback(container);
        bindCheckAll(true);

        container.addEventListener('change', e => {
            if (e.target.classList.contains('chk-floorplan')) refreshMappingState();
        });
        $id('chkAllFloorplan')?.addEventListener('change', refreshMappingState);

        refreshMappingState();
    }


    function bindCheckAll(enable) {
        const chkAll = $id('chkAllFloorplan');
        const container = $id('card_project_floor_plan')?.querySelector('.vertical-scroll .list-group');
        if (!chkAll || !container) return;

        chkAll.checked = false;
        chkAll.disabled = !enable;

        chkAll.onchange = () => {
            const items = container.querySelectorAll('.chk-floorplan');
            items.forEach(chk => { chk.checked = chkAll.checked; });
        };
    }

    // ====== RENDER: card_unit ======
    function renderUnits(list, totalCount) {
        const card = $id('card_unit');
        if (!card) return;

        const group = card.querySelector('.vertical-scroll .list-group');
        if (!group) return;

        const total = Number(totalCount ?? (Array.isArray(list) ? list.length : 0)) || 0;
        ensureUnitCounter(total);

        if (!Array.isArray(list) || list.length === 0) {
            group.innerHTML = `<div class="list-group-item text-muted">— ไม่พบ Unit —</div>`;
            const chkAll = $id('chkAllUnit');
            if (chkAll) { chkAll.checked = false; chkAll.disabled = true; }
            wireUnitSearch([]);
            refreshMappingState();
            return;
        }

        const rows = list.map(u => {
            const id = escapeHtml(u.ID ?? '');
            const code = escapeHtml(u.UnitCode ?? '');
            const type = escapeHtml(u.UnitType ?? '');
            const collapseId = 'collapse_' + slug(code);

            const typeBadge = type
                ? `<span class="badge rounded-pill text-bg-secondary ms-2" title="Unit Type">${type}</span>`
                : '';

            return `
        <div class="list-group-item list-hover-primary" data-id="${id}" data-code="${code}" data-unittype="${type}">
            <div class="d-flex justify-content-between align-items-center">
                <div class="d-flex align-items-center gap-2">
                    <input class="form-check-input chk-unit" type="checkbox" />
                    <span>Unit : <strong>${code}</strong></span>
                    ${typeBadge}
                </div>
                <button class="btn-e btn-delete" role="button" data-bs-toggle="collapse" href="#${collapseId}" aria-expanded="false" aria-controls="${collapseId}">
                    <i class="icofont icofont-edit"></i>
                </button>
            </div>
            <div class="collapse mt-2" id="${collapseId}">
                <div class="file-list"></div>
            </div>
        </div>`;
        }).join('');

        group.innerHTML = rows;

        // Check-all
        const chkAll = $id('chkAllUnit');
        if (chkAll) { chkAll.disabled = false; chkAll.checked = false; }
        bindUnitCheckAll();

        // selection change -> refresh mapping state
        group.addEventListener('change', e => {
            if (e.target.classList.contains('chk-unit')) refreshMappingState();
        });
        chkAll?.addEventListener('change', refreshMappingState);

        wireUnitSearch(list);
        refreshMappingState();
    }

    function ensureUnitCounter(total) {
        const card = $id('card_unit');
        if (!card) return;

        // 1) If the counter already exists, just update it
        let cnt = card.querySelector('#count_unit_total');
        if (cnt) {
            cnt.textContent = `ทั้งหมด ${Number(total) || 0}`;
            return;
        }

        // 2) Otherwise, create it next to the first heading in the header
        const header = card.querySelector('.card-header');
        if (!header) return;

        // find any heading tag or a title element with .mb-0
        const title =
            header.querySelector('h1, h2, h3, h4, h5, h6') ||
            header.querySelector('.mb-0');

        if (!title) return;

        cnt = document.createElement('small');
        cnt.id = 'count_unit_total';
        cnt.className = 'text-muted ms-2';
        cnt.textContent = `ทั้งหมด ${Number(total) || 0}`;
        title.insertAdjacentElement('afterend', cnt);
    }

    function ensureUnitCheckAllRow() {
        const card = $id('card_unit');
        const vscroll = card?.querySelector('.vertical-scroll');
        const group = card?.querySelector('.vertical-scroll .list-group');
        if (!card || !vscroll || !group) return;

        // ถ้าเคยมีแล้วไม่ต้องสร้างซ้ำ
        if (vscroll.querySelector('#unitCheckAllRow')) return;

        const html = `
                    <div id="unitCheckAllRow" class="form-check mb-2">
                        <input class="form-check-input" type="checkbox" id="chkAllUnit" style="border:2px solid grey; accent-color:#0d6efd;" />
                        <label class="form-check-label fw-semibold" for="chkAllUnit">เลือกทั้งหมด</label>
                    </div>`;
        // แทรก check-all บนสุด เหนือ list-group
        vscroll.insertAdjacentHTML('afterbegin', html);
    }

    function bindUnitCheckAll() {
        const chkAll = $id('chkAllUnit');
        const group = $id('card_unit')?.querySelector('.vertical-scroll .list-group');
        if (!chkAll || !group) return;

        chkAll.checked = false;
        chkAll.onchange = () => {
            const items = group.querySelectorAll('.chk-unit');
            items.forEach(chk => { chk.checked = chkAll.checked; });
        };
    }

    function wireUnitSearch(dataList) {
        const input = $id('searchUnitInput');
        const group = $id('card_unit')?.querySelector('.vertical-scroll .list-group');
        if (!input || !group) return;

        input.oninput = () => {
            const q = (input.value || '').trim().toLowerCase();
            // กรองเฉพาะ item ที่มี data-code (ไม่ยุ่งกับแถว check-all)
            const items = group.querySelectorAll('.list-group-item[data-code]');

            if (!q) {
                items.forEach(el => el.classList.remove('d-none'));
                return;
            }

            items.forEach(el => {
                const code = (el.getAttribute('data-code') || '').toLowerCase();
                if (code.includes(q)) el.classList.remove('d-none');
                else el.classList.add('d-none');
            });
        };
    }

    // ====== INIT ======
    // replace your current init() with this
    async function init() {
        const projectEl = $id('projectSelect');
        const unitTypeEl = $id('ddl_unittype');
        if (!projectEl || !unitTypeEl) return;

        // Init Choices: Project (single)
        choicesProject = new Choices(projectEl, {
            placeholderValue: '🔍 พิมพ์ค้นหาโครงการ...',
            searchEnabled: true,
            itemSelectText: '',
            shouldSort: false
        });

        // Auto-select first project (if any)
        const firstOpt = projectEl.querySelector('option');
        if (firstOpt) choicesProject.setChoiceByValue(firstOpt.value);

        // Init Choices: UnitType (multiple)
        choicesUnitType = new Choices(unitTypeEl, {
            removeItemButton: true,
            searchEnabled: true,
            shouldSort: false,
            placeholder: true,
            placeholderValue: '— เลือก Unit Type —'
        });
        setChoicesEmpty(choicesUnitType, '— เลือก Unit Type —');

        // Bind change
        projectEl.addEventListener('change', onProjectChanged);

        // Auto-load Unit Types for the selected project
        await onProjectChanged();

        // Bind Search button (still usable for manual re-search)
        const btnSearch = $id('BtnSearch');
        btnSearch?.addEventListener('click', onSearchClick);

        // 🔥 Auto-search on first load (no event passed)
        await onSearchClick();

        // Collapse chevron
        const body = $id('filterBody');
        const chev = $id('filterChevron');
        if (body && chev) {
            body.addEventListener('shown.bs.collapse', () => chev.classList.replace('bi-chevron-down', 'bi-chevron-up'));
            body.addEventListener('hidden.bs.collapse', () => chev.classList.replace('bi-chevron-up', 'bi-chevron-down'));
        }
    }

    // simple toast fallback (replace with Swal.fire ได้)
    function toastError(msg) {
        try { console.warn(msg); } catch { }
    }

    // auto-init
    document.addEventListener('DOMContentLoaded', init);

    // expose for partial reload
    window.ProjectUnitFloorplan = { init, onSearchClick };

    // --- selection counters & mapping button state ---
    function getSelectedCounts() {
        const fp = document.querySelectorAll('#card_project_floor_plan .chk-floorplan:checked').length;
        const un = document.querySelectorAll('#card_unit .chk-unit:checked').length;
        return { fp, un };
    }

    function refreshMappingState() {
        const { fp, un } = getSelectedCounts();
        const label = document.getElementById('mapCounts');
        const btn = document.getElementById('btnSaveMapping');

        if (label) label.textContent = `${fp} floorplan${fp === 1 ? '' : 's'} • ${un} unit${un === 1 ? '' : 's'}`;
        if (btn) btn.disabled = !(fp > 0 && un > 0);
    }


    document.addEventListener('DOMContentLoaded', () => {
        document.getElementById('btnSaveMapping')?.addEventListener('click', onSaveMapping);
    });

    async function onSaveMapping() {
        const btn = document.getElementById('btnSaveMapping');
        if (!btn || btn.disabled) return;

        const projectId = choicesProject?.getValue(true) || '';
        const floorplanIds = [...document.querySelectorAll('#card_project_floor_plan .chk-floorplan:checked')]
            .map(chk => chk.closest('.list-group-item')?.dataset.id)
            .filter(Boolean);
        const unitIds = [...document.querySelectorAll('#card_unit .chk-unit:checked')]
            .map(chk => chk.closest('.list-group-item')?.dataset.id)
            .filter(Boolean);

        // client-side guards
        if (!projectId) {
            showWarning('กรุณาเลือกโครงการก่อนบันทึก');
            return;
        }
        if (floorplanIds.length === 0) {
            showWarning('กรุณาเลือก Floor plan อย่างน้อย 1 รายการ');
            return;
        }
        if (unitIds.length === 0) {
            showWarning('กรุณาเลือก Unit อย่างน้อย 1 รายการ');
            return;
        }

        // Build FormData with arrays so ASP.NET binds to List<Guid>
        const fd = new FormData();
        fd.append('ProjectID', projectId);
        floorplanIds.forEach(id => fd.append('FloorPlanIDs', id));
        unitIds.forEach(id => fd.append('UnitIDs', id));

        const originalHtml = btn.innerHTML;
        try {
            btn.disabled = true;
            btn.innerHTML = '<i class="bi bi-hourglass-split me-1"></i> Saving...';
            showLoading();

            const resp = await fetch(baseUrl + 'Projectandunitfloorplan/SaveMapping', {
                method: 'POST',
                body: fd
            });

            const json = await resp.json().catch(() => ({}));

            if (!resp.ok) {
                errorMessage('บันทึกไม่สำเร็จ (HTTP ' + resp.status + ')', 'Save Failed');
                return;
            }

            if (json && json.success) {
                // server returns { success, message, selectedFloorPlans?, selectedUnits? }
                successMessage(json.message || 'บันทึก Mapping สำเร็จ', 'Success');
            } else {
                // show server validation message if provided
                errorMessage(json?.message || 'บันทึกไม่สำเร็จ กรุณาลองใหม่', 'Save Failed');
            }
        } catch (err) {
            console.error(err);
            errorMessage('เกิดข้อผิดพลาดระหว่างบันทึก กรุณาลองใหม่อีกครั้ง', 'Save Failed');
        } finally {
            hideLoading();
            btn.innerHTML = originalHtml;
            btn.disabled = false;
            refreshMappingState(); // recalc middle dock button state
        }
    }


})();
