function loadPartial(viewName) {
    $.ajax({
        url: baseUrl + 'OtherSettings/LoadPartial', // ✅ สร้าง Action นี้ใน Controller
        type: 'GET',
        dataType: 'html',
        data: { viewName: viewName },
        success: function (html) {
            $('#Box_show_partial').html(html);
        },
        error: function (xhr) {
            console.error('❌ Error loading partial:', xhr);
            $('#Box_show_partial').html('<div class="alert alert-danger">โหลดข้อมูลไม่สำเร็จ</div>');
        }
    });
}

let calendarInstance = null;
let firstDateValue = null;
let MonthSeletedBTN = null;

function LoadPartialshopevent() {

    const Buid = $('#ddl-bu-shop-event').val();
    const projectIdList = $('#ddl-project-shop-event').val() || [];
    const projectId = projectIdList.join(',');

    const year = $('#ddl-year-shop-event').val();
    const $calendar = $('#calendar');

    if ($calendar.length) {
        $calendar.html(`
            <div class="text-center p-3">
                <div class="spinner-border text-primary" role="status"></div>
                <div>กำลังโหลดปฏิทิน...</div>
            </div>
        `);
    }

    $.getJSON(baseUrl + 'OtherSettings/GetEventsForCalendar', {
        Buid: Buid,
        projectID: projectId,
        year: year,
        month: '' // ส่งค่าว่างเพื่อดึงทั้งปี
    })
        .done(function (eventList) {

            initFullCalendarWithEvents(eventList, function () {
                updateMonthBadgesFromEventList(eventList);

                // ✅ เพิ่มตรงนี้ให้เลื่อนไปยังเดือนที่เคยเลือก
                if (MonthSeletedBTN) {
                    goToCalendarMonth(MonthSeletedBTN);
                }
            });

            renderEventSummaryBox(eventList);

        })
        .fail(function (xhr) {
            console.error('❌ โหลด Event ไม่สำเร็จ:', xhr);
            $calendar.html('<div class="alert alert-danger">โหลดปฏิทินไม่สำเร็จ</div>');
        });
}


//function LoadPartialshopevent(monthOverride = '') {
//    const projectIdList = $('#ddl-project-shop-event').val() || [];
//    const projectId = projectIdList.join(',');

//    const year = $('#ddl-year-shop-event').val();
//    const month = monthOverride || '';

//    const $calendar = $('#calendar');

//    if ($calendar.length) {
//        $calendar.html(`
//            <div class="text-center p-3">
//                <div class="spinner-border text-primary" role="status"></div>
//                <div>กำลังโหลดปฏิทิน...</div>
//            </div>
//        `);
//    }

//    $.getJSON(baseUrl + 'OtherSettings/GetEventsForCalendar', {
//        projectID: projectId,
//        year: year,
//        month: month
//    })
//    .done(function (eventList) {

//        // 1️⃣  FullCalendar
//        initFullCalendarWithEvents(eventList, function () {
//            updateMonthBadgesFromEventList(eventList);
//        }, monthOverride);

//        // 2️⃣  Sidebar refresh
//        renderEventSummaryBox(eventList);

//    })
//    .fail(function (xhr) {
//        console.error('❌ โหลด Event ไม่สำเร็จ:', xhr);
//        $calendar.html('<div class="alert alert-danger">โหลดปฏิทินไม่สำเร็จ</div>');
//    });


//}

