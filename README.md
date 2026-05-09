# sonHali — VR Çevre & Geri Dönüşüm Eğitim Oyunu

> Unity 2022 LTS + Meta XR / OpenXR ile geliştirilmiş, eğitim odaklı bir VR uygulaması. Oyuncu sanal sahnelerde (mutfak, bahçe, fabrika, sınıf) çöp ayrıştırma ve geri dönüşüm görevleri yaparak öğrenir.

---

## Projenin Amacı

Sanal gerçeklik ortamında, çevre bilinci ve geri dönüşüm konularında interaktif bir eğitim deneyimi sunmak. Kullanıcı:

- Çöpleri doğru kategorilere (organik, plastik, cam, kağıt vb.) ayırır
- Atıkları konteynerlere taşır, kamyonlarla toplar
- Görev tabanlı sahnelerde puanlanır ve ilerler
- Birden fazla ortam üzerinden (mutfak, bahçe, fabrika, sınıf) öğrenmeyi pekiştirir

---

## Kullanılan Teknolojiler

| Katman | Teknoloji |
|---|---|
| Engine | **Unity 2022.3.62f3 (LTS)** |
| Render | Universal Render Pipeline (URP) 14.0.12 |
| XR | OpenXR 1.14.3 + XR Interaction Toolkit 3.1.2 + XR Hands 1.8.0 |
| Hedef Platform | **Meta Quest** (Meta XR SDK All 85.0.0) — ayrıca PC VR uyumlu |
| Backend | Firebase (Auth, Firestore, Realtime DB, Storage, Analytics) |
| Input | Unity Input System 1.14.0 |
| AR (mevcut) | AR Foundation 5.2.2 |
| Dil | C# |

---

## Temel Özellikler

- 🥽 **VR Etkileşim**: XR Interaction Toolkit ile el takibi ve controller desteği
- 🌍 **Çoklu Sahne**: `main`, `bahce`, `mutfak`, `fabrika`, `sınıf`, `BasicScene`
- ♻️ **Atık Yönetimi**: `WasteItem`, `WasteType`, `BinZone`, `Task2BinZone`, çöp kovaları ve konteynerler
- 🚛 **Kamyon Sistemi**: `TruckController`, `TruckDeliveryZone`, `TruckDropZone`, `ConveyorMover`
- 📋 **Görev Akışı**: `TaskManager`, `Task2Manager`, ekrana bilgi paneli (`InfoPanelInteract`)
- ⏱️ **Süreli Mücadele**: `WasteCountdown` ile geri sayım
- 🔥 **Firebase Entegrasyonu**: Kullanıcı kimliği, sahne ilerleme kaydı, veri analitiği
- 🎯 **Sahne Sayacı**: `SahneSayaci`, `OyunBitirici` ile oyun akışı kontrolü
- 🎨 **URP + Waste Overgrowth Asset**: Distopik/atık temalı çevre görselleri

---

## Kurulum Adımları

