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