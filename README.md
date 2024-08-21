# ApiRest_Sitt

# Task Manager API

## Descripción

Este es el backend de la aplicación de gestión de tareas (To-Do List) que proporciona una API RESTful construida con .NET Core. La API permite a los usuarios autenticarse y gestionar sus tareas.

## Requisitos Previos

- .NET 6.0 SDK o superior
- SQL Server (o cualquier otro servidor de base de datos compatible con Entity Framework Core)

## Configuración

### Clonar el Repositorio

git clone https://github.com/tuusuario/task-manager-backend.git
cd task-manager-backend"

### Configurar la Base de Datos
1. Modificar la Cadena de Conexión:
Abre el archivo appsettings.json y ajusta la cadena de conexión para que apunte a tu servidor de base de datos:

"ConnectionStrings": {
  "DefaultConnection": "Server=tu_servidor;Database=TaskManagerDb;User Id=tu_usuario;Password=tu_contraseña;"
}

2. Aplicar las Migraciones:
Ejecuta los siguientes comandos para crear la base de datos y aplicar las migraciones:
dotnet ef database update TaskManagerDb

3. Ejecutar la API
Para ejecutar la API, usa el siguiente comando:
dotnet run

La API estará disponible en https://localhost:7070

##Endpoints
Login: POST /api/Login/login

Registrar usuario: POST /api/Login/register

Obtener tareas: GET /api/Tasks

Crear tarea: POST /api/Tasks

Marcar tarea como completada: PUT /api/Tasks/{id}

Eliminar tarea: DELETE /api/Tasks/{id}
