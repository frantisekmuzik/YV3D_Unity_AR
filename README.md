# Rozšířená realita v Unity

## [Prezentace](https://docs.google.com/presentation/d/1yr78qxF_FNwyZyHgeEm2Z07C7zaaVTGNtII7SNzERgI/edit?usp=sharing)

# Vytvoření hlavního menu aplikace a základní ovládání Unity

## 1. Import projektu

- import projektu "**YV3D_test**" a představení Unity UI

## 2. Vytvoření UI tlačítek

- v hlavním menu se vytvoří 2 tlačítka - jedno pro Markerless AR, druhé pro Location-based AR

![](/img/img01.png)

- **základní metody pro Unity C#**
    - **Awake()**
        - popis: Volá se jako první při inicializaci skriptu (před funkcí Start). Používá se pro inicializaci, která nezávisí na jiných skriptech.
        - spuštění: Ihned po přidání skriptu do scény nebo při aktivaci objektu.
    - **Start()**
        - popis: Volá se jednou na začátku životního cyklu objektu, těsně před prvním voláním funkce Update. Vhodné pro inicializaci závislou na ostatních objektech.
        - spuštění: Po dokončení všech Awake() metod ve scéně.
    - **Update()**
        - popis: Volá se každým snímkem (frame). Používá se pro logiku, která se má provádět průběžně, např. kontrola vstupů od uživatele nebo pohyb objektů.
        - spuštění: V každém snímku během běhu aplikace.
    - **OnEnable()/OnDisable()**
        - popis: Volá se, když je objekt nebo skript povolen (enabled)/zakázán (disabled). 
        - spuštění: Při aktivaci/deaktivaci objektu nebo komponenty.

- výpis zprávy do konzole při stisknutí tlačítka
```c#
    // Markerless AR tlačítko
    public void OnMarkerlessARClicked()
    {
        Debug.Log("Tabletop AR button has been clicked");
    }
```
- změna scény tlačítkem

```c#
    // Markerless AR tlačítko
    public void OnMarkerlessARClicked()
    {
        Debug.Log("Tabletop AR button has been clicked");
        SceneManager.LoadScene("SimpleSample");
    }
```

- přiřazení skriptu pro ovládání *Menu* do nového game objectu *MenuComponent*

- zpětné tlačítko  *SimpleSample* -> *Menu*

- **Poznámka:** pro fungování přepínání scén v telefonu je nutné dané scény vložit do aktivních scén v *Build Settings* a po zapnutí aplikace se zapne první scéna v pořadí
 
![](/img/img02.png)

# Markerless AR


# Location-based AR