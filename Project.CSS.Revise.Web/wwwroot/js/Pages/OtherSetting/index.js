let buChoices = null;
let projectChoices = null;

function loadBUOptions(callback) {
    $('.loader-wrapper').show();

    $.ajax({
        url: baseUrl + 'OtherSettings/Page_Load',
        type: 'POST',
        dataType: 'json',
        success: function (res) {
            if (res.success && res.buList?.length) {
                const buSelect = document.getElementById('ddl-bu-shop-event');

                // Reset old Choices
                if (buChoices) {
                    buChoices.destroy();
                }

                // Clear old options
                buSelect.innerHTML = '';

                // ✅ Add default option manually
                const defaultOption = new Option('เลือก BU', '', true, true); // <-- selected: true
                defaultOption.disabled = true;
                defaultOption.hidden = true;
                buSelect.add(defaultOption);


                // Populate new options
                res.buList.forEach(x => {
                    const option = new Option(x.Name, x.ID, false, false);
                    buSelect.add(option);
                });

                // Init Choices.js
                buChoices = new Choices(buSelect, {
                    removeItemButton: true,
                    itemSelectText: '',           // ❌ ไม่แสดง "Press to select"
                    searchEnabled: true,
                    placeholder: true,
                    shouldSort: false
                });

                // ✅ Force clear selection (no auto-select)
                buChoices.setChoiceByValue('');


                // ✅ ถ้าเปลี่ยน BU → ต้องโหลด Project หรือเคลียร์ Project ถ้าไม่มี BU
                buSelect.addEventListener('change', function () {
                    const selectedBU = Array.from(buSelect.selectedOptions).map(opt => opt.value).join(',');

                    if (selectedBU === '') {
                        // 🧹 Hard reset Project dropdown
                        if (projectChoices) {
                            projectChoices.destroy();
                        }

                        const projectSelect = document.getElementById('ddl-project-shop-event');
                        projectSelect.innerHTML = ''; // clear <option> list

                        // ✅ Add default option manually
                        const defaultOption = new Option('เลือกโครงการ', '', true, true);
                        defaultOption.disabled = true;
                        defaultOption.hidden = true;
                        projectSelect.add(defaultOption);


                        // 🔁 Re-init with empty Choices
                        projectChoices = new Choices(projectSelect, {
                            removeItemButton: false,
                            searchEnabled: true,         // ❌ ปิด search
                            itemSelectText: '',           // ❌ ไม่มี "Press to select"
                            placeholder: false,
                            shouldSort: false
                        });

                    } else {
                        loadProjectOptions(selectedBU);
                    }
                });

                if (typeof callback === 'function') callback();
            }
        },
        error: function () {
            console.error("โหลด BU ไม่สำเร็จ");
        },
        complete: function () {
            $('.loader-wrapper').fadeOut();
        }
    });
}

function loadProjectOptions(buIds) {
    /*console.log("🔍 BU ที่เลือก:", buIds);*/

    const projectContainer = document.getElementById('project-dropdown-container');
    const projectSelect = document.getElementById('ddl-project-shop-event');

    // Show loader
    projectContainer.querySelector('#project-loading').style.display = 'flex';

    $.ajax({
        url: baseUrl + 'OtherSettings/GetProjectListByBU',
        type: 'POST',
        dataType: 'json',
        data: { L_BUID: buIds },
        success: function (res) {
            /*console.log("✅ Project Response:", res);*/

            // ✅ Destroy old Choices.js
            if (projectChoices) {
                projectChoices.destroy();
            }

            // ✅ Clear old options
            projectSelect.innerHTML = '';

            // ✅ Add default option manually
            const defaultOption = new Option('เลือกโครงการ', '', true, true); // selected & selectedIndex = 0
            defaultOption.disabled = true;
            defaultOption.hidden = true;
            projectSelect.add(defaultOption);

            // ✅ Add dynamic options
            res.data.forEach(x => {
                const option = new Option(x.ProjectNameTH, x.ProjectID, false, false); // <-- not selected
                projectSelect.add(option);
            });

            // ✅ Re-init Choices.js (no input style)
            projectChoices = new Choices(projectSelect, {
                removeItemButton: false,
                searchEnabled: true,     // ✅ ปิดช่องค้นหาเพื่อให้เหมือน <select> ปกติ
                itemSelectText: '',       // ✅ เอา Press to select ออก
                placeholder: false,       // ✅ ไม่ให้มี placeholder เด้งขึ้น
                shouldSort: false
            });

        },
        error: function (xhr, status, error) {
   /*         console.error("❌ โหลด Project ไม่สำเร็จ:", error);*/
        },
        complete: function () {
            //console.log("✅ โหลด Project เสร็จสมบูรณ์");
            projectContainer.querySelector('#project-loading').style.display = 'none';
        }
    });
}

