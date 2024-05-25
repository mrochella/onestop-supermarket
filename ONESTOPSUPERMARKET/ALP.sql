CREATE DATABASE IF NOT EXISTS db_supermarket;
USE db_supermarket;

DROP TABLE IF EXISTS cart;
DROP TABLE IF EXISTS fav;
DROP TABLE IF EXISTS history;
DROP TABLE IF EXISTS promoproduk;
DROP TABLE IF EXISTS transaksi;
DROP TABLE IF EXISTS customer;
DROP TABLE IF EXISTS produk;

CREATE TABLE produk (
  idproduk VARCHAR(4) PRIMARY KEY,
  namaproduk VARCHAR(100) NOT NULL,
  hargaproduk INT NOT NULL,
  kategori VARCHAR(15) NOT NULL,
  stock INT NOT NULL,
  deskripsi VARCHAR(200) NOT NULL,
  gambarproduk varchar(30) NOT NULL
);

CREATE TABLE promoproduk (
  idproduk VARCHAR(4) NOT NULL REFERENCES produk (idproduk),
  hargapromo INT NOT NULL,
  statuspromo VARCHAR(12) DEFAULT 'Tidak aktif',
  CONSTRAINT promoproduk_pk PRIMARY KEY (idproduk),
  CONSTRAINT promoproduk_chk CHECK (statuspromo = 'Aktif' OR statuspromo = 'Tidak aktif')
);

CREATE TABLE customer (
  idcustomer VARCHAR(4) PRIMARY KEY,
  namacustomer VARCHAR(20) NOT NULL,
  telpcustomer CHAR(13) NOT NULL,
  alamatcustomer VARCHAR(50) NOT NULL,
  emailcustomer VARCHAR(50) NOT NULL,
  passwordcustomer VARCHAR(8) NOT NULL
);

CREATE TABLE transaksi (
  idtransaksi VARCHAR(4) PRIMARY KEY,
  tanggaltransaksi DATE,
  totalharga INT NOT NULL,
  idcustomer VARCHAR(4) NOT NULL,
  statuspesanan VARCHAR(15) DEFAULT 'Sedang diproses',
  CONSTRAINT transaksi_fk FOREIGN KEY (idcustomer) REFERENCES customer (idcustomer),
  CONSTRAINT transaksi_chk CHECK (statuspesanan = 'Sedang diproses' OR statuspesanan = 'Sudah diterima')
);

CREATE TABLE history (
  idtransaksi VARCHAR(4) NOT NULL,
  idcustomer VARCHAR(4) references customer(idcustomer), 
  namacustomer VARCHAR(20) references customer(namacustomer),
  namaproduk VARCHAR(100) references produk(namaproduk),
  hargaproduk INT references produk(hargaproduk),
  quantity INT NOT NULL,
  totalharga INT NOT NULL,
  statuspesanan VARCHAR(15) references transaksi(statuspesanan),
  CONSTRAINT history_fk1 FOREIGN KEY (idtransaksi) REFERENCES transaksi (idtransaksi)
);

CREATE TABLE cart (
  idcustomer VARCHAR(4) NOT NULL,
  idproduk VARCHAR(4) NOT NULL REFERENCES produk (idproduk),
  quantity INT NOT NULL,
  status_del VARCHAR(1) DEFAULT '0',
  FOREIGN KEY (idcustomer) REFERENCES customer (idcustomer),
  CONSTRAINT cart_chk CHECK (status_del = '0' OR status_del = '1')
);

CREATE TABLE fav (
  idcustomer VARCHAR(4) NOT NULL,
  idproduk VARCHAR(4) NOT NULL,
  status_del VARCHAR(1) DEFAULT '0',
  FOREIGN KEY (idcustomer) REFERENCES customer (idcustomer),
  FOREIGN KEY (idproduk) REFERENCES produk (idproduk),
  CONSTRAINT fav_chk CHECK (status_del = '0' OR status_del = '1')
);