function initFullCalendarWithEvents(eventsRaw, onComplete, monthOverride = '') {
    const selectedYear = $('#ddl-year-shop-event').val();
    const selectedMonth = monthOverride && monthOverride !== '' ? parseInt(monthOverride) : 1;
    const initialDate = new Date(`${selectedYear}-${String(selectedMonth).padStart(2, '0')}-01`);

    // ✅ เตรียมแปลง event สำหรับ FullCalendar
    const events = eventsRaw.map(ev => {
        const startISO = parseToISO(ev.start);
        const endISO = parseToISO(ev.end); // ❌ ไม่ต้องเพิ่มวัน

        return {
            title: ev.title,
            start: startISO,
            end: endISO,
            color: ev.color,
            extendedProps: {
                modaltype: ev.modaltype,
                originalData: ev
            }
        };
    });



    const calendarEl = document.getElementById('calendar');
    calendarEl.innerHTML = '';

    if (calendarInstance) {
        calendarInstance.destroy();
        calendarInstance = null;
    }

    calendarInstance = new FullCalendar.Calendar(calendarEl, {
        locale: 'th',
        dayHeaderFormat: { weekday: 'short' },
        titleFormat: { year: 'numeric', month: 'long' },
        initialView: 'dayGridMonth',
        initialDate: initialDate,
        aspectRatio: 1.5,
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        events: events,
        displayEventTime: false,
        fixedWeekCount: true,
        contentHeight: 850,  // ✅ ให้ปรับอัตโนมัติ หรือใช้ contentHeight: 700
        dayMaxEvents: 3,     // จำกัด 3 event ต่อวัน, ที่เหลือจะโชว์ "+2 more"
        views: {
            timeGridWeek: {
                dayHeaderFormat: { day: 'numeric', weekday: 'short' }  // 👉 1 จ., 2 อ.
            },
            timeGridDay: {
                dayHeaderFormat: { day: 'numeric', weekday: 'short' }
            }
        },
        eventClick: function (info) {
            const modaltype = info.event.extendedProps.modaltype;
            if (modaltype === 1) {
                EditEventProjectModal(info.event.extendedProps.originalData);
            } else if (modaltype === 2) {
                EditEventModal(info.event.extendedProps.originalData);
            }
        },
        eventDidMount: function (info) {
            const el = info.el;
            el.style.cursor = 'pointer';
            el.style.borderRadius = '6px';
            el.style.padding = '2px 6px';
            el.style.fontWeight = 'bold';
            el.style.backgroundColor = info.event.backgroundColor || '#3498db';
            el.style.textShadow = '0 0 2px rgba(255,255,255,0.6)';
        },

        datesSet: function (info) {
            const currentDate = calendarInstance.getDate();
            const month = currentDate.getMonth() + 1;

            $('.month-btn').removeClass('active');
            $(`.month-btn[data-month="${month}"]`).addClass('active');

            updateMonthBadgesFromEventList(eventsRaw);
        }
    });


    calendarInstance.render();

    if (typeof onComplete === 'function') {
        onComplete();
    }
}

function updateMonthBadgesFromEventList(eventList) {
    const monthCounts = {};

    eventList.forEach(ev => {
        const startDate = new Date(ev.start);
        if (!isNaN(startDate.getTime())) {
            const month = startDate.getMonth() + 1;
            monthCounts[month] = (monthCounts[month] || 0) + 1;
        }
    });

    $('.month-btn').each(function () {
        const month = parseInt($(this).data('month'), 10);
        $(this).find('.badge').remove();

        if (monthCounts[month]) {
            const badge = `<span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">${monthCounts[month]}</span>`;
            $(this).append(badge);
        }
    });
}

