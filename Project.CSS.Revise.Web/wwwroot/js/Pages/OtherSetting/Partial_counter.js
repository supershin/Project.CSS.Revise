// -------- Helpers --------
function getMultiSelectValues(selector) {
    const el = document.querySelector(selector);
    if (!el) return [];
    // ทำงานได้ทั้ง native <select multiple> และที่ใช้ Choices.js
    return Array.from(el.options).filter(o => o.selected).map(o => o.value).filter(Boolean);
}
function toCommaList(arr, { trailing = false } = {}) {
    if (!arr || !arr.length) return '';
    return arr.join(',') + (trailing ? ',' : '');
}
function positionCountInclusive(start, end) {
    const s = Number(start) || 0, e = Number(end) || 0;
    return Math.max(0, e - s + 1);
}
function emptyCard(msg) {
    return `
    <div class="col-12">
      <div class="proj-card d-flex flex-column">
        <div class="text-muted">${msg}</div>
      </div>
    </div>`;
}
function escapeHtml(s) {
    return (s ?? '').toString()
        .replace(/&/g, '&amp;').replace(/</g, '&lt;')
        .replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#39;');
}

// -------- Card builders --------
function buildBankCard(row) {
    const title = escapeHtml(row.ProjectName || row.ProjectID || '');
    const subtype = escapeHtml(row.QueueTypeName || 'Bank Pre-Approve (สินเชื่อ)');
    const units = positionCountInclusive(row.StartCounter, row.EndCounter);
    const staffCount = Number(row.COUNT_Staff) || 0;
    const bankCount = Number(row.COUNT_Bank) || 0;

    return `
    <div class="col-4">
      <div class="proj-card d-flex flex-column"
           role="button" tabindex="0" style="cursor:pointer"
           data-id="${row.ID}"
           data-project="${escapeHtml(row.ProjectID)}"
           data-queue="48"
           data-end="${row.EndCounter}">
        <div class="d-flex justify-content-between align-items-start">
          <div><h6>${title}</h6></div>
          <span class="count-badge is-top-right is-green">
              ${units} Counter
              <i class="fa fa-edit" style="margin-left:6px; cursor:pointer;"></i>
         </span>
        </div>

        <div class="mt-auto pt-3 d-flex justify-content-between align-items-center flex-wrap btn-row">

          <!-- DOWNLOAD ZIP button -->
          <button class="action-btn"
                  title="Download all QR codes"
                  onclick="event.stopPropagation(); handleDownloadQrZipClick(this)"
                  data-pid="${escapeHtml(row.ProjectID)}"
                  data-pname="${escapeHtml(row.ProjectName || row.ProjectID || '')}"
                  data-qtype="bank"
                  data-qty="${units}">
            <i class="bi bi-qr-code me-1"></i>Download QR
          </button>

          <div class="d-flex btn-row">
            <button class="icon-btn" title="Staff count" onclick="event.stopPropagation()">
              <i class="bi bi-person me-1"></i>Staff ${staffCount}
            </button>
            <button class="icon-btn" title="Bank count" onclick="event.stopPropagation()">
              <i class="bi bi-bank me-1"></i>Bank ${bankCount}
            </button>
          </div>
        </div>
      </div>
    </div>`;
}

// --- helpers: filename + force download ---
function getFilenameFromHeaders(res, fallbackName) {
    const disp = res.headers.get('Content-Disposition');
    if (!disp) return fallbackName;
    let m = /filename\*?=(?:UTF-8'')?["']?([^"';]+)["']?/i.exec(disp);
    if (!m) m = /filename=["']?([^"';]+)["']?/i.exec(disp);
    return m && m[1] ? decodeURIComponent(m[1]) : fallbackName;
}

function triggerDownload(blob, filename) {
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    a.remove();
    URL.revokeObjectURL(url);
}