INSERT INTO produk (`idproduk`, `namaproduk`, `hargaproduk`, `kategori`, `stock`, `deskripsi`, `gambarproduk`) VALUES
('A001', 'Something Fresh Apel Fuji 1Kg', 34000, 'Fruit', 100, 'Apel kaya akan nutrisi bagi daya tahan tubuh.', '\\bin\\debug\\FOTO PRODUK\\0.jpg'),
('A002', 'Something Fresh Jeruk Sunkist 1Kg', 32500, 'Fruit', 100, 'Jenis buah jeruk yang di dalamnya terdapat kandungan serat larut yang disebut dengan pektin.', '\\bin\\debug\\FOTO PRODUK\\1.jpg'),
('A003', 'Lumbung Pisang Cavendish 1Kg', 27000, 'Fruit', 100, 'Pisang Cavendish merupakan merupakan salah satu kmoditas buah tropis favorit di dunia.', '\\bin\\debug\\FOTO PRODUK\\2.jpg'),
('A004', 'Kiwi Fruit 1Kg', 87000, 'Fruit', 100, 'Buah kiwi adalah buah yang berasal dari tanaman liana asli Selandia Baru.', '\\bin\\debug\\FOTO PRODUK\\3.jpg'),
('A005', 'Avocado Mentega 1Kg', 22000, 'Fruit', 100, 'Buah alpukat adalah buah yang berasal dari pohon alpukat.', '\\bin\\debug\\FOTO PRODUK\\4.jpg'),
('A006', 'Anggur Red Globe 1kg', 67500, 'Fruit', 100, 'Anggur merupakan sumber yang kaya akan nutrisi.', '\\bin\\debug\\FOTO PRODUK\\5.jpg'),

('B001', 'Fresh Tomato 1Kg', 30000, 'Vegetables', 100, 'Tomat mengandung vitamin A, vitamin C, dan antioksidan yang mencegah penyakit kanker.', '\\bin\\debug\\FOTO PRODUK\\6.jpg'),
('B002', 'Potato 1Kg', 19500, 'Vegetables', 100, 'Kentang mengandung berbagai nutrisi penting seperti vitamin C, vitamin B6, kalium, serat.', '\\bin\\debug\\\\FOTO PRODUK\\7.jpg'),
('B003', 'Cetroland Brokoli Fresh 1Kg', 40000, 'Vegetables', 100, 'Sayur brokoli ini emiliki bunga yang berwarna hijau, susunannya rapat, dan batang pohon yang tebal.', '\\bin\\debug\\FOTO PRODUK\\8.jpg'),
('B004', 'Garlic 1Kg', 20000, 'Vegetables', 100, 'Bawang putih adalah tanaman umbi yang dikenal secara luas karena aroma dan rasa kuatnya yang khas.', '\\bin\\debug\\FOTO PRODUK\\9.jpg'),
('B005', 'Onion 1Kg', 10000, 'Vegetables', 100, 'Meningkatkan sistem imun tubuh, mengatasi sembelit, meningkatkan kepadatan tulang.', '\\bin\\debug\\FOTO PRODUK\\10.jpg'),
('B006', 'Carrot 1Kg', 13900, 'Vegetables', 100, 'Wortel diperkaya dengan kandungan karbohidrat yang cukup besar.', '\\bin\\debug\\FOTO PRODUK\\11.jpg'),

('C001', 'Whole Milk Diamond 1L', 15250, 'Dairy', 100, 'Whole Milk Diamond adalah produk susu yang berkualitas tinggi dan diproses dengan hati-hati.', '\\bin\\debug\\FOTO PRODUK\\12.jpg'),
('C002', 'Fresh Milk Greenfields 1L', 20500, 'Dairy', 100, 'Fresh Milk Greenfields adalah susu segar berkualitas tinggi yang diproduksi oleh Greenfields.', '\\bin\\debug\\FOTO PRODUK\\13.jpg'),
('C003', 'Yogurt Greenfields 500 mL', 48000, 'Dairy', 100, 'Greenfields minuman yogurt dari susu segar dan sumber serat pangan dengan rasa strawberry.', '\\bin\\debug\\FOTO PRODUK\\14.jpg'),
('C004', 'Chocolate Ice Cream Magnum', 20000, 'Dairy', 100, 'Chocolate Ice Cream Magnum adalah produk es krim yang sangat populer dan menggugah selera.', '\\bin\\debug\\FOTO PRODUK\\15.jpg'),
('C005', 'Cheedar Kraft Cheese', 23000, 'Dairy', 100, 'Kraft keju cheddar dengan calci-milk mengandung manfaat baik dari susu dan tinggi kalsium.', '\\bin\\debug\\FOTO PRODUK\\16.jpg'),
('C006', 'Perfetto Mozarella Cheese', 115000, 'Dairy', 100, 'Perfetto Mozzarella Cheese adalah keju mozzarella yang lezat dan berkualitas tinggi.', '\\bin\\debug\\FOTO PRODUK\\17.jpg'),

