let today = moment().format("YYYY-MM-DD");
let tomorrow = moment().add(1, 'days').format("YYYY-MM-DD");
var selectedOrigin;
var selectedDestination;
$("#departureDate").val(tomorrow);
$(document).ready(function () {

    $("#departureDate").attr("min", today);

    $("#departureDate").val(tomorrow)

    $("#origin").select2({
        theme: 'bootstrap4',
    });

    var busLocationArray = [];

    $("#origin > option").each(function () {
        busLocationArray.push({
            "id": this.value,
            "text": this.text
        });
    });

    $("#destination").select2({
        theme: 'bootstrap4',
    });

    $('#origin').on("select2:select", function (e) {
        $("#destination").select2().empty();
        var filtered = busLocationArray.filter(function (item) {
            return item.id !== $('#origin').val();
        });
        $("#destination").select2({
            theme: 'bootstrap4',
            data: filtered
        });
    });

    $('#origin').val(localStorage.getItem("origin")).trigger('change')
    $('#destination').val(localStorage.getItem("destination")).trigger('change')
    $('#departureDate').val(localStorage.getItem("departureDate"));

    if (localStorage.getItem("destination") === "") {
        $('#origin').val(349).trigger('change')
        $('#destination').val(356).trigger('change')
        $("#departureDate").val(tomorrow);
    }
    selectedDay();
});

function getToday() {
    document.getElementById("today").classList.add("selected-day");
    document.getElementById("tomorrow").classList.remove("selected-day");
    $("#departureDate").val(today);
    localStorage.setItem("departureDate", today);
}

function getTomorrow() {
    document.getElementById("tomorrow").classList.add("selected-day");
    document.getElementById("today").classList.remove("selected-day");
    $("#departureDate").val(tomorrow);
    localStorage.setItem("departureDate", tomorrow);
}

function changeLocation() {

    selectedOrigin = $("#origin option:selected").val();
    selectedDestination = $("#destination option:selected").val();
    $('#destination').val(selectedOrigin).trigger('change')
    $('#origin').val(selectedDestination).trigger('change')
}

function selectedDay() {
    if ($("#departureDate").val() === today) {
        document.getElementById("today").classList.add("selected-day");
        document.getElementById("tomorrow").classList.remove("selected-day");
    }
    else if ($("#departureDate").val() === tomorrow) {
        document.getElementById("tomorrow").classList.add("selected-day");
        document.getElementById("today").classList.remove("selected-day");
    }
    else {
        document.getElementById("tomorrow").classList.remove("selected-day");
        document.getElementById("today").classList.remove("selected-day");
    }
}

$('#departureDate').change(function () {
    localStorage.setItem("departureDate", this.value);
    selectedDay();
});

$('#origin').change(function () {
    localStorage.setItem("origin", this.value);
});

$('#destination').change(function () {
    localStorage.setItem("destination", this.value);
});