function gtagHelperAI()
{
    gtag('event', 'click', { 'event_category': 'AddItems', 'event_label': 'NewItem' });
}
function CallQueryAndGtag() {
    let sellerObj = {"Id":0,"ShopName": document.getElementById("name").value,"Email": document.getElementById("Input_Email").value,"Address": document.getElementById("address").value,"Contact": document.getElementById("number").value,"Password": document.getElementById("Input_Password").value}
    let sellerString = JSON.stringify(sellerObj);
    $.ajax({
        async: false,
        type: "POST",
        data: JSON.stringify(sellerObj),
        contentType: "application/json",
        url: `https://localhost:44390/api/online/addNewSeller`
    });
    gtag('event', 'click', { 'event_category': 'SFS', 'event_label': 'CA' });
}
function CallUpdate(sellerEmail) {
    let sellerObj = GetSeller(sellerEmail)
    sellerObj['contact'] = document.getElementById('Input_PhoneNumber').value;
    $.ajax({
        async: false,
        type: "PUT",
        data: JSON.stringify(sellerObj),
        contentType: "application/json",
        url: `https://localhost:44390/api/online/updateSeller/${sellerObj['id']}`
    });
}
function CallUpdatePassword(sellerEmail) {
    let oldPassword = document.getElementById('Input_OldPassword').value;
    let newPassword = document.getElementById('Input_NewPassword').value;
    let confirmPassword = document.getElementById('Input_ConfirmPassword').value;
    let sellerObj = GetSeller(sellerEmail)
    if (oldPassword==sellerObj['password'] && newPassword == confirmPassword) {
        sellerObj['password'] = newPassword
        $.ajax({
            async: false,
            type: "PUT",
            data: JSON.stringify(sellerObj),
            contentType: "application/json",
            url: `https://localhost:44390/api/online/updateSeller/${sellerObj['id']}`
        });
    }
}
function DeleteSeller(sellerEmail) {
    let password = document.getElementById('Input_Password').value;
    let sellerObj = GetSeller(sellerEmail)
    if (password == sellerObj['password']) {
        $.ajax({
            async: false,
            type: "DELETE",
            url: `https://localhost:44390/api/online/removeSeller/${sellerObj['id']}`
        });
    }
}
function FillWithPhoneNumber(sellerEmail) {
    if (sellerEmail != '' && sellerEmail != null) {
        let phoneNoElem = document.getElementById("Input_PhoneNumber");
        let sellerObj = GetSeller(sellerEmail);
        phoneNoElem.value = sellerObj["contact"];
    }
}
function GetSeller(sellerEmail) {
    let sellerObj = {};
    $.ajax({
        async: false,
        type: "GET",
        url: `https://localhost:44390/api/online/getSeller/${sellerEmail}`,
        success: function (data) {
            sellerObj = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(`${textStatus}: ${errorThrown}`);
        }
    });
    return sellerObj;
}