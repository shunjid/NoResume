var requestToCodeForces;
var requestToGitHub;
var requestToUva;
var gitHubApiURL = "https://api.github.com/users/";
var uva_api_url = 'https://uhunt.onlinejudge.org/api/';
var cf_api_url = 'https://codeforces.com/api/';

$(document).ready(function(){
    var postLoader = $('#postLoader');
    postLoader.hide();

    $("#formWorkingProfile").submit(function(e){
        e.preventDefault();
        postLoader.show();
        var githubUsername = $('#GithubUsername').val();
        var cf_handle = $('#CodeforcesUsername').val();
        var uvaHandle = $('#UhuntUsername').val();

        requestToCodeForces = $.get(cf_api_url + 'user.status', { handle: cf_handle }, function (data, status) {
            requestToUva = $.get(uva_api_url + 'uname2uid/' + uvaHandle, function (uvaData, uvaStatus) {
               if(uvaData !== 0){
                   requestToGitHub = $.get(gitHubApiURL + githubUsername, function (userInformation, userStatus) {
                       console.log(userInformation);
                       $.post('', $('#formWorkingProfile').serialize(), function (response) {
                           if(response != null || response !== "" || response !== "null"){
                               M.toast({
                                   html: 'Data updated Successfully',
                                   classes: 'green darken-1 rounded'
                               });
                           }else{
                               M.toast({
                                   html: 'Something went wrong !',
                                   classes: 'red darken-4 rounded'
                               });
                           }
                       });
                   }).fail(function (xhr, status) {
                       if (status != 'abort') showUserDefinedToast("GitHub username is invalid","red darken-1 rounded");
                   });
               }else{
                   showUserDefinedToast("UVA username is invalid", "red darken-1 rounded");
               }
           });
        }).fail(function(xhr, status) {
            if (status != 'abort') showUserDefinedToast("Codeforces username is invalid","red darken-1 rounded");
        })
        .always(function() {
            postLoader.hide();
        });
    })
});

function showUserDefinedToast(message, style) {
    M.toast({
        html: message,
        classes: style
    });
}