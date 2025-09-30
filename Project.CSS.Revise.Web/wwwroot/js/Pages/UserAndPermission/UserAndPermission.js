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

    loadPermissionMatrix();

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

        // ✅ reset filter dropdown to "All"
        const stateEl = document.getElementById('filterCheckedState');
        if (stateEl) {
            stateEl.value = 'all';
        }

        // ✅ re-apply filters (show all rows by default)
        if (typeof applyProjectFilters === 'function') {
            applyProjectFilters();
        }
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
            errorMessage(msg, 'บันทึกไม่สำเร็จ');
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

function getCheckedProjects() {
    const checked = [];
    document.querySelectorAll('#Tb_ProjectsUserMapping tbody .select-item:checked')
        .forEach(cb => {
            const row = cb.closest('tr');
            checked.push({
                ProjectID: cb.value,
                BUName: row.children[1]?.innerText.trim() || '',
                ProjectName: row.querySelector('.fw-semibold')?.innerText.trim() || '',
                ProjectName_Eng: row.querySelector('.text-muted')?.innerText.trim() || ''
            });
        });
    return checked;
}

document.getElementById('save_user_project')?.addEventListener('click', () => {
    const selected = getCheckedProjects(); // [{ ProjectID, ... }]
    const payload = {
        UserID: currentEditUserId,
        ProjectID: selected.map(p => p.ProjectID) // [] allowed for clear-all
    };

    // If nothing selected -> confirm "clear all"
    if (selected.length === 0) {
        Swal.fire({
            icon: 'question',
            title: 'Clear all projects?',
            text: 'คุณต้องการลบการผูกโครงการทั้งหมดของผู้ใช้นี้หรือไม่',
            showCancelButton: true,
            confirmButtonText: 'Yes, clear all',
            cancelButtonText: 'Cancel'
        }).then(result => {
            if (!result.isConfirmed) return;
            showLoading();
            fetch(baseUrl + 'UserAndPermission/IUDProjectUserMapping', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload) // ProjectID: []
            })
                .then(res => res.json())
                .then(json => {
                    if (json.success) {
                        hideLoading();
                        Swal.fire({
                            icon: 'success',
                            title: 'Cleared',
                            text: 'ลบการผูกโครงการทั้งหมดเรียบร้อยแล้ว'
                        });
                    } else {
                        hideLoading();
                        Swal.fire({
                            icon: 'error',
                            title: 'Save Failed',
                            text: json.message || 'ไม่สามารถบันทึกได้'
                        });
                    }
                })
                .catch(err => {
                    console.error(err);
                    hideLoading();
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'เกิดข้อผิดพลาดในการเชื่อมต่อ'
                    });
                });
        });
        return; // stop here; handled by confirm branch
    }
    showLoading();
    // Normal save (some projects selected)
    fetch(baseUrl + 'UserAndPermission/IUDProjectUserMapping', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
    })
        .then(res => res.json())
        .then(json => {
            if (json.success) {
                hideLoading();
                Swal.fire({
                    icon: 'success',
                    title: 'Saved!',
                    text: 'บันทึกโครงการเรียบร้อยแล้ว'
                });
            } else {
                hideLoading();
                Swal.fire({
                    icon: 'error',
                    title: 'Save Failed',
                    text: json.message || 'ไม่สามารถบันทึกได้'
                });
            }
        })
        .catch(err => {
            console.error(err);
            hideLoading();
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'เกิดข้อผิดพลาดในการเชื่อมต่อ'
            });
        });
});



const searchEl = document.getElementById('searchProjectsUserMapping');
const stateEl = document.getElementById('filterCheckedState');

function applyProjectFilters() {
    const kw = (searchEl?.value || '').trim().toLowerCase();
    const mode = stateEl?.value || 'all';

    document.querySelectorAll('#Tb_ProjectsUserMapping tbody tr').forEach(tr => {
        const cb = tr.querySelector('.select-item');
        const isChecked = !!(cb && cb.checked);

        // status filter
        let passState = true;
        if (mode === 'checked') passState = isChecked;
        if (mode === 'unchecked') passState = !isChecked;

        // keyword filter
        let passKw = true;
        if (kw) {
            const text = tr.innerText.toLowerCase();
            passKw = text.includes(kw);
        }

        tr.style.display = (passState && passKw) ? '' : 'none';
    });
}

