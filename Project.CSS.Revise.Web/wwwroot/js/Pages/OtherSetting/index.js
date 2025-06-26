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
                const defaultOption = new Option('เลือก BU', '', true, false);
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
                    searchEnabled: true,
                    placeholder: true,
                    shouldSort: false
                });

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
                        const defaultOption = new Option('เลือกโครงการ', '', false, true);
                        defaultOption.disabled = true;
                        defaultOption.hidden = true;
                        projectSelect.add(defaultOption);

                        console.log("เลือกโครงการ:")

                        // 🔁 Re-init with empty Choices
                        projectChoices = new Choices(projectSelect, {
                            removeItemButton: false,
                            searchEnabled: true,
                            placeholder: true,
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

            // Destroy old choices
            if (projectChoices) {
                projectChoices.destroy();
            }

            // Clear old options
            projectSelect.innerHTML = '';

            // ✅ Add default option: เลือกโครงการ
            const defaultOption = new Option('เลือกโครงการ', '', false, false);
            defaultOption.disabled = true;
            defaultOption.hidden = true;
            projectSelect.add(defaultOption);

            // ✅ Add dynamic options
            res.data.forEach(x => {
                const option = new Option(x.ProjectNameTH, x.ProjectID, true, false);
                projectSelect.add(option);
            });

            // ✅ Re-init Choices.js (do not auto select)
            projectChoices = new Choices(projectSelect, {
                removeItemButton: false,
                searchEnabled: true,
                placeholder: true,
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
function openNewEventModal() {
    const modal = new bootstrap.Modal(document.getElementById('modal-new-event'));
    modal.show();
}

$(document).ready(function () {
    // ✅ 1. Init Choices เปล่าๆ ให้กับ Project ตอนโหลดหน้าเลย
    const projectSelect = document.getElementById('ddl-project-shop-event');
    projectChoices = new Choices(projectSelect, {
        removeItemButton: false,
        searchEnabled: true,
        placeholder: true,
        shouldSort: false
    });

    loadBUOptions(() => {
    });

    $('#dateRange').daterangepicker({
        autoUpdateInput: false,
        locale: {
            format: 'DD/MM/YYYY',
            cancelLabel: 'Clear',
            applyLabel: 'Apply',
            daysOfWeek: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
            monthNames: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
            firstDay: 1
        }
    });

    $('#dateRange').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('DD/MM/YYYY') + ' - ' + picker.endDate.format('DD/MM/YYYY'));
    });

    $('#dateRange').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });

    loadPartial('Partial_shop_event');
});

$(document).on('click', '.month-btn', function () {
    const month = $(this).data('month');
    const year = new Date().getFullYear();
    const date = `${year}-${String(month).padStart(2, '0')}-01`;

    // FullCalendar jump to month
    const calendarApi = $('#calendar').fullCalendar ? $('#calendar') : null;
    if (calendarApi) {
        calendarApi.fullCalendar('gotoDate', date);
    }

    // Highlight active
    $('.month-btn').removeClass('active');
    $(this).addClass('active');
});
