// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

public class Kullanici
{
    public string KullaniciAdi { get; set; }
    public string Sifre { get; set; }
    public string Email { get; set; }

    public Kullanici(string kullaniciAdi, string sifre, string email)
    {
        KullaniciAdi = kullaniciAdi;
        Sifre = sifre;
        Email = email;
    }

    public bool GirisYap(string sifre)
    {
        return Sifre == sifre;
    }
}

public class Urun
{
    public string Adi { get; set; }
    public double Fiyat { get; set; }
    public string Aciklama { get; set; }
    public int StokMiktari { get; set; }

    public Urun(string adi, double fiyat, string aciklama, int stokMiktari)
    {
        Adi = adi;
        Fiyat = fiyat;
        Aciklama = aciklama;
        StokMiktari = stokMiktari;
    }

    public void UrunGuncelle(string adi, double fiyat, string aciklama, int stokMiktari)
    {
        Adi = adi;
        Fiyat = fiyat;
        Aciklama = aciklama;
        StokMiktari = stokMiktari;
    }
}

public class Sepet
{
    public List<Urun> Urunler { get; set; } = new List<Urun>();

    public void SepeteEkle(Urun urun)
    {
        Urunler.Add(urun);
    }

    public void SepetiGoster()
    {
        double toplamTutar = 0;
        Console.WriteLine("Sepetinizdeki Ürünler:");
        foreach (var urun in Urunler)
        {
            Console.WriteLine($"{urun.Adi} - {urun.Fiyat} TL");
            toplamTutar += urun.Fiyat;
        }
        Console.WriteLine($"Toplam Tutar: {toplamTutar} TL");
    }

    public void UrunCikarma(string urunAdi)
    {
        var urun = Urunler.FirstOrDefault(x => x.Adi == urunAdi);
        if (urun != null)
        {
            Urunler.Remove(urun);
            Console.WriteLine($"{urunAdi} sepetten çıkarıldı.");
        }
        else
        {
            Console.WriteLine($"{urunAdi} sepetinizde bulunmuyor.");
        }
    }
}

public class Siparis
{
    public Kullanici Kullanici { get; set; }
    public List<Urun> Urunler { get; set; }
    public string Durum { get; set; }

    public Siparis(Kullanici kullanici, List<Urun> urunler)
    {
        Kullanici = kullanici;
        Urunler = urunler;
        Durum = "Hazırlanıyor";
    }

    public void SiparisDurumuGoster()
    {
        Console.WriteLine($"Sipariş Durumu: {Durum}");
    }
}

public class Odeme
{
    public double Tutar { get; set; }
    public string OdemeYontemi { get; set; }

    public Odeme(double tutar, string odemeYontemi)
    {
        Tutar = tutar;
        OdemeYontemi = odemeYontemi;
    }

    public void OdemeIslemi()
    {
        Console.WriteLine($"{OdemeYontemi} ile {Tutar} TL ödeme başarılı.");
    }
}

public class Admin
{
    public string AdminAdi { get; set; }

    public Admin(string adminAdi)
    {
        AdminAdi = adminAdi;
    }

    public void UrunEkle(List<Urun> urunler, string adi, double fiyat, string aciklama, int stokMiktari)
    {
        urunler.Add(new Urun(adi, fiyat, aciklama, stokMiktari));
        Console.WriteLine($"{adi} ürünü sisteme eklendi.");
    }

    public void UrunSil(List<Urun> urunler, string adi)
    {
        var urun = urunler.FirstOrDefault(x => x.Adi == adi);
        if (urun != null)
        {
            urunler.Remove(urun);
            Console.WriteLine($"{adi} ürünü sistemden silindi.");
        }
        else
        {
            Console.WriteLine($"{adi} ürünü bulunamadı.");
        }
    }

    public void UrunGuncelle(List<Urun> urunler, string adi, string yeniAdi, double yeniFiyat, string yeniAciklama, int yeniStokMiktari)
    {
        var urun = urunler.FirstOrDefault(x => x.Adi == adi);
        if (urun != null)
        {
            urun.UrunGuncelle(yeniAdi, yeniFiyat, yeniAciklama, yeniStokMiktari);
            Console.WriteLine($"{adi} ürünü güncellendi.");
        }
        else
        {
            Console.WriteLine($"{adi} ürünü bulunamadı.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Urun> urunler = new List<Urun>
        {
            new Urun("Laptop", 5000, "Gaming Laptop", 10),
            new Urun("Telefon", 3000, "Akıllı Telefon", 20),
            new Urun("Kulaklık", 500, "Kablosuz Kulaklık", 30)
        };

        Kullanici kullanici = new Kullanici("ali", "1234", "ali@example.com");
        Admin admin = new Admin("admin");

        Sepet sepet = new Sepet();

        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("E-Ticaret Sistemi");
            Console.WriteLine("1. Ürünleri Görüntüle");
            Console.WriteLine("2. Sepete Ürün Ekle");
            Console.WriteLine("3. Sepeti Görüntüle");
            Console.WriteLine("4. Sepetten Ürün Çıkar");
            Console.WriteLine("5. Sipariş Ver");
            Console.WriteLine("6. Çıkış");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Mevcut Ürünler:");
                    for (int i = 0; i < urunler.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {urunler[i].Adi} - {urunler[i].Fiyat} TL");
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("Sepete eklemek istediğiniz ürünün numarasını girin: ");
                    if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= urunler.Count)
                    {
                        sepet.SepeteEkle(urunler[productIndex - 1]);
                        Console.WriteLine($"{urunler[productIndex - 1].Adi} sepete eklendi.");
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz ürün numarası.");
                    }
                    break;

                case "3":
                    Console.Clear();
                    sepet.SepetiGoster();
                    break;

                case "4":
                    Console.Clear();
                    Console.Write("Çıkarmak istediğiniz ürünün adını girin: ");
                    string urunAdi = Console.ReadLine();
                    sepet.UrunCikarma(urunAdi);
                    break;

                case "5":
                    Console.Clear();
                    if (sepet.Urunler.Count > 0)
                    {
                        double toplamTutar = sepet.Urunler.Sum(u => u.Fiyat);
                        Odeme odeme = new Odeme(toplamTutar, "Kredi Kartı");
                        odeme.OdemeIslemi();
                        Siparis siparis = new Siparis(kullanici, sepet.Urunler);
                        siparis.SiparisDurumuGoster();
                    }
                    else
                    {
                        Console.WriteLine("Sepetiniz boş. Sipariş verilemez.");
                    }
                    break;

                case "6":
                    isRunning = false;
                    Console.WriteLine("Çıkılıyor...");
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("Devam etmek için bir tuşa basın...");
                Console.ReadKey();
            }
        }
    }
}

