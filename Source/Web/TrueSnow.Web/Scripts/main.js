$(document).ready(function () {
    $('.createCommentForm').on('submit', function (e) {
        e.preventDefault();

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (data) {
                var modal = $('div[modal-data-id="' + data.PostId + '"]');

                var row = $("<div></div>").addClass("row");

                var col2 = $("<div></div>").addClass("col-md-2");
                var img = $('<img alt="avatar" class="img-responsive img-circle" style="width: 60px; float: right" />')
                    .attr("src", "/File/" + data.CreatorAvatarId);
                col2.append(img);

                var col10 = $('<div style="margin-left: -20px; padding-top: 8px;"></div>').addClass("col-md-10");
                var a = $("<a></a>").addClass("a-profile").attr("href", "/Profile/" + data.CreatorId).html("<b>" + data.CreatorName + "</b>");
                col10.append(a);
                var small = $("<small></small>").html(" " + data.CreatedOn);
                col10.append(small);
                var p = $('<p style="overflow: auto"></p>').html(data.Content);
                col10.append(p);

                row.append(col2);
                row.append(col10);

                var hr = $('<hr style="margin: 10px 35px;" />');

                modal.append(row);
                modal.append(hr);

                $('.commentsCount-' + data.PostId).html(" " + data.CommentsCount);
                $('.form-control').val('');
            }
        });
    });

    $('span[data-action="like"]').click(function () {
        var id = $(this).attr('data-id');

        $.post(
            '/Likes/Like',
            {
                postId: id
            },
            function (data) {
                $('.likesCount-' + id).html(" " + data.Count);
            });

        $(this).hide();
        $('span[data-action="unlike"][data-id="' + id + '"]').show();
    });

    $('span[data-action="unlike"]').click(function () {
        var id = $(this).attr('data-id');

        $.post(
            '/Likes/Unlike',
            {
                postId: id
            },
            function (data) {
                $('.likesCount-' + id).html(" " + data.Count);
            });

        $(this).hide();
        $('span[data-action="like"][data-id="' + id + '"]').show();
    });

    $('.parallax').parallax();

    $('.tooltipped').tooltip({ delay: 50 });

    $('.button-collapse').sideNav();

    $('ul.tabs').tabs();

    $('select').material_select();

    //$('#profile-content-tabs').pushpin({ top: 670, offset: 60 });

    $('.modal-trigger').leanModal({
        dismissible: true,
        opacity: .5,
        in_duration: 300,
        out_duration: 300
    });

    $("#btnLogout").on('click', function () {
        $('#logoutForm').submit();
    });

    function stickyTabs($elem) {
        $elem.removeClass('fixed');
        var navHeight = $('#main-header').height();
        var winScroll = $(window).scrollTop();
        var threshold = winScroll + navHeight;
        var elemOffset = $elem.offset().top;

        if (elemOffset <= threshold) {
            $elem.addClass('fixed');
            $elem.next('.tabs-content').addClass('stickyTab');
        } else {
            $elem.removeClass('fixed');
            $elem.next('.tabs-content').removeClass('stickyTab');
        }
    }

    $('.profile-actions .discover').click(function () {
        $.scrollTo('#profile-content', 1000, { easing: 'easeInOutExpo', offset: { top: -60, left: 0 } });
    });

    $('#board-info .board-discover').click(function () {
        $.scrollTo('#board-content', 1000, { easing: 'easeInOutExpo', offset: { top: -60, left: 0 } });
    });

    $(window).scroll(function () {
        if ($('.tabs-menu').length != 0) {
            stickyTabs($('.tabs-menu'));
        }
    });
});

$(window).load(function () {
    $('.tabs-content').addClass('moved');

    $('#followLink').click(function (e) {
        e.preventDefault();
        var href = $(this).attr('href');

        swal({
            title: 'Followed!',
            type: 'success',
            allowOutsideClick: true
        }, function () {
            setTimeout(function () {
                window.location = href;
            }, 200);
        });
    });

    $('#unfollowLink').click(function (e) {
        e.preventDefault();
        var href = $(this).attr('href');

        swal({
            title: 'Are you sure?',
            type: 'warning',
            allowOutsideClick: true,
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, unfollow!",
            closeOnConfirm: false
        }, function () {
            swal({
                title: 'Unfollowed!',
                type: 'success',
                allowOutsideClick: true
            }, function () {
                setTimeout(function () {
                    window.location = href;
                }, 200);
            });
        });
    });
});