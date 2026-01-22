/***********************
 * UserBank - index.js *
 ***********************/


// ===== Utils =====
const $id = (x) => document.getElementById(x);

// สร้าง element แบบง่าย
const el = (tag, cls = '', html = '') => {
    const e = document.createElement(tag);
    if (cls) e.className = cls;
    if (html) e.innerHTML = html;
    return e;
};

let choicesBank, choicesProject;
let cacheBanks = null, cacheProjects = null;
let choicesFilterBanks; // multi-select for bank filter (top-left)

// ---- Bank icons (ใช้โฟลเดอร์ภายใต้เว็บรูทเดียว) ----
const BANK_ICON_DIR = 'image/ThaiBankicon/';
const bankIconPath = (code) => (baseUrl + BANK_ICON_DIR + `${(code || '').toUpperCase()}.png`);

function bindLogoWithFallback(imgEl, code) {
    if (!imgEl) return;
    const src = bankIconPath(code);
    imgEl.src = src;
    imgEl.onerror = () => {
        imgEl.onerror = null;
        imgEl.src = baseUrl + BANK_ICON_DIR + 'DEFAULT.png';
        console.warn('Bank logo not found:', src);
    };
}

// ===== Normalize key =====
const normalizeUserBank = (o = {}) => ({
    id: o.ID ?? o.id ?? null,
    fullName: o.FullName ?? o.fullName ?? '',
    bankCode: o.BankCode ?? o.bankCode ?? '',
    bankName: o.BankName ?? o.bankName ?? '',
    role: o.Role ?? o.role ?? null // 1=Lead, 2=Crew, 3=No-crew
});

// ===== Page init =====
document.addEventListener('DOMContentLoaded', () => {
    loadUserBankPage();

    // เปลี่ยนธนาคารหลัก (เผื่อเอาไปกรองต่อ)
    //$id('ddl_bank')?.addEventListener('change', () => {
        // TODO: add filter logic if needed
    //});

    // Remove แถวในทีม (UI)
    $id('teamTbody')?.addEventListener('click', (e) => {
        const btn = e.target.closest('.btn-remove-row');
        if (!btn) return;
        btn.closest('tr')?.remove();
    });
});

// ===== Call /UserBank/Page_Load =====
async function loadUserBankPage() {
    try {
        window.showLoading?.();

        const res = await fetch(baseUrl + 'UserBank/Page_Load', { method: 'GET' });
        const json = await res.json();

        if (!json?.success) return;

        // 1) เติม dropdown ธนาคาร (listBank: ValueInt, Text)
        renderBankDropdown(json.listBank || []);

        // 2) วาดการ์ด CountUserByBank (listCountUserByBankk: CntUserByBank, BankCode, BankName)
        renderCountUserByBankCards(json.listCountUserByBankk || []);

        // 3) วาดรายชื่อผู้ใช้ (listUserBank: FullName, BankName, BankCode, Role)
        renderUserBankList(json.listUserBank || []);

    } catch (err) {
        console.error(err);
    } finally {
        window.hideLoading?.();
    }
}

// ===== Render dropdown =====
function renderBankDropdown(listBank) {
    const ddl = $id('ddl_bank');
    if (!ddl) return;

    // init Choices once
    if (typeof Choices !== 'undefined' && !choicesFilterBanks) {
        choicesFilterBanks = new Choices('#ddl_bank', {
            removeItemButton: true,
            searchEnabled: true,
            shouldSort: false,
            placeholder: true,
            placeholderValue: 'เลือกธนาคาร',
            itemSelectText: ''
        });

        // react to changes (selected bank IDs array)
        ddl.addEventListener('change', () => {
            const selected = choicesFilterBanks.getValue(true); // ['1','2', ...]
            // TODO: call your filter reload here, e.g.:
            // reloadUserList({ bankIds: selected });
        });
    }

    // Build options for Choices
    const options = (listBank || []).map(b => ({
        value: String(b.ValueInt),
        label: b.Text
    }));

    if (choicesFilterBanks) {
        // Clear and set fresh options (preserve selected empty)
        choicesFilterBanks.clearStore();
        choicesFilterBanks.setChoices(options, 'value', 'label', true);
    } else {
        // Fallback (if Choices not loaded)
        ddl.querySelectorAll('option:not([value=""])').forEach(o => o.remove());
        for (const b of (listBank || [])) {
            const opt = document.createElement('option');
            opt.value = b.ValueInt;
            opt.textContent = b.Text;
            ddl.appendChild(opt);
        }
    }
}


// ===== Render CountUserByBank cards =====
function renderCountUserByBankCards(items) {
    const host = $id('bankCountCards');
    if (!host) return;
    host.innerHTML = '';

    (items || []).forEach(x => {
        const col = el('div', 'col-6 col-sm-4 col-lg-2 mb-2');
        const card = el('div', 'card shadow-sm h-100');
        card.style.borderRadius = '10px';

        const body = el('div', 'card-body d-flex align-items-center gap-2 p-2');

        const iconBox = el(
            'div',
            'd-flex justify-content-center align-items-center rounded',
            `<img src="${bankIconPath(x.BankCode)}" alt="${x.BankCode}" style="width:28px;height:28px;object-fit:contain;">`
        );
        iconBox.style.width = '36px';
        iconBox.style.height = '36px';
        iconBox.style.background = 'rgba(0,0,0,.04)';

        const textBox = el('div', 'flex-grow-1');
        textBox.innerHTML = `
      <div class="fw-semibold" style="font-size:0.85rem;line-height:1.2;">
        ${x.BankName || x.BankCode || '-'}
      </div>
      <div class="text-muted small" style="font-size:0.75rem;">
        Users: <span class="fw-bold">${Number(x.CntUserByBank || 0).toLocaleString()}</span>
      </div>
    `;

        body.appendChild(iconBox);
        body.appendChild(textBox);
        card.appendChild(body);
        col.appendChild(card);
        host.appendChild(col);
    });
}

