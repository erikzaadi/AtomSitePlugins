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
        var wantedLimit = $("#limit", $form).val()
        _TwitterSetupMessage("Loading..", false);
        $(":input", $form).attr('disabled', 'disabled');
        $.ajax(
    {
        url: $form.attr('action'),
        data:
            {
                id: username,
                limit: wantedLimit
            },
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
        if (!data || !data.UserName) {
            SetupFailed(data);
            return;
        }
        $(":input", "#TwitterSetupForm").attr('disabled', '');
        $("#id", "#TwitterSetupForm").val(data.UserName);
        $("#limit", "#TwitterSetupForm").val(data.Limit);
        _TwitterSetupMessage("Success..", false);
    }

    // Twitter Setup End

    // Twitter Client Start


    TwitterPlugin.InitTwitterClient = function() {
        //TODO : Add "More" button which fetches more tweets..
    };

    // Twitter Client End
})(jQuery);