('D001', 'Coca Cola 1,5 mL', 15000, 'Soda', 100, 'Coca cola minuman beraroma kola berkarbonasi botol 1,5 mL.', '\\bin\\debug\\FOTO PRODUK\\18.jpg'),
('D002', 'Sprite 1,5 mL', 15000, 'Soda', 100, 'Sprite minuman rasa jeruk lemon & lime berkarbonasi botol 1500 mL.', '\\bin\\debug\\FOTO PRODUK\\19.jpg'),
('D003', 'Fanta 1,5 mL', 15000, 'Soda', 100, 'Fanta minuman berkarbonasi rasa stroberi yang menyegarkan.', '\\bin\\debug\\FOTO PRODUK\\20.jpg'),
('D004', 'Dr Pepper 355 mL', 30000, 'Soda', 100, 'Dr Pepper adalah minuman berkarbonasi yang terkenal dan sangat digemari di Amerika Serikat.', '\\bin\\debug\\FOTO PRODUK\\21.jpg'),
('D005', 'A&W Sarsaparila 250 mL', 7000, 'Soda', 100, 'A&W Sarsaparilla adalah minuman berkarbonasi yang terkenal dengan rasa uniknya.', '\\bin\\debug\\FOTO PRODUK\\22.jpg'),
('D006', 'GreenSands 330 mL', 9500, 'Soda', 100, 'Green sands minuman berkarbonasi dengan ekstrak apel dan jeruk.', '\\bin\\debug\\FOTO PRODUK\\23.jpg'),
('D007', 'Schweppes 330 mL', 7000, 'Soda', 100, 'Schweppes dikenal karena kualitas dan rasa segar yang khas dalam minuman berkarbonasinya.', '\\bin\\debug\\FOTO PRODUK\\24.jpg'),

('E001', 'Beer Bintang 620 mL', 36000, 'Beer', 100, 'Bintang radler 0.0% merupakan minuman malt berkarbonasi rasa BLKCURRANT&LIME, tidak mengandung alcohol.', '\\bin\\debug\\FOTO PRODUK\\25.jpg'),
('E002', 'Smirnoff 750 mL', 275000, 'Beer', 100, 'Smirnoff telah menjadi salah satu merek minuman beralkohol terpopuler yang digunakan dalam pembuatan koktail serta minuman campuran lainnya.', '\\bin\\debug\\FOTO PRODUK\\26.jpg'),
('E003', 'Jack Daniels Old No.7 700 mL', 925000, 'Beer', 100, 'Jack Daniels old no. 7 adalah wiski Tennessee terkenal yang terkenal dengan sejarahnya yang kaya dan cita rasanya yang khas.', '\\bin\\debug\\FOTO PRODUK\\27.jpg'),
('E004', 'Baileys Iris Cream 750 mL', 575000, 'Beer', 100, 'Baileys Iris Cream 750 mL adalah minuman krim likuer yang lezat dan menggugah selera.', '\\bin\\debug\\FOTO PRODUK\\28.jpg'),
('E005', 'Soju Jinro Plum 360 mL', 95000, 'Beer', 100, 'Soju Jinro Plum adalah minuman beralkohol yang populer asal Korea Selatan.', '\\bin\\debug\\FOTO PRODUK\\29.jpg'),

