var transactionId;
$(document).ready(function(){
    $('.modal').modal();
    
    
    var postLoader = $('#postLoader');
    postLoader.hide();

    $("#BioEditorForm").submit(function(e){
        e.preventDefault();
        postLoader.show();

        $.post('', $('#BioEditorForm').serialize(), function (response) {
            if(response != null || response !== "" || response !== "null"){
                M.toast({
                    html: 'Data updated Successfully',
                    classes: 'green darken-1 rounded'
                });
                $('#DevCurrentCity').text(response.currentCity);
                $('#DevShortDescription').html(response.shortDescription);
                $('input[name=PhoneNumber]').val(response.developerPhoneNumber);
                true === response.isAvailableForJob ? $("#DevJobAvailability").text("Yes"):$("#DevJobAvailability").text("No");
            }else{
                M.toast({
                    html: 'Something went wrong !',
                    classes: 'red darken-4 rounded'
                });
            }
            postLoader.hide();
        });
    });

    
    $("#subscriptionForm").submit(function(e){
        e.preventDefault();
        var phonenumber = $("#PhoneNumber").val();
        if(phonenumber.length != 11)
        {
            M.toast({
                html: 'Number must be 11 digit !',
                classes: 'red darken-4'
            });
        }
        else if (phonenumber.startsWith("017") || phonenumber.startsWith("013"))
        {
            $("#otpForm").hide();
            $("#agreement").show();
            $('.modal').modal('open');
            $("#acceptbtn").click(function(){
                
                $("#agreement").hide();
                $("#otpForm").show();

                phonenumber = "88"+phonenumber;
                $.post('/ShortBios/createOTP', {phonenumber : phonenumber}, function (responseData) {
                    
                    // nothing 
 
                }).done(function (xhr, status){
                   // nothing
                });
          
            });

            $("#rejectbtn").click(function(){
                $('.modal').modal('close');
            });
        }
        else
        {
            M.toast({
                html: 'You have to use GP number !',
                classes: 'red darken-4'
            });
        }
    });
        
        $("#otpForm").submit(function (e) {
           e.preventDefault();
           var tpin = $("#otp").val();
         
            $.post('/ShortBios/chargeOTP', {tpin : tpin}, function (otpdata) {

            }).done(function (xhr, status){
                // nothing
                location.reload();
                
             }).fail(function (xhr, status){
                // nothing
                M.toast({
                    html: 'Incorrent OTP number !',
                    classes: 'red darken-4'
                });
             });;
        });
});