// wire events
searchEl?.addEventListener('input', applyProjectFilters);
stateEl?.addEventListener('change', applyProjectFilters);

// call once after rendering table
applyProjectFilters();

async function loadPermissionMatrix() {
    const tbody = document.getElementById('permissionMatrixBody');
    if (!tbody) return console.warn('permissionMatrixBody not found');

    tbody.innerHTML = `<tr><td colspan="6" class="text-center text-muted py-4">Loading permissions…</td></tr>`;

    try {
        const res = await fetch(baseUrl + 'UserAndPermission/GetListPermissionMatrix', { method: 'POST' });
        const json = await res.json();
        const rows = Array.isArray(json?.data) ? json.data : [];

        if (rows.length === 0) {
            tbody.innerHTML = `<tr><td colspan="6" class="text-center text-muted py-4">No permission data</td></tr>`;
            return;
        }

        const esc = (s) => (s ?? '').toString()
            .replace(/&/g, '&amp;').replace(/</g, '&lt;')
            .replace(/>/g, '&gt;').replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;');

        const parts = [];

        for (const r of rows) {
            const level = Number(r.Level) || 2;
            const isLeaf = !!r.IsLeaf;
            const hasAny = !!(r.CanView || r.CanAdd || r.CanEdit || r.CanDelete || r.CanDownload);
            const nameEsc = esc(r.Name);

            const indentSpan = level === 1
                ? `<span class="tree-toggle disabled"><i class="bi bi-caret-down-fill"></i></span>`
                : level === 2
                    ? `<span class="tree-indent"></span>${isLeaf ? '' : `<span class="tree-toggle disabled"><i class="bi bi-caret-down-fill"></i></span>`}`
                    : `<span class="tree-indent l3"></span>`;

            const iconHtml = isLeaf
                ? `<i class="bi bi-file-text me-1"></i>`
                : (level === 1
                    ? `<i class="bi bi-folder2-open text-primary me-1"></i>`
                    : `<i class="bi bi-folder2 text-secondary me-1"></i>`);

            const nameCell = `
        <td class="sticky-col">
          ${indentSpan}
          ${iconHtml}
          <span class="fw-semibold">${nameEsc}</span>
        </td>`;

            // helper to render an action cell with perm-chk + data attrs
            const actionCell = (available, actionKey, initialChecked, menuId, nameEsc) => {
                if (!available) return `<td class="text-center no-action">—</td>`;
                const chk = initialChecked ? 'checked' : '';
                return `<td class="text-center">
            <input type="checkbox" class="form-check-input perm-chk"
                   data-id="${menuId}" data-name="${nameEsc}"
                   data-action="${actionKey}" ${chk}>
          </td>`;
            };


            // If your API doesn’t send selected states yet, these will be false (unchecked).
            const v = !!r.View, a = !!r.Add, u = !!r.Update, d = !!r.Delete, dl = !!r.Download;

            const mid = Number(r.MenuID) || 0;
            parts.push(`
                          <tr class="tree-row l${level}${hasAny ? ' has-perm' : ''}">
                            ${nameCell}
                            ${actionCell(!!r.CanView, 'View', v, mid, nameEsc)}
                            ${actionCell(!!r.CanAdd, 'Add', a, mid, nameEsc)}
                            ${actionCell(!!r.CanEdit, 'Update', u, mid, nameEsc)}
                            ${actionCell(!!r.CanDelete, 'Delete', d, mid, nameEsc)}
                            ${actionCell(!!r.CanDownload, 'Download', dl, mid, nameEsc)}
                          </tr>
                        `);
        }

        tbody.innerHTML = parts.join('');
    } catch (err) {
        console.error(err);
        tbody.innerHTML = `<tr><td colspan="6" class="text-center text-danger py-4">Failed to load permissions</td></tr>`;
    }
}


