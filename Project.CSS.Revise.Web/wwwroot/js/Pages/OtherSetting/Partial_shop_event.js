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
function LoadPartialshopevent(monthOverride = '') {
    const projectId = $('#ddl-project-shop-event').val();
    const year = $('#ddl-year-shop-event').val();
    const month = monthOverride || '';
    const showby = $('#ddl-showby-shop-event').val();

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
        projectID: projectId,
        year: year,
        month: month,
        showby: showby
    })
    .done(function (eventList) {

        // 1️⃣  FullCalendar
        initFullCalendarWithEvents(eventList, function () {
            updateMonthBadgesFromEventList(eventList);
        }, monthOverride);

        // 2️⃣  Sidebar refresh
        renderEventSummaryBox(eventList);

    })
    .fail(function (xhr) {
        console.error('❌ โหลด Event ไม่สำเร็จ:', xhr);
        $calendar.html('<div class="alert alert-danger">โหลดปฏิทินไม่สำเร็จ</div>');
    });


}

//function initFullCalendarWithEvents(eventsRaw, onComplete, monthOverride = '') {
//    const selectedYear = $('#ddl-year-shop-event').val();
//    const selectedMonth = monthOverride && monthOverride !== '' ? parseInt(monthOverride) : 1;

//    const initialDate = new Date(`${selectedYear}-${String(selectedMonth).padStart(2, '0')}-01`);

//    const events = eventsRaw.map(ev => {
//        const startISO = parseToISO(ev.start);
//        let endISO = parseToISO(ev.end);

//        if (endISO) {
//            const end = new Date(endISO);
//            end.setDate(end.getDate() + 1); // ✅ always add 1 day (FullCalendar-exclusive)
//            endISO = end.toISOString();
//        }

//        return {
//            title: ev.title,
//            start: startISO,
//            end: endISO,
//            color: ev.color
//            //extendedProps: {
//            //    location: ev.Location
//            //}
//        };
//    });

//    const calendarEl = document.getElementById('calendar');

//    calendarEl.innerHTML = '';

//    if (calendarInstance) {
//        calendarInstance.destroy();
//        calendarInstance = null;
//    }

//    calendarInstance = new FullCalendar.Calendar(calendarEl, {
//        locale: 'en',
//        dayHeaderFormat: { weekday: 'short' },
//        titleFormat: {
//            year: 'numeric',
//            month: 'long'
//        },
//        initialView: 'dayGridMonth',
//        initialDate: initialDate,
//        aspectRatio: 1.5,
//        headerToolbar: {
//            left: 'prev,next today',
//            center: 'title',
//            right: 'dayGridMonth,timeGridWeek,timeGridDay'
//        },
//        events: events,
//        displayEventTime: false,
//        eventClick: function (info) {
//            openEventModal(info.event);
//        },
//        eventDidMount: function (info) {
//            const el = info.el;
//            el.style.cursor = 'pointer';
//            el.style.borderRadius = '6px';
//            el.style.padding = '2px 6px';
//            el.style.fontWeight = 'bold';
//            el.style.backgroundColor = info.event.backgroundColor || '#3498db';
//            el.style.color = 'white';
//        }
//    });

//    calendarInstance.render();

//    // ✅ callback เมื่อเสร็จ
//    if (typeof onComplete === 'function') {
//        onComplete();
//    }
//}

function initFullCalendarWithEvents(eventsRaw, onComplete, monthOverride = '') {
    const selectedYear = $('#ddl-year-shop-event').val();
    const selectedMonth = monthOverride && monthOverride !== '' ? parseInt(monthOverride) : 1;
    const initialDate = new Date(`${selectedYear}-${String(selectedMonth).padStart(2, '0')}-01`);

    // ✅ เตรียมแปลง event สำหรับ FullCalendar
    const events = eventsRaw.map(ev => {
        const startISO = parseToISO(ev.start);
        let endISO = parseToISO(ev.end);

        if (endISO) {
            const end = new Date(endISO);
            end.setDate(end.getDate() + 1);
            endISO = end.toISOString();
        }

        return {
            title: ev.title,
            start: startISO,
            end: endISO,
            color: ev.color,
            extendedProps: {
                modaltype: ev.modaltype,
                originalData: ev // เก็บ event ทั้งตัว (ถ้าจะใช้ข้อมูลอื่นด้วย)
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
        locale: 'en',
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
        eventClick: function (info) {
            const modaltype = info.event.extendedProps.modaltype;
            if (modaltype === 1) {
                EditEventProjectModal(info.event.extendedProps.originalData);
            } else if (modaltype === 2) {
                EditEventModal(info.event.extendedProps.originalData);
            } else {
                console.warn('⚠️ Unknown modaltype:', modaltype);
            }
        },
        eventDidMount: function (info) {
            const el = info.el;
            el.style.cursor = 'pointer';
            el.style.borderRadius = '6px';
            el.style.padding = '2px 6px';
            el.style.fontWeight = 'bold';
            el.style.backgroundColor = info.event.backgroundColor || '#3498db';
            el.style.color = 'white';
        },

        // ✅ จุดนี้จะถูกเรียกเมื่อกด prev/next/today
        datesSet: function (info) {
            const currentDate = calendarInstance.getDate(); // ใช้วันที่ของเดือนจริง ๆ
            const month = currentDate.getMonth() + 1;

            console.log('🗓️ datesSet: current date →', currentDate);
            console.log('📅 month:', month);

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
                        .map(t => `<span class="badge bg-warning text-white border">
                                     <i class="fa fa-tag me-1"></i> ${t.trim()}
                                   </span>`).join('');

                    html += `<li class="d-flex justify-content-between align-items-start mb-1">
                                <div class="flex-grow-1 pe-3" style="min-width:0;">
                                    <div class="fw-bold text-truncate">${ev.Eventname}</div>
                                    <div class="text-muted small">
                                        Location in <a href="#">${ev.Eventlocation}</a>
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
    // เปิด modal ที่ใช้สำหรับโครงการ


    $('#modal-edit-event-in-project').modal('show');
    // ดึงข้อมูล event.name, location, tag, etc.
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

    // ปรับปุ่ม active
    $('.month-btn').removeClass('active');
    $(`.month-btn[data-month="${month}"]`).addClass('active');
}









