# Library Applicaiton

## Proje Açıklaması

- Bir kütüphane uygulamasıdır. Amacı kütüphanede bulunan kitapların ödünç verme işlemlerini yapmak. Aynı zamanda kitapların nerede kütüphanede mi değilse kimde, ne zaman teslim edilecek gibi bilgileri tutmamızı sağlayan bir uygulamadır.

## Kullanılan Teknolojiler

- Visual Studio 2022,
- .net Core(6.0) da katmanlı mimari ve code first,
- Microsoft EntityFrameworkCore kütüphaneleri,
- Veri tabanı için MSSQL kullandım,
- HTML5, Bootstrap, Jquery, JS kullandım.
- Projemdeki hataları yazdırmak için serilog kütüphanelerinden yardım aldım.
  
## Kurulum

- Projeyi ilk olarak github üzerinden zipleyebilir yada clone a repository kısmından alabilirsiniz. Açtıktan sonra yapmanız gerekenler;
- İlk olarak     LibraryApplication/appsettings.json içinde AppDbcontext kısmında kendi sql bilgilerinizi girmeniz(database ismini kendi isteğinize göre kullanabilirsiniz).
 ```c#
   "ConnectionStrings": {
  "AppDbContext": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
   }
  ```
- İkinci olarak  LibraryApplication.DataAccessLayer katmanındaki migration dosyasını silip Package Manager Console üzerinden "add-migration Mig1" yazıp DataAccessLayer katmanında çalıştırmak sonrasında update-database diyerek sql'e database kurulumu yapılmış olur.  
- Eğer viewmodel gelmezse kendin oluşturabilirsin.
```sql
  CREATE VIEW BooksBorrowerInfosViewModel AS
SELECT
    b.BookId,
	b.Title,
	b.Author,
	b.ImageUrl,
	b.IsBorrowed,
	borrow.FirstName,
	borrow.LastName,
	borrow.ReturnDate,
    borrow.IsReturned
FROM Books b
JOIN BorrowerInfos borrow ON b.BookId = borrow.BookId;
```

## Proje Açıklaması(Detaylı)

- Projemi .net core (6.0) olarak açtım ve 3 tane class library ekledim. Bunlar benim katmanlarım DataAccessLayer, EntityLayer ve BusinessLayer.
- EntityLayer katmanımda modelleri tutuyorum. Hazırladığım Book, BorrowerInfo ve BooksBorrowerInfosViewModel bunlar adlarından anlaşılacağı üzere Book kitap bilgilerini BorrowerInfo ise ödünç alan kişilerin bilgilerini tutuyor. View modelim ise bu iki modelin kolonlarını içeririyor.
- Projemin ana noktası olan ödünç verme olayını modellerimi tasarlarken Books tablosuna IsBorrowed kolonunu(bool) ekleyerek sistemi oluşturdum. Bu şekilde IsBorrowed eğer true ise kitabım kütüphanede değil false ise kütüphanede olarak tanımladım.
- Aynı şekilde bunu BorrowerInfo tablosundada koydum. Burada IsReturned(yani aslında geri verildimi)(bool) true ise kütüphanede, false ise şuan alan kişide şeklinde yorumladım.
- Bu modelleri codefirst yardımı ile MSSql'e taşıdım. Dataaccess katmanında yazdığım AppDbContext ve ana projemde appsettings.json kısmında bulunan
  ```c#
   "ConnectionStrings": {
  "AppDbContext": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
   }
  ```
sayesinde database ile bağlantı kuruyorum. Aynı zamanda program.cs kısmına builder.Services.AddDbContext kısmını ekledim.
- İhtiyacım olan katmanlara EntityFrameworkCore kütüphanelerini indiriyorum(ihtiyacım çerçevesinde). Sonrasında DataAccess Katmanıma Repositorylerimi yazıyorum bu katmanda direkt database ile bağlantılı çalışıyorum. Burada özellikle crud işlemlerini metodlar halinde yazılıyor.
- Abstract ve Concrete klasörleri oluşturarak interfaceleri abstract, classları Concrete klasörlerinde tutuyorum. İçlerine metodları yazıyorum.
- DataAccess katmanına ServiceRegistiration classı ekliyorum. Bu class sayesinde program.cs e gidip tek tek yazmak yerine katmanda gerekli olan dependency injection gibi yapıları içine yazıyorum. Böylelikle kod karmaşıklığından da kaçınmış olunuyor.
- ServiceRegistiration içine yazdıklarımızdan sonra program.cs e ekliyoruz(builder.Services.AddDataAccessLayerServices(); ben mesela bu şekilde ekledim).
- Sonrasın da Business katmanına geçiyorum aynı şekilde buna da ServiceRegistiration ekliyorum. Bu katmanda Serviceleri kullanıyorum. Yine burada da Interface kullanıyorum. Her modelim için ayrı servisler yazdım.
- Bu yazdığım servisler sayesinde benim Controllerımdaki yük ortadan kalkmış oldu ve controllerım da sadece servisler ile bağlantılı olarak çalışacak şekilde yazdım.
- Business katmanımda <br/>
```c#
        public async Task<List<Book>> GetListBooks()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetListBooks)} was started");

                List<Book> books = await _bookDal.GetList(); 

                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(GetListBooks)} is error");
                throw;
            }
        }
```
 BookService içinde yazdığım kitapları listeleyen bir metod.
  <br/>
  ```c#
   private readonly IBookDal _bookDal;
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;

        public BookService(IBookDal bookDal, ILogger<BookService> logger, IMapper mapper)
        {
            _bookDal = bookDal;
            _logger = logger;
            _mapper = mapper;
        }
```
 burada repositoryler ile bağlantı kuruyorum . _bookDal sayesinde repositoryde yazdığım crud metodlarını kullanabiliyorum. Aynı zamanda loglama ve mapper kullanıdığımıda görebilirsiniz.