('F001', 'Wipol Karbol Cemara 750 mL', 18000, 'Cleaners', 100, 'Memiliki lantai dan ruangan yang bersih dari kuman merupakan salah satu indikasi penting kebersihan rumah secara menyeluruh.', '\\bin\\debug\\FOTO PRODUK\\30.jpg'),
('F002', 'SOS Karbol lantai Refill 700 mL', 15000, 'Cleaners', 100, 'Salah satu varian dari cairan pembersih lantai SOS.', '\\bin\\debug\\FOTO PRODUK\\31.jpg'),
('F003', 'Bayclin Cairan Pemutih 1L', 20000, 'Cleaners', 100, 'Produk pembersih yang efektif dan berkualitas tinggi yang digunakan untuk memutihkan dan membersihkan berbagai permukaan.', '\\bin\\debug\\FOTO PRODUK\\32.jpg'),
('F004', 'Attack Easy Deterjen 700 gr', 23000, 'Cleaners', 100, 'Deterjen yang bekerja cepat dan mampu membersihkan kotoran secara maksimal.', '\\bin\\debug\\FOTO PRODUK\\33.jpg'),
('F005', 'Sunlight Jeruk Nipis 700 mL', 16000, 'Cleaners', 100, 'Mampu menghilangkan lemak dan secara aktif mengangkat dan menghilangkan lemak membandel.', '\\bin\\debug\\FOTO PRODUK\\34.jpg'),
('F006', 'Rinso Anti Noda Deterjen 1.2 Kg', 45000, 'Cleaners', 100, 'Produk deterjen yang efektif dalam menghilangkan noda membandel pada pakaian.', '\\bin\\debug\\FOTO PRODUK\\35.jpg'),

('G001', 'Close Up Pasta Gigi 160 gr', 19500, 'Personal Care', 100, 'Close up pasta gigi 160 gr adalah produk perawatan gigi yang dirancang untuk memberikan kebersihan maksimal', '\\bin\\debug\\FOTO PRODUK\\36.jpg'),
('G002', 'Pepsodent Pasta Gigi 225 gr', 14000, 'Personal Care', 100, 'Pepsodent pasta gigi white diformulasikan untuk membantu mengangkat noda sehingga gigi tampak putih alami.', '\\bin\\debug\\FOTO PRODUK\\37.jpg'),
('G003', 'Colgate Pasta Gigi 160 gr', 16000, 'Personal Care', 100, 'Colgate Pasta Gigi 160 gr dirancang untuk memberikan perlindungan dan kebersihan gigi yang optimal.', '\\bin\\debug\\FOTO PRODUK\\38.jpg'),
('G004', 'Tresemme Shampoo Keratin 170 mL', 37000, 'Personal Care', 100, 'Tresemme Shampoo Keratin 170 mL adalah sebuah produk perawatan rambut yang dikembangkan oleh merek Tresemme.', '\\bin\\debug\\FOTO PRODUK\\39.jpg'),
('G005', 'Grace and Glow Oil Control 400 mL', 64000, 'Personal Care', 100, 'Grace and Glow Oil Control mengandung bahan-bahan yang membantu mengurangi kilap berlebih pada wajah.', '\\bin\\debug\\FOTO PRODUK\\40.jpg'),
('G006', 'Head&Shoulders Shampoo 400 mL', 60000, 'Personal Care', 100, 'Head & Shoulders Shampoo 400 mL mengandung formula anti-ketombe yang efektif.', '\\bin\\debug\\FOTO PRODUK\\41.jpg'),

('H001', 'Tissue NICE 180 lembar', 9000, 'Paper Goods', 100, 'Tissue NICE 180 lembar adalah sebuah produk tisu yang dirancang untuk memberikan kenyamanan dan kepraktisan dalam kebutuhan sehari-hari.', '\\bin\\debug\\FOTO PRODUK\\42.jpg'),
('H002', 'Paseo Tissue Smart 250 lembar', 8000, 'Paper Goods', 100, 'Paseo Tissue Smart adalah sebuah merek tisu yang menawarkan kualitas dan kepraktisan dalam satu produk.', '\\bin\\debug\\FOTO PRODUK\\43.jpg'),
('H003', 'Tissue See U toilet roll 10 roll', 20000, 'Paper Goods', 100, 'Setiap gulungan tisu toilet ini terbuat dari bahan berkualitas tinggi yang lembut dan kuat.', '\\bin\\debug\\FOTO PRODUK\\44.jpg'),
('H004', 'Tissue Multi Pop Up Facial 200 lembar', 7000, 'Paper Goods', 100, 'Tisu ini dirancang khusus untuk perawatan wajah sehari-hari.', '\\bin\\debug\\FOTO PRODUK\\45.jpg'),
('H005', 'Guardian Facial Tissue 200 lembar', 15000, 'Paper Goods', 100, 'Tisu wajah ini ideal digunakan untuk berbagai kebutuhan sehari-hari, baik untuk membersihkan wajah dan menyerap keringat.', '\\bin\\debug\\FOTO PRODUK\\46.jpg'),

