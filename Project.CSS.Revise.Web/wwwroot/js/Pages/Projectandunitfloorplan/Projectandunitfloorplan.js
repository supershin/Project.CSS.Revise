// wwwroot/js/Pages/Projectandunitfloorplan/Projectandunitfloorplan.js
// Project & Unit Floorplan – filter relation (Project -> UnitType) + Search & Render Cards
// Requires: Choices.js + Bootstrap JS

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

        setHiddenProject(projectId);

        if (!projectId) {
            btn && btn.blur();
            alert('กรุณาเลือกโครงการ');
            return;
        }

        try {
            if (acSearch) acSearch.abort();
            acSearch = new AbortController();

            if (btn) { btn.disabled = true; btn.dataset.originalHtml = btn.innerHTML; btn.innerHTML = '<i class="bi bi-hourglass-split"></i> Searching...'; }

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

    const setHiddenProject = (val) => {
        const h = $id('hd_ProjectID');
        if (h) h.value = val || '';
    };

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
            const rawPath = item.FilePath ?? '';
            const mime = String(item.MimeType ?? '').toLowerCase();

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
            const cnt = Number(u.Cnt_FloorPlan ?? 0) || 0;
            const collapseId = 'collapse_' + slug(code);

            const typeBadge = `<span class="badge rounded-pill text-bg-info ms-2" title="Unit Type">Type: ${type || '-'}</span>`;
            const countBadge = `<span class="badge rounded-pill text-bg-secondary ms-2 badge-imgcount" title="จำนวนรูปภาพ">
                                <i class="bi bi-images me-1"></i><span class="imgcount-val">${cnt}</span>
                            </span>`;

            return `
        <div class="list-group-item list-hover-primary" data-id="${id}" data-code="${code}" data-unittype="${type}">
            <div class="d-flex justify-content-between align-items-center">
                <div class="d-flex align-items-center gap-2">
                    <input class="form-check-input chk-unit" type="checkbox" />
                    <span>Unit : <strong>${code}</strong></span>
                    ${typeBadge}
                    ${countBadge}
                </div>
                <button class="btn-e btn-delete"
                        role="button"
                        data-bs-toggle="collapse"
                        href="#${collapseId}"
                        aria-expanded="false"
                        aria-controls="${collapseId}">
                    <i class="icofont icofont-search"></i>
                </button>
            </div>
            <div class="collapse mt-2" id="${collapseId}">
                <div class="file-list"><!-- filled on demand --></div>
            </div>
        </div>`;
        }).join('');

        group.innerHTML = rows;

        const chkAll = $id('chkAllUnit');
        if (chkAll) { chkAll.disabled = false; chkAll.checked = false; }
        bindUnitCheckAll();

        group.addEventListener('change', e => {
            if (e.target.classList.contains('chk-unit')) refreshMappingState();
        });
        chkAll?.addEventListener('change', refreshMappingState);

        group.querySelectorAll('.collapse').forEach(col => {
            col.addEventListener('show.bs.collapse', async () => {
                const parent = col.closest('.list-group-item');
                const unitId = parent?.getAttribute('data-id');
                const fileListEl = col.querySelector('.file-list');
                if (!unitId || !fileListEl) return;

                const count = await loadUnitFloorPlans(unitId, fileListEl);
                if (typeof count === 'number') {
                    const badge = parent.querySelector('.badge-imgcount .imgcount-val');
                    if (badge) badge.textContent = String(count);
                }
            }, { once: false });
        });

        wireUnitSearch(list);
        refreshMappingState();
    }

    async function loadUnitFloorPlans(unitId, fileListEl) {
        if (fileListEl.dataset.loaded === '1') return Number(fileListEl.dataset.count || 0);

        fileListEl.innerHTML = `
        <div class="text-muted small d-flex align-items-center gap-2">
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            กำลังโหลด...
        </div>`;

        try {
            const fd = new FormData();
            fd.append('Unit', unitId);

            const resp = await fetch(baseUrl + 'Projectandunitfloorplan/GetListFloorPlansByUnit', {
                method: 'POST',
                body: fd
            });
            const j = await resp.json();
            const list = (j?.success && Array.isArray(j.data)) ? j.data : [];

            if (!list.length) {
                fileListEl.innerHTML = `<div class="text-muted small">— ไม่มีไฟล์ Floor plan —</div>`;
                fileListEl.dataset.loaded = '1';
                fileListEl.dataset.count = '0';
                return 0;
            }

            const itemsHtml = list.map(fp => {
                const mapId = escapeHtml(fp.ID ?? '');
                const name = escapeHtml(fp.FileName ?? '');
                const raw = fp.FilePath ?? '';
                const mime = String(fp.MimeType ?? '').toLowerCase();
                const url = encodeURI(String(raw || ''));

                const thumbImg = isImageMime(mime)
                    ? `<img src="${url}" width="50" height="50" class="img-thumbnail thumb-img" alt="${name}" />`
                    : thumbIconHtml(name);

                const thumbWrapped = isImageMime(mime)
                    ? `<a class="viewer-link" href="javascript:void(0);" onclick='openViewer && openViewer("${url}")' title="${name}">${thumbImg}</a>`
                    : thumbImg;

                return `
                <div class="file-item d-inline-flex flex-column align-items-center me-2 mb-2" data-mapid="${mapId}">
                    ${thumbWrapped}
                    <button type="button" class="btn btn-sm text-danger file-remove" title="Remove file"
                            onclick="onRemoveUnitFloorPlan('${mapId}', this)">
                        <i class="fa fa-times-circle"></i>
                    </button>
                    <div class="text-center small mt-1" title="${name}">${name}</div>
                </div>`;
            }).join('');

            fileListEl.innerHTML = itemsHtml;

            if (typeof wireThumbFallback === 'function') wireThumbFallback(fileListEl);

            fileListEl.dataset.loaded = '1';
            fileListEl.dataset.count = String(list.length);
            return list.length;
        } catch (err) {
            console.error(err);
            fileListEl.innerHTML = `<div class="text-danger small">โหลดไฟล์ไม่สำเร็จ</div>`;
            fileListEl.dataset.loaded = '1';
            fileListEl.dataset.count = '0';
            return 0;
        }
    }

    async function onRemoveUnitFloorPlan(mapId, btn) {
        if (!mapId) return;

        if (typeof Swal !== 'undefined') {
            const res = await Swal.fire({
                icon: 'question',
                title: 'Remove this image?',
                text: 'This will unmap the floor plan from the unit.',
                showCancelButton: true,
                confirmButtonText: 'Remove',
                cancelButtonText: 'Cancel',
                buttonsStyling: false,
                customClass: { confirmButton: 'btn btn-danger', cancelButton: 'btn btn-secondary ms-2' }
            });
            if (!res.isConfirmed) return;
        } else if (!confirm('Remove this image?')) {
            return;
        }

        try {
            showLoading?.();

            const fd = new FormData();
            fd.append('id', mapId);

            const resp = await fetch(baseUrl + 'Projectandunitfloorplan/RemoveUnitFloorPlan', {
                method: 'POST',
                body: fd
            });
            const json = await resp.json().catch(() => ({}));

            if (!resp.ok || json?.success !== true) {
                errorMessage?.(json?.message || 'Remove failed.', 'Remove Failed');
                return;
            }

            const item = btn.closest('.file-item');
            const listWrap = btn.closest('.file-list');
            if (item && listWrap) {
                item.remove();

                const newCount = listWrap.querySelectorAll('.file-item').length;
                listWrap.dataset.count = String(newCount);

                const parentItem = listWrap.closest('.list-group-item');
                const badge = parentItem?.querySelector('.badge-imgcount .imgcount-val');
                if (badge) badge.textContent = String(newCount);

                if (newCount === 0) {
                    listWrap.innerHTML = `<div class="text-muted small">— ไม่มีไฟล์ Floor plan —</div>`;
                }
            }

            successMessage?.(json?.message || 'Removed.', 'Success');
        } catch (err) {
            console.error(err);
            errorMessage?.('Remove failed. Please try again.', 'Remove Failed');
        } finally {
            hideLoading?.();
        }
    }

    // expose for inline onclick
    window.onRemoveUnitFloorPlan = onRemoveUnitFloorPlan;

    function ensureUnitCounter(total) {
        const card = $id('card_unit');
        if (!card) return;

        let cnt = card.querySelector('#count_unit_total');
        if (cnt) {
            cnt.textContent = `ทั้งหมด ${Number(total) || 0}`;
            return;
        }

        const header = card.querySelector('.card-header');
        if (!header) return;

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

        if (vscroll.querySelector('#unitCheckAllRow')) return;

        const html = `
                    <div id="unitCheckAllRow" class="form-check mb-2">
                        <input class="form-check-input" type="checkbox" id="chkAllUnit" style="border:2px solid grey; accent-color:#0d6efd;" />
                        <label class="form-check-label fw-semibold" for="chkAllUnit">เลือกทั้งหมด</label>
                    </div>`;
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

    function wireUnitSearch() {
        const input = $id('searchUnitInput');
        const group = $id('card_unit')?.querySelector('.vertical-scroll .list-group');
        if (!input || !group) return;

        input.oninput = () => {
            const q = (input.value || '').trim().toLowerCase();
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
    async function init() {
        const projectEl = $id('projectSelect');
        const unitTypeEl = $id('ddl_unittype');
        if (!projectEl || !unitTypeEl) return;

        // keep hidden project up-to-date immediately
        setHiddenProject(projectEl.value || '');
        projectEl.addEventListener('change', () => setHiddenProject(projectEl.value || ''));

        // Choices: Project (single)
        choicesProject = new Choices(projectEl, {
            placeholderValue: '🔍 พิมพ์ค้นหาโครงการ...',
            searchEnabled: true,
            itemSelectText: '',
            shouldSort: false
        });

        // Auto-select first project (if any)
        const firstOpt = projectEl.querySelector('option');
        if (firstOpt) choicesProject.setChoiceByValue(firstOpt.value);

        // Choices: UnitType (multiple)
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

        // Auto-load Unit Types
        await onProjectChanged();

        // Search button
        const btnSearch = $id('BtnSearch');
        btnSearch?.addEventListener('click', onSearchClick);

        // Auto-search first load
        await onSearchClick();

        // Collapse chevron
        const body = $id('filterBody');
        const chev = $id('filterChevron');
        if (body && chev) {
            body.addEventListener('shown.bs.collapse', () => chev.classList.replace('bi-chevron-down', 'bi-chevron-up'));
            body.addEventListener('hidden.bs.collapse', () => chev.classList.replace('bi-chevron-up', 'bi-chevron-down'));
        }

        // --- Upload modal wiring (Add button) ---
        wireUploadModal();
    }

    function toastError(msg) {
        try { console.warn(msg); } catch { }
    }

    document.addEventListener('DOMContentLoaded', init);

    window.ProjectUnitFloorplan = { init, onSearchClick };

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

        const fd = new FormData();
        fd.append('ProjectID', projectId);
        floorplanIds.forEach(id => fd.append('FloorPlanIDs', id));
        unitIds.forEach(id => fd.append('UnitIDs', id));

        const originalHtml = btn.innerHTML;
        try {
            btn.disabled = true;
            btn.innerHTML = '<i class="bi bi-hourglass-split me-1"></i> Saving...';
            showLoading?.();

            const resp = await fetch(baseUrl + 'Projectandunitfloorplan/SaveMapping', {
                method: 'POST',
                body: fd
            });

            const json = await resp.json().catch(() => ({}));

            if (!resp.ok) {
                errorMessage?.('บันทึกไม่สำเร็จ (HTTP ' + resp.status + ')', 'Save Failed');
                return;
            }

            if (json && json.success) {
                successMessage?.(json.message || 'บันทึก Mapping สำเร็จ', 'Success');
            } else {
                errorMessage?.(json?.message || 'บันทึกไม่สำเร็จ กรุณาลองใหม่', 'Save Failed');
            }
        } catch (err) {
            console.error(err);
            errorMessage?.('เกิดข้อผิดพลาดระหว่างบันทึก กรุณาลองใหม่อีกครั้ง', 'Save Failed');
        } finally {
            hideLoading?.();
            btn.innerHTML = originalHtml;
            btn.disabled = false;
            refreshMappingState();
        }
    }

    // ====== Upload modal (Add -> pick multiple -> rename -> upload) ======
    function wireUploadModal() {
        const $ = (id) => document.getElementById(id);
        const addBtn = $('btnFloorplanAdd');
        const modalEl = $('floorplanUploadModal');
        if (!addBtn || !modalEl) return;

        // always open via Bootstrap when user clicks Add
        addBtn.addEventListener('click', (e) => {
            e.preventDefault();
            const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
            modal.show();
        });

        // state inside modal
        let picks = []; // [{file, name, url}]

        // reset modal UI each time it opens
        modalEl.addEventListener('show.bs.modal', () => {
            $('fpProjectEcho') && ($('fpProjectEcho').textContent = ($id('hd_ProjectID')?.value || '(not selected)'));
            resetPicker();
            const input = $('fpFilesInput');
            if (input) input.value = '';
        });

        // choose files
        $('fpFilesInput')?.addEventListener('change', (e) => {
            const files = Array.from(e.target.files || []);
            files.forEach(f => addPick(f));
            renderPreview();
            e.target.value = ''; // allow picking same file again
        });

        // upload
        $('fpBtnUpload')?.addEventListener('click', onUpload);

        // helpers
        function resetPicker() {
            picks.forEach(p => p.url && URL.revokeObjectURL(p.url));
            picks = [];
            renderPreview();
        }
        function addPick(file) {
            const url = URL.createObjectURL(file);
            picks.push({ file, name: file.name, url });
        }
        function removePick(idx) {
            const p = picks[idx];
            if (p?.url) URL.revokeObjectURL(p.url);
            picks.splice(idx, 1);
            renderPreview();
        }
        function renderPreview() {
            const wrap = $('fpPreviewList');
            if (!wrap) return;
            if (picks.length === 0) {
                wrap.innerHTML = `<div class="col-12 text-muted">No files selected.</div>`;
                return;
            }
            wrap.innerHTML = picks.map((p, i) => {
                const safeName = (p.name || '').replace(/"/g, '&quot;');
                return `
        <div class="col-12 col-md-6">
          <div class="card h-100">
            <div class="card-body d-flex gap-3">
              <img src="${p.url}" alt="" class="rounded border" style="width:80px;height:80px;object-fit:cover;">
              <div class="flex-grow-1">
                <div class="mb-2">
                  <label class="form-label small text-muted">File name to save</label>
                  <input class="form-control form-control-sm" value="${safeName}" data-idx="${i}" />
                </div>
                <div class="text-muted small">${bytes(p.file.size)}</div>
              </div>
              <button class="btn btn-sm btn-outline-danger" title="Remove" data-remove="${i}">
                <i class="bi bi-x-lg"></i>
              </button>
            </div>
          </div>
        </div>`;
            }).join('');

            wrap.querySelectorAll('button[data-remove]').forEach(btn => {
                btn.addEventListener('click', () => {
                    const idx = Number(btn.getAttribute('data-remove'));
                    removePick(idx);
                });
            });

            wrap.querySelectorAll('input[data-idx]').forEach(inp => {
                inp.addEventListener('input', () => {
                    const idx = Number(inp.getAttribute('data-idx'));
                    picks[idx].name = (inp.value || '').trim();
                });
            });
        }
        function bytes(n) {
            if (n < 1024) return `${n} B`;
            if (n < 1024 * 1024) return `${(n / 1024).toFixed(1)} KB`;
            return `${(n / 1024 / 1024).toFixed(1)} MB`;
        }

        async function onUpload() {
            const projectId = $id('hd_ProjectID')?.value || '';
            if (!projectId) {
                (window.showWarning || alert)('Please select a project first.');
                return;
            }
            if (picks.length === 0) {
                (window.showWarning || alert)('Please choose at least one image.');
                return;
            }

            const fd = new FormData();
            fd.append('ProjectID', projectId);
            picks.forEach(p => {
                const safe = (p.name || p.file.name || 'image.png').replace(/[\\/:*?"<>|]+/g, '_');
                fd.append('files', p.file, safe);
                fd.append('names', safe);
            });

            const btn = $('fpBtnUpload');
            const origHtml = btn.innerHTML;
            btn.disabled = true;
            btn.innerHTML = '<span class="spinner-border spinner-border-sm me-1"></span> Uploading...';
            (window.showLoading || (() => { }))();

            try {
                // adjust to your endpoint
                const resp = await fetch(baseUrl + 'Projectandunitfloorplan/UploadFloorplan', {
                    method: 'POST',
                    body: fd
                });
                const json = await resp.json().catch(() => ({}));

                if (!resp.ok || json?.success !== true) {
                    (window.errorMessage || alert)(json?.message || 'Upload failed.', 'Upload');
                    return;
                }

                (window.successMessage || alert)(json?.message || 'Uploaded!', 'Upload');
                bootstrap.Modal.getInstance(modalEl)?.hide();
                resetPicker();

                // refresh left list
                window.ProjectUnitFloorplan?.onSearchClick?.();
            } catch (e) {
                console.error(e);
                (window.errorMessage || alert)('Upload failed.', 'Upload');
            } finally {
                (window.hideLoading || (() => { }))();
                btn.disabled = false;
                btn.innerHTML = origHtml;
            }
        }
    }

})();
