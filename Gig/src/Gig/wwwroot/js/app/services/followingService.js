var FollowingService = function () {
    var follow = function (artistId, success, error) {
        $.post("/api/following/follow", { FolloweeId: artistId })
            .success(success)
            .error(error);

    };

    var unfollow = function (artistId, success, error) {
        $.ajax({
            url: "/api/following/unfollow?followeeId=" + artistId,
            type: "DELETE",
            success: success,
            error: error
        });
    };

    return {
        follow: follow,
        unfollow: unfollow
    };
}();