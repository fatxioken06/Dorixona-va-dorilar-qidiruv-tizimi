# Dorixona-va-dorilar-qidiruv-tizimi

Dorixona Ma'lumotlarini Boshqarish Tizimi
Umumiy ma'lumot
Ushbu loyiha dorixona ma'lumotlarini boshqarish uchun mo'ljallangan tizim bo'lib, C# dasturlash tili va Windows Forms platformasi asosida ishlab chiqilgan. Tizim SQL Server ma'lumotlar bazasi bilan integratsiyalangan bo'lib, dorilar haqidagi ma'lumotlarni samarali boshqarish imkonini beradi. Loyiha kichik va o'rta hajmdagi dorixonalar uchun qulay yechim sifatida yaratilgan bo'lib, foydalanuvchilarga dorilarni qo'shish, o'chirish, izlash, yangilash va sotish kabi asosiy funksiyalarni taqdim etadi.
Loyiha 2025-yil 16-may holatiga qadar yakunlangan bo'lib, sinov jarayonlaridan muvaffaqiyatli o'tgan.
Xususiyatlar

Dori qo'shish: Yangi dori ma'lumotlarini tizimga kiritish.
Dori o'chirish: Keraksiz dorilarni xavfsiz tarzda o'chirish.
Dori izlash: Dori nomlari yoki kategoriyasi bo'yicha tez qidiruv.
Dori yangilash: Mavjud dori ma'lumotlarini tahrirlash.
Dori sotish: Dori sotish jarayonini boshqarish va qoldiq miqdorini yangilash.

Texnologiyalar

Dasturlash tili: C#
Platforma: Windows Forms
Ma'lumotlar bazasi: SQL Server
Integratsiya: ADO.NET (parametrlashtirilgan so'rovlar bilan xavfsizlik ta'minlandi)

Ma'lumotlar bazasi tuzilmasi
Tizim Dorilar nomli jadval asosida ishlaydi. Jadval quyidagi ustunlarni o'z ichiga oladi:



Ustun nomi
Turi
Tavsif



ID
INT
Yagona identifikator (Primary Key, avtomatik o'suvchi)


Nomi
NVARCHAR(100)
Dorining nomi (majburiy)


Narxi
DECIMAL(10,2)
Dorining narxi (majburiy)


Soni
INT
Mavjud miqdor (majburiy)


YaroqlilikMuddati
DATE
Yaroqlilik muddati (ixtiyoriy)


IshlabChiqaruvchi
NVARCHAR(100)
Ishlab chiqaruvchi (ixtiyoriy)


Kategoriyasi
NVARCHAR(50)
Kategoriyasi (ixtiyoriy)


RetseptTalabQilinadi
BIT
Retsept talab qilinadimi (majburiy)


Jadvalni yaratish uchun SQL so'rovi:
CREATE TABLE Dorilar (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nomi NVARCHAR(100) NOT NULL,
    Narxi DECIMAL(10,2) NOT NULL,
    Soni INT NOT NULL,
    YaroqlilikMuddati DATE,
    IshlabChiqaruvchi NVARCHAR(100),
    Kategoriyasi NVARCHAR(50),
    RetseptTalabQilinadi BIT NOT NULL
);

O'rnatish va sozlash

Tizim talablari:

Operatsion tizim: Windows 7 yoki undan yuqori.
Dasturiy ta'minot: Visual Studio, SQL Server.
Minimal xotira: 4 GB RAM, 10 GB bo'sh disk maydoni.


O'rnatish qadamlari:

Repozitoriyani klon qiling:
git clone https://github.com/your-username/your-repo.git


SQL Server’da Dorilar jadvalini yuqoridagi so'rov yordamida yarating.

Visual Studio’da loyiha faylini oching (.sln fayli).

SQL Server ulanish sozlamalarini connectionString sifatida kodda o'rnating (masalan, DESKTOP-D01M0CC\SQLEXPRESS).

Loyihani kompilyatsiya qiling va ishga tushiring.




Foydalanish bo'yicha ko'rsatmalar

Dori qo'shish:

"Qo‘shish" formasini oching.
Barcha maydonlarni to‘ldiring (masalan, Nomi, Narxi, Soni).
"Saqlash" tugmasini bosing.


Dori o'chirish:

"O‘chirish" formasida DataGridViewdan kerakli dorini tanlang.
"O‘chirish" tugmasini bosing va tasdiqlang.


Dori izlash:

"Izlash" formasida qidiruv so‘zini kiriting (masalan, "Paracetamol").
"Qidirish" tugmasini bosing.


Dori yangilash:

"Yangilash" formasida dorini tanlang va ma'lumotlarni tahrirlang.
"Yangilash" tugmasini bosing.


Dori sotish:

"Sotish" formasida dorini tanlang va sotiladigan miqdorni kiriting.
"Sotish" tugmasini bosing.



Muammolarni bartaraf etish

Ulanish xatosi: SQL Server ulanish sozlamalarini tekshiring (connectionString).
Ma'lumot kiritish xatosi: Maydonlarga to‘g‘ri qiymatlar kiriting (masalan, Narxi raqamli bo‘lishi kerak).
Dastur ishlamayapti: Visual Studio’da loyihani qayta kompilyatsiya qiling yoki xato xabarlarini tekshiring.

Muallif

Muallif: Fatxulla Qahhorov
Sana: 2025-yil 16-may
Aloqa: fatxullaqahhorov882gmail.com

