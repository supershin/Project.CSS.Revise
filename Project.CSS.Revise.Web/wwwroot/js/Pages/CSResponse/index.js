(function () {
    const block = e => { e.preventDefault(); e.stopPropagation(); };
    document.addEventListener('contextmenu', block, { capture: true });
    document.addEventListener('dragstart', block, { capture: true });
    document.addEventListener('copy', block, { capture: true });
    document.addEventListener('keydown', function (e) {
        const k = e.key?.toUpperCase();
        if (k === 'F12') return block(e);
        if (e.ctrlKey && e.shiftKey && ['I', 'J', 'C'].includes(k)) return block(e);
        if (e.ctrlKey && (k === 'U' || k === 'S' || k === 'P')) return block(e); // View Source / Save / Print
    }, { capture: true });
})();

document.querySelectorAll('.hover-icon-swap').forEach(container => {
    const icon = container.querySelector('.icon-toggle');
    const defaultClass = icon.dataset.default;
    const hoverClass = icon.dataset.hover;

    container.addEventListener('mouseenter', () => {
        icon.classList.remove(defaultClass);
        icon.classList.add(hoverClass);
    });

    container.addEventListener('mouseleave', () => {
        icon.classList.remove(hoverClass);
        icon.classList.add(defaultClass);
    });
});