let currentDept = { id: null, name: '' };
let currentRole = { id: null, name: '' };

function setHeader(deptName, roleName) {
    if (deptName !== null) document.getElementById('Dep_select').textContent = deptName || '—';
    if (roleName !== null) document.getElementById('Role_select').textContent = roleName || '—';
}

function enableFooter(enabled) {
    document.getElementById('btnSavePermissions').disabled = !enabled;
    document.getElementById('btnResetPermissions').disabled = !enabled;
}

function wireDeptRoleClicks() {
    document.querySelectorAll('.dept-item').forEach(el => {
        el.addEventListener('click', () => {
            document.querySelectorAll('.dept-item.active').forEach(a => a.classList.remove('active'));
            el.classList.add('active');
            currentDept.id = el.dataset.id;
            currentDept.name = el.dataset.name;
            setHeader(currentDept.name, null);
            enableFooter(!!(currentDept.id && currentRole.id));
        });
    });

    document.querySelectorAll('.role-item').forEach(el => {
        el.addEventListener('click', () => {
            document.querySelectorAll('.role-item.active').forEach(a => a.classList.remove('active'));
            el.classList.add('active');
            currentRole.id = el.dataset.id;
            currentRole.name = el.dataset.name;
            setHeader(null, currentRole.name);
            enableFooter(!!(currentDept.id && currentRole.id));
        });
    });
}

// Collect checked actions grouped by menu name
function collectPermissionItems() {
    // key by MenuID (falls back to Name if MenuID missing)
    const itemsByKey = new Map();

    document.querySelectorAll('#permissionMatrixBody .perm-chk').forEach(chk => {
        const idStr = chk.getAttribute('data-id');
        const mid = idStr ? Number(idStr) : null;
        const name = chk.getAttribute('data-name') || '';
        const action = chk.getAttribute('data-action'); // View|Add|Update|Delete|Download
        const val = chk.checked;

        if (!action) return;

        const key = (mid && !Number.isNaN(mid)) ? `id:${mid}` : `name:${name}`;
        if (!itemsByKey.has(key)) {
            itemsByKey.set(key, {
                MenuID: (mid && !Number.isNaN(mid)) ? mid : undefined,
                Name: (!mid || Number.isNaN(mid)) ? name : undefined,
                View: false, Add: false, Update: false, Delete: false, Download: false
            });
        }
        itemsByKey.get(key)[action] = val;
    });

    return Array.from(itemsByKey.values())
        .filter(it => it.View || it.Add || it.Update || it.Delete || it.Download);
}


async function savePermissions() {
    if (!currentDept.id || !currentRole.id) {
        Swal?.fire({ icon: 'warning', title: 'Select Department & Role', text: 'กรุณาเลือก Department และ Role' });
        return;
    }

    const items = collectPermissionItems();

    const payload = {
        QCTypeID: 10,
        DepartmentID: Number(currentDept.id),
        RoleID: Number(currentRole.id),
        Items: items
    };

    try {
        const res = await fetch(baseUrl + 'UserAndPermission/SaveRolePermissions', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });
        const json = await res.json();
        if (json?.success) {
            Swal?.fire({ icon: 'success', title: 'Saved', text: 'บันทึกสิทธิสำเร็จ' });
        } else {
            Swal?.fire({ icon: 'error', title: 'Save failed', text: json?.message || 'ไม่สามารถบันทึกได้' });
        }
    } catch (err) {
        console.error(err);
        Swal?.fire({ icon: 'error', title: 'Error', text: 'เกิดข้อผิดพลาดในการเชื่อมต่อ' });
    }
}

function resetPermissions() {
    document.querySelectorAll('#permissionMatrixBody .perm-chk').forEach(chk => chk.checked = false);
}