// --- click handler wired from the button above ---
async function handleDownloadQrZipClick(btn) {
    const originalHTML = btn.innerHTML;

    // read data-* from the button
    const projectId = btn.dataset.pid || '';
    const projectName = btn.dataset.pname || projectId || '';
    const queueType = btn.dataset.qtype || 'bank';   // "bank" | "inspect"
    const qty = Number(btn.dataset.qty || '0');

    if (!projectId || qty < 1) {
        errorMessage?.('Missing project or counter quantity.');
        return;
    }

    const url = `${baseUrl}QrDemo/zip?projectId=${encodeURIComponent(projectId)}&projectName=${encodeURIComponent(projectName)}&queueType=${encodeURIComponent(queueType)}&qty=${qty}`;

    try {
        showLoading?.();
        btn.disabled = true;
        btn.innerHTML = `<span class="spinner-border spinner-border-sm me-1"></span> Creating ZIP...`;

        const res = await fetch(url, { method: 'GET' });
        if (!res.ok) throw new Error('HTTP ' + res.status);

        const blob = await res.blob();
        const filename = getFilenameFromHeaders(res, `QRCodes_${projectId}_${queueType}.zip`);
        triggerDownload(blob, filename);

        successMessage?.('ดาวน์โหลดสำเร็จ: ' + filename);
    } catch (err) {
        console.error('Download QR ZIP error:', err);
        errorMessage?.('ดาวน์โหลดล้มเหลว');
    } finally {
        hideLoading?.();
        btn.disabled = false;
        btn.innerHTML = originalHTML;
    }
}


function buildInspectCard(row) {
    const title = escapeHtml(row.ProjectName || row.ProjectID || '');
    const units = positionCountInclusive(row.StartCounter, row.EndCounter);

    return `
              <div class="col-4">
                <div class="proj-card d-flex flex-column"
                     role="button" tabindex="0" style="cursor:pointer"
                     data-id="${row.ID}"
                     data-project="${escapeHtml(row.ProjectID)}"
                     data-queue="49"
                     data-end="${row.EndCounter}">

                  <!-- top-right pill using your .count-badge -->
                    <span class="count-badge is-top-right is-amber">
                      ${units} Counter <i class="fa fa-edit"></i>
                    </span>
                  <div class="d-flex justify-content-between align-items-start">
                    <div><h6>${title}</h3></div>
                  </div>

                  <div class="mt-auto pt-3 d-flex justify-content-between align-items-center flex-wrap btn-row">

                  <!-- DOWNLOAD ZIP button -->
                  <button class="action-btn"
                          title="Download all QR codes"
                          onclick="event.stopPropagation(); handleDownloadQrZipClick(this)"
                          data-pid="${escapeHtml(row.ProjectID)}"
                          data-pname="${escapeHtml(row.ProjectName || row.ProjectID || '')}"
                          data-qtype="inspect"
                          data-qty="${units}">
                    <i class="bi bi-qr-code me-1"></i>Download QR
                  </button>

                  </div>
                </div>
              </div>`;
}

// -------- Render Counters--------

//document.addEventListener('shown.bs.tab', function (ev) {
//    // ev.target = the newly activated tab <a>
//    if (ev.target && ev.target.id === 'counter-tab') {
//        fetchProjectCounters?.(); // call your fetch
//    }
//});


const QUEUE = { ALL: -1, BANK: 48, INSPECT: 49 };

function getSelectedQueueType() {
    let v = document.getElementById('ddl_counter_type')?.value ?? "-1";
    if (v === "" || v === null) v = "-1";
    const n = Number(v);
    return isNaN(n) ? QUEUE.ALL : n;
}

// ซ่อน/แสดงกล่องตาม queue type
function updateCounterSectionsVisibility() {
    const q = getSelectedQueueType();
    const bankBox = document.getElementById('show_hide_bank');
    const inspectBox = document.getElementById('show_hide_inspect');

    const showBank = (q === QUEUE.ALL || q === QUEUE.BANK);
    const showInspect = (q === QUEUE.ALL || q === QUEUE.INSPECT);

    bankBox?.classList.toggle('d-none', !showBank);
    inspectBox?.classList.toggle('d-none', !showInspect);
}

function renderCounters(data) {
    const bankWrap = document.getElementById('bank_cards');
    const inspectWrap = document.getElementById('inspect_cards');
    if (bankWrap) bankWrap.innerHTML = '';
    if (inspectWrap) inspectWrap.innerHTML = '';

    if (!Array.isArray(data) || data.length === 0) {
        if (bankWrap) bankWrap.innerHTML = emptyCard('ไม่พบข้อมูล');
        if (inspectWrap) inspectWrap.innerHTML = emptyCard('ไม่พบข้อมูล');
        return;
    }

    const bank = data.filter(x => Number(x.QueueTypeID) === 48);
    const inspect = data.filter(x => Number(x.QueueTypeID) === 49);

    if (bankWrap) bankWrap.innerHTML = bank.length ? bank.map(buildBankCard).join('') : emptyCard('ไม่พบข้อมูล Bank Pre-Approve');
    if (inspectWrap) inspectWrap.innerHTML = inspect.length ? inspect.map(buildInspectCard).join('') : '';

    // 👉 enable click-to-edit
    /*bindCardClickHandlers();*/
}

