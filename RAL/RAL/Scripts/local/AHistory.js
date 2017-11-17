function setAHsitoryTable() {
    setAHistoryTableHeader();
    setAHistoryColumnsAlign();
    setAHistoryTableBody();
    setAHistoryTableTooltips();
    setAHistoryTableOptions();
    setAHistoryTableFooter();
    resetAHistoryIndices();
}

function setAHistoryTableHeader() {
    $('#ahistory-table th').addClass('table-header1');
    $('#ahistory-table th').css('border-bottom', '0.5px solid rgb(28, 30, 34)');
    $('#ahistory-table th').css('padding-top', '0.8em');
    $('#ahistory-table th').css('padding-bottom', '0.8em');
    $('#ahistory-table th').css('padding-left', '2em');
    $('#ahistory-table th').first().css('border-radius', '6px 0 0 0');
    $('#ahistory-table th').last().css('border-radius', '0 6px 0 0');
}

function setAHistoryColumnsAlign() {
    $('#ahistory-table th').addClass('text-center');
    $('#ahistory-table tbody td').addClass('text-center');

    $('#ahistory-table thead :nth-child(1)').addClass('text-left');
    $('#ahistory-table thead :nth-child(2)').addClass('text-left');
    $('#ahistory-table thead :nth-child(3)').addClass('text-left');
    $('#ahistory-table tbody :nth-child(1)').addClass('text-left');
    $('#ahistory-table tbody :nth-child(2)').addClass('text-left');
    $('#ahistory-table tbody :nth-child(3)').addClass('text-left');
}

function setAHistoryTableBody() {
    $('#ahistory-table a').css('text-decoration', 'none');
    $('#ahistory-table tbody td').css('padding-left', '2em');

    $('#ahistory-table thead :nth-child(10)').css('padding-right', '2em');
    $('#ahistory-table tbody :nth-child(10)').css('padding-right', '2em');

    $('#ahistory-table thead :nth-child(2)').css('width', '18em');
    $('.aname-td').css('width', '18em');

    $('#ahistory-table thead :nth-child(9)').css('width', '7em');
    $('#astatus-td').css('width', '7em');
}

function setAHistoryTableTooltips() {
    //set header tooltips
    $('#ahistory-table th span').css('margin-left', '-90px');
    $('#ahistory-table th span:eq(3)').css('margin-left', '-110px');
    $('#ahistory-table th span:eq(4)').css('margin-left', '-120px');
    $('#ahistory-table th span:eq(5)').css('margin-left', '-120px');
}

function setAHistoryTableOptions() {
    $('form[action$="/DeleteWatcheda"] .btn').css('width', '7em');

    $('#ahistory-table span.aid-hid').css('display', 'none');
    $('#ahistory-table span.aname-hid').css('display', 'none');

    // pass parameters to delete form
    $('#ahistory-table button.delete-watcheda').click(function () {
        var optionsDiv = $(this).parent().parent();

        //aid_2del
        var a2del_id = $('span.aid-hid', optionsDiv).text();
        $('#a2del_id').attr('value', a2del_id);

        //aid_2del
        var a2del_name = $('span.aname-hid', optionsDiv).text();
        $('#a2del_name').attr('value', a2del_name);
    });
}

function setAHistoryTableFooter() {
    //well color
    var color = $('#ahistory-table tbody :nth-child(1) :eq(1)').css('background');
    $('#ahistory-table .well').css('background', color);

    //well width
    $('#ahistory-table .well').css('margin', 'auto');
    $('#ahistory-table .well').css('width', 'auto');
    $('#ahistory-table .well div').css('width', 'auto');
    
    setAHistoryWellContent();
}

function setAHistoryWellContent() {
    movies = getMoviesCount();
    tvova = getTVOVACount();
    episodes = getEpisodeCount();

    $('.well .badge:eq(0)').text(movies);
    $('.well .badge:eq(1)').text(tvova);
    $('.well .badge:eq(2)').text(episodes);
}

