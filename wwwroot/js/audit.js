var auditData = {};
$(document).ready(function(){
    var request = $.getJSON('https://api.ipdata.co/?api-key=test', function (responseData, status) {
        auditData = {
            AuditDescription : "/Home/Index",
            IsExceptionThrown : false,
            DeveloperOrAnonymous : "Anonymous",
            TimeOfAction : new Date().getDate(),
            InternetProtocol : responseData.ip,
            Country : responseData.country_name,
            CountryCode : responseData.country_code,
            Latitude : responseData.latitude,
            Longitude : responseData.longitude
        };

        $.post('Home/Audit', { jObject: auditData }, function (responseData) {
            var auditMessage = $('#_auditMessage');
            auditMessage.html("<i class=\"fa fa-map-marker orange-text\" aria-hidden=\"true\"></i> "+responseData[responseData.length -1].country
                + ' : Total visited :' + responseData.length);
        }, "json");
    });
});