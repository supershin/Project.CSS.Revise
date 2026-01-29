document.addEventListener("DOMContentLoaded", function () {
    const projectChoice = new Choices("#ddl_Project", {
        searchEnabled: true,
        removeItemButton: true, 
        itemSelectText: "",
        shouldSort: false,
        placeholder: true,
        placeholderValue: "Select project...",
        allowHTML: false
    });

    const CSResponsChoice = new Choices("#ddl_CSReponse", {
        searchEnabled: true,
        removeItemButton: true,
        itemSelectText: "",
        shouldSort: false,
        placeholder: true,
        placeholderValue: "Select Cs reponse...",
        allowHTML: false
    });


    fpRegisterStart = flatpickr("#txt_DateStart", {
        dateFormat: "d/m/Y",
        altInput: false,
        allowInput: true,
        onChange: function (selectedDates) {
            if (fpRegisterEnd && selectedDates.length) {
                fpRegisterEnd.set("minDate", selectedDates[0]);
            }
        }
    });

    fpRegisterEnd = flatpickr("#txt_DateEnd", {
        dateFormat: "d/m/Y",
        altInput: false,
        allowInput: true,
    });
});