// === Render รายชื่อซ้าย และ bind click ===
function renderUserBankList(data) {
    const container = document.getElementById('userBankList');
    if (!container) return;

    container.innerHTML = '';

    (data || []).map(normalizeUserBank).forEach((u, idx) => {
        const item = document.createElement('a');
        item.className = 'list-group-item list-group-item-action list-hover-primary nav-link d-flex align-items-start gap-3 p-2 mb-2 position-relative';
        if (idx === 0) item.classList.add('active');
        item.style.borderRadius = '8px';
        item.style.transition = 'background-color 0.2s';
        item.href = '#v-pills-user';
        item.setAttribute('role', 'tab');

        // เก็บ id + role
        item.dataset.id = u.id;
        item.dataset.role = u.role; // 1=Lead, 2=Crew, 3=No-crew

        item.innerHTML = `
      ${u.role === 1 ? `<span class="ribbon-badge">LEAD</span>` : ``}
      <div class="icon-container d-flex justify-content-center align-items-center" style="width:36px;height:36px;padding:2px;">
        <img src="${bankIconPath(u.bankCode)}" alt="${u.bankCode}" style="width:100%;height:100%;object-fit:contain;">
      </div>
      <div class="flex-grow-1">
        <div class="text-dark fw-semibold">${u.fullName || '-'}</div>
        <div class="text-muted small">
          <span class="text-primary">${u.bankName || u.bankCode || '-'}</span>
        </div>
      </div>
    `;

        // click → โหลดข้อมูลแก้ไข + โหลดทีม
        item.addEventListener('click', async (e) => {
            e.preventDefault();

            // เปลี่ยน active ใน list
            [...container.querySelectorAll('.list-group-item')].forEach(a => a.classList.remove('active'));
            item.classList.add('active');

            const id = Number(item.dataset.id);
            if (!id) return;
            if (typeof loadUserBankForEdit === 'function') {
                await loadUserBankForEdit(id);
            }
        });

        container.appendChild(item);
    });

    // auto load item แรก
    const first = container.querySelector('.list-group-item');
    if (first) first.click();
}

function ensureChoices() {
    if (typeof Choices === 'undefined') {
        console.warn('Choices.js not loaded on this page.');
        return;
    }
    if (!choicesBank) {
        choicesBank = new Choices('#ddlEditBank', {
            removeItemButton: false, searchEnabled: true, shouldSort: false, placeholderValue: 'Select bank'
        });
    }
    if (!choicesProject) {
        choicesProject = new Choices('#projectMultiSelect', {
            removeItemButton: true, searchEnabled: true, shouldSort: false, placeholderValue: 'Select projects'
        });
    }
}

// เติมข้อมูล dropdown (ใช้กับ Choices)
function setChoicesOptions(choices, list) {
    // list: [{value, label}]
    if (!choices) return;
    choices.clearStore();
    choices.setChoices(list, 'value', 'label', true);
}

async function getBanks() {
    if (cacheBanks) return cacheBanks;
    const res = await fetch(baseUrl + 'UserBank/GetListBank');
    const json = await res.json();
    // server ส่ง { ValueInt, Text }
    cacheBanks = (json || []).map(x => ({ value: String(x.ValueInt), label: x.Text }));
    return cacheBanks;
}

async function getProjects() {
    if (cacheProjects) return cacheProjects;
    const res = await fetch(baseUrl + 'UserBank/GetListProject');
    const json = await res.json();
    // server ส่ง { ValueString, Text } หรือ ValueInt แล้วแต่ฝั่ง server
    cacheProjects = (json || []).map(x => ({ value: String(x.ValueString ?? x.ValueInt ?? x.Value), label: x.Text }));
    return cacheProjects;
}

