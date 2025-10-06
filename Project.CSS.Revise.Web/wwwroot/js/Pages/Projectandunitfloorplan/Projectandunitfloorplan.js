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

        // header count
        const counter = $id('count_Floor_plan_lists');
        if (counter) counter.textContent = `ทั้งหมด ${totalCount ?? (Array.isArray(list) ? list.length : 0)}`;

        // body list
        const container = card.querySelector('.vertical-scroll .list-group');
        if (!container) return;

        if (!Array.isArray(list) || list.length === 0) {
            container.innerHTML = `<div class="list-group-item text-muted">— ไม่มีไฟล์ Floor plan —</div>`;
            bindCheckAll(false);
            return;
        }

        const rows = list.map(item => {
            const id = escapeHtml(item.ID ?? '');
            const fileName = escapeHtml(item.FileName ?? '');
            const filePath = escapeHtml(item.FilePath ?? '');
            const mime = String(item.MimeType ?? '').toLowerCase();

            // If MIME is image/* -> try real img with JS fallback on error
            // Else -> show icon immediately
            const thumbHtml = isImageMime(mime)
                ? `<img src="${filePath}" width="50" height="50" class="img-thumbnail thumb-img" alt="${fileName}" />`
                : thumbIconHtml(fileName);

            // Wrap in <a> for images only (so click opens viewer). If not image, no link.
            const thumbWrapper = isImageMime(mime)
                ? `<a href="javascript:void(0);" onclick='openViewer && openViewer("${filePath}")'>${thumbHtml}</a>`
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

        // Wire fallback for broken images
        wireThumbFallback(container);

        // bind check-all
        bindCheckAll(true);
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

        // สร้างแถว Check-all (ครั้งเดียว)
        ensureUnitCheckAllRow();

        const group = card.querySelector('.vertical-scroll .list-group');
        if (!group) return;

        // อัปเดตตัวนับทั้งหมด
        const total = totalCount ?? (Array.isArray(list) ? list.length : 0);
        ensureUnitCounter(total);

        if (!Array.isArray(list) || list.length === 0) {
            group.innerHTML = `<div class="list-group-item text-muted">— ไม่พบ Unit —</div>`;
            const chkAll = $id('chkAllUnit');
            if (chkAll) {
                chkAll.checked = false;
                chkAll.disabled = true;
            }
            wireUnitSearch([]); // clear search binding
            return;
        }

        const rows = list.map(u => {
            const id = escapeHtml(u.ID ?? '');
            const code = escapeHtml(u.UnitCode ?? '');
            const type = escapeHtml(u.UnitType ?? ''); // ✅ NEW: unit type
            const collapseId = 'collapse_' + slug(code);

            // แสดง badge ของ Unit Type ถ้ามีค่า
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
                <div class="file-list"><!-- unit files here (future) --></div>
            </div>
        </div>`;
        }).join('');

        group.innerHTML = rows;

        // เปิดการทำงาน check-all
        const chkAll = $id('chkAllUnit');
        if (chkAll) {
            chkAll.disabled = false;
            chkAll.checked = false;
        }
        bindUnitCheckAll();

        // live search
        wireUnitSearch(list);
    }


    function ensureUnitCounter(total) {
        const card = $id('card_unit');
        if (!card) return;

        // เอา counter ไปไว้ข้างหัวข้อ (ถ้ายังไม่มี ให้สร้าง)
        let header = card.querySelector('.card-header');
        if (!header) return;

        let title = header.querySelector('h4');
        if (!title) return;

        let cnt = header.querySelector('#count_unit_total');
        if (!cnt) {
            cnt = document.createElement('small');
            cnt.id = 'count_unit_total';
            cnt.className = 'text-muted ms-2';
            title.insertAdjacentElement('afterend', cnt);
        }
        cnt.textContent = `ทั้งหมด ${total}`;
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
})();
