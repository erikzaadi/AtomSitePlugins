// GA4AtomSite Setup Start
function InitGA4AtomSiteSetup() {
    $("#GA4AtomSiteSetupForm").submit(GA4AtomSiteSetupFormSubmitted);
}

function GA4AtomSiteSetupMessage(Text, Error) {
    $("#GA4AtomSiteSetupMessage").toggleClass('GA4AtomSiteSetupError', Error).text(Text);
}

function GA4AtomSiteSetupFormSubmitted() {
    var $form = $("#GA4AtomSiteSetupForm");
    var username = $("#GAID", $form).val();
    if (!username || !username.length || username.length < 1) {
        GA4AtomSiteSetupMessage("Google Analytics Account ID required..", true);
    }
    GA4AtomSiteSetupMessage("Loading..", false);
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
        success: GA4AtomSiteBackFromSetup,
        error: GA4AtomSiteSetupFailed
    });
    return false;
}

function GA4AtomSiteSetupFailed(data) {
    $(":input", "#GA4AtomSiteSetupForm").attr('disabled', '');
    GA4AtomSiteSetupMessage("Invalid Account ID..", true);
}

function GA4AtomSiteBackFromSetup(data) {
    if (!data || !data.success) {
        SetupFailed(data);
        return;
    }
    $(":input", "#GA4AtomSiteSetupForm").attr('disabled', '');
    $("#GAID", "#GA4AtomSiteSetupForm").val(data.GAID);
    GA4AtomSiteSetupMessage("Success..", false);
}

// GA4AtomSite Setup End

