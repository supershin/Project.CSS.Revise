// ==== เพิ่มด้านบนไฟล์ ====
let deptChoices, roleChoices;
//let ddlDepartmentEditChoices, ddlBuChoices, ddlRoleChoices;
let currentEditUserId = null;   // <-- เก็บ ID ของ user ที่กำลังแก้ไข
let ddlDepartmentCreateChoices, ddlRoleCreateChoices, ddlBuCreateChoices;

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
        removeItemButton: true,
        allowSearch: true,
        shouldSort: false,
        placeholderValue: '— เลือก BU —',
        searchPlaceholderValue: 'พิมพ์ค้นหา BU …'
    });

    ddlRoleChoices = new Choices('#ddl_Role_edit', {
        shouldSort: false,
        searchEnabled: true,
        removeItemButton: false, // single select
        allowHTML: false,
        itemSelectText: '',
        placeholder: true,
        placeholderValue: '— เลือก Role —',
    });


    ddlDepartmentCreateChoices = new Choices('#ddl_Department_create', {
        shouldSort: false, searchEnabled: true, itemSelectText: '', removeItemButton: false,
        placeholder: true, placeholderValue: '— เลือกแผนก —'
    });

    ddlRoleCreateChoices = new Choices('#ddl_Role_create', {
        shouldSort: false, searchEnabled: true, itemSelectText: '', removeItemButton: false,
        placeholder: true, placeholderValue: '— เลือก Role —'
    });

    ddlBuCreateChoices = new Choices('#ddl_Bu_create', {
        removeItemButton: true, allowSearch: true, shouldSort: false,
        placeholderValue: '— เลือก BU —', searchPlaceholderValue: 'พิมพ์ค้นหา BU …'
    });

    onSearchUsers();

    document.getElementById('btnSearchUser')?.addEventListener('click', onSearchUsers);

    document.getElementById('create_user_form')?.addEventListener('submit', onCreateUserSubmit);

    // ผูก submit ของฟอร์มแก้ไข
    const frm = document.getElementById('edit_user');
    if (frm) {
        frm.addEventListener('submit', onSaveEditUser);
    }

    // ปุ่ม Cancel (ล้างค่าแบบง่าย ๆ)
    const btnCancel = document.getElementById('cancel_edit_user');
    if (btnCancel) {
        btnCancel.addEventListener('click', () => {
            frm?.reset();
        });
    }
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
    container.innerHTML = '';

    if (!Array.isArray(data) || data.length === 0) {
        container.innerHTML = `
            <div class="text-center text-muted py-4">
                <i class="fa fa-user-circle fa-2x d-block mb-2"></i>
                ไม่พบผู้ใช้งานตามเงื่อนไขที่ค้นหา
            </div>`;
        return;
    }

    const esc = (s) => (s ?? '').toString()
        .replace(/&/g, '&amp;').replace(/</g, '&lt;')
        .replace(/>/g, '&gt;').replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');

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
        a.dataset.userid = String(u.ID);            // ✅ เก็บ user id ไว้
        a.setAttribute('aria-selected', 'false');

        if (idx === 0) {
            a.classList.add('active');
            a.setAttribute('aria-selected', 'true');
        }

        a.innerHTML = `
            <div class="icon-container d-flex justify-content-center align-items-center bg-light rounded-circle"
                 style="width:36px;height:36px;">
                <svg width="20" height="20" fill="currentColor" class="text-primary">
                    <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"></path>
                    <path fill-rule="evenodd" d="M8 9a5 5 0 0 0-5 5v.5h10V14a5 5 0 0 0-5-5z"></path>
                </svg>
            </div>
            <div class="flex-grow-1">
                <div class="text-dark fw-semibold text-truncate">${thaiName}</div>
                <div class="text-muted small text-truncate">
                    <span>${engName}</span> —
                    <span class="text-primary">${deptName}</span>
                </div>
            </div>
        `;

        a.addEventListener('click', (e) => {
            e.preventDefault();
            setActiveUserItem(idx);
            const uid = a.dataset.userid;
            loadUserDetail(uid);                 // ✅ โหลดรายละเอียด
        });

        container.appendChild(a);
    });

    // ✅ โหลดรายละเอียดของรายการแรกอัตโนมัติ
    const first = container.querySelector('.list-group-item');
    if (first) {
        const uid = first.getAttribute('data-userid');
        if (uid) loadUserDetail(uid);
    }
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

