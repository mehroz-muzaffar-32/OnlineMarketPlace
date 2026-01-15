function gtagHelperAI()
{
    gtag('event', 'click', { 'event_category': 'AddItems', 'event_label': 'NewItem' });
}

window.ShowDialog = function() {
    document.getElementById('my-dialog').showModal();
}