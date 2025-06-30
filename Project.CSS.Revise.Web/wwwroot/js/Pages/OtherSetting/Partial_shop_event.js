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
        month: month
    })
        .done(function (eventList) {
            initFullCalendarWithEvents(eventList, function () {
                updateMonthBadges();
            }, monthOverride); // ✅ ส่ง monthOverride เข้าไปด้วย
        })
        .fail(function (xhr) {
            console.error('❌ โหลด Event ไม่สำเร็จ:', xhr);
            $calendar.html('<div class="alert alert-danger">โหลดปฏิทินไม่สำเร็จ</div>');
        });
}
function initFullCalendarWithEvents(eventsRaw, onComplete, monthOverride = '') {
    const selectedYear = $('#ddl-year-shop-event').val();
    const selectedMonth = monthOverride && monthOverride !== '' ? parseInt(monthOverride) : 1;

    const initialDate = new Date(`${selectedYear}-${String(selectedMonth).padStart(2, '0')}-01`);

    const events = eventsRaw.map(ev => {
        const startISO = parseToISO(ev.StartDate);
        let endISO = parseToISO(ev.EndDate);

        if (endISO) {
            const end = new Date(endISO);
            end.setDate(end.getDate() + 1); // ✅ always add 1 day (FullCalendar-exclusive)
            endISO = end.toISOString();
        }

        return {
            title: ev.Name,
            start: startISO,
            end: endISO,
            color: ev.Color,
            extendedProps: {
                location: ev.Location
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
        titleFormat: {
            year: 'numeric',
            month: 'long'
        },
        initialView: 'dayGridMonth',
        initialDate: initialDate,
        aspectRatio: 1.5,
        headerToolbar: {
            left: '',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        events: events,
        displayEventTime: false,
        eventClick: function (info) {
            openEventModal(info.event);
        },
        eventDidMount: function (info) {
            const el = info.el;
            el.style.cursor = 'pointer';
            el.style.borderRadius = '6px';
            el.style.padding = '2px 6px';
            el.style.fontWeight = 'bold';
            el.style.backgroundColor = info.event.backgroundColor || '#3498db';
            el.style.color = 'white';
        }
    });

    calendarInstance.render();

    // ✅ callback เมื่อเสร็จ
    if (typeof onComplete === 'function') {
        onComplete();
    }
}
function parseToISO(dateStr) {
    const parsed = new Date(dateStr);
    return !isNaN(parsed.getTime()) ? parsed.toISOString() : null;
}
function openEventModal(event) {
    const title = event.title;
    const location = event.extendedProps?.location || 'ไม่ระบุ';
    const start = new Date(event.start).toLocaleDateString('th-TH');
    const end = new Date(event.end).toLocaleDateString('th-TH');

    $('#modalEventTitle').text(title);
    $('#modalEventDate').text(`${start} - ${end}`);
    $('#modalEventLocation').text(location);

    $('#modalEventInfo').modal('show');
}
function updateMonthBadges() {
    const projectId = $('#ddl-project-shop-event').val();
    const year = $('#ddl-year-shop-event').val();

    if (!projectId || !year) return;

    $.getJSON(baseUrl + 'OtherSettings/GetlistCountEventByMonth', {
        projectID: projectId,
        year: year
    })
        .done(function (data) {
            // เคลียร์ badge ทั้งหมดก่อน
            $('.month-btn').each(function () {
                const badge = $(this).find('.badge');
                if (badge.length) {
                    badge.remove(); // ลบ badge เดิม
                }
            });

            // ใส่ badge เฉพาะเดือนที่มีข้อมูล
            data.forEach(ev => {
                const month = ev.MonthNumber;
                const count = ev.EventCount;

                const btn = $(`.month-btn[data-month="${month}"]`);
                if (btn.length && count > 0) {
                    const badge = `<span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">${count}</span>`;
                    btn.append(badge);
                }
            });
        })
        .fail(function (xhr) {
            console.error('❌ โหลดจำนวน Event รายเดือนล้มเหลว:', xhr);
        });
}







