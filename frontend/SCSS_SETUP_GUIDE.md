# SCSS Configuration Guide

## ✅ Projekt został pomyślnie skonfigurowany do pracy z SCSS!

### Co zostało zmienione:

1. **Angular CLI Configuration (`angular.json`)**:
   - Dodano `"style": "scss"` do schematów komponentów
   - Zmieniono ścieżki z `styles.css` na `styles.scss`
   - Zaktualizowano konfigurację dla build i test targets

2. **Service Worker Configuration (`ngsw-config.json`)**:
   - Dodano obsługę plików `*.scss` w cache

3. **Dependencies**:
   - Zainstalowano pakiet `sass` jako dev dependency

4. **File Structure**:
   - Przemianowano `src/styles.css` na `src/styles.scss`
   - Utworzono folder `src/styles/` z modularną strukturą:
     - `_variables.scss` - zmienne globalne
     - `_mixins.scss` - mixiny do reużycia

### Korzyści z używania SCSS:

#### 1. **Zmienne**
```scss
// Definiowanie zmiennych
$primary-color: #007bff;
$spacing-unit: 1rem;

// Używanie w stylach
.button {
  background-color: $primary-color;
  margin: $spacing-unit;
}
```

#### 2. **Mixiny**
```scss
// Definiowanie mixina
@mixin button-variant($bg-color, $text-color: white) {
  background-color: $bg-color;
  color: $text-color;
  &:hover {
    background-color: darken($bg-color, 10%);
  }
}

// Używanie mixina
.btn-primary {
  @include button-variant($primary-color);
}
```

#### 3. **Nesting**
```scss
.navbar {
  background: white;
  
  .nav-item {
    padding: 1rem;
    
    &:hover {
      background: #f5f5f5;
    }
    
    &.active {
      font-weight: bold;
    }
  }
}
```

#### 4. **Functions i loops**
```scss
// Generowanie utility classes
@for $i from 1 through 5 {
  .mb-#{$i} {
    margin-bottom: $spacing-unit * $i;
  }
}
```

#### 5. **Importowanie modułów**
```scss
@import "styles/variables";
@import "styles/mixins";
```

### Jak tworzyć nowe komponenty z SCSS:

```bash
ng generate component my-component --style=scss
```

lub jeśli masz już skonfigurowane schematy (co zrobiliśmy), po prostu:

```bash
ng generate component my-component
```

### Dostępne utility classes:

- **Margins**: `.mb-1`, `.mb-2`, `.mb-3`, `.mb-4`, `.mb-5`
- **Margins top**: `.mt-1`, `.mt-2`, `.mt-3`, `.mt-4`, `.mt-5`
- **Padding**: `.p-1`, `.p-2`, `.p-3`, `.p-4`, `.p-5`
- **Buttons**: `.btn`, `.btn-primary`, `.btn-secondary`, `.btn-success`, `.btn-danger`
- **Cards**: `.card`, `.card-lg`
- **Text**: `.text-truncate`

### Przykład użycia w komponencie:

```typescript
@Component({
  selector: 'app-example',
  template: `
    <div class="card">
      <h2 class="mb-2">Title</h2>
      <p class="text-truncate">Long text that will be truncated...</p>
      <button class="btn btn-primary mt-3">Action</button>
    </div>
  `,
  styles: [`
    .card {
      max-width: 400px;
      
      h2 {
        color: $primary-color; // Używaj zmiennych globalnych
      }
    }
  `]
})
export class ExampleComponent { }
```

### Struktura plików stylów:

```
src/
├── styles.scss (główny plik stylów)
└── styles/
    ├── _variables.scss (zmienne globalne)
    └── _mixins.scss (mixiny do reużycia)
```

### Następne kroki:

1. **Rozbudowa zmiennych**: Dodaj więcej zmiennych do `_variables.scss`
2. **Więcej mixinów**: Stwórz dodatkowe mixiny w `_mixins.scss`
3. **Komponenty**: Twórz komponenty używając SCSS
4. **Responsywność**: Wykorzystaj mixiny do responsive design
5. **Tematyzacja**: Użyj zmiennych do łatwej zmiany kolorów

### Przydatne komendy:

```bash
# Uruchomienie serwera deweloperskiego
npm start

# Build projektu
npm run build

# Linting
npm run lint
```

Teraz możesz w pełni korzystać z możliwości SCSS w swoim projekcie Angular! 🎉
