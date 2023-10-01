//index sayfasında ödünç ver butonuna basıldığında ilgili inputların açılmasını ve kapanmasını sağladım
$(document).ready(function () {
    $("#State").change(function () {
        if ($(this).is(":checked")) {
            $("#loan-group").show();
            $("#borrower-group").show();
        } else {
            $("#loan-group").hide();
            $("#borrower-group").hide();
        }
    });
});

//Bu methodda listeleme işlemleri ve pagination işlemlerini yaptım
function getBooks(page, size) {
    $.ajax({
        url: `Home/GetBooks?page=${page}&size=${size}`,
        method: 'GET',
        success: function (data) {
            var bookList = data.books;
            var tbody = $('#book-list');
            tbody.empty();
            var bookstateText = '';
            var formattedDate = '';
            var totalPages = Math.ceil(data.totalBookCount / size);
            var currentPage = page;
            //sayfalamayı oluşturmak için generatePagination'a gerekli değerleri gönderdim
            generatePagination(totalPages, currentPage);
            $.each(bookList, function (index, book) {
                if (book.loanDate != null) {
                    formattedDate = moment(book.loanDate).format("DD.MM.YYYY");
                }
                else {
                    formattedDate = '';
                }

                bookstateText = "Dışarıda";
                if (book.state === 0) {
                    bookstateText = "Kütüphanede";
                }
                //Kütüphanade  ve dışarıda olma durumlarına gör rowları ayarladım
                var row = `<tr>
                          <td>${book.name}</td>
                          <td>${book.authorName}</td>
                          <td><img style="width:50px" src="images/${book.photo}"></td>
                          <td>${bookstateText}</td>
                          <td></td>
                          <td></td>
                          <td><button data-toggle="modal" data-target="#borrowerModal" dataid="${book.id}" class="borrowed btn btn-success">Ödünç ver</button></td>
                      </tr>`;

                if (book.state == 1) {
                    row = `<tr>
                          <td>${book.name}</td>
                          <td>${book.authorName}</td>
                          <td><img style="width:50px" src="images/${book.photo}"></td>
                          <td>${bookstateText}</td>
                              <td>${formattedDate}</td>
                          <td>${book.borrowerName}</td>
                        <td><button data-toggle="modal" data-target="#borrowerModal" dataid="${book.id}" class="borrowed btn btn-success">Ödünç ver</button></td>
                      </tr>`;
                }

                tbody.append(row);
            });
        },
        error: function (error) {
            console.error('Kitaplar alınamadı:', error);
        }
    });
}

//paging'ı oluşturdum
function generatePagination(totalPages, currentPage) {
    var paginationContainer = $("#pagination-container");
    paginationContainer.empty();

    if (totalPages <= 1) {
        return;
    }

    if (currentPage > 1) {
        paginationContainer.append('<li class="page-item"><a class="page-link" href="#" data-page="' + (currentPage - 1) + '">Önceki</a></li>');
    }

    for (var i = 1; i <= totalPages; i++) {
        var isActive = i === currentPage ? "active" : "";
        paginationContainer.append('<li class="page-item ' + isActive + '"><a class="page-link" href="#" data-page="' + i + '">' + i + '</a></li>');
    }

    if (currentPage < totalPages) {
        paginationContainer.append('<li class="page-item"><a class="page-link" href="#" data-page="' + (currentPage + 1) + '">Sonraki</a></li>');
    }
}

//burada ödünç ver operasyonun update kısmını yaptım ve front tarafında validasyonları gerçekleştirdim.
$("#borrowerForm").validate({
    rules: {
        loanname: {
            required: true
        },
        loandateborrower: {
            required: true
        }
    },
    messages: {
        loanname: {
            required: "Ödünç alan kişi zorunlu olarak girilmelidir"
        },
        loandateborrower: {
            required: "Tarih zorunludur"
        }
    },
    submitHandler: function (form) {
        var borrower = $("#loanname").val();
        var loanDate = $("#loandateborrower").val();
        var id = $("#borrowerSave").attr("dataid");
        var borrower = $("#loanname").val();
        var loanDate = $("#loandateborrower").val();
        var updateBookCommandRequest = {
            Id: id,
            Borrower: borrower,
            Date: loanDate
        };
        $.ajax({
            url: '/Home/UpdateBook',
            method: 'POST',
            data: JSON.stringify(updateBookCommandRequest),
            contentType: 'application/json',
            success: function (data) {
                if (data.isSuccessfull) {
                    toastr.success("Başarıyla kaydedildi");
                    //kaydetme işleminden sonra tekrar row'ı yeniledim
                    getBooks(1, 10);
                    $("#borrowerModal").modal("hide");
                } else {
                    toastr.error(data.message);
                }
            },
            error: function () {
                $("#loanname").val("");
                $("#loandateborrower").val("");
            }
        });
    }
});

$(document).on("click", "#borrowerSave", function () {
    $("#borrowerForm").submit();
});


//paging linkine basıldığında ilgili sayfanın getirilmesini yazdım
$(document).on("click", ".page-link", function (e) {
    e.preventDefault();
    var page = parseInt($(this).data("page"));
    getBooks(page, 10);
});

//Ödünç ver butonuna basıldığında ilgili öğenin popup'ta açılmasını ve değerlerin gelmesini sağladım
$(document).on("click", ".borrowed", function () {
    var model = $(this);
    var id = model.attr("dataid");
    var getBookQueryRequest = {
        Id: id
    };
    $.ajax({
        url: '/Home/GetBook',
        method: 'POST',
        data: JSON.stringify(getBookQueryRequest),
        contentType: 'application/json',
        success: function (data) {
            if (data.isSuccessfull) {
                $("#loanname").val(data.borrower);
                var loanDate = new Date(data.loanDate);
                loanDate.setDate(loanDate.getDate() + 1);
                var formattedLoanDate = loanDate.toISOString().split('T')[0];
                $("#loandateborrower").val(formattedLoanDate);
                $("#borrowerSave").attr("dataid", data.id);
            } else {
                toastr.error(data.message);
            }
        },
        error: function () {
            $("#loanname").val("");
            $("#loandateborrower").val("");
        }
    });

});