$(document).ready(function () {
    // ✅ 1. Init Choices เปล่าๆ ให้กับ Project ตอนโหลดหน้าเลย
    const projectShopSelect = document.getElementById('ddl-project-shop-event');
    projectChoices = new Choices(projectShopSelect, {
        removeItemButton: false,
        itemSelectText: '',
        searchEnabled: true,
        placeholder: true,
        shouldSort: false
    });

    // ✅ 2. Generate year options: current year -5 to +5
    const yearSelect = document.getElementById('ddl-year-shop-event');
    const currentYear = new Date().getFullYear();
    for (let i = currentYear - 5; i <= currentYear + 5; i++) {
        const option = document.createElement('option');
        option.value = i;
        option.text = i;
        yearSelect.appendChild(option);
    }

    // ✅ 3. Init Choices ใน Modal สำหรับ multi-select project
    const modalProjectSelect = document.getElementById('ddl-modal-new-event-projects');
    if (modalProjectSelect) {
        new Choices(modalProjectSelect, {
            removeItemButton: true,
            searchEnabled: true,
            placeholder: true,
            placeholderValue: 'เลือก Project ได้หลายรายการ',
            noResultsText: 'ไม่พบรายการ',
        });
    }

    // ✅ 4. โหลด BU และ Partial
    loadBUOptions(() => { });
    loadPartial('Partial_shop_event');
});

function openNewEventModal() {
    
    const modalElement = document.getElementById('modal-new-event');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
}

function getEventFormData() {
    const tagifyRaw = tagifyInstance.value.map(t => ({
        value: t.value,
        label: t.label || t.value
    }));

    const eventName = $('#txt-modal-new-event-name').val().trim();
    const eventType = $('#ddl-modal-new-event-type-id').val().trim();
    /*const eventColor = $('#color-modal-new-event-type-color').val().trim();*/
    const eventLocation = $('#txt-modal-new-event-location').val().trim();
    const projectIds = $('#ddl-modal-new-event-projects').val();
    const start = $('#txt-modal-new-event-start-date-time').val();
    const end = $('#txt-modal-new-event-end-date-time').val();
    const isActive = $('#tg-modal-new-event-status').is(':checked');

    return {
        eventName,
        eventType,
        /*eventColor,*/
        eventLocation,
        tagItems: tagifyRaw,
        projectIds,
        startDateTime: start,
        endDateTime: end,
        isActive
    };
}

$('#modal-new-event').on('show.bs.modal', function () {
    /*loadShopTabDataTest();*/

    // ✅ ซ่อนปุ่ม Tab <li> ทั้ง Shop แบบถูกต้อง (ไม่ใช่ .hide() แต่ใช้ d-none)
    //$('#li-tab-shop').addClass('d-none');
    // ✅ Reset ไปที่ Event tab ทุกครั้งที่เปิด
    $('#modal-Event-add-tab').addClass('active');
    $('#modal-Event-add').addClass('show active');
    // ❌ ล้างสถานะ Shop tab เผื่อเคยเปิดไว้
    $('#modal-Shop-add-tab').removeClass('active');
    $('#modal-Shop-add').removeClass('show active');
});

