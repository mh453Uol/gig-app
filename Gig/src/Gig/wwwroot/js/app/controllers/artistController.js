var ArtistController = function (followingService) {

    var button, artistId;

    var initialize = function (container) {

        $(container).on("click",".js-toggle-following",onToggleFollowing);
    };

    var onToggleFollowing = function (e) {

            button = $(e.target);

            artistId = button.attr("data-artist-id");

            if (button.hasClass("btn-default")) {

                followingService.follow(artistId, toggleButton, onError);
            }
            else {
                followingService.unfollow(artistId, toggleButton, onError);
            }
    }

    var toggleButton = function () {
        var text = "";

        if ($(button).text().trim() == "Following Artist") {
            text = "Follow Artist";
        } else {
            text = "Following Artist";
        }

        //console.log(text, 1 + $(button).text().trim());

        $(button).toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    var onError = function () {
        alert("Something went wrong");
    }

    return {
        initialize: initialize
    }

}(FollowingService);