async function loadUserBankForEdit(userId) {
    try {
        window.showLoading?.();
        ensureChoices();

        // โหลด dropdown (ครั้งแรก)
        const [banks, projects] = await Promise.all([getBanks(), getProjects()]);
        setChoicesOptions(choicesBank, banks);
        setChoicesOptions(choicesProject, projects);

        // ดึงข้อมูลผู้ใช้
        const url = baseUrl + 'UserBank/GetUserBankById?id=' + encodeURIComponent(userId);
        const res = await fetch(url);
        if (!res.ok) throw new Error(`GET ${res.status} ${res.statusText}`);

        const json = await res.json();
        if (!json?.success || !json.data) throw new Error('No data');

        const u = json.data;
        window.currentEditUserId = Number(u.id ?? u.ID ?? userId) || 0;

        // ---- Header: โลโก้ + ชื่อ + ธนาคาร ----
        const logo = $id('editUserBankLogo');
        const nameEl = $id('editUserBankFullName');
        const bankEl = $id('editUserBankBankName');

        const bankCode = u.bankCode ?? u.BankCode ?? '';
        const first = u.firstName ?? u.FirstName ?? '';
        const last = u.lastName ?? u.LastName ?? '';
        const bankName = u.bankName ?? u.BankName ?? '';

        if (logo) bindLogoWithFallback(logo, bankCode);
        if (nameEl) nameEl.textContent = `${first} ${last}`.trim();
        if (bankEl) bankEl.textContent = bankName || bankCode || '';

        // ---- ฟอร์มหลัก ----
        const isLeadFromData = !!(u.isLeadBank ?? u.IsLeadBank);
        const bankIdStr = String(u.bankID ?? u.BankID ?? '');

        const chkIsLead = $id('chkIsLead');
        if (chkIsLead) chkIsLead.checked = isLeadFromData;

        if (choicesBank) {
            bankIdStr ? choicesBank.setChoiceByValue(bankIdStr) : choicesBank.removeActiveItems();
        }

        const inpFirstName = $id('inpFirstName');
        const inpLastName = $id('inpLastName');
        const inpMobile = $id('inpMobile');
        const inpEmail = $id('inpEmail');
        const inpUsername = $id('inpUsername');
        const inpPassword = $id('inpPassword');

        if (inpFirstName) inpFirstName.value = first;
        if (inpLastName) inpLastName.value = last;
        if (inpMobile) inpMobile.value = u.mobile ?? u.Mobile ?? '';
        if (inpEmail) inpEmail.value = u.email ?? u.Email ?? '';
        if (inpUsername) inpUsername.value = u.userName ?? u.UserName ?? '';
        if (inpPassword) inpPassword.value = u.password ?? u.Password ?? '';

        // Projects (Choices multi)
        const projValues = (u.projectUserBank ?? u.ProjectUserBank ?? [])
            .map(p => String(p.projectID ?? p.ProjectID))
            .filter(Boolean);

        if (choicesProject) {
            choicesProject.removeActiveItems();
            if (projValues.length) choicesProject.setChoiceByValue(projValues);
        }

        // ===== Area (checkbox จาก CSV) =====
        (function setAreaChecksFromCsv() {
            const csv = String(u.areaID ?? u.AreaID ?? '').trim(); // ตัวอย่าง: "461,463,464"
            const selected = new Set(csv ? csv.split(',').map(s => s.trim()).filter(Boolean) : []);
            const all = document.querySelectorAll('input[name="AreaIDs"]');
            all.forEach(el => { el.checked = false; });
            all.forEach(el => {
                const v = String(el.value).trim();
                if (selected.has(v)) el.checked = true;
            });
        })();

        // ===== ตัดสินบทบาทหัวหน้าทีม (ถ้าเมนูซ้าย active บอก role=1 ให้ถือเป็นหัวหน้า) =====
        const activeLeft = document.querySelector('#userBankList .list-group-item.active');
        const roleFromLeft = Number(activeLeft?.dataset.role || NaN);
        const isLead = isLeadFromData || roleFromLeft === 1;

        // Team Name show/hide (NOT team lead => show)
        const parentTeamInput = $id('inpParentTeam');
        const parentTeamRow = parentTeamInput?.closest('.mb-3.row');
        const parentTeamStr = (u.parentTeam ?? u.ParentTeam ?? '').trim();

        if (isLead) {
            if (parentTeamRow) parentTeamRow.classList.add('d-none');
            if (parentTeamInput) parentTeamInput.value = '';
        } else {
            if (parentTeamRow) parentTeamRow.classList.remove('d-none');
            if (parentTeamInput) parentTeamInput.value = parentTeamStr || '';
        }

        // ซ่อน/แสดงแท็บ Team ตามบทบาท
        toggleTeamTab(isLead);

        // ===== โหลดตารางทีมตามบทบาท =====
        try {
            const fromApi = {
                id: u.id ?? u.ID,
                role: u.role, // อาจไม่มี
                isLeadBank: u.isLeadBank ?? u.IsLeadBank,
                parentBankID: u.parentBankID ?? u.ParentBankID
            };

            let parentId = getParentBankIdForTeam(fromApi);

            if (!parentId) {
                const active = document.querySelector('#userBankList .list-group-item.active');
                if (active) {
                    const roleAttr = Number(active.dataset.role || NaN);
                    const idAttr = Number(active.dataset.id || NaN);
                    if (roleAttr === 1 && idAttr) parentId = idAttr;
                }
            }

            if (parentId) {
                window.currentLeadTeamId = Number(parentId) || 0;
                window.currentBankIdForAdd = Number(u.bankID ?? u.BankID ?? bankIdStr ?? 0);

                await loadTeamTableByParent(parentId);
                await setTeamName(parentId, u);
                if (bankIdStr) loadAddMoreUserBankDropdown(bankIdStr, parentId);
            } else {
                renderTeamRows([]);
                await setTeamName(null, u);
                if (bankIdStr) loadAddMoreUserBankDropdown(bankIdStr, 0);
            }
        } catch (e) {
            console.error(e);
            renderTeamRows([]);
        }

    } catch (e) {
        console.error(e);
        if (typeof Swal !== 'undefined') {
            await Swal.fire({ icon: 'error', title: 'โหลดข้อมูลล้มเหลว', text: String(e?.message || e) });
        }
    } finally {
        window.hideLoading?.();
    }
}


function toggleTeamTab(isLead) {
    const teamTab = document.getElementById('team-tab');      // <a>
    const teamLi = teamTab?.closest('.nav-item');            // <li>
    const teamPane = document.getElementById('team-filter');   // pane

    const detailTab = document.getElementById('detail-tab');
    const detailPane = document.getElementById('detail-filter');

    if (!teamTab || !teamPane || !detailTab || !detailPane) return;

    if (isLead) {
        // แสดงแท็บ Team
        teamLi?.classList.remove('d-none');
        teamTab.removeAttribute('tabindex');
    } else {
        // ซ่อนแท็บ Team + สลับไป Detail
        teamLi?.classList.add('d-none');
        teamTab.setAttribute('tabindex', '-1');

        // ยกเลิก active บน Team ถ้าเผลอค้าง
        teamTab.classList.remove('active', 'show');
        teamPane.classList.remove('active', 'show');

        // เปิด Detail ให้ชัดเจน
        detailTab.classList.add('active');
        detailPane.classList.add('active', 'show');
    }
}


