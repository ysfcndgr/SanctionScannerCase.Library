$(document).ready(function () {
    //filetype'ı özel olarak kontrol etmek için aşağıdaki methodu kullandım.
    $.validator.addMethod("fileType", function (value, element) {
        var acceptedTypes = ["png", "jpg", "jpeg"];
        var fileType = value.split('.').pop().toLowerCase();
        return $.inArray(fileType, acceptedTypes) !== -1;
    }, "Sadece PNG, JPG veya JPEG formatlarına izin verilir.");

    //aşağıda formun validasyon işlemlerini front tarafında yaptım
    $("#book-form").validate({
        rules: {
            name: {
                required: true
            },
            author: {
                required: true,
                minlength: 3
            },
            borrower: {
                required: function (element) {
                    return $("#State").is(":checked");
                }
            },
            loandate: {
                required: function (element) {
                    return $("#State").is(":checked");
                }
            },
            bookphoto: {
                required: "Fotoğraf gerekli",
                fileType: true
            }
        },
        messages: {
            name: {
                required: "Kitap adı gerekli."
            },
            author: {
                required: "Yazar adı gerekli.",
                minlength: "Yazar adı en az 3 karakter olmalıdır."
            },
            borrower: {
                required: "Ödünç veriliyorsa ödünç alanın adı gerekli."
            },
            loandate: {
                required: "Ödünç veriliyorsa geri getirme tarihi gerekli."
            }
        },
        submitHandler: function (form) {
            //sonucun başarılı olduğu durumda değerleri alıp backende gönderdim
            
            var name = $("#name").val();
            var author = $("#author").val();
            var state = $("#State").is(":checked") == true ? 1 : 0;
            var borrower = $("#borrower").val();
            var loanDate = $("#loandate").val();
            var bookphoto = $("#bookphoto")[0].files[0];
            var formData = new FormData();
            formData.append("name", name);
            formData.append("author", author);
            formData.append("State", state);
            formData.append("borrower", borrower);
            formData.append("loandate", loanDate);
            formData.append("bookphoto", bookphoto);

            $.ajax({
                url: '/Home/AddBook',
                method: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.isSuccessfull) {
                        toastr.success(data.message);
                        getBooks(1, 10);
                        form.reset();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function () {
                    toastr.error("Kitap eklenirken bir hata oluştu.");
                }
            });
        }
    });
});