function wireFooterButtons() {
    document.getElementById('btnSavePermissions')?.addEventListener('click', (e) => {
        e.preventDefault();
        savePermissions();
    });
    document.getElementById('btnResetPermissions')?.addEventListener('click', (e) => {
        e.preventDefault();
        resetPermissions();
    });
}


async function loadPermissionMatrixFor(deptId, roleId) {
    const tbody = document.getElementById('permissionMatrixBody');
    if (!tbody) return;

    tbody.innerHTML = `<tr><td colspan="6" class="text-center text-muted py-4">Loading permissions…</td></tr>`;

    try {
        const res = await fetch(baseUrl + 'UserAndPermission/GetRolePermissions', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ QCTypeID: 10, DepartmentID: Number(deptId), RoleID: Number(roleId) })
        });
        const json = await res.json();
        const rows = (json?.success && Array.isArray(json?.data)) ? json.data : [];

        if (rows.length === 0) {
            tbody.innerHTML = `<tr><td colspan="6" class="text-center text-muted py-4">No permission data</td></tr>`;
            return;
        }

        const esc = (s) => (s ?? '').toString()
            .replace(/&/g, '&amp;').replace(/</g, '&lt;')
            .replace(/>/g, '&gt;').replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;');

        const parts = [];

        const actionCell = (available, actionKey, isChecked, menuId, nameEsc) => {
            if (!available) return `<td class="text-center no-action">—</td>`;
            return `<td class="text-center">
                <input type="checkbox" class="form-check-input perm-chk"
                       data-id="${menuId}"
                       data-name="${nameEsc}"
                       data-action="${actionKey}"
                       ${isChecked ? 'checked' : ''}>
              </td>`;
        };

        for (const r of rows) {
            const level = Number(r.Level) || 2;
            const isLeaf = !!r.IsLeaf;
            const nameEsc = esc(r.Name);
            const mid = Number(r.MenuID) || 0;
            const hasAny = !!(r.CanView || r.CanAdd || r.CanEdit || r.CanDelete || r.CanDownload);

            const indentSpan = level === 1
                ? `<span class="tree-toggle disabled"><i class="bi bi-caret-down-fill"></i></span>`
                : level === 2
                    ? `<span class="tree-indent"></span>${isLeaf ? '' : `<span class="tree-toggle disabled"><i class="bi bi-caret-down-fill"></i></span>`}`
                    : `<span class="tree-indent l3"></span>`;

            const iconHtml = isLeaf
                ? `<i class="bi bi-file-text me-1"></i>`
                : (level === 1
                    ? `<i class="bi bi-folder2-open text-primary me-1"></i>`
                    : `<i class="bi bi-folder2 text-secondary me-1"></i>`);

            const nameCell = `
        <td class="sticky-col">
          ${indentSpan}
          ${iconHtml}
          <span class="fw-semibold">${nameEsc}</span>
        </td>`;

            parts.push(`
        <tr class="tree-row l${level}${hasAny ? ' has-perm' : ''}">
          ${nameCell}
          ${actionCell(!!r.CanView, 'View', !!r.SelView, mid, nameEsc)}
          ${actionCell(!!r.CanAdd, 'Add', !!r.SelAdd, mid, nameEsc)}
          ${actionCell(!!r.CanEdit, 'Update', !!r.SelEdit, mid, nameEsc)}
          ${actionCell(!!r.CanDelete, 'Delete', !!r.SelDelete, mid, nameEsc)}
          ${actionCell(!!r.CanDownload, 'Download', !!r.SelDownload, mid, nameEsc)}
        </tr>
      `);
        }

        tbody.innerHTML = parts.join('');

        // Footer buttons enable when both selected
        enableFooter(!!(currentDept.id && currentRole.id));
    } catch (err) {
        console.error(err);
        tbody.innerHTML = `<tr><td colspan="6" class="text-center text-danger py-4">Failed to load permissions</td></tr>`;
    }
}

