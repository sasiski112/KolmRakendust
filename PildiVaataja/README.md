# Pildi vaataja

## Eesmärk ja kirjeldus
Lihtne Windows Forms'i rakendus piltide vaatamiseks. Kasutaja valib ühe või mitu pilti, saab neid vaadata,
muuta tausta värvi, käivitada automaatse slaidishow ja salvestada pildi teises failivormingus.
Kõik juhtelemendid on loodud koodis (ilma Toolbox'ita) ning kood on jaotatud meetoditeks vastavalt OOP põhimõtetele.

## Kasutatud tehnoloogiad
- C# (.NET 8)
- Windows Forms (`PictureBox`, `OpenFileDialog`, `SaveFileDialog`, `ColorDialog`, `Timer`)

## Kuidas käivitada ja kasutada
1. Ava `KolmRakendust.sln` Visual Studio 2022-s või käivita käsurealt: `dotnet run --project Launcher`
2. Vali avaaknas **Pildi vaataja**.
3. **Näita pilti** – vali pilt(e) failidialoogist. **Sobita pilt aknasse** – märkeruut, mis venitab/sobitab pildi.
4. **Vali taustavärv** – avab värvivaliku. **Tühjenda pilt** – eemaldab pildi.
5. **Alusta slaidishow'd** – vaheta pilte automaatselt (vähemalt 2 pilti, pilt vahetub iga 3 sekundi järel).
6. **Salvesta kui...** – salvesta pilt PNG, JPEG või BMP formaati.

## Arendusideed (lisatud)
1. Taustavärvi muutmine (`ColorDialog`).
2. Slaidishow automaatse vahetusega.
3. Pildi salvestamine teise formaati.

## Eeldav edasi areng
- Pööramine ja peegeldamine.
- Suurendamine hiireratta või liuguriga (zoom).
- Lihtsad filtrid (mustvalge, heledus, kontrast).
- Piltide pisipiltide riba ja kausta valik.
