# StudienimportService

Ein einfacher (Windows-)Dienst zum Import von Studien aus dem Studienserver des UKW

## (De)Installation als Dienst

Ausführen des folgenden Befehls installiert den Dienst als Windows-Dienst:

```
StudienimportService.exe /Install
```

Der folgende Befehl stoppt den Dienst und entfernt den zugehörigen Dienst

```
StudienimportService.exe /Uninstall
```

## Konfiguration

Die Datei `appsettings.Example.json` enthält ein Beispiel zur Konfiguration des Dienstes und muss zur Verwendung in `appsettings.json` umbenannt werden.

* `TaskDelay`: Zeitraum zwischen der Ausführung von Aktualisierungen in Tagen. Wenn nicht angegeben, dann 7 Tage.
* `RequestUrl`: Url des Studienservers. **Benötigt**
* `PostUrl`: Url von Onkostar mit `/remote/importStudien` und den Parametern `autoUser` und `autoPass`. **Benötigt**