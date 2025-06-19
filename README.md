# Aia ärataja

## Tallinna ülikooli Digitehnoloogiate instituudi suvepraktika 2025 

Antud projekti eesmärgiks oli luua mäng pikselgraafikaga mäng, mis tooks rohkem tähelepanu TLÜ ülikooliaiale. Mängus liigud ringi, räägid tegelastega, kes annavad taimi ja siis istutad neid maha. Kui kõik taimed(6) on kogutud, on mäng lõppenud.
Mängu idee on ELU aine grupi nr.2 "Jätkusuutlik ülikooliaed" poolt pakutud välja. 

Tegemist on Unity ja C# kirjutatud pikselgraafika mänguga.

## Kasutatud tehnoloogiad
* Unity 2022.3.6f1
   * Packages
      * TextMeshPro
      * WebGL publisher
      * Input System
   * Renci.SshNet 2020.0.2.0
   * System.Data.SqlClient 4.6.27618.1
   * UnityNpgSQL 2.2.7.0

## Paigaldusjuhised
1. Looge tühi 2D Unity projekt
2. Laadige alla GitHub _repo_'st Assets, Usersettings, Projectsettings, Packages
3. Asendada tühja projekti failid ära allalaetud omadega
4. Nüüd on kõik laetud Unitysse
5. Ava Unitys Assets --> Scenes
6. Avage scene, mille kallal soovite tegeleda'
<details>
<summary>7. Et andmebaasiga ühendust saada, muuta DBconnection.cs failis leitav andmete fail oma faili asukohaga</summary>
Fail näeb välja selline
lin2User=YOURUSER<br>
lin2Pass=YOURPASS<br>
greenyUser=YOURGREENYDATABASEUSER<br>
greenyPass=YOURGREENYDATABASEUSER<br>
</details>

[Andmebaas](https://github.com/TaunoLainevool/YlikooliAed-admin)

## Pildid
Peamenüü<br>
![Screenshot 2025-06-19 115444](https://github.com/user-attachments/assets/8c436ea5-0bd5-49ab-aced-08f7ad6a12de)
Edetabel<br>
![edetabel](https://github.com/user-attachments/assets/bb3cfea8-eaa9-4513-8a45-cbc2de411fa2)
Tegelase isikustamine<br>
![customization](https://github.com/user-attachments/assets/9fe208a6-0bab-439d-9f44-1fad2f264ec2)
Mänguala<br>
![game area](https://github.com/user-attachments/assets/8dc4ed4a-81f7-4659-926b-9ff2e2de62fa)
Tegelasega rääkimine<br>
![talking](https://github.com/user-attachments/assets/dffad83f-41f2-4a6e-9403-324842622ca8)

## Meeskond
* Ralf Soiela
* Renat Magsumov
* Tauno Lainevool
* Carl-Eric Sepp
