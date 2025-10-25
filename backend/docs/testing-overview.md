# Testing Overview

This document centralizes detailed testing practices, conventions, and future improvements for the backend solution.

## 1. Scope & Goals
Provides:
- Layer-aligned testing guidelines (Domain, Application, Infrastructure, API unit scope)
- Patterns for handlers and repositories
- Naming, structure, and data seeding conventions
- API unit test scenario matrices
- Coverage targets and improvement backlog

## 2. Current Test Project Overview
Project: `tests/lrembecki.obsluga-it.unit-tests`

Frameworks & Tooling:
- Test framework: xUnit (`xunit`, `xunit.runner.visualstudio`)
- Coverage: `coverlet.collector`
- In-memory database: `Microsoft.EntityFrameworkCore.InMemory` (repository tests)
- Target framework: .NET 8 (`net8.0`)

Project References:
- Application layer (`lrembecki.obsluga-it.application`)
- Domain layer (`lrembecki.obsluga-it.domain`)
- Infrastructure layer (`lrembecki.obsluga-it.infrastructure`)

## 3. Existing Test Structure
```
unit-tests/
├── Application/
│   └── GetSubscriptionsHandlerTests.cs
└── Infrastructure/
    └── Repositories/
        ├── SubscriptionRepositoryTests.cs
        ├── SubscriptionUserRepositoryTests.cs
        └── UserRepositoryTests.cs
```

## 4. Observed Conventions
- File naming: `<TargetClassName>Tests.cs`
- Class naming: `<TargetClassName>Tests`
- One test class per concrete handler or repository implementation
- Local static EF InMemory context factory per repository test class
- Inline seeding; no shared fixtures yet
- `[Fact]` only (no parameterized `[Theory]` usage yet)
- Direct `Assert.*` (FluentAssertions not yet adopted)
- Isolation via unique database names (GUID) per test

## 5. Recommended Enhancements (Incremental)
1. Shared utilities:
   - `TestDbContextFactory`
   - `Builders` (e.g. `UserBuilder`, `SubscriptionBuilder`)
2. Adopt `[Theory]` + `InlineData` / `MemberData` for boundary & negative tests
3. (Optional) Introduce FluentAssertions for expressive assertions
4. Add Domain tests (value object validation, invariants, equality semantics)
5. Add failure-path tests for handlers (null returns, exceptions, empty collections)
6. Mapping contract tests once DTO mappers emerge

## 6. Layer-Specific Guidelines
| Layer | What to Test | How | Notes |
|-------|--------------|-----|-------|
| Domain | Invariants, factory methods (`Create`), value object equality, state transitions | Pure in-memory | No mocks; deterministic business rules only |
| Application | Command/query handlers orchestration | Fakes or simple mocks | Keep fakes lean (avoid re-implementing logic) |
| Infrastructure | EF queries, mappings, includes, constraints | EF InMemory w/ isolated DB | Assert navigation hydration & mapping correctness |
| API (Unit Scope) | Minimal controller behavior: branching & exception mapping | Direct controller invocation | Routing/auth in integration layer |

## 7. Test Data Patterns
- Use domain factory methods (e.g. `Subscription.Create(...)`) to avoid duplication
- Local tuple helpers for related seeding: `(user, subscription, link)`
- Introduce builders for complex aggregates when repetition grows

## 8. Example Patterns
Handler test pattern:
```csharp
private sealed class FakeSubscriptionRepository : ISubscriptionRepository {
    private readonly List<SubscriptionVM> _items = new();
    public FakeSubscriptionRepository(IEnumerable<SubscriptionVM>? seed = null) { if (seed != null) _items.AddRange(seed); }
    public Task<List<SubscriptionVM>> GetAllAsync() => Task.FromResult(_items.ToList());
    public Task<Subscription?> GetByIdAsync(Guid id) => Task.FromResult<Subscription?>(null);
}

[Fact]
public async Task HandleAsync_ReturnsAllSubscriptions() { /* Arrange-Act-Assert */ }
```
Repository test pattern:
```csharp
private static ApplicationDbContext CreateContext() =>
    new(new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .EnableSensitiveDataLogging()
        .Options);

[Fact]
public async Task GetAllAsync_ReturnsMappedViewModels() { /* Seed -> Act -> Assert */ }
```

## 9. API Unit Testing Contract
Controller unit tests focus on:
- Delegating to application layer
- Translating results to response envelope (`success`, `data`, `error`)
- Mapping domain/application exceptions to appropriate HTTP status codes

Do NOT unit test:
- Routing
- Auth middleware
- Framework model binding (unless custom)

### Sample Endpoint Scenario Matrix (POST /api/users)
| Scenario | Expected | Strategy |
|----------|----------|----------|
| Valid request | 201 + user payload | Mock handler success |
| Duplicate email | 409 + error | Throw domain ConflictException from handler |
| Validation error | 400 + details | Simulate validator failure / invalid model state |
| Unexpected exception | 500 | Force unexpected handler exception |
| Unauthorized | 401 | Covered in integration tests |

Replicate pattern for Bookings, Services, Auth endpoints.

## 10. Assertion Template Example
```csharp
var response = await controller.CreateUser(request);
var created = Assert.IsType<CreatedResult>(response.Result);
var body = Assert.IsType<ApiResponse<UserDto>>(created.Value);
Assert.True(body.Success);
Assert.NotEqual(Guid.Empty, body.Data.Id);
```

## 11. Naming & Structure Recommendations
Proposed structure evolution:
```
unit-tests/
├── Domain/
├── Application/
├── Infrastructure/
└── Api/
```
- Keep test classes < 300 lines
- Consistent AAA regions or comments
- One primary behavior per test (allow grouped asserts when cohesive)

## 12. Edge Case Checklist (Per Handler / Repository)
- Empty result set
- Single item
- Multiple items (ordering if relevant)
- Not found (null)
- Conflict / duplicate
- Exception translation / propagation

## 13. Coverage Targets
- Initial: 70% line
- Medium-term: 85% line, 70% branch
- Critical flows (auth, pricing, booking state): 95%+ branch

## 14. Adding New Tests – Quick Start
1. Create file in correct layer directory
2. Reuse builders/factories (add if missing)
3. Keep Arrange minimal (focus only on scenario-specific data)
4. Avoid over-stubbing (prefer fakes with in-memory lists)
5. Run locally:
```powershell
dotnet test --collect:"XPlat Code Coverage"
```

## 15. Future Improvements (Backlog)
- Custom `WebApplicationFactory` for richer integration tests
- Snapshot testing for stable JSON envelopes
- Mutation testing (Stryker.NET)
- BenchmarkDotNet for performance-critical domain logic
- Introduce parameterized test data sources for combinatorial cases

## 16. Change Management
- Any addition to shared test utilities should include a brief README snippet or XML doc comment
- Maintain deterministic seeds only where assertion clarity demands it

---
This document ensures consistent, scalable test quality aligned with Clean Architecture boundaries.