document.addEventListener("DOMContentLoaded", function () {
    // ====== Choices instances ===================================================
    new Choices('#csUserSelect', { placeholderValue: '🔍 พิมพ์ค้นหาชื่อพนักงาน...', searchEnabled: true, itemSelectText: '', shouldSort: false });

    const projectChoices = new Choices('#projectSelect', {
        placeholderValue: '🔍 พิมพ์ค้นหาโครงการ...', searchEnabled: true, itemSelectText: '', shouldSort: false
    });
    const buildChoices = new Choices('#buildingMultiSelect', {
        removeItemButton: true, placeholderValue: '🔍 เลือกอาคาร', searchEnabled: true, itemSelectText: '', shouldSort: false
    });
    const floorChoices = new Choices('#floorMultiSelect', {
        removeItemButton: true, placeholderValue: '🔍 เลือกชั้น (เช่น B-2)', searchEnabled: true, itemSelectText: '', shouldSort: false
    });
    const unitChoices = new Choices('#roomMultiSelect', {
        removeItemButton: true, placeholderValue: '🔍 เลือกยูนิต', searchEnabled: true, itemSelectText: '', shouldSort: false
    });

    // ====== Helpers =============================================================
    const $ = (sel) => document.querySelector(sel);

    function formPost(url, data) {
        return fetch(url, {
            method: 'POST', // Controller ตอนนี้เป็น [HttpPost]
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },
            body: new URLSearchParams(data)
        }).then(r => r.json());
    }

    function setLoading(selectEl, choicesInst, msg = 'กำลังโหลด...') {
        choicesInst.clearStore();
        choicesInst.clearChoices();
        choicesInst.setChoices([{ value: '', label: msg, disabled: true }], 'value', 'label', true);
        selectEl.setAttribute('disabled', 'disabled');
    }
    function clearSelect(selectEl, choicesInst, placeholder = '— เลือก —') {
        choicesInst.clearStore();
        choicesInst.clearChoices();
        choicesInst.setChoices([{ value: '', label: placeholder, disabled: true }], 'value', 'label', true);
        selectEl.setAttribute('disabled', 'disabled');
    }
    function enableSelect(selectEl) { selectEl.removeAttribute('disabled'); }
    function getSelectedValues(choicesInst) {
        const val = choicesInst.getValue(true);
        return Array.isArray(val) ? val.filter(v => v !== '') : (val ? [val] : []);
    }
    const csv = arr => (arr && arr.length ? arr.join(',') : '');

    // ====== Loaders (ใช้ baseUrl) ==============================================
    async function loadBuilds(projectId) {
        const el = $('#buildingMultiSelect');
        setLoading(el, buildChoices);
        try {
            const res = await formPost(baseUrl + 'CSResponse/GetlistBuildInProject', { ProjectID: projectId });
            const items = (res?.data || []).map(x => ({
                value: String(x.ValueString),
                label: String(x.Text)
            })).filter(o => o.value);
            buildChoices.clearStore();
            buildChoices.setChoices(items, 'value', 'label', true);
            enableSelect(el);
        } catch (e) {
            console.error(e);
            clearSelect(el, buildChoices, 'โหลดอาคารไม่สำเร็จ');
        }
    }

    async function loadFloors(projectId, buildsCsv) {
        const el = $('#floorMultiSelect');
        setLoading(el, floorChoices);
        try {
            const res = await formPost(baseUrl + 'CSResponse/GetListFloorInBuildInProject', {
                ProjectID: projectId,
                Builds: buildsCsv
            });
            // API คืน { Value = "B-2", Text = "ตึก B ชั้น 2" }
            const items = (res?.data || []).map(x => ({
                value: String(x.ValueString),
                label: String(x.Text)
            })).filter(o => o.value);
            floorChoices.clearStore();
            floorChoices.setChoices(items, 'value', 'label', true);
            enableSelect(el);
        } catch (e) {
            console.error(e);
            clearSelect(el, floorChoices, 'โหลดชั้นไม่สำเร็จ');
        }
    }

    async function loadUnits(projectId, buildsCsv, pairsCsv) {
        const el = $('#roomMultiSelect');
        setLoading(el, unitChoices);
        try {
            const res = await formPost(baseUrl + 'CSResponse/GetListUnitInFloorInBuildInProject', {
                ProjectID: projectId,
                Builds: buildsCsv,
                Floors: pairsCsv // ตรงกับ model.IDString3 (@Pairs) ใน service
            });
            // API คืน UnitCode เป็นทั้ง Value/Text
            const items = (res?.data || []).map(x => ({
                value: String(x.ValueString),
                label: String(x.Text)
            })).filter(o => o.value);
            unitChoices.clearStore();
            unitChoices.setChoices(items, 'value', 'label', true);
            enableSelect(el);
        } catch (e) {
            console.error(e);
            clearSelect(el, unitChoices, 'โหลดยูนิตไม่สำเร็จ');
        }
    }

    // ====== Chain handlers (Project เป็น key) ==================================
    // === เปลี่ยนเฉพาะ Handlers ด้านล่างนี้ ===

    async function onProjectChange() {
        const projectId = projectChoices.getValue(true);

        // reset children
        clearSelect($('#buildingMultiSelect'), buildChoices, '— เลือกอาคาร —');
        clearSelect($('#floorMultiSelect'), floorChoices, '— เลือกชั้น —');
        clearSelect($('#roomMultiSelect'), unitChoices, '— เลือกยูนิต —');

        if (!projectId) {
            // ถ้าไม่เลือก project → ปิดทั้งหมด
            return;
        }

        // ✅ เลือก Project แล้ว: โหลด "ทั้งหมดของโปรเจกต์" ได้เลย
        await loadBuilds(projectId);                    // รายการอาคารทั้งหมด
        await loadFloors(projectId, '');                // ไม่ส่ง Builds → ได้ทุก Build-Floor
        await loadUnits(projectId, '', '');             // ไม่ส่ง Builds/Pair → ได้ทุก Unit ในโปรเจกต์
    }

    async function onBuildChange() {
        const projectId = projectChoices.getValue(true);
        if (!projectId) return;

        const buildsCsv = csv(getSelectedValues(buildChoices)); // ex: "A,B" | "" (ว่าง = ทุกอาคาร)

        // รีเฟรช floors/units ตาม build ที่เลือก (ว่าง = ทุก build)
        setLoading($('#floorMultiSelect'), floorChoices);
        setLoading($('#roomMultiSelect'), unitChoices);

        await loadFloors(projectId, buildsCsv);         // ส่งว่างได้ → คืนทุกชั้น ทุกอาคาร
        // ถ้า floors ยังไม่เลือกอะไร → โหลด units ทั้งหมดของโปรเจกต์ + เงื่อนไข build (หรือทุก build ถ้าว่าง)
        const pairsCsv = csv(getSelectedValues(floorChoices)); // ex: "B-2,B-3" | ""
        await loadUnits(projectId, buildsCsv, pairsCsv);       // pairsCsv ว่างได้ → ทุกยูนิต
    }

    async function onFloorChange() {
        const projectId = projectChoices.getValue(true);
        if (!projectId) return;

        const buildsCsv = csv(getSelectedValues(buildChoices)); // '' = ทุกอาคาร
        const pairsCsv = csv(getSelectedValues(floorChoices)); // '' = ทุกชั้น

        setLoading($('#roomMultiSelect'), unitChoices);
        await loadUnits(projectId, buildsCsv, pairsCsv);        // ว่าง = ทุกยูนิตในโปรเจกต์/บิลด์ที่เลือก
    }

    // bind events (คงเดิม)
    $('#projectSelect').addEventListener('change', onProjectChange);
    $('#buildingMultiSelect').addEventListener('change', onBuildChange);
    $('#floorMultiSelect').addEventListener('change', onFloorChange);

    // initial state
    clearSelect($('#buildingMultiSelect'), buildChoices, '— เลือกอาคาร —');
    clearSelect($('#floorMultiSelect'), floorChoices, '— เลือกชั้น —');
    clearSelect($('#roomMultiSelect'), unitChoices, '— เลือกยูนิต —');

    // ถ้า server preselect project → โหลดทั้งหมดทันที
    const initialProject = projectChoices.getValue(true);
    if (initialProject) { onProjectChange(); }

});