// ===== helper: สรุป role → parentBankId =====
// role: 1 = Team Lead, 2 = Crew, 3 = No-crew
function getParentBankIdForTeam(u) {
    const id = Number(u.id ?? u.ID);
    const role = Number(u.role ?? u.Role);
    const isLead = !!(u.isLeadBank ?? u.IsLeadBank) || role === 1;
    const parent = Number(u.parentBankID ?? u.ParentBankID);

    if (isLead && id) return id;    // Lead → ใช้ ID ตัวเอง
    if (parent) return parent;      // Crew → ใช้ ParentBankID
    return null;                    // No-crew → ไม่มีทีม
}

// ===== Team table: load + render (REAL DATA) =====
async function loadTeamTableByParent(parentBankId) {
    const tbody = $id('teamTbody');
    if (!tbody) return;
    tbody.innerHTML = `<tr><td colspan="4" class="text-muted small">Loading team...</td></tr>`;
    try {
        const res = await fetch(baseUrl + 'UserBank/GetlistUserBankInTeam?ParentBankID=' + encodeURIComponent(parentBankId));
        const json = await res.json();
        if (!json?.success || !Array.isArray(json.data)) {
            tbody.innerHTML = `<tr><td colspan="4" class="text-muted small">No data.</td></tr>`;
            return;
        }
        renderTeamRows(json.data);
    } catch (e) {
        console.error(e);
        tbody.innerHTML = `<tr><td colspan="4" class="text-danger small">Load team failed.</td></tr>`;
    }
}

function renderTeamRows(list) {
    const tbody = $id('teamTbody');
    if (!tbody) return;

    if (!list?.length) {
        tbody.innerHTML = `<tr><td colspan="3" class="text-muted small">No members in this team.</td></tr>`;
        return;
    }

    tbody.innerHTML = list.map(x => `
    <tr data-id="${x.ID}">
      <td>
        <div class="fw-semibold">${x.FullName ?? '-'}</div>
        <div class="small">${x.Mobile ?? '-'}</div>
        <div class="small text-muted">${x.Email ?? '-'}</div>
      </td>
      <td>${x.AreaName ?? '-'}</td>
      <td class="text-end">
        <button type="button" class="btn btn-sm btn-outline-warning btn-leave-row">
          <i class="fa fa-user-minus me-1"></i> Leave
        </button>
      </td>
    </tr>
  `).join('');
}


// Debounce
const debounce = (fn, ms = 250) => { let t; return (...a) => { clearTimeout(t); t = setTimeout(() => fn(...a), ms); }; };

async function setTeamName(parentBankId, currentUserLike) {
    const box = document.getElementById('inpTeamName');
    if (!box) return;

    // no-crew
    if (!parentBankId) { box.value = 'ไม่มีทีม'; return; }

    const curId = Number(currentUserLike.id ?? currentUserLike.ID);
    const role = Number(currentUserLike.role ?? currentUserLike.Role);
    const isLead = !!(currentUserLike.isLeadBank ?? currentUserLike.IsLeadBank) || role === 1;

    // ถ้าเป็น Lead เอง → ใช้ข้อมูลตัวเอง
    if (isLead && curId === Number(parentBankId)) {
        const bankName = currentUserLike.bankName ?? currentUserLike.BankName ?? '';
        const first = currentUserLike.firstName ?? currentUserLike.FirstName ?? '';
        const last = currentUserLike.lastName ?? currentUserLike.LastName ?? '';
        box.value = `${bankName} - ${[first, last].filter(Boolean).join(' ')}`.trim();
        return;
    }

    // ถ้าเป็น Crew → ไปดึงข้อมูล Lead ตาม parentBankId
    try {
        const res = await fetch(baseUrl + 'UserBank/GetUserBankById?id=' + encodeURIComponent(parentBankId));
        const json = await res.json();
        if (!json?.success || !json.data) { box.value = 'ไม่มีทีม'; return; }

        const lead = json.data;
        const bankName = lead.bankName ?? lead.BankName ?? '';
        const first = lead.firstName ?? lead.FirstName ?? '';
        const last = lead.lastName ?? lead.LastName ?? '';
        box.value = `${bankName} - ${[first, last].filter(Boolean).join(' ')}`.trim();
    } catch {
        box.value = 'ไม่มีทีม';
    }
}

async function loadAddMoreUserBankDropdown(bankId, leadTeamId) {
    const ddl = document.getElementById('ddl-add-more-user-bank');
    if (!ddl) return;

    // reset
    ddl.innerHTML = `<option value="">เลือกสมาชิกเข้าทีม</option>`;
    if (!bankId) return;

    // ถ้าไม่มี parent/lead ให้ส่ง 0 (model binder จะรับเป็น 0)
    const safeLeadId = leadTeamId ? Number(leadTeamId) : 0;

    try {
        const url = baseUrl + 'UserBank/GetlistUserBankInTeamForAdd'
            + '?BankID=' + encodeURIComponent(bankId)
            + '&LeadteamID=' + encodeURIComponent(safeLeadId);

        const res = await fetch(url);
        const json = await res.json();

        if (!Array.isArray(json) || !json.length) return;

        for (const u of json) {
            const opt = document.createElement('option');
            opt.value = u.ValueInt;
            opt.textContent = u.Text;
            ddl.appendChild(opt);
        }
    } catch (err) {
        console.error('loadAddMoreUserBankDropdown failed', err);
    }
}

// Team search state
let currentTeamQuery = "";

// Apply filter to team rows
function applyTeamSearchFilter(q = currentTeamQuery) {
    const tbody = document.getElementById('teamTbody');
    if (!tbody) return;

    const rows = Array.from(tbody.querySelectorAll('tr'));
    let visibleCount = 0;

    rows.forEach(tr => {
        // skip helper rows (like loading/no data)
        if (tr.dataset.id === undefined) return;
        const text = tr.innerText.toLowerCase();
        const show = !q || text.includes(q.toLowerCase());
        tr.style.display = show ? '' : 'none';
        if (show) visibleCount++;
    });

    // show "no results" row if needed
    let empty = tbody.querySelector('tr.__noresult');
    if (!visibleCount) {
        if (!empty) {
            empty = document.createElement('tr');
            empty.className = '__noresult';
            empty.innerHTML = `<td colspan="4" class="text-muted small">No matched members.</td>`;
            tbody.appendChild(empty);
        }
    } else {
        empty?.remove();
    }
}

