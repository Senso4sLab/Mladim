using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mladim.Domain.Enums;

[Flags]
public enum OrganizationRegions
{
    [Display(Name = "Gorenjska")]
    Gorenjska = 1,
    [Display(Name = "Goriška")]
    Goriška = 2,
    [Display(Name = "Jugovzhodna-Slovenija")]
    Jugovzhodna = 4,
    [Display(Name = "Koroška")]
    Koroška = 8,
    [Display(Name = "Obalno-kraška")]
    Obalno = 16,
    [Display(Name = "Osrednjeslovenska")]
    Osrednjeslovenska = 32,
    [Display(Name = "Podravska")]
    Podravska = 64,
    [Display(Name = "Pomurska")]
    Pomurska = 128,
    [Display(Name = "Posavska")]
    Posavska = 265,
    [Display(Name = "Primorsko-notranjska")]
    Primorsko = 512,
    [Display(Name = "Savinjska")]
    Savinjska = 1042,
    [Display(Name = "Zasavska")]
    Zasavska = 2048,
};


[Flags]
public enum OrganizationTypes
{
    [Display(Name = "Mladinska organizacija")]
    Mladinska = 1,
    [Display(Name = "Nacionalna mladinska organizacija")]
    Nacionalna = 2,
    [Display(Name = "Mladinski center")]
    Center = 4,
    [Display(Name = "Druga nevladna organizacija")]
    Nevladne = 8,
};


[Flags]
public enum OrganizationStatus
{
    [Display(Name = "Društvo")]
    Društvo = 1,
    [Display(Name = "Zveza društev")]
    Zveza = 2,
    [Display(Name = "Politična stranka")]
    Stranka = 4,
    [Display(Name = "Sindikat")]
    Sindikat = 8,
    [Display(Name = "Zasebni zavod")]
    ZasebniZavod = 16,
    [Display(Name = "Javni zavod")]
    JavniZavod = 32,
    [Display(Name = "Sestavni del občinske uprave")]
    Uprava = 64,
    [Display(Name = "Mladinski svet")]
    MladinskiSvet = 128,
};


[Flags]
public enum AgeGroups
{
    [Display(Name = "12-14")]
    Age12_14 = 1,
    [Display(Name = "15-19")]
    Age15_19 = 2,
    [Display(Name = "20-24")]
    Age20_24 = 4,
    [Display(Name = "25-29")]
    Age25_29 = 8,
    [Display(Name = "30-35")]
    Age30_35 = 16,
};


[Flags]
public enum OrganizationFields
{
    [Display(Name = "Analiza, načrtovanje in organizacija dela")]
    Analiza = 1,
    [Display(Name = "Priprava dela oz. delovnega mesta")]
    Priprava = 2,
    [Display(Name = "Operativna dela")]
    Operativna = 4,
    [Display(Name = "Administrativna dela")]
    Administrativna = 8,
    [Display(Name = "Komercialna dela")]
    Komercialna = 16,
    [Display(Name = "Zagotavljanje kakovosti")]
    Kakovost = 32,
    [Display(Name = "Komunikacija")]
    Komunikacija = 64,
    [Display(Name = "Varovanje zdravja in okolja")]
    Varovanje = 128,
};


[Flags]
public enum ActivityTypes
{
    [Display(Name = "Tabor")]
    Tabor = 1,
    [Display(Name = "Delavnica")]
    Delavnica = 2,
    [Display(Name = "Zaporedna (kontinuirana srečanja skupine)")]
    Srecanje = 4,
    [Display(Name = "Posvet")]
    Posvet = 8,
    [Display(Name = "Kampanja")]
    Kampanija = 16,
    [Display(Name = "Ulično delo")]
    Ulicno = 32,
    [Display(Name = "(Mednarodna) mladinska izmenjava")]
    Izmenjava = 64,
    [Display(Name = "Mladinsko informiranje")]
    Informiranje = 128,
    [Display(Name = "Usposabljanje")]
    Usposabljanje = 256,
    [Display(Name = "Vodenje organizacije")]
    Vodenje = 512,
    [Display(Name = "Mladinski kulturni ali športni dogodek")]
    Sport = 1024,
    [Display(Name = "Večdnevni mladinski kulturni ali športni dogodek")]
    VecdnevniDogodek = 2048,
    [Display(Name = "Mentorstvo/tutorstvo/svetovanje")]
    Svetovanje = 4096,
    [Display(Name = "Mladinska pobuda")]
    Pobuda = 8192,
    [Display(Name = "Izlet")]
    Izlet = 16384,
    [Display(Name = "Animacija skupine")]
    Animacija = 32768,
    [Display(Name = "Organiziranje prostovoljnega dela")]
    Prostovoljstvo = 65536,
    [Display(Name = "Zagovorništvo")]
    Zagovorništvo = 131072,
    [Display(Name = "Dolgotrajsno partnersko sodelovanje")]
    Partnerstvo = 262144,
    [Display(Name = "Digitalno mladisnko delo")]
    Digitalno = 524288,
};


[Flags]
public enum YouthSectors
{
    [Display(Name = "Avtonomija mladih")]
    Avtonimija = 1,
    [Display(Name = "Neformalno učenje in usposabljanje ter večanje kompetenc mladih")]
    Usposabljanje = 2,
    [Display(Name = "Dostop mladih do trga delovne sile in razvoj podjetnosti mladih")]
    Podjetnost = 4,
    [Display(Name = "Skrb za mlade z manj priložnostmi v družbi")]
    Skrb = 8,
    [Display(Name = "Prostovoljstvo, solidarnost in medgeneracijsko sodelovanje mladih")]
    Prostovoljstvo = 16,
    [Display(Name = "Mobilnost mladih in mednarodno povezovanje")]
    Mobilnost = 32,
    [Display(Name = "Zdrav način življenja in preprečevanje različnih oblik odvisnosti mladih")]
    Odvisnost = 64,
    [Display(Name = "Dostop mladih do kulturnih dobrin in spodbujanje ustvarjalnosti ter inovativnosti mladih")]
    Inovativnost = 128,
    [Display(Name = "Sodelovanje mladih pri upravljanju javnih zadev v družbi")]
    Upravljanje = 256,
};

[Flags]
public enum Gender
{
    [Display(Name = "Moški")]
    Male = 32,
    [Display(Name = "Ženska")]
    Female = 64,
    [Display(Name ="Drugo")]
    Other = 128,
};

[Flags]
public enum MemberType
{
    StaffMember = 1,
    Participant = 2,
};

[Flags]
public enum GroupType
{   
    Project = 1,
    Activity = 2,
};