### Ön Gereksinimler
- [Unity Hub](https://unity.com/download)
- **Unity 2022.3.62f3** (LTS) — Unity Hub üzerinden Android Build Support modülüyle kurun (Quest için)
- Git + [Git LFS](https://git-lfs.com/) (zorunlu — büyük binary dosyalar LFS üzerinden depolanır)
- Meta Quest cihazı (geliştirme modu açık) veya XR Device Simulator
- Firebase projesi (kendi projenizi oluşturmanız gerekir, aşağıya bakın)

### 1. Repoyu klonlayın

```powershell
git lfs install
git clone https://github.com/<KULLANICI_ADI>/sonHali.git
cd sonHali
```

> ⚠️ `git lfs install` komutunu klonlamadan ÖNCE çalıştırın, aksi halde LFS dosyaları placeholder olarak iner.

### 2. Firebase Unity SDK'yı indirin

Repo, dosya boyutu nedeniyle Firebase Unity SDK kurulum paketlerini içermez. Aşağıdaki adımları uygulayın:

1. https://firebase.google.com/download/unity adresinden Firebase Unity SDK'yı indirin (proje Firebase 13.x kullanmaktadır)
2. ZIP'i açın
3. Unity'de proje açıkken `Assets > Import Package > Custom Package` üzerinden ihtiyacınız olan modülleri import edin:
   - `FirebaseAuth.unitypackage`
   - `FirebaseFirestore.unitypackage`
   - `FirebaseDatabase.unitypackage`
   - `FirebaseStorage.unitypackage`
   - `FirebaseAnalytics.unitypackage`
   - (Gerekiyorsa diğerleri)

### 3. Firebase yapılandırması

Repo, gizlilik nedeniyle `google-services.json` dosyasını içermez. Şablon olarak `Assets/firebase/google-services.example.json` bırakılmıştır.

1. https://console.firebase.google.com adresinden kendi projenizi oluşturun
2. Android uygulaması ekleyin — paket adı **`com.Unity.Technologies.VR.Template`** (veya `Player Settings`'te belirlediğiniz paket adı)
3. `google-services.json` dosyasını indirin
4. Şu konuma kopyalayın: `Assets/firebase/google-services.json`
5. Desktop için: Firebase Unity SDK'nın oluşturduğu `google-services-desktop.json` dosyasını `Assets/StreamingAssets/` altına yerleştirin

### 4. Projeyi Unity'de açın

1. Unity Hub'da **Open** → klonlanan klasörü seçin
2. Unity ilk açılışta `Library/` klasörünü yeniden üretecektir (5–15 dk sürebilir)
3. Açılınca: `Assets/Scenes/main.unity` sahnesini açın

---

## Çalıştırma Komutları

| İşlem | Yol |
|---|---|
| Editor'de oynat | Unity Editor → Play butonu (XR Device Simulator ile test edilebilir) |
| Quest cihazına build | `File > Build Settings > Android > Build And Run` (Quest USB ile bağlı) |
| PC VR (OpenXR) | `File > Build Settings > Windows > Build` |

---

## Klasör Yapısı

```
sonHali/
├── Assets/                       # Unity asset'leri (kod, sahne, model, materyal)
│   ├── Scenes/                   # Sahneler: main, bahce, fabrika, mutfak, sınıf, BasicScene
│   ├── aliassets/Scripts/        # Oyun mantığı (Task, Bin, Truck, Waste sistemleri)
│   ├── Menu SC/                  # Menü, analiz, ajanda script'leri
│   ├── Material/, Prefabs/       # Materyaller ve prefab'lar
│   ├── Plugins/                  # 3rd-party plugin'ler
│   ├── firebase/                 # Firebase (config + native lib'ler — config gitignore'da)
│   ├── Oculus/                   # Meta XR / Oculus entegrasyon
│   ├── URP_WasteOvergrowth_SA/   # URP ortam asset paketi
│   ├── mutfak/, garden/, çöp/    # Sahne-spesifik modeller ve tekstür
│   ├── konteynır/, sahneler/     # Konteyner ve sahne asset'leri
│   ├── güncelsınıf/, npc/        # Sınıf ve NPC asset'leri
│   └── Resources/, Settings/     # Runtime kaynak ve URP ayarları
├── Packages/                     # Unity Package Manager manifest
├── ProjectSettings/              # Proje ayarları (commit edilir)
├── .gitignore                    # Library/, Logs/, .unitypackage vb. ignore kuralları
├── .gitattributes                # Git LFS + YAML merge ayarları
└── README.md
```

> `Library/`, `Logs/`, `Temp/`, `obj/`, `UserSettings/` klasörleri **commit edilmez** — Unity ilk açılışta yeniden üretir.
> `.unitypackage` installer dosyaları boyutu nedeniyle commit edilmez — Firebase SDK / asset paketlerini ilgili kaynaklardan tekrar indirin.

---

## Ekran Görüntüleri

> 🖼️ Aşağıya VR oyunundan ekran görüntüleri eklenecektir.

| Ana Menü | Mutfak Sahnesi | Bahçe Sahnesi |
|---|---|---|
| _(eklenecek)_ | _(eklenecek)_ | _(eklenecek)_ |

| Fabrika Sahnesi | Sınıf Sahnesi | Görev Tamamlama |
|---|---|---|
| _(eklenecek)_ | _(eklenecek)_ | _(eklenecek)_ |

Ekran görüntüleri için: `docs/screenshots/` klasörü oluşturup PNG'leri buraya koyun ve markdown'da referans verin:
```markdown
![Ana Menü](docs/screenshots/main_menu.png)
```

---

## Geliştirici

**Proje sahibi:** Yiğit Alakuş

İletişim ve katkı için:
- GitHub: [@yigitalakus1](https://github.com/yigitalakus1)
- E-posta: asmedius588@gmail.com

---

## Notlar

- Repo **Git LFS** kullanır. Klonlamadan önce `git lfs install` çalıştırın.
- Firebase API key'leri ve `google-services.json` dosyası gizlilik nedeniyle commit edilmez. Kendi Firebase projenizi yapılandırın.
- Unity sahne dosyaları (`*.unity`, `*.prefab`, `*.asset`) `Force Text` formatında ve `unityyamlmerge` ile merge edilecek şekilde ayarlanmıştır.

## Lisans

Lisans bilgisi henüz belirlenmemiştir.
