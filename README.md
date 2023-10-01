# Kütüphane Uygulaması

Projede Domain Driven Design tasarım yöntemini kullandım. Pattern olarak MediaTR, CQRS, UnitOfWork, Yazma ve okuma işlemleri için ayrı Generic Repository'ler oluşturdum.Web projesini ayağa kaldırdığınız zaman 
migrationların otomatik olarak çalışması için kod yazdım böylelikle Web projesini çalıştırdığınızda local db'nizde db'nin ve tablonunuzun oluşması lazım. Örnek olması açısından projeye UnitTest ekledim ve
2 durumu örnek olarak gösterdim. Yine örnek olması açısından bir api projesi ekledim ve bir örnek yaptım, proje içerisinde SanctionScanner.WebApi.postman_collection.json adında postman collection bıraktım.
Loglama için serilog kullandım logları C dizinin altında Logs klasörünün altına logluyor.
Frontend tarafında bildirimler için toastr kütüphanesi kullandım, jquery validasyon kullandım ve bootsrap kullandım. Validasyon işlemlerini hem frontend hem backend tarafında kontrol ettim. Ekleme listeleme ve 
ödünç ver butonuna basıldıktan sonra kaydedilmesi durumunda en çok dikkat ettiğim sayfanın yenilenmeden devam etmesini sağladım.Sayfaya paging ekledim 10 tane kayıttan fazla ekleyince paging navbarının görünmesini sağladım.

# Index

![1](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/e979afd6-3148-4354-823f-40ca094ddafa)

# Validasyonlar

![2](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/9a2d7724-ecda-4298-ada6-6b2114f20559)

![3](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/a98fe4fd-90ca-4735-8a78-4e467c2c7ad6)

# Kitap Ekleme

![4](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/c7147b7e-9737-40ca-bf22-41351002ae64)

![5](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/eecab3fc-36c3-4316-a865-2874a6a75af1)

# Ödünç Verme

![6](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/c885f0c3-be50-4998-8220-570cc6c92349)

# Paging

10 taneden fazla öğe eklendiği zaman paging aktif olur.

![7](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/e8dd4011-c9c4-4154-8a4a-fabb45ee19d8)

![8](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/ecce7bd6-1384-4693-a7d8-0c30dc3d29a7)
