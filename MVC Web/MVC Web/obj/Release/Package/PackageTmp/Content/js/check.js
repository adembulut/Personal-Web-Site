function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function replyCalistir(object) {
    // $('#CommentID').val(object.get)

    var deger = object.dataset.name;
    var id = object.dataset.id;

    $('#CommentID').val(id);
    $('#RefComment').val(deger);

    var div = document.getElementById("RefDiv");
    div.style.visibility = "visible";
    div.hidden = false;
}

$(document).ready(function () {
    $('#refRemove').click(function () {
        $('#CommentID').val("0");
        $('#RefComment').val("");
        var refDiv = document.getElementById("RefDiv");
        refDiv.style.visibility = "hidden";
    });
});


function calistir() {



    var Email = $('#Email').val();





    if (validateEmail(Email)) {
        var ArticleID = $('#ArticleID').val();
        var CommentID = $('#CommentID').val();
        var NameSurname = $('#NameSurname').val();
        var Text = $('#Text').val();
        if (NameSurname.length > 0) {
            if (Text.length > 0) {

                var dataset = { ArticleID: ArticleID, CommentID: CommentID, NameSurname: NameSurname, Text: Text };
                $.ajax({
                    url: '/Blog/AddComment',
                    type: 'POST',
                    data: dataset,
                    success: function (result) {
                        alert(result);
                        document.getElementById("CommentForm").reset();
                        location.reload();
                    },
                    failure: function (result) {
                        alert("Something wrong");
                    }
                });
            } else {
                alert("Yorum girmelisiniz");

            }
        } else {
            alert("İsim Soyisim alanını doldurun");
        }





    } else {
        alert("Geçerli bir mail adresi girmelisiniz");
    }
}