('I001', 'Kanzler Sosis Beef Cocktail 250 gr', 27000, 'Meat Fish', 100, 'Sosis ini memiliki berat kemasan 250 gram dan terbuat dari daging sapi pilihan yang segar.', '\\bin\\debug\\FOTO PRODUK\\47.jpg'),
('I002', 'Ikan Kakap Fillet 100 gr', 32000, 'Meat Fish', 100, 'Ikan kakap adalah jenis ikan laut yang populer dikonsumsi karena dagingnya yang lezat dan teksturnya yang lembut.', '\\bin\\debug\\FOTO PRODUK\\48.jpg'),
('I003', 'Kanzler Crispy Chicken Nugget 450 gr', 40000, 'Meat Fish', 100, 'Produk makanan siap saji yang terdiri dari potongan daging ayam lezat yang dilapisi dengan remah-remah tepung crispy.', '\\bin\\debug\\FOTO PRODUK\\49.jpg'),
('I004', 'Salmon Trout Premium Fillet 200 gr', 75000, 'Meat Fish', 100, 'Fillet ikan salmon trout ini merupakan pilihan yang sempurna untuk pecinta makanan laut yang menginginkan rasa dan kualitas yang tinggi.', '\\bin\\debug\\FOTO PRODUK\\50.jpg'),
('I005', 'Sosis Kanzler Singles 65 gr', 7500, 'Meat Fish', 100, 'Sosis ini terbuat dari daging sapi segar pilihan yang diolah dengan hati-hati untuk memberikan rasa yang lezat dan tekstur yang sempurna.', '\\bin\\debug\\FOTO PRODUK\\51.jpg'),

('J001', 'Chitato Sapi Panggang 68 gr', 11000, 'Snack', 100, 'Dibuat dengan menggunakan bahan-bahan berkualitas tinggi dan diproses dengan sempurna untuk memberikan rasa yang kaya dan gurih.', '\\bin\\debug\\FOTO PRODUK\\52.jpg'),
('J002', 'Pringles Potato Chips 107 gr', 20000, 'Snack', 100, 'Pringles Potato Chips 107 gram merupakan pilihan yang praktis dan cocok untuk menemani mu.', '\\bin\\debug\\FOTO PRODUK\\53.jpg'),
('J003', 'Nestle Sereal Koko Crunch 170 gr', 21000, 'Snack', 100, 'Sereal ini memiliki rasa dan tekstur yang lezat, dengan campuran biji-bijian gandum garing yang dilapisi dengan lapisan cokelat kaya.', '\\bin\\debug\\FOTO PRODUK\\54.jpg'),
('J004', 'Qtela Singkong Balado 185 gr', 13000, 'Snack', 100, 'Rasakan sensasi kelezatan dan kecrispyan singkong dengan rasa balado yang memukau dalam setiap gigitan.', '\\bin\\debug\\FOTO PRODUK\\55.jpg'),
('J005', 'Pilus Tic Tac Snack 80 gr', 7000, 'Snack', 100, 'Dengan rasa yang lezat dan kemasan yang praktis, camilan ini menjadi teman yang sempurna untuk menghilangkan rasa lapar di setiap kesempatan.', '\\bin\\debug\\FOTO PRODUK\\56.jpg');