$(document).on('submit', '.form.theme-form', function (e) {
    e.preventDefault();

    const formData = getEventFormData();

        fetch(baseUrl + 'OtherSettings/InsertNewEventsAndtags', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        })
        .then(res => res.json())
        .then(res => {
            if (res.success) {
                const eventIDs = res.id; // [90,91]
                const eventIDString = eventIDs.join(','); // ✅ ไม่มี , ข้างหน้า

                document.getElementById('hiddenEventID').value = eventIDString;

                Swal.fire({
                    icon: 'success',
                    title: 'สำเร็จ!',
                    text: res.message
                }).then(() => {
                    $('#li-tab-shop').removeClass('d-none'); // แสดง Shop tab
                    const shopTab = new bootstrap.Tab(document.getElementById('modal-Shop-add-tab'));
                    shopTab.show();

                    // ✅ ไม่ต้อง encodeURIComponent เพราะ server รอ string ตรง ",90,91"
                    fetch(baseUrl + 'OtherSettings/GetDataTabShopFromInsert?EventID=' + eventIDString)
                        .then(r => r.json())
                        .then(data => {
                            document.activeElement?.blur();

                            console.log('EventProjects:', data.EventProjects);

                            renderDropdownOptions(
                                'ddl-modal-new-event-project-selected',
                                data.EventProjects,
                                'เลือกโครงการ',
                                function (EventID) {
                                    fetchDataByProject(EventID);
                                }
                            );
                        });
                });
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'แจ้งเตือน',
                    text: res.message
                });
            }
        })
        .catch(err => {
            console.error('❌ Insert Error:', err);
            Swal.fire({
                icon: 'error',
                title: 'เกิดข้อผิดพลาด',
                text: 'ไม่สามารถบันทึกข้อมูลได้ กรุณาลองใหม่อีกครั้ง'
            });
        });
});

function renderDropdownOptions(selectElementId, items, placeholderText = 'เลือกข้อมูล', onChangeCallback = null) {
    const select = document.getElementById(selectElementId);
    if (!select) return;

    // Clear old options
    select.innerHTML = '';

    // Add default option
    const defaultOption = document.createElement('option');
    defaultOption.value = '';
    defaultOption.textContent = placeholderText;
    select.appendChild(defaultOption);

    // Add each item
    items.forEach(item => {
        const option = document.createElement('option');
        option.value = item.ValueInt;
        option.textContent = item.Text ?? '';
        select.appendChild(option);
    });

    // Attach onchange callback if needed
    if (onChangeCallback) {
        select.onchange = () => {
            const selectedProjectID = select.value;
            if (selectedProjectID) {
                onChangeCallback(selectedProjectID);
            }
        };
    }
}

function fetchDataByProject(EventID) {
    fetch(baseUrl + 'OtherSettings/GetDataDateTabShopFromInsert?EventID=' + EventID)
        .then(res => res.json())
        .then(data => {
            console.log('ข้อมูลวันที่ + ร้านค้า:', data);

            if (data.EventDates) {
                renderEventDates(data.EventDates);
            }

            if (data.Shops) {
                renderShops(data.Shops);
            }
        })
        .catch(err => {
            console.error('เกิดข้อผิดพลาดในการโหลดข้อมูล:', err);
        });
}


function renderEventDates(dates) {
    const track = document.getElementById("calendarTrack");
    track.innerHTML = ''; // ล้างของเดิม

    dates.forEach(item => {
        const btn = document.createElement("button");
        btn.className = "calendar-item";
        btn.textContent = item.Text;
        btn.setAttribute("data-value", item.ValueString);
        btn.onclick = () => selectCalendarItem(btn);
        track.appendChild(btn);
    });
}

