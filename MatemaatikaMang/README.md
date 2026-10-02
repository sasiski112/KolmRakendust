# Matemaatiline äraarvamismäng

## Eesmärk ja kirjeldus
Mäng, kus tuleb lahendada neli arvutust (liitmine, lahutamine, korrutamine, jagamine) enne aja lõppu.
Õige vastus värvub kohe roheliseks. Mängu saab teha kolmel raskusastmel ning programm arvestab punkte.
Ülesande loogika (`Ulesanne`) ja põhivorm (`MatemaatikaVorm`) on eraldi klassid.

## Kasutatud tehnoloogiad
- C# (.NET 8)
- Windows Forms (`NumericUpDown`, `ComboBox`, `Timer`, `TableLayoutPanel`)

## Kuidas käivitada ja kasutada
1. Ava `KolmRakendust.sln` Visual Studio 2022-s või käivita käsurealt: `dotnet run --project Launcher`
2. Vali avaaknas **Matemaatiline mäng**.
3. Vali raskusaste (Lihtne – 40 s, Keskmine – 30 s, Raske – 20 s).
4. Vajuta **Alusta mängu** ja sisesta vastused.
5. Mäng lõpeb, kui kõik vastused on õiged või aeg saab otsa.

**Punktid:** 10 punkti iga õige vastuse eest + 2 punkti iga allesjäänud sekundi eest (kui kõik on õiged).

## Arendusideed (lisatud)
1. Erinevad tehete tüübid (+, −, ×, ÷).
2. Ajapiirang ja punktiarvestus.
3. Raskusastme valik.

## Eeldav edasi areng
- Tulemuste tabel (parimad tulemused faili salvestamisega).
- Valitavad tehted (kasutaja valib märkeruutudega).
- Rohkem ülesandeid ühes voorus ja tasemete süsteem.
- Helid õige/vale vastuse puhul.