function setAHistorySortBindings() {
   
    $.tablesorter.addParser({
        id: "dateParser",
        is: function (s) {
            return false;
        },
        format: function (s) {
            var dateString = s.replace(/^\s+|\s+$/g, '');

            var date = new Date();
            date.setYear(parseInt(dateString.substr(6, 10), 10));
            date.setMonth(parseInt(dateString.substr(3, 2), 10)-1);
            date.setDate(parseInt(dateString.substr(0, 2), 10));
            date.setHours(parseInt(dateString.substr(11, 2), 10));
            date.setMinutes(parseInt(dateString.substr(14, 2), 10));

            return $.tablesorter.formatFloat(date.getTime());
        },
        type: "numeric"
    });
    
    $("#ahistory-table").tablesorter({
        // set default sort by 6 col
        sortList: [[5, 0]],

        headers: { 
            // assign the first column (we start counting zero) 
            0: { 
                // disable it by setting the property sorter to false 
                sorter: false 
            },
            5: {
                sorter: "dateParser"
            },
            6: {
                sorter: "dateParser"
            },
            9: { 
                sorter: false 
            } 
        } 
    }); 

    $("#ahistory-table").bind("sortEnd", function () {
        resetAHistoryIndices();
    });

}

function unbindTablesorter() {
    $('#ahistory-table')
    .unbind('appendCache applyWidgetId applyWidgets sorton update updateCell')
    .removeClass('tablesorter')
    .find('thead th')
    .unbind('click mousedown')
    .removeClass('header headerSortDown headerSortUp');
}

function updateAHistoryTable(response) {
    $('#AHistoryBody').html(response);
    setAHsitoryTable();

    // let the tablesorter plugin know that we made a update 
    $("#ahistory-table").trigger("update");
}

function redirectToAnotherAHistoryTable(response) {
    var curUrl = window.location.pathname;
    var newAnimeHasStatDropped = false;

    if (response.indexOf('<span>Dropped') != -1) {
        newAnimeHasStatDropped = true;
    }
    
    if (newAnimeHasStatDropped && curUrl != '/AHistory/Dropped') {
        postSetNewWatcheda(response);
        window.location = '/AHistory/Dropped';
        return true;
    }
    else if (!newAnimeHasStatDropped && curUrl != '/AHistory') {
        postSetNewWatcheda(response);
        window.location = '/AHistory';
        return true;
    }

    return false;
}

function getAidFromResponse(response) {
    var string2Search = '"aid-hid">';
    var newWatchedaId = response.substring(response.indexOf(string2Search) + string2Search.length);
    return newWatchedaId.substring(0, response.indexOf('<') - 1);
}

function postSetNewWatcheda(response) {
    var requestString = "/AHistory/setNewWatcheda?id=" + getAidFromResponse(response);

    $.post(requestString, function (data) { });
}

function resumeAHistoryTable() {
    $('#ahistory-table').css('display', 'inline');
    $('.main-container').css('height', 'auto');
}

function addAHistoryTableRow(row, dateStr) {
    tableSortDesc();

    //$("#ahistory-table tbody").append(row);
    $("#ahistory-table :nth-child(" + getRowNum2AppendInMiddle() + ")").after(row);
    setAHsitoryTable();

    // let the tablesorter plugin know that we made a update 
    $("#ahistory-table").trigger("update");

    tableSortDesc();
}

function getRowNum2Append(dateStr) {
    var newRowTime = getDateFromString(dateStr);

    var n = 0;
    for (i = 0; i < $('#ahistory-table tr').length; i++) {
        var curTime = getDateFromAHistoryTableRow(i);
        if (newRowTime == curTime) { continue; }
        if (newRowTime < curTime) { n = i; break; }
    }

    return n;
}

/*
function getRowNum2Append() {
    var newRowIndex = $('#ahistory-table tr.emphasis').index();
    var newRowTime = getDateFromAHistoryTableRow(newRowIndex);

    var n = 0;
    for (i = 0; i < $('#ahistory-table tr').length; i++) {
        var curTime = getDateFromAHistoryTableRow(i);
        if (newRowTime == curTime) { continue; }
        if (newRowTime < curTime) { n = i; break; }
    }

    return n;
}
*/