document.getElementById('inpSearchTeam')?.addEventListener(
    'input',
    debounce((e) => {
        currentTeamQuery = (e.target.value || '').trim().toLowerCase();
        applyTeamSearchFilter(currentTeamQuery);
    }, 250)
);

applyTeamSearchFilter();  // reapply currentTeamQuery to new rows

// ล้างค่าฟอร์ม + selections + validation + รหัสผ่าน
function resetAddUserBankForm() {
    const form = document.getElementById('formAddUserBank');
    if (!form) return;

    const areaRadios = document.querySelectorAll('input[name="addArea"]');
    areaRadios.forEach(r => r.checked = (r.value === '460'));

    // ล้าง input ธรรมดา
    form.classList.remove('was-validated');
    document.getElementById('addIsLeadBank').checked = false;
    document.getElementById('addFirstName').value = '';
    document.getElementById('addLastName').value = '';
    document.getElementById('addMobile').value = '';
    document.getElementById('addEmail').value = '';
    document.getElementById('addUsername').value = '';
    document.getElementById('addPassword').value = '';

    // รีเซ็ต password field เป็น type=password และปุ่มเป็น 'show'
    const pw = document.getElementById('addPassword');
    if (pw) pw.setAttribute('type', 'password');
    const btnPw = document.getElementById('btnToggleAddPw');
    if (btnPw) btnPw.textContent = 'show';

    // ล้าง selections ของ Choices (แต่คง options ไว้)
    try { choicesAddBank?.removeActiveItems(); } catch { }
    try { choicesAddProjects?.removeActiveItems(); } catch { }
}

// ----- Choices instances for Add modal -----
let choicesAddBank, choicesAddProjects;

// เปิด modal (ถ้าอยากเปิดจากปุ่มอื่น ให้เรียก openAddUserBankModal();)
function openAddUserBankModal() {
    resetAddUserBankForm();
    const m = new bootstrap.Modal(document.getElementById('modalAddUserBank'));
    m.show();
}

// Init Choices เมื่อเปิด modal ครั้งแรก
document.getElementById('modalAddUserBank')?.addEventListener('shown.bs.modal', async () => {
    // สร้าง choices ถ้ายังไม่มี
    if (typeof Choices === 'undefined') {
        console.warn('Choices.js not loaded.');
        return;
    }
    if (!choicesAddBank) {
        choicesAddBank = new Choices('#ddlAddBank', { searchEnabled: true, shouldSort: false, placeholderValue: 'Select bank' });
    }
    if (!choicesAddProjects) {
        choicesAddProjects = new Choices('#ddlAddProjects', { removeItemButton: true, searchEnabled: true, shouldSort: false, placeholderValue: 'Select projects' });
    }

    // โหลด DDLs
    await Promise.all([populateAddBanks(), populateAddProjects()]);
});

// เติม Bank
async function populateAddBanks() {
    try {
        const res = await fetch(baseUrl + 'UserBank/GetListBank');
        const json = await res.json(); // [{ValueInt, Text}, ...]
        const options = (json || []).map(x => ({ value: String(x.ValueInt), label: x.Text }));
        choicesAddBank?.clearStore();
        choicesAddBank?.setChoices(options, 'value', 'label', true);
    } catch (e) { console.error(e); }
}

// เติม Projects
async function populateAddProjects() {
    try {
        const res = await fetch(baseUrl + 'UserBank/GetListProject');
        const json = await res.json(); // [{ValueString/ValueInt/Value, Text}, ...]
        const options = (json || []).map(x => ({ value: String(x.ValueString ?? x.ValueInt ?? x.Value), label: x.Text }));
        choicesAddProjects?.clearStore();
        choicesAddProjects?.setChoices(options, 'value', 'label', true);
    } catch (e) { console.error(e); }
}

// Toggle password
document.getElementById('btnToggleAddPw')?.addEventListener('click', () => {
    const inp = document.getElementById('addPassword');
    if (!inp) return;
    const show = inp.getAttribute('type') === 'password';
    inp.setAttribute('type', show ? 'text' : 'password');
    document.getElementById('btnToggleAddPw').textContent = show ? 'hide' : 'show';
});

// Save (รวบรวมค่า + validate ฝั่ง client; ต่อ API insert ภายหลัง)
document.getElementById('btnSaveNewUserBank')?.addEventListener('click', async () => {
    showLoading();
    const form = document.getElementById('formAddUserBank');
    if (!form.checkValidity()) {
        form.classList.add('was-validated');
        return;
    }
    // ใหม่ (checkbox หลายตัว → CSV string เช่น "461,463,464")
    const areaIds = Array.from(document.querySelectorAll('input[name="addArea"]:checked'))
        .map(el => String(el.value));

    const payload = {
        IsLeadBank: document.getElementById('addIsLeadBank').checked,
        BankID: choicesAddBank?.getValue(true) ? Number(choicesAddBank.getValue(true)) : null,
        FirstName: document.getElementById('addFirstName').value.trim(),
        LastName: document.getElementById('addLastName').value.trim(),
        Mobile: document.getElementById('addMobile').value.trim(),
        Email: document.getElementById('addEmail').value.trim(),
        UserName: document.getElementById('addUsername').value.trim(),
        Password: document.getElementById('addPassword').value,
        AreaID: areaIds.length ? areaIds.join(',') : '',
        ProjectUserBank: (choicesAddProjects?.getValue(true) || []).map(pid => ({ ProjectID: String(pid) }))
    };


    try {
        const res = await fetch(baseUrl + 'UserBank/InsertUserBank', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });
        const json = await res.json();

        if (json?.success) {
            // ปิด modal + รีเฟรชหน้า list
            bootstrap.Modal.getInstance(document.getElementById('modalAddUserBank'))?.hide();
            successMessage('บันทึกสำเร็จ');  
            //แก้ฟีลเตอร์ไม่หายเเล้ว
            await doSearchUserBank(); // reload list/cards
        } else {
            hideLoading()
            successMessage('บันทึกล้มเหลว');

        }
    } catch (e) {
        console.error(e);
        successMessage('บันทึกล้มเหลว');
    }
});


