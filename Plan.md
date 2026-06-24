# Plan de Trabajo — Sistema de Control de Asistencia e Inventario
**Equipo:** 5 Integrantes (3 Backend + 2 Frontend)

---

## ⚙️ Backend (Blazor Server + EF Core + PostgreSQL)

### 🧑‍💻 Integrante 1: Servicios Core y Catálogo
| # | Tarea | Archivos a crear/modificar |
| :--- | :--- | :--- |
| 1 | Configurar `DbContext` y Modelos/Entidades base | `Models/`, `Data/` |
| 2 | Crear `Services/ICategoriaService.cs` + `CategoriaService.cs` (CRUD completo) | `Services/` |
| 3 | Crear `Services/IProveedorService.cs` + `ProveedorService.cs` (CRUD completo) | `Services/` |
| 4 | Registrar servicios en `Program.cs` (AddScoped + Inyección) | `Program.cs` |

### 🧑‍💻 Integrante 2: Servicios de Inventario y Lógica de Negocio
| # | Tarea | Archivos a crear/modificar |
| :--- | :--- | :--- |
| 1 | Crear `Services/IProductoService.cs` + `ProductoService.cs` (CRUD + búsqueda) | `Services/` |
| 2 | Crear `Services/IMovimientoInventarioService.cs` + `MovimientoInventarioService.cs` | `Services/` |
| 3 | Implementar validaciones de negocio (stock no negativo, precios, etc.) | `Services/` |
| 4 | Configurar y aplicar FluentValidation (o validación manual) | `.csproj`, `Validators/` |

### 🧑‍💻 Integrante 3: Infraestructura, DevOps y Soporte
| # | Tarea | Ruta / Archivo |
| :--- | :--- | :--- |
| 1 | Manejo global de errores y logging centralizado | `Program.cs`, `Middleware/` |
| 2 | Agregar Health Checks (`AddHealthChecks`) + endpoint `/health` | `Program.cs` |
| 3 | Crear `Dockerfile` + `.dockerignore` + `docker-compose.yml` | Raíz del proyecto |
| 4 | Ejecución de migraciones de BD y scripts iniciales (Seeders) | `Data/Migrations/` |

---

## 🎨 Frontend (Componentes Blazor y UI)

### 🧑‍💻 Integrante 4: Vistas de Catálogos y Layout Base
| # | Tarea | Ruta |
| :--- | :--- | :--- |
| 1 | Configurar Layout base, menús y navegación (`NavMenu.razor`) | `Components/Layout/` |
| 2 | Crear páginas de Categorías (Lista, Crear, Editar, Eliminar) | `/categorias` |
| 3 | Crear páginas de Proveedores (Lista, Crear, Editar, Eliminar) | `/proveedores` |
| 4 | Implementar componentes reutilizables (Diálogos, notificaciones) | `Components/Shared/` |

### 🧑‍💻 Integrante 5: Vistas de Inventario y Dashboard
| # | Tarea | Ruta |
| :--- | :--- | :--- |
| 1 | Crear páginas de Productos (Lista, CRUD, Filtros) | `/productos` |
| 2 | Formularios de Producto y Movimientos con dropdowns | `Components/Pages/` |
| 3 | Crear páginas de Movimientos (Registro, visualización stock) | `/movimientos` |
| 4 | Crear Dashboard (KPIs, stock bajo, últimos movimientos) | `/dashboard` |

---

## 👥 Tareas Compartidas / Colaboración

| Tarea | Responsables |
| :--- | :--- |
| Review cruzado de PRs | Todos |
| Integración y pruebas (Servicios ↔ Componentes) | Todos |
| Sincronización de nombres (Métodos, Modelos, DTOs) | Back + Front |
| Pruebas manuales de flujo completo | Todos |
| Definir y respetar estándares de código y convenciones | Todos |