// attach ONCE
document.addEventListener('DOMContentLoaded', () => {
    const handleCardActivate = (e) => {
        // only inside our two containers
        const scope = e.target.closest('#bank_cards, #inspect_cards');
        if (!scope) return;

        // keyboard support
        if (e.type === 'keydown' && !['Enter', ' '].includes(e.key)) return;
        if (e.type === 'keydown') e.preventDefault();

        // ignore clicks on buttons inside the card
        if (e.target.closest('button')) return;

        const card = e.target.closest('.proj-card[role="button"]');
        if (!card) return;

        const id = Number(card.dataset.id) || 0;
        const queue = Number(card.dataset.queue) || 0;
        const proj = card.dataset.project || '';

        window.__selectedProjectIds = proj ? [proj] : [];
        openEditById(id, queue);
    };

    document.addEventListener('click', handleCardActivate);
    document.addEventListener('keydown', handleCardActivate);
});

// -------- Fetch list Counters project--------
async function fetchProjectCounters() {
    const bu = getMultiSelectValues('#ddl_BUG_counter');            // ex. ['1','3']
    const prj = getMultiSelectValues('#ddl_PROJECT_counter');       // ex. ['102C028','102C029']
    const prjst = getMultiSelectValues('#ddl_project_status');
    let rawValue = document.getElementById('ddl_counter_type')?.value ?? "-1";

    // ถ้า clear แล้ว rawValue จะเป็น "" => แก้ให้เป็น "-1"
    if (rawValue === "" || rawValue === null) {
        rawValue = "-1";
    }
    const queueType = Number(rawValue);

    const params = new URLSearchParams({
        Bu: toCommaList(bu, { trailing: false }),                      // "1,3"
        ProjectID: toCommaList(prj, { trailing: true }),               // "102C028,102C029," สำหรับ logic LIKE ฝั่ง SQL
        QueueType: isNaN(queueType) ? '-1' : String(queueType),
        ProjectStatus: isNaN(prjst) ? '-1' : String(prjst)
    });

    const btn = document.getElementById('btnSearchCounter');
    const originalHTML = btn ? btn.innerHTML : '';
    try {
        if (btn) { btn.disabled = true; btn.innerHTML = `<span class="spinner-border spinner-border-sm me-1"></span> Loading...`; }

        const res = await fetch(baseUrl + 'OtherSettings/GetListProjectCounter?' + params.toString(), {
            method: 'GET'
        });

        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        const json = await res.json(); // คาดว่าเป็น List<ProjectCounterMappingModel.ListData>
        renderCounters(json);
        updateCounterSectionsVisibility(); // <- ตรงนี้สำคัญ
    } catch (err) {
        console.error('GetListProjectCounter error:', err);
        renderCounters([]);
    } finally {
        if (btn) { btn.disabled = false; btn.innerHTML = originalHTML; }
    }
}

// -------- Wire button --------
document.addEventListener('DOMContentLoaded', function () {
    const searchBtn = document.getElementById('btnSearchCounter');
    if (searchBtn) {
        searchBtn.addEventListener('click', function (e) {
            e.preventDefault();
            fetchProjectCounters();
        });
    }
});

let counterModalInstance = null;
let projectMapChoices = null;

