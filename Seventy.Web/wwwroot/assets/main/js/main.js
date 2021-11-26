
$(document).ready(function () {

    $(".carousel-caption h5").addClass('animated slideInLeft');

    $(".carousel-caption p").addClass('animated slideInRight');

    $('select').select2({
        dir: 'rtl',
        allowClear: true,
        width: '100%'
    });

    $('.typing1').inewsticker({
        speed: 100,
        effect: 'typing',
        dir: 'rtl',
        font_size: 48,
        font_family: null,
        display: 'inline-block',
        delay_after: 0,
    });
    $('.typing2').inewsticker({
        speed: 100,
        effect: 'typing',
        dir: 'rtl',
        font_size: 48,
        font_family: null,
        display: 'inline-block',
        delay_after: 0,
    });
    $('.typing3').inewsticker({
        speed: 100,
        effect: 'typing',
        dir: 'rtl',
        font_size: 48,
        font_family: null,
        display: 'inline-block',
        delay_after: 0,
    });

    $('.owl-carousel').owlCarousel({
        rtl: true,
        loop: true,
        margin: 10,
        nav: true,
        autoplay: true,
        autoplayTimeout: 2500,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1000: {
                items: 5
            }
        }
    });


    $('.navbar .nav-link, .navbar-brand').click(function () {
        var sectionTo = $(this).attr('href');
        $('html, body').animate({
            scrollTop: $(sectionTo).offset().top - 70
        }, 600);
    });
});