function wireDeptRoleClicks() {
    document.querySelectorAll('.dept-item').forEach(el => {
        el.addEventListener('click', () => {
            document.querySelectorAll('.dept-item.active').forEach(a => a.classList.remove('active'));
            el.classList.add('active');
            currentDept.id = el.dataset.id;
            currentDept.name = el.dataset.name;
            setHeader(currentDept.name, null);
            if (currentDept.id && currentRole.id) loadPermissionMatrixFor(currentDept.id, currentRole.id);
            enableFooter(!!(currentDept.id && currentRole.id));
        });
    });

    document.querySelectorAll('.role-item').forEach(el => {
        el.addEventListener('click', () => {
            document.querySelectorAll('.role-item.active').forEach(a => a.classList.remove('active'));
            el.classList.add('active');
            currentRole.id = el.dataset.id;
            currentRole.name = el.dataset.name;
            setHeader(null, currentRole.name);
            if (currentDept.id && currentRole.id) loadPermissionMatrixFor(currentDept.id, currentRole.id);
            enableFooter(!!(currentDept.id && currentRole.id));
        });
    });

    //if (currentDept.id && currentRole.id) {
    //    loadPermissionMatrixFor(currentDept.id, currentRole.id);
    //}

}

document.addEventListener('DOMContentLoaded', () => {
    wireDeptRoleClicks();
    wireFooterButtons();

    // Optional: auto-select first dept & role
    const firstDept = document.querySelector('.dept-item');
    const firstRole = document.querySelector('.role-item');
    if (firstDept) firstDept.click();
    if (firstRole) firstRole.click();

    // Save/Reset stays disabled until both are selected
    enableFooter(!!(currentDept.id && currentRole.id));

    if (currentDept.id && currentRole.id) {
        loadPermissionMatrixFor(currentDept.id, currentRole.id);
    }
});

// Open Role Edit modal with selected item
document.querySelectorAll('.role-item .btn-icon').forEach(btn => {
    btn.addEventListener('click', (e) => {
        const item = e.currentTarget.closest('.role-item');
        const id = item?.dataset.id;
        const name = item?.dataset.name || '';
        document.getElementById('roleEditId').value = id || '';
        document.getElementById('roleEditName').value = name;
    });
});
// Create
document.getElementById('btnRoleAddSave')?.addEventListener('click', async () => {
    const name = (document.getElementById('roleAddName').value || '').trim();
    if (!name) {
        Swal?.fire({ icon: 'warning', title: 'Enter role name', text: 'Role name is required' });
        return;
    }

    try {
        const res = await fetch(baseUrl + 'UserAndPermission/Role/Create', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name })
        });
        const json = await res.json();
        if (json?.success) {
            bootstrap.Modal.getInstance(document.getElementById('mdlRoleAdd'))?.hide();
            appendRoleToList(json.id, name);
            Swal?.fire({ icon: 'success', title: 'Created', text: 'เพิ่ม Role สำเร็จ' });
            document.getElementById('roleAddName').value = '';
        } else {
            Swal?.fire({ icon: 'error', title: 'Failed', text: json?.message || 'ไม่สามารถบันทึกได้' });
        }
    } catch (err) {
        console.error(err);
        Swal?.fire({ icon: 'error', title: 'Error', text: 'เกิดข้อผิดพลาดในการเชื่อมต่อ' });
    }
});

// Update
document.getElementById('btnRoleEditUpdate')?.addEventListener('click', async () => {
    const id = Number(document.getElementById('roleEditId').value) || 0;
    const name = (document.getElementById('roleEditName').value || '').trim();
    if (!id || !name) {
        Swal?.fire({ icon: 'warning', title: 'ข้อมูลไม่ครบ', text: 'ต้องมีรหัสและชื่อ Role' });
        return;
    }

    try {
        const res = await fetch(baseUrl + 'UserAndPermission/Role/Update', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id, name })
        });
        const json = await res.json();
        if (json?.success) {
            bootstrap.Modal.getInstance(document.getElementById('mdlRoleEdit'))?.hide();
            updateRoleInList(id, name);
            Swal?.fire({ icon: 'success', title: 'Updated', text: 'อัปเดต Role สำเร็จ' });
        } else {
            Swal?.fire({ icon: 'error', title: 'Failed', text: json?.message || 'ไม่สามารถอัปเดตได้' });
        }
    } catch (err) {
        console.error(err);
        Swal?.fire({ icon: 'error', title: 'Error', text: 'เกิดข้อผิดพลาดในการเชื่อมต่อ' });
    }
});