async function addCrewFromDropdown() {
    const btn = document.getElementById('crewDropdown');
    const ddl = document.getElementById('ddl-add-more-user-bank');
    if (!ddl || !btn) return;

    const userId = Number(ddl.value || 0);                 // selected crew
    const leadId = Number(window.currentLeadTeamId || 0);  // current team lead
    const bankId = Number(window.currentBankIdForAdd || 0);

    // ---- Validate with SweetAlert2 ----
    if (!leadId) {
        if (typeof Swal !== 'undefined') {
            await Swal.fire({ icon: 'error', title: 'ไม่พบหัวหน้าทีม', text: 'LeadteamID หายไป', confirmButtonText: 'ตกลง' });
        } else {
            alert('ไม่พบหัวหน้าทีม (LeadteamID).');
        }
        return;
    }
    if (!userId) {
        if (typeof Swal !== 'undefined') {
            await Swal.fire({ icon: 'warning', title: 'ยังไม่ได้เลือกสมาชิก', text: 'กรุณาเลือกสมาชิกเข้าทีมก่อน', confirmButtonText: 'ตกลง' });
        } else {
            alert('กรุณาเลือกสมาชิกเข้าทีมก่อน');
        }
        return;
    }

    // ---- Confirm ----
    let confirmed = true;
    if (typeof Swal !== 'undefined') {
        const res = await Swal.fire({
            title: 'เพิ่มสมาชิกเข้าทีม?',
            text: 'ยืนยันการเพิ่มสมาชิกคนนี้เข้าทีมของหัวหน้าปัจจุบัน',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'เพิ่ม',
            cancelButtonText: 'ยกเลิก',
            reverseButtons: true,
            focusCancel: true
        });
        confirmed = res.isConfirmed;
    }
    if (!confirmed) return;

    // UI: disable while posting
    const prevHTML = btn.innerHTML;
    btn.disabled = true;
    btn.innerHTML = `<span class="spinner-border spinner-border-sm me-2"></span>Adding...`;

    try {
        const body = new URLSearchParams({ UserBankID: String(userId), LeadteamID: String(leadId) });

        // NOTE: ถ้าพ่อใหญ่ใช้ action ชื่ออื่น (เช่น InsertNewEventsAndShops) ก็เปลี่ยน URL ตรงนี้
        const res = await fetch(baseUrl + 'UserBank/MoveUserbankToTeam', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },
            body
        });
        const json = await res.json();

        if (json?.success) {
            // refresh team table
            await loadTeamTableByParent(leadId);

            // refresh dropdown (remove the just-added user from candidates)
            if (bankId) {
                await loadAddMoreUserBankDropdown(bankId, leadId);
            }

            // reset selection
            ddl.value = '';

            // success toast
            if (typeof Swal !== 'undefined') {
                await Swal.fire({ icon: 'success', title: 'เพิ่มสมาชิกสำเร็จ', timer: 1200, showConfirmButton: false });
            }
        } else {
            if (typeof Swal !== 'undefined') {
                await Swal.fire({ icon: 'error', title: 'ไม่สำเร็จ', text: 'เพิ่มสมาชิกเข้าทีมไม่สำเร็จ' });
            } else {
                alert('เพิ่มสมาชิกเข้าทีมไม่สำเร็จ');
            }
        }
    } catch (e) {
        console.error(e);
        if (typeof Swal !== 'undefined') {
            await Swal.fire({ icon: 'error', title: 'เกิดข้อผิดพลาด', text: 'โปรดลองอีกครั้ง' });
        } else {
            alert('เกิดข้อผิดพลาดขณะเพิ่มสมาชิก');
        }
    } finally {
        btn.disabled = false;
        btn.innerHTML = prevHTML;
    }
}


// wire the button
document.getElementById('crewDropdown')?.addEventListener('click', addCrewFromDropdown);

