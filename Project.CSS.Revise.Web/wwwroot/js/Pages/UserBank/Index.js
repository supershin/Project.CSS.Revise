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


// map ไอคอนธนาคารจาก BankCode (แก้ path ได้)
const bankIconPath = (code) => baseUrl + `image/ThaiBankicon/${(code || '').toUpperCase()}.png`;

// ===== Normalize key =====
const normalizeUserBank = (o = {}) => ({
    id: o.ID ?? o.id ?? null,
    fullName: o.FullName ?? o.fullName ?? '',
    bankCode: o.BankCode ?? o.bankCode ?? '',
    bankName: o.BankName ?? o.bankName ?? '',
    role: o.Role ?? o.role ?? null
});

// ===== Page init =====
document.addEventListener('DOMContentLoaded', () => {
    loadUserBankPage();

    // เมื่อเปลี่ยนธนาคารใน dropdown — เดี๋ยวค่อย bind การกรอง list ด้านซ้ายทีหลัง
    const ddl = $id('ddl_bank');
    if (ddl) {
        ddl.addEventListener('change', () => {
            // TODO: กรองรายชื่อซ้าย/ขวาด้วย bank ที่เลือก (ถ้าต้องการ)
            // reloadUserList({ bankId: ddl.value });
        });
    }
});

// ===== Call /UserBank/Page_Load =====
async function loadUserBankPage() {
    try {
        showLoading();

        const res = await fetch(baseUrl + 'UserBank/Page_Load', { method: 'GET' });
        const json = await res.json();

        if (!json?.success) {
            hideLoading();
            return;
        }

        // 1) เติม dropdown ธนาคาร (listBank: ValueInt, Text)
        renderBankDropdown(json.listBank || []);

        // 2) วาดการ์ด CountUserByBank (listCountUserByBankk: CntUserByBank, BankCode, BankName)
        renderCountUserByBankCards(json.listCountUserByBankk || []);

        // 3) วาดรายชื่อผู้ใช้ (listUserBank: FullName, BankName, BankCode, Role)
        renderUserBankList(json.listUserBank || []);

        hideLoading();
    } catch (err) {
        console.error(err);
        hideLoading();
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
        opt.value = b.ValueInt;      // <-- ValueInt
        opt.textContent = b.Text;    // <-- Text (BankName)
        ddl.appendChild(opt);
    });
}

// ===== Render CountUserByBank cards (กล่องเล็กลงตามที่ขอ) =====
function renderCountUserByBankCards(items) {
    const host = $id('bankCountCards');
    if (!host) return;
    host.innerHTML = '';

    (items || []).forEach(x => {
        const col = el('div', 'col-6 col-sm-4 col-lg-2 mb-2');

        const card = el('div', 'card shadow-sm h-100');
        card.style.borderRadius = "10px";

        const body = el('div', 'card-body d-flex align-items-center gap-2 p-2');

        const iconBox = el(
            'div',
            'd-flex justify-content-center align-items-center rounded',
            `<img src="${bankIconPath(x.BankCode)}" alt="${x.BankCode}" 
            style="width:28px;height:28px;object-fit:contain;">`
        );
        iconBox.style.width = '36px';
        iconBox.style.height = '36px';
        iconBox.style.background = 'rgba(0,0,0,.04)';

        const textBox = el('div', 'flex-grow-1');
        textBox.innerHTML = `
      <div class="fw-semibold" style="font-size: 0.85rem; line-height:1.2;">
        ${x.BankName || x.BankCode || '-'}
      </div>
      <div class="text-muted small" style="font-size: 0.75rem;">
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
    const container = document.getElementById("userBankList");
    if (!container) return;

    container.innerHTML = "";

    (data || []).map(normalizeUserBank).forEach((u, idx) => {
        const item = document.createElement("a");
        item.className =
            "list-group-item list-group-item-action list-hover-primary nav-link d-flex align-items-start gap-3 p-2 mb-2 position-relative";
        if (idx === 0) item.classList.add("active");
        item.style.borderRadius = "8px";
        item.style.transition = "background-color 0.2s";
        item.href = "#v-pills-user";
        item.setAttribute("role", "tab");

        // เก็บ id user เอาไว้
        item.dataset.id = u.id;

        item.innerHTML = `
      ${u.role === 1 ? `<span class="ribbon-badge">LEAD</span>` : ``}
      <div class="icon-container d-flex justify-content-center align-items-center"
           style="width: 36px; height: 36px; padding: 2px;">
        <img src="${bankIconPath(u.bankCode)}" alt="${u.bankCode}"
             style="width: 100%; height: 100%; object-fit: contain;">
      </div>
      <div class="flex-grow-1">
        <div class="text-dark fw-semibold">${u.fullName || '-'}</div>
        <div class="text-muted small">
          <span class="text-primary">${u.bankName || u.bankCode || '-'}</span>
        </div>
      </div>
    `;

        // click → โหลดข้อมูลแก้ไข
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

    // auto load item แรก (ถ้าต้องการ และมีรายการ)
    const first = container.querySelector('.list-group-item');
    if (first) first.click();
}

function ensureChoices() {
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
        showLoading();
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
        const logo = document.getElementById('editUserBankLogo');
        const nameEl = document.getElementById('editUserBankFullName');
        const bankEl = document.getElementById('editUserBankBankName');

        const bankCode = u.bankCode ?? u.BankCode ?? '';
        const first = u.firstName ?? u.FirstName ?? '';
        const last = u.lastName ?? u.LastName ?? '';
        const bankName = u.bankName ?? u.BankName ?? '';

        if (logo) {
            const src = bankIconPath(bankCode);
            logo.src = src;

            // กันรูปหาย: ถ้าพาธผิด ให้ใช้รูป fallback
            logo.onerror = () => {
                logo.onerror = null;
                logo.src = new URL('images/ThaiBankicon/DEFAULT.png', ROOT).href; // ใส่ไฟล์สำรองของคุณ
                console.warn('Bank logo not found:', src);
            };
            console.log(src);
        }
        if (nameEl) nameEl.textContent = `${first} ${last}`.trim();
        if (bankEl) bankEl.textContent = bankName;

        // ---- ฟอร์มหลัก ----
        $id('chkIsLead').checked = !!(u.isLeadBank ?? u.IsLeadBank);

        const bankId = String(u.bankID ?? u.BankID ?? '');
        bankId ? choicesBank.setChoiceByValue(bankId) : choicesBank.removeActiveItems();

        $id('inpFirstName').value = first;
        $id('inpLastName').value = last;
        $id('inpMobile').value = u.mobile ?? u.Mobile ?? '';
        $id('inpEmail').value = u.email ?? u.Email ?? '';
        $id('inpUsername').value = u.userName ?? u.UserName ?? '';
        $id('inpPassword').value = u.password ?? u.Password ?? '';

        const projValues = (u.projectUserBank ?? u.ProjectUserBank ?? [])
            .map(p => String(p.projectID ?? p.ProjectID))
            .filter(Boolean);
        choicesProject.removeActiveItems();
        if (projValues.length) choicesProject.setChoiceByValue(projValues);

    } catch (e) {
        console.error(e);
    } finally {
        hideLoading();
    }
}

