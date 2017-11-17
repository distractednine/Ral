function setMainNavbar() {
    $('.navbar').first().css('margin-right', 'auto');
    $('.navbar').first().css('margin-left', 'auto');
    $('.col-lg-3').first().css('padding', '0.1em');
    $('.nav li').css('width', '10em');
    $('.nav li').css('text-align', 'center');
    $('.nav li :eq(4)').css('margin-left', '-1.3em');
    $('.nav li :eq(4)').css('cursor', 'pointer');
    $('.nav li :eq(4)').toggle();
    $('.dropdown-menu:eq(0) li').css('width', 'auto');
    $('.navbar-nav:eq(0) li a').css('height', '55px');
    $('.navbar-nav:eq(2) li a').css('height', '50px');
    $('.navbar-nav:eq(0) li a').css('font-size', '15px');
    $('.navbar-nav:eq(2) li a').css('font-size', '14px');

    $('.col-lg-5 .nav li').css('cursor', 'pointer');
}

function setInvalidUrlPage() {
    setUsernameDisplaying();
    setMainDivHeight();
    setBigButtons();
}

function setBigButtons() {
    $('.btn-primary').css('width', '6em');
}

function mainMenuButtonItalizing(str) {
    $('li a:contains(' + str + ')').parent().addClass('active');
}

// if there are not enough elements on page
function setMainDivHeight() {
    $('.main-container').css('height', '37em');
}

function setUsernameDisplaying() {
    $('.nav li :eq(4)').toggle();
    var uname = $('#uname-container').html();
    //$('#uname-container').hide();
    $('#username').html(uname + ' ');
}

function setNotification() {
    $('.alert p').css('cursor', 'pointer');

    $('#secondary-buttons').children().click(function () {
        $('.alert .close').click();
    });

    $('.options-td button').click(function () {
        $('.alert .close').click();
    });

    $('.alert p').click(function () {
        $('.alert .close').click();
    });

    $('.close').click(function (e) {
        e.preventDefault();

        $(this).parent().remove();

        removeEmphasis();
    });
}

function addNotification(message) {
    var notif = '<div class="alert label-primary self-center notif-tooltip">' +
                    '<a class="close" href="#">×</a><p>' + message +
                '</p></div>';

    $('#notif-area').html(notif);
    setNotification();
}

function addEmphasis(elem1, elem2) {
    removeEmphasis();

    var tr2highlight = $('#ahistory-table tr:contains(' + elem1 + '):contains(' + elem2 + ')');

    tr2highlight.addClass('emphasis');
    
    tr2highlight.attr('id', 'emphasis');
}

function removeEmphasis() {
    $('#emphasis').attr('id', '');
    $('#ahistory-table .emphasis').removeClass('emphasis');
}