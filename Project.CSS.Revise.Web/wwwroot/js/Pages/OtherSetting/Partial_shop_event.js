function loadPartial(viewName) {
    $('#Box_show_partial').html(`
                <div class="text-center p-5">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                    <div>กำลังโหลดข้อมูล...</div>
                </div>
            `);

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