function renderEventProjects(projects) {
    console.log(projects);
    const container = document.querySelector('#modal-Shop-add .checkbox-checked .card-body');
    container.innerHTML = ''; // ล้างของเดิม

    projects.forEach((proj, i) => {
        const id = `project-check-${i}`;
        container.innerHTML += `
            <div class="form-check checkbox checkbox-primary mb-0">
                <input class="form-check-input" id="${id}" type="checkbox" value="${proj.ValueString}">
                <label class="form-check-label" for="${id}">${proj.Text}</label>
            </div>
        `;
    });
}

function renderShops(shops) {
    console.log(shops);
    const container = document.querySelector('#modal-Shop-add .card-body.pt-3');
    const header = container.querySelector('.shop-item-card'); // เก็บ header ไว้
    container.innerHTML = '';
    container.appendChild(header); // เอา header กลับมา

    shops.forEach((shop, i) => {
        const id = `shop-${i}`;
        container.innerHTML += `
            <div class="shop-item-card p-3 shadow-sm rounded-3 border position-relative" style="display: grid; grid-template-columns: 22px 140px 100px 100px 100px 1fr; gap: 1rem; align-items: center;">
                <div class="form-check m-0">
                    <input class="form-check-input" type="checkbox" id="check-${shop.ValueInt}" />
                </div>

                <div class="fw-semibold fs-6 text-dark">${shop.Text}</div>

                <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota" style="width: 100px;" disabled />
                <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota/Unit" style="width: 100px;" disabled />

                <div class="form-check form-switch ms-3">
                    <input class="form-check-input" type="checkbox" id="switch-${shop.ValueInt}" onchange="toggleQuotaInputs(this)" />
                </div>

                <div class="d-flex gap-2 justify-content-end">
                    <button class="btn btn-sm btn-outline-primary rounded-pill px-3" onclick="editShopRow(this)">
                        <i class="fa fa-edit me-1"></i> แก้ไข
                    </button>
                    <button class="btn btn-sm btn-outline-danger rounded-pill px-3" onclick="deleteShopRow(this)">
                        <i class="fa fa-trash me-1"></i> ลบ
                    </button>
                </div>
            </div>
        `;

    });
}

function loadShopTabDataTest() {
    // ✅ ป้องกัน aria-hidden issue ด้วยการ blur focus ปัจจุบัน
    document.activeElement?.blur();

    fetch(baseUrl + 'OtherSettings/GetDataTabShopFromInsert?EventID=69')
        .then(r => r.json())
        .then(data => {
            renderEventDates(data.EventDates);
            renderEventProjects(data.EventProjects);
            renderShops(data.Shops);
        })
        .catch(err => {
            console.error('❌ Error loading Shop Tab Data:', err);
            Swal.fire({
                icon: 'error',
                title: 'เกิดข้อผิดพลาด',
                text: 'ไม่สามารถโหลดข้อมูลแท็บร้านค้าได้'
            });
        });
}


let currentIndex = 0;
const slideSize = 3;
const itemWidth = 128;
let shopCounter = 0; // global counter

function slideLeft() {
    const track = document.getElementById("calendarTrack");
    currentIndex = Math.max(currentIndex - slideSize, 0);
    updateTransform(track);
}

function slideRight() {
    const track = document.getElementById("calendarTrack");
    const totalItems = track.children.length;
    const maxIndex = totalItems - slideSize;
    currentIndex = Math.min(currentIndex + slideSize, maxIndex);
    updateTransform(track);
}

function updateTransform(track) {
    const x = -currentIndex * itemWidth;
    track.style.transform = `translateX(${x}px)`;
}