function setChoicesSingle(choicesInstance, value) {
    try {
        const v = value == null ? '' : String(value);
        choicesInstance.removeActiveItems();   // เอา selection เดิมออกอย่างเดียว
        if (v !== '') {
            choicesInstance.setChoiceByValue(v); // เลือกค่าที่ต้องการ
        }
    } catch { }
}

function setChoicesMulti(choicesInstance, valuesCsv) {
    try {
        const arr = (valuesCsv || '').split(',').map(s => s.trim()).filter(Boolean);
        choicesInstance.removeActiveItems();
        if (arr.length) choicesInstance.setChoiceByValue(arr);
    } catch { }
}

function fillEditForm(detail) {
    if (!detail) return;

    // ชื่อหัวการ์ด
    const nameShow = document.getElementById('user_name_show');
    if (nameShow) {
        const th = [detail.FirstName, detail.LastName].filter(Boolean).join(' ');
        nameShow.textContent = th || '(ไม่ระบุชื่อ)';
    }

    // Title (native select)
    const ddlTitle = document.getElementById('ddl_Title_edit');
    if (ddlTitle) ddlTitle.value = detail.TitleID ?? '';

    // Inputs ใช้ .value
    const setVal = (id, v) => { const el = document.getElementById(id); if (el) el.value = v ?? ''; };
    setVal('txt_first_name_th_edit', detail.FirstName);
    setVal('txt_last_name_th_edit', detail.LastName);
    setVal('txt_first_name_en_edit', detail.FirstName_Eng);
    setVal('txt_last_name_en_edit', detail.LastName_Eng);
    setVal('txt_number_edit', detail.Mobile);
    setVal('txt_email_edit', detail.Email);
    setVal('txt_userid_edit', detail.UserID);
    setVal('txt_password_edit', detail.Password);
    setVal('txt_comfirm_password_edit', detail.Password);

    // Department (Choices single)
    if (window.ddlDepartmentEditChoices) {
        setChoicesSingle(ddlDepartmentEditChoices, detail.DepartmentID);
    } else {
        const el = document.getElementById('ddl_Department_edit');
        if (el) el.value = detail.DepartmentID ?? '';
    }

    // Role (Choices single)
    if (window.ddlRoleChoices) {
        setChoicesSingle(ddlRoleChoices, detail.RoleID);
    } else {
        const el = document.getElementById('ddl_Role_edit');
        if (el) el.value = detail.RoleID ?? '';
    }

    // BU (Choices multi) — CSV เช่น "1,2,3"
    if (window.ddlBuChoices) {
        setChoicesMulti(ddlBuChoices, detail.BUMaping || '');
    } else {
        const el = document.getElementById('ddl_Bu_edit');
        if (el) {
            [...el.options].forEach(o => (o.selected = false));
            (detail.BUMaping || '')
                .split(',').map(s => s.trim()).filter(Boolean)
                .forEach(v => {
                    const opt = [...el.options].find(o => o.value == v);
                    if (opt) opt.selected = true;
                });
        }
    }

    // Flags
    const chkAdmin = document.getElementById('chk_Flag_admin_edit');
    const chkReadonly = document.getElementById('chk_Flag_readonly_edit');
    if (chkAdmin) chkAdmin.checked = !!detail.FlagAdmin;
    if (chkReadonly) chkReadonly.checked = !!detail.FlagReadonly;
}


