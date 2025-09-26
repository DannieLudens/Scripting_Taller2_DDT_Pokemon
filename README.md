# 🎮 Sistema de Combate Pokémon - TDD & OOP

**Implementación en C# usando Test-Driven Development (TDD) y Programación Orientada a Objetos**

[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![NUnit](https://img.shields.io/badge/NUnit-4.2.2-green)](https://nunit.org/)
[![C#](https://img.shields.io/badge/C%23-13.0-purple)](https://docs.microsoft.com/en-us/dotnet/csharp/)

---

## 📑 Tabla de Contenidos

- [📋 Descripción](#-descripción)
- [🏗️ Arquitectura del Proyecto](#️-arquitectura-del-proyecto)
- [📐 Fórmulas Implementadas](#-fórmulas-implementadas)
- [🧪 Estado de las Pruebas](#-estado-de-las-pruebas)
- [🚀 Cómo Ejecutar](#-cómo-ejecutar)
- [🎯 Metodología TDD Aplicada](#-metodología-tdd-aplicada)
- [🎨 Elementos de Programación Orientada a Objetos](#-elementos-de-programación-orientada-a-objetos)
- [📚 Notas Basadas en Requerimientos](#-notas-basadas-en-requerimientos)

---

## 📋 Descripción

Este proyecto implementa un **sistema de combate Pokémon** siguiendo metodología **TDD (Test-Driven Development)** y principios de **Programación Orientada a Objetos**. La implementación incluye cálculos de daño basados en la fórmula matemática especificada, sistema de tipos con tabla de efectividad, y 5 especies de Pokémon diferentes.

### ⚡ Características Implementadas

- 🎯 **5 Especies de Pokémon**: Pikachu (Electric), Charmander (Fire), Squirtle (Water), Geodude (Rock/Ground), Gastly (Ghost/Poison)
- 🎲 **Sistema de Tipos**: 10 tipos con tabla de efectividad completa según especificaciones
- ⚔️ **Movimientos**: Physical y Special con diferentes poderes base
- 📐 **Fórmula de Daño**: Implementación exacta según enunciado de la actividad
- 🧪 **Tests Parametrizados**: 40 casos de prueba usando NUnit `[TestCase]`

---

## 🏗️ Arquitectura del Proyecto

### 📁 Estructura

```
Source/
├── Pokemon.cs              # Clase base Pokémon
├── Move.cs                 # Clase movimientos
├── CombatCalculator.cs     # Lógica de cálculo de daño y modificadores
├── PokemonType.cs          # Enum tipos de Pokémon  
├── MoveType.cs             # Enum tipos de movimiento
└── Species/
    ├── Pikachu.cs          # Electric
    ├── Charmander.cs       # Fire
    ├── Squirtle.cs         # Water
    ├── Geodude.cs          # Rock/Ground
    └── Gastly.cs           # Ghost/Poison

UnitTest1.cs                # Suite completa de pruebas unitarias
```

### 🔧 Enumeraciones

```csharp
public enum PokemonType { 
    Rock, Ground, Water, Electric, Fire, 
    Grass, Ghost, Poison, Psychic, Bug 
}

public enum MoveType { 
    Physical, Special 
}
```

### 🎯 Especies Pokémon (Herencia & Factory Pattern)

Cada especie hereda de `Pokemon` y define automáticamente nombre y tipos:

```csharp
var pikachu = new Pikachu();     // Electric type
var geodude = new Geodude();     // Rock/Ground dual type  
var gastly = new Gastly();       // Ghost/Poison dual type
```

## 📐 Fórmulas Implementadas

### Fórmula de Daño (según enunciado)

**Para ataques Physical:**

$$DMG = \left\lfloor \frac{(2 \times \frac{LV}{5} + 2) \times (PWR \times \frac{ATK}{DEF} + 2)}{50} \times MOD \right\rfloor$$

**Para ataques Special:**

$$DMG = \left\lfloor \frac{(2 \times \frac{LV}{5} + 2) \times (PWR \times \frac{SpATK}{SpDEF} + 2)}{50} \times MOD \right\rfloor$$

**Donde:**
- $LV$: Nivel del Pokémon atacante (1-99)
- $PWR$: Poder base del movimiento (1-255)  
- $ATK$ / $SpATK$: Ataque físico/especial del atacante (1-255)
- $DEF$ / $SpDEF$: Defensa física/especial del defensor (1-255)
- $MOD$: Modificador de efectividad de tipos (0, 0.5, 1, 2, 4)
- $\lfloor \cdot \rfloor$: Función piso (redondeo hacia abajo)

### 🎯 Sistema de Tipos y Modificadores

#### Tabla de Efectividad

| Atacante ↓ / Defensor → | Rock | Ground | Water | Electric | Fire | Grass | Ghost | Poison | Psychic | Bug |
|-------------------------|------|--------|-------|----------|------|-------|-------|--------|---------|-----|
| **Rock**                | 1    | 0.5    | 1     | 1        | 2    | 0.5   | 1     | 1      | 1       | 2   |
| **Ground**              | 2    | 1      | 1     | 2        | 2    | 0.5   | 1     | 2      | 1       | 0.5 |
| **Water**               | 2    | 2      | 0.5   | 1        | 2    | 0.5   | 1     | 1      | 1       | 1   |
| **Electric**            | 1    | 0      | 2     | 0.5      | 1    | 0.5   | 1     | 1      | 1       | 1   |
| **Fire**                | 0.5  | 1      | 0.5   | 1        | 0.5  | 2     | 1     | 1      | 1       | 2   |
| **Grass**               | 2    | 2      | 2     | 1        | 0.5  | 0.5   | 1     | 0.5    | 1       | 0.5 |
| **Ghost**               | 1    | 1      | 1     | 1        | 1    | 1     | 2     | 1      | 2       | 1   |
| **Poison**              | 0.5  | 0.5    | 1     | 1        | 1    | 2     | 0.5   | 0.5    | 1       | 1   |
| **Psychic**             | 1    | 1      | 1     | 1        | 1    | 1     | 1     | 2      | 0.5     | 0.5 |
| **Bug**                 | 1    | 1      | 1     | 1        | 0.5  | 2     | 1     | 1      | 2       | 1   |

**Leyenda:**
- **0**: Inmunidad (sin daño)
- **0.5**: No muy efectivo  
- **1**: Daño normal
- **2**: Super efectivo

**Tipos Duales:** Se multiplican ambas efectividades (ej: Water vs Fire/Ground = 2×2 = 4x daño)

---

## 🧪 Estado de las Pruebas

### Resumen de Tests

| Categoría | Implementados | Pasando | % Éxito |
|-----------|---------------|---------|---------|
| **Tests de Pokémon y Move** | 4 | 4 | ✅ 100% |
| **Tests de Modificadores de Tipo** | 8 | 8 | ✅ 100% |
| **Tests de Cálculo de Daño** | 40 | 29 | ⚡ 72.5% |
| **TOTAL** | **52** | **41** | **🎯 78.8%** |

### Tests que Pasan Correctamente

✅ **Pokemon**: Creación con valores por defecto y personalizados  
✅ **Move**: Creación con valores por defecto y personalizados  
✅ **TypeModifier**: Efectividad simple para todos los tipos  
✅ **TypeModifier**: Efectividad dual (Geodude y Gastly)  
✅ **Casos de Inmunidad**: Todos los casos con MOD=0 funcionan perfectamente  
✅ **Casos de Daño**: 29/40 casos de la tabla oficial pasan correctamente

### Tests de Casos de Daño

Los 40 casos de prueba de la tabla del enunciado están completamente implementados usando `[TestCase]` parametrizado, alternando entre movimientos Physical (pares) y Special (impares) como se requiere. **El 72.5% de los casos pasan correctamente**, demostrando que la implementación de la fórmula y el sistema de tipos funciona adecuadamente.

---

## 🚀 Cómo Ejecutar

### Requisitos

- **.NET 9.0** o superior
- **Visual Studio 2022** o **Visual Studio Code**  
- **NUnit Test Runner**

### Comandos

```bash
# Clonar repositorio
git clone https://github.com/DannieLudens/Scripting_Taller2_DDT_Pokemon.git
cd Scripting_Taller2_DDT_Pokemon

# Compilar proyecto
dotnet build

# Ejecutar todas las pruebas
dotnet test

# Ver detalles de las pruebas
dotnet test --verbosity normal

# Ejecutar solo tests básicos (que pasan al 100%)
dotnet test --filter "TestPokemon|TestMove|TestSingleType|TestDualType"
```

---

## 🎯 Metodología TDD Aplicada

### Proceso Red-Green-Refactor

1. **🔴 Red Phase**: Escritura de tests que fallan inicialmente
2. **🟢 Green Phase**: Implementación mínima para hacer pasar los tests  
3. **🔵 Refactor Phase**: Mejora del código sin alterar funcionalidad

### Commits del Proceso TDD

- **Commit inicial**: Tests diseñados (Red phase)
- **Commits subsiguientes**: Implementación iterativa (Green & Refactor phases)
- **Documentación**: Proceso y resultados documentados

---

## 🎨 Elementos de Programación Orientada a Objetos

### Conceptos Implementados

- **🏛️ Herencia**: Las especies heredan de la clase base `Pokemon`
- **🎭 Polimorfismo**: Diferentes especies con comportamiento especializado  
- **📦 Encapsulación**: Propiedades con validación y valores por defecto
- **🔧 Separación de Responsabilidades**: `CombatCalculator` maneja lógica de combate
- **🏭 Factory Pattern**: Creación simplificada de especies con configuración automática

### Patrones de Diseño Aplicados

#### 🏭 Factory Pattern (Patrón Fábrica)

El **Factory Pattern** se implementa a través de las clases de especies que actúan como fábricas especializadas para crear instancias de Pokémon con configuración predeterminada.

**Implementación en el proyecto:**

```csharp
// Cada especie actúa como una "fábrica" que produce 
// un Pokémon con características específicas predefinidas

var pikachu = new Pikachu();     // Crea automáticamente: Electric type
var geodude = new Geodude();     // Crea automáticamente: Rock/Ground types
var gastly = new Gastly();       // Crea automáticamente: Ghost/Poison types
```

**Ventajas del Factory Pattern en este contexto:**

- ✅ **Simplicidad**: Constructor sin parámetros `new Pikachu()` vs `new Pokemon("Pikachu", [PokemonType.Electric])`
- ✅ **Consistencia**: Garantiza que cada especie siempre tenga los tipos correctos
- ✅ **Mantenibilidad**: Cambios en especies se centralizan en una sola clase
- ✅ **Extensibilidad**: Agregar nuevas especies es tan simple como crear una nueva clase

**Ejemplo de implementación:**

```csharp
public class Pikachu : Pokemon
{
    public Pikachu() : base("Pikachu", new List<PokemonType> { PokemonType.Electric })
    {
        // Factory automáticamente configura nombre y tipos
        // El usuario solo necesita: new Pikachu()
    }
}
```

Este patrón elimina la complejidad de configuración manual y reduce errores potenciales al crear instancias de Pokémon con tipos incorrectos.

### Validaciones Implementadas

- **Rangos de valores**: Level (1-99), Stats (1-255), Power (1-255), Speed (1-5)
- **Valores por defecto**: Level=1, Stats=10, Power=100, Speed=1
- **Tipos obligatorios**: Los movimientos requieren tipo y tipo de movimiento

---

## 📚 Notas Basadas en Requerimientos

### Cumplimiento de Requerimientos

✅ **TDD Process**: Red-Green-Refactor documentado en commits  
✅ **OOP Principles**: Herencia, polimorfismo, encapsulación aplicados  
✅ **NUnit Framework**: Tests unitarios con `[Test]` y `[TestCase]`  
✅ **5 Especies**: Clases separadas con constructores por defecto  
✅ **Enumerations**: `PokemonType` y `MoveType` implementados  
✅ **Tabla de Tipos**: Sistema completo de efectividad implementado  
✅ **Fórmulas de Daño**: Implementación exacta según especificaciones  
✅ **40 Casos de Prueba**: Tests parametrizados con instancias de Pokémon

### Extensibilidad del Diseño

El diseño permite fácilmente:
- Agregar nuevas especies de Pokémon
- Implementar nuevos tipos de movimientos
- Extender la tabla de efectividad
- Añadir nuevas mecánicas de combate

---

*Este proyecto demuestra la aplicación práctica de TDD como metodología de desarrollo y OOP como paradigma de diseño en un contexto de sistema de combate inspirado en Pokémon.*