function renderEventSummaryBox(eventList) {
    // ── Show "loading..." placeholder ─────────────
    $('#Box_show_partial_event_in_year').html(`
        <div class="text-center p-4 text-muted">
            <div class="spinner-border text-primary mb-2" role="status"></div><br />
            กำลังโหลดข้อมูล...
        </div>
    `);

    setTimeout(() => { // simulate load delay (optional UX)
        // ── group by JS month (1-12) ────────────────
        const monthGroups = {};
        eventList.forEach(ev => {
            const d = new Date(ev.start);
            if (isNaN(d)) return;
            const m = d.getMonth() + 1;
            (monthGroups[m] ??= []).push(ev);
        });

        // ── month name helper ──────────────────────
        const monthNames = [
            '', 'January', 'February', 'March', 'April', 'May', 'June',
            'July', 'August', 'September', 'October', 'November', 'December'
        ];

        let hasData = Object.keys(monthGroups).length > 0;

        // ── build HTML ─────────────────────────────
        let html = '<div class="md-sidebar"><div class="md-sidebar-aside job-sidebar">';
        html += '<div class="default-according style-1 faq-accordion job-accordion" id="accordionoc">';
        html += '<div class="row"><div class="col-xl-12"><div class="card"><div class="card-header">';
        html += '<h5 class="mb-0"><button class="btn btn-link" data-bs-toggle="collapse" data-bs-target="#collapseicon1" aria-expanded="true" aria-controls="collapseicon1">';
        html += 'Events in year.</button></h5></div>';
        html += '<div class="collapse card-body px-0 show" id="collapseicon1" data-bs-parent="#accordionoc">';

        if (hasData) {
            for (let m = 1; m <= 12; m++) {
                const list = monthGroups[m];
                if (!list || !list.length) continue;

                html += `<div class="categories pt-3 pb-0">
                           <div class="learning-header">
                             <span class="f-w-600">🗓️ ${monthNames[m]}</span>
                           </div><ul class="list-unstyled">`;

                list.forEach(ev => {
                    const start = new Date(ev.start);
                    const end = ev.end ? new Date(ev.end) : null;
                    const range = end ? `${start.getDate()}–${end.getDate()}` : `${start.getDate()}`;
                    let monthLabel = monthNames[start.getMonth() + 1].slice(0, 3);
                    if (end && start.getMonth() !== end.getMonth()) {
                        monthLabel += '–' + monthNames[end.getMonth() + 1].slice(0, 3);
                    }


                    const tagHtml = (ev.Eventtags ?? '')
                        .split(',')
                        .filter(t => t.trim())
                        .map(t => `<span class="glam-tag"><i class="fa fa-tag me-1"></i>${t.trim()}</span>`)
                        .join('');


                    html += `<li class="d-flex justify-content-between align-items-start mb-1">
                                <div class="flex-grow-1 pe-3" style="min-width:0;">
                                    <div class="fw-bold text-truncate">
                                        <a href="javascript:void(0)" onclick="OpenEditEventModalFormSummeryYearBox('${ev.EventID}', '${ev.ProjectID}')" class="text-decoration-none">
                                            ${ev.Eventname}
                                        </a>
                                    </div>
                                    <div class="text-muted small">
                                        Location in 
                                        <a href="javascript:void(0)" onclick="OpenEditEventModalFormSummeryYearBox('${ev.EventID}', '${ev.ProjectID}')" class="text-decoration-underline">
                                            ${ev.Eventlocation}
                                        </a>
                                    </div>
                                    <div class="d-flex flex-wrap gap-1 small">${tagHtml}</div>
                                </div>
                                <div class="text-end ps-2" style="white-space:nowrap;">
                                    <h6 class="text-primary mb-0">${range}</h6>
                                    <small class="text-muted">${monthLabel}</small>
                                </div>
                            </li>`;

                });

                html += '</ul></div>';
            }
        } else {
            html += `
                <div class="p-4 text-center text-muted">
                    <i class="fa fa-calendar-times fa-2x mb-2"></i><br />
                    <strong>ไม่มีข้อมูลกิจกรรม</strong><br />
                    สำหรับปีที่เลือกไว้
                </div>
            `;
        }

        html += '</div></div></div></div></div></div>';

        $('#Box_show_partial_event_in_year').html(html);
    }, 300); // Delay 300ms for smooth loading effect
}

function parseToISO(dateStr) {
    const parsed = new Date(dateStr);
    return !isNaN(parsed.getTime()) ? parsed.toISOString() : null;
}

function EditEventProjectModal(event) {
    console.log('📘 Open Project Modal', event);
    openEditEventProjectModal(event.EventID, event.ProjectID);
    $('#modal-edit-event-in-project').modal('show');
}
function OpenEditEventModalFormSummeryYearBox(EventID, ProjectID ){
    openEditEventProjectModal(EventID, ProjectID);
    $('#modal-edit-event-in-project').modal('show');
}

function openEditEventProjectModal(EventID, ProjectID) {
    $('#modal-edit-event-in-project .modal-body').addClass('position-relative').append(`
        <div id="modal-loader" class="position-absolute top-0 start-0 w-100 h-100 d-flex align-items-center justify-content-center" style="background: rgba(255,255,255,0.8); z-index: 10;">
            <div class="spinner-border text-primary" role="status"></div>
        </div>
    `);

    fetch(baseUrl + `OtherSettings/GetDataModalEditEventInProject?EventID=${EventID}&ProjectID=${ProjectID}`)
        .then(res => res.json())
        .then(data => {
            $('#hiddenEditEventID').val(data.EventID);
            $('#hiddenEditProjectID').val(data.ProjectID);
            /*$('#txt-modal-edit-event-in-project-name').val(data.EventName);*/
            $('#txt-modal-edit-event-in-project-project').val(data.ProjectName);
            $('#txt-modal-edit-event-in-project-type-name').val(data.EventName);
            $('#color-modal-edit-event-in-project-type-color').val(data.EventColor);
            $('#txt-modal-edit-event-in-project-location').val(data.EventLocation);

            $('#txt-modal-edit-event-start-date-time').val(data.StartDate);
            $('#txt-modal-edit-event-end-date-time').val(data.EndDate);

            // ⭐️ เรียก flatpickr หลังจากใส่ค่าแล้ว
            flatpickr("#txt-modal-edit-event-start-date-time", {
                enableTime: true,
                time_24hr: true,
                dateFormat: "Y-m-d H:i",
                defaultDate: data.StartDate
            });

            flatpickr("#txt-modal-edit-event-end-date-time", {
                enableTime: true,
                time_24hr: true,
                dateFormat: "Y-m-d H:i",
                defaultDate: data.EndDate
            });

            const calendarTrack = document.getElementById('calendarTrackEditinProject');
            calendarTrack.innerHTML = '';

            let firstDateValue = null;
            let firstBtn = null;

            if (Array.isArray(data.DateEvents)) {
                data.DateEvents.forEach((d, index) => {
                    const btn = document.createElement('button');
                    btn.type = 'button';
                    btn.className = 'calendar-item';
                    btn.innerText = d.Text;
                    btn.setAttribute('data-value', d.Value);

                    btn.onclick = function () {
                        selectCalendarItemInProject(btn);
                    };

                    calendarTrack.appendChild(btn);

                    if (index === 0) {
                        firstDateValue = d.Value;
                        firstBtn = btn;
                    }
                });

                // ✅ กำหนดวันที่แรกเป็นค่าเริ่มต้น
                if (firstBtn) {
                    firstBtn.classList.add('selected');
                    selectCalendarItemInProject(firstBtn);
                }
            } else {
                console.warn('⚠️ DateEvents ไม่ถูกต้อง:', data.DateEvents);
            }
        })
        .finally(() => {
            $('#modal-loader').remove();
        });
}

