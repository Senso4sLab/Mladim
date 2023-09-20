using System;
using System.Collections.Generic;
using System.ComponentModel;
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
public enum OrganizationNPMAims
{
    [Display(Name = "Participacija in zastopanost")]
    [Description("Spodbujanje participacije in zastopanosti mladih žensk in moških.")]
    Participacija = 1,
    [Display(Name = "Razvoj organizacij, področij sektorja in neorganizirane mladine")]
    [Description("Spodbujanje ustanavljanja in razvoja organizacij v mladinskem sektorju, razvoja ključnih področij mladinskega sektorja ter zagotavljanje delovanja neorganizirane mladine.")]
    Razvoj = 2,
    [Display(Name = "Mednarodno mladinsko delo in mobilnosti")]
    [Description("Spodbujanje vključevanja v mednarodno mladinsko delo in učnih mobilnosti v mladinskem delu ter njihova krepitev.")]
    Mobilnost = 4,
    [Display(Name = "Spodbujanje prostovoljstva med mladimi")]
    [Description("Spodbujanje prostovoljstva med mladimi.")]
    prostovoljstvo = 8,   
};





[Flags]
public enum ActivityTypes
{
    [Display(Name = "Tabor")]
    [Description("Skupina mladih več dni biva skupaj na istem mestu, vključuje več drugih aktivnosti.")]
    Tabor = 1,
    [Display(Name = "Delavnica")]
    [Description("1 - 4 ure, skupina udeležencev, ki se sreča v živo.Učni učinki.Krepi veščine, stališča, medosebne odnose...")]
    Delavnica = 2,
    [Display(Name = "Zaporedna (kontinuirana srečanja skupine)")]
    [Description("Kontinuirano srečevanje skupine skozi daljši čas. Primeri: srečanja mladinske skupine, animatorjev, skavtov/tabornikov, mladinski študijski krožek, karierni ali zaposlitveni klub.")]
    Srecanje = 4,
    [Display(Name = "Posvet")]
    [Description("Dogodek na določeno temo lahko večdnevni, ponavadi odprte narave in z udeleženci, ki se ne srečujejo kontinuirano. Primeri poimenovanja: konferenca, posvet, okrogla miza...")]
    Posvet = 8,
    [Display(Name = "Kampanja")]
    [Description("Serija aktivnosti, katerih cilj je spreminjanje vedenj in/ali stališč pri ciljni skupini (primeri ciljnih skupin: odločevalci, mladi, občinska uprava, učenci določene šole, člani mladinskih organizacij...)")]
    Kampanija = 16,
    [Display(Name = "Ulično delo")]
    [Description("Kontinuirano druženje z mladimi na ulici, občasno organiziranje aktivnosti, spremljanje mladih.")]
    Ulicno = 32,
    [Display(Name = "(Mednarodna) mladinska izmenjava")]
    [Description("Nadgradnja tabora. Dodatne dimenzije zahtevnosti: ne le skupina mladih, ki jo poznam, ampak tudi drugi.")]
    Izmenjava = 64,
    [Display(Name = "Mladinsko informiranje")]
    [Description("Izvajanje aktivnosti informiranja za tiste, ki pridejo ter delno tudi aktivno iskanje in naslavljanje drugih ciljnih skupin.")]
    Informiranje = 128,
    [Display(Name = "Usposabljanje")]
    [Description("Dogodek s ciljem načrtne krepitve kompetenc mladih ali mladinskih voditeljev/delavcev za njihovo aktivno participacijo. Pomembna je dolgotrajnost procesa, kontinuiteta.")]
    Usposabljanje = 256,
    [Display(Name = "Vodenje organizacije")]
    [Description("Vodenje lokalne enote ali organizacije / prevzem polne odgovornosti za delo v smeri razvoja enote ali organizacije (odgovornost na področju kadrov, financ, programa...")]
    Vodenje = 512,
    [Display(Name = "Mladinski kulturni ali športni dogodek")]
    [Description("Izvedba enkratnega dogodka ali daljšega kulturnega dogajanja, v katerega so v večinskem delu aktivno vključeni mladi (lahko v različnih vlogah).")]
    Sport = 1024,
    [Display(Name = "Večdnevni mladinski kulturni ali športni dogodek")]
    [Description("Npr. festival, turnir...")]
    VecdnevniDogodek = 2048,
    [Display(Name = "Mentorstvo/tutorstvo/svetovanje")]
    [Description("Delo s posameznikom ali s skupino skozi daljše časovno obdobje z elementi podpore pri doseganju nekega cilja (npr. izvedba mladinske pobude, vodenje dogodka, iskanje zaposlitve...) Primeri: mentor, tutor, coach, svetovalec, karierni svetovalec, mentor na področju glasbe, filma...")]
    Svetovanje = 4096,
    [Display(Name = "Mladinska pobuda")]
    [Description("Inovativen in kompleksen projekt, pri katerem so osrednji akterji mladi in je osredotočen na spremembo v družbi, ki ima učinke, širše kot zgolj na sodelujoče.")]
    Pobuda = 8192,
    [Display(Name = "Izlet")]
    [Description("Vsaj poldnevna načrtovana (ciljno usmerjena) aktivnost ki vključuje spremembo kraja. V naravo, v gore, na prireditev, v drugo mesto, študijski obisk...")]
    Izlet = 16384,
    [Display(Name = "Animacija skupine")]
    [Description("Aktivnost (vsaj 2 uri), katere namen je spoznavanje, prebijanje ledu, socialno vključevanje, povezovanje skupine...")]
    Animacija = 32768,
    [Display(Name = "Organiziranje prostovoljnega dela")]
    [Description("Organiziranje dlje časa trajajočega sodelovanja prostovoljcev in drugih deležnikov (ponavadi ne z mladinskega sektorja) s ciljem delati v dobro tretjih oseb.")]
    Prostovoljstvo = 65536,
    [Display(Name = "Zagovorništvo")]
    [Description("Kompleksnejši proces (koordinacija več aktivnosti, npr: kampanja, shod, peticija, posvetovanje, javna razprava...)")]
    Zagovorništvo = 131072,
    [Display(Name = "Dolgotrajsno partnersko sodelovanje")]
    [Description("Kot predstavnik organizacije trajno gojenje odnosov s partnersko organizacijo iz a) drugega sektorja ali b) drugega okolja, npr. druge države.")]
    Partnerstvo = 262144,
    [Display(Name = "Digitalno mladinsko delo")]
    [Description("Digitalno mladinsko delo pomeni proaktivno uporabo ali naslavljanje digitalnih medijev in tehnologij v mladinskem delu. Digitalno mladinsko delo ni metoda mladinskega dela, vendar je lahko vključeno v katerokoli okolje mladinskega dela (odprto mladinsko delo, mladinsko informiranje in svetovanje, mladinski klubi...).")]
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

[Flags]
public enum ApplicationClaim
{
    [Display(Name = "Delavec")]
    Worker = 1,
    [Display(Name = "Menedžer")]
    Manager = 2,    
}

[Flags]
public enum ApplicationRole
{
    [Display(Name = "Administrator")]
    Admin = 16,
   
}