INSERT INTO promoproduk (`idproduk`, `hargapromo`, `statuspromo`) VALUES
('A002', 22750, 'Aktif'), -- ORANGE
('A006', 47250, 'Aktif'), -- GRAPE
('B005', 15400, 'Tidak aktif'), -- AVOCADO
('B001', 21000, 'Aktif'), -- TOMATO
('B003', 28000, 'Aktif'), -- BROCCOLI
('B004', 14000, 'Tidak aktif'), -- GARLIC
('C002', 18450, 'Aktif'), -- GREENFIELDS
('C006', 103500, 'Aktif'), -- MOZARELLA
('D004', 27000, 'Tidak aktif'), -- DR PEPPER
('D006', 8550, 'Aktif'), -- GREENSANDS
('E003', 832500, 'Aktif'), -- JACK DANIELS
('E005', 85500, 'Tidak aktif'), -- SOJU
('F005', 14400, 'Tidak aktif'), -- SUNLIGHT
('F006', 40500, 'Aktif'), -- RINSO
('G003', 14400, 'Aktif'), -- COLGATE
('H001', 8100, 'Tidak aktif'), -- NICE
('I001', 24300, 'Tidak aktif'),-- KANZLER SOSIS BEEF
('I003', 36000, 'Aktif'), -- KANZLER CHICKEN
('J001', 9900, 'Aktif'), -- CHITATO
('J004', 11700, 'Tidak aktif'); -- QTELA

-- password harus ada huruf besar, hurut kecil dan angka
INSERT INTO customer (`idcustomer`, `namacustomer`, `telpcustomer`, `alamatcustomer`, `emailcustomer`, `passwordcustomer`) VALUES
('C001', 'Franco', '082978342090', 'Jalan Merdeka No 40', 'francoster65@gmail.com', 'Franco65'),
('C002', 'Lylia', '087221990546', 'Jalan Imam Bonjol No 101 A', 'cedartree@gmail.com', 'l1LuhZu2'),
('C003', 'Lancelot', '082976732090', 'Gang Turi No 88', 'replaytop@gmail.com', 'qm8LzfE9'),
('C004', 'Estes', '0812894905462', 'Jalan Gunung Sari No F 09', 'twiggles77@gmail.com', 'fru1tyBB'),
('C005', 'Loki', '08462789543', 'Gang Ikan Putih No 221', 'escobar530@gmail.com', 'Bmb4stic'),
('C006', 'Harley', '0361346743', 'Waterfront 09 G', 'builder0681@gmail.com', 'Magici4n'),
('C007', 'Esmeralda', '08200943216', 'Pasar Atoom Street 2152 T', 'lelixey@gmail.com', 'Pyramid8'),
('C008', 'Helcurt', '08832475901', 'Harmoni Plaza Bl K/11', 'redlined@gmail.com', 'CCoTN1JQ'),
('C009', 'Moskov', '08200943216', 'Jalan P Jayakarta 20 Bl B/10', 'movie3263@gmail.com','b3rryBer'),
('C010', 'Odette', '08832475901', 'Jalan Serdang Baru 4 Gg V/7 RT 011/04', 'maverick1977@gmail.com', 'Fifa2009' ),
('C011', 'Cecillion', '08134563200', 'Glodok Plaza Blok K/A-9', 'mejramov@gmail.com','99Danger'),
('C012', 'Miya', '084367953789', '88 Apartment unit 110', 'ghostevil@gmail.com', '6qYOUYEa'),
('C013', 'Alice', '0361485214', 'Jalan Nanas I', 'jiuji55@gmail.com','YILu3psO'),
('C014', 'Nana', '0361368052', 'Pasar Baru Pd Gede Bl A/2','toxicllama@gmail.com','nanana9A'),
('C015', 'Rafaela', '08462736904', 'Kompl Ruko Kalideres Indah Bl D/9', 'evanceteeng@gmail.com','Healing4'),
('C016', 'Kagura', '08834907613', 'Jalan Senangin 11-11 A/13', 'papilloninthegarden@gmail.com', '123a567B'),
('C017', 'Irithel', '0810943786', 'Jalan Mangga Dua Raya Bl E3/9', 'sweetcreature909@gmail.com', 'k9WCPFfd'),
('C018', 'Pharsa', '087348410', 'Denver Apartment Unit 2890', 'setyourheartablaze@gmail.com', 'Mikaela0'),
('C019', 'Lunox', '0361390631', 'Cornell Apartment Unit 3011', 'mickeyandminniemouse@gmail.com', '87G5432i'),
('C020', 'Carmilla', '08907693020', 'Margomulyo Permai H-8', 'bigguylittleheart@gmail.com', 'o4xh9Yma');

