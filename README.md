# Test 3 - Microservicio Infraestructure: Frontend / Backend

----
![A01](./imagenes_proyecto/A01.png)
----

## Tecnologías Utilizadas

* **Frontend:** Angular 21+ (Node 22)
* **Backend:** .NET 9 Framework
* **Base de Datos:** SQL Server (Sin gestor de base de datos) / Instalar localmente Managment Studio, no soporte para docker
* **Gestión de BD:** SQL Server Managmen Studio (local)
* **Orquestación:** Docker Compose
---

Proyectos similares: https://github.com/ANDERSOUNDZ/DOCKER_TEST1/ ( Angular / Spring boot JAVA)

## Arquitectura de Contenedores
---
![A02](imagenes_proyecto/A02.png)
---

Applicacion de administracion
Frontend - Tienda Estilo Bento con Angular material / Angular Schematics

Referencias y Recursos:
1. https://material.angular.dev/components/categories

2.  https://www.codemotion.com/magazine/frontend/lets-create-a-bento-box-design-layout-using-modern-css/

---
![A03](./imagenes_proyecto/A03.png)
Backend / Hexagonal Architecture / Microservicios
![A04](./imagenes_proyecto/A04.png)
---
## Instrucciones de Ejecución

Ingresar a CMD / Powershell / terminal linux / bash :

1. Clona el proyecto
Ejecuta esta linea: git clone https://github.com/ANDERSOUNDZ/test_3_cli

2. Entra a la carpeta
cd test_3_cli

3. Levanta los servicios
---
Ejecuta el comando completo para construir las imágenes y levantar los contenedores en segundo plano:
---
Ejecutar esta linea para el orquestador: docker-compose up --build

( Se construyen y despliegan las bases de datos, los servicios backend y el frontend ya automatizados, adjuntado dos archivos bash para desplegar migraciónes y levantar servicios )
---

Servicios desplegados

Una vez que Docker termine el proceso, podrás acceder a los siguientes servicios:

| Servicio      | URL / Acceso                                      | Puerto | Descripción                       |
|---------------|---------------------------------------------------|--------|-----------------------------------|
| Frontend      | http://localhost:4200/                            | 4200   | Interfaz de Usuario (Angular)     |
| Backends Swagger| http://localhost:8081/swagger/index.html , http://localhost:8082/swagger/index.html       | 8081 / 8082   | API                               |
| SQL Server       | StandAlone SQL Manager                            | ----   | Panel de Gestión de Base de datos visual            |
| Databases      | localhost,1434:1433 / 1433:1433                                     | 1434 / 1433   | Motor SQL Server                  |

----
Configuración de SQL Management Studio ( Visualizar Base de datos )

Una vez que observes en la consola que los servicios están "Healthy":
Abre SQL Server Management Studio (SSMS).

registra las bases de datos: tal cual como esta tipado:

Conexión 1 (Productos):

Server: localhost,1433
Auth: SQL Server Authentication
User: sa / Password: Products123*
activar check SSL

Conexión 2 (Transacciones):

Server: localhost,1434
Auth: SQL Server Authentication
User: sa / Password: Transactions123*
activar check SSL

![A03](./imagenes_proyecto/A05.png) | ![A03](./imagenes_proyecto/A06.png)

----

Applicacion de administración
Frontend - Backend
---
Manejo de rama - Una sola anexado en el documento
---
![1A](./imagenes_proyecto/A07.png)


Funcionalidades Frontend:
---
Gestión de Productos.

Panel de administración que permite: Crear productos, asignar precios, descripcion y cantidad de stock, editar los productos y sus parametros, eliminar productos y ver el producto.
Al registrar producto, se registra una transaccion de compra inicial, de igual manera al editar el producto permite agregar producto, este toma siempre la diferencia del producto ingresa y lo registra para mantener stock.

![1A](./imagenes_proyecto/A08.png)
![1A](./imagenes_proyecto/A09.png)
![1A](./imagenes_proyecto/A10.png)
![1A](./imagenes_proyecto/A11.png)

---

Pequeño modulo de categorias (Relacional pequeña):
Registra, Edita, Elimina categorias para los productos.

![1A](./imagenes_proyecto/A12.png)
![1A](./imagenes_proyecto/A14.png)
![1A](./imagenes_proyecto/A15.png)

---

Historial de transacciónes.

Permite visualizar los registros de stock de productos, esta tabla muestra los registros de compra y venta de los productos, por cada producto que se vende o compra registra el porducto , fecha y cantidad, actualiza tanto cuando compra como vende el stock del producto.

![1A](./imagenes_proyecto/A16.png)

Se agrego un modal que permite visualizar toda la información de la transaccion, ya sea compra o venta, mas una descarga ne pdf de esa infromación: 

![1A](./imagenes_proyecto/A17.png)

Añadi una pequeña Dummy Store para poder hacerla interactiva, se puede comprar 1 producto o añadir otro producto.

![1A](./imagenes_proyecto/A18.png)

Al comprar, este ejecuta la accion de registro y guarda en la tabla de transacciones mientras actualiza el stock con una clase de fabrica en la entidad. 

![1A](./imagenes_proyecto/A19.png)
![1A](./imagenes_proyecto/A21.png)


En el caso de que no exista producto envia una notificacio que ya no hay cupo o no existe stock no permite la compra y se muestra una notificacion en la pantalla.

![1A](./imagenes_proyecto/A20.png)
![1A](./imagenes_proyecto/A23.png)
![1A](./imagenes_proyecto/A24.png)
![1A](./imagenes_proyecto/A25.png)


Funcionalidades Backend
- Arquitectura hexagonal, Puertos / Adaptadores, Fluent Validation, SOLID, Partial Clases, Injection dependency.

![1A](./imagenes_proyecto/A26.png) | ![1A](./imagenes_proyecto/A27.png)

- Registra transacciónes asyncronas mediante coneccion API REST /  HTTP Client

![1A](./imagenes_proyecto/A31.png)

Registra transacciónes y comparten proceso y almacenan: 

1. Tabla Categoria
![1A](./imagenes_proyecto/A30.png)
2. Tabla Productos
![1A](./imagenes_proyecto/A29.png)
3. Tabla Transacciónes
![1A](./imagenes_proyecto/A28.png)

Orquestador Docker Estable
![1A](./imagenes_proyecto/A32.png)

Servicios levantados: 
![1A](./imagenes_proyecto/A33.png)

Es todo. :D