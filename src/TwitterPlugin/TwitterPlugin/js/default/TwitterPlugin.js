// Twitter Setup Start
function InitTwitterSetup() {
    $("#TwitterSetupForm").submit(TwitterSetupFormSubmitted);
}

function TwitterSetupMessage(Text, Error) {
    $("#TwitterSetupMessage").toggleClass('TwitterSetupError', Error).text(Text);
}

function TwitterSetupFormSubmitted() {
    var $form = $("#TwitterSetupForm");
    var username = $("#id", $form).val();
    if (!username || !username.length || username.length < 1) {
        TwitterSetupMessage("User name is required..", true);
    }
    var wantedLimit = $("#limit", $form).val()
    TwitterSetupMessage("Loading..", false);
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
        success: BackFromSetup,
        error: SetupFailed
    });
    return false;
}

function SetupFailed(data) {
    $(":input", "#TwitterSetupForm").attr('disabled', '');
    TwitterSetupMessage("Invalid user..", true);
}

function BackFromSetup(data) {
    if (!data || !data.UserName) {
        SetupFailed(data);
        return;
    }
    $(":input", "#TwitterSetupForm").attr('disabled', '');
    $("#id", "#TwitterSetupForm").val(data.UserName);
    $("#limit", "#TwitterSetupForm").val(data.Limit);
    TwitterSetupMessage("Success..", false);
}

// Twitter Setup End

// Twitter Client Start


function InitTwitterClient() {

}

// Twitter Client End