async function loadProjectCounterMapping(forceReload = false) {
    const loadingEl = document.getElementById('project_mapping_loading');
    const selectEl = document.getElementById('ddl_project_counter_mapping');

    // show loading
    loadingEl?.classList.remove('d-none');

    try {
        const res = await fetch(baseUrl + 'OtherSettings/GetListProjectCounterMapping', { method: 'GET' });
        if (!res.ok) throw new Error('HTTP ' + res.status);
        const data = await res.json(); // expect [{ ValueString, Text }, ...]

        // map to Choices format
        const choicesData = (data || []).map(x => ({
            value: x.ValueString,
            label: x.Text
        }));

        // init or refresh Choices
        if (!projectMapChoices) {
            projectMapChoices = new Choices(selectEl, {
                removeItemButton: true,
                searchEnabled: true,
                placeholder: true,
                placeholderValue: 'เลือกโปรเจกต์…',
                shouldSort: false,
                allowHTML: false
            });
        }

        // clear & set fresh items every open
        projectMapChoices.clearStore();
        projectMapChoices.setChoices(choicesData, 'value', 'label', true);
        projectMapChoices.removeActiveItems(); // no preselected items
    } catch (err) {
        console.error('loadProjectCounterMapping error:', err);
        // fallback UI
        selectEl.innerHTML = '';
        if (projectMapChoices) projectMapChoices.clearStore();
    } finally {
        loadingEl?.classList.add('d-none');
    }
}

async function loadAndRenderBanks(containerId = 'bankGrid') {
    const grid = document.getElementById(containerId);
    if (!grid) return;

    // keep user selection map
    window.__bankSelectionState = window.__bankSelectionState || new Map();

    // ✅ your real folder under wwwroot
    const BANK_LOGO_BASE = baseUrl + 'image/ThaiBankicon/';

    // simple src builder
    const bankLogoSrc = (code) => `${BANK_LOGO_BASE}${encodeURIComponent(code)}.png`;

    // dedupe but keep input order
    const dedupeBy = (arr, key) => {
        const seen = new Set();
        const out = [];
        for (const o of arr || []) {
            const k = o?.[key];
            if (!k || seen.has(k)) continue;
            seen.add(k);
            out.push(o);
        }
        return out;
    };

    const escapeHtml = (s) => (s ?? '').toString()
        .replace(/&/g, '&amp;').replace(/</g, '&lt;')
        .replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#39;');

    function wireCardBehaviour(card) {
        const chk = card.querySelector('.bank-check');
        const qty = card.querySelector('.bank-qty');
        const code = card.getAttribute('data-bank');

        const sync = () => {
            qty.disabled = !chk.checked;
            window.__bankSelectionState.set(code, { checked: chk.checked, qty: Number(qty.value) || 0 });
        };

        chk.addEventListener('change', () => { sync(); recalcBankQuota?.(); });
        qty.addEventListener('input', () => { sync(); recalcBankQuota?.(qty); });

        sync();
    }

    const renderCard = ({ ValueInt: id, ValueString: code, Text: name }) => {
        const prev = window.__bankSelectionState.get(code) || { checked: false, qty: 0 };
        const col = document.createElement('div');
        col.className = 'col-12 col-md-6 col-xl-4';
        col.innerHTML = `
      <div class="card h-100 border-0 shadow-sm" data-bank="${escapeHtml(code)}" data-bank-id="${id}">
        <div class="card-body">
          <div class="d-flex align-items-center justify-content-between gap-3 flex-wrap">
            <div class="d-flex align-items-center gap-3 flex-grow-1">
              <img src="${bankLogoSrc(code)}"
                   alt="${escapeHtml(code)}"
                   onerror="this.onerror=null; this.src='${BANK_LOGO_BASE}_default.png';"
                   class="rounded-circle border"
                   style="width:44px;height:44px;">
              <div>
                <div class="fw-semibold text-wrap">${escapeHtml(code)} - ${escapeHtml(name)}</div>
              </div>
            </div>
            <div class="d-flex align-items-center gap-2">
              <div class="form-check form-switch m-0">
                <input class="form-check-input bank-check" type="checkbox" ${prev.checked ? 'checked' : ''}>
              </div>
              <input type="number" class="form-control form-control-sm bank-qty"
                     placeholder="จำนวน…" min="0" value="${prev.qty || 0}" style="width:110px;">
            </div>
          </div>
        </div>
      </div>`;
        wireCardBehaviour(col.querySelector('.card'));
        return col;
    };

    // loading state
    grid.innerHTML = `
                        <div class="col-12">
                          <div class="d-flex align-items-center text-muted small px-2">
                            <i class="fa fa-spinner fa-spin me-2"></i>Loading banks…
                          </div>
                        </div>`
                    ;

    try {
        const res = await fetch(baseUrl + 'OtherSettings/GetListBank', { method: 'GET' });
        if (!res.ok) throw new Error('HTTP ' + res.status);

        let data = await res.json(); // [{ ValueString, Text, ValueInt }]
        data = dedupeBy(data, 'ValueString');

        // render simple foreach
        grid.innerHTML = '';
        data.forEach(item => grid.appendChild(renderCard(item)));

        // select all / clear (if buttons exist)
        document.getElementById('btnSelectAllBanks')?.addEventListener('click', () => {
            grid.querySelectorAll('.bank-check').forEach(chk => {
                if (!chk.checked) { chk.checked = true; chk.dispatchEvent(new Event('change')); }
            });
        });
        document.getElementById('btnClearBanks')?.addEventListener('click', () => {
            grid.querySelectorAll('.bank-check').forEach(chk => {
                if (chk.checked) { chk.checked = false; chk.dispatchEvent(new Event('change')); }
            });
        });

    } catch (err) {
        console.error('loadAndRenderBanks error:', err);
        grid.innerHTML = `
      <div class="col-12">
        <div class="alert alert-warning mb-0">ไม่สามารถโหลดรายชื่อธนาคารได้</div>
      </div>`;
    }
}