// Select only one
function selectCalendarItem(el) {

    document.querySelectorAll('.calendar-item').forEach(item => item.classList.remove('selected'));
    el.classList.add('selected');

    // ✅ ดึงค่าวันที่จาก data-value
    const selectedDate = el.getAttribute("data-value");

    // ✅ อ่านจาก hidden input แทน hardcoded
    const eventId = parseInt(document.getElementById("hiddenEventID")?.value || "0");

    if (!eventId) {
        console.warn("❌ ยังไม่มี EventID ที่บันทึกไว้");
        return;
    }

    // 🔥 เรียก Controller เพื่อโหลด Project + Shops ตามวันที่ใหม่
    fetch(baseUrl + 'OtherSettings/GetDataCreateEventsAndShops?EventID=' + eventId + '&EventDate=' + selectedDate)
        .then(r => r.json())
        .then(data => {
            console.log("🎯 Loaded ShopTab Data:", data);
            const saveFooter = document.getElementById('shop-save-footer');
            if (data.IsHaveData) {
                renderEventProjectsBydate(data.Projects);
                renderShopsBydate(data.Shops);
                if (saveFooter) saveFooter.style.display = 'none';
            }
            else {
                fetch(baseUrl + 'OtherSettings/GetDataTabShopFromInsert?EventID=' + eventId)
                    .then(r => r.json())
                    .then(data => {
                        /*renderEventDates(data.EventDates);*/
                        renderEventProjects(data.EventProjects);
                        renderShops(data.Shops);
                        if (saveFooter) saveFooter.style.display = 'block';
                    })
                    .catch(err => {
                        console.error('❌ Error loading Shop Tab Data:', err);
                        Swal.fire({
                            icon: 'error',
                            title: 'เกิดข้อผิดพลาด',
                            text: 'ไม่สามารถโหลดข้อมูลแท็บร้านค้าได้'
                        });
                    });  
            }
        })
        .catch(err => {
            console.error("❌ Failed to load shop data:", err);
            Swal.fire({
                icon: 'error',
                title: 'โหลดข้อมูลล้มเหลว',
                text: 'ไม่สามารถโหลดข้อมูลร้านค้าได้'
            });
        });
}

function renderEventProjectsBydate(projects) {
    console.log(projects);
    const container = document.querySelector('#modal-Shop-add .checkbox-checked .card-body');
    container.innerHTML = ''; // ล้างของเดิม

    projects.forEach((proj, i) => {
        const id = `project-check-${i}`;
        container.innerHTML += `
            <div class="form-check checkbox checkbox-primary mb-0">
                <input class="form-check-input" id="${id}" type="checkbox" value="${proj.ProjectID}" ${proj.IsUsed ? 'checked' : ''} disabled>
                <label class="form-check-label" for="${id}">${proj.ProjectName}</label>
            </div>
        `;
    });
}

function renderShopsBydate(shops) {
    console.log(shops);
    const container = document.querySelector('#modal-Shop-add .card-body.pt-3');
    const header = container.querySelector('.shop-item-card'); // เก็บ header ไว้
    container.innerHTML = '';
    container.appendChild(header); // เอา header กลับมา

    shops.forEach((shop, i) => {
        const id = `shop-${i}`;
        const isUsed = shop.IsUsed === true;

        container.innerHTML += `
            <div class="shop-item-card p-3 shadow-sm rounded-3 border position-relative" style="display: grid; grid-template-columns: 22px 140px 100px 100px 100px 1fr; gap: 1rem; align-items: center;">
                <div class="form-check m-0">
                    <input class="form-check-input" type="checkbox" id="${shop.ID}" ${isUsed ? 'checked' : ''} disabled/>
                </div>

                <div class="fw-semibold fs-6 text-dark">${shop.Name}</div>

                <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota" style="width: 100px;" value="${shop.ShopQuota ?? 0}" disabled />
                <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota/Unit" style="width: 100px;" value="${shop.UnitQuota ?? 0}" disabled />

                <div class="form-check form-switch ms-3">
                    <input class="form-check-input" type="checkbox" id="switch-${shop.ID}" onchange="toggleQuotaInputs(this)" disabled />
                </div>

                <div class="d-flex gap-2 justify-content-end">
                    <button class="btn btn-sm btn-outline-primary rounded-pill px-3" onclick="editShopRow(this)" disabled>
                        <i class="fa fa-edit me-1"></i> แก้ไข
                    </button>
                    <button class="btn btn-sm btn-outline-danger rounded-pill px-3" onclick="deleteShopRow(this)" disabled>
                        <i class="fa fa-trash me-1"></i> ลบ
                    </button>
                </div>
            </div>
        `;
    });
}