// Leave (remove from team) handler with SweetAlert2 confirm
document.getElementById('teamTbody')?.addEventListener('click', async (e) => {
    const btn = e.target.closest('.btn-leave-row');
    if (!btn) return;

    const tr = btn.closest('tr');
    const userId = Number(tr?.dataset.id || 0);
    const leadId = Number(window.currentLeadTeamId || 0);
    const bankId = Number(window.currentBankIdForAdd || 0);

    if (!userId) { alert('ไม่พบรหัสสมาชิก'); return; }
    if (!leadId) { alert('ไม่พบหัวหน้าทีม (LeadteamID)'); return; }

    // --- SweetAlert2 confirm ---
    let confirmed = true;
    if (typeof Swal !== 'undefined') {
        const res = await Swal.fire({
            title: 'นำสมาชิกออกจากทีม?',
            text: 'สมาชิกคนนี้จะถูกถอดออกจากทีมของหัวหน้าปัจจุบัน',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'ใช่, นำออก',
            cancelButtonText: 'ยกเลิก',
            reverseButtons: true,
            focusCancel: true
        });
        confirmed = res.isConfirmed;
    } else {
        confirmed = confirm('ยืนยันนำสมาชิกคนนี้ออกจากทีม?');
    }
    if (!confirmed) return;

    const prevHTML = btn.innerHTML;
    btn.disabled = true;
    btn.innerHTML = `<span class="spinner-border spinner-border-sm me-2"></span>Processing...`;

    try {
        const body = new URLSearchParams({ UserBankID: String(userId), LeadteamID: String(leadId) });
        const res = await fetch(baseUrl + 'UserBank/LeavUserbankFromTeam', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },
            body
        });
        const json = await res.json();

        if (json?.success) {
            await loadTeamTableByParent(leadId);
            if (bankId) await loadAddMoreUserBankDropdown(bankId, leadId);

            if (typeof Swal !== 'undefined') {
                await Swal.fire({ icon: 'success', title: 'นำออกแล้ว', timer: 1200, showConfirmButton: false });
            }
        } else {
            if (typeof Swal !== 'undefined') {
                await Swal.fire({ icon: 'error', title: 'ไม่สำเร็จ', text: 'นำสมาชิกออกจากทีมไม่สำเร็จ' });
            } else {
                alert('นำสมาชิกออกจากทีมไม่สำเร็จ');
            }
        }
    } catch (err) {
        console.error(err);
        if (typeof Swal !== 'undefined') {
            await Swal.fire({ icon: 'error', title: 'เกิดข้อผิดพลาด', text: 'โปรดลองอีกครั้ง' });
        } else {
            alert('เกิดข้อผิดพลาดขณะนำสมาชิกออกจากทีม');
        }
    } finally {
        btn.disabled = false;
        btn.innerHTML = prevHTML;
    }
});



async function saveCurrentUserBank() {
    const btn = document.getElementById('btnSaveUserBank');
    if (!btn) return;

    // อ่านค่า
    const id = Number(window.currentEditUserId || 0);
    const isLead = document.getElementById('chkIsLead').checked;
    const firstName = document.getElementById('inpFirstName').value.trim();
    const lastName = document.getElementById('inpLastName').value.trim();
    const mobile = document.getElementById('inpMobile').value.trim();
    const email = document.getElementById('inpEmail').value.trim();
    const userName = document.getElementById('inpUsername').value.trim();
    const password = document.getElementById('inpPassword').value;  // ถ้าไม่เปลี่ยนจะเป็นค่าเดิมที่โหลดมา
    const bankIdRaw = (choicesBank ? choicesBank.getValue(true) : null);
    const bankId = bankIdRaw ? Number(bankIdRaw) : null;

    const projectIds = (choicesProject ? choicesProject.getValue(true) : []) || [];
    const projectList = projectIds.map(pid => ({ ProjectID: String(pid) }));

    // ตรวจความครบถ้วน (จำเป็นขั้นต่ำ)
    const requiredMissing = [];
    if (!id) requiredMissing.push('ID');
    if (!firstName) requiredMissing.push('First Name');
    if (!lastName) requiredMissing.push('Last Name');
    if (!userName) requiredMissing.push('Username');
    // password: ให้ผ่านได้ เพราะฟิลด์ถูกเติมมาจากเซิร์ฟเวอร์แล้ว

    if (requiredMissing.length) {
        if (typeof Swal !== 'undefined') {
            await Swal.fire({
                icon: 'warning',
                title: 'ข้อมูลไม่ครบ',
                html: 'กรุณากรอก: <b>' + requiredMissing.join(', ') + '</b>',
                confirmButtonText: 'ตกลง'
            });
        } else {
            alert('กรุณากรอก: ' + requiredMissing.join(', '));
        }
        return;
    }

    const areaIds = Array.from(document.querySelectorAll('input[name="AreaIDs"]:checked')).map(el => String(el.value).trim()).filter(v => v);

    // payload ตามโมเดล UserBankEditModel
    const payload = {
        ID: id,
        IsLeadBank: isLead,
        BankID: bankId,
        FirstName: firstName,
        LastName: lastName,
        Mobile: mobile,
        Email: email,
        UserName: userName,
        Password: password,
        ProjectUserBank: projectList,
        AreaID: areaIds.length ? areaIds.join(',') : ''
    };

    // ยืนยันก่อนบันทึก
    let confirmed = true;
    if (typeof Swal !== 'undefined') {
        const res = await Swal.fire({
            title: 'บันทึกการแก้ไข?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'บันทึก',
            cancelButtonText: 'ยกเลิก',
            reverseButtons: true
        });
        confirmed = res.isConfirmed;
    }
    if (!confirmed) return;

    // UI ระหว่างบันทึก
    const prevHTML = btn.innerHTML; btn.disabled = true;
    btn.innerHTML = `<span class="spinner-border spinner-border-sm me-2"></span>Saving...`;

    try {
        console.log(payload);
        const res = await fetch(baseUrl + 'UserBank/UpdateUserBank', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        // 👇 add this
        if (!res.ok) {
            const text = await res.text().catch(() => '');
            throw new Error(`POST ${res.status} ${res.statusText} — ${text}`);
        }

        const json = await res.json();


        if (json?.success) {
            // รีโหลดข้อมูลรวม (การ์ด/ลิสต์/ฯลฯ)
            window.showLoading?.();
            await doSearchUserBank();

            // พยายามเลือกกลับมาที่ user เดิม
            const list = document.querySelector('#userBankList');
            const target = list?.querySelector(`.list-group-item[data-id="${id}"]`);
            target?.click();
            window.hideLoading?.();
            if (typeof Swal !== 'undefined') {
                await Swal.fire({ icon: 'success', title: 'บันทึกสำเร็จ', timer: 1200, showConfirmButton: false });
            }
        } else {
            if (typeof Swal !== 'undefined') {
                await Swal.fire({ icon: 'error', title: 'บันทึกไม่สำเร็จ', text: 'โปรดลองอีกครั้ง' });
            } else {
                alert('บันทึกไม่สำเร็จ');
            }
        }
    } catch (err) {
        console.error(err);
        if (typeof Swal !== 'undefined') {
            await Swal.fire({ icon: 'error', title: 'เกิดข้อผิดพลาด', text: 'โปรดลองอีกครั้ง' });
        } else {
            alert('เกิดข้อผิดพลาด');
        }
    } finally {
        btn.disabled = false;
        btn.innerHTML = prevHTML;
    }
}

// bind ปุ่ม Save
document.getElementById('btnSaveUserBank')?.addEventListener('click', saveCurrentUserBank);

document.getElementById('btnDeleteUserBank')?.addEventListener('click', async () => {
    const id = Number(window.currentEditUserId || 0);
    if (!id) {
        if (typeof Swal !== 'undefined') {
            await Swal.fire({ icon: 'warning', title: 'ไม่พบรหัสผู้ใช้', text: 'โปรดเลือกผู้ใช้ก่อน' });
        } else { alert('No user selected.'); }
        return;
    }

    // Confirm
    let confirmed = true;
    if (typeof Swal !== 'undefined') {
        const res = await Swal.fire({
            title: 'ลบผู้ใช้ธนาคารนี้?',
            text: 'ระบบจะปิดการใช้งาน (FlagActive = 0 ). ถ้าเป็นหัวหน้าทีม ลูกทีมทั้งหมดจะถูกถอดออกจากทีมด้วย',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'ยืนยันลบ',
            cancelButtonText: 'ยกเลิก',
            reverseButtons: true,
            focusCancel: true
        });
        confirmed = res.isConfirmed;
    } else {
        confirmed = confirm('Confirm delete this user?');
    }
    if (!confirmed) return;

    const btn = document.getElementById('btnDeleteUserBank');
    const prevHTML = btn.innerHTML;
    btn.disabled = true;
    btn.innerHTML = `<span class="spinner-border spinner-border-sm me-2"></span>Deleting...`;

    try {
        const body = new URLSearchParams({ id: String(id) });
        const res = await fetch(baseUrl + 'UserBank/DeleteUserBank', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },
            body
        });

        if (!res.ok) {
            const text = await res.text().catch(() => '');
            throw new Error(`POST ${res.status} ${res.statusText} — ${text}`);
        }
        const json = await res.json();

        if (json?.success) {
            if (typeof Swal !== 'undefined') {
                await Swal.fire({ icon: 'success', title: 'ลบสำเร็จ', timer: 1200, showConfirmButton: false });
            }
            // reload left list + cards

            await doSearchUserBank();

            // Clear right panel fields (optional)
            document.getElementById('editUserBankFullName').textContent = '';
            document.getElementById('editUserBankBankName').textContent = '';
            document.getElementById('editUserBankLogo').src = baseUrl + 'image/ThaiBankicon/DEFAULT.png';

            // reset current id
            window.currentEditUserId = 0;
        } else {
            if (typeof Swal !== 'undefined') {
                await Swal.fire({ icon: 'error', title: 'ลบไม่สำเร็จ', text: 'โปรดลองอีกครั้ง' });
            } else { alert('Delete failed'); }
        }
    } catch (err) {
        console.error(err);
        if (typeof Swal !== 'undefined') {
            await Swal.fire({ icon: 'error', title: 'เกิดข้อผิดพลาด', text: 'โปรดลองอีกครั้ง' });
        } else { alert('Error while deleting'); }
    } finally {
        btn.disabled = false;
        btn.innerHTML = prevHTML;
    }
});