// ---- QUOTA ENFORCER ----
function ensureQuotaHint() {
    const qty = document.getElementById('counterQty');
    if (!qty) return null;
    // add a small hint element under the input if missing
    let hint = document.getElementById('quotaRemainingHint');
    if (!hint) {
        hint = document.createElement('div');
        hint.id = 'quotaRemainingHint';
        hint.className = 'small mt-1 text-muted';
        qty.insertAdjacentElement('afterend', hint);
    }
    return hint;
}

function sumCheckedBankQty() {
    const cards = document.querySelectorAll('#bankGrid .card, #bankGridEdit .card');
    return Array.from(cards).reduce((acc, card) => {
        const chk = card.querySelector('.bank-check');
        const qty = card.querySelector('.bank-qty');
        if (chk && chk.checked && qty) acc += Number(qty.value) || 0;
        return acc;
    }, 0);
}

function recalcBankQuota(lastQtyInput = null) {
    // detect which UI is active (add vs edit)

    const inEdit = !!(
        (lastQtyInput && lastQtyInput.closest('#bankGridEdit')) ||
        (window.__activeBankGridId === 'bankGridEdit') ||
        document.getElementById('modalQueue48')?.classList.contains('show')
    );

    // use the right cap input
    const cap = inEdit
        ? (Number(document.getElementById('counterQty48')?.value) || 0)   // EDIT modal
        : (Number(document.getElementById('counterQty')?.value) || 0);  // ADD modal

    // choose the right save button to disable/enable
    const saveBtn = inEdit ? document.getElementById('btnSaveQueue48') : document.getElementById('btnSaveCounter');

    // only show the remaining-hint for ADD modal (keep old behavior)
    const hint = inEdit ? null : ensureQuotaHint();



    // total from both grids
    const total = sumCheckedBankQty();                 // you already updated this to both grids

}

function resetAddCounterModalUI() {
    // Uncheck counter type checkboxes
    const chkBank = document.getElementById('chk-md-add-counter-bank');
    const chkInspect = document.getElementById('chk-md-add-counter-inspect');
    if (chkBank) chkBank.checked = false;
    if (chkInspect) chkInspect.checked = false;

    // Default qty = 20
    const qty = document.getElementById('counterQty');
    if (qty) qty.value = 20;

    // Clear project mapping selection (Choices.js aware)
    const sel = document.getElementById('ddl_project_counter_mapping');
    if (window.projectMapChoices) {
        projectMapChoices.removeActiveItems(); // clears selected items
    } else if (sel) {
        Array.from(sel.options).forEach(o => (o.selected = false));
    }

    // Clear bank selection state and grid contents (will reload fresh)
    window.__bankSelectionState = new Map();
    const grid = document.getElementById('bankGrid');
    if (grid) grid.innerHTML = '';

    // Hide Staff tab + switch to Details
    document.getElementById('staffs-tab-li')?.classList.add('d-none');
    document.getElementById('details-tab')?.click();

    // Refresh quota hint/UI
    recalcBankQuota();

    // Optional: focus qty field for quick typing
    setTimeout(() => document.getElementById('counterQty')?.focus(), 80);
}

