//Scoping the plugin's javascript to avoid namespace conflicts
var TwitterPlugin = [];
; (function($) {

    // Twitter Setup Start
    TwitterPlugin.InitTwitterSetup = function() {
        $("#TwitterSetupForm").submit(_TwitterSetupFormSubmitted);
    };

    function _TwitterSetupMessage(Text, Error) {
        $("#TwitterSetupMessage").toggleClass('TwitterSetupError', Error).text(Text);
    }

    function _TwitterSetupFormSubmitted() {
        var $form = $("#TwitterSetupForm");
        var username = $("#id", $form).val();
        if (!username || !username.length || username.length < 1) {
            _TwitterSetupMessage("User name is required..", true);
            return false;
        }
        _TwitterSetupMessage("Loading..", false);
        $(":input", $form).attr('disabled', 'disabled');
        $.ajax(
    {
        url: $form.attr('action'),
        data: $.param($(":input", $form)),
        dataType: 'json',
        type: 'post',
        success: _BackFromSetup,
        error: _SetupFailed
    });
        return false;
    }

    function _SetupFailed(data) {
        $(":input", "#TwitterSetupForm").attr('disabled', '');
        _TwitterSetupMessage("Invalid user..", true);
    }

    function _BackFromSetup(data) {
        if (!data || !data.UserName)
            return _SetupFailed(data);
        $(":input", "#TwitterSetupForm").attr('disabled', '');
        $("#id", "#TwitterSetupForm").val(data.UserName);
        $("#limit", "#TwitterSetupForm").val(data.Limit);
        _TwitterSetupMessage("Success..", false);
    }

    // Twitter Setup End

    // Twitter Client Start


    TwitterPlugin.InitTwitterClient = function() {
        var refreshInMinutes = $("#TwitterClientForm #ClientRefreshDuration").val();
        if (refreshInMinutes && !isNaN(refreshInMinutes)) {
            _Interval = setInterval(function() { _RefreshTwitterClient(); }, 1000 * 60 * refreshInMinutes);
        }
        $("#TwitterClientForm").submit(_TwitterClientSubmitted);
        $(".TwitterMoreButton").focus(function() { $(this).blur(); });
    };

    var _Interval;

    function _TwitterClientSubmitted() {
        clearInterval(_Interval);
        if ($(".TwitterMoreButton").hasClass('TwitterMoreButtonLoading'))
            return false;
        $(".TwitterMoreButton").addClass('TwitterMoreButtonLoading');
        var $form = $(this);
        $.ajax(
        {
            url: $form.attr('action'),
            data: $.param($(":input", $form)),
            dataType: 'html',
            type: 'get',
            success: _ClientRefreshed,
            error: _SetupFailed
        });
        return false;
    }

    function _ClientFailed(data) {
        $(".TwitterMoreButton").removeClass('TwitterMoreButtonLoading');
    }
    function _RefreshTwitterClient() {
        $(".TwitterMoreButton").addClass('TwitterMoreButtonLoading');
        $("#PagingIndex").val(0);
        $.get($("#TwitterClientForm").attr('action'), {}, _ClientRefreshed);
    }
    function _ClientRefreshed(data) {
        if (!data)
            return _ClientFailed();
        var temp = $("#PagingIndex").val();
        var paging = temp ? parseInt(temp) : 2;
        $("#PagingIndex").val(isNaN(paging) ? 2 : ++paging);
        var $toAppend = $('<div style="display:none" />');
        $toAppend.fadeOut()
            .append(data);
        $(".TwitterWidget .TwitterStatuses")[paging < 2 ? 'html' : 'append']($toAppend);
        $toAppend.slideToggle();
        $(".TwitterMoreButton").removeClass('TwitterMoreButtonLoading');
    }
    // Twitter Client End
})(jQuery);
