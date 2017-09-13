var AttendanceService = function () {

    var attendGig = function (gigId, toggleButton, onError) {
        $.post("/api/attendances", { gigId: gigId })
            .success(toggleButton)
            .error(onError);
    }

    var removeAttendance = function (gigId, toggleButton, onError) {
        $.ajax({
            url: "/api/attendances?id=" + gigId,
            method: "DELETE",
            success: toggleButton,
            error: onError
        });
    }

    return {
        attendGig: attendGig,
        removeAttendance: removeAttendance
    }
}();