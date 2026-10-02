# Sarnaste piltide leidmise mäng

## Eesmärk ja kirjeldus
Mälumäng, kus mängija avab kaarte kaupa kaks korraga ja peab leidma kõik samasugused paarid.
Kaart (`Kaart`) on eraldi klass, mis teab oma paari ja oma sisu; pildid joonistatakse koodis lihtsate värviliste kujunditena.
Mängul on taimer, punktisüsteem ja kaks taset.

## Kasutatud tehnoloogiad
- C# (.NET 8)
- Windows Forms (`Panel`, `Button`, `Timer`, `System.Drawing`)

## Kuidas käivitada ja kasutada
1. Ava `KolmRakendust.sln` Visual Studio 2022-s või käivita käsurealt: `dotnet run --project Launcher`
2. Vali avaaknas **Sarnaste piltide mäng**.
3. Vali tase (4x4 või 6x6) ja režiim (Sümbolid või Pildid), vajuta **Uus mäng**.
4. Klõpsa kahel kaardil – kui need on samad, jäävad nad nähtavaks (roheline), muidu pöörduvad tagasi.
5. Mäng lõpeb, kui kõik paarid on leitud.

**Punktid:** +10 leitud paari eest, −2 vale paari eest (alla nulli ei lähe). Aeg algab esimesest klõpsust.

## Arendusideed (lisatud)
1. Tegelikud pildid sümbolite asemel (režiim "Pildid").
2. Taimer ja punktisüsteem.
3. Tasemed (4x4 ja 6x6).

## Eeldav edasi areng
- Parimate tulemuste salvestamine.
- Oma piltide laadimine kaustast.
- Kahe mängija režiim.
- Animatsioon kaardi ümberpööramisel ja helid.
