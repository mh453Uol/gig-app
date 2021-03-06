﻿var GigsController = function (attendanceService) {

    var button;

    var initialize = function (container) {
        $(container).on("click", ".js-toggle-attendance", onToggleAttendance);
    }

    var onToggleAttendance = function (e) {

        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default")) {
            attendanceService
                .attendGig(gigId,
                toggleButton, onError);
        } else {
            attendanceService
                .removeAttendance(gigId,
                toggleButton, onError);
        }
    }

    var onError = function () {
        alert("Something went wrong");
    }

    var toggleButton = function () {
        var text = (button.text() == "Going") ? "Going ?" : "Going";
        //$(button).toggleClass("btn-info").toggleClass("btn-default").text(text);
        if ($(button).text().trim() == "Going") {
            $(button).removeClass("btn-info").addClass("btn-default").text("Going ?");
        } else {
            $(button).removeClass("btn-default").addClass("btn-info").text("Going");
        }
    };

    return {
        initialize: initialize
    }

}(AttendanceService);