// Select all
function selectAllDays() {
    document.querySelectorAll('.calendar-item').forEach(item => item.classList.add('selected'));
}

function addNewShop() {
    const container = document.querySelector('#modal-Shop-add .card-body.pt-3');
    const headerRow = container.querySelector('.shop-item-card');
    const newRow = document.createElement('div');

    shopCounter++;

    newRow.className = "shop-item-card p-3 shadow-sm rounded-3 border position-relative";
    newRow.style = "display: grid; grid-template-columns: 22px 140px 100px 100px 100px 1fr; gap: 1rem; align-items: center;";

    newRow.innerHTML = `
        <!-- Checkbox -->
        <div class="form-check m-0">
            <input class="form-check-input" type="checkbox" id="check--99" />
        </div>

        <!-- ชื่อร้านค้า (text input) -->
        <input type="text" class="form-control form-control-sm fw-semibold text-dark"
               placeholder="ชื่อร้านค้า"
               style="width: 100%;"
               value="ร้านใหม่ #${shopCounter}" />

        <!-- Quota Inputs -->
        <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota" style="width: 100px;" disabled />
        <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota/Unit" style="width: 100px;" disabled />

        <!-- Switch -->
        <div class="form-check form-switch ms-3">
            <input class="form-check-input" type="checkbox" id="switchUse${shopCounter}" onchange="toggleQuotaInputs(this)" />
        </div>

        <!-- Action Buttons -->
        <div class="d-flex gap-2 justify-content-end">
            <button class="btn btn-sm btn-outline-primary rounded-pill px-3" onclick="editShopRow(this)">
                <i class="fa fa-edit me-1"></i> แก้ไข
            </button>
            <button class="btn btn-sm btn-outline-danger rounded-pill px-3" onclick="deleteShopRow(this)">
                <i class="fa fa-trash me-1"></i> ลบ
            </button>
        </div>
    `;

    container.appendChild(newRow);
}

function deleteShopRow(button) {
    const row = button.closest('.shop-item-card');
    row.remove();
}

function editShopRow(button) {
    const row = button.closest('.shop-item-card');

    // หาชื่อร้านที่เป็น div
    const nameDiv = row.querySelector('.fw-semibold.fs-6.text-dark');

    // ถ้าเจอและยังเป็น div (ไม่ได้ถูกเปลี่ยนเป็น input แล้ว)
    if (nameDiv && nameDiv.tagName === 'DIV') {
        const currentName = nameDiv.innerText.trim();

        // สร้าง input element แทน div
        const input = document.createElement('input');
        input.type = 'text';
        input.className = 'form-control form-control-sm fw-semibold text-dark';
        input.style = 'min-width: 140px; width: 140px;';
        input.value = currentName;

        // แทนที่ div ด้วย input
        nameDiv.replaceWith(input);

        // auto focus
        input.focus();

        // (Optional) กด Enter หรือ blur แล้วกลับเป็น label ก็ได้
        input.addEventListener('blur', () => {
            revertInputToLabel(input);
        });

        input.addEventListener('keydown', (e) => {
            if (e.key === 'Enter') {
                e.preventDefault();
                input.blur();
            }
        });
    }
}

// 🔁 เปลี่ยน input กลับเป็น div หลังจาก blur หรือ enter
function revertInputToLabel(input) {
    const value = input.value.trim() || 'ไม่ระบุชื่อร้าน';
    const div = document.createElement('div');
    div.className = 'fw-semibold fs-6 text-dark';
    div.style = 'min-width: 140px;';
    div.innerText = value;

    input.replaceWith(div);
}