async function openCounterModalMock() {

    // 🔄 reset everything to clean defaults
    resetAddCounterModalUI();

    // load list EVERY time before showing (so it's always fresh)
    await loadProjectCounterMapping(true);

    await loadAndRenderBanks(); 
    // show modal
    const m = new bootstrap.Modal(document.getElementById('counterModal'));
    m.show();
}


document.addEventListener('DOMContentLoaded', () => {
    const chkBank = document.getElementById('chk-md-add-counter-bank');
    const staffTabLi = document.getElementById('staffs-tab-li');

    if (!chkBank || !staffTabLi) return;

    const toggleStaffTab = () => {
        if (chkBank.checked) {
            staffTabLi.classList.remove('d-none');
        } else {
            // Hide tab + reset active if currently selected
            staffTabLi.classList.add('d-none');

            const staffTabBtn = document.getElementById('staffs-tab');
            const staffPane = document.getElementById('staffs-pane');
            if (staffTabBtn?.classList.contains('active')) {
                // Force switch back to details tab if Staff was active
                document.getElementById('details-tab')?.click();
            }
        }
    };

    // Run once on load
    toggleStaffTab();

    // Update on checkbox change
    chkBank.addEventListener('change', toggleStaffTab);
});
document.getElementById('counterQty')?.addEventListener('input', () => recalcBankQuota());
document.getElementById('counterQty48')?.addEventListener('input', () => recalcBankQuota());

/*recalcBankQuota(); */

// ---- Collect payload for CreateCounterRequest ----
function collectCreateCounterPayload() {
    const types = Array.from(document.querySelectorAll('#details-pane .form-check-input[type="checkbox"]:checked'))
        .map(chk => Number(chk.value))
        .filter(v => !Number.isNaN(v));

    const counterQty = Number(document.getElementById('counterQty')?.value) || 0;

    const projectIds = Array.from(document.querySelector('#ddl_project_counter_mapping')?.selectedOptions || [])
        .map(o => o.value)
        .filter(Boolean);

    const banks = Array.from(document.querySelectorAll('#bankGrid .card')).map(card => ({
        bankId: Number(card.getAttribute('data-bank-id')) || 0,     // NEW
        code: card.getAttribute('data-bank') || '',
        checked: !!card.querySelector('.bank-check')?.checked,
        qty: Number(card.querySelector('.bank-qty')?.value) || 0
    }));

    return {
        counterTypeIds: types,
        counterQty: counterQty,
        projectIds: projectIds,
        banks: banks
    };
}

function applyBankStateToGrid(containerId = 'bankGrid') {
    const grid = document.getElementById(containerId);
    if (!grid) return;

    // mark active grid so recalc uses the right cap
    window.__activeBankGridId = containerId;

    grid.querySelectorAll('.card[data-bank]').forEach(card => {
        const code = card.getAttribute('data-bank');
        const s = window.__bankSelectionState?.get(code);
        const chk = card.querySelector('.bank-check');
        const qty = card.querySelector('.bank-qty');
        if (s) {
            chk.checked = !!s.checked;
            qty.value = s.qty ?? 0;
            qty.disabled = !chk.checked;
        } else {
            chk.checked = false;
            qty.value = 0;
            qty.disabled = true;
        }
        // ensure state + total refresh
        chk.dispatchEvent(new Event('change'));
    });

    recalcBankQuota();
}

function bindCardClickHandlers() {
    const onClick = (e) => {
        const card = e.target.closest('.proj-card[role="button"]');
        if (!card || e.target.closest('button')) return;
        const id = Number(card.dataset.id);
        const queue = Number(card.dataset.queue);
        // keep current project id(s) for save payloads if you need them later
        const proj = card.dataset.project;
        window.__selectedProjectIds = proj ? [proj] : [];
        openEditById(id, queue);
    };
    const onKey = (e) => {
        const card = e.target.closest('.proj-card[role="button"]');
        if (!card) return;
        if (e.key === 'Enter' || e.key === ' ') { e.preventDefault(); onClick(e); }
    };

    document.getElementById('bank_cards')?.addEventListener('click', onClick);
    document.getElementById('inspect_cards')?.addEventListener('click', onClick);
    document.getElementById('bank_cards')?.addEventListener('keydown', onKey);
    document.getElementById('inspect_cards')?.addEventListener('keydown', onKey);
}

