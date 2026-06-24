# Esquema de Base de Datos y Relaciones

Basado en el diagrama de la imagen proporcionada ("Mer Lenguaje 2.png"), a continuación se detalla el esquema de las tablas y sus relaciones.

## 1. Esquema de Tablas

### Tabla: `categorias`
| Campo | Tipo | Restricciones / Claves |
| :--- | :--- | :--- |
| `id` | SERIAL | **PK** (Primary Key) |
| `nombre` | VARCHAR(100) | Unique, **NN** (Not Null) |
| `descripcion` | TEXT | |

### Tabla: `proveedores`
| Campo | Tipo | Restricciones / Claves |
| :--- | :--- | :--- |
| `id` | SERIAL | **PK** (Primary Key) |
| `nombre` | VARCHAR(150) | **NN** (Not Null) |
| `telefono` | VARCHAR(20) | |
| `email` | VARCHAR(100) | |

### Tabla: `productos`
| Campo | Tipo | Restricciones / Claves |
| :--- | :--- | :--- |
| `id` | SERIAL | **PK** (Primary Key) |
| `codigo_sku` | VARCHAR(50) | Unique, **NN** (Not Null) |
| `nombre` | VARCHAR(150) | **NN** (Not Null) |
| `descripcion` | TEXT | |
| `precio_compra` | DECIMAL(10,2) | **NN** (Not Null) |
| `precio_venta` | DECIMAL(10,2) | **NN** (Not Null) |
| `stock_actual` | INT | **NN** (Not Null) |
| `stock_minimo` | INT | **NN** (Not Null) |
| `categoria_id` | INT | **FK** (Foreign Key), **NN** (Not Null) |
| `proveedor_id` | INT | **FK** (Foreign Key) |

### Tabla: `movimientos_inventario`
| Campo | Tipo | Restricciones / Claves |
| :--- | :--- | :--- |
| `id` | SERIAL | **PK** (Primary Key) |
| `producto_id` | INT | **FK** (Foreign Key), **NN** (Not Null) |
| `tipo` | VARCHAR(20) | **NN** (Not Null) |
| `cantidad` | INT | **NN** (Not Null) |
| `fecha` | TIMESTAMP | **NN** (Not Null) |
| `motivo` | VARCHAR(255) | |

---

## 2. Relaciones

El diagrama presenta las siguientes relaciones (todas de **Uno a Muchos - 1:N**):

*   **Categorías a Productos (1:N):**
    *   Una categoría puede estar asociada a múltiples productos.
    *   Un producto pertenece obligatoriamente a una sola categoría (`categoria_id` es NN).
    *   **Enlace:** `categorias.id`  `productos.categoria_id`

*   **Proveedores a Productos (1:N):**
    *   Un proveedor puede suministrar múltiples productos.
    *   Un producto puede estar asociado a un proveedor de forma opcional (ya que `proveedor_id` no es NN, permitiendo valores nulos).
    *   **Enlace:** `proveedores.id`  `productos.proveedor_id`

*   **Productos a Movimientos de Inventario (1:N):**
    *   Un producto puede registrar múltiples movimientos de inventario (entradas, salidas, etc.).
    *   Un movimiento de inventario está vinculado obligatoriamente a un único producto (`producto_id` es NN).
    *   **Enlace:** `productos.id`  `movimientos_inventario.producto_id`
