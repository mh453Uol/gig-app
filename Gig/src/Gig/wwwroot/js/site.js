// Write your Javascript code.

//Notifications
$(document).ready(function () {

    handleReadNotifications();

    function handleReadNotifications() {
        $(".js-notification-button").click(function (e) {
            var isUnreadNotification = $(e.target).data("is-unread-notifications");

            if (!isUnreadNotification) { return; }

            var notificationsIds = [];
            $(".js-notifications li[data-notification-id]").map(function () {
                var id = $(this).attr("data-notification-id");
                notificationsIds.push(id);
            });

            var postData = { notifications: notificationsIds };

            $.ajax({
                url: "/api/notification/seen",
                type: "POST",
                data: postData,
                datatype: "json",
                traditional: true,
                success: function () {
                    console.log("success");

                },
                error: function () {
                    console.log("error");
                }
            });
        });
    }
});