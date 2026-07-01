# TorneosFutbolMVC
# Sistema Torneos de Futbol

Aplicación web desarrollada en **.NET Core MVC** para la administración de torneos, equipos y partidos, utilizando **Supabase (PostgreSQL)** como backend mediante API REST.

## Tecnologías utilizadas
- **Framework:** .NET Core MVC
- **Backend/Base de Datos:** Supabase (PostgreSQL)
- **SDK:** supabase-csharp (v1.1.1)
- **Frontend:** Bootstrap (diseño responsivo)

##  Estructura del Sistema
El proyecto sigue una arquitectura MVC con una capa de servicios independiente:
- **Models:** Modelos de datos (`Torneo`, `Equipo`, `Partido`).
- **Services:** Capa lógica que consume la API de Supabase.
- **Controllers:** Orquestación de peticiones y reglas de negocio.
- **ViewModels:** Contenedores de datos para las vistas complejas.

##  Configuración
1. Clona el repositorio.
2. Abre `appsettings.json` y configura las credenciales:
   ```json
   {
     "Supabase": {
       "Url": "https://padlpbmydbqlmetecfrp.supabase.co",
       "Key": "sb_publishable_WY6d_lTSDzCj_6cj0NwuwA_FlqGWEc7"
     }
   }