-  Mapper kullanırkenden AutoMapper kütüphanelerinden yararlandım.
-  Servislerde ki metodları yazarken projemin gerekliliklerine göre yazdım.
-  Projeme bir view sayfası ekleyerek devam ediyorum burada bir bootstrap yardımı aldım sonrasında içinde düzenlemeler yapıyorum gerekli olan dosyaları wwwroot a ekliyorum. Viewdaki sayfalardaki karmaşıklığı önlemek için partiallar ve layout kullanıyorum
-  Index sayfam için
 ```c#
 public async Task<IActionResult> Index()
 {
     var booklist = await _bookService.GetListBooks();
     return View(booklist);
 }
```
servisten aldığım method ile listeleme işlemini yapıyor. 
-  Index Sayfamda birden fazla kez listeleme kullanmam gerekiyordu farklı yollardan listeleme yaptım kitapları listelerken
  ```
@model IEnumerable<LibraryApplication.EntityLayer.Models.Book>
```
model den yararlanırken BorrowerInfo tablomdan da bilgileri listelemem gerekti bunun için ilk olarak bir modelview oluşturdum. Bu modelview içinden veri çekmek için parametre kulllandım ve bu parametreyi ajax yardımı ile çektim sonrasında methodumdan bana dönen verileri javascript ile modal içine yazdırdım.
```c#
[HttpPost]
public async Task<JsonResult> BookBorrowerInfos(Guid BookId)
{
    try
    {
        //It works when I click on the "Ödünç verildi" button. It help shows me the book name, delivery date and book ID.
        var BorrowerBooksInfo = await _booksBorrowerInfosService.GetListBooksBorrowerInfo(BookId.ToString());

        if (BorrowerBooksInfo is null)
        {
            TempData["WarningMessage"] = "Bilgilere ulaşamadık.";
        }

        return new JsonResult(JsonConvert.SerializeObject(BorrowerBooksInfo));

    }
    catch (Exception ex)
    {
        _logger.LogError(ex.Message, LogLevel.Error, $"{nameof(BookBorrowerInfos)} is error");
        throw;
    }
}
```
- Genel hatlarıyla proje gelişmeye açık şekilde yazıldı. Anlaşılır olması için notlar yazıldı. Adlandırma yapılırken yapılan işe verilere dikkat edildi.
- Hataları yakalamak için loglamalar yapıldı.


## Program Kullanımı

- Uygulama açıldığında kitapların listelendiği bir sayfa karşılıyor bizi (kitaplar kitap ismine göre a dan z ye sıralanmış şekilde listeleniyor.
  
  ![IndexPage](https://github.com/SevvalFrdn/LibraryApplication/blob/master/LibraryApplication/wwwroot/GithubImage/2.png)

  ödünç verilen kitaplar görüldüğü gibi üzerinde ödünç verildi butonu ile belirtiliyor ve ödünç ver butonları tıklanamaz hale geliyor.

- Aşağıda görüldüğü üzere Ödünç ver butonuna tıklayınca bir modal açılıyor ve buradan kitabı almak isteyen kişinin bilgileri modaldaki ödünç ver butonuna basılarak kaydediliyor.(burada en altta gördüğünüz ıd ile hangi kitabın ödünç verildiği database de tutuluyor)

  ![IndexPage](https://github.com/SevvalFrdn/LibraryApplication/blob/master/LibraryApplication/wwwroot/GithubImage/3.png)
  
- Burada açılan her modal tıklanılan kitaba ait id ile geliyor.
- Aşağıdaki fotoğrafta bu sefer Ödünç verildi butonuna basınca kitabı kimin aldığı, ne zaman teslim edeceği ve kitap id si bulunuyor.

 ![IndexPage](https://github.com/SevvalFrdn/LibraryApplication/blob/master/LibraryApplication/wwwroot/GithubImage/4.png)

- Ve son olarak kitap ekleme sayfamız var burada kolaylıkla ismini ve yazarına yazdıktan sonra dosyalardan fotoğrafı seçerseniz database'e kayıt ediliyor.

 ![IndexPage](https://github.com/SevvalFrdn/LibraryApplication/blob/master/LibraryApplication/wwwroot/GithubImage/5.png)


## İletişim

- sevvalfrdn12@gmail.com
 

  