INSERT INTO transaksi (`idtransaksi`, `tanggaltransaksi`, `totalharga`, `idcustomer`, `statuspesanan`) VALUES
('T001', '2022-01-01', 19500, 'C001', 'Sudah diterima'),
('T002', '2022-02-17', 20500, 'C002', 'Sudah diterima'),
('T003', '2022-02-28', 20000, 'C003', 'Sudah diterima'),
('T004', '2022-03-05', 21000, 'C004', 'Sudah diterima'),
('T005', '2022-05-29', 67500, 'C005', 'Sudah diterima'),
('T006', '2022-07-07', 130000, 'C006', 'Sudah diterima'),
('T007', '2022-09-13', 17000, 'C007', 'Sudah diterima'),
('T008', '2022-10-20', 1020000, 'C008', 'Sudah diterima'),
('T009', '2022-11-02', 94000, 'C009', 'Sudah diterima'),
('T010', '2022-12-30', 95000, 'C010', 'Sudah diterima'),
('T011', '2023-01-01', 275000, 'C011', 'Sudah diterima'),
('T012', '2023-01-17', 148000, 'C012', 'Sudah diterima'),
('T013', '2023-02-07', 143400, 'C013', 'Sudah diterima'),
('T014', '2023-02-24', 55000, 'C014', 'Sudah diterima'),
('T015', '2023-03-25', 1595000, 'C015', 'Sedang diproses'),
('T016', '2023-03-26', 79000, 'C016', 'Sedang diproses'),
('T017', '2023-04-12', 115000, 'C017', 'Sedang diproses'),
('T018', '2023-04-19', 332000, 'C018', 'Sedang diproses'),
('T019', '2023-05-02', 61000, 'C019', 'Sedang diproses'),
('T020', '2023-05-08', 20000, 'C020', 'Sedang diproses');