async function loadUserDetail(userId) {
    if (!userId) return;
    try {
        showLoading?.();
        const form = new FormData();
        form.append('L_UserID', String(userId));

        const res = await fetch(baseUrl + 'UserAndPermission/GetDetailsUser', {
            method: 'POST',
            body: form
        });
        const json = await res.json();

        if (!json?.success) {
            console.warn('GetDetailsUser failed', json?.message || json);
            return;
        }

        // เก็บ user id ปัจจุบัน
        currentEditUserId = json.data?.ID || userId;

        // 👉 fetch project mapping
        await loadProjectsUserMapping(userId);

        fillEditForm(json.data);
    } catch (err) {
        console.error(err);
    } finally {
        hideLoading?.();
    }
}

async function loadProjectsUserMapping(userId) {
    try {
        const form = new FormData();
        form.append('UserID', String(userId));

        const res = await fetch(baseUrl + 'UserAndPermission/GetProjectsUserMapping', {
            method: 'POST',
            body: form
        });
        const json = await res.json();

        if (!json?.success) {
            console.warn('GetProjectsUserMapping failed', json?.message || json);
            return;
        }

        renderProjectsUserMapping(json.data);
    } catch (err) {
        console.error(err);
    }
}

function renderProjectsUserMapping(projects) {
    const tbody = document.querySelector('#Tb_ProjectsUserMapping tbody');
    if (!tbody) return;
    tbody.innerHTML = '';

    projects.forEach(p => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td class="active">
                <input type="checkbox" class="select-item checkbox"
                       value="${p.ProjectID}"
                       ${p.ISCheck ? 'checked' : ''} />
            </td>
            <td>${p.BUName || ''}</td>
            <td>
                <div class="fw-semibold">${p.ProjectName || ''}</div>
                <div class="text-muted small">${p.ProjectName_Eng || ''}</div>
            </td>
        `;
        tbody.appendChild(tr);

        // ✅ highlight row if already checked
        if (p.ISCheck) tr.classList.add('checked-row');

        // ✅ add event for toggle highlight
        tr.querySelector('input[type="checkbox"]').addEventListener('change', (e) => {
            if (e.target.checked) {
                tr.classList.add('checked-row');
            } else {
                tr.classList.remove('checked-row');
            }
        });
    });
}



document.getElementById('searchProjectsUserMapping')?.addEventListener('input', (e) => {
    const keyword = e.target.value.toLowerCase();
    document.querySelectorAll('#Tb_ProjectsUserMapping tbody tr').forEach(tr => {
        const text = tr.innerText.toLowerCase();
        tr.style.display = text.includes(keyword) ? '' : 'none';
    });
});


async function onSaveEditUser(e) {
    e.preventDefault();

    if (!currentEditUserId) {
        errorToast('กรุณาเลือกผู้ใช้จากด้านซ้ายก่อน');
        return;
    }

    // --- helper ---
    const getVal = (id) => document.getElementById(id)?.value?.trim() ?? '';

    // เก็บค่าจากฟอร์ม
    const titleId = getVal('ddl_Title_edit');
    const firstTh = getVal('txt_first_name_th_edit');
    const lastTh = getVal('txt_last_name_th_edit');
    const firstEn = getVal('txt_first_name_en_edit');
    const lastEn = getVal('txt_last_name_en_edit');
    const mobile = getVal('txt_number_edit');
    const email = getVal('txt_email_edit');
    const userId = getVal('txt_userid_edit');
    const password = getVal('txt_password_edit');
    const confirmPwd = getVal('txt_comfirm_password_edit');

    // Dept/Role จาก Choices (single)
    const deptId = (ddlDepartmentEditChoices?.getValue(true) ??
        document.getElementById('ddl_Department_edit')?.value ?? '').toString();

    const roleId = (ddlRoleChoices?.getValue(true) ??
        document.getElementById('ddl_Role_edit')?.value ?? '').toString();

    const buIds = (ddlBuChoices?.getValue(true) || []).map(v => String(v));

    // Flags
    const flagAdmin = !!document.getElementById('chk_Flag_admin_edit')?.checked;
    const flagReadonly = !!document.getElementById('chk_Flag_readonly_edit')?.checked;

    // --- Validation (ฟิลด์บังคับ) ---
    if (!titleId || titleId === "0") {
        errorToast('กรุณาเลือก Title');
        return;
    }
    if (!firstTh || !lastTh) {
        errorToast('กรุณากรอกชื่อ–นามสกุล (ไทย)');
        return;
    }
    if (!firstEn || !lastEn) {
        errorToast('กรุณากรอกชื่อ–นามสกุล (อังกฤษ)');
        return;
    }
    if (!deptId) {
        errorToast('กรุณาเลือก Department');
        return;
    }
    if (!roleId) {
        errorToast('กรุณาเลือก Role');
        return;
    }
    if (!userId) {
        errorToast('กรุณากรอก User ID');
        return;
    }
    if (!password) {
        errorToast('กรุณากรอก Password');
        return;
    }
    if (!confirmPwd) {
        errorToast('กรุณากรอก Confirm Password');
        return;
    }
    if (password !== confirmPwd) {
        errorToast('ยืนยันรหัสผ่านไม่ตรงกัน');
        return;
    }

    // --- FormData → match กับ UpdateUserRequest ---
    const form = new FormData();
    form.append('ID', String(currentEditUserId));
    form.append('TitleID', titleId);
    form.append('FirstName', firstTh);
    form.append('LastName', lastTh);
    form.append('FirstName_Eng', firstEn);
    form.append('LastName_Eng', lastEn);
    form.append('Mobile', mobile);
    form.append('Email', email);
    form.append('UserID', userId);
    form.append('Password', password);
    form.append('DepartmentID', deptId);
    buIds.forEach(v => form.append('BUIds', v)); 
    form.append('RoleID', roleId);
    form.append('FlagAdmin', flagAdmin ? 'true' : 'false');
    form.append('FlagReadonly', flagReadonly ? 'true' : 'false');

    try {
        showLoading();
        const res = await fetch(baseUrl + 'UserAndPermission/Update', {
            method: 'POST',
            body: form
        });
        const json = await res.json();

        if (json?.success) {
            successToast('บันทึกข้อมูลเรียบร้อย');
            onSearchUsers(); // รีโหลดรายการซ้าย
        } else {
            const msg =
                json?.message ||
                (json?.duplicate ? 'ข้อมูลซ้ำ' : 'บันทึกไม่สำเร็จ');
            errorToast(msg);
            if (json?.duplicate) {
                console.warn('Duplicate detail:', json.duplicate);
            }
        }
    } catch (err) {
        console.error(err);
        errorToast('เกิดข้อผิดพลาดระหว่างบันทึก');
    } finally {
        hideLoading();
    }
}

// helper: ดึงค่าจาก input
const gv = (id) => (document.getElementById(id)?.value ?? '').trim();

async function onCreateUserSubmit(e) {
    e.preventDefault();

    const titleId = gv('ddl_Title_create');
    const firstTh = gv('txt_first_name_th_create');
    const lastTh = gv('txt_last_name_th_create');
    const firstEn = gv('txt_first_name_en_create');
    const lastEn = gv('txt_last_name_en_create');
    const mobile = gv('txt_number_create');
    const email = gv('txt_email_create');
    const userId = gv('txt_userid_create');
    const pwd = gv('txt_password_create');
    const pwd2 = gv('txt_confirm_password_create');

    const deptId = (ddlDepartmentCreateChoices?.getValue(true) ??
        document.getElementById('ddl_Department_create')?.value ?? '').toString();

    const roleId = (ddlRoleCreateChoices?.getValue(true) ??
        document.getElementById('ddl_Role_create')?.value ?? '').toString();

    const buIds = ddlBuCreateChoices?.getValue(true) || []; // อาจเป็น number|array
    const buIdArr = Array.isArray(buIds) ? buIds : (buIds ? [buIds] : []);

    const flagAdmin = !!document.getElementById('chk_Flag_admin_create')?.checked;
    const flagReadonly = !!document.getElementById('chk_Flag_readonly_create')?.checked;

    // ----- Validate ขั้นพื้นฐาน -----
    if (!titleId || titleId === '0') return errorToast('กรุณาเลือก Title');
    if (!firstTh || !lastTh) return errorToast('กรุณากรอกชื่อ–นามสกุล (ไทย)');
    if (!firstEn || !lastEn) return errorToast('กรุณากรอกชื่อ–นามสกุล (อังกฤษ)');
    if (!deptId) return errorToast('กรุณาเลือก Department');
    if (!roleId) return errorToast('กรุณาเลือก Role');
    if (!userId) return errorToast('กรุณากรอก User ID');
    if (!pwd) return errorToast('กรุณากรอก Password');
    if (!pwd2) return errorToast('กรุณากรอก Confirm Password');
    if (pwd !== pwd2) return errorToast('ยืนยันรหัสผ่านไม่ตรงกัน');

    // ----- สร้าง FormData ให้ตรงกับ CreateUserRequest -----
    const form = new FormData();
    form.append('TitleID', titleId);
    form.append('FirstName', firstTh);
    form.append('LastName', lastTh);
    form.append('FirstName_Eng', firstEn);
    form.append('LastName_Eng', lastEn);
    form.append('Mobile', mobile);
    form.append('Email', email);
    form.append('UserID', userId);
    form.append('Password', pwd);
    form.append('DepartmentID', deptId);
    form.append('RoleID', roleId);
    form.append('FlagAdmin', flagAdmin ? 'true' : 'false');
    form.append('FlagReadonly', flagReadonly ? 'true' : 'false');

    // ✅ แนวทางที่ bind กับ List<int> ได้แน่นอน: ส่งหลายค่าใน key เดียวกัน
    buIdArr.map(String).forEach(v => form.append('BUIds', v));

    try {
        showLoading?.();

        // ส่งให้ Controller ตรวจ duplicate + บันทึกในที่เดียว
        const res = await fetch(baseUrl + 'UserAndPermission/Create', { method: 'POST', body: form });
        const json = await res.json();

        if (json?.success) {
            successToast('สร้างผู้ใช้เรียบร้อย');

            // ปิด modal + เคลียร์ฟอร์ม + ล้าง choices + refresh รายชื่อ
            const modalEl = document.getElementById('modalCreateUser');
            bootstrap.Modal.getOrCreateInstance(modalEl).hide();

            document.getElementById('create_user_form')?.reset();
            ddlDepartmentCreateChoices?.removeActiveItems();
            ddlRoleCreateChoices?.removeActiveItems();
            ddlBuCreateChoices?.removeActiveItems();

            onSearchUsers?.();
        } else {
            const msg = json?.message || (json?.duplicate ? 'ข้อมูลซ้ำ' : 'บันทึกไม่สำเร็จ');
            errorMessage(msg,'บันทึกไม่สำเร็จ');
            if (json?.duplicate) console.warn('Duplicate detail:', json.duplicate);
        }
    } catch (err) {
        console.error(err);
        errorToast('เกิดข้อผิดพลาดระหว่างบันทึก');
    } finally {
        hideLoading?.();
    }
}

// เคลียร์ฟอร์มเมื่อ modal ถูกเปิด
document.getElementById('modalCreateUser')?.addEventListener('show.bs.modal', () => {
    // reset form ทั้งหมด
    document.getElementById('create_user_form')?.reset();

    // เคลียร์ Choices
    ddlDepartmentCreateChoices?.removeActiveItems();
    ddlRoleCreateChoices?.removeActiveItems();
    ddlBuCreateChoices?.removeActiveItems();

    // reset select ปกติ (กันกรณี Choices ไม่ได้ init)
    const dept = document.getElementById('ddl_Department_create');
    if (dept) dept.value = '';

    const role = document.getElementById('ddl_Role_create');
    if (role) role.value = '';

    const bu = document.getElementById('ddl_Bu_create');
    if (bu) [...bu.options].forEach(o => o.selected = false);

    // reset checkbox
    const chkAdmin = document.getElementById('chk_Flag_admin_create');
    if (chkAdmin) chkAdmin.checked = false;

    const chkReadonly = document.getElementById('chk_Flag_readonly_create');
    if (chkReadonly) chkReadonly.checked = true; // default on
});


