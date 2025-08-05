// ------------------------ YEAR DROPDOWN ------------------------

function populateYearDropdown() {
    const ddlYear = document.getElementById('ddl_year');
    if (!ddlYear) return;

    const currentYear = new Date().getFullYear();

    ddlYear.innerHTML = ''; // Clear existing options

    for (let i = currentYear - 3; i <= currentYear + 3; i++) {
        const option = document.createElement('option');
        option.value = i;
        option.text = i;

        if (i === currentYear) {
            option.setAttribute('selected', 'selected'); // Mark current year
        }

        ddlYear.appendChild(option);
    }
}

function initYearDropdown() {
    populateYearDropdown();

    new Choices('#ddl_year', {
        removeItemButton: true,
        placeholderValue: 'เลือกปีได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });
}

// ------------------------ QUARTER & MONTH DROPDOWNS ------------------------

let choicesQuarter;
let choicesMonth;

const monthMap = {
    Q1: [1, 2, 3],
    Q2: [4, 5, 6],
    Q3: [7, 8, 9],
    Q4: [10, 11, 12]
};

const monthLabels = {
    1: "Jan", 2: "Feb", 3: "Mar",
    4: "Apr", 5: "May", 6: "Jun",
    7: "Jul", 8: "Aug", 9: "Sep",
    10: "Oct", 11: "Nov", 12: "Dec"
};

function initQuarterDropdown() {
    choicesQuarter = new Choices('#ddl_quarter', {
        removeItemButton: true,
        placeholderValue: 'เลือก Quarterได้มากกว่า 1',
        shouldSort: false
    });

    document.getElementById('ddl_quarter').addEventListener('change', updateMonthDropdownFromQuarter);
}

function initMonthDropdown() {
    choicesMonth = new Choices('#ddl_month', {
        removeItemButton: true,
        placeholderValue: 'เลือกเดือนได้มากกว่า 1',
        shouldSort: false
    });

    updateMonthDropdown([]); // Load all months initially
}

function updateMonthDropdownFromQuarter() {
    const selectedQuarters = choicesQuarter.getValue(true); // array of Q1–Q4
    updateMonthDropdown(selectedQuarters);
}

function updateMonthDropdown(selectedQuarters) {
    let allowedMonths = [];

    if (selectedQuarters.length === 0) {
        allowedMonths = Array.from({ length: 12 }, (_, i) => i + 1);
    } else {
        selectedQuarters.forEach(q => {
            allowedMonths = allowedMonths.concat(monthMap[q] || []);
        });
    }

    allowedMonths = [...new Set(allowedMonths)].sort((a, b) => a - b);

    choicesMonth.clearStore();
    choicesMonth.setChoices(
        allowedMonths.map(m => ({
            value: m,
            label: monthLabels[m],
            selected: false
        })),
        'value',
        'label',
        true
    );
}

// ------------------------ PLAN TYPE & BUG DROPDOWN ------------------------

function initPlanTypeDropdown() {
    new Choices('#ddl_plantype', {
        removeItemButton: true,
        placeholderValue: 'เลือกประเภทแผนได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });
}

let choicesBug;
let choicesProject;

function initBuDropdown() {
        choicesBug = new Choices('#ddl_bug', {
        removeItemButton: true,
        placeholderValue: 'เลือกกลุ่มธุรกิจได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });

    document.getElementById('ddl_bug').addEventListener('change', onBuChanged);
}

// ------------------------ PROJECT ------------------------

function initProjectDropdown() {
    choicesProject = new Choices('#ddl_project', {
        removeItemButton: true,
        placeholderValue: 'เลือกโปรเจกต์ได้มากกว่า 1',
        searchEnabled: true,
        itemSelectText: '',
        shouldSort: false
    });

    loadProjectFromBU(); // load all initially
}

function onBuChanged() {
    const selectedBUs = choicesBug.getValue(true); // array of selected BU IDs
    loadProjectFromBU(selectedBUs);
}

function loadProjectFromBU(selectedBUs = []) {
    const formData = new FormData();
    formData.append("L_BUID", selectedBUs.join(",")); // join IDs or blank if none

    fetch( baseUrl + 'Projecttargetrolling/GetProjectListByBU' , {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(json => {
            if (json.success) {
                const projectList = json.data || [];

                choicesProject.clearStore();
                choicesProject.setChoices(
                    projectList.map(p => ({
                        value: p.ProjectID,
                        label: p.ProjectNameTH
                    })),
                    'value',
                    'label',
                    true
                );
            }
        })
        .catch(error => {
            console.error('Load project failed:', error);
        });
}

// ------------------------ INIT ALL ------------------------

function initAllDropdowns() {
    initYearDropdown();
    initQuarterDropdown();
    initMonthDropdown();
    initPlanTypeDropdown();
    initBuDropdown();
    initProjectDropdown();
}

// 🔥 เรียกเลยโดยไม่ต้องใช้ DOMContentLoaded
initAllDropdowns();



// ------------------------ SEARCH TABLE ------------------------
function collectRollingPlanFilters() {
    return {
        L_Year: $('#ddl_year').val().join(','),
        L_Quarter: choicesQuarter.getValue(true).join(','),
        L_Month: choicesMonth.getValue(true).join(','),
        L_PlanTypeID: $('#ddl_plantype').val() ? $('#ddl_plantype').val().join(',') : '',
        L_Bu: $('#ddl_bug').val() ? $('#ddl_bug').val().join(',') : '',
        L_ProjectID: $('#ddl_project').val() ? $('#ddl_project').val().join(',') : ''
    };
}

function searchRollingPlanData() {
    const filter = collectRollingPlanFilters();

    const formData = new FormData();
    for (const key in filter) {
        formData.append(key, filter[key]);
    }

    fetch(baseUrl + 'Projecttargetrolling/GetDataTableProjectAndTargetRolling', {
        method: 'POST',
        body: formData
    })
        .then(res => res.json())
        .then(json => {
            if (json.success) {
                renderTableFromJson(json.data);
            }
        })
        .catch(err => {
            console.error('Error fetching data:', err);
        });
}

function renderTableFromJson(data) {
    let html = `
        <table id="rollingPlanTable" class="table table-bordered table-striped w-auto">
            <thead>
                <tr>
                    <th>Project Name</th>
                    <th>Plan Type Name</th>
                    <th>Year</th>
                    <th>Jan_Unit</th>
                    <th>Jan_Value</th>
                    <th>Feb_Unit</th>
                    <th>Feb_Value</th>
                    <th>Mar_Unit</th>
                    <th>Mar_Value</th>
                    <th>Apr_Unit</th>
                    <th>Apr_Value</th>
                    <th>May_Unit</th>
                    <th>May_Value</th>
                    <th>Jun_Unit</th>
                    <th>Jun_Value</th>
                    <th>Jul_Unit</th>
                    <th>Jul_Value</th>
                    <th>Aug_Unit</th>
                    <th>Aug_Value</th>
                    <th>Sep_Unit</th>
                    <th>Sep_Value</th>
                    <th>Oct_Unit</th>
                    <th>Oct_Value</th>
                    <th>Nov_Unit</th>
                    <th>Nov_Value</th>
                    <th>Dec_Unit</th>
                    <th>Dec_Value</th>
                </tr>
            </thead>
            <tbody>
    `;

    if (data.length > 0) {
        data.forEach(row => {
            html += `<tr>
               
                <td>${row.ProjectName ?? ''}</td>
                <td>${row.PlanTypeName ?? ''}</td>
                <td>${row.PlanYear ?? ''}</td>
                <td>${row.Jan_Unit ?? '-'}</td>
                <td>${row.Jan_Value ?? '-'}</td>
                <td>${row.Feb_Unit ?? '-'}</td>
                <td>${row.Feb_Value ?? '-'}</td>
                <td>${row.Mar_Unit ?? '-'}</td>
                <td>${row.Mar_Value ?? '-'}</td>
                <td>${row.Apr_Unit ?? '-'}</td>
                <td>${row.Apr_Value ?? '-'}</td>
                <td>${row.May_Unit ?? '-'}</td>
                <td>${row.May_Value ?? '-'}</td>
                <td>${row.Jun_Unit ?? '-'}</td>
                <td>${row.Jun_Value ?? '-'}</td>
                <td>${row.Jul_Unit ?? '-'}</td>
                <td>${row.Jul_Value ?? '-'}</td>
                <td>${row.Aug_Unit ?? '-'}</td>
                <td>${row.Aug_Value ?? '-'}</td>
                <td>${row.Sep_Unit ?? '-'}</td>
                <td>${row.Sep_Value ?? '-'}</td>
                <td>${row.Oct_Unit ?? '-'}</td>
                <td>${row.Oct_Value ?? '-'}</td>
                <td>${row.Nov_Unit ?? '-'}</td>
                <td>${row.Nov_Value ?? '-'}</td>
                <td>${row.Dec_Unit ?? '-'}</td>
                <td>${row.Dec_Value ?? '-'}</td>
            </tr>`;
        });
    } else {
        html += `<tr><td colspan="28" class="text-center">ไม่พบข้อมูล</td></tr>`;
    }

    html += `
            </tbody>
        </table>
    `;

    $('#rolling-plan-container').html(html); // ✅ direct to wrapper
}


$('button:contains("Search")').on('click', function () {
    searchRollingPlanData();
});

