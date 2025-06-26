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
function LoadPartialshopevent() {
    const projectId = $('#ddl-project-shop-event').val();
    const dateRange = $('#dateRange').val();

    const $panel = $('#event-list-panel');
    const $calendar = $('#calendar');

    if ($panel.length) {
        $panel.html(`
            <li class="list-group-item text-center text-muted">กำลังโหลดข้อมูล...</li>
        `);
    }

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
        daterang: dateRange
    })
        .done(function (eventList) {
            initFullCalendarWithEvents(eventList, function () {
                console.log('✅ Calendar loaded successfully');
                // 🔁 callback logic ถ้าต้องทำอะไรต่อ เช่น scroll, focus, reload
            });
        })
        .fail(function (xhr) {
            console.error('❌ โหลด Event ไม่สำเร็จ:', xhr);
            if ($panel.length) {
                $panel.html('<li class="list-group-item text-danger">โหลดรายการไม่สำเร็จ</li>');
            }
            if ($calendar.length) {
                $calendar.html('<div class="alert alert-danger">โหลดปฏิทินไม่สำเร็จ</div>');
            }
        });
}
function initFullCalendarWithEvents(eventsRaw, onComplete) {
    const events = eventsRaw.map(ev => {
        const startISO = parseToISO(ev.StartDate);
        let endISO = parseToISO(ev.EndDate);

        if (endISO) {
            const end = new Date(endISO);
            end.setDate(end.getDate() + 1); // ✅ always add 1 day (FullCalendar-exclusive)
            endISO = end.toISOString();
        }

        /*console.log(ev.Name);*/

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


    const firstDateStr = events.length > 0 ? events[0].start : null;
    let initialDate = new Date();

    if (firstDateStr) {
        const parsed = new Date(firstDateStr);
        if (!isNaN(parsed.getTime())) {
            initialDate = parsed;
        }
    }

    const calendarEl = document.getElementById('calendar');

    // ✅ ล้าง loading HTML ที่ค้างอยู่
    calendarEl.innerHTML = '';

    if (calendarInstance) {
        calendarInstance.destroy();
        calendarInstance = null;
    }

    calendarInstance = new FullCalendar.Calendar(calendarEl, {
        locale: 'en', // ✅ ใช้ 'en' เพื่อบังคับปี ค.ศ.
        dayHeaderFormat: { weekday: 'short' }, // แสดงชื่อวันแบบย่อ

        // ใช้ titleFormat เอง
        titleFormat: {
            year: 'numeric',
            month: 'long'
        },

        initialView: 'dayGridMonth',
        initialDate: initialDate,
        //height: 'auto',         // ✅ Important: Make height flexible
        aspectRatio: 1.5,       // ✅ Adjusts width/height balance
        headerToolbar: {
            left: 'prev,next today',
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

    const panel = document.getElementById('event-list-panel');
    panel.innerHTML = '';

    if (!events || events.length === 0) {
        panel.innerHTML = '<li class="list-group-item">ไม่พบกิจกรรม</li>';
    } else {
        events.forEach((ev, index) => {
            const item = document.createElement('li');
            item.className = 'list-group-item d-flex justify-content-between align-items-center';

            // ✅ สร้าง <span> ข้อความ title ด้านซ้าย
            const titleSpan = document.createElement('span');
            titleSpan.textContent = ev.title;

            // ✅ สร้าง <span> จุดสีด้านขวา
            const colorDot = document.createElement('span');
            colorDot.style.display = 'inline-block';
            colorDot.style.width = '12px';
            colorDot.style.height = '12px';
            colorDot.style.borderRadius = '50%';
            colorDot.style.backgroundColor = ev.color || '#999';
            colorDot.style.flexShrink = '0';

            item.appendChild(titleSpan);
            item.appendChild(colorDot);
            panel.appendChild(item);
        });
    }

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






