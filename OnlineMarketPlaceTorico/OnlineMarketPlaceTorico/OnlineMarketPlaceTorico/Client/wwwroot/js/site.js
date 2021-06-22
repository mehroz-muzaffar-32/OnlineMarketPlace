function gtagHelperAI()
{
    gtag('event', 'click', { 'event_category': 'AddItems', 'event_label': 'NewItem' });
}
function CallQueryAndGtag() {
    let sellerObj = {"Id":0,"ShopName": document.getElementById("name").value,"Email": document.getElementById("Input_Email").value,"Address": document.getElementById("address").value,"Contact": document.getElementById("number").value,"Password": document.getElementById("Input_Password").value}
    let sellerString = JSON.stringify(sellerObj);
    $.ajax({
        type: "POST",
        url: `https://localhost:44390/api/online/addNewSeller/${sellerString}`
    });
    gtag('event', 'click', { 'event_category': 'SFS', 'event_label': 'CA' });
}