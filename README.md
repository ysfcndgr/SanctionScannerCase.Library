# Kütüphane Uygulaması

Projede Domain Driven Design tasarım yöntemini kullandım. Pattern olarak MediaTR, CQRS, UnitOfWork, Yazma ve okuma işlemleri için ayrı Generic Repository'ler oluşturdum.Web projesini ayağa kaldırdığınız zaman 
migrationların otomatik olarak çalışması için kod yazdım böylelikle Web projesini çalıştırdığınızda local db'nizde db'nin ve tablonunuzun oluşması lazım. Örnek olması açısından projeye UnitTest ekledim ve
2 durumu örnek olarak gösterdim. Yine örnek olması açısından bir api projesi ekledim ve bir örnek yaptım, proje içerisinde SanctionScanner.WebApi.postman_collection.json adında postman collection bıraktım.
Loglama için serilog kullandım.
Frontend tarafında bildirimler için toastr kütüphanesi kullandım, jquery validasyon kullandım ve bootsrap kullandım. Validasyon işlemlerini hem frontend hem backend tarafında kontrol ettim. Ekleme listeleme ve 
ödünç ver butonuna basıldıktan sonra kaydedilmesi durumunda sayfanın yenilenmeden akışına devam etmesini sağladım.Sayfaya paging ekledim 10 tane kayıttan fazla ekleyince paging navbarının görünmesini sağladım.

# Index
![image](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/edb6b2df-e850-48a9-b2f9-babeae9f02f8)

# Validasyonlar

![image](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/74016c99-39b2-489c-888f-1ca93dcc6aed)
![image](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/f1853497-9f29-4276-af18-7ab97ee2c383)

# Kitap Ekleme

![image](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/6fc618ae-cde8-4284-ae2b-354829522086)
![image](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/aa879a2a-8dab-4876-8686-5c408bcd0f04)

# Ödünç Verme

![image](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/98ad6170-32d6-4836-8fc6-bb0eb777e223)

# Paging

10 taneden fazla öğe eklendiği zaman paging aktif olur.

![image](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/9c252ec8-5fea-4767-8c3c-53c10a829c9b)
![image](https://github.com/ysfcndgr/SanctionScannerCase.Library/assets/32979760/9b3e8ab4-c25f-42fd-aa17-1a38db5527d0)

