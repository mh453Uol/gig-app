var ArtistController = function (followingService) {

    var button, artistId;

    var initialize = function (container) {

        $(container).on("click",".js-toggle-following",onToggleFollowing);
    };

    var onToggleFollowing = function (e) {

            button = $(e.target);

            artistId = button.attr("data-artist-id");

            if (button.hasClass("btn-default")) {

                followingService.follow(artistId, onFollowToggle, onError);
            }
            else {
                followingService.unfollow(artistId, onUnfollowToggle, onError);
            }
    }

    var onUnfollowToggle = function () {
        $(".js-toggle-following.btn-info[data-artist-id=" + artistId + "]").each(toggleButton);
    }

    var onFollowToggle = function () {
        $(".js-toggle-following.btn-default[data-artist-id=" + artistId + "]").each(toggleButton);
    }


    var toggleButton = function (index,element) {
        var text = $(element).text() == "Following Artist" ? "Follow Artist" : "Following Artist";

        $(element).toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    var onError = function () {
        alert("Something went wrong");
    }

    return {
        initialize: initialize
    }

}(FollowingService);