INSERT INTO history (`idtransaksi`, `idcustomer`, `namacustomer`, `namaproduk`, `hargaproduk`, `quantity`, `totalharga`, `statuspesanan`) VALUES
('T001', 'C001', 'Franco', 'Potato', 19500, 1, 19500, 'Sudah diterima'),
('T002', 'C002', 'Lylia', 'Fresh Milk Greenfields 1L', 20500, 1, 20500, 'Sudah diterima'),
('T003', 'C003', 'Lancelot','Tissue See U toilet roll 10 roll', 20000, 1, 20000, 'Sudah diterima'),
('T004', 'C004', 'Estes', 'Nestle Sereal Koko Crunch 170 gr', 21000, 1, 21000, 'Sudah diterima'),
('T005', 'C005', 'Loki', 'Grape', 67500, 1, 67500, 'Sudah diterima'),
('T006', 'C006', 'Harley', 'Perfetto Mozarella Cheese', 115000, 1, 115000, 'Sudah diterima'),
('T006', 'C006', 'Harley', 'Coca Cola 1,5 mL', 15000, 1,  15000, 'Sudah diterima'),
('T007', 'C007', 'Esmeralda', 'Onion', 10000, 1, 10000, 'Sudah diterima'),
('T007', 'C007', 'Esmeralda', 'A&W Sarsaparila 250 mL', 7000, 1, 7000, 'Sudah diterima'),
('T008', 'C008', 'Helcurt', 'Jack Daniels Old No.7 700 mL', 925000, 1, 925000, 'Sudah diterima'),
('T008', 'C008', 'Helcurt', 'Soju Jinro Plum 360 mL', 95000, 1, 95000, 'Sudah diterima'),
('T009', 'C009', 'Moskov','SOS Karbol lantai Refill 700 mL', 15000, 1, 15000, 'Sudah diterima'),
('T009', 'C009', 'Moskov','Grace and Glow Oil Control 400 mL', 64000, 1, 64000, 'Sudah diterima'),
('T009', 'C009', 'Moskov','Guardian Facial Tissue 200 lembar', 15000, 1, 15000, 'Sudah diterima'),
('T010', 'C010', 'Odette', 'Salmon Trout Premium Fillet 200 gr', 75000, 1, 75000, 'Sudah diterima'),
('T010', 'C010', 'Odette', 'Pringles Potato Chips 107 gr', 20000, 1, 20000, 'Sudah diterima'),
('T011', 'C011', 'Cecillion', 'Smirnoff 750 mL', 275000, 1, 275000, 'Sudah diterima'),
('T012', 'C012', 'Miya', 'Kiwi', 87000, 1, 87000, 'Sudah diterima'),
('T012', 'C012', 'Miya', 'Banana', 27000, 1, 27000, 'Sudah diterima'),
('T012', 'C012', 'Miya', 'Apple', 34000, 1, 34000, 'Sudah diterima'),
('T013', 'C013', 'Alice', 'Avocado', 22000, 1, 22000, 'Sudah diterima'),
('T013', 'C013', 'Alice', 'Grape', 67500, 1, 67500, 'Sudah diterima'),
('T013', 'C013', 'Alice', 'Broccoli', 40000, 1, 40000, 'Sudah diterima'),
('T013', 'C013', 'Alice', 'Carrot', 13900, 1, 13900, 'Sudah diterima'),
('T014', 'C014', 'Nana', 'Schweppes 330 mL', 7000, 1, 7000, 'Sudah diterima'),
('T014', 'C014', 'Nana', 'Yogurt Greenfields 500 mL', 48000, 1, 48000, 'Sudah diterima'),
('T015', 'C015', 'Rafaela', 'Jack Daniels Old No.7 700 mL', 925000, 1, 925000, 'Sedang diproses'),
('T015', 'C015', 'Rafaela', 'Soju Jinro Plum 360 mL', 95000, 1, 95000, 'Sedang diproses'),
('T015', 'C015', 'Rafaela', 'Baileys Iris Cream 750 mL', 575000, 1, 575000, 'Sedang diproses'),
('T016', 'C016', 'Kagura','Grace and Glow Oil Control 400 mL', 64000, 1, 64000, 'Sedang diproses'),
('T016', 'C016', 'Kagura','Guardian Facial Tissue 200 lembar', 15000, 1, 15000, 'Sedang diproses'),
('T017', 'C017', 'Irithel', 'Tissue See U toilet roll 10 roll', 20000, 1, 20000, 'Sedang diproses'),
('T017', 'C017', 'Irithel', 'Salmon Trout Premium Fillet 200 gr', 75000, 1, 75000, 'Sedang diproses'),
('T017', 'C017', 'Irithel', 'Pringles Potato Chips 107 gr', 20000, 1, 20000, 'Sedang diproses'),
('T018', 'C018', 'Pharsa', 'Cheedar Kraft Cheese', 23000, 1, 23000, 'Sedang diproses'),
('T018', 'C018', 'Pharsa', 'Smirnoff 750 mL', 275000, 1, 275000, 'Sedang diproses'),
('T018', 'C018', 'Pharsa', 'Apple', 34000, 1, 34000, 'Sedang diproses'),
('T019', 'C019', 'Lunox', 'Banana', 27000, 2, 54000, 'Sedang diproses'),
('T019', 'C019', 'Lunox', 'Apple', 34000, 1, 34000, 'Sedang diproses'),
('T020', 'C020', 'Carmilla', 'Chocolate Ice Cream Magnum', 20000, 1, 20000, 'Sedang diproses');

INSERT INTO cart (`idcustomer`, `idproduk`, `quantity`) VALUES
('C001', 'F001', 2),
('C002', 'F002', 1),
('C003', 'A001', 1),
('C004', 'A003', 3),
('C005', 'C005', 4),
('C006', 'F001', 2),
('C007', 'D005', 1),
('C008', 'H001', 1),
('C009', 'I002', 3),
('C010', 'A003', 4),
('C011', 'B005', 2),
('C012', 'F003', 1),
('C013', 'D002', 1),
('C014', 'B001', 3),
('C015', 'J003', 4),
('C016', 'F004', 2),
('C017', 'V001', 1),
('C018', 'C003', 1),
('C019', 'A005', 3),
('C020', 'A004', 4);

INSERT INTO fav(`idcustomer`, `idproduk`) VALUES
('C001', 'F003'),
('C002', 'F001'),
('C003', 'A005'),
('C004', 'A003'),
('C005', 'J001'),
('C006', 'F003'),
('C007', 'D004'),
('C008', 'B002'),
('C009', 'B001'),
('C010', 'I004'),
('C011', 'I005'),
('C012', 'F003'),
('C013', 'D002'),
('C014', 'J004'),
('C015', 'A005'),
('C016', 'F002'),
('C017', 'B005'),
('C018', 'C003'),
('C019', 'A001'),
('C020', 'A002');