// open edit by mapping id (calls your API, fills modal by type)
async function openEditById(id, queueTypeId) {
    try {
        const res = await fetch(`${baseUrl}OtherSettings/GetProjectCounterDetail?id=${encodeURIComponent(id)}`);
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        const { data } = await res.json(); // ← matches Controller action below

        if (queueTypeId === 48) {
            document.getElementById('Projectnamebank').value = data?.ProjectName || "";
            document.getElementById('counterQty48').value = Number(data?.EndCounter) || 0;
            document.getElementById('hdmdIDprojectcounterbankedit').value = Number(data?.ID) || 0;
            document.getElementById('Updatedatebank').value = data?.UpdateDate || "";
            document.getElementById('Updatebybank').value = data?.UpdateName || "";
            await loadAndRenderBanksEdit('bankGridEdit');
            await hydrateBankSelectionFromVm(data?.Banks);
            applyBankStateToGrid('bankGridEdit');   // <- was 'bankGrid'
            const m = new bootstrap.Modal(document.getElementById('modalQueue48'));
            m.show();
        } else {
            // 49
            document.getElementById('counterQty49').value = Number(data?.EndCounter) || 0;
            document.getElementById('ProjectnameInspect').value = data?.ProjectName || "";
            document.getElementById('hdmdIDprojectcounterInspectedit').value = Number(data?.ID) || 0;
            document.getElementById('UpdatedateInspect').value = data?.UpdateDate || "";
            document.getElementById('UpdatebyInspect').value = data?.UpdateName || "";
            const m = new bootstrap.Modal(document.getElementById('modalQueue49'));
            m.show();
        }
    } catch (err) {
        console.error('openEditById error:', err);
    }
}

async function loadAndRenderBanksEdit(containerId = 'bankGridEdit') {
    const grid = document.getElementById(containerId);
    if (!grid) return;

    // VERY simple path builder
    const BANK_LOGO_BASE = baseUrl + 'image/ThaiBankicon/';

    grid.innerHTML = '<div class="col-12 small text-muted px-2">Loading…</div>';

    try {
        const res = await fetch(baseUrl + 'OtherSettings/GetListBank', { method: 'GET' });
        if (!res.ok) throw new Error('HTTP ' + res.status);
        const banks = await res.json(); // [{ ValueString: BankCode, Text: BankName, ValueInt: Id }]

        grid.innerHTML = '';

        // simple foreach render
        banks.forEach(b => {
            const code = (b?.ValueString || '').trim();
            const name = b?.Text || '';
            const id = b?.ValueInt ?? '';

            const html = `
                            <div class="col-12 col-md-6 col-xl-4">
                              <div class="card h-100 border-0 shadow-sm" data-bank="${code}" data-bank-id="${id}">
                                <div class="card-body">
                                  <div class="d-flex align-items-center justify-content-between gap-3 flex-wrap">
                                    <div class="d-flex align-items-center gap-3 flex-grow-1">
                                      <img src="${BANK_LOGO_BASE}${code}.png"
                                           alt="${code}"
                                           class="rounded-circle border"
                                           style="width:44px;height:44px;">
                                      <div class="fw-semibold text-wrap">${code} - ${name}</div>
                                    </div>
                                    <div class="d-flex align-items-center gap-2">
                                      <div class="form-check form-switch m-0">
                                        <input class="form-check-input bank-check" type="checkbox">
                                      </div>
                                      <input type="number" class="form-control form-control-sm bank-qty"
                                             placeholder="จำนวน…" min="0" value="0" style="width:110px;">
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                            `;

            grid.insertAdjacentHTML('beforeend', html);
        });

        // minimal wiring (optional)
        grid.querySelectorAll('.card').forEach(card => {
            const chk = card.querySelector('.bank-check');
            const qty = card.querySelector('.bank-qty');
            qty.disabled = !chk.checked;
            chk.addEventListener('change', () => qty.disabled = !chk.checked);
        });

    } catch (err) {
        console.error('loadAndRenderBanks error:', err);
        grid.innerHTML = `
      <div class="col-12">
        <div class="alert alert-warning mb-0">ไม่สามารถโหลดรายชื่อธนาคารได้</div>
      </div>`;
    }
}


