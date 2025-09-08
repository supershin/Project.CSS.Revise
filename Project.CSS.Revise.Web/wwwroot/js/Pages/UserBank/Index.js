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
    $id('ddl_bank')?.addEventListener('change', () => {
        // TODO: add filter logic if needed
    });

    // Select-all ในตารางทีม
    $id('chkAllRows')?.addEventListener('change', (e) => {
        document.querySelectorAll('#teamTbody .team-check').forEach(cb => cb.checked = e.target.checked);
    });

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

    // เคลียร์ค่าเดิม (เว้น option แรกไว้)
    ddl.querySelectorAll('option:not([value=""])').forEach(o => o.remove());

    (listBank || []).forEach(b => {
        const opt = document.createElement('option');
        opt.value = b.ValueInt;   // <-- ValueInt
        opt.textContent = b.Text; // <-- Text (BankName)
        ddl.appendChild(opt);
    });
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
        const res = await fetch(baseUrl + 'UserBank/GetUserBankById?id=' + encodeURIComponent(userId));
        const json = await res.json();
        if (!json?.success || !json.data) return;

        const u = json.data;

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
        $id('chkIsLead').checked = !!(u.isLeadBank ?? u.IsLeadBank);

        const bankId = String(u.bankID ?? u.BankID ?? '');
        if (choicesBank) {
            bankId ? choicesBank.setChoiceByValue(bankId) : choicesBank.removeActiveItems();
        }


        $id('inpFirstName').value = first;
        $id('inpLastName').value = last;
        $id('inpMobile').value = u.mobile ?? u.Mobile ?? '';
        $id('inpEmail').value = u.email ?? u.Email ?? '';
        $id('inpUsername').value = u.userName ?? u.UserName ?? '';
        $id('inpPassword').value = u.password ?? u.Password ?? '';

        const projValues = (u.projectUserBank ?? u.ProjectUserBank ?? [])
            .map(p => String(p.projectID ?? p.ProjectID))
            .filter(Boolean);
        if (choicesProject) {
            choicesProject.removeActiveItems();
            if (projValues.length) choicesProject.setChoiceByValue(projValues);
        }

        // ===== หลัง populate ฟอร์มเสร็จ → โหลดทีมตามบทบาท =====
        try {
            const fromApi = {
                id: u.id ?? u.ID,
                role: u.role,  // อาจไม่มีเมื่อมาจาก /GetUserBankById
                isLeadBank: u.isLeadBank ?? u.IsLeadBank,
                parentBankID: u.parentBankID ?? u.ParentBankID
            };

            let parentId = getParentBankIdForTeam(fromApi);

            // เผื่อกรณี role ไม่มากับ /GetUserBankById → ใช้ active item ทางซ้ายช่วย
            if (!parentId) {
                const active = document.querySelector('#userBankList .list-group-item.active');
                if (active) {
                    const roleAttr = Number(active.dataset.role || NaN);
                    const idAttr = Number(active.dataset.id || NaN);
                    if (roleAttr === 1 && idAttr) parentId = idAttr;
                }
            }

            if (parentId) {
                await loadTeamTableByParent(parentId);
                await setTeamName(parentId, u);      // ★ ตั้งชื่อทีม
                if (bankId) loadAddMoreUserBankDropdown(bankId, parentId);
            } else {
                renderTeamRows([]); // no-crew
                await setTeamName(null, u);  
                if (bankId) loadAddMoreUserBankDropdown(bankId, 0); 
            }
        } catch (e) {
            console.error(e);
            renderTeamRows([]);
        }

    } catch (e) {
        console.error(e);
    } finally {
        window.hideLoading?.();
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
        tbody.innerHTML = `<tr><td colspan="4" class="text-muted small">No members in this team.</td></tr>`;
        return;
    }

    tbody.innerHTML = list.map(x => `
    <tr data-id="${x.ID}">
      <td><input class="form-check-input team-check" type="checkbox"></td>
      <td>
        <div class="fw-semibold">${x.FullName ?? '-'}</div>
        <div class="small">${x.Mobile ?? '-'}</div>
        <div class="small text-muted">${x.Email ?? '-'}</div>
      </td>
      <td>${x.AreaName ?? '-'}</td>
      <td class="text-end">
        <button type="button" class="btn btn-sm btn-outline-danger btn-remove-row">
          <i class="fa fa-trash me-1"></i> Remove
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
    const form = document.getElementById('formAddUserBank');
    if (!form.checkValidity()) {
        form.classList.add('was-validated');
        return;
    }

    const payload = {
        IsLeadBank: document.getElementById('addIsLeadBank').checked,
        BankID: choicesAddBank?.getValue(true) ? Number(choicesAddBank.getValue(true)) : null,
        FirstName: document.getElementById('addFirstName').value.trim(),
        LastName: document.getElementById('addLastName').value.trim(),
        Mobile: document.getElementById('addMobile').value.trim(),
        Email: document.getElementById('addEmail').value.trim(),
        UserName: document.getElementById('addUsername').value.trim(),
        Password: document.getElementById('addPassword').value,
        AreaID: Number(document.querySelector('input[name="addArea"]:checked')?.value || 460), // ★
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
            await loadUserBankPage(); // reload list/cards
        } else {
            alert('Insert failed');
        }
    } catch (e) {
        console.error(e);
        alert('Insert error');
    }
});