function selectCalendarItemInProject(button) {
    const allButtons = document.querySelectorAll('#calendarTrackEditinProject .calendar-item');
    allButtons.forEach(btn => btn.classList.remove('selected'));
    button.classList.add('selected');

    const selectedDate = button.getAttribute('data-value');
    const eventID = document.getElementById('hiddenEditEventID')?.value;
    const projectID = document.getElementById('hiddenEditProjectID')?.value;

    loadShopsForEditInProject(eventID, projectID, selectedDate);
}

function loadShopsForEditInProject(eventID, projectID, selectedDate) {
    const container = document.querySelector('#modal-edit-event-in-project .card-body.pt-3');
    if (!container) return;

    // ล้างรายการร้านค้าเก่า (ไม่ลบ HEADER)
    const oldShopCards = container.querySelectorAll('.shop-item-card:not(:first-child)');
    oldShopCards.forEach(el => el.remove());

    fetch(`${baseUrl}OtherSettings/GetDataCreateEventsAndShops?EventID=${eventID}&EventDate=${selectedDate}&ProjectID=${projectID}`)
        .then(res => res.json())
        .then(data => {
            if (Array.isArray(data)) {
                data.forEach((shop, index) => {
                    const shopID = shop.ID || -1;
                    const name = shop.Name || '';
                    const unitQuota = shop.UnitQuota || 0;
                    const shopQuota = shop.ShopQuota || 0;
                    const isUsed = shop.IsUsed === true;

                    const newRow = document.createElement('div');
                    newRow.className = 'shop-item-card d-grid align-items-center p-3 shadow-sm rounded-3 border position-relative';
                    newRow.style.gridTemplateColumns = '22px 140px 100px 100px 100px 1fr';
                    newRow.style.gap = '1rem';

                    newRow.innerHTML = `

                            <div class="form-check m-0">
                                <input class="form-check-input" type="checkbox" id="check-${shopID}" ${isUsed ? 'checked' : ''}/>
                            </div>

                            <div class="fw-semibold fs-6 text-dark">${name}</div>

                            <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota" style="width: 100px;" value="${shopQuota ?? 0}" disabled />
                            <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota/Unit" style="width: 100px;" value="${unitQuota ?? 0}" disabled />

                            <div class="form-check form-switch ms-3">
                                <input class="form-check-input" type="checkbox" id="switch-${shop.ID}" onchange="toggleQuotaInputs(this)"/>
                            </div>

                            <div class="d-flex gap-2 justify-content-end">
                            </div>

                    `;

                    container.appendChild(newRow);
                });
            }
        })
        .catch(err => {
            console.error('❌ Error loading shops:', err);
            Swal.fire({
                icon: 'error',
                title: 'ผิดพลาด!',
                text: 'ไม่สามารถโหลดข้อมูลร้านค้าได้'
            });
        });
}

