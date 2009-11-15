//Scoping the plugin's javascript to avoid namespace conflicts
var GA4AtomSite = [];
; (function($) {
    // GA4AtomSite Setup Start
    GA4AtomSite.InitGA4AtomSiteSetup = function() {
        $("#GA4AtomSiteSetupForm").submit(_GA4AtomSiteSetupFormSubmitted);
    };

    function _GA4AtomSiteSetupMessage(Text, Error) {
        $("#GA4AtomSiteSetupMessage").toggleClass('GA4AtomSiteSetupError', Error).text(Text);
    }

    function _GA4AtomSiteSetupFormSubmitted() {
        var $form = $("#GA4AtomSiteSetupForm");
        var username = $("#GAID", $form).val();
        if (!username || !username.length || username.length < 1) {
            _GA4AtomSiteSetupMessage("Google Analytics Account ID required..", true);
            return false;
        }
        _GA4AtomSiteSetupMessage("Loading..", false);
        $(":input", $form).attr('disabled', 'disabled');
        $.ajax(
    {
        url: $form.attr('action'),
        data:
            {
                GAID: username
            },
        dataType: 'json',
        type: 'post',
        success: _GA4AtomSiteBackFromSetup,
        error: _GA4AtomSiteSetupFailed
    });
        return false;
    }

    function _GA4AtomSiteSetupFailed(data) {
        $(":input", "#GA4AtomSiteSetupForm").attr('disabled', '');
        _GA4AtomSiteSetupMessage("Invalid Account ID..", true);
    }

    function _GA4AtomSiteBackFromSetup(data) {
        if (!data || !data.success) {
            _GA4AtomSiteSetupFailed(data);
            return;
        }
        $(":input", "#GA4AtomSiteSetupForm").attr('disabled', '');
        $("#GAID", "#GA4AtomSiteSetupForm").val(data.GAID);
        _GA4AtomSiteSetupMessage("Success..", false);
    }

    // GA4AtomSite Setup End
})(jQuery);