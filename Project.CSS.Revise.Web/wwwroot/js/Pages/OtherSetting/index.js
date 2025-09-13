let buChoices = null;
let projectChoices = null;

let buChoices_Counter = null;
let projectChoices_Counter = null;

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
                        const defaultOption = new Option('เลือกโครงการ', '', false, false);
                        defaultOption.disabled = true;
                        defaultOption.hidden = true;
                        projectSelect.add(defaultOption);


                        // 🔁 Re-init with empty Choices
                        projectChoices = new Choices(projectSelect, {
                            removeItemButton: true,
                            searchEnabled: true,
                            placeholderValue: 'เลือกโครงการ',     // ✅ เพิ่ม placeholder ตรงนี้
                            noResultsText: 'ไม่พบโครงการ',
                            noChoicesText: 'ไม่มีตัวเลือก',
                            itemSelectText: '',
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

function loadBUCounterOptions(callback) {
    $('.loader-wrapper').show();

    $.ajax({
        url: baseUrl + 'OtherSettings/Page_Load',
        type: 'POST',
        dataType: 'json',
        success: function (res) {
            if (res.success && res.buList?.length) {
                const buSelect = document.getElementById('ddl_BUG_counter');

                // Reset old Choices
                if (buChoices_Counter) {
                    buChoices_Counter.destroy();
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
                buChoices_Counter = new Choices(buSelect, {
                    removeItemButton: true,
                    itemSelectText: '',           // ❌ ไม่แสดง "Press to select"
                    searchEnabled: true,
                    placeholder: true,
                    shouldSort: false
                });

                // ✅ Force clear selection (no auto-select)
                buChoices_Counter.setChoiceByValue('');


                // ✅ ถ้าเปลี่ยน BU → ต้องโหลด Project หรือเคลียร์ Project ถ้าไม่มี BU
                buSelect.addEventListener('change', function () {
                    const selectedBU = Array.from(buSelect.selectedOptions).map(opt => opt.value).join(',');

                    if (selectedBU === '') {
                        // 🧹 Hard reset Project dropdown
                        if (projectChoices_Counter) {
                            projectChoices_Counter.destroy();
                        }

                        const projectSelect = document.getElementById('ddl_PROJECT_counter');
                        projectSelect.innerHTML = ''; // clear <option> list

                        // ✅ Add default option manually
                        const defaultOption = new Option('เลือกโครงการ', '', false, false);
                        defaultOption.disabled = true;
                        defaultOption.hidden = true;
                        projectSelect.add(defaultOption);


                        // 🔁 Re-init with empty Choices
                        projectChoices_Counter = new Choices(projectSelect, {
                            removeItemButton: true,
                            searchEnabled: true,
                            placeholderValue: 'เลือกโครงการ',     // ✅ เพิ่ม placeholder ตรงนี้
                            noResultsText: 'ไม่พบโครงการ',
                            noChoicesText: 'ไม่มีตัวเลือก',
                            itemSelectText: '',
                            placeholder: false,
                            shouldSort: false
                        });

                    } else {
                        loadProjectCounterOptions(selectedBU);
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
    const projectContainer = document.getElementById('project-dropdown-container');
    const projectSelect = document.getElementById('ddl-project-shop-event');
    const overlay = document.getElementById('project_loading');

    // show overlay + shimmer
    overlay.style.display = 'flex';
    projectContainer.classList.add('loading');

    $.ajax({
        url: baseUrl + 'OtherSettings/GetProjectListByBU',
        type: 'POST',
        dataType: 'json',
        data: { L_BUID: buIds },
        success: function (res) {
            if (projectChoices) projectChoices.destroy();

            const items = (res && res.success && Array.isArray(res.data)) ? res.data : [];

            // reset options + placeholder
            projectSelect.innerHTML = '';
            const def = new Option('เลือกโครงการ', '', false, false);
            def.disabled = true; def.hidden = true;
            projectSelect.add(def);

            // add options
            items.forEach(x => {
                projectSelect.add(new Option(x.ProjectNameTH, x.ProjectID, false, false));
            });

            // re-init Choices
            projectChoices = new Choices(projectSelect, {
                removeItemButton: true,
                searchEnabled: true,
                placeholderValue: 'เลือกโครงการ',
                noResultsText: 'ไม่พบโครงการ',
                noChoicesText: 'ไม่มีตัวเลือก',
                itemSelectText: '',
                shouldSort: false
            });
        },
        error: function (xhr, status, error) {
            console.error('❌ โหลด Project ไม่สำเร็จ:', error);
            projectSelect.innerHTML = '';
            const def = new Option('เลือกโครงการ', '', false, false);
            def.disabled = true; def.hidden = true;
            projectSelect.add(def);
            projectChoices = new Choices(projectSelect, {
                removeItemButton: true,
                searchEnabled: true,
                placeholderValue: 'เลือกโครงการ',
                noResultsText: 'ไม่พบโครงการ',
                noChoicesText: 'ไม่มีตัวเลือก',
                itemSelectText: '',
                shouldSort: false
            });
        },
        complete: function () {
            // hide overlay + shimmer
            overlay.style.display = 'none';
            projectContainer.classList.remove('loading');
        }
    });
}

function loadProjectCounterOptions(buIds) {
    const projectContainer = document.getElementById('project_dropdown_container_counter');
    const projectSelect = document.getElementById('ddl_PROJECT_counter');
    const overlay = document.getElementById('project_loading_counter');

    // ✅ แสดง overlay + ใส่ state "loading" ให้ container
    overlay.style.display = 'flex';
    projectContainer.classList.add('loading');

    $.ajax({
        url: baseUrl + 'OtherSettings/GetProjectListByBU',
        type: 'POST',
        dataType: 'json',
        data: { L_BUID: buIds },
        success: function (res) {
            if (projectChoices_Counter) projectChoices_Counter.destroy();

            // ปลอดภัยก่อนใช้ res.data
            const items = (res && res.success && Array.isArray(res.data)) ? res.data : [];

            // เคลียร์ + ใส่ placeholder ใหม่
            projectSelect.innerHTML = '';
            const def = new Option('เลือกโครงการ', '', false, false);
            def.disabled = true; def.hidden = true;
            projectSelect.add(def);

            // เติมรายการ
            items.forEach(x => {
                projectSelect.add(new Option(x.ProjectNameTH, x.ProjectID, false, false));
            });

            // Re-init Choices
            projectChoices_Counter = new Choices(projectSelect, {
                removeItemButton: true,
                searchEnabled: true,
                placeholderValue: 'เลือกโครงการ',
                noResultsText: 'ไม่พบโครงการ',
                noChoicesText: 'ไม่มีตัวเลือก',
                itemSelectText: '',
                shouldSort: false
            });
        },
        error: function (xhr, status, error) {
            console.error("❌ โหลด Project ไม่สำเร็จ:", error);
        },
        complete: function () {
            // ✅ ซ่อน overlay + เอา state loading ออก
            overlay.style.display = 'none';
            projectContainer.classList.remove('loading');
        }
    });
}

$(document).ready(function () {
    // ✅ 1. Init Choices เปล่าๆ ให้กับ Project ตอนโหลดหน้าเลย
    const projectShopSelect = document.getElementById('ddl-project-shop-event');
    projectChoices = new Choices(projectShopSelect, {
        removeItemButton: true,   
        searchEnabled: true,
        placeholderValue: 'เลือกโครงการ',     // ✅ เพิ่ม placeholder ตรงนี้
        noResultsText: 'ไม่พบโครงการ',
        noChoicesText: 'ไม่มีตัวเลือก',
        shouldSort: false
    });

    const projectcounterSelect = document.getElementById('ddl_PROJECT_counter');
    projectChoices_Counter = new Choices(projectcounterSelect, {
        removeItemButton: true,
        searchEnabled: true,
        placeholderValue: 'เลือกโครงการ',     // ✅ เพิ่ม placeholder ตรงนี้
        noResultsText: 'ไม่พบโครงการ',
        noChoicesText: 'ไม่มีตัวเลือก',
        shouldSort: false
    });

    const ddlcountertype = document.getElementById('ddl_counter_type');
    if (ddlcountertype) {
        new Choices(ddlcountertype, {
            removeItemButton: true,
            searchEnabled: true,
            placeholderValue: 'ทั้งหมด',     // ✅ เพิ่ม placeholder ตรงนี้
            noResultsText: 'ไม่พบประเภทจุดให้บริการ',
            noChoicesText: 'ไม่มีตัวเลือก',
            shouldSort: false
        });
    }


    // ✅ 2. Generate year options: current year -5 to +5
    const yearSelect = document.getElementById('ddl-year-shop-event');
    const currentYear = new Date().getFullYear();

    // ลูปสร้างปี -3 ถึง +3
    for (let i = currentYear - 3; i <= currentYear + 3; i++) {
        const option = document.createElement('option');
        option.value = i;
        option.text = i;

        // ✅ ทำให้ปีปัจจุบันถูกเลือก
        if (i === currentYear) {
            option.selected = true;
        }

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
    loadBUCounterOptions(() => { });
    loadPartial('Partial_shop_event');
    LoadPartialshopevent();
    initProjectstatusDropdown();

});

function initProjectstatusDropdown() {
    choicesBug = new Choices('#ddl_project_status', {
        removeItemButton: true,
        placeholderValue: 'เลือกสถานะโครงการได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });

    document.getElementById('ddl_project_status').addEventListener('change', onBuChanged);
}

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

    const eventName = $('#ddl-modal-new-event-type-id option:selected').text().trim(); // ✅ แก้ตรงนี้
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
    $('#btn-cancel-new-event').click();
    // ✅ ซ่อนปุ่ม Tab <li> ทั้ง Shop แบบถูกต้อง (ไม่ใช่ .hide() แต่ใช้ d-none)
    $('#li-tab-shop').addClass('d-none');
    const saveBtn = document.getElementById('btn-new-save-event');
    const cancelBtn = document.getElementById('btn-cancel-new-event');
    saveBtn.disabled = false;
    cancelBtn.disabled = false;
    // ✅ Reset ไปที่ Event tab ทุกครั้งที่เปิด
    $('#modal-Event-add-tab').addClass('active');
    $('#modal-Event-add').addClass('show active');
    // ❌ ล้างสถานะ Shop tab เผื่อเคยเปิดไว้
    $('#modal-Shop-add-tab').removeClass('active');
    $('#modal-Shop-add').removeClass('show active');
});

$('#form-new-event').on('submit', function (e) {
    e.preventDefault();

    const formData = getEventFormData();

    showLoading();

    fetch(baseUrl + 'OtherSettings/InsertNewEventsAndtags', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
        .then(res => res.json())
        .then(res => {
            hideLoading();
            if (res.success) {
                const eventIDs = res.id; // [90,91]
                const eventIDString = eventIDs.join(',');

                document.getElementById('hiddenEventID').value = eventIDString;

                Swal.fire({
                    icon: 'success',
                    title: 'สำเร็จ!',
                    text: res.message
                }).then(() => {
                    $('#li-tab-shop').removeClass('d-none');
                    const shopTab = new bootstrap.Tab(document.getElementById('modal-Shop-add-tab'));
                    shopTab.show();

                    fetch(baseUrl + 'OtherSettings/GetDataTabShopFromInsert?EventID=' + eventIDString)
                        .then(r => r.json())
                        .then(data => {
                            document.activeElement?.blur();

                            console.log('EventProjects:', data.EventProjects);
                            const saveBtn = document.getElementById('btn-new-save-event');
                            const cancelBtn = document.getElementById('btn-cancel-new-event');
                            saveBtn.disabled = true;
                            cancelBtn.disabled = true;
                            renderDropdownOptions(
                                'ddl-modal-new-event-project-selected',
                                data.EventProjects,
                                'เลือกโครงการ',
                                function (EventID) {
                                    console.log("Selected ProjectID:", EventID);
                                    fetchDataByProject(EventID);    
                                    LoadPartialshopevent();
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
            hideLoading();
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
    const hiddenInput = document.getElementById("hiddenProjectIDinSelected");

    if (!select) return;

    // Clear old options
    select.innerHTML = '';

    // Add default option
    const defaultOption = document.createElement('option');
    defaultOption.value = '';
    defaultOption.textContent = placeholderText;
    select.appendChild(defaultOption);

    // Map for lookup later (EventID -> ProjectID)
    const eventToProjectMap = {};

    // Add each item
    items.forEach((item, index) => {
        const option = document.createElement('option');
        option.value = item.ValueInt;
        option.textContent = item.Text ?? '';
        select.appendChild(option);

        // Map: EventID → ProjectID
        eventToProjectMap[item.ValueInt] = item.ValueString;
    });

    // Attach onchange callback
    if (onChangeCallback) {
        select.onchange = () => {
            const EventID = select.value;
            const ProjectID = eventToProjectMap[EventID];

            // ✅ อัปเดต hidden
            if (hiddenInput) {
                hiddenInput.value = ProjectID ?? '';
            }

            if (EventID) {
                onChangeCallback(EventID);
            }
        };
    }

    // ✅ Auto select first item if exists
    if (items.length > 0) {
        const firstValue = items[0].ValueInt;
        select.value = firstValue;

        // ✅ อัปเดต hidden ด้วย
        if (hiddenInput) {
            hiddenInput.value = items[0].ValueString ?? '';
        }

        // ✅ Trigger callback manually
        if (onChangeCallback) {
            onChangeCallback(firstValue);
        }
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

    let firstButton = null;

    dates.forEach((item, index) => {
        const btn = document.createElement("button");
        btn.className = "calendar-item";
        btn.textContent = item.Text;
        btn.setAttribute("data-value", item.ValueString);
        btn.onclick = () => selectCalendarItem(btn);
        track.appendChild(btn);

        // เก็บปุ่มแรกไว้
        if (index === 0) {
            firstButton = btn;
        }
    });

    // ✅ เรียก selectCalendarItem กับปุ่มแรกทันที
    if (firstButton) {
        selectCalendarItem(firstButton);
    }
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

    // ลบ class selected ออกจากทั้งหมดก่อน
    document.querySelectorAll('.calendar-item').forEach(item => item.classList.remove('selected'));
    el.classList.add('selected');

    // ✅ ดึงค่าวันที่จากปุ่ม
    const selectedDate = el.getAttribute("data-value");

    // ✅ ดึง EventID จาก dropdown ที่เลือกอยู่ตอนนี้
    const eventId = document.getElementById("ddl-modal-new-event-project-selected")?.value || "";

    // ✅ ดึง ProjectID จาก hidden input
    const projectId = document.getElementById("hiddenProjectIDinSelected")?.value || "";

    // ✅ Log ทั้งหมดเพื่อตรวจสอบ
    console.log("Selected Date:", selectedDate);
    console.log("Selected EventID:", eventId);
    console.log("Selected ProjectID (from hidden):", projectId);

    if (!eventId) {
        console.warn("❌ ยังไม่มี EventID ที่บันทึกไว้");
        return;
    }

    //🔥 เรียก Controller เพื่อโหลด Project + Shops ตามวันที่ใหม่
    fetch(
        baseUrl + 'OtherSettings/GetDataCreateEventsAndShops' +
        '?EventID=' + eventId +
        '&EventDate=' + encodeURIComponent(selectedDate) +
        '&ProjectID=' + encodeURIComponent(projectId)
    )
    .then(r => r.json())
    .then(data => {

        if (data && data.length > 0) {
            renderShopsBydate(data);
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
            <div class="shop-item-card p-3 shadow-sm rounded-3 border position-relative";>
                <div class="form-check m-0">
                    <input class="form-check-input" type="checkbox" id="check-${shop.ID}" ${isUsed ? 'checked' : ''}/>
                </div>

                <div class="fw-semibold fs-6 text-dark">${shop.Name}</div>

                <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota" style="width: 100px;" value="${shop.ShopQuota ?? 0}" disabled />
                <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota/Unit" style="width: 100px;" value="${shop.UnitQuota ?? 0}" disabled />

                <div class="form-check form-switch ms-3">
                    <input class="form-check-input" type="checkbox" id="switch-${shop.ID}" onchange="toggleQuotaInputs(this)"/>
                </div>

                <div class="d-flex gap-2 justify-content-end">
                </div>
            </div>
        `;
    });
}

// Select all
//function selectAllDays() {
//    document.querySelectorAll('.calendar-item').forEach(item => item.classList.add('selected'));
//}

function addNewShop() {
    const container = document.querySelector('#modal-Shop-add .card-body.pt-3');
    const headerRow = container.querySelector('.shop-item-card');
    const newRow = document.createElement('div');

    shopCounter++;

    newRow.className = "shop-item-card p-3 shadow-sm rounded-3 border position-relative";
    newRow.style = "display: grid; grid-template-columns: 32px 1.5fr 1fr 1fr 1fr 1.2fr; gap: 1.2rem; align-items: center; width: 100%;";

    newRow.innerHTML = `
        <!-- Checkbox -->
        <div class="form-check m-0">
            <input class="form-check-input" type="checkbox" id="check--99" checked/>
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
    // ✅ 1. เก็บวันที่ที่เลือกเป็น array เสมอ
    const selectedDates = [...document.querySelectorAll('#calendarTrack .calendar-item.selected')]
        .map(btn => btn.getAttribute('data-value'))
        .filter(x => x); // เก็บเฉพาะวันที่ที่ไม่ว่าง

    // ✅ 2. เก็บ ProjectID จาก hidden แล้วแปลงเป็น array (แม้จะมีแค่ค่าเดียว)
    const projectId = document.getElementById("hiddenProjectIDinSelected")?.value || "";
    const selectedProjects = projectId ? [projectId] : [];

    // ✅ 3. เก็บข้อมูลร้านค้าทั้งหมด
    const shopCards = document.querySelectorAll('#modal-Shop-add .card-body.pt-3 .shop-item-card:not(:first-child)');
    const ShopsItems = [];

    shopCards.forEach(card => {
        const checkbox = card.querySelector('input.form-check-input[type="checkbox"][id^="check-"]');
        const idStr = checkbox?.id?.split('-')[1] || "-1";
        const shopID = parseInt(idStr) || -99;
        const isUsed = checkbox?.checked || false;

        const isEditMode = card.querySelector('input[type="text"]');
        const name = isEditMode
            ? isEditMode.value.trim()
            : (card.querySelector('.fw-semibold.fs-6.text-dark')?.innerText.trim() || '');

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
        EventID: document.getElementById("ddl-modal-new-event-project-selected").value,
        ProjectIds: selectedProjects,   // ✅ ส่งเป็น array เสมอ
        DatesEvent: selectedDates,      // ✅ ส่งเป็น array เสมอ
        ShopsItems: ShopsItems
    };

    /*console.log("🧾 Sending Shop Data:", model);*/
    showLoading();

    fetch(baseUrl + 'OtherSettings/InsertNewEventsAndShops', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(model)
    })
        .then(res => res.json())
        .then(res => {
            hideLoading();
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
            hideLoading();
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

document.getElementById('save-edit-event').addEventListener('click', function (e) {
    e.preventDefault();

    const eventID = document.getElementById('hiddenEditEventID')?.value;
    const projectID = document.getElementById('hiddenEditProjectID')?.value;
    const selectedDateBtn = document.querySelector('#calendarTrackEditinProject .calendar-item.selected');
    const selectedDate = selectedDateBtn?.getAttribute('data-value');

    if (!eventID || !projectID || !selectedDate) {
        Swal.fire({
            icon: 'warning',
            title: 'แจ้งเตือน',
            text: 'กรุณาระบุข้อมูลให้ครบก่อนบันทึก'
        });
        return;
    }

    const rows = document.querySelectorAll('#modal-edit-event-in-project .shop-item-card');
    const shopItems = [];

    rows.forEach((row, index) => {
        if (index === 0) return;

        const checkbox = row.querySelector('input[type="checkbox"]');
        const isUsed = checkbox?.checked || false;

        const idStr = checkbox?.id?.split('-')[1] || "-1";
        const shopID = parseInt(idStr) || -99;

        const shopQuotaInput = row.querySelector('input[placeholder="Quota"]');
        const unitQuotaInput = row.querySelector('input[placeholder="Quota/Unit"]');

        // ✅ รองรับทั้ง input และ div
        const nameInputOrDiv = row.querySelector('input[type="text"], div.fw-semibold.fs-6.text-dark');
        const name = nameInputOrDiv?.value?.trim() || nameInputOrDiv?.innerText?.trim();

        const unitQuota = parseInt(unitQuotaInput?.value) || 0;
        const shopQuota = parseInt(shopQuotaInput?.value) || 0;

        if (name) {
            shopItems.push({
                ID: shopID,
                Name: name,
                UnitQuota: unitQuota,
                ShopQuota: shopQuota,
                IsUsed: isUsed
            });
        }
    });



    const model = {
        EventID: parseInt(eventID),
        ProjectIds: [projectID],
        DatesEvent: [selectedDate],
        ShopsItems: shopItems
    };

    showLoading();

    fetch(`${baseUrl}OtherSettings/InsertNewEventsAndShops`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(model)
    })
        .then(res => res.json())
        .then(res => {
            hideLoading();
            if (res.success === 1) {
                Swal.fire({
                    icon: 'success',
                    title: 'บันทึกสำเร็จ',
                    text: res.message
                })
            } else {
                Swal.fire({
                    icon: 'warning',
                    title: 'เกิดข้อผิดพลาด',
                    text: res.message || 'ไม่สามารถบันทึกข้อมูลได้'
                });
            }
        })
        .catch(err => {
            hideLoading();
            console.error('❌ Save Edit Error:', err);
            Swal.fire({
                icon: 'error',
                title: 'ผิดพลาด!',
                text: 'เกิดข้อผิดพลาดระหว่างบันทึก กรุณาลองใหม่'
            });
        });
});

function UpdateDateTimeEvent() {
    const eventID = $('#hiddenEditEventID').val();
    const projectID = $('#hiddenEditProjectID').val();
    const startDate = $('#txt-modal-edit-event-start-date-time').val();
    const endDate = $('#txt-modal-edit-event-end-date-time').val();

    if (!eventID || !projectID || !startDate || !endDate) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Information',
            text: 'Please fill in all required fields before saving.'
        });
        return;
    }

    const formData = new FormData();
    formData.append('EventID', eventID);
    formData.append('ProjectID', projectID);
    formData.append('StartDate', startDate);
    formData.append('EndDate', endDate);

    showLoading();

    fetch(baseUrl + 'OtherSettings/UpdateDateTimeEvent', {
        method: 'POST',
        body: formData
    })
        .then(res => res.json())
        .then(response => {
            hideLoading();
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: response.message
                });
                openEditEventProjectModal(eventID, projectID);
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Update Failed',
                    text: response.message
                });
            }
        })
        .catch(error => {
            hideLoading();
            console.error('Error:', error);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'An unexpected error occurred.'
            });
        });
}

function deleteEventInProject() {
    const eventID = document.getElementById('hiddenEditEventID').value;
    const projectID = document.getElementById('hiddenEditProjectID').value;

    if (!eventID || !projectID) {
        Swal.fire('Warning', 'Event or Project information not found!', 'warning');
        return;
    }

    Swal.fire({
        title: 'Are you sure you want to delete this event?',
        text: 'This action cannot be undone.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            showLoading();
            const formData = new FormData();
            formData.append('EventID', eventID);
            formData.append('ProjectID', projectID);

            fetch(baseUrl + 'OtherSettings/InActiveEvent', {
                method: 'POST',
                body: formData
            })
                .then(res => res.json())
                .then(data => {
                    hideLoading();
                    if (data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Success',
                            text: data.message
                        });
                        // Optionally refresh calendar or modal
                        LoadPartialshopevent();
                        /*goToCalendarMonth(MonthSeletedBTN);*/
                        $('#modal-edit-event-in-project').modal('hide');
                    } else {
                        Swal.fire('เกิดข้อผิดพลาด', data.message, 'error');
                    }
                })
                .catch(err => {
                    hideLoading();
                    console.error('❌ Delete Error:', err);
                    Swal.fire('เกิดข้อผิดพลาด', 'ไม่สามารถลบกิจกรรมได้', 'error');
                });
        }
    });
}