function addNewEditShop() {
    const container = document.querySelector('#modal-edit-event-in-project .card-body.pt-3');
    if (!container) return;

    const newRow = document.createElement('div');
    newRow.className = 'shop-item-card d-grid align-items-center p-3 shadow-sm rounded-3 border position-relative';
    newRow.style.gridTemplateColumns = '22px 140px 100px 100px 100px 1fr';
    newRow.style.gap = '1rem';

    newRow.innerHTML = `
        <div class="form-check m-0">
            <input class="form-check-input" type="checkbox" id="check--99" check/>
        </div>

        <input type="text" class="form-control form-control-sm fw-semibold text-dark"
               placeholder="ชื่อร้านค้า"
               style="min-width: 140px; width: 140px;" />

        <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota" style="width: 100px;" value="0" disabled />

        <input type="number" class="form-control form-control-sm quota-input" placeholder="Quota/Unit" style="width: 100px;" value="0" disabled />

        <div class="form-check form-switch ms-3">
            <input class="form-check-input" type="checkbox" onchange="toggleQuotaInputs(this)" />
        </div>

        <div class="d-flex gap-2 justify-content-end">
            <button type="button" class="btn btn-sm btn-outline-danger" onclick="this.closest('.shop-item-card').remove()">
                <i class="fa fa-trash"></i>
            </button>
        </div>
    `;

    container.appendChild(newRow);
}

function loadShopsForDate(dateValue, EventID, ProjectID) {
    fetch(baseUrl + `OtherSettings/GetDataTrEventsAndShopsinProjects?EventID=${EventID}&projectID=${ProjectID}&eventDate=${dateValue}`)
        .then(res => res.json())
        .then(shopList => {
            const container = document.querySelector('#modal-edit-event-in-project .card-body.pt-3.pb-4.px-3');
            const header = container.querySelector('.shop-item-card');
            container.innerHTML = '';
            container.appendChild(header);

            shopList.forEach((shop, i) => {
                const id = `edit-shop-${i}`;
                const isUsed = shop.IsUsed === true;

                container.innerHTML += `
                    <div class="shop-item-card p-3 shadow-sm rounded-3 border position-relative" 
                         style="display: grid; grid-template-columns: 22px 140px 100px 100px 100px 1fr; gap: 1rem; align-items: center;">

                        <!-- Checkbox -->
                        <div class="form-check m-0">
                            <input class="form-check-input" type="checkbox" id="${id}" ${isUsed ? 'checked' : ''} disabled />
                        </div>

                        <!-- Name -->
                        <div class="fw-semibold fs-6 text-dark">${shop.Name}</div>

                        <!-- Shop Quota -->
                        <input type="number" class="form-control form-control-sm quota-input" 
                               placeholder="Quota" style="width: 100px;" value="${shop.ShopQuota ?? 0}" disabled />

                        <!-- Unit Quota -->
                        <input type="number" class="form-control form-control-sm quota-input" 
                               placeholder="Quota/Unit" style="width: 100px;" value="${shop.UnitQuota ?? 0}" disabled />

                        <!-- Switch -->
                        <div class="form-check form-switch ms-3">
                            <input class="form-check-input" type="checkbox" id="switch-${shop.id}" onchange="toggleQuotaInputs(this)" disabled />
                        </div>

                        <!-- Actions -->
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
        });
}

btn.onclick = function () {
    selectCalendarItemInProject(btn);
    loadShopsForDate(d.Value, EventID, ProjectID); // ✅ ทำงานถูกต้องแล้ว
};

function slideLeftEditinProject() {
    const track = document.getElementById("calendarTrackEditinProject");
    currentIndex = Math.max(currentIndex - slideSize, 0);
    updateTransformEditinProject(track);
}

function slideRightEditinProject() {
    const track = document.getElementById("calendarTrackEditinProject");
    const totalItems = track.children.length;
    const maxIndex = totalItems - slideSize;
    currentIndex = Math.min(currentIndex + slideSize, maxIndex);
    updateTransformEditinProject(track);
}

function updateTransformEditinProject(track) {
    const x = -currentIndex * itemWidth;
    track.style.transform = `translateX(${x}px)`;
}

function EditEventModal(event) {
    console.log('📗 Open Event Modal', event);
    // เปิด modal ที่ใช้สำหรับ Event ปกติ
    //$('#modal-event').modal('show');
}

function goToCalendarMonth(month) {
    const year = $('#ddl-year-shop-event').val() || new Date().getFullYear();
    const date = new Date(year, month - 1, 1);
    if (calendarInstance) {
        calendarInstance.gotoDate(date);
    }
    MonthSeletedBTN = month;
    // ปรับปุ่ม active
    $('.month-btn').removeClass('active');
    $(`.month-btn[data-month="${month}"]`).addClass('active');
}












