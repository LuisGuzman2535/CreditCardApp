# CreditCardApp
# Estado de Cuenta de Tarjeta de Crédito

## Descripción
Esta aplicación web permite visualizar el estado de cuenta de una tarjeta de crédito, incluyendo el detalle de movimientos, cálculo de cuota mínima, cálculo de contado e interés bonificable, y mostrar el saldo utilizado y disponible de la tarjeta de crédito.

## Tecnologías Utilizadas
- ASP.NET Web API
- ASP.NET MVC
- Razor
- jQuery
- SQL Server

## Funcionalidades
- Mostrar el nombre del titular de la tarjeta de crédito y el número de tarjeta.
- Mostrar el saldo actual de la tarjeta de crédito, el límite de crédito y el saldo disponible.
- Mostrar una lista de las compras realizadas con la tarjeta de crédito en este mes.
- Calcular y mostrar la cuota mínima a pagar.
- Calcular y mostrar el monto total a pagar.
- Calcular y mostrar el monto total de contado a pagar con intereses.
- Formularios para agregar nuevas compras y realizar pagos.
- Historial de transacciones del mes.

## Requisitos
- .NET 8.0
- SQL Server

## Configuración del Proyecto
1. Clonar el repositorio o descargar zip:
   ```bash
   git clone https://github.com/LuisGuzman2535/CreditCardApp.git

#Configuraciónes generales de la solución
## 1. Configurar la cadena de conexión a la base de datos en appsettings.json ubicado en el proyecto CreditCardAPI.
- En el Server colocar tu servidor de sql server.
- 
## 2. Configurar dirección base para el cliente HTTP.
- En el archivo promgram.cs, en builder.Services.AddHttpClient colocar la uri, actualmente es https://localhost:7067/, en caso de error probar con http://localhost:5008/

## 3. Ejecutar los scripts de la base de datos ubicados en la carpeta DatabaseScripts.
## 4. Prueba de Endpoints de la API (CreditCardAPI). La colección de Postman que permite probar  la API se encuentra en la carpeta Postman.
- Obtener Estado de Cuenta
   URL: /api/EstadoDeCuenta/GetEstadoCuenta/{id}
   Método: GET
   Descripción: Obtiene el estado de cuenta de una tarjeta de crédito por ID de tarjeta de credito.
- Obtener Historial de Transacciones
   URL: /api/EstadoDeCuenta/GetHistorialTransacciones/{id}
   Método: GET
   Descripción: Obtiene el historial de transacciones de una tarjeta de crédito por ID de tarjeta de credito.
- Obtener Tarjetas de Credito
   URL: /api/TarjetasDeCredito/TarjetaDeCredito/{id}
   Método: GET
   Descripción: Obtiene las tarjetas de credito por por ID de tarjeta de credito.
- Obtener Listado de Tarjetas de Credito
   URL: /api/TarjetasDeCredito/ListadoDeTarjetas/{id}
   Método: GET
   Descripción: Obtiene todas las tarjetas de credito.
- Obtener Agregar Transaccion
   URL: /api/Transaccion/AddTransaccion
   Método: POST
   Descripción: Agrega una transacción ya se de Compra o Pago.
  
## 5. Cómo Probar la Aplicación
- Ejecutar el proyecto CreditCardAPI en Visual Studio.
- Utilizar la colección de Postman incluida en el repositorio para probar los endpoints de la API.
- Ejecutar el proyecto abriendo nuevamente Visual Studio pero ahora ejecutando CreditCardMVC.
  
## Vistas de la aplicación 
- Pantalla de Listado de Tarjetas de Crédito muestra una tabla con todas las tarjetas de crédito asociadas al usuario.
- Pantalla de Estado de Cuenta permite al usuario visualizar el estado de cuenta detallado de una tarjeta de crédito específica. Tambien tiene un botón de Exportar a Excel: Permite al usuario exportar el historial de compras a un archivo Excel para un análisis más detallado o para mantener un registro.
- Pantalla de Registrar Compra permite al usuario agregar nuevas compras realizadas con la tarjeta de crédito.
- Pantalla de Registrar Pago permite al usuario ingresar pagos realizados hacia la tarjeta de crédito.
- Pantalla de Transacciones muestra una lista completa de todas las transacciones realizadas con la tarjeta de crédito, incluyendo tanto compras como pagos.
  
