# Mi idea

> Completa este archivo con ayuda de tu asistente de código. Él lo lee antes de cada tarea (ver AGENTS.md).

## ¿Qué es?
Una biblioteca escolar: catálogo de libros, usuarios con rol y control de préstamos.

## ¿Para quién?
Para la biblioteca de mi escuela. El admin registra los libros y los usuarios; los profesores y el admin controlan quién lleva qué libro; los alumnos ven el catálogo y sus préstamos.

## Elementos que maneja
- Libros: id, título, autor, ISBN, categoría y stock.
- Usuarios: id, nombre, email, contraseña y rol (alumno, profesor, admin).
- Préstamos: id, libro, usuario, fecha de préstamo, fecha de devolución esperada y fecha de devolución real.

## Dato extra
- Disponibilidad: un libro está disponible cuando `stock - préstamos activos > 0`.
- Un préstamo está **retrasado** si ya pasó la fecha de devolución esperada y aún no se devuelve.

## Marca
- Nombre: Mi biblioteca
- Eslogan: Lee, devuelve, repite
- Color principal (hex): #7C3AED

## Asistente de mi producto (bonus)
Sugerir qué libro leer según los gustos del usuario, usando el catálogo real.
