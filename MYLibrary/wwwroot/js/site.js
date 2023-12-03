$(document).ready(() => {
    getBooks();
})

const apiUrl = "https://localhost:7161/api/Library"; //TODO DÜZENLENECEK

getBookDetails = (id) => {
    $.get({
        url: apiUrl + '/GetBookById/' + id,
        success: function (data) {
            if (data === null || data === undefined || data === "") {
                return;
            }
            const book = {
                Id: data.id,
                Name: data.name,
                Author: data.author,
                Image: data.image,
                IsAvailable: data.isAvailable,
                Summary: data.summary,
                Borrower: data.borrower,
                ExpireDate: data.expireDate,
            };

            window.open('/Home/BookDetails?id=' + encodeURIComponent(book.Id));/* + '&name=' + encodeURIComponent(book.Name) + '&author=' + encodeURIComponent(book.Author) + '&image=' + encodeURIComponent(book.Image) + '&isAvailable=' + book.IsAvailable + '&summary=' + encodeURIComponent(book.Summary) + '&borrower=' + encodeURIComponent(book.Borrower) + '&expireDate='+encodeURIComponent(book.ExpireDate), '_blank');*/

            console.log("Kitaplar getirildi");
        },
        error: function (xhr, status, error) {
            console.log("Kitaplar getirilirken bir hata oluştu");
        }
    });
};


//Bütün kitapları getiren metod. (Kitap getiren iki farklı metod olmasının sebebi: bu metodun ilk açılışta gelmesi ve diğer metoddan daha az yorucu olmasıdır)
getBooks = () => {
    const container = $("#book-container");
    containerClear(container);
    $.get({
        url: apiUrl + '/getBooks',
        success: function (data) {
            if (data === null || data === undefined || data === "") {
                return;
            }

            data.forEach(function (book) {

                const html = fillHtml(book);

                container.append(html);
            });
            console.log("Kitaplar getirildi");
        },
        error: function (xhr, status, error) {
            console.log("Kitaplar getirilirken bir hata oluştu");
        }
    });
}

//İsme göre kitap getiren metod
getBooksByName = () => {
    const container = $("#book-container");
    containerClear(container);

    const inputBookName = $('#searchbar-input').val();
    if (stringIsNullOrEmpty(inputBookName)) {
        getBooks();
        return;
    }

    $.get({
        url: apiUrl + '/getBooksByName/' + inputBookName,
        success: function (data) {
            if (data === null || data === undefined || data === "") {
                return;
            }

            data.forEach(function (book) {
                
                const html = fillHtml(book);

                container.append(html);
            });



            console.log("Kitaplar getirildi");
        },
        error: function (xhr, status, error) {
            console.log("Kitaplar getirilirken bir hata oluştu");
        }
    });
}


//Yeni kitap ekler.
addNewBook = () => {

    const modal = $('#addbook-modal');
    var bookName = $('#modal-book-name').val();
    var author = $('#modal-author').val();
    var img = $('#modal-book-img').val();
    var sum = $('#modal-book-sum').val();
    if (!bookName || !author || !img) {
        alert("Lütfen bütün alanları doldurunuz.");
        return;
    }

    $.post({
        url: apiUrl + '/addBook',
        contentType: 'application/json',
        data: JSON.stringify({
            Name: bookName,
            Author: author,
            Image: img,
            IsAvailable: true,//Eklenen kitap kütüphanede olacağından direkt olarak kütüphanede diye atanmıştır.
            Summary: sum,
            Borrower: "",
            ExpireDate:"",
        }),
        success: (response) => {
            alert("Kitap ekleme işlemi başarıyla gerçekleşti.");
            $('#addbook-modal').modal('hide');
            getBooks();
        },
        error: (error) => {
            alert("Kitap eklerken bir hata oluştu");
            $('#addbook-modal').modal('hide');
        }
    });


}

//Kitap ödünç vermek için kullanılır.
lendBook = (bookId) => {

    const modal = $('#lendbook-modal');
    var borrower = $('#modal-borrower').val();
    var expireDateValue = $('#modal-expire-date').val();


    const dateObject = new Date(expireDateValue);

    const day = dateObject.getDate();
    const month = dateObject.getMonth() + 1;
    const year = dateObject.getFullYear();

    expireDate = day + '/' + month+'/'+year

    if (!borrower || !expireDate) {
        alert("Lütfen bütün alanları doldurunuz.");
        return;
    }
    $.get({
        url: apiUrl + '/lendBook' + "?bookId=" + encodeURIComponent(bookId) + "&borrowerName=" + borrower + "&expireDate=" + expireDate,
        contentType: 'application/json',
        success: (response) => {
            alert("Kitap ödünç verme işlemi başarıyla gerçekleşti.");
            modal.modal('hide');
            location.reload();
        },
        error: (error) => {
            alert("Kitap ödünç verilirken bir hata oluştu");
            modal.modal('hide');
        }
    });

}

fillHtml = (book) => {
    const bookName = book.name;
    const author = book.author;
    const isAvailable = book.isAvailable;
    const img = book.image;
    const id = book.id;
    const html = 
    `
                 <div class="book">
                     <img src="${img}" />
                     <div class="book-details">
                         <a id="${id}" onclick="getBookDetails(${id})" class="book-name">${bookName}</a>
                         <span class="author-name">${author}</span>
                         <span class="is-availible">
                        <i class="fa-solid fa-circle" style="color: ${isAvailable ? "green" : "red"};"></i>
                        ${isAvailable ? "KÜTÜPHANEDE" : "DIŞARDA"} 
                         </span>
                     </div>
                 </div>`;

    return html;
}

containerClear = (container) => {
    container.empty();
}

stringIsNullOrEmpty = (content) => {
    if (content === "" || content === undefined || content === null) {
        return true;
    }
    return false;
}