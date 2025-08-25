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
  <div class="col-12 col-md-6">
    <div class="proj-card d-flex flex-column"
         role="button" tabindex="0" style="cursor:pointer"
         data-id="${row.ID}"
         data-project="${escapeHtml(row.ProjectID)}"
         data-queue="48"
         data-end="${row.EndCounter}">
      <div class="d-flex justify-content-between align-items-start">
        <div>
          <h3 class="site-name">${title}</h3>
          <div class="mt-1"><span class="subtype">${subtype}</span></div>
        </div>
        <span class="count-badge">${units} หน่วย</span>
      </div>
      <div class="mt-auto pt-3 d-flex justify-content-between align-items-center flex-wrap btn-row">
        <button class="action-btn" onclick="event.stopPropagation()"><i class="fa-solid fa-qrcode me-1"></i>Download QR</button>
        <div class="d-flex btn-row">
          <button class="icon-btn" title="Staff count" onclick="event.stopPropagation()">
            <i class="fa-solid fa-user"></i>Staff ${staffCount}
          </button>
          <button class="icon-btn" title="Bank count" onclick="event.stopPropagation()">
            <i class="fa-solid fa-landmark"></i>Bank ${bankCount}
          </button>
        </div>
      </div>
    </div>
  </div>`;
}

function buildInspectCard(row) {
    const title = escapeHtml(row.ProjectName || row.ProjectID || '');
    const subtype = escapeHtml(row.QueueTypeName || 'Inspect (รับมอบห้อง)');
    const units = positionCountInclusive(row.StartCounter, row.EndCounter);

    return `
  <div class="col-12 col-md-6">
    <div class="proj-card d-flex flex-column"
         role="button" tabindex="0" style="cursor:pointer"
         data-id="${row.ID}"
         data-project="${escapeHtml(row.ProjectID)}"
         data-queue="49"
         data-end="${row.EndCounter}">
      <div class="d-flex justify-content-between align-items-start">
        <div>
          <h3 class="site-name">${title}</h3>
          <div class="mt-1"><span class="subtype">${subtype}</span></div>
        </div>
        <span class="count-badge" style="background:#f0e8ff;color:#6f3acb;">${units} หน่วย</span>
      </div>
      <div class="mt-auto pt-3 d-flex justify-content-between align-items-center flex-wrap btn-row">
        <button class="action-btn" onclick="event.stopPropagation()"><i class="fa-solid fa-qrcode me-1"></i>Download QR</button>
      </div>
    </div>
  </div>`;
}

function bindCardClickHandlers() {
    const openFromCard = (card) => {
        if (!card) return;
        const queue = Number(card.dataset.queue);
        const end = Number(card.dataset.end || 0);
        const proj = card.dataset.project;

        // keep currently selected project for save payloads
        window.__selectedProjectIds = proj ? [proj] : [];

        // open the right modal (48 = bank, 49 = non-bank)
        showQueueTypeModal(queue, end);
    };

    const containerClick = (e) => {
        const card = e.target.closest('.proj-card[role="button"]');
        if (!card) return;
        // ignore clicks from inner buttons
        if (e.target.closest('button')) return;
        openFromCard(card);
    };

    const containerKey = (e) => {
        const card = e.target.closest('.proj-card[role="button"]');
        if (!card) return;
        if (e.key === 'Enter' || e.key === ' ') {
            e.preventDefault();
            openFromCard(card);
        }
    };

    document.getElementById('bank_cards')?.addEventListener('click', containerClick);
    document.getElementById('inspect_cards')?.addEventListener('click', containerClick);
    document.getElementById('bank_cards')?.addEventListener('keydown', containerKey);
    document.getElementById('inspect_cards')?.addEventListener('keydown', containerKey);
}


// -------- Render --------
function renderCounters(data) {
    const bankWrap = document.getElementById('bank_cards');
    const inspectWrap = document.getElementById('inspect_cards');
    if (bankWrap) bankWrap.innerHTML = '';
    if (inspectWrap) inspectWrap.innerHTML = '';

    if (!Array.isArray(data) || data.length === 0) {
        if (bankWrap) bankWrap.innerHTML = emptyCard('ไม่พบข้อมูล');
        if (inspectWrap) inspectWrap.innerHTML = '';
        return;
    }

    const bank = data.filter(x => Number(x.QueueTypeID) === 48);
    const inspect = data.filter(x => Number(x.QueueTypeID) === 49);

    if (bankWrap) bankWrap.innerHTML = bank.length ? bank.map(buildBankCard).join('') : emptyCard('ไม่พบข้อมูล Bank Pre-Approve');
    if (inspectWrap) inspectWrap.innerHTML = inspect.length ? inspect.map(buildInspectCard).join('') : '';

    // 👉 enable click-to-edit
    bindCardClickHandlers();
}


// -------- Fetch --------
async function fetchProjectCounters() {
    const bu = getMultiSelectValues('#ddl_BUG_counter');            // ex. ['1','3']
    const prj = getMultiSelectValues('#ddl_PROJECT_counter');       // ex. ['102C028','102C029']
    let rawValue = document.getElementById('ddl_counter_type')?.value ?? "-1";

    // ถ้า clear แล้ว rawValue จะเป็น "" => แก้ให้เป็น "-1"
    if (rawValue === "" || rawValue === null) {
        rawValue = "-1";
    }
    const queueType = Number(rawValue);
    console.log('ddl_counter_type : ' + queueType);

    const params = new URLSearchParams({
        Bu: toCommaList(bu, { trailing: false }),                      // "1,3"
        ProjectID: toCommaList(prj, { trailing: true }),               // "102C028,102C029," สำหรับ logic LIKE ฝั่ง SQL
        QueueType: isNaN(queueType) ? '-1' : String(queueType)
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

// Load banks (keep server order by LineOrder) and render cards into #bankGrid
async function loadAndRenderBanks(containerId = 'bankGrid') {
    const grid = document.getElementById(containerId);
    if (!grid) return;

    // persist user toggles across reloads (one global map)
    window.__bankSelectionState = window.__bankSelectionState || new Map();

    // Build logo base URL (no Razor "~/" needed)
    const ROOT = (document.querySelector('base')?.href || window.location.origin + '/');
    const BANK_LOGO_BASE = new URL('image/ThaiBankicon/', ROOT).href;

    // filename is BankCode + ".png"
    const bankLogoSrc = (code) => `${BANK_LOGO_BASE}${code}.png`;

    // dedupe but KEEP INPUT ORDER (server already orders by LineOrder)
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

        chk.addEventListener('change', () => { sync(); recalcBankQuota(); });
        qty.addEventListener('input', () => { sync(); recalcBankQuota(qty); });

        sync();
    }


    const renderCard = ({ ValueInt: id, ValueString: code, Text: name }) => {
        const prev = window.__bankSelectionState.get(code) || { checked: false, qty: 0 };
        const col = document.createElement('div');
        col.className = 'col-12 col-md-6 col-xl-4';
        col.innerHTML = `<div class="card h-100 border-0 shadow-sm" data-bank="${escapeHtml(code)}" data-bank-id="${id}">
                              <div class="card-body">
                                <div class="d-flex align-items-center justify-content-between gap-3 flex-wrap">
                                  <div class="d-flex align-items-center gap-3 flex-grow-1">
                                    <img src="${bankLogoSrc(code)}" alt="${escapeHtml(code)}" class="rounded-circle border" style="width:44px;height:44px;">
                                    <div>
                                      <div class="fw-semibold text-wrap">${escapeHtml(code)} - ${escapeHtml(name)}</div>
                                    </div>
                                  </div>
                                  <div class="d-flex align-items-center gap-2">
                                    <div class="form-check form-switch m-0">
                                      <input class="form-check-input bank-check" type="checkbox" ${prev.checked ? 'checked' : ''}>
                                    </div>
                                    <input type="number" class="form-control form-control-sm bank-qty" placeholder="จำนวน…" min="0" value="${prev.qty || 0}" style="width:110px;">
                                  </div>
                                </div>
                              </div>
                           </div>`;
        wireCardBehaviour(col.querySelector('.card'));
        return col;
    };


    // Loading state
    grid.innerHTML = `
    <div class="col-12">
      <div class="d-flex align-items-center text-muted small px-2">
        <i class="fa fa-spinner fa-spin me-2"></i>Loading banks…
      </div>
    </div>`;

    try {
        const res = await fetch(baseUrl + 'OtherSettings/GetListBank', { method: 'GET' });
        if (!res.ok) throw new Error('HTTP ' + res.status);
        // expect server already ordered by LineOrder
        let data = await res.json(); // [{ ValueString: BankCode, Text: BankName }]

        // keep server order, just dedupe by code
        data = dedupeBy(data, 'ValueString');

        // render
        grid.innerHTML = '';
        const frag = document.createDocumentFragment();
        data.forEach(item => frag.appendChild(renderCard(item)));
        grid.appendChild(frag);

        // Select all / Clear (if buttons exist)
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
    return Array.from(document.querySelectorAll('#bankGrid .card')).reduce((acc, card) => {
        const chk = card.querySelector('.bank-check');
        const qty = card.querySelector('.bank-qty');
        if (chk && chk.checked && qty) acc += Number(qty.value) || 0;
        return acc;
    }, 0);
}

/**
 * Recalculate remaining quota and clamp last changed input if over cap.
 * @param {HTMLInputElement|null} lastQtyInput - pass the qty input that changed (for clamping)
 */
function recalcBankQuota(lastQtyInput = null) {
    const saveBtn = document.getElementById('btnSaveCounter');
    const hint = ensureQuotaHint();
    const cap = Number(document.getElementById('counterQty')?.value) || 0;

    // If we need to clamp, compute "others" then force this input to fit.
    if (lastQtyInput) {
        const cards = Array.from(document.querySelectorAll('#bankGrid .card'));
        const codeOfLast = lastQtyInput.closest('.card')?.getAttribute('data-bank');
        const sumOthers = cards.reduce((acc, card) => {
            const chk = card.querySelector('.bank-check');
            const qty = card.querySelector('.bank-qty');
            const code = card.getAttribute('data-bank');
            if (!chk || !qty) return acc;
            if (chk.checked && code !== codeOfLast) acc += Number(qty.value) || 0;
            return acc;
        }, 0);

        const maxForThis = Math.max(0, cap - sumOthers);
        const cur = Number(lastQtyInput.value) || 0;
        if (cur > maxForThis) {
            lastQtyInput.value = String(maxForThis);
            showWarning("กรุณาตรวจสอบจำนวนเคาน์เตอร์", 2000);
        }
    }

    const total = sumCheckedBankQty();
    const remaining = Math.max(0, cap - total);

    // hint UI
    if (hint) {
        hint.textContent = `คงเหลือ: ${remaining}`;
        hint.classList.toggle('text-danger', total > cap);
        hint.classList.toggle('text-muted', total <= cap);
    }

    // disable save if over (defensive)
    if (saveBtn) saveBtn.disabled = total > cap;

    // 🔔 show warning if over cap, but throttle so it doesn't spam
    if (total > cap) {
        console.log('total > cap');
        if (!window.__quotaWarnCooldown) {
            showWarning("จำนวนสตาฟรวมเกินกว่าโควต้าที่กำหนด", 60 * 1000); // 1 minute
            window.__quotaWarnCooldown = true;
            setTimeout(() => { window.__quotaWarnCooldown = false; }, 10000); // 10s cooldown
        }
    }
}


async function openCounterModalMock() {
    const modalEl = document.getElementById('counterModal');
    if (!counterModalInstance) {
        counterModalInstance = new bootstrap.Modal(modalEl, { backdrop: 'static' });
    }

    // load list EVERY time before showing (so it's always fresh)
    await loadProjectCounterMapping(true);

    await loadAndRenderBanks(); 
    // show modal
    counterModalInstance.show();
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
recalcBankQuota(); 

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


// ---- Post payload to server ----
document.getElementById('btnSaveCounter')?.addEventListener('click', async () => {
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
        console.log('Server response:', json);

        // show success feedback
        showWarning('บันทึกสำเร็จ', 2000);
        // close modal
        document.querySelector('#counterModal [data-bs-dismiss="modal"]')?.click();

    } catch (err) {
        console.error('CreateCounter error:', err);
        showWarning('บันทึกล้มเหลว', 3000);
    }
});


// hydrate selection from server VM (Banks[])
function hydrateBankSelectionFromVm(banks) {
    window.__bankSelectionState = new Map();
    (banks || []).forEach(b => {
        const code = b.BankCode;                  // from VM
        const qty = Number(b.Staff) || 0;
        const checked = (b.FlagActive === true) && qty > 0;
        if (code) window.__bankSelectionState.set(code, { checked, qty });
    });
}

// after loadAndRenderBanks(), sync check/qty on the rendered cards to __bankSelectionState
function applyBankStateToGrid(containerId = 'bankGrid') {
    const grid = document.getElementById(containerId);
    if (!grid) return;
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

// open edit by mapping id (calls your API, fills modal by type)
async function openEditById(id, queueTypeId) {
    try {
        console.log(id);
        const res = await fetch(`${baseUrl}OtherSettings/GetProjectCounterDetail?id=${encodeURIComponent(id)}`);
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        const { data } = await res.json(); // ← matches Controller action below

        if (queueTypeId === 48) {
            document.getElementById('counterQty48').value = Number(data?.EndCounter) || 0;
            // pre-hydrate bank state from server
            hydrateBankSelectionFromVm(data?.Banks);
            // render bank grid, then apply state and show
            await loadAndRenderBanks('bankGrid');
            applyBankStateToGrid('bankGrid');

            const m = new bootstrap.Modal(document.getElementById('modalQueue48'));
            m.show();
        } else {
            // 49
            document.getElementById('counterQty49').value = Number(data?.EndCounter) || 0;
            const m = new bootstrap.Modal(document.getElementById('modalQueue49'));
            m.show();
        }
    } catch (err) {
        console.error('openEditById error:', err);
        // (optional) toast/alert
    }
}

// If you previously wired clicks, switch them to use openEditById:
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
