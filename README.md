# Prueba Desarrollador .NET Alexis Jair Del Castillo Castillo

Desarrollo de una aplicación web de tipo CRUD utilizando tecnologías de .NET.

## Requisitos

- Visual Studio 2022
- Postman

## Instrucciones

1. Abre el proyecto en Visual Studio 2022.
2. Antes de ejecutarlo, asegúrate de que el proyecto tenga las bibliotecas necesarias. El proyecto está desarrollado en capas, así que verifica que los siguientes paquetes estén instalados en el administrador del NuGet de cada capa:
    - **Capa BL:** Microsoft.AspNetCore.Hosting.Abstractions
    - **Capa PL:** Microsoft.AspNet.WebApi.Client
    - **Capa SL:** 
        - AspNetCore.Authentication.Basic
        - Microsoft.AspNetCore.Hosting.Abstractions
        - Newtonsoft.Json
3. Además, especifica la ruta del archivo JSON al inicio de las capas BL y SL en la variable `filePath`. Por ejemplo:
    filePath = "C:\\Users\\usuario\\Proyecto\\book.json"
   (El archivo de prueba se encuentra dentro de la carpeta "JSON")
5. Una vez completados estos pasos, puedes ejecutar el proyecto.
6. Al ejecutar la aplicación, se abrirán dos ventanas en el navegador: una con el CRUD a través de la interfaz gráfica y la otra con los servicios de la API. Puedes probar el CRUD con la interfaz gráfica si lo deseas.
7. Abre Postman e importa la colección adjunta con el proyecto que se encuentra en la carpeta "POSTMAN". La colección ya tiene las credenciales configuradas, pero si no las contiene, abre la colección llamada "Books" y en la pestaña "Authorization", especifica el tipo "Basic Auth" e ingresa las siguientes credenciales:
    - **Username:** admin
    - **Password:** pass123
8. Con las credenciales configuradas, puedes probar cada uno de los métodos guardados en la colección.

## Notas

- Esta es la estructura del JSON para agregar o actualizar:
  {
    "IdBook": "16",
    "BookName": "The Great Gatsby",
    "Author": "F. Scott Fitzgerald",
    "ReleaseDate": "1925-04-10"
  }