function getRowNum2AppendInMiddle() {
    var rowCount = parseInt($('#ahistory-table tr').length / 2, 10);

    if (rowCount < 1) {
        rowCount = 1;
    }

    return rowCount;
}

function sortTableByStartDate() {
    $('#ahistory-table thead :nth-child(6)').click();
}

function tableSortAsc() {
    var first = getDateFromAHistoryTableRow(0);
    var second = getDateFromAHistoryTableRow(1);

    // if sort is descending sort again
    if (first > second) {
        sortTableByStartDate();
    }
}

function tableSortDesc() {
    sortTableByStartDate();

    var first = getDateFromAHistoryTableRow(0);
    var second = getDateFromAHistoryTableRow(1);

    // if sort is ascending sort again
    if (first < second) {
        sortTableByStartDate();
    }
}

function getDateFromAHistoryTableRow(rowNumber) {
    // get string from row in table
    var dateInTD = $("#ahistory-table tbody tr:eq(" + rowNumber + ") td:eq(5)").html().replace(/^\s+|\s+$/g, '');

    // parse string from row in table
    var date = new Date();
    date.setYear(parseInt(dateInTD.substr(6, 10), 10));
    date.setMonth(parseInt(dateInTD.substr(3, 2), 10) - 1);
    date.setDate(parseInt(dateInTD.substr(0, 2), 10));
    date.setHours(parseInt(dateInTD.substr(11, 2), 10));
    date.setMinutes(parseInt(dateInTD.substr(14, 2), 10));

    return date.getTime();
}

function getDateFromString(dateStr) {
    // parse string from row in table
    var date = new Date();
    date.setYear(parseInt(dateStr.substr(6, 10), 10));
    date.setMonth(parseInt(dateStr.substr(3, 2), 10) - 1);
    date.setDate(parseInt(dateStr.substr(0, 2), 10));
    date.setHours(parseInt(dateStr.substr(11, 2), 10));
    date.setMinutes(parseInt(dateStr.substr(14, 2), 10));

    return date.getTime();
}

function deleteAHistoryTableRow(aid2del) {
    var tr2del = $('#ahistory-table tr:has(span.aid-hid:contains(' + aid2del + '))').first();
    tr2del.remove();

    // let the tablesorter plugin know that we made a update 
    $("#ahistory-table").trigger("update");
    resetAHistoryIndices();

    setAHistoryWellContent();
}

function scrollToAHistoryTableRow() {
    setTimeout('document.getElementById("emphasis").scrollIntoView();', 800);
}

function getMoviesCount() {
    var count = 0;

    $('#ahistory-table tbody tr').each(function () {
        var type = $(':nth-child(3)', this).first().text().replace(/^\s+|\s+$/g, '');
        if (type.indexOf("Movie") >= 0) {
            count++;
        }
    });

    return count;
}

function getTVOVACount() {
    var count = 0;

    $('#ahistory-table tbody tr').each(function () {
        var type = $(':nth-child(3)', this).first().text().replace(/^\s+|\s+$/g, '');
        if (type.indexOf("Movie") < 0) {
            count++;
        }
    });

    return count;
}

function getEpisodeCount() {
    var count = 0;

    $('#ahistory-table tbody tr').each(function () {
        var type = $(':nth-child(3)', this).first().text().replace(/^\s+|\s+$/g, '');
        if (type.indexOf("Movie") < 0) {
            count += parseInt($(':nth-child(5)', this).text().replace(/^\s+|\s+$/g, ''));
        }
    });

    return count;
}

function resetAHistoryIndices() {
    var i = 1;
    $('#ahistory-table tbody tr').each(function () {
        $(':nth-child(1)', this).first().text(i++);
    });
}

function showEmptyListNotif() {
    var emptyListNotif =
        '<div class="empty-list-notif">' +
            '<br />' +
            '<h3>This watching list is empty.</h3>' +
            '<h3>Fill it to browse your anime watching history.</h3>' +
        '</div>';
    $('.table-content').first().append(emptyListNotif);

    //hide ahistory table
    $('#ahistory-table').css('display','none');
}