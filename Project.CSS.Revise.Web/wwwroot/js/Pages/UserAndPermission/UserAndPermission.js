// --- Choices init ---
let deptChoices, roleChoices;

document.addEventListener('DOMContentLoaded', () => {
    deptChoices = new Choices('#ddlDepartment', {
        removeItemButton: true,
        allowSearch: true,
        shouldSort: false,
        placeholderValue: '— เลือกแผนก —',
        searchPlaceholderValue: 'พิมพ์ค้นหาแผนก…'
    });

    roleChoices = new Choices('#ddlRole', {
        removeItemButton: true,
        allowSearch: true,
        shouldSort: false,
        placeholderValue: '— เลือกบทบาท —',
        searchPlaceholderValue: 'พิมพ์ค้นหาบทบาท…'
    });

    ddlDepartmentEditChoices = new Choices('#ddl_Department_edit', {
        shouldSort: false,
        searchEnabled: true,
        removeItemButton: false, // single select
        allowHTML: false,
        itemSelectText: '',
        placeholder: true,
        placeholderValue: '— เลือกแผนก —'
    });

    ddlBuChoices = new Choices('#ddl_Bu_edit', {
        shouldSort: false,
        searchEnabled: true,
        removeItemButton: false, // single select
        allowHTML: false,
        itemSelectText: '',
        placeholder: true,
        placeholderValue: '— เลือก BU —',
    });

    onSearchUsers();

    document.getElementById('btnSearchUser')?.addEventListener('click', onSearchUsers);
});

// --- helper: join values เป็น CSV ---
function getChoicesCsv(choicesInstance) {
    try {
        const vals = choicesInstance?.getValue(true) || []; // true -> value only
        return (Array.isArray(vals) ? vals : [vals]).filter(Boolean).join(',');
    } catch { return ''; }
}

// --- call backend ---
async function onSearchUsers() {
    showLoading();
    const name = (document.getElementById('txtSearchName')?.value || '').trim();
    const depCsv = getChoicesCsv(deptChoices);
    const roleCsv = getChoicesCsv(roleChoices);

    const form = new FormData();
    form.append('L_Name', name);
    form.append('L_DepartmentID', depCsv);
    form.append('L_RoleID', roleCsv);

    const res = await fetch(baseUrl + 'UserAndPermission/GetlistUser', { method: 'POST', body: form });
    const json = await res.json();

   /* console.log('users:', json?.data);*/
    renderUserList(json?.data || []);
    hideLoading()
    // TODO: render ตาราง (DataTables/HTML ตามที่พ่อใหญ่ชอบ)
}

function renderUserList(data) {
    const container = document.getElementById('userList');
    if (!container) return;

    // เคลียร์เดิม
    container.innerHTML = '';

    // ถ้าไม่มีข้อมูล
    if (!Array.isArray(data) || data.length === 0) {
        container.innerHTML = `
            <div class="text-center text-muted py-4">
                <i class="fa fa-user-circle fa-2x d-block mb-2"></i>
                ไม่พบผู้ใช้งานตามเงื่อนไขที่ค้นหา
            </div>`;
        return;
    }

    // helper: กัน XSS
    const esc = (s) => (s ?? '').toString()
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');

    // สร้างรายการ
    data.forEach((u, idx) => {
        const thaiName = esc(u.FullnameTH || '');
        const engName = esc(u.FullnameEN || '');
        const deptName = esc(u.DepartmentName || 'ยังไม่ได้ระบุแผนก');

        const a = document.createElement('a');
        a.className = `list-group-item list-group-item-action list-hover-primary contact-tab-${idx} nav-link d-flex align-items-start gap-3 p-2 mb-2`;
        a.href = '#v-pills-user';
        a.role = 'tab';
        a.style.borderRadius = '8px';
        a.style.transition = 'background-color 0.2s';
        a.dataset.index = String(idx);
        a.setAttribute('aria-selected', 'false');

        // ทำให้ item แรก active
        if (idx === 0) {
            a.classList.add('active');
            a.setAttribute('aria-selected', 'true');
        }

        a.innerHTML = `
            <!-- Icon / Avatar -->
            <div class="icon-container d-flex justify-content-center align-items-center bg-light rounded-circle"
                 style="width:36px;height:36px;">
                <svg width="20" height="20" fill="currentColor" class="text-primary">
                    <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"></path>
                    <path fill-rule="evenodd" d="M8 9a5 5 0 0 0-5 5v.5h10V14a5 5 0 0 0-5-5z"></path>
                </svg>
            </div>
            <!-- Text Content -->
            <div class="flex-grow-1">
                <div class="text-dark fw-semibold text-truncate">${thaiName}</div>
                <div class="text-muted small text-truncate">
                    <span>${engName}</span> —
                    <span class="text-primary">${deptName}</span>
                </div>
            </div>
        `;

        // คลิกเพื่อ active และเรียกโหลดรายละเอียด (ถ้ามี)
        a.addEventListener('click', (e) => {
            e.preventDefault();
            setActiveUserItem(idx);
            // TODO: loadUserDetail(u.ID) // ถ้าต้องโหลดรายละเอียดฝั่งขวา
        });

        container.appendChild(a);
    });
}

// ตั้ง active ให้ item ที่คลิก
function setActiveUserItem(activeIndex) {
    const container = document.getElementById('userList');
    if (!container) return;
    const items = container.querySelectorAll('.list-group-item');
    items.forEach((el, i) => {
        if (i === activeIndex) {
            el.classList.add('active');
            el.setAttribute('aria-selected', 'true');
        } else {
            el.classList.remove('active');
            el.setAttribute('aria-selected', 'false');
        }
    });
}

