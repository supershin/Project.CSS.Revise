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

    const setChoicesEmpty = (inst, text = '— No Data —') => {
        if (!inst) return;
        try {
            inst.clearStore();
            inst.setChoices([{ value: '', label: text, disabled: true }], 'value', 'label', true);
            inst.removeActiveItems();
        } catch { }
    };

    const setChoicesLoading = (inst, text = 'Loading...') => {
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

    //const baseUrl = (typeof window.baseUrl === 'string' && window.baseUrl) ? window.baseUrl : '/';

    const escapeHtml = (s) => String(s ?? '')
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');

    const slug = (s) => String(s ?? '').replace(/[^a-zA-Z0-9_-]/g, '_');

    function joinUrl(base, path) {
        const b = String(base || '').replace(/\/+$/, '');     // trim trailing /
        const p = String(path || '').replace(/^\/+/, '');     // trim leading /
        return b && p ? `${b}/${p}` : (b || p);
    }

    function toFullUrl(path) {
        const p = String(path || '');
        // if already absolute (http/https/protocol-relative/data/blob), return as-is
        if (/^(?:https?:|data:|blob:|\/\/)/i.test(p)) return p;
        return joinUrl(baseUrl, p); // baseUrl is guaranteed to have value
    }



    // ====== filter chain: Project -> UnitType ======
    async function onProjectChanged() {
        const projectId = choicesProject?.getValue(true);
        if (!projectId) {
            setChoicesEmpty(choicesUnitType, '— Select one or more Unit Types —');
            return;
        }

        setChoicesLoading(choicesUnitType, 'Loading Unit Type...');

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
        showLoading();
        try {
            if (acSearch) acSearch.abort();
            acSearch = new AbortController();

            //if (btn) { btn.disabled = true; btn.dataset.originalHtml = btn.innerHTML; btn.innerHTML = '<i class="bi bi-hourglass-split"></i> Searching...'; }

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
            hideLoading();
        } catch (err) {
            if (err?.name !== 'AbortError') {
                console.error('Search failed:', err);
                hideLoading();
                toastError('ค้นหาล้มเหลว กรุณาลองใหม่');
            }
        } finally {
            //if (btn) { btn.disabled = false; btn.innerHTML = btn.dataset.originalHtml || '<i class="icofont icofont-search"></i> Search'; }
            hideLoading();
        }
    }

    const setHiddenProject = (val) => {
        const h = $id('hd_ProjectID');
        if (h) h.value = val || '';
    };

    // keep last loaded list for filtering
    let _floorplanData = [];

    // simple case-insensitive contains
    function _contains(hay, needle) {
        return (hay || '').toLowerCase().includes((needle || '').toLowerCase());
    }

    // filter the DOM by query (no re-render)
    function filterFloorPlans(query) {
        const card = document.getElementById('card_project_floor_plan');
        if (!card) return;

        const container = card.querySelector('.vertical-scroll .list-group');
        const counter = document.getElementById('count_Floor_plan_lists');
        if (!container) return;

        const rows = container.querySelectorAll('.list-group-item');
        let visibleCount = 0;

        rows.forEach(row => {
            // skip the empty placeholder row, if any
            if (!row.dataset || !row.dataset.filename) return;

            const name = row.dataset.filename || '';
            const match = !query || _contains(name, query);
            row.classList.toggle('d-none', !match);
            if (match) visibleCount++;
        });

        // update counter to reflect visible items
        if (counter) counter.textContent = `Total ${visibleCount}`;

        // If you want "Check All" to act only on visible rows, you can reset it:
        const chkAll = document.getElementById('chkAllFloorplan');
        if (chkAll) chkAll.checked = false;
    }


    // ====== RENDER: card_project_floor_plan (viewer + edit + delete) ======
    function renderFloorPlans(list, totalCount) {
        const card = $id('card_project_floor_plan');
        if (!card) return;

        const counter = $id('count_Floor_plan_lists');
        const container = card.querySelector('.vertical-scroll .list-group');
        if (!container) return;

        // keep data for future filtering
        _floorplanData = Array.isArray(list) ? list.slice() : [];

        // helper to set counter text
        const setCounter = (n) => {
            if (counter) counter.textContent = `Total ${Number.isFinite(n) ? n : (Array.isArray(list) ? list.length : 0)}`;
        };

        // empty state
        if (!Array.isArray(list) || list.length === 0) {
            container.innerHTML = `<div class="list-group-item text-muted">— Not found floor plan —</div>`;
            setCounter(0);
            bindCheckAll(false);
            refreshMappingState?.();
            return;
        }

        // permissions (fallback false)
        const canEdit = !!(window.APP_PERM && window.APP_PERM.update);
        const canDelete = !!(window.APP_PERM && window.APP_PERM.delete);

        // keep current project id for data attribute
        const currentProjectId = $id('hd_ProjectID')?.value || '';

        const rows = list.map(item => {
            const id = escapeHtml(item.ID ?? '');
            const fileName = escapeHtml(item.FileName ?? '');
            const rawPath = item.FilePath ?? '';
            const mime = String(item.MimeType ?? '').toLowerCase();
            const fileUrl = encodeURI(toFullUrl(rawPath));

            const thumbHtml = isImageMime(mime)
                ? `<img src="${fileUrl}" width="50" height="50" class="img-thumbnail thumb-img" alt="${fileName}" />`
                : thumbIconHtml(fileName);

            // keep openViewer, but mark with class + data-url
            const thumbWrapper = isImageMime(mime)
                ? `<a href="javascript:void(0);" class="viewer-link" data-url="${fileUrl}" title="Open view">${thumbHtml}</a>`
                : thumbHtml;

            // action buttons by permission
            const actions = [];
            if (canEdit) {
                actions.push(`
        <button type="button" class="btn-e btn-edit" title="Edit">
          <i class="bi bi-pencil"></i>
        </button>`);
            }
            if (canDelete) {
                actions.push(`
        <button type="button" class="btn-c btn-delete" title="Delete">
          <i class="bi bi-trash"></i>
        </button>`);
            }
            const actionsHtml = actions.length ? `<div class="btn-group">${actions.join('&nbsp;')}</div>` : '';

            return `
      <div class="list-group-item d-flex justify-content-between align-items-center list-hover-primary"
           data-id="${id}"
           data-project="${escapeHtml(currentProjectId)}"
           data-filename="${fileName}"
           data-filepath="${escapeHtml(rawPath)}"
           data-mime="${escapeHtml(mime)}">
        <div class="d-flex align-items-center gap-2">
          <input class="form-check-input chk-floorplan" type="checkbox" />
          ${thumbWrapper}
          <span title="${fileName}">${fileName}</span>
        </div>
        ${actionsHtml}
      </div>`;
        }).join('');

        container.innerHTML = rows;
        setCounter(list.length);

        if (typeof wireThumbFallback === 'function') wireThumbFallback(container);
        bindCheckAll(true);

        // checklist change
        container.addEventListener('change', e => {
            if (e.target.classList.contains('chk-floorplan')) refreshMappingState?.();
        });
        $id('chkAllFloorplan')?.addEventListener('change', () => refreshMappingState?.());

        // one delegated click handler for viewer + edit + delete
        if (!container.dataset.boundClicks) {
            container.addEventListener('click', async (e) => {
                // viewer link
                const vlink = e.target.closest('a.viewer-link');
                if (vlink) {
                    e.preventDefault();
                    e.stopPropagation();
                    const url = vlink.getAttribute('data-url') || '';
                    if (url && typeof window.openViewer === 'function') {
                        window.openViewer(url);
                    }
                    return;
                }

                // find the row once
                const row = e.target.closest('.list-group-item');
                if (!row) return;

                // edit (will only match when canEdit == true; button not rendered otherwise)
                if (e.target.closest('.btn-e.btn-edit')) {
                    const payload = {
                        id: row.dataset.id || '',
                        projectId: row.dataset.project || '',
                        fileName: row.dataset.filename || '',
                        filePath: row.dataset.filepath || '',
                        mimeType: row.dataset.mime || ''
                    };
                    if (typeof window.openEditFloorplanModal === 'function') {
                        window.openEditFloorplanModal(payload);
                    }
                    return;
                }

                // delete (will only match when canDelete == true; button not rendered otherwise)
                if (e.target.closest('.btn-c.btn-delete')) {
                    const floorPlanId = row.dataset.id || '';
                    const fileName = row.dataset.filename || row.querySelector('span[title]')?.getAttribute('title') || '';
                    if (!floorPlanId) {
                        (window.errorMessage || alert)('Missing FloorPlanID.');
                        return;
                    }

                    // confirm
                    let confirmed = false;
                    if (typeof Swal !== 'undefined') {
                        const res = await Swal.fire({
                            icon: 'warning',
                            title: 'Delete this floor plan?',
                            html: `File name: <strong>${fileName || '-'}</strong>`,
                            showCancelButton: true,
                            confirmButtonText: 'Delete',
                            cancelButtonText: 'Cancel',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-danger',
                                cancelButton: 'btn btn-secondary ms-2'
                            }
                        });
                        confirmed = res.isConfirmed;
                    } else {
                        confirmed = confirm(`Delete this floor plan?\n${fileName || ''}`);
                    }
                    if (!confirmed) return;

                    try {
                        (window.showLoading || (() => { }))();

                        const fd = new FormData();
                        fd.append('FloorPlanID', floorPlanId);

                        const resp = await fetch(baseUrl + 'Projectandunitfloorplan/DeleteFloorplan', {
                            method: 'POST',
                            body: fd
                        });
                        const json = await resp.json().catch(() => ({}));

                        if (!resp.ok || json?.success !== true) {
                            (window.errorMessage || alert)(json?.message || 'Delete failed.', 'Delete');
                            return;
                        }

                        (window.successMessage || alert)(json?.message || 'Deleted.', 'Delete');

                        // Remove row immediately and update UI
                        row.remove();
                        const remaining = container.querySelectorAll('.list-group-item[data-filename]').length;

                        if (remaining === 0) {
                            container.innerHTML = `<div class="list-group-item text-muted">— Not found floor plan —</div>`;
                            bindCheckAll(false);
                            setCounter(0);
                        } else {
                            bindCheckAll(true);
                            setCounter(remaining);
                        }
                        refreshMappingState?.();
                    } catch (err) {
                        console.error(err);
                        (window.errorMessage || alert)('Delete failed. Please try again.', 'Delete');
                    } finally {
                        (window.hideLoading || (() => { }))();
                    }
                }
            });
            container.dataset.boundClicks = '1';
        }

        // apply current search filter if any
        const q = $id('fpSearch')?.value || '';
        if (q) filterFloorPlans(q);

        refreshMappingState?.();
    }



    // ====== Wire search input once ======
    document.addEventListener('DOMContentLoaded', () => {
        const inp = $id('fpSearch');
        if (inp && !inp.dataset.bound) {
            inp.addEventListener('input', (e) => {
                filterFloorPlans(e.target.value);
            });
            inp.dataset.bound = '1';
        }
    });



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
            const addrno = escapeHtml(u.AddrNo ?? '');
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
                    <span> Unit : <strong>${code}</strong> Address : <strong>${addrno}</strong>  </span>
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

    async function loadUnitFloorPlans(unitId, fileListEl, opts = {}) {
        const force = !!opts.force;

        // if already loaded and not forced, use cached DOM
        if (!force && fileListEl.dataset.loaded === '1') {
            return Number(fileListEl.dataset.count || 0);
        }

        // mark as loading
        fileListEl.dataset.loaded = '0';
        fileListEl.innerHTML = `
        <div class="text-muted small d-flex align-items-center gap-2">
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            Loading...
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
                fileListEl.innerHTML = `<div class="text-muted small">— Not found floor plan —</div>`;
                fileListEl.dataset.loaded = '1';
                fileListEl.dataset.count = '0';
                return 0;
            }

            const itemsHtml = list.map(fp => {
                const mapId = escapeHtml(fp.ID ?? '');
                const name = escapeHtml(fp.FileName ?? '');
                const raw = fp.FilePath ?? '';
                const mime = String(fp.MimeType ?? '').toLowerCase();
                const fileUrl = encodeURI(toFullUrl(raw));

                const thumbImg = isImageMime(mime)
                    ? `<img src="${fileUrl}" width="50" height="50" class="img-thumbnail thumb-img" alt="${name}" />`
                    : thumbIconHtml(name);

                const thumbWrapped = isImageMime(mime)
                    ? `<a class="viewer-link" href="javascript:void(0);" onclick='openViewer && openViewer("${fileUrl}")' title="${name}">${thumbImg}</a>`
                    : thumbImg;

                const canDelete = !!(window.APP_PERM && window.APP_PERM.delete);    
                const actions = [];
                if (canDelete) {
                    actions.push(`<button type="button" class="btn btn-sm text-danger file-remove" title="Remove file"
                                          onclick="onRemoveUnitFloorPlan('${mapId}', this)">
                                    <i class="fa fa-times-circle"></i>
                                </button>`);
                }
                return `
                <div class="file-item d-inline-flex flex-column align-items-center me-2 mb-2" data-mapid="${mapId}">
                    ${thumbWrapped}
                    ${actions}
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
            fileListEl.innerHTML = `<div class="text-danger small">Failed to load file</div>`;
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

        // disable the clicked button while working
        const origHtml = btn.innerHTML;
        btn.disabled = true;
        btn.innerHTML = '<i class="fa fa-spinner fa-spin"></i>';

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

            // ✅ Re-fetch fresh data for this unit section
            const listWrap = btn.closest('.file-list');
            const parentItem = listWrap?.closest('.list-group-item'); // unit row
            const unitId = parentItem?.getAttribute('data-id');

            if (listWrap && unitId) {
                // force reload from server (no cache)
                const newCount = await loadUnitFloorPlans(unitId, listWrap, { force: true });

                // update badge from fresh count
                const badge = parentItem.querySelector('.badge-imgcount .imgcount-val');
                if (badge) badge.textContent = String(newCount || 0);
            }

            successMessage?.(json?.message || 'Removed.', 'Success');
        } catch (err) {
            console.error(err);
            errorMessage?.('Remove failed. Please try again.', 'Remove Failed');
        } finally {
            hideLoading?.();
            btn.disabled = false;
            btn.innerHTML = origHtml;
        }
    }

    // expose for inline onclick
    window.onRemoveUnitFloorPlan = onRemoveUnitFloorPlan;

    function ensureUnitCounter(total) {
        const card = $id('card_unit');
        if (!card) return;

        let cnt = card.querySelector('#count_unit_total');
        if (cnt) {
            cnt.textContent = `Total ${Number(total) || 0}`;
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
            placeholderValue: '🔍 Search projects...',
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
            placeholderValue: '— Select one or more Unit Types —'
        });
        setChoicesEmpty(choicesUnitType, '— Select one or more Unit Types —');

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
            const sync = () => {
                const open = body.classList.contains('show');
                chev.classList.toggle('bi-chevron-up', open);
                chev.classList.toggle('bi-chevron-down', !open);
            };
            body.addEventListener('shown.bs.collapse', sync);
            body.addEventListener('hidden.bs.collapse', sync);
            sync(); // set initial state
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
        const btn = document.getElementById('btnSaveMapping');
        if (!btn) return;

        const enabled = fp > 0 && un > 0;
        btn.disabled = !enabled;

        // Only change color style (don’t touch class)
        if (enabled) {
            btn.style.backgroundColor = '#28a745'; // green (Bootstrap success tone)
            btn.style.borderColor = '#28a745';
            btn.style.opacity = '1';
            btn.style.cursor = 'pointer';
        } else {
            btn.style.backgroundColor = '#6c757d'; // grey (Bootstrap secondary)
            btn.style.borderColor = '#6c757d';
            btn.style.opacity = '0.65';
            btn.style.cursor = 'not-allowed';
        }
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

        if (!projectId) { showWarning('Please select a project before saving.'); return; }
        if (floorplanIds.length === 0) { showWarning('Please select at least one floor plan.'); return; }
        if (unitIds.length === 0) { showWarning('Please select at least one unit.'); return; }

        const fd = new FormData();
        fd.append('ProjectID', projectId);
        floorplanIds.forEach(id => fd.append('FloorPlanIDs', id));
        unitIds.forEach(id => fd.append('UnitIDs', id));

        const originalHtml = btn.innerHTML;
        try {
            btn.disabled = true;
            //btn.innerHTML = '<i class="bi bi-hourglass-split me-1"></i> Saving...';
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
                // ✅ Clear current selections
                document.querySelectorAll('#card_project_floor_plan .chk-floorplan:checked')
                    .forEach(chk => chk.checked = false);
                document.querySelectorAll('#card_unit .chk-unit:checked')
                    .forEach(chk => chk.checked = false);
                document.getElementById('chkAllFloorplan') && (document.getElementById('chkAllFloorplan').checked = false);
                document.getElementById('chkAllUnit') && (document.getElementById('chkAllUnit').checked = false);
                refreshMappingState();

                // ✅ Re-run search to fetch fresh data
                // (keeps filters; same as clicking the Search button)
                await window.ProjectUnitFloorplan?.onSearchClick?.();
            } else {
                errorMessage?.(json?.message || 'บันทึกไม่สำเร็จ กรุณาลองใหม่', 'Save Failed');
            }
        } catch (err) {
            console.error(err);
            errorMessage?.('เกิดข้อผิดพลาดระหว่างบันทึก กรุณาลองใหม่อีกครั้ง', 'Save Failed');
        } finally {
            hideLoading?.();
            //btn.innerHTML = originalHtml;
            btn.disabled = false;
            refreshMappingState();
            successMessage?.('Mapping saved successfully');
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
                fd.append('Images', p.file, safe);    // matches DTO
                fd.append('FileName', safe);          // matches DTO
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

    // ====== Edit Floorplan (open modal + preview + save) ======
    (function () {
        // small helpers (local)
        const $$ = (id) => document.getElementById(id);
        const _isImg = (mime) => /^image\//.test(String(mime || '').toLowerCase());
        const _isPdf = (mimeOrName) => {
            const s = String(mimeOrName || '').toLowerCase();
            return s.includes('application/pdf') || s.endsWith('.pdf');
        };

        let _fpEditObjectUrl = null; // for preview revocation

        // Open the modal with current row data
        window.openEditFloorplanModal = function ({ id, projectId, fileName, filePath, mimeType }) {
            const modalEl = $$('floorplanEditModal');
            if (!modalEl) return;

            // fill fields
            $$('fpEdit_FloorPlanID') && ($$('fpEdit_FloorPlanID').value = id || '');
            $$('fpEdit_ProjectID') && ($$('fpEdit_ProjectID').value = projectId || '');
            $$('fpEdit_FileName') && ($$('fpEdit_FileName').value = fileName || '');

            // preview + view link
            const preview = $$('fpEdit_Preview');
            const viewLink = $$('fpEdit_ViewLink');
            const url = toFullUrl(filePath);

            if (preview) {
                preview.src = _isImg(mimeType) ? url : (_isPdf(mimeType || fileName) ? '' : url);
                preview.alt = fileName || '';
                preview.style.opacity = _isImg(mimeType) ? '1' : '0.3';
            }
            if (viewLink) {
                viewLink.href = url || '#';
                viewLink.classList.toggle('disabled', !url);
            }

            // reset file input
            const fileInp = $$('fpEdit_File');
            if (fileInp) fileInp.value = '';

            // show project name echo (optional)
            const echo = $$('fpEdit_projectEcho');
            if (echo) echo.textContent = projectId ? `Project: ${projectId}` : '';

            // show modal
            const bs = bootstrap.Modal.getOrCreateInstance(modalEl);
            bs.show();
        };

        // live preview when user picks a new file
        function _wireFilePreviewOnce() {
            const fileInp = $$('fpEdit_File');
            if (!fileInp || fileInp.dataset.bound === '1') return;

            fileInp.addEventListener('change', () => {
                const preview = $$('fpEdit_Preview');
                const file = fileInp.files && fileInp.files[0];
                if (!preview) return;

                // revoke old
                if (_fpEditObjectUrl) {
                    URL.revokeObjectURL(_fpEditObjectUrl);
                    _fpEditObjectUrl = null;
                }

                if (!file) {
                    // no new file -> keep whatever existing UI had
                    return;
                }

                if (_isPdf(file.type) || _isPdf(file.name)) {
                    // pdf: no image preview; just dim the preview box
                    preview.src = '';
                    preview.style.opacity = '0.3';
                    preview.alt = file.name;
                } else if (_isImg(file.type)) {
                    _fpEditObjectUrl = URL.createObjectURL(file);
                    preview.src = _fpEditObjectUrl;
                    preview.style.opacity = '1';
                    preview.alt = file.name;
                } else {
                    // other types: clear src
                    preview.src = '';
                    preview.style.opacity = '0.3';
                    preview.alt = file.name;
                }
            });

            // cleanup when modal hides
            const modalEl = $$('floorplanEditModal');
            modalEl?.addEventListener('hidden.bs.modal', () => {
                if (_fpEditObjectUrl) {
                    URL.revokeObjectURL(_fpEditObjectUrl);
                    _fpEditObjectUrl = null;
                }
            });

            fileInp.dataset.bound = '1';
        }

        // save (POST -> UpdateFloorplan)
        async function _wireSaveOnce() {
            const btn = $$('fpEdit_Save');
            if (!btn || btn.dataset.bound === '1') return;

            btn.addEventListener('click', async () => {
                const floorPlanId = $$('fpEdit_FloorPlanID')?.value || '';
                const projectId = $$('fpEdit_ProjectID')?.value || '';
                const newName = ($$('fpEdit_FileName')?.value || '').trim();
                const fileInp = $$('fpEdit_File');
                const file = fileInp?.files?.[0];

                if (!floorPlanId) {
                    (window.errorMessage || alert)('Missing FloorPlanID.');
                    return;
                }
                if (!projectId) {
                    (window.errorMessage || alert)('Please select a project first.');
                    return;
                }
                // Note: allow both rename-only (no file) and file-replace (with/without rename)

                const fd = new FormData();
                fd.append('FloorPlanID', floorPlanId);
                fd.append('ProjectID', projectId);
                if (newName) fd.append('NewFileName', newName);
                if (file) fd.append('NewImage', file, file.name);

                const originalHtml = btn.innerHTML;
                try {
                    btn.disabled = true;
                    btn.innerHTML = '<i class="bi bi-hourglass-split me-1"></i> Saving...';
                    (window.showLoading || (() => { }))();

                    const resp = await fetch(baseUrl + 'Projectandunitfloorplan/UpdateFloorplan', {
                        method: 'POST',
                        body: fd
                    });
                    const json = await resp.json().catch(() => ({}));

                    if (!resp.ok || json?.success !== true) {
                        (window.errorMessage || alert)(json?.message || 'Save failed.', 'Update');
                        return;
                    }

                    (window.successMessage || alert)(json?.message || 'Saved.', 'Update');

                    // close modal
                    const modalEl = $$('floorplanEditModal');
                    bootstrap.Modal.getInstance(modalEl)?.hide();

                    // refresh floorplan list to reflect new name/thumbnail
                    window.ProjectUnitFloorplan?.onSearchClick?.();
                } catch (err) {
                    console.error(err);
                    (window.errorMessage || alert)('Save failed. Please try again.', 'Update');
                } finally {
                    (window.hideLoading || (() => { }))();
                    btn.disabled = false;
                    btn.innerHTML = originalHtml;
                }
            });

            btn.dataset.bound = '1';
        }

        // bind once after DOM ready
        document.addEventListener('DOMContentLoaded', () => {
            _wireFilePreviewOnce();
            _wireSaveOnce();
        });
    })();

    // ====== Delete Floorplan (soft delete with confirm) ======
    (function () {
        const $id = (x) => document.getElementById(x);

        function bindFloorplanDeleteOnce() {
            const container = $id('card_project_floor_plan')?.querySelector('.vertical-scroll .list-group');
            if (!container || container.dataset.boundDelete === '1') return;

            container.addEventListener('click', async (e) => {
                const delBtn = e.target.closest('.btn-c.btn-delete');
                if (!delBtn) return;

                const row = delBtn.closest('.list-group-item');
                const floorPlanId = row?.dataset.id || '';
                const fileName = row?.dataset.filename || row?.querySelector('span[title]')?.getAttribute('title') || '';

                if (!floorPlanId) {
                    (window.errorMessage || alert)('Missing FloorPlanID.');
                    return;
                }

                // Confirm
                let confirmed = false;
                if (typeof Swal !== 'undefined') {
                    const res = await Swal.fire({
                        icon: 'warning',
                        title: 'Delete this floor plan?',
                        html: `File name: <strong>${fileName || '-'}</strong><br><small class="text-muted">If this file is mapped to a Unit, the system will not allow deletion.</small>
`,
                        showCancelButton: true,
                        confirmButtonText: 'Delete',
                        cancelButtonText: 'Cancel',
                        buttonsStyling: false,
                        customClass: { confirmButton: 'btn btn-danger', cancelButton: 'btn btn-secondary ms-2' }
                    });
                    confirmed = res.isConfirmed;
                } else {
                    confirmed = confirm(`Delete this floor plan?\n${fileName || ''}`);
                }
                if (!confirmed) return;

                // Call API
                try {
                    (window.showLoading || (() => { }))();

                    const fd = new FormData();
                    fd.append('FloorPlanID', floorPlanId);

                    const resp = await fetch(baseUrl + 'Projectandunitfloorplan/DeleteFloorplan', {
                        method: 'POST',
                        body: fd
                    });
                    const json = await resp.json().catch(() => ({}));

                    if (!resp.ok || json?.success !== true) {
                        (window.errorMessage || alert)(json?.message || 'Delete failed.', 'Delete');
                        return;
                    }

                    (window.successMessage || alert)(json?.message || 'Deleted.', 'Delete');

                    // วิธีที่ 1: รีเฟรชทั้งลิสต์ให้ชัวร์ (แนะนำ)
                    window.ProjectUnitFloorplan?.onSearchClick?.();

                    // วิธีที่ 2: ลบแถวออกทันที (ถ้าอยากเร็ว ไม่ต้องรีเฟรชทั้งหมด)
                    // row?.remove();
                    // refreshMappingState();

                } catch (err) {
                    console.error(err);
                    (window.errorMessage || alert)('Delete failed. Please try again.', 'Delete');
                } finally {
                    (window.hideLoading || (() => { }))();
                }
            });

            container.dataset.boundDelete = '1';
        }

        // bind once
        document.addEventListener('DOMContentLoaded', bindFloorplanDeleteOnce);
    })();

})();
