// Write your Javascript code.

//Notifications
$(document).ready(function () {

    handleReadNotifications();

    function handleReadNotifications() {
        $(".js-notification-button").click(function (e) {
            var button = $(e.target);
            var isUnreadNotification = $(".js-notification-button")
                .attr("data-is-unread-notifications");

            if (isUnreadNotification == "false") { return; }

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
                    $(".js-notification-button").data("is-unread-notifications", false);
                    $(".js-notification-label").fadeOut();
                },
                error: function () {
                    console.log("error");
                }
            });
        });
    }
});