// Delete (soft)
document.getElementById('btnRoleEditDelete')?.addEventListener('click', async () => {
    const id = Number(document.getElementById('roleEditId').value) || 0;
    const name = (document.getElementById('roleEditName').value || '').trim();
    if (!id) return;

    const confirm = await Swal?.fire({
        icon: 'warning',
        title: 'ลบ Role?',
        html: `คุณต้องการลบ <b>${name}</b> หรือไม่?<br><small class="text-muted">* จะเป็นการปิดใช้งาน (soft delete)</small>`,
        showCancelButton: true,
        confirmButtonText: 'Delete',
    });
    if (!confirm?.isConfirmed) return;

    try {
        const res = await fetch(baseUrl + 'UserAndPermission/Role/Delete', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id })
        });
        const json = await res.json();
        if (json?.success) {
            bootstrap.Modal.getInstance(document.getElementById('mdlRoleEdit'))?.hide();
            removeRoleFromList(id);
            Swal?.fire({ icon: 'success', title: 'Deleted', text: 'ลบ Role สำเร็จ' });
        } else {
            Swal?.fire({ icon: 'error', title: 'Cannot delete', text: json?.message || 'ไม่สามารถลบได้' });
        }
    } catch (err) {
        console.error(err);
        Swal?.fire({ icon: 'error', title: 'Error', text: 'เกิดข้อผิดพลาดในการเชื่อมต่อ' });
    }
});

// Append a new role item to the roles list
function appendRoleToList(id, name) {
    const list = document.querySelector('.roles-list'); // <— updated
    if (!list) return;

    const el = document.createElement('div');
    el.className = 'list-group-item list-group-item-action d-flex align-items-center justify-content-between role-item';
    el.dataset.id = String(id);
    el.dataset.name = name;

    el.innerHTML = `
    <div class="dept-name text-truncate" title="${escapeHtml(name)}">
      <i class="bi bi-shield-lock text-primary me-2"></i>
      <span class="fw-semibold"><a>${escapeHtml(name)}</a></span>
    </div>
    <button class="btn btn-light rounded-circle btn-icon" title="Edit" data-bs-toggle="modal" data-bs-target="#mdlRoleEdit">
      <i class="bi bi-pencil"></i>
    </button>
  `;

    list.prepend(el);
}

// Update an existing role item’s displayed name
function updateRoleInList(id, name) {
    const el = document.querySelector(`.roles-list .role-item[data-id="${id}"]`); // <— updated
    if (!el) return;
    el.dataset.name = name;
    const nameSpan = el.querySelector('.dept-name span a');
    const nameDiv = el.querySelector('.dept-name');
    if (nameSpan) nameSpan.textContent = name;
    if (nameDiv) nameDiv.title = name;
}

// Remove a role item from the roles list
function removeRoleFromList(id) {
    const el = document.querySelector(`.roles-list .role-item[data-id="${id}"]`); // <— updated
    if (el?.parentElement) el.parentElement.removeChild(el);
}

// Delegate click for dynamically added edit buttons (safer than wiring one-by-one)
document.querySelector('.roles-list')?.addEventListener('click', (e) => {
    const btn = e.target.closest('.btn-icon');
    if (!btn) return;
    const item = btn.closest('.role-item');
    if (!item) return;
    document.getElementById('roleEditId').value = item.dataset.id || '';
    document.getElementById('roleEditName').value = item.dataset.name || '';
});

// Utility
function escapeHtml(s) {
    return (s ?? '').toString()
        .replace(/&/g, '&amp;').replace(/</g, '&lt;')
        .replace(/>/g, '&gt;').replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');
}

