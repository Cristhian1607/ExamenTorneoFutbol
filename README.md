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

SQL Para el Supabase
-- Tabla torneos
create table if not exists torneos (
  id         serial primary key,
  nombre     varchar(150) not null,
  edicion    integer not null,
  activo     boolean not null default true,
  creado_en  timestamptz not null default now()
);

-- Tabla equipos
create table if not exists equipos (
  id         serial primary key,
  torneo_id  integer not null references torneos(id),
  nombre     varchar(150) not null,
  ciudad     varchar(150)
);

-- Tabla partidos
create table if not exists partidos (
  id                  serial primary key,
  torneo_id           integer not null references torneos(id),
  equipo_local_id     integer not null references equipos(id),
  equipo_visitante_id integer not null references equipos(id),
  goles_local         integer,
  goles_visitante     integer,
  fecha_partido       timestamptz not null,
  jugado              boolean not null default false
);


-- RLS
alter table torneos enable row level security;
alter table equipos  enable row level security;
alter table partidos enable row level security;

-- Politica: permitir todo con la anon key 
create policy "public_all_torneos" on torneos for all using (true) with check (true);
create policy "public_all_equipos" on equipos for all using (true) with check (true);
create policy "public_all_partidos" on partidos for all using (true) with check (true);
