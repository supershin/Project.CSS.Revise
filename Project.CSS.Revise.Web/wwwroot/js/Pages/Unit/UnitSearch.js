$(document).ready(function () {
    // --- 1. ตั้งค่า Select2 ครั้งเดียวให้ครอบคลุมทุกตัว ---
    $('.multi-select').select2({
        dropdownParent: $('#conditionModal'),
        placeholder: " Select status...",
        width: '100%',
        allowClear: true
    });

    // --- 2. เมื่อกดปุ่ม Display Results ใน Modal ---
    $('#btnDisplayResults').on('click', function () {
        // ปิด Modal
        $('#conditionModal').modal('hide');

        // แสดงผลตาราง
        showSearchResults();
    });

    // --- 3. ดักจับปุ่ม Enter ที่ช่อง Search หน้าหลัก ---
    $("#txtSearch").keypress(function (e) {
        if (e.which == 13) {
            showSearchResults();
        }
    });
});

function showSearchResults() {
    // ซ่อน Empty State และโชว์ตาราง
    $("#emptyState").addClass("d-none");
    $("#resultSection").removeClass("d-none");

    // --- 4. จำลองข้อมูลตาราง (Mock Data) ---
    var mockData = [
        { code: "A-05-12", customer: "คุณใจดี มีตังค์", status: "In Process", color: "warning" },
        { code: "B-02-09", customer: "Mr. John Doe", status: "Approve", color: "success" },
        { code: "C-10-01", customer: "คุณสมชาย ขยันเรียน", status: "Wait Bank", color: "info" }
    ];

    var html = "";
    mockData.forEach(function (item) {
        html += `
            <tr>
                <td class="ps-4">
                    <span class="badge bg-light text-dark border-dashed" style="font-size: 14px;">${item.code}</span>
                </td>
                <td class="fw-medium">${item.customer}</td>
                <td>
                    <span class="badge rounded-pill bg-${item.color}-subtle text-${item.color}">
                        <i class="fa-solid fa-circle me-1" style="font-size: 8px;"></i>${item.status}
                    </span>
                </td>
                <td class="text-center">
                    <button class="btn btn-sm btn-light border rounded-circle shadow-sm">
                        <i class="fa fa-eye text-primary"></i>
                    </button>
                </td>
            </tr>`;
    });

    $("#tblBody").html(html);
}
function showSearchResults() {
    $("#emptyState").addClass("d-none");
    $("#resultSection").removeClass("d-none");

    // จำลองข้อมูล (ใส่คลาส sticky-col ที่ td แรก)
    var html = `
        <tr class="text-nowrap">
            <td class="ps-4 sticky-col fw-bold text-primary">A-05-12</td>
            <td>123/45</td>
            <td>A</td>
            <td>5</td>
            <td>1BR</td>
            <td>28.5</td>
            <td><span class="badge bg-success-subtle text-success">Active</span></td>
            <td>2,500,000</td>
            <td>Yes</td>
            <td>2,450,000</td>
            <td>CN-001</td>
            <td>V-99</td>
            <td>C-888</td>
            <td>คุณใจดี มีตังค์</td>
            <td>Draft 1</td>
            <td>Approved</td>
            <td>Waiting</td>
            <td>KBank</td>
            <td>2025-12-01</td>
            <td>2025-12-15</td>
            <td>2026-01-30</td>
            <td>1</td>
            <td>2026-01-20</td>
            <td>0</td>
            <td>Normal</td>
            <td>2026-01-25</td>
            <td>Pass</td>
            <td>2026-01-26</td>
            <td>โอนแล้ว</td>
            <td>อยู่ระหว่างติดตาม</td>
            <td>2026-02-15</td>
            <td>2026-02-28</td>
            <td>Admin</td>
            <td>-</td>
            <td>-</td>
            <td>-</td>
            <td>-</td>
            <td>-</td>
            <td>-</td>
            <td>-</td>
            <td>BBL</td>
         
        </tr>`;

    $("#tblBody").html(html);
}