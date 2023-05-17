# StudienimportService

Ein einfacher (Windows-)Dienst zum Import von Studien aus dem Studienserver des UKW

## (De)Installation als Dienst

Ausführen des folgenden Befehls installiert den Dienst als Windows-Dienst:

```
StudienimportService.exe /Install
```
Dies installiert den Dienst als "delayed-auto". Er wird daher automatisch nach Server-Neustart nach anderen Autostart-Diensten gestartet.


Der folgende Befehl stoppt den Dienst und entfernt den zugehörigen Dienst

```
StudienimportService.exe /Uninstall
```

## Konfiguration

Die Datei `appsettings.Example.json` enthält ein Beispiel zur Konfiguration des Dienstes und muss zur Verwendung in `appsettings.json` umbenannt werden.

* `TaskDelay`: Zeitraum zwischen der Ausführung von Aktualisierungen in Tagen. Wenn nicht angegeben, dann 7 Tage.
* `RequestUrl`: Url des Studienservers. **Benötigt**
* `PostUrl`: Url von Onkostar mit `/remote/importStudien` und den Parametern `autoUser` und `autoPass`. **Benötigt**

## Logging

Fehler werden im LogLevel "Critical" geloggt, damit die entsprechende Information auf jeden Fall bemerkt wird.