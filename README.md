# ChallengeN5 Backend

Este proyecto es una API de .NET Core para administrar permisos con persistencia en SQL Server, Elasticsearch y seguimiento de operaciones a través de Kafka.

## Índice

- [Configuración Inicial](#configuración-inicial)
  - [Base de Datos](#base-de-datos)
  - [Configuración de la Base de Datos](#configuración-de-la-base-de-datos)
- [Docker Compose](#docker-compose)
- [Ejecución](#ejecución)
- [APIs](#apis)
- [Contribuciones](#contribuciones)

## Configuración Inicial

### Base de Datos

Antes de iniciar, debes crear las tablas y la base de datos ejecutando los scripts ubicados en la carpeta `db`. Estos scripts deben ser ejecutados en el orden apropiado para garantizar la creación correcta de la base de datos y sus relaciones.

### Configuración de la Base de Datos

Antes de iniciar el proyecto, es vital configurar tus propias credenciales de base de datos:

1. Abre el archivo `appsettings.json` que se encuentra en la raíz del proyecto.
2. Localiza la sección `ConnectionStrings`.
3. Modifica las propiedades `User Id` y `Password` con tus credenciales:

``json
"ConnectionStrings": {
    "DefaultConnection": "Server=(local);Database=ManagePermissions;User Id=[TU_USUARIO];Password=[TU_CONTRASEÑA];TrustServerCertificate=true"
}

Asegúrate de reemplazar `[TU_USUARIO]` con tu nombre de usuario y `[TU_CONTRASEÑA]` con tu contraseña.

## Docker Compose

Para ejecutar el entorno con Docker, asegúrate de tener instalado Docker y Docker Compose en tu máquina. Luego, en la raíz del proyecto, ejecuta:

``bash
docker-compose up

Esto iniciará los servicios de Kafka, Zookeeper y Elasticsearch. Asegúrate de tener los puertos apropiados disponibles en tu máquina.

## Ejecución

Con todo en su lugar, ahora puedes ejecutar el backend. Navega a la raíz del proyecto y ejecuta:

``bash
dotnet run

## APIs

El proyecto provee las siguientes APIs principales:
- Request Permission
- Modify Permission
- Get Permissions

Para más detalles, consulta la documentación Swagger una vez que el servidor esté en ejecución.

