# Dokumentacja Wdrożenia: Ecommerce API

Niniejsza dokumentacja opisuje architekturę chmurową oraz proces automatyzacji **CI/CD** wykorzystany do hostowania i wdrażania aplikacji Web API.

---

## Architektura i Wykorzystane Usługi Azure

Do uruchomienia aplikacji w chmurze wykorzystano następujące komponenty:

* **Azure App Service (Linux):** Platforma hostująca aplikację.
* **Azure Database for PostgreSQL (Flexible Server):** Zarządzalna baza danych relacyjnych do przechowywania danych.
* **GitHub Actions:** Narzędzie do automatyzacji procesów budowania i wdrażania.

---

## Proces CI/CD (GitHub Actions)

Workflow automatycznie buduje i wdraża aplikację po każdym wypchnięciu zmian do gałęzi głównej.

### 1. Etap: Budowanie (Build Job)
* **Przygotowanie środowiska:** Konfiguracja SDK .NET 10.
* **Migration Bundling:** Generowanie samowykonującego się pliku binarnego `efbundle` zawierającego migracje Entity Framework Core.
* **Publishing:** Kompilacja i publikacja aplikacji.
* **Artifact Upload:** Przesłanie skompilowanych plików oraz migracji jako artefaktów gotowych do wdrożenia.

### 2. Etap: Wdrażanie (Deploy Job)
* **Database Migration:** Automatyczne uruchomienie pliku `efbundle`.
* **Azure Web App Deploy:** Przesłanie paczki aplikacji bezpośrednio do usługi Azure App Service.

---

## Dostęp do Aplikacji

Aplikacja jest dostępna publicznie pod poniższym adresem:

**Adres URL:** [https://sample-ecommerce-a9b9eth3edhqaugj.switzerlandnorth-01.azurewebsites.net/index.html](https://sample-ecommerce-a9b9eth3edhqaugj.switzerlandnorth-01.azurewebsites.net/index.html)

---

## Konfiguracja i Zmienne Środowiskowe

Aby pipeline działał poprawnie, w ustawieniach repozytorium (**Settings > Secrets and variables > Actions**) zostały skonfigurowane następujące wpisy:

| Klucz (Secret) | Opis |
| :--- | :--- |
| `AZURE_WEBAPP_PUBLISH_PROFILE` | Profil publikacji pobrany z portalu Azure ("Get publish profile"). |
| `DB_CONNECTION_STRING` | Pełny ciąg połączenia do bazy danych PostgreSQL. |

> [!WARNING]
**Ważna uwaga:** Aby środowisko GitHub Actions miało dostęp do bazy w celu wykonania migracji, w ustawieniach firewalla usługi Azure Database for PostgreSQL należy włączyć opcję "Allow public access from any Azure service within Azure to this server".
