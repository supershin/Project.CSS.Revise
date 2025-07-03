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
    const eventType = $('#txt-modal-new-event-type-name').val().trim();
    const eventColor = $('#color-modal-new-event-type-color').val().trim();
    const eventLocation = $('#txt-modal-new-event-location').val().trim();
    const projectIds = $('#ddl-modal-new-event-projects').val();
    const start = $('#txt-modal-new-event-start-date-time').val();
    const end = $('#txt-modal-new-event-end-date-time').val();
    const isActive = $('#tg-modal-new-event-status').is(':checked');

    return {
        eventName,
        eventType,
        eventColor,
        eventLocation,
        tagItems: tagifyRaw,
        projectIds,
        startDateTime: start,
        endDateTime: end,
        isActive
    };
}

//$('#modal-new-event').on('show.bs.modal', function () {
//    // ✅ ซ่อนปุ่ม Tab <li> ทั้ง Shop แบบถูกต้อง (ไม่ใช่ .hide() แต่ใช้ d-none)
//    $('#li-tab-shop').addClass('d-none');

//    // ✅ Reset ไปที่ Event tab ทุกครั้งที่เปิด
//    $('#modal-Event-add-tab').addClass('active');
//    $('#modal-Event-add').addClass('show active');

//    // ❌ ล้างสถานะ Shop tab เผื่อเคยเปิดไว้
//    $('#modal-Shop-add-tab').removeClass('active');
//    $('#modal-Shop-add').removeClass('show active');
//});

$(document).on('submit', '.form.theme-form', function (e) {
    e.preventDefault(); // ❌ ป้องกัน form reload หน้า

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
                Swal.fire({
                    icon: 'success',
                    title: 'สำเร็จ!',
                    text: res.message
                }).then(() => {
                    // ✅ ปิด modal + รีเฟรชตาราง
                    $('#li-tab-shop').removeClass('d-none'); // แสดง Shop tab
                    const shopTab = new bootstrap.Tab(document.getElementById('modal-Shop-add-tab'));
                    shopTab.show(); // auto switch ไป Shop tab
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