function toggleQuotaInputs(switchInput) {
    const row = switchInput.closest('.shop-item-card');
    const inputs = row.querySelectorAll('.quota-input');

    inputs.forEach(input => {
        input.disabled = !switchInput.checked;
    });
}

function toggleCheckAll(el) {
    const isChecked = el.checked;
    const checkboxes = document.querySelectorAll('.shop-item-card input[type="checkbox"][id^="check-"]');
    checkboxes.forEach(cb => cb.checked = isChecked);
}

function saveShopTab() {
    const selectedDates = [...document.querySelectorAll('#calendarTrack .calendar-item.selected')]
        .map(btn => btn.getAttribute('data-value')) // dd/MM/yyyy
        .filter(x => x);

    const selectedProjects = [...document.querySelectorAll('#modal-Shop-add .checkbox-checked input[type="checkbox"]:checked')]
        .map(cb => cb.value);

    const shopCards = document.querySelectorAll('#modal-Shop-add .card-body.pt-3 .shop-item-card:not(:first-child)');
    const ShopsItems = [];

    shopCards.forEach(card => {
        // 👇 1. หา Checkbox ที่มี id เช่น "shop-123"
        const checkbox = card.querySelector('input.form-check-input[type="checkbox"][id^="check-"]');
        console.log('checkbox : ' + checkbox);
        const idStr = checkbox?.id?.split('-')[1] || "-1"; // shop-123 → "123"
        console.log('idStr : ' + idStr);
        const shopID = parseInt(idStr) || -99;

        // 👇 2. ตรวจว่า checkbox ถูกติ๊กหรือไม่
        const isUsed = checkbox?.checked || false;

        // 👇 3. หา shop name (input หรือ div)
        const isEditMode = card.querySelector('input[type="text"]');
        const name = isEditMode
            ? isEditMode.value.trim()
            : (card.querySelector('.fw-semibold.fs-6.text-dark')?.innerText.trim() || '');

        // 👇 4. quota
        const quotaInputs = card.querySelectorAll('.quota-input');
        const quota = parseInt(quotaInputs[0]?.value) || 0;
        const quotaPerUnit = parseInt(quotaInputs[1]?.value) || 0;

        ShopsItems.push({
            ID: shopID,
            Name: name,
            UnitQuota: quotaPerUnit,
            ShopQuota: quota,
            IsUsed: isUsed
        });
    });

    const model = {
        EventID: parseInt(document.getElementById("hiddenEventID")?.value || "0"),
        ProjectIds: selectedProjects,
        DatesEvent: selectedDates,
        ShopsItems: ShopsItems
    };

    console.log("🧾 Sending Shop Data:", model);

    fetch(baseUrl + 'OtherSettings/InsertNewEventsAndShops', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(model)
    })
        .then(res => res.json())
        .then(res => {
            if (res.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'สำเร็จ!',
                    text: res.message
                });
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'ผิดพลาด!',
                    text: res.message
                });
            }
        })
        .catch(err => {
            console.error("❌ Error saving shop data:", err);
            Swal.fire({
                icon: 'error',
                title: 'ผิดพลาด!',
                text: 'ไม่สามารถบันทึกข้อมูลร้านค้าได้'
            });
        });
}


function updateColorByEventType() {
    const ddl = document.getElementById('ddl-modal-new-event-type-id');
    const colorInput = document.getElementById('color-modal-new-event-type-color');

    const selectedIndex = ddl.selectedIndex;
    if (selectedIndex === 0) {
        // First item (placeholder)
        colorInput.value = '#808080'; // grey
        return;
    }

    const selectedOption = ddl.options[selectedIndex];
    const color = selectedOption.getAttribute('data-color') || '#808080';
    colorInput.value = color;
}