// Helper to read selected bank ids (strings)
function getSelectedBankIds() {
    if (window.choicesFilterBanks) return choicesFilterBanks.getValue(true); // ['1','2']
    const ddl = $id('ddl_bank');
    if (!ddl) return [];
    return Array.from(ddl.selectedOptions).map(o => o.value).filter(Boolean);
}

// Call API and re-render list
async function doSearchUserBank() {
    try {
        window.showLoading?.();

        const q = ($id('searchPerson')?.value || '').trim();
        const bankIdsCsv = getSelectedBankIds().join(',');

        const url = baseUrl + 'UserBank/SearchUserBank?' + new URLSearchParams({
            BankIDs: bankIdsCsv,
            TextSearch: q
        });

        const res = await fetch(url, { method: 'GET' });
        if (!res.ok) {
            const text = await res.text().catch(() => '');
            throw new Error(`GET ${res.status} ${res.statusText} — ${text}`);
        }

        const json = await res.json();
        if (json?.success) {
            const list = json.listUserBank || [];
            renderUserBankList(list);

            // Optional: if no result, clear right side
            if (!list.length) {
                $id('editUserBankFullName') && ($id('editUserBankFullName').textContent = '');
                $id('editUserBankBankName') && ($id('editUserBankBankName').textContent = '');
                $id('teamTbody') && ($id('teamTbody').innerHTML = `<tr><td colspan="4" class="text-muted small">No members in this team.</td></tr>`);
            }
        } else {
            // fallback: clear list
            renderUserBankList([]);
        }
    } catch (err) {
        console.error('SearchUserBank failed', err);
    } finally {
        window.hideLoading?.();
    }
}

// Click on the button
$id('btnSearchUser')?.addEventListener('click', doSearchUserBank);

// Press Enter in the search box
$id('searchPerson')?.addEventListener('keydown', (e) => {
    if (e.key === 'Enter') {
        e.preventDefault();
        doSearchUserBank();
    }
});

// Optional: trigger search when banks change (multi-select)
$id('ddl_bank')?.addEventListener('change', () => {
    // auto-search on filter change (or keep manual — your call)
    // doSearchUserBank();
});
