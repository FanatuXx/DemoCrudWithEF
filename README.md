# DemoCrudWithEF

Application de démonstration CRUD (Groupes / Albums) en ASP.NET Core MVC avec Entity Framework Core, illustrant aussi l'intégration de composants **Blazor Server** dans une application MVC classique.

## Structure de la solution

La solution est composée de 3 projets :

### `DemoCrudWithEF` (Web)

Le projet web ASP.NET Core MVC (`net10.0`, `Microsoft.NET.Sdk.Web`). C'est le point d'entrée de l'application (`Program.cs`).

- `Controllers/` — contrôleurs MVC (`GroupeController`, `HomeController`)
- `Models/` — view models / forms utilisés par les vues (ex. `AddAlbumForm`)
- `Views/` — vues Razor (`.cshtml`) organisées par contrôleur, ainsi que les composants Blazor (`.razor`), ex. `Views/Groupe/AddAlbumComponent.razor`
- `wwwroot/` — assets statiques (css, js, librairies client)

Ce projet référence `DemoCrudWithEF.Domain`.

### `DemoCrudWithEF.Domain` (Domaine / Accès aux données)

Bibliothèque de classes (`net10.0`) qui porte la logique métier et l'accès aux données via Entity Framework Core (`Microsoft.EntityFrameworkCore.SqlServer`).

- `Entities/` — entités EF Core (`Groupe`, `Album`)
- `Configurations/` — configuration du mapping EF Core
- `Migrations/` — migrations EF Core
- `Commands/` — objets représentant une intention d'écriture (`CreateAlbumCommand`, `AddGroupeCommand`, `UpdateGroupeCommand`)
- `Queries/` — objets représentant une intention de lecture
- `Repositories/` — interfaces d'accès aux données (`IGroupeRepository`, `IAlbumRepository`)
- `Services/` — implémentations des repositories (`GroupeService`, `AlbumService`)
- `Errors/` — types d'erreurs métier

Ce projet suit une approche **CQS (Command/Query Separation)** : les repositories exposent des méthodes `HandleAsync(Command, ...)` plutôt que des méthodes CRUD génériques.

Ce projet référence `Tools`.

### `Tools` (Utilitaires transverses)

Bibliothèque de classes (`net10.0`) contenant des types utilitaires partagés, notamment le pattern `CommandQuerySeparation` (dossier `CommandQuerySeparation/`) et des types `Results/` (résultats d'opération, gestion d'erreurs sans exceptions).

Ne référence aucun autre projet — c'est la brique la plus bas niveau de la solution.

### Dépendances entre projets

```
DemoCrudWithEF (Web, MVC + Blazor Server)
    └── DemoCrudWithEF.Domain (EF Core, logique métier)
            └── Tools (utilitaires transverses)
```

## Prérequis

- .NET SDK 10
- SQL Server LocalDB (la chaîne de connexion dans `Program.cs` pointe vers `(localdb)\MSSQLLocalDB`, base `MusicDb`)

## Intégrer un composant Blazor dans l'application MVC

Le projet web est une application **MVC classique**, pas une application Blazor. Pour pouvoir y afficher des composants Blazor interactifs (comme `AddAlbumComponent.razor`), l'app est configurée en mode **Blazor Server** au sein de MVC. Voici les étapes suivies pour que ça fonctionne, à reproduire pour tout nouveau composant :

### 1. Enregistrer les services Blazor Server

Dans `Program.cs` :

```csharp
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();
```

### 2. Mapper le hub Blazor

Toujours dans `Program.cs`, après `MapControllerRoute` :

```csharp
app.MapBlazorHub();
```

C'est ce endpoint qui gère la connexion SignalR (le "circuit") entre le navigateur et le serveur, indispensable pour que les événements (`@onclick`, `OnValidSubmit`, etc.) fonctionnent.

### 3. Ajouter le script client Blazor dans le layout

Dans `Views/Shared/_Layout.cshtml`, ajouter le `HeadOutlet` dans le `<head>` :

```html
<component type="typeof(Microsoft.AspNetCore.Components.Web.HeadOutlet)"
           render-mode="ServerPrerendered" />
```

et charger `blazor.server.js` **une seule fois**, juste avant la fermeture de `</body>` (après les autres scripts) :

```html
<script src="~/_framework/blazor.server.js"></script>
```

> ⚠️ Piège rencontré : ce script ne doit être inclus **qu'une seule fois**, et **jamais dans le `<head>`**. S'il est chargé avant que le contenu du `<body>` existe (ou dupliqué), le circuit Blazor démarre sur une page incomplète (ou tente de démarrer deux fois), et les événements des composants (boutons, formulaires) ne se déclenchent plus, sans erreur visible côté serveur.

### 4. Créer le composant `.razor`

Placer le fichier `.razor` dans `Views/<Controller>/` (ex. `Views/Groupe/AddAlbumComponent.razor`). Il peut recevoir des paramètres via `[Parameter]` et injecter des services (repositories, etc.) via `@inject`.

### 5. Insérer le composant dans une vue `.cshtml`

Dans la vue Razor MVC hôte (ex. `Views/Groupe/Details.cshtml`), utiliser le tag helper `<component>` :

```html
<component type="typeof(DemoCrudWithEF.Views.Groupe.AddAlbumComponent)"
           render-mode="Server" param-Form="@Model.AddAlbumForm" />
```

- `type` : le type C# généré pour le composant.
- `render-mode="Server"` (ou `ServerPrerendered`) : indique que le composant doit être rendu et rester interactif via le circuit Blazor Server.
- `param-<NomDuParametre>` : permet de passer une valeur à chaque `[Parameter]` public du composant.

### Résumé des points de vigilance

- `AddServerSideBlazor()` + `AddSignalR()` doivent être enregistrés.
- `app.MapBlazorHub()` doit être mappé.
- `blazor.server.js` : une seule inclusion, en bas de `<body>`.
- Le `HeadOutlet` est nécessaire pour que les composants puissent modifier le `<head>` (titre, meta, etc.).