async function hydrateBankSelectionFromVm(banks) {
    try {
    window.__bankSelectionState = new Map();
    (banks || []).forEach(b => {
        const code = b.BankCode;                  // from VM
        const qty = Number(b.Staff) || 0;
        const checked = (b.FlagActive === true) && qty > 0;
        if (code) window.__bankSelectionState.set(code, { checked, qty });
    });
    } catch (err) {
        console.error('load hydrateBankSelectionFromVm is error:', err);
    }
}

function collectEditBanks(containerId = 'bankGridEdit') {
    return Array.from(document.querySelectorAll(`#${containerId} .card[data-bank]`))
        .map(card => ({
            bankId: Number(card.getAttribute('data-bank-id')) || 0,
            code: card.getAttribute('data-bank') || '',
            checked: !!card.querySelector('.bank-check')?.checked,
            qty: Number(card.querySelector('.bank-qty')?.value) || 0
        }));
}


document.getElementById('btnSaveCounter')?.addEventListener('click', async () => {
    showLoading();
    const payload = collectCreateCounterPayload();
    try {
        const res = await fetch(baseUrl + 'OtherSettings/CreateCounter', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)
        });

        if (!res.ok) throw new Error('HTTP ' + res.status);

        const json = await res.json();
        document.querySelector('#counterModal [data-bs-dismiss="modal"]')?.click();
        fetchProjectCounters?.();
        hideLoading();
        successMessage('บันทึกสำเร็จ');  
    } catch (err) {
        hideLoading();
        errorMessage('บันทึกล้มเหลว');  
    }
});

document.getElementById('btnSaveQueue48')?.addEventListener('click', async () => {
    showLoading();
    const id = Number(document.getElementById('hdmdIDprojectcounterbankedit')?.value) || 0;
    const counterQty = Number(document.getElementById('counterQty48')?.value) || 0;
    const banks = collectEditBanks('bankGridEdit');

    if (id <= 0) {
        hideLoading();
        showWarning('ไม่พบรหัสข้อมูล (ID).', 5000);
        return;
    }

    const payload = { id, queueTypeId: 48, counterQty, banks };

    try {
        const res = await fetch(baseUrl + 'OtherSettings/UpdateCounterBank', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        const json = await res.json(); // อ่าน JSON ก่อน
        if (!res.ok || !json?.ok) {
            throw new Error(json?.message || `HTTP ${res.status}`);
        }

        // ✅ success
        document.querySelector('#modalQueue48 [data-bs-dismiss="modal"]')?.click();
        fetchProjectCounters?.();
        hideLoading();
        successMessage(json?.message || 'อัปเดตสำเร็จ');

    } catch (err) {
        hideLoading();
        errorMessage(err.message || 'อัปเดตไม่สำเร็จ');
        console.error('UpdateCounterBank error:', err);
    }
});


document.getElementById('btnSaveQueue49')?.addEventListener('click', async () => {
    showLoading();
    const id = Number(document.getElementById('hdmdIDprojectcounterInspectedit')?.value) || 0;
    const counterQty = Number(document.getElementById('counterQty49')?.value) || 0;
    if (id <= 0) { showWarning('ไม่พบรหัสข้อมูล (ID).', 2500); return; }
    const payload = {
        id,
        queueTypeId: 49,   
        counterQty: counterQty,
    };

    try {
        const res = await fetch(baseUrl + 'OtherSettings/UpdateCounterInspect', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });
        if (!res.ok) throw new Error('HTTP ' + res.status);
        const json = await res.json();
        if (!json?.ok) throw new Error(json?.message || 'Update failed');
        document.querySelector('#modalQueue49 [data-bs-dismiss="modal"]')?.click(); // close
        fetchProjectCounters?.();
        hideLoading();
        successMessage('อัปเดตสำเร็จ');          
    } catch (err) {
        hideLoading();
        errorMessage('อัปเดตไม่สำเร็จ');
    }
});


// helper: ดึง filename จาก Content-Disposition
function getFilenameFromHeaders(res, fallbackName) {
    const disp = res.headers.get('Content-Disposition');
    if (!disp) return fallbackName;
    let m = /filename\*?=(?:UTF-8'')?["']?([^"';]+)["']?/i.exec(disp);
    if (!m) m = /filename=["']?([^"';]+)["']?/i.exec(disp);
    return m && m[1] ? decodeURIComponent(m[1]) : fallbackName;
}

// helper: บังคับดาวน์โหลด blob
function triggerDownload(blob, filename) {
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    a.remove();
    URL.revokeObjectURL(url);
}
