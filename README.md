# GestionPrestamoLibro

GestionPrestamoLibro es un sistema de gestión de préstamos hecho con Blazor Web App (.NET 10) y Entity Framework Core con SQL Server.

## Instalación

1. Abrir la solución `GestionPrestamoLibro.sln` en Visual Studio.
2. Restaurar los paquetes:
    ```sh
    dotnet restore
    ```
3. Revisar la cadena de conexión `SqlConStr` en `GestionPrestamoLibro.Blazor/appsettings.json`.
4. Crear la base de datos desde la Consola del Administrador de Paquetes:
    ```sh
    Update-Database
    ```

## Uso

```sh
dotnet build
dotnet run --project GestionPrestamoLibro.Blazor
```

## Estructura del proyecto

- **Components/**: componentes Razor de la aplicación.
- **Context/**: contexto de la base de datos.
- **Migrations/**: migraciones de Entity Framework.
- **Models/**: modelos de datos.
- **Services/**: clases de servicios.
- **wwwroot/**: archivos estáticos.

## Paquetes NuGet

- Microsoft.EntityFrameworkCore.SqlServer 10.0.0
- Microsoft.EntityFrameworkCore.Tools 10.0.0
- Microsoft.AspNetCore.Components.QuickGrid 10.0.0
- Blazor.Bootstrap 3.2.0
- Aplicada1.Core 1.0.0
