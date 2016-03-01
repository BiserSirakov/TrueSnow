$(document).ready(function () {
    $('ul.tabs').tabs();

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
        };
    }

    $('.profile-actions .discover').click(function () {
        $.scrollTo('#profile-content', 1000, { easing: 'easeInOutExpo', offset: { top: -60, left: 0 } });
    });

    $('#board-info .board-discover').click(function () {
        $.scrollTo('#board-content', 1000, { easing: 'easeInOutExpo', offset: { top: -60, left: 0 } });
    });

    $('.datepicker').pickadate();

    $('select').material_select();

    function customParallax($elem) {
        var speed = 0.3;
        var offsetY = $elem.offset().top;
        var winH = $(window).height(); 
        var scrollTop = $(window).scrollTop();
        var movement = winH * speed;
        var check = offsetY - scrollTop;
        var percentuale = check / winH;
        var newPosition = +(percentuale * movement);
        newPosition = newPosition + 'px'; $elem.css({ 'background-position': '50% ' + newPosition });
    }

    $('.custom-parallax').each(function () {
        customParallax($(this));
    });

    $(window).scroll(function () {
        if ($('.tabs-menu').length != 0) {
            stickyTabs($('.tabs-menu'));
        }

        $('.custom-parallax').each(function () {
            customParallax($(this));
        });
    });
});

$(window).load(function () {
    if ($('#stream').length > 0) {
        var container = document.querySelector('#stream');
        var msnry = new Masonry(container, { itemSelector: '.col' });
    }

    $('.tabs-content').addClass('moved');
});