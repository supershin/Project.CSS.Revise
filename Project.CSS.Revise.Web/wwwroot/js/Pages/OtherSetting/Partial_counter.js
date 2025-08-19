// -------- Helpers --------
function getMultiSelectValues(selector) {
    const el = document.querySelector(selector);
    if (!el) return [];
    // ทำงานได้ทั้ง native <select multiple> และที่ใช้ Choices.js
    return Array.from(el.options).filter(o => o.selected).map(o => o.value).filter(Boolean);
}
function toCommaList(arr, { trailing = false } = {}) {
    if (!arr || !arr.length) return '';
    return arr.join(',') + (trailing ? ',' : '');
}
function positionCountInclusive(start, end) {
    const s = Number(start) || 0, e = Number(end) || 0;
    return Math.max(0, e - s + 1);
}
function emptyCard(msg) {
    return `
    <div class="col-12">
      <div class="proj-card d-flex flex-column">
        <div class="text-muted">${msg}</div>
      </div>
    </div>`;
}
function escapeHtml(s) {
    return (s ?? '').toString()
        .replace(/&/g, '&amp;').replace(/</g, '&lt;')
        .replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#39;');
}

// -------- Card builders --------
function buildBankCard(row) {
    const title = escapeHtml(row.ProjectName || row.ProjectID || '');
    const subtype = escapeHtml(row.QueueTypeName || 'Bank Pre-Approve (สินเชื่อ)');
    const units = positionCountInclusive(row.StartCounter, row.EndCounter);
    const staffCount = Number(row.COUNT_Staff) || 0;
    const bankCount = Number(row.COUNT_Bank) || 0;

    return `
  <div class="col-12 col-md-6">
    <div class="proj-card d-flex flex-column">
      <div class="d-flex justify-content-between align-items-start">
        <div>
          <h3 class="site-name">${title}</h3>
          <div class="mt-1"><span class="subtype">${subtype}</span></div>
        </div>
        <span class="count-badge">${units} หน่วย</span>
      </div>
      <div class="mt-auto pt-3 d-flex justify-content-between align-items-center flex-wrap btn-row">
        <button class="action-btn"><i class="fa-solid fa-qrcode me-1"></i>Download QR</button>
        <div class="d-flex btn-row">
          <button class="icon-btn" title="Staff count">
            <i class="fa-solid fa-user"></i>Staff ${staffCount}
          </button>
          <button class="icon-btn" title="Bank count">
            <i class="fa-solid fa-landmark"></i>Bank ${bankCount}
          </button>
        </div>
      </div>
    </div>
  </div>`;
}

function buildInspectCard(row) {
    const title = escapeHtml(row.ProjectName || row.ProjectID || '');
    const subtype = escapeHtml(row.QueueTypeName || 'Inspect (รับมอบห้อง)');
    const units = positionCountInclusive(row.StartCounter, row.EndCounter);

    return `
  <div class="col-12 col-md-6">
    <div class="proj-card d-flex flex-column">
      <div class="d-flex justify-content-between align-items-start">
        <div>
          <h3 class="site-name">${title}</h3>
          <div class="mt-1"><span class="subtype">${subtype}</span></div>
        </div>
        <span class="count-badge" style="background:#f0e8ff;color:#6f3acb;">${units} หน่วย</span>
      </div>
      <div class="mt-auto pt-3 d-flex justify-content-between align-items-center flex-wrap btn-row">
        <button class="action-btn"><i class="fa-solid fa-qrcode me-1"></i>Download QR</button>
      </div>
    </div>
  </div>`;
}

// -------- Render --------
function renderCounters(data) {
    const bankWrap = document.getElementById('bank_cards');
    const inspectWrap = document.getElementById('inspect_cards');
    if (bankWrap) bankWrap.innerHTML = '';
    if (inspectWrap) inspectWrap.innerHTML = '';

    if (!Array.isArray(data) || data.length === 0) {
        if (bankWrap) bankWrap.innerHTML = emptyCard('ไม่พบข้อมูล');
        if (inspectWrap) inspectWrap.innerHTML = '';
        return;
    }

    const bank = data.filter(x => Number(x.QueueTypeID) === 48);
    const inspect = data.filter(x => Number(x.QueueTypeID) === 49);

    if (bankWrap) bankWrap.innerHTML = bank.length ? bank.map(buildBankCard).join('') : emptyCard('ไม่พบข้อมูล Bank Pre-Approve');
    if (inspectWrap) inspectWrap.innerHTML = inspect.length ? inspect.map(buildInspectCard).join('') : '';
}

// -------- Fetch --------
async function fetchProjectCounters() {
    const bu = getMultiSelectValues('#ddl_BUG_counter');            // ex. ['1','3']
    const prj = getMultiSelectValues('#ddl_PROJECT_counter');       // ex. ['102C028','102C029']
    let rawValue = document.getElementById('ddl_counter_type')?.value ?? "-1";

    // ถ้า clear แล้ว rawValue จะเป็น "" => แก้ให้เป็น "-1"
    if (rawValue === "" || rawValue === null) {
        rawValue = "-1";
    }
    const queueType = Number(rawValue);
    console.log('ddl_counter_type : ' + queueType);

    const params = new URLSearchParams({
        Bu: toCommaList(bu, { trailing: false }),                      // "1,3"
        ProjectID: toCommaList(prj, { trailing: true }),               // "102C028,102C029," สำหรับ logic LIKE ฝั่ง SQL
        QueueType: isNaN(queueType) ? '-1' : String(queueType)
    });

    const btn = document.getElementById('btnSearchCounter');
    const originalHTML = btn ? btn.innerHTML : '';
    try {
        if (btn) { btn.disabled = true; btn.innerHTML = `<span class="spinner-border spinner-border-sm me-1"></span> Loading...`; }

        const res = await fetch(baseUrl + 'OtherSettings/GetListProjectCounter?' + params.toString(), {
            method: 'GET'
        });

        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        const json = await res.json(); // คาดว่าเป็น List<ProjectCounterMappingModel.ListData>
        renderCounters(json);
    } catch (err) {
        console.error('GetListProjectCounter error:', err);
        renderCounters([]);
    } finally {
        if (btn) { btn.disabled = false; btn.innerHTML = originalHTML; }
    }
}

// -------- Wire button --------
document.addEventListener('DOMContentLoaded', function () {
    const searchBtn = document.getElementById('btnSearchCounter');
    if (searchBtn) {
        searchBtn.addEventListener('click', function (e) {
            e.preventDefault();
            fetchProjectCounters();
        });
    }
});